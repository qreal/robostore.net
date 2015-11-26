
/*
Сей класс решает, что делать с полученным сообщением
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        if (result != text)
          return false;
      }
      catch (Exception e)
      {
        //Console.WriteLine("Unexpected exception : {0}", e.ToString());
        return false;
      }
      return true;
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


      // сообщение от робота. Его нужно отправить на Сервер.
      if (message.Server == null)
      {
        sendMessageToServer(message.Robot.RobotID.ToString(), message.Text);
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
