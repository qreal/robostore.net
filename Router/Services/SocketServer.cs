using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Router.Models.Data;

/*
Классс принимает сообщения от Сервера и Робота и дает обработчику
*/

namespace Router.Services
{
  public class SocketServer
  {
    /*
    Класс для обработки сообщений
    */
    private static MessageProcessor processor = new MessageProcessor(new Data());

    /*
    Получить сообщение и отправить его обратно
    */

    public void ReceiveEcho()
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
          Console.WriteLine("received:\n" + data);
          Task.Factory.StartNew(() =>
          {
            processor.Proccess(data).Start();
          });


          StreamWriter outputStream = new StreamWriter(new BufferedStream(client.GetStream()));
          outputStream.WriteLine(data);
          outputStream.Flush();

          client.Close();
          break;
        }
      }
      listener.Stop();
    }
  }
}
