/*
Конфигурация робота + онлайн ли он
*/

// пока не нужен

namespace Router.Models
{
  public class Robot
  {
    public Configuration Config { get; set; }
    public bool isOnline { get; set; }
  }
}
