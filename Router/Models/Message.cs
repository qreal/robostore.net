using System.Collections.Generic;

/*
Общий формат сообщения.
Общий класс всех 3х сущностей
*/

namespace Router.Models
{
  public class Message
  {
    public string Server { get; set; }
    public Configuration Robot { get; set; }
    public List<string> Commands { get; set; }
    public string Text { get; set; }
  }
}
