using ApiCrud.Models;

namespace ApiCrud.Documentos;

public record DocumentosDTO
(
    int idDocumento,
    string Nome,
    string Descricao, 
    Status Status,
    string Versaoatual,
    string Categoria
);