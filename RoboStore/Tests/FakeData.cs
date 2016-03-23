using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Data;
using Domain.Entities;

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
    private List<User> _users = new List<User>();
    private List<RobotCommand> _robotCommands = new List<RobotCommand>(); 

    public FakeData()
    {
      // создали по одной сущности в каждый из списков
      var user = new User
      {
        Login = MoqDataGenerator.GetRandomString(10),
        Password = MoqDataGenerator.GetRandomString(10),
        UserID = 1
      };
      var robot = new Robot
      {
        RobotID = MoqDataGenerator.GetRandomNumber(1, 100),
        Configurations = new List<Configuration>(),
        ProgramRobots = new List<ProgramRobot>(),
        ActivationCode = MoqDataGenerator.GetRandomNumber(1,100)
      };
      var program = CreateProgram(id:1);

      var configuration = new Configuration
      {
        ConfigurationID = MoqDataGenerator.GetRandomNumber(10, 100),
        Port = MoqDataGenerator.GetRandomNumber(10, 11111)
      };
      var programRobot = new ProgramRobot
      {
        ProgramRobotID = MoqDataGenerator.GetRandomNumber(10, 100)
      };

      var image = new Image
      {
        ImageMimeType = MoqDataGenerator.GetRandomString(10),
        ImageData = MoqDataGenerator.GetSomeBytes(),
        ImageID = 1
      };

      var robotCommand = new RobotCommand
      {
        RobotCommandID = MoqDataGenerator.GetRandomNumber(10, 100),
        Type = 0
      };

      // добавили связи между сущностями
      robot.Configurations.Add(configuration);
      robot.ProgramRobots.Add(programRobot);
      robot.User = user;
      robot.UserID = user.UserID;
      configuration.Robot = robot;
      configuration.RobotID = robot.RobotID;
      program.ProgramRobots.Add(programRobot);
      program.Image = image;
      program.ImageID = image.ImageID;
      programRobot.Robot = robot;
      programRobot.Program = program;
      programRobot.RobotID = robot.RobotID;
      programRobot.ProgramID = program.ProgramID;
      programRobot.CurrentVersion = program.ActualVersion - 1;
      robotCommand.Argument = program.ProgramID;
      robotCommand.Robot = robot;
      robotCommand.RobotID = robot.RobotID;

      // добавили сущности в списки сущностей
      _robots.Add(robot);
      _programs.Add(program);
      _configurations.Add(configuration);
      _programRobots.Add(programRobot);
      _users.Add(user);
      _robotCommands.Add(robotCommand);

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
    public IEnumerable<User> Users => _users;
    public IEnumerable<RobotCommand> RobotCommands => _robotCommands; 

    public Task<object> AddAsync(object o)
    {
      return Task<object>.Factory.StartNew(() =>

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
          case "User":
            _users.Add((User)o);
            break;
          case "RobotCommand":
            _robotCommands.Add((RobotCommand)o);
            break;
        }
        return o;
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
          case "User":
            _users = _users.FindAll(x => x.UserID != ((User)o).UserID);
            _users.Add((User)o);
            break;
          case "RobotCommand":
            _robotCommands = _robotCommands.FindAll(x => x.RobotCommandID != ((RobotCommand)o).RobotCommandID);
            _robotCommands.Add((RobotCommand)o);
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
          case "User":
            _users = _users.FindAll(x => x.UserID != ((User)o).UserID);
            break;
          case "RobotCommand":
            _robotCommands = _robotCommands.FindAll(x => x.RobotCommandID != ((RobotCommand)o).RobotCommandID);
            break;
        }
      });
    }

    private Program CreateProgram(int id) =>
      new Program
    {
      ActualVersion = MoqDataGenerator.GetRandomNumber(10, 100),
      Code = MoqDataGenerator.GetRandomString(100),
      Name = MoqDataGenerator.GetRandomString(10),
      ProgramID = id,
      ProgramRobots = new List<ProgramRobot>()
    };
  }
}
