using Domain.Entities;

namespace Domain.Data
{
  public class Data : IData
  {
    private readonly DataContext _context = new DataContext();

    public IRepository<Configuration> Configurations { get; }
    public IRepository<Robot> Robots { get; }
    public IRepository<Program> Programs { get; }
    public IRepository<ProgramRobot> ProgramRobots { get; }
    public IRepository<User> Users { get; }
    public IRepository<RobotCommand> RobotCommands { get; }
    public IRepository<Image> Images { get; } 

    public Data()
    {
      Configurations = new EFRepository<Configuration>(_context, _context.Configurations);
      Robots = new EFRepository<Robot>(_context, _context.Robots);
      Programs = new EFRepository<Program>(_context, _context.Programs);
      ProgramRobots = new EFRepository<ProgramRobot>(_context, _context.ProgramRobots);
      Users = new EFRepository<User>(_context, _context.Users);
      RobotCommands = new EFRepository<RobotCommand>(_context, _context.RobotCommands);
      Images = new EFRepository<Image>(_context, _context.Images);
    }
  }
}
