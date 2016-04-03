using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Domain.Entities;
using Domain.Services;

namespace Domain.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Robot> Robots { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<ProgramRobot> ProgramRobots { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RobotCommand> RobotCommands { get; set; }
    public DbSet<Image> Images { get; set; }

    // применить доступные изменения из БД, поумолчанию измененные объекты не подтягиваются
    public void RefreshModified()
    {
      var ctx = ((IObjectContextAdapter)this).ObjectContext;
      // Get all objects in statemanager with entityKey 
      // (context.Refresh will throw an exception otherwise) 
      var objects = (from entry in ctx.ObjectStateManager.GetObjectStateEntries(
                                                 EntityState.Added
                                                | EntityState.Deleted
                                                | EntityState.Modified
                                                | EntityState.Unchanged)
                     where entry.EntityKey != null
                     select entry.Entity);
      ctx.Refresh(RefreshMode.StoreWins, objects);
    }

  }
}
