using Tests.Services;

/*
Общие штуки для тестов бизнес логики выносим сюда
*/

namespace Tests.Logic
{
  public class LogicTest
  {
    // Эмулируем главную БД
    protected readonly FakeData data = new FakeData();
    //protected readonly FakeRobotConnector robotConnector = new FakeRobotConnector();
  }
}
