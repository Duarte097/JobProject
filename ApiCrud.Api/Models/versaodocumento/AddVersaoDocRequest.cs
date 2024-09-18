namespace ApiCrud.VersaoDoc;

public record AddVersaoDocRequest(
    string NumeroVersao,
    string CaminhoVersaoArquivo,
    string Descricao
);
