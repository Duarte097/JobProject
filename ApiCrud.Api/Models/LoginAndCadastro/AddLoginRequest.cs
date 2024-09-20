
namespace ApiCrud.Usuario
{
    public record AddLoginRequest(
        //int IdUsuarios,
        string Email,
        string SenhaHash
    );
}