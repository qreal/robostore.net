using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Data
{
  public class GenericRepository<TEntity> where TEntity : class
  {
    internal RobotDBEntities context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(RobotDBEntities context)
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
