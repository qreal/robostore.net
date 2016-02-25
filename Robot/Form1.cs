using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Robot.Models;
using Robot.Services;
using Store.ViewModels.Configuration;
using Store.ViewModels.Program;

/*
Пока только 1 робот и Id у него захардкожен и = 1.
*/

namespace Robot
{
  public partial class Form1 : Form
  {
    private ConfigurationManager configurationManager;
    private ProgramManager programManager;
    private MessageParser messageParser;

    private SocketServer server;

    public Form1()
    {
      InitializeComponent();
      configurationManager = new ConfigurationManager();
      programManager = new ProgramManager();
      messageParser = new MessageParser(this);
    }

    private async void buttonStartReceiving_Click(object sender, EventArgs e)
    {
      server = new SocketServer();
      // ждем подключения и передаем управление вызывающему коду
      string result = await server.StartListeningTask();
      // вернулись с подключением и записали результат
      listBox.Items.Add(string.Format("Server:{0}\n", result));
      // парсим входную строку
      messageParser.ParseCommand(result);
      // рекурсивно вызвались, ибо слушаем всегда
      buttonStartReceiving_Click(sender, e);
    }

    private void listBox_DoubleClick(object sender, EventArgs e)
    {
      listBox.Items.Clear();
    }

    private async void buttonGetConfigurations_Click(object sender, EventArgs e)
    {
      var configurations = configurationManager.GetConfigurations();
      listBox.Items.Add("Configurations from server are: \n");
      int i = 0;
      foreach (var config in configurations)
      {
        listBox.Items.Add(++i + ". Port: {" + config.Port + "}");
      }

    }

    private async void buttonSendConfiguration_Click(object sender, EventArgs e)
    {
      configurationManager.PostConfiguration(new ConfigurationImport {Port = int.Parse(textBoxPort.Text), RobotID = 4});
      listBox.Items.Add("Current configuration sent to server \n");
    }

    private async void buttonGetProgram_Click(object sender, EventArgs e)
    {
      var programs = await programManager.GetProgramAsync();
      foreach (var program in programs)
      {
        OutputProgram(program);
      }
    }


    public void OutputProgram(ProgramExport program)
    {
      FormWriteLine("Got a program from server: \n");
      FormWriteLine("Name:" + program.Name);
      FormWriteLine("Version:" + program.ActualVersion);
      FormWriteLine("Code:" + program.Code);
    }

    public void FormWriteLine(string text) => listBox.Items.Add(text);
    
  }
}
