using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Services;

namespace Tests.Services
{
  public class FakeRobotConnector : IRobotConnector
  {
    public int couterConnections = 0;

    public bool SendMessageToRobot(string text)
    {
      couterConnections++;
      return text.Length > 0;
    } 
  }
}
