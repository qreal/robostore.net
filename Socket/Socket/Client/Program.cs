/*
Взято отсюда
*/

namespace Client
{
  class Program
  {
    static void Main(string[] args)
    {
      SynchronousSocketClient client = new SynchronousSocketClient();
      client.StartClient();
    }
  }
}
