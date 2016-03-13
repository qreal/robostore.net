using System.Web.Mvc;
using Domain.Data;
using Store.Models.Robot;
using Store.Services;

namespace Store.Controllers
{
  public class RobotController : Controller
  {
    public RobotController(IData d)
    {

    }

    public ViewResult SendForm()
    {
      // пока загулшки везде
      int robotId = 4;
      return View(new RobotCode {RobotId = robotId, Code = ""});
    }

    [HttpPost]
    public ActionResult Edit(RobotCode rc)
    {
      //var robotConnector = new RobotConnector();
      //robotConnector.SendMessageToRobot(rc.Code);
      // todo переделать
      return RedirectToAction("Index", "Home");
    }
  }
}