
namespace ApiCrud.Usuario
{
    public record AddLoginRequest(
        string Email,
        string SenhaHash,
        int IdUsuarios
    );
}