using System;
using System.Collections.Generic;
using System.Linq;
using autenticacao.Models;

namespace autenticacao.Repositorys
{

  public static class FuncionarioRepository
  {
    private static List<Funcionario> colaboradores = new List<Funcionario>()
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
    public static Funcionario VerificarUsuarioESenha(string username, string password)
    {
      return colaboradores.Where(x => x.Nome.ToLower() == username.ToLower() && x.Senha == password)
      .FirstOrDefault();
    }

    public static void Adicionar(Funcionario funcionario)
    {
      colaboradores.Add(funcionario);
    }

    public static List<Funcionario> ListarFuncionario()
    {
      return colaboradores;
    }



  }

}