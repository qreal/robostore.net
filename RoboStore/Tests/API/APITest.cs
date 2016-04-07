using Tests.DB;

/*
Общий класс для API тестов
*/

namespace Tests.API
{
  public class APITest
  {
    protected RDBEntities6 context = new RDBEntities6();
    protected string serverUrl = "http://robstark.azurewebsites.net";
  }
}
