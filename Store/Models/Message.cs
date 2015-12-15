using System.Collections.Generic;

/*
Общий формат сообщения.
Общий класс всех 3х сущностей
*/

namespace Store.Models
{
  public class Message
  {
    public int From { get; set; }
    public int To { get; set; }
    public List<string> Commands { get; set; }
    public string Text { get; set; }
  }
}
