using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Newtonsoft.Json;
using Store.Models.Data;
using Store.ViewModels;

/*
Класс для работы с Роутером
*/

/*
Внимание!
Пока работаем только с одним роботом и его Number(триковский номер) будет захардкожено.
Пусть оно будет = agent_007
*/

namespace Store.Services
{
  public class RouterConnector
  {
    // отправить сообщение по сокету по порту 11012
    public bool SendMessageToRobot(MessageVM messageToRobot)
    {
      string text = JsonConvert.SerializeObject(messageToRobot);

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

        Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint);

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
  }
}
