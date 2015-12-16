using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Store.Models.Data;
using Store.Services;
using Store.ViewModels;

namespace Store.Controllers
{
  public class MessageController : Controller
  {
    private static List<MessageVM> messages = new List<MessageVM>();
    private RouterConnector routerConnector;
    private IData data;

    public MessageController(IData d)
    {
      data = d;
      routerConnector = new RouterConnector();
    }

    /*
    Принимает по API сообщения от Роботов
    */
    [HttpPost]
    public JsonResult Post(MessageVM msg)
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
    public ActionResult SendForm(MessageVM msg)
    {
      if (ModelState.IsValid)
      {
        // from server
        msg.From = "0";
        // надем Робота
        Robot robot = data.Robots.First(x => x.Number == msg.To);
        // если можно отправить на Робота, то ОК отправим.
        if (robot.isOnline && routerConnector.SendMessageToRobot(msg))
        {
          msg.Text = "(sent) " + msg.Text;
          messages.Add(msg);
          return RedirectToAction("ShowAll");
        }
        // А если нет, то положим сообщение в БД
        else
        {
          var x = data.Messages;
          data.AddAsync(new Message() {Robot = robot, Text = msg.Text}).Wait();
          msg.Text = "(sent to router) " + msg.Text;
          messages.Add(msg);
          return RedirectToAction("ShowAll");
        }
        
      }
      else
      {
        // there is a validation error
        return View(msg);
      }
    }
  }
}
