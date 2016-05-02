using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain;
using Domain.Command;
using Domain.Data;
using Domain.Entities;
using Domain.Pagination;
using Domain.Programs;
using Domain.Robots;
using Store.Models.Home;
using Store.Services;

namespace Store.Controllers
{
  public class ProgramController : Controller
  {
    // сколько програм мы отображаем на одной странице
    private int pageSize = 4;
    private readonly PaginationManager _paginationManager;
    private readonly ContentManager _contentManager;
    private readonly ProgramManager _programManager;
    private readonly RobotManager _robotManager;
    private readonly CommandManager _commandManager;

    public ProgramController(IData data)
    {
      _contentManager = new ContentManager(data);
      _paginationManager = new PaginationManager(data);
      _programManager = new ProgramManager(data);
      _robotManager = new RobotManager(data);
      _commandManager = new CommandManager(data);
    }

    /*
      Отображем список программ
    */

    public ViewResult ShowAllPrograms(int page = 1)
    {
      var robotSelectListContent = 
        _robotManager.GetMyRobots(FakeSession.User).
        Select(robot => new
        {
          RobotID = robot.RobotID + "",
          RobotName = "Robot №" + robot.RobotID
        }).ToList();

      ViewBag.robotSelectListContent = new MultiSelectList(robotSelectListContent, "RobotID", "RobotName");
      ViewBag.robotSelectListContentLength = _robotManager.GetMyRobots(FakeSession.User).Count();

      return View(new PagedContentViewModel<Program>
      {
        PageContent = _paginationManager.FormProgramPage(pageSize, page),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = pageSize,
          TotalItems = _contentManager.AmountPrograms
        }
      });
    }
      

    public ViewResult ShowRobotPrograms(int robotId)
    {
      return View(_programManager.GetRobotProgramsByRobotIdAsync(robotId));
    }

    public ActionResult UpdateRobotProgram(int programRobotId)
    {
      var robot = _robotManager.GetRobotByProgramRobotId(programRobotId);
      var program = _programManager.GetProgramByProgramRobotId(programRobotId);
      _commandManager.AskRobotAboutProgram(robot, program, RobotCommandTypes.Update);
      _programManager.UpdateProgramRobotAsync(programRobotId);
      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }

    public ActionResult RemoveRobotProgram(int programRobotId)
    {
      var robot = _robotManager.GetRobotByProgramRobotId(programRobotId);
      var program = _programManager.GetProgramByProgramRobotId(programRobotId);
      _commandManager.AskRobotAboutProgram(robot, program, RobotCommandTypes.Remove);
      _programManager.RemoveProgramRobot(programRobotId);
      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }
  }
}
