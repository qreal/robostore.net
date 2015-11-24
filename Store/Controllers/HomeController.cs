using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
      public static List<string> Messages = new List<string>();

        // GET: Home
        public ActionResult Index()
        {
          return RedirectToAction(actionName: "ShowAll", controllerName: "Message");
        }
    }
}