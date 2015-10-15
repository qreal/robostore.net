using System.Linq;
using System.Web.Mvc;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;

/*
Используется для администраторской работы с данными - CRUD.
Пока только для User и Robots
В порядке исключения метод Index() - отображающий пользователей HE называется IndexUser()
Используются coderegion
Todo: подумать над упрощением: потому что код один и тот же для сущностей
*/

namespace Robots.WebUI.Controllers
{
  public class AdminController : Controller
  {
    private ICommonRepository<User> usersRepository;
    private ICommonRepository<Robot> robotsRepository;

    // Для того, чтобы передать инфу в Контроллер Nav о том, какую кагорию(User или Robot) мы смотрим
    public static int categoryState;
     

    public AdminController(ICommonRepository<User> users, ICommonRepository<Robot> robots )
    {
      usersRepository = users;
      robotsRepository = robots;
    }

    #region CRUD User
    public ViewResult Index()
    {
      // для навигации
      categoryState = 0;
      return View(usersRepository.Data);
    }

    public ViewResult EditUser(int userId)
    {
      User user = usersRepository.Data.FirstOrDefault(p => p.UserID == userId);
      return View(user);
    }

    [HttpPost]
    public ActionResult EditUser(User user)
    {
      if (ModelState.IsValid)
      {
        usersRepository.SaveData(user);
        TempData["message"] = string.Format("User {0} has been saved", user.Name);
        return RedirectToAction("Index");
      }
      else
      {
        // there is something wrong with the data values
        return View(user);
      }
    }

    public ViewResult CreateUser()
    {
      return View("EditUser", new User());
    }

    [HttpPost]
    public ActionResult DeleteUser(int userId)
    {
      User deletedUser = usersRepository.DeleteData(userId);
      if (deletedUser != null)
      {
        TempData["message"] = string.Format("User {0} was deleted",
          deletedUser.Name);
      }
      return RedirectToAction("Index");
    }

    #endregion

    #region CRUD Robot

    public ViewResult IndexRobot()
    {
      // для навигации
      categoryState = 1;
      return View(robotsRepository.Data);
    }

    
    public ViewResult EditRobot(int robotId)
    {
      Robot robot = robotsRepository.Data.FirstOrDefault(p => p.RobotID == robotId);
      return View(robot);
    }

   

    [HttpPost]
    public ActionResult EditRobot(Robot robot)
    {
      if (ModelState.IsValid)
      {
        robotsRepository.SaveData(robot);
        TempData["message"] = string.Format("Robot {0} has been saved", robot.Name);
        return RedirectToAction("Index");
      }
      else
      {
        // there is something wrong with the data values
        return View(robot);
      }
    }

   

    public ViewResult CreateRobot()
    {
      return View("EditRobot", new Robot());
    }

   

    [HttpPost]
    public ActionResult DeleteRobot(int robotId)
    {
      Robot deletedRobot = robotsRepository.DeleteData(robotId);
      if (deletedRobot != null)
      {
        TempData["message"] = string.Format("Robot {0} was deleted", deletedRobot.Name);
      }
      return RedirectToAction("IndexRobot");
    }
    #endregion
  }
}