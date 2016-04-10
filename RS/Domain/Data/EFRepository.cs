using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Data
{
  public class EFRepository <TEntity> : IRepository<TEntity> where TEntity : class
  {
    private DataContext context;
    private DbSet<TEntity> dbSet;

    public EFRepository(DataContext c)
    {
      context = c;
      dbSet = context.Set<TEntity>();
    }

    public IEnumerable<TEntity> Data
    {

      get
      {
        var list = dbSet.ToList();
        foreach (var data in list)
        {
          context.Entry(data).Reload();
        }
        return list;
      }
    }

    public void Add(TEntity entity)
    {
      dbSet.Add(entity);
      context.SaveChanges();
    }

    public void Update(TEntity entity)
    {
      dbSet.Attach(entity);
      context.Entry(entity).State = EntityState.Modified;
      context.SaveChanges();
    }

    public void Remove(TEntity entity)
    {
      dbSet.Remove(entity);
      context.SaveChanges();
    }
  }
}
