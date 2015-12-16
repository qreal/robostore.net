/*
Конфигурация робота
Общий класс всех 3х сущностей
Пока можно менять только порт, который Робот слушает.
*/

namespace Store.Models
{

  public class Configuration
  {
    public int ConfigurationID { get; set; }
    public int RobotID { get; set; }
    public int Port { get; set; }
  }

}
