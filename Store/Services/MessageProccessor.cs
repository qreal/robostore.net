using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Store.Models.Data;
using Store.ViewModels;

namespace Store.Services
{
  /*
  Поиск комманд в сообщениях
  */
  public static class MessageVMExtensions
  {
    public static bool HasCommand(this MessageVM msg, string cm) => msg.Commands.Any(command => command == cm);
  }

  public class MessageProccessor
  {
    private IData data;
    private const string Init = "<INIT>";

    public MessageProccessor(IData d)
    {
      data = d;
    }

    public async Task proccess(MessageVM message)
    {
      if (message.HasCommand(Init))
      {
        // нужно вытащить конфигурацию через декод JSON
        InitialConfiguration configurationIni = JsonConvert.DeserializeObject<InitialConfiguration>(message.Text);
        Configuration configuration = new Configuration()
        {
          Port = configurationIni.Port
        };

        Robot robot = new Robot()
        {
          isOnline = true,
          IP = configurationIni.IP,
          Number = configurationIni.Number
        };
        await data.AddAsync(robot);
        var x = 5;
        configuration.RobotID = robot.RobotID;
        await data.AddAsync(configuration);
      }
    }
  }
}
