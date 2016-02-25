using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
  public interface IRobotConnector
  {
    bool SendMessageToRobot(string text);
  }
}
