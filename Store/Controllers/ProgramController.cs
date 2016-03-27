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
    public ViewResult ShowAllPrograms(int page = 1) =>
      View(new PagedContentViewModel<Program>
      {
        PageContent = _paginationManager.FormProgramPage(pageSize, page),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = pageSize,
          TotalItems = _contentManager.AmountPrograms
        }
      });

    public ViewResult ShowRobotPrograms(int robotId)
    {
      return View(_programManager.GetRobotProgramsByRobotIdAsync(robotId));
    }

    public async Task<ActionResult> UpdateRobotProgram(int programRobotId)
    {
      var robot = _robotManager.GetRobotByProgramRobotId(programRobotId);
      var program = _programManager.GetProgramByProgramRobotId(programRobotId);
      await _commandManager.AskRobotAboutProgramAsync(robot, program, RobotCommandTypes.Update);
      await _programManager.UpdateProgramRobotAsync(programRobotId);
      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }

    public async Task<ActionResult> RemoveRobotProgram(int programRobotId)
    {
      var robot = _robotManager.GetRobotByProgramRobotId(programRobotId);
      var program = _programManager.GetProgramByProgramRobotId(programRobotId);
      await _commandManager.AskRobotAboutProgramAsync(robot, program, RobotCommandTypes.Remove);
      await _programManager.RemoveProgramRobotAsync(programRobotId);
      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }
  }
}
