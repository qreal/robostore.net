using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robot
{
  public partial class Form1 : Form
  {
    private Server server = null;

    public Form1()
    {
      InitializeComponent();
    }

    private void listBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      listBox.Text = "";
    }

    private void buttonConfiguration_Click(object sender, EventArgs e)
    {
      Client client = new Client();
      Thread sendThread = new Thread(new ThreadStart(() => client.StartClient("configuration_test")));
      sendThread.Start();
      if (sendThread.Join(TimeSpan.FromSeconds(5)))
      {
        listBox.Items.Add("\tsuccessfully sent config:\n");
        listBox.Items.Add(client.result);
      }
      else
      {
        listBox.Items.Add("\tcannot sent config");
      }
      sendThread.Abort();
    }

    

    private void buttonStartReceiving_Click(object sender, EventArgs e)
    {
      server = new Server();
      Task.Factory.StartNew(() => server.StartListening());
    }

    private void buttonStopReceiving_Click(object sender, EventArgs e)
    {
     Server.works = false;
      listBox.Items.Add(Server.data);
    }
  }
}
