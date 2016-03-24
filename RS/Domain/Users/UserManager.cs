using System.Linq;
using System.Threading.Tasks;
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

    public async Task CreateUser(string login, string password)
      => await data.Users.AddAsync(new User {Login = login, Password = password});
  }
}
