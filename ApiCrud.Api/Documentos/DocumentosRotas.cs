using ApiCrud.Models; 

namespace ApiCrud.Documentos
{
    public static class DocumentosRotas
    {
        public static void AddRotasDocumentos(this WebApplication app)
        {
            app.MapGet("documento", () => new Documento(1, "Leonardo", "teste", new DateTime(1997, 12, 22), "teste", "1.0", "Contrato"));
        }
    }
}
