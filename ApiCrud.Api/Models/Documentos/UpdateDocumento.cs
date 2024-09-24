using ApiCrud.Models;
namespace ApiCrud.Documentos;

public record UpdateDocumentos
(
    string Nome,
    string Descricao,
    Status Status,
    string Caminho,
    string Versaoatual,
    string Categoria
);