using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;
using Tests.Services;

namespace Tests
{
  /*
  Класс реализует интерфейс данных из БД, чтобы проверить бизнесс логику
  */

  public class FakeData: IData
  {
    private List<Robot> _robots = new List<Robot>();
    private List<Program> _programs = new List<Program>();
    private List<Configuration> _configurations = new List<Configuration>();
    private List<ProgramRobot> _programRobots = new List<ProgramRobot>();

    public FakeData()
    {
      // создали по одной сущности в каждый из списков
      var robot = new Robot
      {
        RobotID = MoqDataGenerator.GetRandomNumber(1, 100),
        Configurations = new List<Configuration>(),
        ProgramRobots = new List<ProgramRobot>()
      };
      var program = CreateProgram(id:1);

      var configuration = new Configuration
      {
        ConfigurationID = MoqDataGenerator.GetRandomNumber(10, 100),
        Port = MoqDataGenerator.GetRandomNumber(10, 11111)
      };
      var programRobot = new ProgramRobot
      {
        CurrentVersion = MoqDataGenerator.GetRandomNumber(10, 100),
        ProgramRobotID = MoqDataGenerator.GetRandomNumber(10, 100)
      };

      var image = new Image
      {
        ImageMimeType = MoqDataGenerator.GetRandomString(10),
        ImageData = MoqDataGenerator.GetSomeBytes(),
        ImageID = 1
      };

      // добавили связи между сущностями
      robot.Configurations.Add(configuration);
      robot.ProgramRobots.Add(programRobot);
      configuration.Robot = robot;
      configuration.RobotID = robot.RobotID;
      program.ProgramRobots.Add(programRobot);
      program.Image = image;
      program.ImageID = image.ImageID;
      programRobot.Robot = robot;
      programRobot.Program = program;
      programRobot.RobotID = robot.RobotID;
      programRobot.ProgramID = program.ProgramID;

      // добавили сущности в списки сущностей
      _robots.Add(robot);
      _programs.Add(program);
      _configurations.Add(configuration);
      _programRobots.Add(programRobot);

      // добавили еще 4 программы для теста pagination
      for (var i = 0; i < 4; i++)
      {
        _programs.Add(CreateProgram(i + 2));
      }
    }

    public IEnumerable<Robot> Robots => _robots;
    public IEnumerable<Program> Programs => _programs;
    public IEnumerable<Configuration> Configurations => _configurations;
    public IEnumerable<ProgramRobot> ProgramRobots => _programRobots;

    public Task<int> AddAsync(object o)
    {
      return Task<int>.Factory.StartNew(() =>

      {
        // тут нужно получить тип объекта и далее его добавить/удалить в Репозиторий
        var objectName = o.GetType().ToString().Split('.').Last();

        switch (objectName)
        {
          case "Robot":
            _robots.Add((Robot)o);
            break;
          case "Program":
            _programs.Add((Program)o);
            break;
          case "Configuration":
            _configurations.Add((Configuration)o);
            break;
          case "ProgramRobot":
            _programRobots.Add((ProgramRobot)o);
            break;
        }
        // 42 потому что никаких статусных кодов мы не возвращаем
        return 42;
      });
    }

    public Task UpdateAsync(object o)
    {
      return Task.Factory.StartNew(() =>
      {
        // тут нужно получить тип объекта и далее его добавить/удалить в Репозиторий
        var objectName = o.GetType().ToString().Split('.').Last();

        switch (objectName)
        {
          case "Robot":
            _robots = _robots.FindAll(x => x.RobotID != ((Robot)o).RobotID);
            _robots.Add((Robot)o);
            break;
          case "Program":
            _programs = _programs.FindAll(x => x.ProgramID != ((Program)o).ProgramID);
            _programs.Add((Program)o);
            break;
          case "Configuration":
            _configurations = _configurations.FindAll(x => x.ConfigurationID != ((Configuration)o).ConfigurationID);
            _configurations.Add((Configuration)o);
            break;
          case "ProgramRobot":
            // выпилил элемент с таким ID
            _programRobots = _programRobots.FindAll(x => x.ProgramRobotID != ((ProgramRobot)o).ProgramRobotID);
            // и добавил измененный 
            _programRobots.Add((ProgramRobot)o);
            break;
        }
      });
    }

    public Task RemoveAsync(object o)
    {
      return Task.Factory.StartNew(() =>
      {
        // тут нужно получить тип объекта и далее его добавить/удалить в Репозиторий
        var objectName = o.GetType().ToString().Split('.').Last();

        switch (objectName)
        {
          case "Robot":
            _robots =
              _robots.FindAll(x => x.RobotID != ((Robot)o).RobotID);
            break;
          case "Program":
            _programs = _programs.FindAll(x => x.ProgramID != ((Program)o).ProgramID);
            break;
          case "Configuration":
            _configurations = _configurations.FindAll(x => x.ConfigurationID != ((Configuration)o).ConfigurationID);
            break;
          case "ProgramRobot":
            // выпилил элемент с таким ID
            _programRobots = _programRobots.FindAll(x => x.ProgramRobotID != ((ProgramRobot)o).ProgramRobotID);
            break;
        }
      });
    }

    private Program CreateProgram(int id) =>
      new Program
    {
      ActualVersion = MoqDataGenerator.GetRandomNumber(1, 10),
      Code = MoqDataGenerator.GetRandomString(100),
      Name = MoqDataGenerator.GetRandomString(10),
      ProgramID = id,
      ProgramRobots = new List<ProgramRobot>()
    };
  }
}
