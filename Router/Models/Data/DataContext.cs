using System.Data.Entity;
using Router.Models.Entities;

namespace Router.Models.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<Robot> Robots { get; set; }
    public DbSet<StoredMessage> Messages { get; set; } 
  }
}
