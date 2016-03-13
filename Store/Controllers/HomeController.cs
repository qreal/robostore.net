using System.Threading.Tasks;
using System.Web.Mvc;
using Domain;
using Domain.Data;
using Domain.Entities;
using Domain.Pagination;
using Domain.Programs;
using Store.Models.Home;

namespace Store.Controllers
{
  public class HomeController : Controller
  {
    // сколько програм мы отображаем на одной странице
    private int pageSize = 4;
    private ProgramManager _programManager;
    private PaginationManager _paginationManager;
    private ContentManager _contentManager;
    private const int TestRobotId = 4;

    public HomeController(IData d)
    {
      _programManager = new ProgramManager(d);
      _paginationManager = new PaginationManager(d);
      _contentManager = new ContentManager(d);
    }

    /*
      Отображем список программ
    */
    public ViewResult Index(int page = 1) =>
      View(new PagedContentViewModel<Program>
      {
        PageContent = _paginationManager.FormProgramPage(page, pageSize),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = pageSize,
          TotalItems = _contentManager.AmountPrograms
        }
      });

    /*
      Отображем список роботов
    */
    public ViewResult ShowRobots(int page = 1) => 
      View(new PagedContentViewModel<Robot>
      {
        PageContent = _paginationManager.FormRobotPage(page, pageSize),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = pageSize,
          TotalItems = _contentManager.AmoutRobots
        }
      });
    
    /*
      Добавляем выбранную программу в программы для робота и сообщаем ему об этом
    */
    public async Task<string> AddProgramToRobot(int programId)
    {
      await _programManager.AddProgramToRobot(programId, TestRobotId);
      return "success";
    }


    // меню с ссылками на 2 таблицы
    public PartialViewResult Menu(string category = null)
    {
      ViewBag.SelectedCategory = category;
      return PartialView("Menu", new[] { "Program", "Robots" });
    }


    public FileContentResult GetImage(int programId)
    {
      var image = _contentManager.GetImageById(programId);
      return File(image.ImageData, image.ImageMimeType);
    }
  }
}