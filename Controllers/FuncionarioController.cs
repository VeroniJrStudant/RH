using System.Linq;
using Microsoft.AspNetCore.Mvc;
using autenticacao.Repositorys;
using autenticacao.Models;
using Microsoft.AspNetCore.Authorization;
using autenticacao.Enums;
using Microsoft.OpenApi.Extensions;
using DTO;

namespace autenticacao.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FuncionarioController : ControllerBase
  {

    [HttpPost]
    [Route("cadastrar-novo-funcionario")]
    [Authorize(Roles = "Administrador")]
    public IActionResult InserirColaborador([FromBody] FuncionarioDTO cadastrar)
    {
      FuncionarioRepository.Adicionar(new Funcionario
      {
        Nome = cadastrar.Nome,
        Salario = cadastrar.Salario,
        Senha = cadastrar.Senha,
        Permissao = (Permissoes)cadastrar.Permissao,
      });
      return Created("", cadastrar);
    }

    [HttpDelete]
    [Route("excluir-funcionario/{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    public IActionResult ExcluirFuncionario([FromRoute] int id)
    {
      var excluir = FuncionarioRepository.Obter().Find(x => x.Id == id);
      if (excluir.Permissao == Enums.Permissoes.Gerente)
      {
        FuncionarioRepository.deletar(excluir);
        return Ok();
      }
      if (excluir == null)
        return NotFound("Funcionário não encontrado");

      return BadRequest($"Sem permissão de {excluir.PermissaoNome} para excluir este funcionário");
    }

    [HttpDelete]
    [Route("excluir-gerente/{id}")]
    [Authorize(Roles = "Administrador")]
    public IActionResult ExcluirGerente([FromRoute] int id)
    {
      var apagarGerente = FuncionarioRepository.Obter().Find(x => x.Id == id);
      if (apagarGerente.Permissao == Enums.Permissoes.Gerente)
      {
        FuncionarioRepository.deletar(apagarGerente);
        return Ok();
      }
      if (apagarGerente == null)
        return NotFound("Funcionario não encontrado");
      return BadRequest($"Usuário possui permissão de {apagarGerente.PermissaoNome}, verifique seu usuário.");
    }

    [HttpPatch]
    [Route("alterar-salario")]
    [Authorize]
    public IActionResult AlterarSalario([FromBody] Funcionario modificar)
    {
      FuncionarioRepository.Editar(modificar, modificar.Id);
      return Ok();
    }

    [Route("listar")]
    [Authorize]
    [HttpGet]
    public IActionResult Listar()
      => User.IsInRole(Permissoes.Funcionario.GetDisplayName())
        ? Ok(FuncionarioRepository.Obter().Select(x => new { x.Nome, x.PermissaoNome }))
        : Ok(FuncionarioRepository.Obter());
  }
}