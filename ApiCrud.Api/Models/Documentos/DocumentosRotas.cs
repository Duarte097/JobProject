using ApiCrud.Data;
using ApiCrud.Models;

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
                  0,
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

            //app.MapGet("documento", () => new Documento(1, "Leonardo", "teste", new DateTime(1997, 12, 22), "teste", "1.0", "Contrato"));
                        // Exemplo de GET
            rotasdocumentos.MapGet("{id}", async (int id, AppDbContext context) => {
                var documentos = await context.Documentos.FindAsync(id);
                return documentos is not null ? Results.Ok(documentos) : Results.NotFound();
            });
        }
    }
}
