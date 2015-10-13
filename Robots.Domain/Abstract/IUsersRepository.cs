using System.Collections.Generic;
using Robots.Domain.Entities;

/*
Используем паттерн репозиторий, чтобы скрыть как мы храним и откуда берем данные.
Реализация интерфейса будет получена от Ninject
*/

namespace Robots.Domain.Abstract
{
  public interface IUserRepository
  {
    IEnumerable<User> Users { get; }
    void SaveUser(User User);
    User DeleteUser(int UserID);
  }
}
