using System.Web.Mvc;
using Domain;
using Domain.Data;

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


    // меню с ссылками на 2 таблицы
    public PartialViewResult Menu(string category = null)
    {
      ViewBag.SelectedCategory = category;
      return PartialView("Menu", new[] { "My Robots", "Add Robot", "All Program" });
    }


    public FileContentResult GetImage(int programId)
    {
      var image = _contentManager.GetImageById(programId);
      return File(image.ImageData, image.ImageMimeType);
    }
  }
}