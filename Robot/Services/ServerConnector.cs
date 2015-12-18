﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

/*
Класс для взаимодействия с Сервером
Может отправлять сообщения на него (post запросы)
*/

namespace Robot.Services
{
  

  public class ServerConnector
  {
    /*
    Простой post запрос
    */
    public string SendMessageToServer(Models.Entities.Message message)
    {
      WebRequest req = WebRequest.Create("http://localhost:45534/message/post");
      req.Method = "POST";
      req.Timeout = 100000;
      req.ContentType = "application/json";
      byte[] sentData = Encoding.GetEncoding(1251).GetBytes(JsonConvert.SerializeObject(message));
      try
      {
        Stream sendStream = req.GetRequestStream();
        sendStream.Write(sentData, 0, sentData.Length);
        sendStream.Close();
        WebResponse res = req.GetResponse();
        return "ok";
      }
      catch (Exception)
      {

        return "failed";
      }

    }
  }
}