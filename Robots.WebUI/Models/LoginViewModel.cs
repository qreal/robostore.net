using System.ComponentModel.DataAnnotations;

/*
Модель нужна для Account контроллера для работы со входом.
*/

namespace Robots.WebUI.Models
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Please enter a name")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; }
  }
}
