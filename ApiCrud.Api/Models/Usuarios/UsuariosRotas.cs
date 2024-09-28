using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Usuario
{
    public static class UsuariosRotas
    {
        [HttpPost]
        [AllowAnonymous]
        public static void AddRotasUsuarios(this WebApplication app)
        {
            var rotasusuarios = app.MapGroup("Usuarios");
            
            // POST Usuario
            rotasusuarios.MapPost("register", async (AddUsuariosRequest request, AppDbContext context, CancellationToken ct) => {
            var jaExiste  = await context.Usuarios.AnyAsync(Usuario => Usuario.Email == request.Email, ct);

            if(jaExiste)
                return Results.Conflict("Já existe!");

            var senhaHash = UsuarioService.HashPassword(request.SenhaHash);
    
            var novoUsuario = new UsuarioModel(
                request.Nome,
                request.Email,
                senhaHash,
                request.Papel,
                DateTime.Now
            );

            context.Usuarios.Add(novoUsuario);
            await context.SaveChangesAsync(ct);
            
            // Retornando o DTO sem o senhaHash
            var usuarioRetorno = new UsuarioDTO(novoUsuario.Nome, novoUsuario.Papel, novoUsuario.SenhaHash);
            return Results.Ok(usuarioRetorno);
        });

            //GET Usuario
            rotasusuarios.MapGet("{id}", async (int id, AppDbContext context, CancellationToken ct) => {
                var usuario = await context.Usuarios.FindAsync(id);
                return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
            });

            //Update Usuario
            rotasusuarios.MapPut("{id}", async (int id, UpdateUsuariosRequest request, AppDbContext context, CancellationToken ct) => 
            {
                var usuario = await context.Usuarios.SingleOrDefaultAsync(u => u.IdUsuarios == id, ct);

                if (usuario == null)
                    return Results.NotFound();

                
                usuario.Nome = request.nome;
                usuario.Papel = request.Papel; 

                
                if (!string.IsNullOrWhiteSpace(request.SenhaHash))
                {
                    usuario.SenhaHash = UsuarioService.HashPassword(request.SenhaHash); // Hash a nova senha
                }

                await context.SaveChangesAsync(ct);
                return Results.Ok(new UsuarioDTO(usuario.Nome, usuario.Papel, usuario.SenhaHash)); // Retorne o DTO atualizado
            });


            //Delete
            rotasusuarios.MapDelete("{id}", async (int id, AppDbContext context, CancellationToken ct) =>
            {
                var usuario = await context.Usuarios.SingleOrDefaultAsync(usuario => usuario.IdUsuarios == id, ct);

                if(usuario == null)
                    return Results.NotFound();

                //usuario.Desativar();

                await context.SaveChangesAsync(ct);
                return Results.Ok();
            });
        }
    }
}
