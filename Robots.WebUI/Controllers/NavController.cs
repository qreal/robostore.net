using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*
Контроллер отвечает за навигацию.
Сейчас он отвечает за боковое меню главной страницы.
*/

namespace Robots.WebUI.Controllers
{
    public class NavController : Controller
    {
      // боковое меню главной страницы.
      public PartialViewResult MenuPortSide()
      {
        return PartialView();
      }

      // боковое меню в Admin Контроллере
      public PartialViewResult MenuAdmin()
      {
        return PartialView();
      }
    }
}