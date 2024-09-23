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
            var usuarioRetorno = new UsuarioDTO(novoUsuario.IdUsuarios, novoUsuario.Nome, novoUsuario.Papel);
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
                var usuario = await context.Usuarios.SingleOrDefaultAsync(usuario => usuario.IdUsuarios == id, ct);

                if(usuario == null)
                    return Results.NotFound();
                
                usuario.Nome =  request.nome;
                usuario.SenhaHash = request.SenhaHash;
                usuario.Papel =  request.Papel;

                await context.SaveChangesAsync(ct);
                return Results.Ok(new UsuarioDTO(usuario.IdUsuarios, usuario.Nome, usuario.Papel));
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
