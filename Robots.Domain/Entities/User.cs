/*
Сущность Пользователь.
Взял из RD, хз зачем Enabled.
Для хранения и получения User будет использован паттерн Репозиторий и Ninject
*/

namespace Robots.Domain.Entities
{
  public class User
  {
    public int UserID { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public bool Enalbled { get; set; }

  }
}
