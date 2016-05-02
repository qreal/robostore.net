using System.Data.Entity;
using Domain.Entities;

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
  }
}
