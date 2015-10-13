using System.Web.Mvc;
using System.Web.Security;
using Robots.Domain.Abstract;
using Robots.WebUI.Infrastructure.Abstract;
using Robots.WebUI.Models;

/*
Котроллер для управления аккаунтом.
Сейчас отвечает за начальный вход в систему. Берет данные из web.config
Работает с сущностями User
*/

namespace Robots.WebUI.Controllers
{
  public class AccountController : Controller
  {
    // способ аутефикации
    private IAuthProvider authProvider;
    // для работы с пользователями
    private IUserRepository users;

    // получаем от Ninject
    public AccountController(IAuthProvider auth, IUserRepository userRepository)
    {
      authProvider = auth;
      this.users = userRepository;
    }

    public ViewResult Login()
    {
      return View();
    }


    
    public ActionResult SignOut()
    {
      FormsAuthentication.SignOut();
      return Redirect(Url.Action("Index", "Home"));
    }


    [HttpPost]
    public ActionResult Login(LoginViewModel model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        if (authProvider.Authenticate(model.UserName, model.Password))
        {
          return Redirect(returnUrl ?? Url.Action("Index", "Home"));
        }
        else
        {
          ModelState.AddModelError("", "Incorrect username or password");
          return View();
        }
      }
      else
      {
        return View();
      }
    }
  }
}