using Newtonsoft.Json;

namespace Domain.Entities
{
  public class ProgramRobot
  {
    public int ProgramRobotID { get; set; }
    [JsonIgnore]
    public virtual Program Program { get; set; }
    public int ProgramID { get; set; }
    [JsonIgnore]
    public virtual Robot Robot { get; set; }
    public int RobotID { get; set; }
    public int CurrentVersion { get; set; }
  }
}
