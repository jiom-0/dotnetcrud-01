using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
[ApiController]
// [Route("api/[controller]")]
[Route("api/produto")]
[Authorize]
public class ProdutoController : ControllerBase{
    private readonly NgdotnetcrudContext _context;

    public ProdutoController(NgdotnetcrudContext context){
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<Produto>> GetProdutos(){
        var produtos = _context.Produtos.ToList();
        return Ok(produtos);
    }

    [HttpPost]
    public ActionResult<Produto> PostProduto([FromBody] Produto novoProduto){
        Roles valida = new Roles();
        string validado = Roles.ValidaCamposProduto(novoProduto);

        if(validado != "") return StatusCode(StatusCodes.Status500InternalServerError, $"{validado}");

        _context.Produtos.Add(novoProduto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetProdutos), new { id = novoProduto.Id }, novoProduto);
    }

    [HttpPut("{id}")]
    public ActionResult<Produto> PutProduto(int id, [FromBody] Produto novoProduto){
        var produto = _context.Produtos.Find(id);

        Roles valida = new Roles();
        string validado = Roles.ValidaCamposProduto(novoProduto);

        if(validado != "") return StatusCode(StatusCodes.Status500InternalServerError, $"{validado}");
        
        if (produto == null){
            return NotFound();
        }

        // Só atualiza os campos informados
        PropertyInfo[] atributos = novoProduto.GetType().GetProperties();
        foreach (var atributo in atributos){
            var valor = atributo.GetValue(novoProduto);
            if(atributo.Name == "Id" || valor == null) continue;
            atributo.SetValue(produto, atributo.GetValue(novoProduto));
        }
        
        _context.SaveChanges();
        return Ok();
    }

    [HttpPost("/api/produto/bulk")]
    public ActionResult<IEnumerable<Produto>> PostProdutoBulk([FromBody] List<Produto> produtos){
        if (produtos == null || !produtos.Any()) return BadRequest("A lista de produtos está vazia.");
        foreach (var produto in produtos){
            string validado = Roles.ValidaCamposProduto(produto);
            if(validado != "") return StatusCode(StatusCodes.Status500InternalServerError, $"{validado}");

            var produtoExistente = _context.Produtos.Find(produto.Id);
            if (produtoExistente != null){
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Preco = produto.Preco;
                produtoExistente.Descricao = produto.Descricao;
                produtoExistente.SldAtual = produto.SldAtual;
                produtoExistente.Categoria = produto.Categoria;

                _context.Entry(produtoExistente).State = EntityState.Modified;
            }
            else{
                 _context.Produtos.Add(produto);
            }
        }
          
        try{
            _context.SaveChanges();
            return Ok(produtos.Select(p => p.Id).ToList());
        }
        catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduto(int id){
        var produto = _context.Produtos.Find(id);
        if (produto == null){
            return NotFound();
        }
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }
}