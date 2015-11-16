using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
  class Program
  {
    static void Main(string[] args)
    {
      Client client = new Client();
      client.SendServer("Hi");
    }
  }
}
