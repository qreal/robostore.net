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
        listBox.Items.Add("server says:"+client.result+"\n");
      }
      else
      {
        listBox.Items.Add("\tcannot sent config");
      }
      sendThread.Abort();
    }

    private CancellationTokenSource tcs;

    private Task Listen(CancellationToken ct)
    { 
      return Task.Factory.StartNew(() =>
      {
        if (ct.IsCancellationRequested)
        {
          server.Cancellation();
          return;
        }
        string result = server.StartListening();
        listBox.Items.Add(string.Format("Server:{0}\n", result));

      }, ct);
    } 

    private void buttonStartReceiving_Click(object sender, EventArgs e)
    {
      tcs = new CancellationTokenSource();

      // Слушать пока не найдем клиента, после чего конец
      server = new Server();
      Task listenOne = Listen(tcs.Token);

    }

    private void buttonStopReceiving_Click(object sender, EventArgs e)
    {
     //Server.works = false;
      tcs.Cancel();
      try
      {
        server.Cancellation();
      }
      catch (Exception ef)
      {
        Console.WriteLine(ef.ToString());
      }
      
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
