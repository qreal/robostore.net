using System;
using System.Windows.Forms;
using Robot.Models;
using Robot.Services;
using Store.ViewModels.Configuration;

/*
Пока только 1 робот и Id у него захардкожен и = 1.
*/

namespace Robot
{
  public partial class Form1 : Form
  {
    private ConfigurationManager managerConfiguration;
    private ProgramManager managerProgram;

    private SocketServer server;

    public Form1()
    {
      InitializeComponent();
      managerConfiguration = new ConfigurationManager();
      managerProgram = new ProgramManager();
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

    private async void buttonGetConfigurations_Click(object sender, EventArgs e)
    {
      var configurations = managerConfiguration.GetConfigurations();
      listBox.Items.Add("Configurations from server are: \n");
      int i = 0;
      foreach (var config in configurations)
      {
        listBox.Items.Add(++i + ". Port: {" + config.Port + "}");
      }

    }

    private async void buttonSendConfiguration_Click(object sender, EventArgs e)
    {
      managerConfiguration.PostConfiguration(new ConfigurationImport {Port = int.Parse(textBoxPort.Text), RobotID = 4});
      listBox.Items.Add("Current configuration sent to server \n");
    }

    private async void buttonGetProgram_Click(object sender, EventArgs e)
    {
      var program = managerProgram.GetConfigurations();
      listBox.Items.Add("Got a program from server: \n");
      listBox.Items.Add("Name:" + program.Name);
      listBox.Items.Add("Version:" + program.ActualVersion);
      listBox.Items.Add("Code:" + program.Code);
    }
  }
}
