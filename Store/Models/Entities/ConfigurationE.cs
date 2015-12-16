/*
Конфигурация робота
Общий класс всех 3х сущностей
Пока можно менять только порт, который Робот слушает.
*/

using System.ComponentModel.DataAnnotations;

namespace Store.Models.Entities
{

  public class ConfigurationE
  {
    [Key]
    public int ConfigurationID { get; set; }
    public virtual RobotE Robot { get; set; }
    public int RobotID { get; set; }
    public int Port { get; set; }
  }

}
