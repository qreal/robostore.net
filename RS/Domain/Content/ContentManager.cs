using System.Linq;
using Domain.Data;
using Domain.Entities;

/*
Отдаем информацию о данных (статистику)
*/

namespace Domain
{
  public class ContentManager
  {
    private IData data;

    public ContentManager(IData d)
    {
      data = d;
    }

    public int AmoutRobots => data.Robots.Count();
    public int AmountPrograms => data.Programs.Count();

    public Image GetImageById(int programId) =>
     data.Programs.First(x => x.ProgramID == programId).Image;
  }
}
