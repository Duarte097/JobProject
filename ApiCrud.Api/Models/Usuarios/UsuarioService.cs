using BCrypt.Net;

namespace ApiCrud.Usuario;

public class UsuarioService
{
    // Gera um hash seguro para a senha
    public static string HashPassword(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    // Verifica se a senha fornecida corresponde ao hash armazenado
    public static bool VerificarSenha(string senha, string senhaHash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
    }
}
