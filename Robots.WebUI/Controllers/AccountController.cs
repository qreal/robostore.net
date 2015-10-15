using System.Web.Mvc;
using System.Web.Security;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;
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
    private ICommonRepository<User> usersRepository;

    // получаем от Ninject
    public AccountController(IAuthProvider auth, ICommonRepository<User> userRepository)
    {
      authProvider = auth;
      this.usersRepository = userRepository;
    }

    public ViewResult Login()
    {
      return View();
    }

    // По сути второй аргумент здесь нужен для тестирования только.
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

    public ActionResult SignOut()
    {
      FormsAuthentication.SignOut();
      return Redirect(Url.Action("Index", "Home"));
    }

    public ViewResult SignUp()
    {
      return View();
    }

    [HttpPost]
    public ActionResult SignUp(User user)
    {
      /*сделать проверку на уникальность*/
      if (ModelState.IsValid)
      {
        usersRepository.SaveData(user);
        /* Проверить отображение сообщения */
        TempData["message"] = string.Format("{0} has been joined", user.Name);
        return RedirectToAction("Login");
      }
      else
      {
        return View();
      }
    }

  }
}