using System.Collections.Generic;

namespace Store.Models.Program
{
  public class ProgramSummary
  {
    public Domain.Entities.Program Program { get; set; }
    public int ProgramId { get; set; }
    public List<int> RobotIds { get; set; }
  }
}
