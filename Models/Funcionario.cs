using autenticacao.Enums;
using Microsoft.OpenApi.Extensions;

namespace autenticacao.Models
{
  public class Funcionario
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; }
    public decimal Salario { get; set; }
    public Permissoes Permissao { get; set; }
    public string PermissaoNome => Permissao.GetDisplayName();
  }
}