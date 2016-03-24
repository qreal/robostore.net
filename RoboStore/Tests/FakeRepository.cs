using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;

namespace Tests
{
  public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    private List<TEntity> data = new List<TEntity>();

    public IEnumerable<TEntity> Data => data;

    public Task AddAsync(TEntity entity)
    {
      return Task.Factory.StartNew(() =>
      {
        data.Add(entity);
      });
    }

    public Task UpdateAsync(TEntity entity)
    {

      return Task.Factory.StartNew(() =>
      {
        // ничего не делаем, потому что код пользователя уже обновил сущность
      });
    }

    public Task RemoveAsync(TEntity entity)
    {
      return Task.Factory.StartNew(() =>
      {
        data = data.Where(x => x != entity).ToList();
      });
    }
  }
}
