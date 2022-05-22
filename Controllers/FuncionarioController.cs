using System.Security.Authentication;
using System.Security.AccessControl;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using autenticacao.Repositorys;
using autenticacao.Models;
using Microsoft.AspNetCore.Authorization;

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

    [HttpPost]
    [Route("cadastrar-novo-funcionario")]
    [Authorize]
    public IActionResult InserirColaborador([FromBody] Funcionario cadastrar)
    {
      FuncionarioRepository.Adicionar(cadastrar);
      return Created("", cadastrar);
    }

    [HttpPut]
    [Route("")]
  }
}