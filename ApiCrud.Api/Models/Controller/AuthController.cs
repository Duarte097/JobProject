/*using ApiCrud.Data;
using ApiCrud.Models;
using ApiCrud.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastro([FromBody] UsuarioModel model)
    {
        var existingUser = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (existingUser != null)
        {
            return BadRequest("Usuário já registrado com este e-mail.");
        }

        var novoUsuario = new UsuarioModel(
            model.Nome,
            model.Email,
            model.SenhaHash, 
            model.Papel,
            DateTime.Now
        );

        _context.Usuarios.Add(novoUsuario);
        await _context.SaveChangesAsync();

        return Created($"/usuarios/{novoUsuario.IdUsuarios}", novoUsuario);
    }

    /*[HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        // Lógica para autenticar o usuário e gerar um token JWT
    }*/
//}
