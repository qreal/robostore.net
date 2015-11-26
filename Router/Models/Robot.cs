/*
Конфигурация робота + онлайн ли он
*/

namespace Router.Models
{
  public class Robot
  {
    public Configuration Config { get; set; }
    public bool isOnline { get; set; }
  }
}
