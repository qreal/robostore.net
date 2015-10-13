using System.ComponentModel.DataAnnotations;

/*
Модель нужна для Account контроллера для работы со входом.
*/

namespace Robots.WebUI.Models
{
  public class LoginViewModel
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
