using System.Collections.Generic;

namespace Store.Models.Program
{
  public class ProgramAndMyRobots
  {
    public Domain.Entities.Program Program { get; set; }
    public IEnumerable<Domain.Entities.Robot> MyRobots { get; set; }
  }
}
