using System.Data.Entity;
using Robots.Domain.Entities;

/*
Класс для работы Entity Framework с сущность User
*/

namespace Robots.Domain.Concrete
{
  public class EFDbContextUser : DbContext
  {
    public DbSet<User> Users { get; set; }
  }
}
