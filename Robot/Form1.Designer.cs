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
      this.buttonRegister = new System.Windows.Forms.Button();
      this.RouterOff = new System.Windows.Forms.Button();
      this.buttonSayHello = new System.Windows.Forms.Button();
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
      this.buttonStartReceiving.Location = new System.Drawing.Point(295, 184);
      this.buttonStartReceiving.Name = "buttonStartReceiving";
      this.buttonStartReceiving.Size = new System.Drawing.Size(130, 59);
      this.buttonStartReceiving.TabIndex = 1;
      this.buttonStartReceiving.Text = "Start Receiving";
      this.buttonStartReceiving.UseVisualStyleBackColor = true;
      this.buttonStartReceiving.Click += new System.EventHandler(this.buttonStartReceiving_Click);
      // 
      // buttonRegister
      // 
      this.buttonRegister.Location = new System.Drawing.Point(295, 268);
      this.buttonRegister.Name = "buttonRegister";
      this.buttonRegister.Size = new System.Drawing.Size(270, 59);
      this.buttonRegister.TabIndex = 3;
      this.buttonRegister.Text = "Register Robot";
      this.buttonRegister.UseVisualStyleBackColor = true;
      this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
      // 
      // RouterOff
      // 
      this.RouterOff.BackColor = System.Drawing.Color.Red;
      this.RouterOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.RouterOff.Location = new System.Drawing.Point(295, 333);
      this.RouterOff.Name = "RouterOff";
      this.RouterOff.Size = new System.Drawing.Size(270, 46);
      this.RouterOff.TabIndex = 5;
      this.RouterOff.Text = "ROUTER OFF";
      this.RouterOff.UseVisualStyleBackColor = false;
      this.RouterOff.Click += new System.EventHandler(this.RouterOff_Click);
      // 
      // buttonSayHello
      // 
      this.buttonSayHello.Location = new System.Drawing.Point(431, 184);
      this.buttonSayHello.Name = "buttonSayHello";
      this.buttonSayHello.Size = new System.Drawing.Size(130, 59);
      this.buttonSayHello.TabIndex = 6;
      this.buttonSayHello.Text = "Say Hello";
      this.buttonSayHello.UseVisualStyleBackColor = true;
      this.buttonSayHello.Click += new System.EventHandler(this.buttonSayHello_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(855, 484);
      this.Controls.Add(this.buttonSayHello);
      this.Controls.Add(this.RouterOff);
      this.Controls.Add(this.buttonRegister);
      this.Controls.Add(this.buttonStartReceiving);
      this.Controls.Add(this.listBox);
      this.Name = "Form1";
      this.Text = "Robot";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox listBox;
    private System.Windows.Forms.Button buttonStartReceiving;
    private System.Windows.Forms.Button buttonRegister;
    private System.Windows.Forms.Button RouterOff;
    private System.Windows.Forms.Button buttonSayHello;
  }
}

