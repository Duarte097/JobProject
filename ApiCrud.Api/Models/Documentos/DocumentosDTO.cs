namespace ApiCrud.Documentos;

public record DocumentosDTO
(
    int idDocumento,
    string Nome,
    string Descricao,
    //enum Status,
    string Versaoatual,
    string Categoria
);