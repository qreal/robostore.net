/*
Конфигурация робота
Общий класс всех 3х сущностей
*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Router.Models.Entities
{
  public class Configuration
  {
    [Key]
    public int ConfigurationID { get; set; }
    public virtual Robot Robot { get; set; }
    public int RobotID { get; set; }
    public int Port { get; set; }
  }
}
