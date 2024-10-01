using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;

[ApiController]
// [Route("api/[controller]")]
[Route("api/login")]
public class LoginController : ControllerBase{
    private readonly IConfiguration _configuration;
    private readonly NgdotnetcrudContext _context;

    public LoginController(NgdotnetcrudContext context, IConfiguration configuration){
        _context = context;
        _configuration = configuration;
    }

    [HttpPost]
    public ActionResult<List<Usuario>> Autenticar([FromBody] Login login ){
        var usuario = _context.Usuarios.FirstOrDefault(p => p.Email == login.Email && p.Passwd == login.Passwd);
        
        if (usuario == null) return Unauthorized();
        var token = Roles.GerarToken(usuario.Email, usuario.Permission, _configuration);
        return Ok(new { token });
    }
}