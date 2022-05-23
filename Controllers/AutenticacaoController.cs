using autenticacao.Repositorys;
using autenticacao.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace autenticacao.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AutenticacaoController : ControllerBase
  {
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] User dto)
    {
      var user = FuncionarioRepository.ObterPorUsuarioESenha(dto.Username, dto.Password);

      if (user == null) return NotFound();

      var token = TokenService.GenerateToken(user);
      return Ok(new
      {
        token
      });
    }
  }
}
