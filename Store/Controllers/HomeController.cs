using System.Linq;
using System.Web.Mvc;
using Store.Models.Data;
using Store.Models.Services;
using Store.ViewModels.Home;

namespace Store.Controllers
{
  public class HomeController : Controller
  {
    private IData data;
    // сколько програм мы отображаем на одной странице
    private int pageSize = 4;

    public HomeController(IData d)
    {
      data = d;
    }

    /*
      Отображем список программ
    */
    public ViewResult Index(int page = 1)
    {
      var viewModel = new ProgramsListViewModel
      {
        Programs = data.Programs.OrderBy(x => x.Name).
                                 Skip((page - 1)*pageSize).
                                 Take(pageSize),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = pageSize,
          TotalItems = data.Programs.Count()
        }
      };
      return View(viewModel);
    }

  }
}