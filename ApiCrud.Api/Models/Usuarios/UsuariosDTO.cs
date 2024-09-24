namespace ApiCrud.Usuario;

public record UsuarioDTO
(
    string? Nome,
    string? SenhaHash,
    string? Papel
);