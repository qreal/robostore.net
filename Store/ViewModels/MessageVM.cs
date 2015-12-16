using System.Collections.Generic;

/*
Общий формат сообщения.
Общий класс всех 3х сущностей
*/

namespace Store.ViewModels
{
  public class MessageVM
  {
    public string From { get; set; }
    public string To { get; set; }
    public List<string> Commands { get; set; }
    public string Text { get; set; }
  }
}
