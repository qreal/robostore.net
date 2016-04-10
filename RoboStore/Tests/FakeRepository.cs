using System.Collections.Generic;
using System.Linq;
using Domain.Data;

namespace Tests
{
  public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    private List<TEntity> data = new List<TEntity>();

    public IEnumerable<TEntity> Data => data;

    public void  Add(TEntity entity)
    {
      data.Add(entity);
    }

    public void Update(TEntity entity)
    {
      // ничего не делаем, потому что код пользователя уже обновил сущность
    }

    public void Remove(TEntity entity)
    {
      data = data.Where(x => x != entity).ToList();
    }
  }
}
