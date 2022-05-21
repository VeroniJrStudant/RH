using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using autenticacao.Models;
using Microsoft.IdentityModel.Tokens;

namespace autenticacao.Service
{
  public class TokenService
  {
    public static string GenerateToken(Funcionario colaborador)
    {

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(Settings.Secret);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                new Claim(ClaimTypes.Name, colaborador.Nome),
                new Claim(ClaimTypes.Role, colaborador.PermissaoNome),
                new Claim("Projeto", "DevInHouse"),
          }),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}