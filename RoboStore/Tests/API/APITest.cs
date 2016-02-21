using Tests.DB;

/*
Общий класс для API тестов
*/

namespace Tests.API
{
  public class APITest
  {
    protected RobotsDBEntities context = new RobotsDBEntities();
    protected string serverUrl = "http://robstore.azurewebsites.net/api";
  }
}
