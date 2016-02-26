using Tests.DB;

/*
Общий класс для API тестов
*/

namespace Tests.API
{
  public class APITest
  {
    protected RobotsDBEntities1 context = new RobotsDBEntities1();
    protected string serverUrl = "http://robstore.azurewebsites.net/api";
  }
}
