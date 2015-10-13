using System.Linq;
using System.Web.Security;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;
using Robots.WebUI.Infrastructure.Abstract;

namespace Robots.WebUI.Infrastructure.Concrete
{
  public class FormsAuthProvider : IAuthProvider
  {
    
    private IUserRepository users;

    public FormsAuthProvider(IUserRepository userRepository)
    {
      users = userRepository;
    }
    

  public bool Authenticate(string username, string password)
    {
      // Этот код берет логин и пароль из web.config
      // bool result = FormsAuthentication.Authenticate(username, password);

      // ищем такого пользователя в нашей БД. Увы пока пароли хранятся в открытом виде.
      bool result = users.Users.FirstOrDefault(x => x.Name == username && x.Password == password) != null;

      if (result)
      {
        FormsAuthentication.SetAuthCookie(username, false);
      }
      return result;
    }
  }
}
