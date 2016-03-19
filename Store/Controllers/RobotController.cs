using System.Threading.Tasks;
using System.Web.Mvc;
using Domain;
using Domain.Data;
using Domain.Entities;
using Domain.Pagination;
using Domain.Robots;
using Store.Models.Home;
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

    public RobotController(IData d)
    {
      _paginationManager = new PaginationManager(d);
      _contentManager = new ContentManager(d);
      _robotManager = new RobotManager(d);
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
    public async Task<ActionResult> AddRobotForm(RobotActivationCode activationCode)
    {
      var robot = _robotManager.GetRobotByActivationCode(activationCode.Code);
      await _robotManager.BindRobotToUser(robot, FakeSession.User);
      return RedirectToAction("ShowMyRobots", "Robot");
    }
  }
}