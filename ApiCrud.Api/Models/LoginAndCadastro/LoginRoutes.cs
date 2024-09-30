using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiCrud.Data;
using ApiCrud.Models;
using ApiCrud.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiCrud.Usuario;

public static class LoginRotas
{
    [HttpPost]
    //[Route("login")]
    [AllowAnonymous]
    public static void AddRotasLogin(this WebApplication app)
    {
        var rotasLogin = app.MapGroup("Usuarios");

     
        rotasLogin.MapPost("login", async (AddLoginRequest request, AppDbContext context, IConfiguration configuration) =>
        {
          
            var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usuario == null)
            {
                return Results.NotFound("Usuário não encontrado");
            }


            if (usuario.SenhaHash == null || !UsuarioService.VerificarSenha(request.SenhaHash, usuario.SenhaHash))
            {
                return Results.Unauthorized();
            }


            var token = TokenService.GenerateToken(request, configuration);
            usuario.SenhaHash = "";

            return Results.Ok(new { user = usuario, Token = token, usuarioId = usuario.IdUsuarios });
        });

        rotasLogin.MapGet("anonymous", [AllowAnonymous](HttpContext httpContext) => 
        {
            return "Anônimo";
        });

        rotasLogin.MapGet("authenticated", [Authorize](HttpContext httpContext) => 
        {
            return $"Autenticado - {httpContext.User.Identity?.Name}";
        });

        rotasLogin.MapGet("employee", [Authorize(Roles = "employee, manager")](HttpContext httpContext) => 
        {
            return "Funcionário";
        });

        rotasLogin.MapGet("manager", [Authorize(Roles = "manager")](HttpContext httpContext) => 
        {
            return "Gerente";
        });
    }
}