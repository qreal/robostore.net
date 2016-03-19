using Tests.DB;

/*
Общий класс для API тестов
*/

namespace Tests.API
{
  public class APITest
  {
    protected RDBEntities2 context = new RDBEntities2();
    protected string serverUrl = "http://robstark.azurewebsites.net";
  }
}
