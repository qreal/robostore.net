using System.Web.Mvc;
using Domain;
using Domain.Command;
using Domain.Data;
using Domain.Entities;
using Domain.Pagination;
using Domain.Programs;
using Domain.Robots;
using Domain.Users;
using Store.Models.Home;
using Store.Models.Program;
using Store.Models.Robot;
using Store.Services;

namespace Store.Controllers
{
  public class RobotController : Controller
  {
    private int pageSize = 4;
    private PaginationManager _paginationManager;
    private ContentManager _contentManager;
    private RobotManager _robotManager;
    private ProgramManager _programManager;
    private CommandManager _commandManager;
    private UserManager _userManager;

    public RobotController(IData d)
    {
      _paginationManager = new PaginationManager(d);
      _contentManager = new ContentManager(d);
      _robotManager = new RobotManager(d);
      _programManager = new ProgramManager(d);
      _commandManager = new CommandManager(d);
    }

    /*
      Отображем список роботов
    */

    public ViewResult ShowMyRobots(int page = 1) =>
      View(new PagedContentViewModel<Robot>
      {
        PageContent = _paginationManager.FormMyRobotPage(_robotManager.GetMyRobots(FakeSession.User), pageSize, page),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = pageSize,
          TotalItems = _contentManager.AmoutRobots
        }
      });

    [HttpGet]
    public ActionResult AddRobotForm()
    {
      return View();
    }

    [HttpPost]
    public ActionResult AddRobotForm(RobotActivationCode activationCode)
    {
      var robot = _robotManager.GetRobotByActivationCode(activationCode.Code);
      _robotManager.BindRobotToUser(robot, FakeSession.User);
      FakeSession.RobotIds.Add(robot.RobotID);
      return RedirectToAction("ShowMyRobots", "Robot", new {category = "My Robots"});
    }

    public ViewResult AddProgramToRobot(ProgramSummary programSummary)
    {
      foreach (var robotId in programSummary.RobotIds)
      {
        var robot = _robotManager.GetRobotById(robotId);
        var program = _programManager.GetProgramById(programSummary.ProgramId);
        _programManager.CreateProgramRobot(robot, program);
        _commandManager.AskRobotAboutProgram(robot, program, RobotCommandTypes.Install);
      }
      return View();
    }

  }
}