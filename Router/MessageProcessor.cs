
/*
Сей класс решает, что делать с полученным сообщением
*/

using System;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json;
using Router.Models;


namespace Router
{
  public class MessageProcessor
  {
    private void sendMessage(string sender, string text)
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

    public void Proccess(string str)
    {
      /*
        Мы знаем, что все сообщения должны заканчиваться на <EOF>
      */
      string json = str.Replace("<EOF>", "");
      Message message = JsonConvert.DeserializeObject<Message>(json);
      // сообщение от робота
      if (message.Server == null)
      {
        sendMessage(message.Robot.Port.ToString(), message.Text);
      }
    }

  }
}
