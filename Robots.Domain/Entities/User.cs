/*
Сущность Пользователь.
Взял из RD, хз зачем Enabled.
Для хранения и получения User использован паттерн Репозиторий и Ninject
Атрибуты указаны для View регистрации чтобы удобно создать формочку.
*/

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Robots.Domain.Entities
{
  public class User
  {
    [HiddenInput(DisplayValue = false)]
    public int UserID { get; set; }
    [Required(ErrorMessage = "Please enter a name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; }
    [HiddenInput(DisplayValue = false)]
    public bool Enabled { get; set; }

  }
}
