using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Robot.Services;
using Robot.ViewModels;
using Message = Robot.Models.Entities.Message;

/*
Пока только 1 робот и Id у него захардкожен и = 1.
*/

namespace Robot
{
  public partial class Form1 : Form
  {
    // Info about Robot
    private int PortListening = 11012;
    // Это какой-то триковский номер
    private string Number = "agent_007";

    private SocketServer server = null;
    private ServerConnector serverConnector;

    public Form1()
    {
      InitializeComponent();
      serverConnector = new ServerConnector();
    }

    // Отправляем конфигурацию
    // пока не работает
    private void buttonRegister_Click(object sender, EventArgs e)
    {
      // создали кофигурацию
      InitialConfiguration initialConfiguration = new InitialConfiguration()
      {
        Port = PortListening,
        Number = Number,
        IP = Dns.Resolve(Dns.GetHostName()).AddressList[0].ToString()
      };

      // создали пустое сообщение с командой регистрации Робота в Системе
      Message message = new Message()
      {
        Commands = new List<string>() {"<INIT>"},
        To = "0",
        From = Number,
        Text = JsonConvert.SerializeObject(initialConfiguration)
      };

      Thread sendThread = new Thread(new ThreadStart( () => serverConnector.SendMessageToServer(message)));
      sendThread.Start();
      if (sendThread.Join(TimeSpan.FromSeconds(50)))
      {
        listBox.Items.Add("\tsuccessfully sent config:\n");
      }
      else
      {
        listBox.Items.Add("\tcannot sent config timeout");
      }
      sendThread.Abort();
    }


    private async void buttonStartReceiving_Click(object sender, EventArgs e)
    {
      server = new SocketServer();
      // ждем подключения и передаем управление вызывающему коду
      string result = await server.StartListeningTask();
      // вернулись с подключением и записали результат
      listBox.Items.Add(string.Format("Server:{0}\n", result));
      // рекурсивно вызвались, ибо слушаем всегда
      buttonStartReceiving_Click(sender, e);
    }

    private void listBox_DoubleClick(object sender, EventArgs e)
    {
      listBox.Items.Clear();
    }

    private void RouterOff_Click(object sender, EventArgs e)
    {
      Task.Factory.StartNew(() =>
      {
        new RouterConnector().SendMessageToRouter("<OFF>");
        listBox.Items.Add("Bela Lugosis is dead x_x\n");
      });
    }

    private void buttonSayHello_Click(object sender, EventArgs e)
    {

      // создали сообщение
      Message message = new Message()
      {
        //Commands = new List<string>() { "<OFF>" },
        Commands = new List<string>(),
        From = Number,
        To = "0",
        Text = "hello"
      };

      Thread sendThread = new Thread(new ThreadStart(() => serverConnector.SendMessageToServer(message)));
      sendThread.Start();
      if (sendThread.Join(TimeSpan.FromSeconds(5)))
      {
        listBox.Items.Add("\tsuccessfully said hello:\n");
      }
      else
      {
        listBox.Items.Add("\tcannot sent config timeout");
      }
      sendThread.Abort();

    }

    private void GetMailButton_Click(object sender, EventArgs e)
    {
      // создали сообщение
      Message message = new Message()
      {
        Commands = new List<string>() {"<MAIL>"},
        From = Number,
        To = "0",
        Text = ""
      };

      string messageFinal = JsonConvert.SerializeObject(message);

      // dont forget to add EOF
      messageFinal += "<EOF>";

      // Тут даем 5 секунд потоку на выполнение и если что убиваем его
      RouterConnector routerConnector = new RouterConnector();
      Thread sendThread = new Thread(new ThreadStart(() => routerConnector.SendMessageToRouter(messageFinal)));
      sendThread.Start();
      if (sendThread.Join(TimeSpan.FromSeconds(5)))
      {
        listBox.Items.Add("\tsuccessfully sent mail request:\n");
        listBox.Items.Add("server says:" + routerConnector.result + "\n");
      }
      else
      {
        listBox.Items.Add("\tcannot sent mail query");
      }
      sendThread.Abort();
    }
  }
}
