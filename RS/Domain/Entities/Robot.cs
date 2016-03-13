using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Entities
{
  public class Robot
  {
    public int RobotID { get; set; }
    [JsonIgnore]
    public virtual List<Configuration> Configurations { get; set; }
    [JsonIgnore]
    public virtual List<ProgramRobot> ProgramRobots { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; }
    public int? UserID { get; set; }

  }
}
