using System.Collections.Generic;
using System.Threading.Tasks;
using Router.Models.Entities;

/*
Интерфейс для работы с данными из БД.
Нужен для возможности эмилировать это взаимодействие.
*/

namespace Router.Models.Data
{
  public interface IData
  {
    IEnumerable<Configuration> Configurations { get; }
    IEnumerable<Robot> Robots { get;}
    IEnumerable<StoredMessage> Messages { get;}
    Task<int> AddAsync(object o);
    Task UpdateAsync(object o);
    Task RemoveAsync(object o);
  }
}
