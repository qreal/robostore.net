using Microsoft.AspNet.Mvc;
using Roboto.Models.DataModels;
using Roboto.Models.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Roboto.Controller
{
    public class AccountController : Microsoft.AspNet.Mvc.Controller
  {
        public IActionResult Login()
        {
          return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
          if (ModelState.IsValid)
          {
            // Todo восстановить проверку подлинности
            // if (authProvider.Authenticate(model.UserName, model.Password))
            if (true)
            {
              return Redirect(Url.Action("Index", "Home"));
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

    // Todo: написать
    public IActionResult SignOut()
    {
      // FormsAuthentication.SignOut();
      return Redirect(Url.Action("Index", "Home"));
    }

    public IActionResult SignUp()
    {
      return View();
    }

    // Todo: add check for login unique
    [HttpPost]
    public IActionResult SignUp(User user)
    {
      if (ModelState.IsValid)
      {
        // Todo: add save new user
        //usersRepository.SaveData(user);
        TempData["message"] = $"{user.Name} has been joined";
        return RedirectToAction("Login");
      }
      else
      {
        return View();
      }
    }



  }
}
