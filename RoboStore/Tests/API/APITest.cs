using Tests.DB;

/*
Общий класс для API тестов
*/

namespace Tests.API
{
  public class APITest
  {
    protected RDBEntities1 context = new RDBEntities1();
    protected string serverUrl = "http://robstore.azurewebsites.net/api";
  }
}
