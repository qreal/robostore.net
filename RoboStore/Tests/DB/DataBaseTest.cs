using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/*
CRUD тесты для всех сущностей в БД
*/

namespace Tests.DB
{
  [TestClass]
  public class DataBaseTest
  {
    private readonly RDBEntities4 _context = new RDBEntities4();

    // создадим тестовые данные

    private User user = new User
    {
      Login = MoqDataGenerator.GetRandomString(10),
      Password = MoqDataGenerator.GetRandomString(10)
    };

    private Robot robot = new Robot
    {
      ActivationCode = MoqDataGenerator.GetRandomNumber(0,9999)
    };

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

    private Image image = null;

    private RobotCommand robotCommand = new RobotCommand
    {
      Argument = MoqDataGenerator.GetRandomNumber(1, 100),
      Type = MoqDataGenerator.GetRandomNumber(1, 100)
    };

    private int _amountRobotsWas;
    private int _amountProgramsWas;
    private int _amountConfigurationsWas;
    private int _amountProgramRobotsWas;
    private int _amountImagesWas;
    private int _amountUsersWas;
    private int _amountRobotCommandsWas;

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

      CheckCreateData();
      CheckUpdateData();
      CheckRemoveData();
      //string[] filePaths = Directory.GetFiles(@"img\");
      //foreach (var file in filePaths)
      //{
      //  loadImage(file.Split('.')[0], file);
      //}
    }

    // tmp
    //public void loadImage(string name, string file)
    //{
    //  Image img = new Image
    //  {
    //    Name = name
    //  };
    //  // загружаем в базу тестовую картинку
    //  using (FileStream fsSource = new FileStream(file,
    //    FileMode.Open, FileAccess.Read))
    //  {
    //    byte[] bytes = new byte[fsSource.Length];
    //    int numBytesToRead = (int)fsSource.Length;
    //    int numBytesRead = 0;
    //    while (numBytesToRead > 0)
    //    {
    //      // Read may return anything from 0 to numBytesToRead.
    //      int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

    //      // Break when the end of the file is reached.
    //      if (n == 0)
    //        break;

    //      numBytesRead += n;
    //      numBytesToRead -= n;
    //    }

    //    // сохраняем картинку в БД
    //    img.ImageData = bytes;
    //    // получаем MimeType файла
    //    img.ImageMimeType = MimeMapping.GetMimeMapping(file);
    //  }
    //  _context.Images.Add(img);
    //  _context.SaveChanges();
    //}

    private Image LoadTestImageFromFile()
    {
      Image img = new Image
      {
        Name = "test_imageForDBTest"
      };
      // загружаем в базу тестовую картинку
      using (FileStream fsSource = new FileStream("test_image.jpg",
        FileMode.Open, FileAccess.Read))
      {
        byte[] bytes = new byte[fsSource.Length];
        int numBytesToRead = (int)fsSource.Length;
        int numBytesRead = 0;
        while (numBytesToRead > 0)
        {
          // Read may return anything from 0 to numBytesToRead.
          int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

          // Break when the end of the file is reached.
          if (n == 0)
            break;

          numBytesRead += n;
          numBytesToRead -= n;
        }

        // сохраняем картинку в БД
        img.ImageData = bytes;
        // получаем MimeType файла
        img.ImageMimeType = MimeMapping.GetMimeMapping("test_image.jpg");
      }
      return img;
    }

    private void CheckCreateData()
    {
      // возьмем картинку из файла
      image = LoadTestImageFromFile();

      // установим связи
      configuration.Robot = robot;
      program.Image = image;
      programRobot.Robot = robot;
      programRobot.Program = program;
      robot.User = user;
      robotCommand.Robot = robot;

      // запомним текущие количества
      _amountConfigurationsWas = _context.Configurations.Count();
      _amountProgramRobotsWas = _context.ProgramRobots.Count();
      _amountProgramsWas = _context.Programs.Count();
      _amountRobotsWas = _context.Robots.Count();
      _amountImagesWas = _context.Images.Count();
      _amountUsersWas = _context.Users.Count();
      _amountRobotCommandsWas = _context.RobotCommands.Count();

      // добавим в бд
      _context.Users.Add(user);
      _context.Images.Add(image);
      _context.Robots.Add(robot);
      _context.Programs.Add(program);
      _context.ProgramRobots.Add(programRobot);
      _context.Configurations.Add(configuration);
      _context.RobotCommands.Add(robotCommand);
      _context.SaveChanges();

      // проверим количества
      Assert.AreEqual(_amountConfigurationsWas + 1, _context.Configurations.Count());
      Assert.AreEqual(_amountProgramRobotsWas + 1, _context.ProgramRobots.Count());
      Assert.AreEqual(_amountProgramsWas + 1, _context.Programs.Count());
      Assert.AreEqual(_amountRobotsWas + 1, _context.Robots.Count());
      Assert.AreEqual(_amountImagesWas + 1, _context.Images.Count());
      Assert.AreEqual(_amountUsersWas + 1, _context.Users.Count());
      Assert.AreEqual(_amountRobotCommandsWas + 1, _context.RobotCommands.Count());

      // проверить, что наши экземпляры сущностей лежат в БД
      CheckEntitiesIsInDb();
    }

