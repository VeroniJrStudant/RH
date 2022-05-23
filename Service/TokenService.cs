using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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


    public static string GenerateRefreshToken()
    {
      var randomNumber = new byte[32];
      using var rng = RandomNumberGenerator.Create();
      rng.GetBytes(randomNumber);
      return Convert.ToBase64String(randomNumber);
    }


    public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
        ValidateIssuer = false,
        ValidateAudience = false,
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var secutiryToken);

      if (secutiryToken is not JwtSecurityToken jwtSecurityToken)
        throw new SecurityTokenException("Invalid token");

      return principal;

    }
    

    private static List<Tuple<string, string>> _refreshsTokens = new List<Tuple<string, string>>();

    public static void SaveRefreshToken(string username, string refreshToken)
      => _refreshsTokens.Add(new Tuple<string, string>(username, refreshToken));

    public static string GetRefreshToken(string username)
        => _refreshsTokens.FirstOrDefault(x => x.Item1 == username).Item2;

    public static void DeleteRefreshToken(string username, string refreshToken)
    {
      var item = _refreshsTokens.FirstOrDefault(x => x.Item1 == username && x.Item2 == refreshToken);
      _refreshsTokens.Remove(item);
    }
  }
}