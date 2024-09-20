using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiCrud.Usuario;
using Microsoft.IdentityModel.Tokens;

namespace ApiCrud.Services;
public static class TokenService
{
    public static string GenerateToken(AddLoginRequest user, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        // Obtém a chave do arquivo de configuração
        var key = configuration["Jwt:Key"];
        
        // Verifica se a chave é nula ou vazia e lança uma exceção
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "A chave JWT não está configurada corretamente no appsettings.json.");
        }
        
        var keyBytes = Encoding.ASCII.GetBytes(key);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Hash, user.SenhaHash.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

} 