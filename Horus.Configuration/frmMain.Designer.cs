namespace Horus.Config
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDrivers = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabDevices = new System.Windows.Forms.TabPage();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabDrivers.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabDevices.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDrivers);
            this.tabControl1.Controls.Add(this.tabDevices);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(515, 348);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDrivers
            // 
            this.tabDrivers.Controls.Add(this.groupBox2);
            this.tabDrivers.Controls.Add(this.button3);
            this.tabDrivers.Controls.Add(this.groupBox1);
            this.tabDrivers.Controls.Add(this.listBox1);
            this.tabDrivers.Location = new System.Drawing.Point(4, 22);
            this.tabDrivers.Name = "tabDrivers";
            this.tabDrivers.Padding = new System.Windows.Forms.Padding(3);
            this.tabDrivers.Size = new System.Drawing.Size(507, 322);
            this.tabDrivers.TabIndex = 0;
            this.tabDrivers.Text = "Installed Drivers";
            this.tabDrivers.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(228, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 87);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Driver Info";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(342, 281);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(155, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Configure Driver";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(228, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 155);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Parameters";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 100);
            this.label1.TabIndex = 4;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(184, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Reset";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(10, 14);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(210, 290);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tabDevices
            // 
            this.tabDevices.Controls.Add(this.button7);
            this.tabDevices.Controls.Add(this.button6);
            this.tabDevices.Controls.Add(this.button5);
            this.tabDevices.Controls.Add(this.listBox2);
            this.tabDevices.Controls.Add(this.button4);
            this.tabDevices.Location = new System.Drawing.Point(4, 22);
            this.tabDevices.Name = "tabDevices";
            this.tabDevices.Padding = new System.Windows.Forms.Padding(3);
            this.tabDevices.Size = new System.Drawing.Size(507, 322);
            this.tabDevices.TabIndex = 1;
            this.tabDevices.Text = "Attached Devices";
            this.tabDevices.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(361, 209);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(133, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "Configure Device";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(361, 97);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(133, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "Remove from List";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(361, 59);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(133, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Add Non Discoverable";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(17, 59);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(330, 173);
            this.listBox2.TabIndex = 4;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(17, 15);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(181, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Discover Attached Devices";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 374);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "Horus Config";
            this.tabControl1.ResumeLayout(false);
            this.tabDrivers.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabDevices.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDrivers;
        private System.Windows.Forms.TabPage tabDevices;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
    }
}

