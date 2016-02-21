using System.Collections.Generic;
using Newtonsoft.Json;

namespace Store.Models.Entities
{
  public class Program
  {
    public int ProgramID { get; set; }
    public int ActualVersion { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    [JsonIgnore]
    public virtual List<ProgramRobot> ProgramRobots { get; set; } 
  }
}
