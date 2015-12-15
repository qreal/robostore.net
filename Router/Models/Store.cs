using System.Collections.Generic;
using Router.Models.Entities;

/*
  Пока вместо мб хранит инфу о роботах
*/

// пока не нужен

namespace Router.Models
{
  public class Store
  {
    public static List<Robot> Robots = new List<Robot>();
    public static List<Configuration> Configurations = new List<Configuration>();
    public static List<StoredMessage> StoredMessages = new List<StoredMessage>(); 
  }
}
