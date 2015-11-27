using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Robot
{
  public partial class Form1 : Form
  {
    // Info about Robot
    private int PortListening = 11012;
    private int RoobotID = 23;

    private Server server = null;

    public Form1()
    {
      InitializeComponent();
    }

    // Отправляем конфигурацию
    private void buttonConfiguration_Click(object sender, EventArgs e)
    {
      // создали кофигурацию
      Configuration configuration = new Configuration()
      {
        Port = PortListening,
        RobotID = RoobotID
      };

      // создали пустое сообщение с командой выключить
      Message message = new Message()
      {
        Commands = new List<string>() {"<OFF>"},
        Server = null,
        Robot = configuration,
        Text = null
      };

      string messageFinal = JsonConvert.SerializeObject(message);

      // dont forget add EOF
      messageFinal += "<EOF>";

      Client client = new Client();
      Thread sendThread = new Thread(new ThreadStart(() => client.StartClient(messageFinal)));
      sendThread.Start();
      if (sendThread.Join(TimeSpan.FromSeconds(5)))
      {
        listBox.Items.Add("\tsuccessfully sent config:\n");
        listBox.Items.Add("server says:" + client.result + "\n");
      }
      else
      {
        listBox.Items.Add("\tcannot sent config");
      }
      sendThread.Abort();
    }

    private void buttonStartReceiving_Click(object sender, EventArgs e)
    {
      Task.Factory.StartNew(() =>
      {
        server = new Server();
        string result = server.StartListening();
        listBox.Items.Add(string.Format("Server:{0}\n", result));
        buttonStartReceiving_Click(sender, e);
      });
    }

    private void listBox_DoubleClick(object sender, EventArgs e)
    {
      listBox.Items.Clear();
    }

    private void RouterOff_Click(object sender, EventArgs e)
    {
      Task.Factory.StartNew(() =>
      {
        new Client().StartClient("<OFF>");
        listBox.Items.Add("Bela Lugosis is dead x_x\n");
      });
    }

    private void buttonSayHello_Click(object sender, EventArgs e)
    {
      //создали кофигурацию
      Configuration configuration = new Configuration()
      {
        Port = PortListening,
        RobotID = RoobotID
      };

      // создали пустое сообщение с командой выключить
      Message message = new Message()
      {
        //Commands = new List<string>() { "<OFF>" },
        Commands = null,
        Server = null,
        Robot = configuration,
        Text = "hello"
      };

      string messageFinal = JsonConvert.SerializeObject(message);

      // dont forget to add EOF
      messageFinal += "<EOF>";

      // Тут даем 5 секунд потоку на выполнение и если что убиваем его
      Client client = new Client();
      Thread sendThread = new Thread(new ThreadStart(() => client.StartClient(messageFinal)));
      sendThread.Start();
      if (sendThread.Join(TimeSpan.FromSeconds(5)))
      {
        listBox.Items.Add("\tsuccessfully sent config:\n");
        listBox.Items.Add("server says:" + client.result + "\n");
      }
      else
      {
        listBox.Items.Add("\tcannot sent config");
      }
      sendThread.Abort();
    }
  }
}
