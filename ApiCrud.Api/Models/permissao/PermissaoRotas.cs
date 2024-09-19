using ApiCrud.Data;
using ApiCrud.Models;

namespace ApiCrud.Permisao
{
    public static class PermissaoRotas{
        public static void AddRotasPermissao(this WebApplication app){
            var rotaspermissao = app.MapGroup("permissao");
            
            // POST
            rotaspermissao.MapPost("", async (AddPermissaoRequest request, AppDbContext context) => {
                var novaPermissao = new Permissao(
                    request.Permissaotipo,
                    request.Usuarioid,
                    request.Documentoid

                );
                context.Permissao.Add(novaPermissao); 
                await context.SaveChangesAsync();
                return Results.Created($"/permissao/{novaPermissao.IdPermissao}", novaPermissao);
            });

            //GET
            rotaspermissao.MapGet("{id}", async (int id, AppDbContext context) => {
                var permissao= await context.Permissao.FindAsync(id);
                return permissao is not null ? Results.Ok(permissao) : Results.NotFound();
            });
        }
    }

}