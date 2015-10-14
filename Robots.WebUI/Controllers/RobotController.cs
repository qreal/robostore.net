using System.Web.Mvc;

namespace Robots.WebUI.Controllers
{
    public class RobotController : Controller
    {
        // GET: Robot
        public ActionResult Index()
        {
            return View();
        }
    }
}