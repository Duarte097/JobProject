namespace ApiCrud.Usuario;

public record UpdateUsuariosRequest(
    string nome,
    string SenhaHash,
    string Papel
);