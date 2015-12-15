using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

/*
Класс для работы с Роутером
*/

/*
Внимание!
Пока работаем только с одним роботом и его ID будет захардкожено.
Пусть оно будет = 1
*/

/*
TODO
Сейчас все отапрвялется на Роутер. 
Сделать систему с ожиданием в 1 секунду, если не получили эхо, то отправляем на Роутер
*/

namespace Store.Models
{
  public class RouterConnector
  {
    // отправить на захардкоденного робота
    public void SendToRobot(Message messageToRobot)
    {
      messageToRobot.From = 0;
      messageToRobot.To = 1;
      
      // Data buffer for incoming data.
      byte[] bytes = new Byte[1024];
      string result = "";

      // Establish the remote endpoint for the socket.
      // This example uses port 11000 on the local computer.
      IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
      IPAddress ipAddress = ipHostInfo.AddressList[0];
      IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11011);

      // Create a TCP/IP  socket.
      Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


      // Connect the socket to the remote endpoint. Catch any errors.
      try
      {
        sender.Connect(remoteEP);

        Debug.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

        string messageFinal = JsonConvert.SerializeObject(messageToRobot) + "<EOF>";

        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes(messageFinal);

        // Send the data through the socket.
        sender.Send(msg);

        // Receive the response from the remote device.
        int bytesRec = sender.Receive(bytes);
        result = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        Debug.WriteLine("Echoed test = {0}", result);

        // Release the socket.
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
      }
      catch (Exception e)
      {
        Debug.WriteLine("Unexpected exception : {0}", e.ToString());
      }
    }
  }
}
