/*
Класс Робот
У Агеева Robot, RobotInfo и RobotWrapper - 3 разных класса, я же пока не вижу причин так делать
Todo: разобраться с использованием
Todo: статус - string стоит ли?
*/

namespace Robots.Domain.Entities
{
  public class Robot
  {
    public int RobotID { get; set; }
    public string Name { get; set; }
    public string SSID { get; set; }
    public int UserID { get; set; }
    /*
    Хз зачем, пока эти 3 штуки
    */
    public string ModelConfig { get; set; }
    public string SystemConfig { get; set; }
    public string Program { get; set; }
    public string Status { get; set; }
  }
}
