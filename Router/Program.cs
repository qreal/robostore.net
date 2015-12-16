using System;
using System.Data.SqlTypes;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Router.Models.Data;


/*
  Роутер спокойно слушает свой порт.
  Как только он получил сообщение от Робота, он должен обработать сообщение и начать 
  контролировать онлайн ли Робот.
*/

namespace Router
{
  class Program
  {

    /*
    Класс для обработки сообщений
    */
    private static MessageProcessor processor = new MessageProcessor(new Data());

    /*
    Получить сообщение и отправить его обратно
    */
    private static void ReceiveEcho()
    {
      TcpListener listener = new TcpListener(11011);
      listener.Start();
      string data = "";
      // команада остановки
      while (data.IndexOf("<OFF>", StringComparison.Ordinal) == -1)
      {
        while (true)
        {
          TcpClient client = listener.AcceptTcpClient();
          Console.WriteLine("connected!\n");

          Stream inputStream = new BufferedStream(client.GetStream());
          data = "";
          while (true)
          {
            // Это дело не стопится, тут позже можно вкрутить таймер, а пока мы точно знаем формат сообщения
            var next_char = inputStream.ReadByte();

            data += Convert.ToChar(next_char);
            if (data.IndexOf("<EOF>", StringComparison.Ordinal) != -1)
              break;
          }
          Console.WriteLine("received:\n"+data);
          processor.Proccess(data);

          StreamWriter outputStream = new StreamWriter(new BufferedStream(client.GetStream()));
          outputStream.WriteLine(data);
          outputStream.Flush();

          client.Close();
          break;
        }
      }
      listener.Stop();
    }

    static void Main(string[] args)
    {
      /*
        Запускаем таску, которая принимает сокет содинения и обрабатывает 
        их через класс MessageProcessor. По завершении этой таски убиваем основной поток.
      */

      bool run = true;
      Task listening = Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Router started working");
        ReceiveEcho();
        run = false;
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
