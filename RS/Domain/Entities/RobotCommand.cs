using Newtonsoft.Json;

/*
Это не вспомогательная таблица, название выбрано,
чтобы не путаться с другими типами Command.
*/

namespace Domain.Entities
{
  public class RobotCommand
  {
    public int RobotCommandID { get; set; }
    public int RobotID { get; set; }
    [JsonIgnore]
    public virtual Robot Robot { get; set; }
    public int Type { get; set; }
    public int Argument { get; set; }
    public bool Received { get; set; }
  }
}
