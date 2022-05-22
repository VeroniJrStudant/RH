
using autenticacao.DTOs;
using autenticacao.Repositorys;
using autenticacao.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace autenticacao.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AutenticacaoController : ControllerBase
  {
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginDTO dto)
    {
      var user = FuncionarioRepository.VerificarUsuarioESenha(dto.Username, dto.Password);

      if (user == null) return NotFound();

      var token = TokenService.GenerateToken(user);
      // var newRefleshToken = TokenService.GenerateRefleshToken();
      // TokenService.SaveRefleshToken(user.PermissaoNome, newRefleshToken);

      return Ok(new
      {
        token
      });
    }

    // [HttpPost]
    // [Route("refresh")]
    // [AllowAnonymous]
    // public ActionResult<dynamic> RefreshToken([FromQuery] string token, [FromQuery] string refreshToken)
    // {

    //   var principal = TokenService.GetPrincipalFromExpiredToken(token);
    //   var username = principal.Identity.Name;
    //   var savedRefreshToken = TokenService.GetRefreshToken(username);

    //   if (savedRefreshToken != refreshToken)
    //     throw new SecurityTokenException("Invalid refresh token");

    //   var newToken = TokenService.GenerateToken(principal.Claims);
    //   var newRefreshToken = TokenService.GenerateRefreshToken();
    //   TokenService.DeleteRefreshToken(username, refreshToken);
    //   TokenService.SaveRefreshToken(username, newRefreshToken);

    //   return Ok(new
    //   {
    //     token,
    //     newRefreshToken
    //   });
    // }

    // [HttpPost]
    // [Route("reflesh")]
    // [AllowAnonymous]
    // public ActionResult<dynamic> RefleshToken([FromQuery] string token, [FromQuery] string refleshToken)
    // {
    //   var principal = TokenService.GetPrincipalFromExpiredToken(token);
    //   var username = principal.Identity.Name;
    //   var savedRefreshToken = TokenService.GetRefreshToken(username);

    //   if (savedRefreshToken != refreshToken)
    //     throw new SecurityTokenException("Invalid refresh token");

    //   var newToken = TokenService.GenerateToken(principal.Claims);
    //   var newRefreshToken = TokenService.GenerateRefreshToken();
    //   TokenService.DeleteRefreshToken(username, newRefreshToken);
    //   TokenService.SaveRefreshToken(username, newRefreshToken);

    //   return new ObjectResult(new
    //   {
    //     token = newToken,
    //     refreshToken = newRefreshToken

    //   });
  }
}
