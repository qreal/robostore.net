﻿
/*
Сей класс решает, что делать с полученным сообщением
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Router.Models;

/*
Сейчас выбор робота закахркожен, позже будет получать из списка
*/


namespace Router
{
  public class MessageProcessor
  {
    /*
    Простой post запрос
    */
    private void sendMessageToServer(string sender, string text)
    {
      var webClient = new WebClient();
      var pars = new NameValueCollection();
      pars.Add("format", "json");
      pars.Add("Text", text);
      pars.Add("RobotID", sender);
      var response = webClient.UploadValues("http://localhost:45534/message/post", pars);
      string result = System.Text.Encoding.UTF8.GetString(response);
      Console.WriteLine("Server reports:" + result);
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
        return false;
      }
      return true;
    }

    public static void sendEcho(int RobotId)
    {
      Configuration configuration = new Configuration() { RobotID = RobotId, Port = 0 };
      Message message = new Message()
      {
        Commands = new List<string>() { "<ECHO>" },
        Robot = configuration,
        Server = null,
        Text = null
      };
      bool result = true;
      while (result)
      {
        Thread.Sleep(1000);
        result = MessageProcessor.SendMessageToRobot(JsonConvert.SerializeObject(message));
        if (result)
          Console.WriteLine("Robot is online now");
      }
      Console.WriteLine("Robot is offline now");
      Store.Robots.First(x => x.Config.RobotID == 1).isOnline = false;
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
      if (message.Server == null)
      {
        // работу с сервером временно отменил.
        //sendMessageToServer(message.Robot.RobotID.ToString(), message.Text);


        // Добавляем в список Роботов, если его еще там нет.
        if (Store.Robots.FirstOrDefault(x => x.Config.RobotID == message.Robot.RobotID) == null)
        {
          Store.Robots.Add(new Robot()
          {
            Config = message.Robot,
            isOnline = true
          });
        }

        /*
          Запускаем таску, которая шлет эхо сообщения на робота и 
          останавливается, если при передаче происзошла ошибка.
        */
        Task hearBeating = Task.Factory.StartNew(() =>
        {
          sendEcho(message.Robot.RobotID);
        });
      }
      // сообщение от сервера. Его нужно отправить на Робота.
      else
      {
        // потому что идет к роботу
        message.Server = null;
        // потому что для робота они другие
        message.Commands = null;
        SendMessageToRobot(JsonConvert.SerializeObject(message));
      }
    }

  }
}
