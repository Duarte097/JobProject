namespace ApiCrud.Documentos;

public record AddDocumentosRequest(
    string Nome,
    string Descricao,
    string Categoria,
    string Caminho,
    string Versao
);