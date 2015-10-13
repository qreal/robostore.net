using System.Data.Entity;
using Robots.Domain.Entities;

/*
Класс для работы Entity Framework с сущность User
*/

namespace Robots.Domain.Concrete
{
  public class EFDbContextRobots : DbContext
  {
    public DbSet<User> Users { get; set; }
  }
}
