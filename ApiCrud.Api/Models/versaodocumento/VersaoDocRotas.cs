
using ApiCrud.Api.Models;
using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

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

        //Update 
        rotasversaodoc.MapPut("{id}", async (int id, UpdateVersaoDoc request, AppDbContext context, CancellationToken ct) => 
        {
            var versaodoc = await context.VersaoDoc.SingleOrDefaultAsync(versaodoc => versaodoc.IdVersaodocumento == id, ct);

            if(versaodoc == null)
                return Results.NotFound();
            
            versaodoc.Numeroversao =  request.Numeroversao;
            //versaodoc.Dataversao = request.Dataversao;
            versaodoc.Descricao =  request.Descricao;

            await context.SaveChangesAsync(ct);
            return Results.Ok(new VersaoDocDTO(versaodoc.Numeroversao, versaodoc.Descricao));
        });

        //Delete
        rotasversaodoc.MapDelete("{id}", async (int id, AppDbContext context, CancellationToken ct) =>
        {
            var versaodoc = await context.VersaoDoc.SingleOrDefaultAsync(versaodoc => versaodoc.IdVersaodocumento == id, ct);

            if(versaodoc == null)
                return Results.NotFound();

            //versaodoc.Desativar();

            await context.SaveChangesAsync(ct);
            return Results.Ok();
        });
    }
}