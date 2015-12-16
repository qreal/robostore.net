using System.Data.Entity;

/*
Класс выполняет CRUD с таблицей БД
*/

namespace Router.Models.Data
{
  public class GenericRepository<TEntity> where TEntity : class
  {
    internal DataContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(DataContext context)
    {
      this.context = context;
      this.dbSet = context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
      dbSet.Add(entity);
    }

    public void Remove(int id)
    {
      TEntity entityToDelete = dbSet.Find(id);
      if (context.Entry(entityToDelete).State == EntityState.Detached)
      {
        dbSet.Attach(entityToDelete);
      }
      dbSet.Remove(entityToDelete);
    }

    public void Update(TEntity entityToUpdate)
    {
      dbSet.Attach(entityToUpdate);
      context.Entry(entityToUpdate).State = EntityState.Modified;
    }
  }
}
