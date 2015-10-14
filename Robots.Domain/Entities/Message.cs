/*
Класс сообщение.
Todo: вынести RobotIDs в отдельную таблицу
Todo: разобраться зачем этот класс
*/

namespace Robots.Domain.Entities
{
  public class Message
  {
    // Мб тут лучше int ?
    public string From { get; set; }
    public string Type { get; set; }
    public int RobotID { get; set; }
    public int UserID { get; set; }
    // Вот это по возможности срочно поменять.
    public string RobotIDs { get; set; }
  }
}
