using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
/*
Стартовый контроллер.
Сейчас отвечает за выдачу демо страниц разделов.
*/

namespace Roboto.Controller
{
  public class HomeController : Microsoft.AspNet.Mvc.Controller
  {
    // GET: /<controller>/
    public IActionResult Index()
    {
      return View();
    }


    public IActionResult Demo_Map()
    {
      return View();
    }

    public IActionResult Demo_MyDiagrams()
    {
      return View();
    }

    public IActionResult Demo_MyRobots()
    {
      return View();
    }

    public IActionResult Demo_Settings()
    {
      return View();
    }
  }
}
