using ApiCrud.Data;
using ApiCrud.Models;
using ApiCrud.Services;
using ApiCrud.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] AddLoginRequest model)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado");
        }

        if (string.IsNullOrEmpty(usuario.SenhaHash) || string.IsNullOrEmpty(model.SenhaHash))
        {
            return Unauthorized("Credenciais inválidas");
        }

        if (!UsuarioService.VerificarSenha(model.SenhaHash, usuario.SenhaHash))
        {
            return Unauthorized("Credenciais inválidas");
        }

        var token = TokenService.GenerateToken(model, _configuration);
        usuario.SenhaHash = "";

        return Ok(new { user = usuario, Token = token });
    }

    [HttpGet]
    [Route("anonymous")]
    [AllowAnonymous]
    public string Anonymous() => "Anônimo";

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public string Authenticated() => $"Autenticado - {User.Identity?.Name}";

    [HttpGet]
    [Route("employee")]
    [Authorize(Roles = "employee, manager")]
    public string Employee() => "Funcionário";

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = "manager")]
    public string Manager() => "Gerente";
}
