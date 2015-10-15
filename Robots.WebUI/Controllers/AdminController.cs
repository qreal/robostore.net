using System.Linq;
using System.Web.Mvc;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;

/*
Используется для администраторской работы с данными - CRUD.
Пока только для User и Robots
В порядке исключения метод Index() - отображающий пользователей HE называется IndexUser()
Todo: подумать над упрощением: потому что код один и тот же для сущностей. Ввести хотя бы code regions пока что.
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

    public ViewResult IndexRobot()
    {
      return View(robotsRepository.Data);
    }

    public ViewResult EditUser(int userId)
    {
      User user = usersRepository.Data.FirstOrDefault(p => p.UserID == userId);
      return View(user);
    }

    public ViewResult EditRobot(int robotId)
    {
      Robot robot = robotsRepository.Data.FirstOrDefault(p => p.RobotID == robotId);
      return View(robot);
    }

    [HttpPost]
    public ActionResult EditUser(User user)
    {
      if (ModelState.IsValid)
      {
        usersRepository.SaveData(user);
        TempData["message"] = string.Format("{0} has been saved", user.Name);
        return RedirectToAction("Index");
      }
      else
      {
        // there is something wrong with the data values
        return View(user);
      }
    }

    [HttpPost]
    public ActionResult EditRobot(Robot robot)
    {
      if (ModelState.IsValid)
      {
        robotsRepository.SaveData(robot);
        TempData["message"] = string.Format("{0} has been saved", robot.Name);
        return RedirectToAction("Index");
      }
      else
      {
        // there is something wrong with the data values
        return View(robot);
      }
    }

    public ViewResult CreateUser()
    {
      return View("EditUser", new User());
    }

    public ViewResult CreateRobot()
    {
      return View("EditRobot", new Robot());
    }

    [HttpPost]
    public ActionResult DeleteUser(int userId)
    {
      User deletedUser = usersRepository.DeleteData(userId);
      if (deletedUser != null)
      {
        TempData["message"] = string.Format("{0} was deleted",
          deletedUser.Name);
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteRobot(int robotId)
    {
      Robot deletedRobot = robotsRepository.DeleteData(robotId);
      if (deletedRobot != null)
      {
        TempData["message"] = string.Format("{0} was deleted", deletedRobot.Name);
      }
      return RedirectToAction("IndexRobot");
    }
  }
}