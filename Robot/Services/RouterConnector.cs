using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

/*
Самый простой эхо клиент
Используется для отправки сообщений на Роутер
*/

namespace Robot.Services
{
  public class RouterConnector
  {
    public string result = "";

    public string SendMessageToRouter(string input)
    {
      // Data buffer for incoming data.
      byte[] bytes = new byte[1024];

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

        Console.WriteLine("Socket connected to {0}",
          sender.RemoteEndPoint.ToString());

        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes(input + "<EOF>");

        // Send the data through the socket.
        int bytesSent = sender.Send(msg);

        // Receive the response from the remote device.
        int bytesRec = sender.Receive(bytes);
        result = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        Console.WriteLine("Echoed test = {0}", result);

        // Release the socket.
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("Unexpected exception : {0}", e.ToString());
      }

      return result;
    }
  }
}