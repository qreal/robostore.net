using System.Data.Entity;

namespace Store.Models.Data
{
  public class GenericRepository<TEntity> where TEntity : class
  {
    internal DataContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(DataContext context)
    {
      this.context = context;
     dbSet = context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
      dbSet.Add(entity);
    }

    public void Remove(TEntity entity)
    {
      dbSet.Remove(entity);
    }

    public void Update(TEntity entityToUpdate)
    {
      dbSet.Attach(entityToUpdate);
      context.Entry(entityToUpdate).State = EntityState.Modified;
    }
  }
}
