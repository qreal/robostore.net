using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

/*
Самый простой эхо сервер
Принимает сообщения от Роутера и Сервера
*/

public class SocketServer
{
  // Incoming data from the client.
  private string data = null;

  private Socket listener;
  private Socket handler;

  public void Cancellation()
  {
    handler.Shutdown(SocketShutdown.Both);
    handler.Close();
    listener.Close();
  }

  public string StartListening()
  {
    // Data buffer for incoming data.
    byte[] bytes = new Byte[1024];

    // Establish the local endpoint for the socket.
    // Dns.GetHostName returns the name of the 
    // host running the application.
    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
    IPAddress ipAddress = ipHostInfo.AddressList[0];
    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11012);

    // Create a TCP/IP socket.
    listener = new Socket(AddressFamily.InterNetwork,
      SocketType.Stream, ProtocolType.Tcp);

    // Bind the socket to the local endpoint and 
    // listen for incoming connections.
    try
    {
      listener.Bind(localEndPoint);
      listener.Listen(1); // The maximum length of the pending connections queue.
      Debug.WriteLine("Waiting for a connection...");
      // Program is suspended while waiting for an incoming connection.
      handler = listener.Accept();
      data = null;

      // An incoming connection needs to be processed.
      while (true)
      {
        bytes = new byte[1024];
        int bytesRec = handler.Receive(bytes);
        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
        if (data.IndexOf("<EOF>") > -1)
        {
          break;
        }
      }

      // Show the data on the console.
      Debug.WriteLine("Text received : {0}", data);

      // Echo the data back to the client.
      byte[] msg = Encoding.ASCII.GetBytes(data);

      handler.Send(msg);
      Cancellation();
      return data;
    }
    catch (Exception e)
    {
      Console.WriteLine(e.ToString());
    }
    Cancellation();
    Console.WriteLine("\nPress ENTER to continue...");
    Console.Read();
    return data;
  }
}