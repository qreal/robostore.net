using System.Web.Mvc;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;

/*
Стартовый контроллер.
Сейчас отвечает за выдачу демо страниц разделов.
*/

namespace Robots.WebUI.Controllers
{
  // Для простой авторизации используется временная пара в файле web.config
  [Authorize]
  public class HomeController : Controller
  {
    
    // GET: Home
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Demo_Map()
    {
      return View();
    }

    public ActionResult Demo_MyDiagrams()
    {
      return View();
    }

    public ActionResult Demo_MyRobots()
    {
      return View();
    }

    public ActionResult Demo_Settings()
    {
      return View();
    }
  }
}