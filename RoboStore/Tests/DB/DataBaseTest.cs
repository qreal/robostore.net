using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Services;

/*
CRUD тесты для всех сущностей в БД
*/

namespace Tests.DB
{
  [TestClass]
  public class DataBaseTest
  {
    private readonly RobotsDBEntities1 _context = new RobotsDBEntities1();

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

    private Image image = new Image()
    {
      ImageData = MoqDataGenerator.GetSomeBytes(),
      ImageMimeType = MoqDataGenerator.GetRandomString(6)
    };

    private int _amountRobotsWas;
    private int _amountProgramsWas;
    private int _amountConfigurationsWas;
    private int _amountProgramRobotsWas;
    private int _amountImagesWas;

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
      program.Image = image;
      programRobot.Robot = robot;
      programRobot.Program = program;

      // запомним текущие количества
      _amountConfigurationsWas = _context.Configurations.Count();
      _amountProgramRobotsWas = _context.ProgramRobots.Count();
      _amountProgramsWas = _context.Programs.Count();
      _amountRobotsWas = _context.Robots.Count();
      _amountImagesWas = _context.Images.Count();

      // добавим в бд
      _context.Images.Add(image);
      _context.Robots.Add(robot);
      _context.Programs.Add(program);
      _context.ProgramRobots.Add(programRobot);
      _context.Configurations.Add(configuration);
      _context.SaveChanges();

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
      _context.Entry(program).State = EntityState.Modified;
      _context.Entry(programRobot).State = EntityState.Modified;
      _context.Entry(configuration).State = EntityState.Modified;
      _context.SaveChanges();

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
      int imageId = image.ImageID;

      // удалим из бд
      _context.Robots.Remove(robot);
      _context.Configurations.Remove(configuration);
      _context.ProgramRobots.Remove(programRobot);
      _context.Programs.Remove(program);
      _context.Images.Remove(image);
      _context.SaveChanges();

      // проверим количества 
      Assert.AreEqual(_amountRobotsWas, _context.Robots.Count());
      Assert.AreEqual(_amountConfigurationsWas, _context.Configurations.Count());
      Assert.AreEqual(_amountProgramRobotsWas, _context.ProgramRobots.Count());
      Assert.AreEqual(_amountProgramsWas, _context.Programs.Count());
      Assert.AreEqual(_amountImagesWas, _context.Images.Count());

      // проверим, что таких сущностей теперь нету
      Assert.AreEqual(null, _context.Programs.FirstOrDefault(x => x.ProgramID == programId));
      Assert.AreEqual(null, _context.Robots.FirstOrDefault(x => x.RobotID == robotId));
      Assert.AreEqual(null, _context.ProgramRobots.FirstOrDefault(x => x.ProgramRobotID == programRobotId));
      Assert.AreEqual(null, _context.Configurations.FirstOrDefault(x => x.ConfigurationID == configurationId));
      Assert.AreEqual(null, _context.Images.FirstOrDefault(x => x.ImageID == imageId));
    }

    // проверить, что наши экземпляры сущностей лежат в БД
    private void CheckEntitiesIsInDB()
    {
      Assert.AreSame(robot, _context.Robots.ToList().Last());
      Assert.AreSame(program, _context.Programs.ToList().Last());
      Assert.AreSame(configuration, _context.Configurations.ToList().Last());
      Assert.AreSame(programRobot, _context.ProgramRobots.ToList().Last());
      Assert.AreSame(image, _context.Images.ToList().Last());
    }
  }

}
