using System.Collections.Generic;
using System.Web.Mvc;
using Store.Models;
using Store.ViewModels;

namespace Store.Controllers
{
  public class MessageController : Controller
  {
    private static List<MessageRobot> messages = new List<MessageRobot>();
    private RouterConnector routerConnector;

    public MessageController()
    {
      routerConnector = new RouterConnector();
    }

    [HttpPost]
    public JsonResult Post(MessageRobot msg)
    {
      // добавим тут синтаксис Sent и Received, чтобы было что-то похожее на переписку
      msg.Text = "(received) " + msg.Text;
      messages.Add(msg);
      return Json("success");
    }

    public ActionResult ShowAll()
    {
      return View(messages);
    }

    [HttpGet]
    public ActionResult SendForm()
    {
      return View();
    }

    [HttpPost]
    public ActionResult SendForm(MessageRobot msg)
    {
      if (ModelState.IsValid)
      {
        routerConnector.SendToRobot(msg);
        msg.Text = "(sent) " + msg.Text;
        messages.Add(msg);
        return RedirectToAction("ShowAll");
      }
      else
      {
        // there is a validation error
        return View(msg);
      }
    }
  }
}
