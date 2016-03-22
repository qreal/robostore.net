using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Data;
using Store.Models.Home;

namespace Store.Controllers
{
  public class HomeController : Controller
  {
    // сколько програм мы отображаем на одной странице
    private int pageSize = 4;
    private ContentManager _contentManager;

    public HomeController(IData d)
    {
      _contentManager = new ContentManager(d);
    }

    public PartialViewResult Menu()
    {
      var names = new[] { "My Robots", "Add Robot", "All Program" };
      var menuItems = names.Select(x => new LeftMenuItem
      {
        Name = x
      }).ToArray();
      menuItems[0].Url = Url.Action("ShowMyRobots", "Robot");
      menuItems[1].Url = Url.Action("AddRobotForm", "Robot");
      menuItems[2].Url = Url.Action("ShowAllPrograms", "Program");
      return PartialView("Menu", menuItems);
    }

    public FileContentResult GetImage(int programId)
    {
      var image = _contentManager.GetImageById(programId);
      return File(image.ImageData, image.ImageMimeType);
    }
  }
}