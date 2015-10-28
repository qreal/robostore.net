/*
Контроллер отвечает за навигацию.
Сейчас он отвечает за боковое меню главной страницы.
*/

using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Roboto.Controller
{
  public class NavController : Microsoft.AspNet.Mvc.Controller
  {
    // боковое меню главной страницы.
    public PartialViewResult MenuPortSide()
    {
      return PartialView();
    }
  }
}
