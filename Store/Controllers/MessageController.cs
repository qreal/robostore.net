using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Store.Models.Data;
using Store.Services;
using Store.ViewModels;

namespace Store.Controllers
{
  public class MessageController : Controller
  {
    private static List<MessageVM> messages = new List<MessageVM>();
    private RobotConnector _robotConnector;
    private IData data;
    private MessageProccessor proccessor;

    public MessageController(IData d)
    {
      data = d;
      _robotConnector = new RobotConnector();
      proccessor = new MessageProccessor(data);
    }

    /*
    Принимает по API сообщения от Роботов
    */
    [HttpPost]
    public async Task<JsonResult> Post(MessageVM msg)
    {
      messages.Add(msg);
      await proccessor.proccess(msg);
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
    public async Task <ActionResult> SendForm(MessageVM msg)
    {
      if (ModelState.IsValid)
      {
        // from server
        msg.From = "0";
        // надем Робота
        Robot robot = data.Robots.First(x => x.Number == msg.To);
        // если можно отправить на Робота, то ОК отправим.
        if (robot.isOnline && _robotConnector.SendMessageToRobot(msg))
        {
          msg.Text = "(sent) " + msg.Text;
          messages.Add(msg);
          return RedirectToAction("ShowAll");
        }
        // А если нет, то положим сообщение в БД
        else
        {
          var x = data.Messages;
          await data.AddAsync(new Message() {Robot = robot, Text = msg.Text});
          msg.Text = "(saved to DB) " + msg.Text;
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
