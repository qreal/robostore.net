using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Entities
{
  public class Program
  {
    public int ProgramID { get; set; }
    public int ActualVersion { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    [JsonIgnore]
    public virtual List<ProgramRobot> ProgramRobots { get; set; }
    
    public string Description { get; set; }
    [JsonIgnore]
    public virtual Image Image { get; set; }

    public int? ImageID { get; set; }
  }
}
