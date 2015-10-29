using System;

namespace Socket.SocketServer
{
  class Program
  {
    static void Main(string[] args)
    {
      SynchronousSocketListener.StartListening();
    }
  }
}
