using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
namespace autenticacao.Enums
{
  public enum Permissoes
  {
    [XmlEnumAttribute("Administrador")]
    [Display(Name = "Administrador")]
    Administrador = 0,
    [XmlEnumAttribute("Gerente")]
    [Display(Name = "Gerente")]
    Gerente = 1,
    [XmlEnumAttribute("Funcionario")]
    [Display(Name = "Funcionario")]
    Funcionario = 2,
  }
}
