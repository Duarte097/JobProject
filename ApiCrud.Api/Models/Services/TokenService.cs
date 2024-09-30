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
        
     
        var key = configuration["Jwt:Key"];
        
 
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "A chave JWT não está configurada corretamente no appsettings.json.");
        }
        
        var keyBytes = Encoding.ASCII.GetBytes(key);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUsuarios.ToString()),
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