using System.Collections.Generic;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;

/*
класс для CRUD User с применением паттерна Репозиторий и EF (Entity Framework)
*/

namespace Robots.Domain.Concrete
{
  public class EFUserRepository : IUserRepository
  {
    private EFDbContextUser context = new EFDbContextUser();

    public IEnumerable<User> Users
    {
      get { return context.Users; }
    }

    public void SaveUser(User User)
    {
      if (User.UserID == 0)
      {
        context.Users.Add(User);
      }
      else
      {
        User dbEntry = context.Users.Find(User.UserID);
        if (dbEntry != null)
        {
          dbEntry.Name = User.Name;
          dbEntry.Password = User.Password;
          dbEntry.Enabled = User.Enabled;
        }
      }
      context.SaveChanges();
    }

    public User DeleteUser(int UserID)
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
