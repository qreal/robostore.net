
/*
Сей класс решает, что делать с полученным сообщением
*/

using System;
using Newtonsoft.Json;
using Router.Models;


namespace Router
{
  public class MessageProcessor
  {
    /*
    Мы знаем, что все сообщения должны заканчиваться на <EOF>
    */

    public void Proccess(string str)
    {
      /*
        Мы знаем, что все сообщения должны заканчиваться на <EOF>
      */
      string json = str.Replace("<EOF>", "");
      Message message = JsonConvert.DeserializeObject<Message>(json);
      Console.WriteLine(message.Robot.Port);
    }

  }
}
