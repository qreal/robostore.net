using System.Web.Helpers;
using System.Web.Mvc;

namespace Store.Controllers
{
  public class MessageController : Controller
  {

    // POST: api/Values
    [HttpPost]
    public JsonResult Post(string value)
    {
      HomeController.Messages.Add(value);
      return Json("success");
    }

  }
}
