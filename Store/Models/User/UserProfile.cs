using System.ComponentModel.DataAnnotations;

namespace Store.Models.User
{
  public class UserProfile
  {
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
  }
}
