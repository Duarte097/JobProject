using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

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

            //Update Documento
            rotaspermissao.MapPut("{id}", async (int id, UpdatePermissao request, AppDbContext context, CancellationToken ct) => 
            {
                var permissao = await context.Permissao.SingleOrDefaultAsync(permissao=> permissao.IdPermissao == id, ct);

                if(permissao == null)
                    return Results.NotFound();
                
                permissao.Permissaotipo = request.permissaotipo;

                await context.SaveChangesAsync(ct);
                return Results.Ok(new PermissaoDTO(permissao.Permissaotipo));
            });

            //Delete
            rotaspermissao.MapDelete("{id}", async (int id, AppDbContext context, CancellationToken ct) =>
            {
                var permissao = await context.Permissao.SingleOrDefaultAsync(permissao => permissao.IdPermissao == id, ct);

                if(permissao == null)
                    return Results.NotFound();

                //permissao.Desativar();

                await context.SaveChangesAsync(ct);
                return Results.Ok();
            });
        }
    }

}