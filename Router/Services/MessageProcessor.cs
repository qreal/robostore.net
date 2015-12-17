/*
Сей класс решает, что делать с полученным сообщением
*/

using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Router.Models;
using Router.Models.Data;
using Router.Models.Entities;

/*
Сейчас выбор робота закахркожен, позже будет получать из списка
*/

namespace Router.Services
{
  // Метод поиска комманд в сообщениях
  public static class MessageExtensions
  {
    public static bool HasCommand(this Message msg, string cm) => msg.Commands.Any(command => command == cm);
  }

  public class MessageProcessor
  {
    private IData data;
    private readonly ServerConnector _serverConnector;
    private readonly RobotConnector _robotConnector;
    // Комманды сообщений (см Протокол)
    private const string Mail = "<MAIL>";
    private const string Init = "<INIT>";


    public MessageProcessor(IData d)
    {
      data = d;
      _robotConnector = new RobotConnector();
      _serverConnector = new ServerConnector();
    }

    public async Task Proccess(string str)
    {
      Console.WriteLine("Proccessing started\n"); //
      /*
        Мы знаем, что все сообщения должны заканчиваться на <EOF>
      */
      string json = str.Replace("<EOF>", "");
      /*
        Также мы знаем, что все сообщения имеют общий вид
      */
      Message message = JsonConvert.DeserializeObject<Message>(json);


      /* сообщение от робота. Его нужно отправить в на Сервер в любом случае.
         Также мы должны начать отправлять эхо запросы и сделить, онлайн ли наш Робот или нет.
      */
      if (message.From != "0")
      {
        Console.WriteLine("Sending to Server\n"); //
        // Пока что все запросы идут на Сервер, в будущем будет не так.
        await _serverConnector.SendMessageToServer(message);
        Console.WriteLine("sent to Server\n"); //

        // запрос на получение всех сообщений
        if (message.HasCommand(Mail))
        {
          Robot robot = data.Robots.First(x => x.Number == message.From);
          var messages = data.Messages.Where(x => x.Robot.RobotID == robot.RobotID);
          Console.WriteLine("We have {0} messages",messages.Count());
          foreach (var mes in messages)
          {
            Console.WriteLine("Seding one to robot");
            await _robotConnector.SendMessageToRobotTask(mes.Text);
            Console.WriteLine("Sent one to robot and deleting");
            await data.RemoveAsync(mes);
            Console.WriteLine("deleted");
          }
        }

        /*
          Запускаем таску, которая шлет эхо сообщения на робота и 
          останавливается, если при передаче происзошла ошибка.
        */
        //await _robotConnector.SendEchoTask(message.From);
        //Task heartBeating = Task.Factory.StartNew(() =>
        //{
        //  _robotConnector.SendEcho(message.From);
        //});
      }
      // сообщение от сервера. 
      else
      {
        // сначала найдем этого Робота в базе.
        Robot robot = data.Robots.First(x => x.Number == message.To);
        // если онлайн и сообщение до него дошло, то ОК. 
        if (robot.isOnline && await _robotConnector.SendMessageToRobotTask(JsonConvert.SerializeObject(message)))
        {

        }
        // если Робот сейчас не online или недоступен, то сохраним сообщение в БД
        else
        {
          await data.AddAsync(new StoredMessage()
          {
            Robot = robot,
            Text = message.Text
          });
        }
      }
    }

  }
}
