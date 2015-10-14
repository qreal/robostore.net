using System.Collections.Generic;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;

namespace Robots.Domain.Concrete
{
  public class EFRobotRepository : ICommonRepository<Robot>
  {
    private EFDbContextRobots context = new EFDbContextRobots();

    public IEnumerable<Robot> Data
    {
      get { return context.Robots; }
    }

    public void SaveData(Robot Robot)
    {
      if (Robot.RobotID == 0)
      {
        context.Robots.Add(Robot);
      }
      else
      {
        Robot dbEntry = context.Robots.Find(Robot.RobotID);
        if (dbEntry != null)
        {
          // Копируем все кроме RobotID ибо это Primary Key в БД
          dbEntry.Name = Robot.Name;
          dbEntry.ModelConfig = Robot.ModelConfig;
          dbEntry.Program = Robot.Program;
          dbEntry.SSID = Robot.SSID;
          dbEntry.Status = Robot.Status;
          dbEntry.SystemConfig = Robot.SystemConfig;
          dbEntry.UserID = Robot.UserID;
        }
      }
      context.SaveChanges();
    }

    public Robot DeleteData(int RobotID)
    {
      Robot dbEntry = context.Robots.Find(RobotID);
      if (dbEntry != null)
      {
        context.Robots.Remove(dbEntry);
        context.SaveChanges();
      }
      return dbEntry;
    }
  }
}
