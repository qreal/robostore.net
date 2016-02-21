using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DB;
using Tests.Services;

/*
CRUD тесты для всех сущностей в БД
*/

namespace Tests
{
  [TestClass]
  public class DataBaseTest
  {
    private RobotsDBEntities context = new RobotsDBEntities();

    // создадим тестовые данные

    private Robot robot = new Robot();

    private Program program = new Program
    {
      Name = MoqDataGenerator.GetRandomString(10),
      Code = MoqDataGenerator.GetRandomString(1000),
      ActualVersion = MoqDataGenerator.GetRandomNumber(1, 11)
    };

    private Configuration configuration = new Configuration
    {
      Port = MoqDataGenerator.GetRandomNumber(1, 10000)
    };

    private ProgramRobot programRobot = new ProgramRobot
    {
      CurrentVersion = MoqDataGenerator.GetRandomNumber(1, 11)
    };

    private int _amountRobotsWas;
    private int _amountProgramsWas;
    private int _amountConfigurationsWas;
    private int _amountProgramRobotsWas;

    [TestMethod]
    public void TestCRUD()
    {
      /*
      Сценарий:
        CREATE:
          0. Сущности уже созданы с тестовыми данными.
          1. Устанавливаем связи между сущностями
          2. Запоминаем количество каждой из сущностей
          3. Сохраняем сущности в БД.
          4. проверить, что наши экземпляры сущностей лежат в БД
          5. Проверяем количества 
          6. Проверяем связи
        UPDATE:
          7. поменяем все кроме ImageRecipe, ибо такие нужно не менять, а удалять
          8. сохраним изменения в БД
          9. проверить, что наши экземпляры сущностей по-прежнему лежат в БД
        REMOVE:
          10. запомним Id сущностей
          11. удалим из бд
          12. проверим количества 
          13. проверим, что таких сущностей теперь нету
      */

      CreateData();
      UpdateData();
      RemoveData();
    }

    private void CreateData()
    {
      // установим связи
      configuration.Robot = robot;
      programRobot.Robot = robot;
      programRobot.Program = program;

      // запомним текущие количества
      _amountConfigurationsWas = context.Configurations.Count();
      _amountProgramRobotsWas = context.ProgramRobots.Count();
      _amountProgramsWas = context.Programs.Count();
      _amountRobotsWas = context.Robots.Count();

      // добавим в бд
      context.Robots.Add(robot);
      context.Programs.Add(program);
      context.ProgramRobots.Add(programRobot);
      context.Configurations.Add(configuration);
      context.SaveChanges();

      // проверить, что наши экземпляры сущностей лежат в БД
      CheckEntitiesIsInDB();
    }

    private void UpdateData()
    {
      // поменяем все кроме ImageRecipe, ибо такие нужно не менять, а удалять
      program.Name = MoqDataGenerator.GetRandomString(10);
      programRobot.CurrentVersion = MoqDataGenerator.GetRandomNumber(10, 100);
      configuration.Port = MoqDataGenerator.GetRandomNumber(10, 11111);

      // сохраним изменения в БД
      context.Entry(program).State = EntityState.Modified;
      context.Entry(programRobot).State = EntityState.Modified;
      context.Entry(configuration).State = EntityState.Modified;
      context.SaveChanges();

      // проверить, что наши экземпляры сущностей по-прежнему лежат в БД
      CheckEntitiesIsInDB();
    }

    private void RemoveData()
    {
      // запомним Id сущностей
      int programId = program.ProgramID;
      int robotId = robot.RobotID;
      int programRobotId = programRobot.ProgramRobotID;
      int configurationId = configuration.ConfigurationID;

      // удалим из бд
      context.Robots.Remove(robot);
      context.Configurations.Remove(configuration);
      context.ProgramRobots.Remove(programRobot);
      context.Programs.Remove(program);
      context.SaveChanges();

      // проверим количества 
      Assert.AreEqual(_amountRobotsWas, context.Robots.Count());
      Assert.AreEqual(_amountConfigurationsWas, context.Configurations.Count());
      Assert.AreEqual(_amountProgramRobotsWas, context.ProgramRobots.Count());
      Assert.AreEqual(_amountProgramsWas, context.Programs.Count());

      // проверим, что таких сущностей теперь нету
      Assert.AreEqual(null, context.Programs.FirstOrDefault(x => x.ProgramID == programId));
      Assert.AreEqual(null, context.Robots.FirstOrDefault(x => x.RobotID == robotId));
      Assert.AreEqual(null, context.ProgramRobots.FirstOrDefault(x => x.ProgramRobotID == programRobotId));
      Assert.AreEqual(null, context.Configurations.FirstOrDefault(x => x.ConfigurationID == configurationId));
    }

    // проверить, что наши экземпляры сущностей лежат в БД
    private void CheckEntitiesIsInDB()
    {
      Assert.AreSame(robot, context.Robots.ToList().Last());
      Assert.AreSame(program, context.Programs.ToList().Last());
      Assert.AreSame(configuration, context.Configurations.ToList().Last());
      Assert.AreSame(programRobot, context.ProgramRobots.ToList().Last());
    }
  }

}
