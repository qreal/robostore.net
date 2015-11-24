using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Mvc;
using Store.ViewModels;

namespace Store.Controllers
{
  public class MessageController : Controller
  {
    private static List<MessageRobot> messages = new List<MessageRobot>();

    [HttpPost]
    public JsonResult Post(MessageRobot msg)
    {
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
        // тут будет отправка сообщений на робота через класс RouterConnector
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
