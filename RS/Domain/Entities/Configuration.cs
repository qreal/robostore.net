using Newtonsoft.Json;

namespace Domain.Entities
{
  public class Configuration
  {
    public int ConfigurationID { get; set; }
    [JsonIgnore]
    public virtual Robot Robot { get; set; }
    public int RobotID { get; set; }
    public int Port { get; set; }
  }
}
