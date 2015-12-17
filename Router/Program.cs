using System;
using System.Threading;
using System.Threading.Tasks;
using Router.Services;

/*
  Роутер спокойно слушает свой порт.
  Как только он получил сообщение от Робота, он должен обработать сообщение и начать 
  контролировать онлайн ли Робот.
*/

namespace Router
{
  class Program
  {
    private static SocketServer server = new SocketServer();

    static void Main(string[] args)
    {
      /*
        Запускаем таску, которая принимает сокет содинения и обрабатывает 
        их через класс MessageProcessor. По завершении этой таски убиваем основной поток.
      */
      Task listening = Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Router started working");
        server.ReceiveEcho();
        //GetMessageMock();
        Console.WriteLine("Router finished working");
      });

      // Что-то типа прогресс бара
      while (!listening.IsCompleted)
      {
        Console.Write(".");
        Thread.Sleep(1000);
      }
    }
  }
}
