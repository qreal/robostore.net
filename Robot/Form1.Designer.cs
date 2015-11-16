﻿namespace Robot
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
      this.buttonConfiguration = new System.Windows.Forms.Button();
      this.buttonStopReceiving = new System.Windows.Forms.Button();
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
      this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
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
      // buttonConfiguration
      // 
      this.buttonConfiguration.Location = new System.Drawing.Point(295, 268);
      this.buttonConfiguration.Name = "buttonConfiguration";
      this.buttonConfiguration.Size = new System.Drawing.Size(270, 59);
      this.buttonConfiguration.TabIndex = 3;
      this.buttonConfiguration.Text = "Send Configuration";
      this.buttonConfiguration.UseVisualStyleBackColor = true;
      this.buttonConfiguration.Click += new System.EventHandler(this.buttonConfiguration_Click);
      // 
      // buttonStopReceiving
      // 
      this.buttonStopReceiving.Location = new System.Drawing.Point(446, 184);
      this.buttonStopReceiving.Name = "buttonStopReceiving";
      this.buttonStopReceiving.Size = new System.Drawing.Size(119, 59);
      this.buttonStopReceiving.TabIndex = 4;
      this.buttonStopReceiving.Text = "Stop Receiving";
      this.buttonStopReceiving.UseVisualStyleBackColor = true;
      this.buttonStopReceiving.Click += new System.EventHandler(this.buttonStopReceiving_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(855, 484);
      this.Controls.Add(this.buttonStopReceiving);
      this.Controls.Add(this.buttonConfiguration);
      this.Controls.Add(this.buttonStartReceiving);
      this.Controls.Add(this.listBox);
      this.Name = "Form1";
      this.Text = "Robot";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox listBox;
    private System.Windows.Forms.Button buttonStartReceiving;
    private System.Windows.Forms.Button buttonConfiguration;
    private System.Windows.Forms.Button buttonStopReceiving;
  }
}
