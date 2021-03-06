﻿using System.Linq;
using Domain.Data;
using Domain.Entities;

/*
    Отдаем информацию о данных (статистику)
    или НЕСущностные файлы, такие как картинки ,например.
*/

namespace Domain.Managers
{
  public class ContentManager
  {
    private IData data;

    public ContentManager(IData d)
    {
      data = d;
    }

    public int AmoutRobots => 
            data.Robots.Data.Count();
    public int AmountPrograms =>
            data.Programs.Data.Count();

    public Image GetImageById(int imageId) =>
      data.Images.Data.FirstOrDefault(x => x.ImageID == imageId);
  }
}
