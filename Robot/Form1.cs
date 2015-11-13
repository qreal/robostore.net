using System;
using System.Windows.Forms;

namespace Robot
{
  public partial class Form1 : Form
  {
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
      SocketClient client = new SocketClient();
      client.SendServer("configuration_test");
      //listBox.Items.Add(client.SendServer("configuration_test"));

    }
  }
}
