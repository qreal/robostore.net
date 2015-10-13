using System.Linq;
using System.Web;
using System.Web.Mvc;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;

/*
Используется для работы с данными - CRUD.
Пока только для User.
*/

namespace Robots.WebUI.Controllers
{
  public class AdminController : Controller
  {
    private IUserRepository repository;

    public AdminController(IUserRepository repo)
    {
      repository = repo;
    }

    public ViewResult Index()
    {
      return View(repository.Users);
    }

    public ViewResult Edit(int UserId)
    {
      User User = repository.Users.FirstOrDefault(p => p.UserID == UserId);
      return View(User);
    }

    [HttpPost]
    public ActionResult Edit(User User, HttpPostedFileBase image = null)
    {
      if (ModelState.IsValid)
      {
        repository.SaveUser(User);
        TempData["message"] = string.Format("{0} has been saved", User.Name);
        return RedirectToAction("Index");
      }
      else
      {
        // there is something wrong with the data values
        return View(User);
      }
    }

    public ViewResult Create()
    {
      return View("Edit", new User());
    }

    [HttpPost]
    public ActionResult Delete(int UserId)
    {
      User deletedUser = repository.DeleteUser(UserId);
      if (deletedUser != null)
      {
        TempData["message"] = string.Format("{0} was deleted",
          deletedUser.Name);
      }
      return RedirectToAction("Index");
    }
  }
}