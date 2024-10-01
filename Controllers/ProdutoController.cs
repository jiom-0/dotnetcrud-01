using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using Host.Models;

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
        _context.Produtos.Add(novoProduto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetProdutos), new { id = novoProduto.Id }, novoProduto);
    }

    [HttpPut("{id}")]
    public ActionResult<Produto> PutProduto(int id, [FromBody] Produto novoProduto){
        var produto = _context.Produtos.Find(id);
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