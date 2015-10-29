using Microsoft.Data.Entity;
using System.Collections.Generic;
using Roboto.Models.Entities;

namespace Roboto.Models
{
  public class DataContext : DbContext
  {
    public  DbSet<User> Users { get; set; }
    public DbSet<Robot> Robots { get; set; }  
  }
}