    private void CheckUpdateData()
    {
      // поменяем все кроме ImageRecipe, ибо такие нужно не менять, а удалять
      program.Name = MoqDataGenerator.GetRandomString(10);
      programRobot.CurrentVersion = MoqDataGenerator.GetRandomNumber(10, 100);
      configuration.Port = MoqDataGenerator.GetRandomNumber(10, 11111);
      user.Login = MoqDataGenerator.GetRandomString(10);
      user.Password = MoqDataGenerator.GetRandomString(10);
      robot.ActivationCode = MoqDataGenerator.GetRandomNumber(0, 9999);
      robotCommand.Argument = MoqDataGenerator.GetRandomNumber(1, 100);
      robotCommand.Type = MoqDataGenerator.GetRandomNumber(1, 100);

      // сохраним изменения в БД
      _context.Entry(program).State = EntityState.Modified;
      _context.Entry(programRobot).State = EntityState.Modified;
      _context.Entry(configuration).State = EntityState.Modified;
      _context.Entry(user).State = EntityState.Modified;
      _context.Entry(robot).State = EntityState.Modified;
      _context.Entry(robotCommand).State = EntityState.Modified;
      _context.SaveChanges();

      // проверить, что наши экземпляры сущностей по-прежнему лежат в БД
      CheckEntitiesIsInDb();
    }

    private void CheckRemoveData()
    {
      // запомним Id сущностей
      int programId = program.ProgramID;
      int robotId = robot.RobotID;
      int programRobotId = programRobot.ProgramRobotID;
      int configurationId = configuration.ConfigurationID;
      int imageId = image.ImageID;
      int userId = user.UserID;
      int robotCommandId = robotCommand.RobotCommandID;

      // удалим из бд (каскадно)
      _context.Robots.Remove(robot);
      _context.Users.Remove(user);
      _context.Programs.Remove(program);
      _context.Images.Remove(image);
      _context.SaveChanges();

      // проверим количества 
      Assert.AreEqual(_amountRobotsWas, _context.Robots.Count());
      Assert.AreEqual(_amountConfigurationsWas, _context.Configurations.Count());
      Assert.AreEqual(_amountProgramRobotsWas, _context.ProgramRobots.Count());
      Assert.AreEqual(_amountProgramsWas, _context.Programs.Count());
      Assert.AreEqual(_amountImagesWas, _context.Images.Count());
      Assert.AreEqual(_amountUsersWas, _context.Users.Count());
      Assert.AreEqual(_amountRobotCommandsWas, _context.RobotCommands.Count());

      // проверим, что таких сущностей теперь нету
      Assert.AreEqual(null, _context.Programs.FirstOrDefault(x => x.ProgramID == programId));
      Assert.AreEqual(null, _context.Robots.FirstOrDefault(x => x.RobotID == robotId));
      Assert.AreEqual(null, _context.ProgramRobots.FirstOrDefault(x => x.ProgramRobotID == programRobotId));
      Assert.AreEqual(null, _context.Configurations.FirstOrDefault(x => x.ConfigurationID == configurationId));
      Assert.AreEqual(null, _context.Images.FirstOrDefault(x => x.ImageID == imageId));
      Assert.AreEqual(null, _context.Users.FirstOrDefault(x => x.UserID == userId));
      Assert.AreEqual(null, _context.RobotCommands.FirstOrDefault(x => x.RobotCommandID == robotCommandId));
    }

    private void CheckEntitiesIsInDb()
    {
      Assert.AreSame(robot, _context.Robots.ToList().Last());
      Assert.AreSame(program, _context.Programs.ToList().Last());
      Assert.AreSame(configuration, _context.Configurations.ToList().Last());
      Assert.AreSame(programRobot, _context.ProgramRobots.ToList().Last());
      Assert.AreSame(image, _context.Images.ToList().Last());
      Assert.AreSame(user, _context.Users.ToList().Last());
      Assert.AreSame(robotCommand, _context.RobotCommands.ToList().Last());
    }
  }

}
