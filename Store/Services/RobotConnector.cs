using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
  public class RobotConnector : IRobotConnector
  {
    public Task<bool> SendMessageToRobotTask(string message)
      => Task.Factory.StartNew(() => SendMessageToRobot(message));

    public enum OperationCategory { None, Program}
    public enum OperationType { None, GetAll}

    private int robotPort = 11012;

    // отправить сообщение по сокету по порту 11012
    public bool SendMessageToRobot(string message)
    {
      // Data buffer for incoming data.
      var bytes = new byte[1024];

      // Establish the remote endpoint for the socket.
      // This example uses port 11000 on the local computer.
      var ipHostInfo = Dns.Resolve(Dns.GetHostName());
      var ipAddress = ipHostInfo.AddressList[0];
      var remoteEp = new IPEndPoint(ipAddress, robotPort);

      // Create a TCP/IP  socket.
      var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


      // Connect the socket to the remote endpoint. Catch any errors.
      try
      {
        sender.Connect(remoteEp);

        Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint);

        // Encode the data string into a byte array.
        byte[] msg = Encoding.ASCII.GetBytes(message + "<EOF>");

        // Send the data through the socket.
        sender.Send(msg);

        // Receive the response from the remote device.
        int bytesRec = sender.Receive(bytes);
        var result = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        Console.WriteLine("Echoed test = {0}", result);

        // Release the socket.
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();

        // Сообщение должно быть эхом (таким же)

        if (message + "<EOF>" != result)
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
