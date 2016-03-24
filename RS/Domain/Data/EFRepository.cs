using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Domain.Data
{
  public class EFRepository <TEntity> : IRepository<TEntity> where TEntity : class
  {
    private DataContext context;
    private DbSet<TEntity> dbSet;

    public EFRepository(DataContext c, IEnumerable<TEntity> d)
    {
      context = c;
      dbSet = context.Set<TEntity>();
      Data = d;
    }

    public IEnumerable<TEntity> Data { get; }

    public async Task AddAsync(TEntity entity)
    {
      dbSet.Add(entity);
      await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
      dbSet.Attach(entity);
      context.Entry(entity).State = EntityState.Modified;
      await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
      dbSet.Remove(entity);
      await context.SaveChangesAsync();
    }
  }
}
