/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDrivers = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbDrivers = new System.Windows.Forms.ListBox();
            this.tabDevices = new System.Windows.Forms.TabPage();
            this.btnConfigureDevice = new System.Windows.Forms.Button();
            this.lbDevices = new System.Windows.Forms.ListBox();
            this.btnFindDevices = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabDrivers.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(593, 389);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDrivers
            // 
            this.tabDrivers.Controls.Add(this.groupBox2);
            this.tabDrivers.Controls.Add(this.groupBox1);
            this.tabDrivers.Controls.Add(this.lbDrivers);
            this.tabDrivers.Location = new System.Drawing.Point(4, 22);
            this.tabDrivers.Name = "tabDrivers";
            this.tabDrivers.Padding = new System.Windows.Forms.Padding(3);
            this.tabDrivers.Size = new System.Drawing.Size(585, 363);
            this.tabDrivers.TabIndex = 0;
            this.tabDrivers.Text = "Installed LogicalDevices";
            this.tabDrivers.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(299, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 142);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Driver Info";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "TODO";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(299, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 155);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Parameters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = "TODO\r\n\r\nNOTE: May not be needed at all\r\n";
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
            // lbDrivers
            // 
            this.lbDrivers.FormattingEnabled = true;
            this.lbDrivers.Location = new System.Drawing.Point(10, 14);
            this.lbDrivers.Name = "lbDrivers";
            this.lbDrivers.Size = new System.Drawing.Size(261, 303);
            this.lbDrivers.TabIndex = 0;
            // 
            // tabDevices
            // 
            this.tabDevices.Controls.Add(this.btnConfigureDevice);
            this.tabDevices.Controls.Add(this.lbDevices);
            this.tabDevices.Controls.Add(this.btnFindDevices);
            this.tabDevices.Location = new System.Drawing.Point(4, 22);
            this.tabDevices.Name = "tabDevices";
            this.tabDevices.Padding = new System.Windows.Forms.Padding(3);
            this.tabDevices.Size = new System.Drawing.Size(585, 363);
            this.tabDevices.TabIndex = 1;
            this.tabDevices.Text = "Logical Devices";
            this.tabDevices.UseVisualStyleBackColor = true;
            // 
            // btnConfigureDevice
            // 
            this.btnConfigureDevice.Location = new System.Drawing.Point(247, 15);
            this.btnConfigureDevice.Name = "btnConfigureDevice";
            this.btnConfigureDevice.Size = new System.Drawing.Size(133, 23);
            this.btnConfigureDevice.TabIndex = 7;
            this.btnConfigureDevice.Text = "Configure Device";
            this.btnConfigureDevice.UseVisualStyleBackColor = true;
            this.btnConfigureDevice.Click += new System.EventHandler(this.btnConfigureDevice_Click);
            // 
            // lbDevices
            // 
            this.lbDevices.FormattingEnabled = true;
            this.lbDevices.Location = new System.Drawing.Point(17, 59);
            this.lbDevices.Name = "lbDevices";
            this.lbDevices.Size = new System.Drawing.Size(363, 173);
            this.lbDevices.TabIndex = 4;
            // 
            // btnFindDevices
            // 
            this.btnFindDevices.Location = new System.Drawing.Point(17, 15);
            this.btnFindDevices.Name = "btnFindDevices";
            this.btnFindDevices.Size = new System.Drawing.Size(181, 23);
            this.btnFindDevices.TabIndex = 3;
            this.btnFindDevices.Text = "Search for Attached Devices";
            this.btnFindDevices.UseVisualStyleBackColor = true;
            this.btnFindDevices.Click += new System.EventHandler(this.btnFindDevices_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 413);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "Horus Config";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabDrivers.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabDevices.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDrivers;
        private System.Windows.Forms.TabPage tabDevices;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbDrivers;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindDevices;
        private System.Windows.Forms.Button btnConfigureDevice;
        private System.Windows.Forms.Label label2;
        protected internal System.Windows.Forms.ListBox lbDevices;
    }
}

