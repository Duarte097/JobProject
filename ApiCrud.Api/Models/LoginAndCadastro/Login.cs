
namespace ApiCrud.Usuario;

public record LoginRequest(
    string Email,
    string SenhaHash
);
