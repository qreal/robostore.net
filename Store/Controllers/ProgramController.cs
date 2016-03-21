using System.Web.Mvc;
using Domain;
using Domain.Data;
using Domain.Entities;
using Domain.Pagination;
using Domain.Programs;
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

    public ProgramController(IData data)
    {
      _contentManager = new ContentManager(data);
      _paginationManager = new PaginationManager(data);
      _programManager = new ProgramManager(data);
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
      return View(_programManager.GetRobotProgramsByRobotId(robotId));
    }
  }
}
