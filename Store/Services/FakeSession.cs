﻿using System.Collections.Generic;
using Domain.Entities;

/*
  Пока что мы эмулируем сессию.
  На входе инициализируем этот класс.
  На выходе обнуляем.
*/

namespace Store.Services
{
  public static class FakeSession
  {
    public static User User { get; set; }

    public static List<int> RobotIds = new List<int>(); 
  }
}
