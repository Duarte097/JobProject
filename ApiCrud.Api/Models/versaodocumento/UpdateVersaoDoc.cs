namespace ApiCrud.VersaoDoc;

public record UpdateVersaoDoc
(
    string Numeroversao,
    string CaminhoVersaoArquivo,
    DateTime Dataversao,
    string Descricao 
);