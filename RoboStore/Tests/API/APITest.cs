using Tests.DB;

/*
Общий класс для API тестов
*/

namespace Tests.API
{
  public class APITest
  {
    protected RDBEntities4 context = new RDBEntities4();
    protected string serverUrl = "http://robstark.azurewebsites.net";
  }
}
