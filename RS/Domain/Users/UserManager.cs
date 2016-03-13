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

    public bool TryEnter(string login, string password)
      => data.Users.Count(x => x.Login == login && x.Password == password) == 1;

    public async Task CreateUser(string login, string password)
      => await data.AddAsync(new User {Login = login, Password = password});
  }
}
