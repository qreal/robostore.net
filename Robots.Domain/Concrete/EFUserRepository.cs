using System.Collections.Generic;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;

/*
класс для CRUD User с применением паттерна Репозиторий и EF (Entity Framework)
*/

namespace Robots.Domain.Concrete
{
  public class EFUserRepository : ICommonRepository<User>
  {
    private EFDbContextRobots context = new EFDbContextRobots();

    public IEnumerable<User> Data
    {
      get { return context.Users; }
    }

    public void SaveData(User data)
    {
      if (data.UserID == 0)
      {
        context.Users.Add(data);
      }
      else
      {
        User dbEntry = context.Users.Find(data.UserID);
        if (dbEntry != null)
        {
          dbEntry.Name = data.Name;
          dbEntry.Password = data.Password;
        }
      }
      context.SaveChanges();
    }

    public User DeleteData(int UserID)
    {
      User dbEntry = context.Users.Find(UserID);
      if (dbEntry != null)
      {
        context.Users.Remove(dbEntry);
        context.SaveChanges();
      }
      return dbEntry;
    }
  }
}
