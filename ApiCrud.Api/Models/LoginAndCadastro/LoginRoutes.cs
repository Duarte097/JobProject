using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Usuario;
public static class LoginRotas
{
    public static void AddRotasLogin(this WebApplication app)
    {   
        var rotasLogin = app.MapGroup("Usuarios");
        
        // POST 
        rotasLogin.MapPost("/login", async (LoginRequest request, AppDbContext context, IConfiguration configuration) => 
        {
            // Verificar se o e-mail existe no banco de dados
            var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
          
            if (usuario == null)
            {
                return Results.NotFound("Usuário não encontrado");
            }

            // Verificar se a senha está correta
            if (!UsuarioService.VerificarSenha(request.SenhaHash, usuario.SenhaHash))
            {
                return Results.Unauthorized();
            }

            // Se as credenciais forem válidas, gerar o token JWT
            var token = "dd";
            //GerarTokenJWT(usuario, configuration);

            return Results.Ok(new { Token = token });
        });
    }
}

