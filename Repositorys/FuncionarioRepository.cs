using System.Runtime.InteropServices.ComTypes;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using autenticacao.Models;
using autenticacao.Enums;

namespace autenticacao.Repositorys
{

  public static class FuncionarioRepository
  {
    private static readonly List<Funcionario> colaboradores = new List<Funcionario>()
    {
        new Funcionario
        {
            Id = 1,
            Nome = "Zezinho",
            Salario = 12000,
            Senha = "aaa",
            Permissao = Enums.Permissoes.Administrador
        }
    };
    private static int _id = 1;

    public static List<Funcionario> Obter()
    {
      return colaboradores;
    }

    public static Funcionario ObterPorUsuarioESenha(string username, string password)
    {
      return colaboradores.FirstOrDefault(x => x.Nome.ToLower() == username.ToLower() && x.Senha == password);
    }

    public static void Adicionar(string nome, string senha, decimal Salario, Permissoes permissao)
    {
      colaboradores.Add(new Funcionario
      {
        Id = _id++,
        Nome = nome,
        Senha = senha,
        Salario = Salario,
        Permissao = permissao
      });
    }

    public static void Adicionar(Funcionario geradorDeId)
    {
      geradorDeId.Id = _id++;
      colaboradores.Add(geradorDeId);
    }

    public static void Editar(Funcionario geradorDeId, int id)
    {
      Funcionario funcionario = colaboradores.Find(x => x.Id == id);
      funcionario.Nome = geradorDeId.Nome;
      funcionario.Senha = geradorDeId.Senha;
      funcionario.Salario = geradorDeId.Salario;
      funcionario.Permissao = geradorDeId.Permissao;
    }

    public static void deletar(Funcionario geradorDeId)
    {
      colaboradores.Remove(geradorDeId);
    }

  }

}