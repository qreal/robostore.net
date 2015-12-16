using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Models.Data
{
  public interface IData
  {
    IEnumerable<Configuration> Configurations { get; }
    IEnumerable<Robot> Robots { get; }
    IEnumerable<Message> Messages { get; }
    Task<int> AddAsync(object o);
    Task UpdateAsync(object o);
    Task RemoveAsync(object o);
  }
}
