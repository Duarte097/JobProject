
using ApiCrud.Api.Models;
using ApiCrud.Data;

namespace ApiCrud.VersaoDoc;

public static class VersaoDoc{
    public static void AddRotasVersaoDoc(this WebApplication app)
    {
        var rotasversaodoc= app.MapGroup("versaodocumento");
        // POST
        rotasversaodoc.MapPost("", async (AddVersaoDocRequest request, AppDbContext context) => {
            var novaVersaoDoc= new Versaodocumento(
                request.NumeroVersao,
                request.DocumentoId,
                DateTime.Now,
                request.CaminhoVersaoArquivo,
                request.Descricao
            );
            context.VersaoDoc.Add(novaVersaoDoc); 
            await context.SaveChangesAsync();
            return Results.Created($"/versaodocumento/{novaVersaoDoc.IdVersaodocumento}", novaVersaoDoc);
        });

            // GET
            rotasversaodoc.MapGet("{id}", async (int id, AppDbContext context) => {
                var versaodocumento = await context.VersaoDoc.FindAsync(id);
                return versaodocumento is not null ? Results.Ok(versaodocumento) : Results.NotFound();
            });
    }
}