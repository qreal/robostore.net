using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Data;
using Domain.Users;
using Store.Models.User;

namespace Store.Controllers
{
  public class UserController : Controller
  {
    private UserManager userManager;

    public UserController(IData d)
    {
      userManager = new UserManager(d);
    }

    // GET: User
    public ActionResult Registration()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Registration(UserProfile profile)
    {
      // если пароль и логин введены
      if (ModelState.IsValid && !userManager.TryEnter(profile.Login, profile.Password))
      {
        await userManager.CreateUser(profile.Login, profile.Password);
        return View("Welcome", (object)profile.Login);
      }
      return View();
    }

    public ActionResult Entrance()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Entrance(UserProfile profile)
    {
      if (ModelState.IsValid && userManager.TryEnter(profile.Login, profile.Password))
      {
        await userManager.CreateUser(profile.Login, profile.Password);
        return RedirectToAction("Index", "Home");
      }
      return View();
    }
  }
}