using System.Linq;
using Domain.Data;
using Domain.Entities;

namespace Domain.Users
{
  public class UserManager
  {
    private IData data;

    public UserManager(IData d)
    {
      data = d;
    }

    public User TryEnter(string login, string password)
      => data.Users.Data.FirstOrDefault(x => x.Login == login && x.Password == password);

    public void CreateUser(string login, string password)
      => data.Users.Add(new User {Login = login, Password = password});
  }
}
