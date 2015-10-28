/*
Сущность Пользователь.
Атрибуты указаны для View регистрации чтобы удобно создать формочку.
*/

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc;

namespace Roboto.Models.Entities
{
  public class User
  {
    [HiddenInput(DisplayValue = false)]
    public int UserID { get; set; }
    [Required(ErrorMessage = "Please enter a name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; }
  }
}
