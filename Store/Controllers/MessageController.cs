using System.Collections.Generic;
using System.Web.Mvc;
using Store.Models;

namespace Store.Controllers
{
  public class MessageController : Controller
  {
    private static List<Message> messages = new List<Message>();
    private RouterConnector routerConnector;

    public MessageController()
    {
      routerConnector = new RouterConnector();
    }

    /*
    Принимает по API сообщения от Роботов
    */
    [HttpPost]
    public JsonResult Post(Message msg)
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

    /*
    Отправить сообщение на Робота (попытаться)
    */
    [HttpGet]
    public ActionResult SendForm()
    {
      return View();
    }

    [HttpPost]
    public ActionResult SendForm(Message msg)
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
