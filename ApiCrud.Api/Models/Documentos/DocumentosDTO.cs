using ApiCrud.Models;

namespace ApiCrud.Documentos;

public record DocumentosDTO
(
    int idDocumento,
    string Nome,
    string Descricao, 
    string Status,
    string Versaoatual,
    string Categoria,
    int UsuarioId
);