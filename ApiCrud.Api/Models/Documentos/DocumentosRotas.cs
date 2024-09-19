using ApiCrud.Data;
using ApiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Documentos
{
    public static class DocumentosRotas
    {
        public static void AddRotasDocumentos(this WebApplication app)
        {
            var rotasdocumentos = app.MapGroup("documento");
            
            //POST
            rotasdocumentos.MapPost("", async (AddDocumentosRequest request, AppDbContext context) => {
                var novoDocumento = new Documento(
                  request.Nome,
                  request.Descricao,
                  DateTime.Now,
                  request.Caminho,
                  request.Versao,
                  request.Categoria,
                  request.UsuarioId 
                );
                context.Documentos.Add(novoDocumento);
                await context.SaveChangesAsync();
                //return Results.Created($"/documentos/{novoDocumento.Id}", novoDocumento);
            });

            // Exemplo de GET
            rotasdocumentos.MapGet("{id}", async (int id, AppDbContext context) => {
                var documentos = await context.Documentos.FindAsync(id);
                return documentos is not null ? Results.Ok(documentos) : Results.NotFound();
            });

            //Update Documento
            rotasdocumentos.MapPut("{id}", async (int id, UpdateDocumentos request, AppDbContext context, CancellationToken ct) => 
            {
                var documento = await context.Documentos.SingleOrDefaultAsync(documento => documento.IdDocumento == id, ct);

                if(documento == null)
                    return Results.NotFound();
                
                documento.Nome = request.Nome;
                documento.Descricao =  request.Descricao;
                documento.Caminho = request.Caminho;
                documento.VersaoAtual =  request.Versaoatual;
                documento.Categoria = request.Categoria;

                await context.SaveChangesAsync(ct);
                return Results.Ok(new DocumentosDTO(documento.IdDocumento, documento.Nome, documento.Descricao, documento.VersaoAtual, documento.Categoria));
            });

            //Delete
            rotasdocumentos.MapDelete("{id}", async (int id, AppDbContext context, CancellationToken ct) =>
            {
                var documento = await context.Documentos.SingleOrDefaultAsync(documento => documento.IdDocumento == id, ct);

                if(documento == null)
                    return Results.NotFound();

                documento.Desativar();

                await context.SaveChangesAsync(ct);
                return Results.Ok();
            });
        }
    }
}
