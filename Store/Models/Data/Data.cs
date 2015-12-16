using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Data
{
  public class Data : IData
  {
    private readonly RobotDBEntities _context;
    private readonly GenericRepository<Configuration> _configurations;
    private readonly GenericRepository<Robot> _robots;
    private readonly GenericRepository<Message> _messages;

    public Data()
    {
      _context = new RobotDBEntities();
      _configurations = new GenericRepository<Configuration>(_context);
      _messages = new GenericRepository<Message>(_context);
      _robots = new GenericRepository<Robot>(_context);
    }

    public IEnumerable<Configuration> Configurations => _context.Configurations;
    public IEnumerable<Robot> Robots => _context.Robots;
    public IEnumerable<Message> Messages => _context.Messages;


    public async Task<int> AddAsync(object o)
    {
      var objectName = o.GetType().ToString().Split('.').Last();
      switch (objectName)
      {
        case "Configuration":
          _configurations.Add((Configuration)o);
          break;
        case "Message":
          _messages.Add((Message)o);
          break;
        case "Robot":
          _robots.Add((Robot)o);
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
        case "Message":
          _messages.Update((Message)o);
          break;
        case "Robot":
          _robots.Update((Robot)o);
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
          _configurations.Remove(((Configuration)o).ConfigurationID);
          break;
        case "Message":
          _messages.Remove(((Message)o).MessageID);
          break;
        case "Robot":
          _robots.Remove(((Robot)o).RobotID);
          break;
      }
      await _context.SaveChangesAsync();
    }

  }
}
