using System.Linq;
using System.Net;
using System.Web.Mvc;
using Domain.Data;
using Domain.Entities;
using Domain.Managers;
using Domain.Managers.Pagination;
using Domain.Managers.RobotCommand;
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

        public ViewResult ShowAllPrograms(int page = 1)
        {
            var robotSelectListContent =
                _robotManager.GetRobotsByUser(FakeSession.User).
                    Select(robot => new
                    {
                        RobotID = robot.RobotID + "",
                        RobotName = "Robot №" + robot.RobotID
                    }).ToList();

            ViewBag.robotSelectListContent = new MultiSelectList(robotSelectListContent, "RobotID", "RobotName");
            ViewBag.robotSelectListContentLength = _robotManager.GetRobotsByUser(FakeSession.User).Count();

            return View(new PagedContentViewModel<Program>
            {
                PageContent = _paginationManager.FormProgramsPage(pageSize, page),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = _contentManager.AmountPrograms
                }
            });
        }

        public ViewResult ShowRobotPrograms(int robotId)
            => View(_programManager.GetRobotProgramsByRobotId(robotId));

        public ActionResult UpdateRobotProgram(int programRobotId)
        {
            var robot = _robotManager.GetRobotByProgramRobotId(programRobotId);
            var program = _programManager.GetProgramByProgramRobotId(programRobotId);
            _commandManager.CreateRobotCommand(robot, program, RobotCommandTypes.Update);
            _programManager.UpdateVersionProgramRobot(programRobotId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult RemoveRobotProgram(int programRobotId)
        {
            var robot = _robotManager.GetRobotByProgramRobotId(programRobotId);
            var program = _programManager.GetProgramByProgramRobotId(programRobotId);
            _commandManager.CreateRobotCommand(robot, program, RobotCommandTypes.Remove);
            _programManager.RemoveProgramRobot(programRobotId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
