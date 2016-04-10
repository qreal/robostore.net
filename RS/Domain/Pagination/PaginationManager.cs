using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Entities;

namespace Domain.Pagination
{
  public class PaginationManager
  {
    private IData data;

    public PaginationManager(IData d)
    {
      data = d;
    }

    public IEnumerable<Program> FormProgramPage(int pageSize, int page) => 
      data.Programs.Data.OrderBy(x => x.Name).
        Skip((page - 1)*pageSize).
        Take(pageSize);

  
    public IEnumerable<Robot> FormMyRobotPage(IEnumerable<Robot> robots, int pageSize, int page) =>
      robots.OrderBy(x => x.RobotID).
        Skip((page - 1)*pageSize).
        Take(pageSize);
  }
}
