using System;

namespace ApiCrud.Usuario
{
    public record AddUsuariosRequest(
        string Nome,
        string Email,
        string SenhaHash,
        string Papel
    );
}
