using System.Data.Entity;
using Store.Models.Entities;

namespace Store.Models.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Robot> Robots { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<ProgramRobot> ProgramRobots { get; set; }

  }
}
