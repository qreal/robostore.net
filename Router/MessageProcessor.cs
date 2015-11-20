
/*
Сей класс решает, что делать с полученным сообщением
*/

using System;


namespace Router
{
  public class MessageProcessor
  {

    /*
    Мы знаем, что все сообщения должны заканчиваться на <EOF>
    */
    public void Proccess(string str)
    {
      string json = str.Replace("<EOF>","");
      Configuration configuration = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Configuration>(json);

      Console.WriteLine(configuration.Port);
    }

  }
}
