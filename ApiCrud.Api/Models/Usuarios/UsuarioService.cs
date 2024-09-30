using BCrypt.Net;

namespace ApiCrud.Usuario;

public class UsuarioService
{

    public static string HashPassword(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

 
    public static bool VerificarSenha(string senha, string senhaHash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
    }
}
