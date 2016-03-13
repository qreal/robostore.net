using Newtonsoft.Json;

namespace Domain.Entities
{
  public class ProgramRobot
  {
    public int ProgramRobotID { get; set; }
    [JsonIgnore]
    public Program Program { get; set; }
    public int ProgramID { get; set; }
    [JsonIgnore]
    public Robot Robot { get; set; }
    public int RobotID { get; set; }
    public int CurrentVersion { get; set; }
    public bool Received { get; set; }
  }
}
