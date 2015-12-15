/*
Сей класс решает, что делать с полученным сообщением
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Router.Models;
using Router.Models.Data;
using Router.Models.Entities;
using Router.ViewModels;

/*
Сейчас выбор робота закахркожен = 1, позже будет получать из списка
*/

/*
TODO 
Вынести комманды в константы куда-нибудь
*/


namespace Router
{
  public class MessageProcessor
  {
    private IData data;

    public MessageProcessor(IData d)
    {
      data = d;
    }

    /*
    Простой post запрос
    */
    private static void sendMessageToServer(Message message)
    {
      var webClient = new WebClient();
      var pars = new NameValueCollection();
      pars.Add("format", "json");
      pars.Add("Text", message.Text);
      pars.Add("From", message.From.ToString());
      pars.Add("To", message.To.ToString());
      try
      {
        var response = webClient.UploadValues("http://localhost:45534/message/post", pars);
        string result = System.Text.Encoding.UTF8.GetString(response);
        Console.WriteLine("Server reports:" + result);
      }
      catch (Exception e)
      {
        Console.WriteLine("sending message to server failed\n");
      }
       
    }

    // отправить сообщение по сокету по порту 11012
    public static bool SendMessageToRobot(string text)
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
        //Console.WriteLine("Unexpected exception : {0}", e.ToString());
        Console.WriteLine("sending message to robot failed\n");
        return false;
      }
      return true;
    }

    public static void sendEcho(int RobotId)
    {
      // создали эхо - сообщение
      Message message = new Message()
      {
        Commands = new List<string>() { "<ECHO>" },
        From = 0,
        To = RobotId,
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
        result = MessageProcessor.SendMessageToRobot(JsonConvert.SerializeObject(message));
        if (result)
        {
          Console.WriteLine("Robot is online now");
          if (!serverKnows)
          {
            Message notificationOn = new Message()
            {
              Commands = null,
              From = RobotId,
              Text = "I am online!",
              To = 0
            };
            sendMessageToServer(notificationOn);
            serverKnows = true;
          }
        }    
      }

      Console.WriteLine("Robot is offline now");
      Message notificationOff = new Message()
      {
        Commands = null,
        From = RobotId,
        Text = "I am offline!",
        To = 0
      };
      sendMessageToServer(notificationOff);
      //Store.Robots.First(x => x.Config.RobotID == 1).isOnline = false;
    }

    public void Proccess(string str)
    {
      /*
        Мы знаем, что все сообщения должны заканчиваться на <EOF>
      */
      string json = str.Replace("<EOF>", "");
      /*
        Также мы знаем, что все сообщения имеют общий вид
      */
      Message message = JsonConvert.DeserializeObject<Message>(json);


      /* сообщение от робота. Его нужно отправить в на Сервер в любом случае.
         Также мы должны начать отправлять эхо запросы и сделить, онлайн ли наш Робот или нет.
      */
      if (message.From != 0)
      {
        sendMessageToServer(message);

        // Если регистрация Робота в Системе
        if (message.Commands.IndexOf("<INIT>") != -1)
        {
          // нужно вытащить конфигурацию через декод JSON
          InitialConfiguration configurationIni = JsonConvert.DeserializeObject<InitialConfiguration>(message.Text);
          Configuration configuration = new Configuration()
          {
            Port = configurationIni.Port
          };

          Robot robot = new Robot()
          {
            isOnline = true,
            IP = Dns.Resolve(Dns.GetHostName()).AddressList[0].ToString(),
            Number = configurationIni.Number
          };
          data.AddAsync(robot).Wait();
          configuration.RobotID = robot.RobotID;
          data.AddAsync(configuration).Wait();
          // создать робота и привязать к конфигурации
        }

        /*
          Запускаем таску, которая шлет эхо сообщения на робота и 
          останавливается, если при передаче происзошла ошибка.
        */
        Task hearBeating = Task.Factory.StartNew(() =>
        {
          sendEcho(message.From);
        });
      }
      // сообщение от сервера. Его нужно отправить на Робота.
      // Но только если он онлайн
      else
      {
        SendMessageToRobot(JsonConvert.SerializeObject(message));
      }
    }

  }
}
