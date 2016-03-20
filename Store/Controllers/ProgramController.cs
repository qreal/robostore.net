using System.Web.Mvc;
using Domain;
using Domain.Data;
using Domain.Entities;
using Domain.Pagination;
using Store.Models.Home;

namespace Store.Controllers
{
  public class ProgramController : Controller
  {
    // сколько програм мы отображаем на одной странице
    private int pageSize = 4;
    private readonly PaginationManager _paginationManager;
    private readonly ContentManager _contentManager;

    public ProgramController(IData data)
    {
      _contentManager = new ContentManager(data);
      _paginationManager = new PaginationManager(data);
    }

    /*
      Отображем список программ
    */
    public ViewResult ShowPrograms(int page = 1) =>
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
  }
}
