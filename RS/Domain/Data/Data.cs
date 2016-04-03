using Domain.Entities;
using Domain.Services;

/*
Используется паттерн singeton
*/

namespace Domain.Data
{
  public class Data : Singleton<Data>, IData
  {
    private DataContext _context = new DataContext();

    public IRepository<Configuration> Configurations { get; }
    public IRepository<Robot> Robots { get;  }
    public IRepository<Program> Programs { get;  }
    public IRepository<ProgramRobot> ProgramRobots { get; }
    public IRepository<User> Users { get;  }
    public IRepository<RobotCommand> RobotCommands { get; }
    public IRepository<Image> Images { get; }

    private Data()
    {
      Configurations = new EFRepository<Configuration>(_context);
      Robots = new EFRepository<Robot>(_context);
      Programs = new EFRepository<Program>(_context);
      ProgramRobots = new EFRepository<ProgramRobot>(_context);
      Users = new EFRepository<User>(_context);
      RobotCommands = new EFRepository<RobotCommand>(_context);
      Images = new EFRepository<Image>(_context);
    }
  }
}
