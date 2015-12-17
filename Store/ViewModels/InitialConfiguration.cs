/*
Объект этого класса нам дает Робот при первом подключении
*/

namespace Store.ViewModels
{
  public class InitialConfiguration
  {
    public int Port { get; set; }
    public string Number { get; set; }
    public string IP { get; set; }
  }
}
