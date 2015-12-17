using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Router.Models;
using Router.Models.Data;

/*
Класс для связи с Роботом.
Может посылать сообщения на Робота обшего вида.
И раз в секунду пустые эхо сообщения, чтобы проверить онлайн ли Робот.
Пока, что взаимодействиет с Сервером ибо мы там отображаем сейчас все, что происходит. Потом уберем это.
*/

namespace Router.Services
{
  public class RobotConnector
  {
    private readonly ServerConnector _serverConnector;
    private readonly IData data;

    public RobotConnector(IData d)
    {
      _serverConnector = new ServerConnector();
      data = d;
    }

    public Task<bool> SendMessageToRobotTask(string text) => Task.Factory.StartNew(() => SendMessageToRobot(text));

    // отправить сообщение по сокету по порту 11012
    public bool SendMessageToRobot(string text)
    {
      // Data buffer for incoming data.
      byte[] bytes = new Byte[1024];
      string result = "";

      // Establish the remote endpoint for the socket.
      // This example uses port 11000 on the local computer.
      IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
      IPAddress ipAddress = ipHostInfo.AddressList[0];
      IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11012);

      // Create a TCP/IP  socket.
      Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


      // Connect the socket to the remote endpoint. Catch any errors.
      try
      {
        sender.Connect(remoteEP);

        Console.WriteLine("Socket connected to {0}",
          sender.RemoteEndPoint.ToString());

        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes(text + "<EOF>");

        // Send the data through the socket.
        int bytesSent = sender.Send(msg);

        // Receive the response from the remote device.
        int bytesRec = sender.Receive(bytes);
        result = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        Console.WriteLine("Echoed test = {0}", result);

        // Release the socket.
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();

        // Сообщение должно быть эхом (таким же)

        if (text + "<EOF>" != result)
          return false;
      }
      catch (Exception e)
      {
        Console.WriteLine("sending message to robot failed\n");
        return false;
      }
      return true;
    }

    public async Task SendEcho(string robotNumber)
    {
      // создали эхо - сообщение
      Message message = new Message()
      {
        Commands = new List<string>() { "<ECHO>" },
        From = "0",
        To = robotNumber,
        Text = null
      };

      // оно дошло?
      bool result = true;

      /*
      нужно сказать серверу, что робот онлайн только 1 раз, поэтому нужен флаг
      */

      bool serverKnows = false;

      while (result)
      {
        Thread.Sleep(1000);
        result = await SendMessageToRobotTask(JsonConvert.SerializeObject(message));
        if (result)
        {
          Console.WriteLine("Robot is online now");
          if (!serverKnows)
          {
            Message notificationOn = new Message()
            {
              Commands = null,
              From = robotNumber,
              Text = "I am online!",
              To = "0"
            };
            await _serverConnector.SendMessageToServer(notificationOn);
            serverKnows = true;
          }
        }
      }

      Console.WriteLine("Robot is offline now");
      Message notificationOff = new Message()
      {
        Commands = null,
        From = robotNumber,
        Text = "I am offline!",
        To = "0"
      };
      var robot = data.Robots.First(x => x.Number == robotNumber);
      robot.isOnline = false;
      await data.UpdateAsync(robot);
      await _serverConnector.SendMessageToServer(notificationOff);
    }
  }
}
