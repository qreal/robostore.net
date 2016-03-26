using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Data;
using Domain.Entities;

namespace Tests
{
  /*
  Класс реализует интерфейс данных, чтобы проверить бизнесс логику
  */

  public class FakeData: IData
  {
    public IRepository<Robot> Robots { get; } = new FakeRepository<Robot>();
    public IRepository<Program> Programs { get; } = new FakeRepository<Program>();
    public IRepository<Configuration> Configurations { get; } = new FakeRepository<Configuration>();
    public IRepository<ProgramRobot> ProgramRobots { get; } = new FakeRepository<ProgramRobot>();
    public IRepository<User> Users { get; } = new FakeRepository<User>();
    public IRepository<RobotCommand> RobotCommands { get; } = new FakeRepository<RobotCommand>();
    public IRepository<Image> Images { get; } = new FakeRepository<Image>(); 

    public FakeData()
    {
      AsyncHelpers.RunSync(FillWithTestData);
    }

    private async Task FillWithTestData()
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
        ActivationCode = MoqDataGenerator.GetRandomNumber(1, 100)
      };
      var program = CreateProgram(id: 1);

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
        ImageID = 1,
        Name = MoqDataGenerator.GetRandomString(10)
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
      await Robots.AddAsync(robot);
      await Programs.AddAsync(program);
      await Configurations.AddAsync(configuration);
      await ProgramRobots.AddAsync(programRobot);
      await Users.AddAsync(user);
      await RobotCommands.AddAsync(robotCommand);
      await Images.AddAsync(image);

      // добавили еще 4 программы для теста pagination
      for (var i = 0; i < 4; i++)
      {
        await Programs.AddAsync(CreateProgram(i + 2));
      }
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
