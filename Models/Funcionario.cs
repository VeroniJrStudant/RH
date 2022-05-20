using autenticacao.Enums;

namespace autenticacao.Models
{
  public class Funcionario
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public Permissions Permissions { get; set; }
  }
}