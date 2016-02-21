namespace Robot
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.listBox = new System.Windows.Forms.ListBox();
      this.buttonStartReceiving = new System.Windows.Forms.Button();
      this.buttonGetConfigurations = new System.Windows.Forms.Button();
      this.buttonSendConfiguration = new System.Windows.Forms.Button();
      this.groupBoxConfiguration = new System.Windows.Forms.GroupBox();
      this.textBoxPort = new System.Windows.Forms.TextBox();
      this.LabelPort = new System.Windows.Forms.Label();
      this.buttonGetProgram = new System.Windows.Forms.Button();
      this.groupBoxConfiguration.SuspendLayout();
      this.SuspendLayout();
      // 
      // listBox
      // 
      this.listBox.FormattingEnabled = true;
      this.listBox.ItemHeight = 16;
      this.listBox.Items.AddRange(new object[] {
            "Hello"});
      this.listBox.Location = new System.Drawing.Point(-1, -2);
      this.listBox.Name = "listBox";
      this.listBox.Size = new System.Drawing.Size(861, 180);
      this.listBox.TabIndex = 0;
      this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
      // 
      // buttonStartReceiving
      // 
      this.buttonStartReceiving.Location = new System.Drawing.Point(12, 184);
      this.buttonStartReceiving.Name = "buttonStartReceiving";
      this.buttonStartReceiving.Size = new System.Drawing.Size(153, 59);
      this.buttonStartReceiving.TabIndex = 1;
      this.buttonStartReceiving.Text = "Start Receiving";
      this.buttonStartReceiving.UseVisualStyleBackColor = true;
      this.buttonStartReceiving.Click += new System.EventHandler(this.buttonStartReceiving_Click);
      // 
      // buttonGetConfigurations
      // 
      this.buttonGetConfigurations.Location = new System.Drawing.Point(12, 249);
      this.buttonGetConfigurations.Name = "buttonGetConfigurations";
      this.buttonGetConfigurations.Size = new System.Drawing.Size(153, 59);
      this.buttonGetConfigurations.TabIndex = 2;
      this.buttonGetConfigurations.Text = "Get Configurations";
      this.buttonGetConfigurations.UseVisualStyleBackColor = true;
      this.buttonGetConfigurations.Click += new System.EventHandler(this.buttonGetConfigurations_Click);
      // 
      // buttonSendConfiguration
      // 
      this.buttonSendConfiguration.Location = new System.Drawing.Point(12, 314);
      this.buttonSendConfiguration.Name = "buttonSendConfiguration";
      this.buttonSendConfiguration.Size = new System.Drawing.Size(153, 59);
      this.buttonSendConfiguration.TabIndex = 3;
      this.buttonSendConfiguration.Text = "Send Configuration";
      this.buttonSendConfiguration.UseVisualStyleBackColor = true;
      this.buttonSendConfiguration.Click += new System.EventHandler(this.buttonSendConfiguration_Click);
      // 
      // groupBoxConfiguration
      // 
      this.groupBoxConfiguration.Controls.Add(this.textBoxPort);
      this.groupBoxConfiguration.Controls.Add(this.LabelPort);
      this.groupBoxConfiguration.Location = new System.Drawing.Point(171, 184);
      this.groupBoxConfiguration.Name = "groupBoxConfiguration";
      this.groupBoxConfiguration.Size = new System.Drawing.Size(254, 189);
      this.groupBoxConfiguration.TabIndex = 4;
      this.groupBoxConfiguration.TabStop = false;
      this.groupBoxConfiguration.Text = "Configuration";
      // 
      // textBoxPort
      // 
      this.textBoxPort.Location = new System.Drawing.Point(17, 48);
      this.textBoxPort.Name = "textBoxPort";
      this.textBoxPort.Size = new System.Drawing.Size(100, 22);
      this.textBoxPort.TabIndex = 1;
      this.textBoxPort.Text = "11012";
      // 
      // LabelPort
      // 
      this.LabelPort.AutoSize = true;
      this.LabelPort.Location = new System.Drawing.Point(14, 28);
      this.LabelPort.Name = "LabelPort";
      this.LabelPort.Size = new System.Drawing.Size(38, 17);
      this.LabelPort.TabIndex = 0;
      this.LabelPort.Text = "Port:";
      // 
      // buttonGetProgram
      // 
      this.buttonGetProgram.Location = new System.Drawing.Point(431, 184);
      this.buttonGetProgram.Name = "buttonGetProgram";
      this.buttonGetProgram.Size = new System.Drawing.Size(153, 59);
      this.buttonGetProgram.TabIndex = 5;
      this.buttonGetProgram.Text = "Get Program";
      this.buttonGetProgram.UseVisualStyleBackColor = true;
      this.buttonGetProgram.Click += new System.EventHandler(this.buttonGetProgram_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(587, 380);
      this.Controls.Add(this.buttonGetProgram);
      this.Controls.Add(this.groupBoxConfiguration);
      this.Controls.Add(this.buttonSendConfiguration);
      this.Controls.Add(this.buttonGetConfigurations);
      this.Controls.Add(this.buttonStartReceiving);
      this.Controls.Add(this.listBox);
      this.Name = "Form1";
      this.Text = "Robot";
      this.groupBoxConfiguration.ResumeLayout(false);
      this.groupBoxConfiguration.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox listBox;
    private System.Windows.Forms.Button buttonStartReceiving;
    private System.Windows.Forms.Button buttonGetConfigurations;
    private System.Windows.Forms.Button buttonSendConfiguration;
    private System.Windows.Forms.GroupBox groupBoxConfiguration;
    private System.Windows.Forms.TextBox textBoxPort;
    private System.Windows.Forms.Label LabelPort;
    private System.Windows.Forms.Button buttonGetProgram;
  }
}

