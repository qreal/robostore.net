using System.ComponentModel.DataAnnotations;

namespace Roboto.Models.DataModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Please enter a name")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; }
  }
}
