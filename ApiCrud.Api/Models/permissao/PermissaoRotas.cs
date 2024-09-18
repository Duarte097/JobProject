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
                    request.Permissaotipo
                );
                context.Permissao.Add(novaPermissao); 
                await context.SaveChangesAsync();
                return Results.Created($"/permissao/{novaPermissao.IdPermissao}", novaPermissao);
            });
        }
    }

}