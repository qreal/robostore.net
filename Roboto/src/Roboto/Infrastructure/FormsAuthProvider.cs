
/*
Реализовать Авторизацию
*/

namespace Roboto.Infrastructure
{
  public class FormsAuthProvider : IAuthProvider
  {
    public bool Authenticate(string username, string password)
    {
      return true;
    }
  }
}
