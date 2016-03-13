using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Data
{
  public interface IData
  {
    IEnumerable<Configuration> Configurations { get; }
    IEnumerable<Robot> Robots { get; }
    IEnumerable<Program> Programs { get; }
    IEnumerable<ProgramRobot> ProgramRobots { get; }
    Task<int> AddAsync(object o);
    Task UpdateAsync(object o);
    Task RemoveAsync(object o);
  }
}
