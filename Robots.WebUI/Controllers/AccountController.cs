using System.Web.Mvc;
using Robots.WebUI.Infrastructure.Abstract;
using Robots.WebUI.Models;

/*
Котроллер для управления аккаунтом.
Сейчас отвечает за начальный вход в систему.
*/

namespace Robots.WebUI.Controllers
{
  public class AccountController : Controller
  {

    private IAuthProvider authProvider;

    // получаем от Ninject
    public AccountController(IAuthProvider auth)
    {
      authProvider = auth;
    }

    public ViewResult Login()
    {
      return View();
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