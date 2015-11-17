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

    private CancellationTokenSource tcs;

    private void buttonStartReceiving_Click(object sender, EventArgs e)
    {
      tcs = new CancellationTokenSource();
      CancellationToken ct = tcs.Token;

      // Слушать пока не найдем клиента, после чего конец
      server = new Server();
      //Task task = Task.Factory.StartNew(() => server.StartListening(), ct);

      Task.Factory.StartNew(() =>
      {
        string result = server.StartListening();
        listBox.Items.Add(string.Format("Server:{0}\n", result));
        //decimal result = CalculateMeaningOfLife();
        //ResultTextBlock.Text = result.ToString();
      });

    }

    private void buttonStopReceiving_Click(object sender, EventArgs e)
    {
     //Server.works = false;
     //tcs.Cancel();
     //listBox.Items.Add(server.result);
    }

    private void listBox_DoubleClick(object sender, EventArgs e)
    {
      listBox.Items.Clear();
    }
  }
}
