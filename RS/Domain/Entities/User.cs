using System.Collections.Generic;
using Newtonsoft.Json;

namespace Domain.Entities
{
  public class User
  {
    public int UserID { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    [JsonIgnore]
    public virtual List<Robot> Robots { get; set; }
  }
}
