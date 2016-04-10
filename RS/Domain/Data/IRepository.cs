using System.Collections.Generic;

namespace Domain.Data
{
  public interface IRepository<T>
  {
    IEnumerable<T> Data { get; }
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
  }
}
