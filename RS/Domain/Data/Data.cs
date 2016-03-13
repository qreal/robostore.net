using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Data
{
  public class Data : IData
  {
    private readonly DataContext _context;
    private readonly GenericRepository<Configuration> _configurations;
    private readonly GenericRepository<Robot> _robots;
    private readonly GenericRepository<Program> _programs;
    private readonly GenericRepository<ProgramRobot> _programRobots;

    public Data()
    {
      _context = new DataContext();
      _configurations = new GenericRepository<Configuration>(_context);
      _programs = new GenericRepository<Program>(_context);
      _robots = new GenericRepository<Robot>(_context);
      _programRobots = new GenericRepository<ProgramRobot>(_context);
    }

    public IEnumerable<Configuration> Configurations => _context.Configurations;
    public IEnumerable<Robot> Robots => _context.Robots;
    public IEnumerable<Program> Programs => _context.Programs;
    public IEnumerable<ProgramRobot> ProgramRobots => _context.ProgramRobots; 


    public async Task<int> AddAsync(object o)
    {
      var objectName = o.GetType().ToString().Split('.').Last();
      switch (objectName)
      {
        case "Configuration":
          _configurations.Add((Configuration)o);
          break;
        case "Program":
          _programs.Add((Program)o);
          break;
        case "Robot":
          _robots.Add((Robot)o);
          break;
        case "ProgramRobot":
          _programRobots.Add((ProgramRobot)o);
          break;
      }
      return await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(object o)
    {
      var objectName = o.GetType().ToString().Split('.').Last();
      switch (objectName)
      {
        case "Configuration":
          _configurations.Update((Configuration)o);
          break;
        case "Program":
          _programs.Update((Program)o);
          break;
        case "Robot":
          _robots.Update((Robot)o);
          break;
        case "ProgramRobot":
          _programRobots.Update((ProgramRobot)o);
          break;
      }
      await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(object o)
    {
      var objectName = o.GetType().ToString().Split('.').Last();
      switch (objectName)
      {
        case "Configuration":
          _configurations.Remove((Configuration)o);
          break;
        case "Program":
          _programs.Remove((Program)o);
          break;
        case "Robot":
          _robots.Remove((Robot)o);
          break;
        case "ProgramRobot":
          _programRobots.Remove((ProgramRobot)o);
          break;
      }
      await _context.SaveChangesAsync();
    }

  }
}
