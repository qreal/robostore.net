using Tests.DB;

/*
Общий класс для API тестов
*/

namespace Tests.API
{
  public class APITest
  {
    protected RDBEntities3 context = new RDBEntities3();
    protected string serverUrl = "http://robstark.azurewebsites.net";
  }
}
