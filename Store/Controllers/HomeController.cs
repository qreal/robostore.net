using System.Threading.Tasks;
using System.Web.Mvc;
using Store.Models.Data;
using Store.Models.Managers;
using Store.Services;

namespace Store.Controllers
{
  public class HomeController : Controller
  {
    private IData data;
    // сколько програм мы отображаем на одной странице
    private int pageSize = 4;
    private ControllerManager _programManager;
    private IRobotConnector _robotConnector;
    private const int TestRobotId = 4;

    public HomeController(IData d, IRobotConnector r)
    {
      _programManager = new ControllerManager(d, r);
    }

    /*
      Отображем список программ
    */
    public ViewResult Index(int page = 1) => View(_programManager.FormProgramList(pageSize, page));

    /*
      Отображем список роботов
    */
    public ViewResult ShowRobots(int page = 1) => View(_programManager.FormRobotList(pageSize, page));
    
    /*
      Добавляем выбранную программу в программы для робота и сообщаем ему об этом
    */
    public async Task<string> AddProgramToRobot(int programId)
    {
      await _programManager.AddProgramToRobot(programId, TestRobotId);
      return "success";
    }

    public string SendProgramDirectly(int robotId)
    {
      return "config";
    }

    // меню с ссылками на 2 таблицы
    public PartialViewResult Menu(string category = null)
    {
      ViewBag.SelectedCategory = category;
      return PartialView("Menu", new[] { "Program", "Robots" });
    }


    public FileContentResult GetImage(int programId)
    {
      var image = _programManager.GetImageById(programId);
      return File(image.ImageData, image.ImageMimeType);
    }

  }
}