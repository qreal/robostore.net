using System.Collections.Generic;
using Newtonsoft.Json;

namespace Store.Models.Entities
{
  public class Robot
  {
    public int RobotID { get; set; }
    [JsonIgnore]
    public virtual List<Configuration> Configurations { get; set; }
    [JsonIgnore]
    public virtual List<ProgramRobot> ProgramRobots { get; set; }

  }
}
