using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
  public class Configuration
  {
    public int RobotID { get; set; }
    public int Port { get; set; }
    public List<string> Commands { get; set; }
  }
}
