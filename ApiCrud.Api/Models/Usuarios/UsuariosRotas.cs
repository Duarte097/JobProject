using ApiCrud.Data;
using ApiCrud.Models;

namespace ApiCrud.Usuario
{
    public static class UsuariosRotas
    {
        public static void AddRotasUsuarios(this WebApplication app)
        {
            var rotasusuarios = app.MapGroup("usuarios");
            
            // POST
            rotasusuarios.MapPost("", async (AddUsuariosRequest request, AppDbContext context) => {
                var novoUsuario = new UsuarioModel(
                  request.Nome,
                  request.Email,
                  request.SenhaHash,
                  request.Papel,
                  DateTime.Now
                );
                context.Usuarios.Add(novoUsuario); // Certifique-se de que `Usuarios` está definido no AppDbContext
                await context.SaveChangesAsync();
                return Results.Created($"/usuarios/{novoUsuario.IdUsuarios}", novoUsuario);
            });

            //GET
            rotasusuarios.MapGet("{id}", async (int id, AppDbContext context) => {
                var usuario = await context.Usuarios.FindAsync(id);
                return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
            });
        }
    }
}
