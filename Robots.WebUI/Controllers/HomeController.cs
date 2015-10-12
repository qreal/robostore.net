using System.Web.Mvc;

namespace Robots.WebUI.Controllers
{
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