using System.Data.Entity;
using Store.Models.Entities;

namespace Store.Models.Data
{
  public class DataContext : DbContext
  {
    public DbSet<ConfigurationE> Configurations { get; set; }
    public DbSet<RobotE> Robots { get; set; }
    public DbSet<StoredMessageE> Messages { get; set; }
  }
}
