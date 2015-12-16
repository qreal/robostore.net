using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Models.Entities;

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
