namespace ApiCrud.Documentos;

public record UpdateDocumentos
(
    string Nome,
    string Descricao,
    //enum Status,
    string Caminho,
    string Versaoatual,
    string Categoria
);