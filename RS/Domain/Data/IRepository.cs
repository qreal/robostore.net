using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Data
{
  public interface IRepository<T>
  {
    IEnumerable<T> Data { get; }
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
  }
}
