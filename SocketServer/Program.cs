using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer
{
  class Program
  {
    static Listener listener = new Listener();

    private static void Listen()
    {
      listener.StartListening();
    }

    private static void test()
    {
      Task.Factory.StartNew(() => listener.StartListening()).Wait();
    }

    static void Main(string[] args)
    {
      test();
    }
  }
}
