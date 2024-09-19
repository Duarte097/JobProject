namespace ApiCrud.VersaoDoc;

public record AddVersaoDocRequest(
    string NumeroVersao,
    int DocumentoId,
    string CaminhoVersaoArquivo,
    string Descricao
);
