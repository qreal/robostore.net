using System.Linq;
using System.Web.Mvc;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;

/*
Используется для администраторской работы с данными - CRUD.
Пока только для User и Robots
В порядке исключения метод Index() - отображающий пользователей HE называется IndexUser()
*/

namespace Robots.WebUI.Controllers
{
  public class AdminController : Controller
  {
    private ICommonRepository<User> usersRepository;
    private ICommonRepository<Robot> robotsRepository; 

    public AdminController(ICommonRepository<User> users, ICommonRepository<Robot> robots )
    {
      usersRepository = users;
      robotsRepository = robots;
    }

    public ViewResult Index()
    {
      return View(usersRepository.Data);
    }

    public ViewResult EditUser(int UserId)
    {
      User User = usersRepository.Data.FirstOrDefault(p => p.UserID == UserId);
      return View(User);
    }

    [HttpPost]
    public ActionResult EditUser(User User)
    {
      if (ModelState.IsValid)
      {
        usersRepository.SaveData(User);
        TempData["message"] = string.Format("{0} has been saved", User.Name);
        return RedirectToAction("Index");
      }
      else
      {
        // there is something wrong with the data values
        return View(User);
      }
    }

    public ViewResult CreateUser()
    {
      return View("EditUser", new User());
    }

    [HttpPost]
    public ActionResult DeleteUser(int UserId)
    {
      User deletedUser = usersRepository.DeleteData(UserId);
      if (deletedUser != null)
      {
        TempData["message"] = string.Format("{0} was deleted",
          deletedUser.Name);
      }
      return RedirectToAction("Index");
    }
  }
}