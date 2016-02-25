using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Store.Models.Data;
using Store.Models.Entities;
using Store.Models.Managers;
using Store.Models.Services;
using Store.Services;
using Store.ViewModels.Home;

namespace Store.Controllers
{
  public class HomeController : Controller
  {
    private IData data;
    // сколько програм мы отображаем на одной странице
    private int pageSize = 4;
    private ProgramManager _programManager;
    private IRobotConnector _robotConnector;
    private const int TestRobotId = 4;

    public HomeController(IData d, IRobotConnector r)
    {
      _programManager = new ProgramManager(d, r);
    }

    /*
      Отображем список программ
    */
    public ViewResult Index(int page = 1) => View(_programManager.FormProgramList(pageSize, page));
    
    /*
      Добавляем выбранную программу в программы для робота и сообщаем ему об этом
    */
    public async Task<string> AddProgramToRobot(int programId)
    {
      await _programManager.AddProgramToRobot(programId, TestRobotId);
      return "success";
    }

  }
}