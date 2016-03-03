using System.Web.Mvc;
using Store.Models.Data;
using Store.Services;
using Store.ViewModels.Robot;

namespace Store.Controllers
{
  public class RobotController : Controller
  {
    private IData data;

    public RobotController(IData d)
    {
      data = d;
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
      var robotConnector = new RobotConnector();
      robotConnector.SendMessageToRobot(rc.Code);
      return RedirectToAction("Index", "Home");
    }
  }
}