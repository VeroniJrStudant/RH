using Microsoft.AspNetCore.Mvc;
using autenticacao.Repositorys;

namespace autenticacao.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FuncionarioController : ControllerBase
  {
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
      var lista = FuncionarioRepository.ListarFuncionario();
      return Ok(lista);
    }
  }
}