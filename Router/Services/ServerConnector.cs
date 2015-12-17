using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Router.Models;

/*
Класс для общения с Сервером
Может отправлять post запросы на Сервер
Потом убрать ибо не нужен, пока только для теста.
*/

namespace Router.Services
{
  public class ServerConnector
  {
    /*
   Простой post запрос
   */
    public async Task<string> SendMessageToServer(Message message)
    {
      WebRequest req = WebRequest.Create("http://localhost:45534/message/post");
      req.Method = "POST";
      req.Timeout = 100000;
      req.ContentType = "application/json";
      byte[] sentData = Encoding.GetEncoding(1251).GetBytes(JsonConvert.SerializeObject(message));
      try
      {
        Stream sendStream = await req.GetRequestStreamAsync();
        await sendStream.WriteAsync(sentData, 0, sentData.Length);
        sendStream.Close();
        WebResponse res =await req.GetResponseAsync();
        return "ok";
      }
      catch (Exception)
      {
        return "failed";
      }

    }
  }
}
