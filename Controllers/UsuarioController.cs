using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
// [Route("api/[controller]")]
[Route("api/usuario")]
[Authorize]
public class UsuarioController : ControllerBase{
    private readonly NgdotnetcrudContext _context;

    public UsuarioController(NgdotnetcrudContext context){
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<Usuario>> GetUsuarios(){
        var usuarios = _context.Usuarios.ToList();
        return Ok(usuarios);
    }
}