using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    private void buttonConfiguration_Click(object sender, EventArgs e)
    {
      Configuration configuration = new Configuration()
      {
        Port = PortListening,
        RobotID = RoobotID,
        Commands = new List<string> { "<OFF>" }
      };

      // Serialize JSON 
      // See more at this https://msdn.microsoft.com/en-us/library/bb412179(v=vs.110).aspx
      MemoryStream stream = new MemoryStream();
      DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Configuration));
      serializer.WriteObject(stream, configuration);
      stream.Position = 0;
      StreamReader streamReader = new StreamReader(stream);
      string message = streamReader.ReadToEnd();

      // dont forget add EOF
      message += "<EOF>";


      Client client = new Client();
      Thread sendThread = new Thread(new ThreadStart(() => client.StartClient(message)));
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
        listBox.Items.Add("server is dead x_x\n");
      });
    }
  }
}
