using ApiCrud.Models;

namespace ApiCrud.Documentos;

public record DocumentosDTO
(
    string Nome,
    string Descricao, 
    string Categoria,
    string Versaoatual,
    Status Status,
    int UsuarioId
);