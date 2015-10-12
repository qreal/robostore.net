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
        // GET: Nav
        public PartialViewResult MenuPortSide()
        {
            return PartialView();
        }
    }
}