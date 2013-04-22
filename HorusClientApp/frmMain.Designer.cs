﻿namespace HorusClientApp
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
            this.btnLocalCamera1 = new System.Windows.Forms.Button();
            this.btnLocalCamera2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRemoteCamera2 = new System.Windows.Forms.Button();
            this.btnRemoteCamera1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxEndpointV1 = new System.Windows.Forms.TextBox();
            this.User = new System.Windows.Forms.Label();
            this.tbxUser = new System.Windows.Forms.TextBox();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLocalCamera1
            // 
            this.btnLocalCamera1.Location = new System.Drawing.Point(27, 24);
            this.btnLocalCamera1.Name = "btnLocalCamera1";
            this.btnLocalCamera1.Size = new System.Drawing.Size(142, 23);
            this.btnLocalCamera1.TabIndex = 0;
            this.btnLocalCamera1.Text = "Camera1.Method1()";
            this.btnLocalCamera1.UseVisualStyleBackColor = true;
            this.btnLocalCamera1.Click += new System.EventHandler(this.btnLocalCamera1_Click);
            // 
            // btnLocalCamera2
            // 
            this.btnLocalCamera2.Location = new System.Drawing.Point(27, 72);
            this.btnLocalCamera2.Name = "btnLocalCamera2";
            this.btnLocalCamera2.Size = new System.Drawing.Size(142, 23);
            this.btnLocalCamera2.TabIndex = 2;
            this.btnLocalCamera2.Text = "Camera2.Method1()";
            this.btnLocalCamera2.UseVisualStyleBackColor = true;
            this.btnLocalCamera2.Click += new System.EventHandler(this.btnLocalCamera2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(500, 362);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnLocalCamera2);
            this.tabPage1.Controls.Add(this.btnLocalCamera1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(492, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Local CRL Client";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbxPassword);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.tbxUser);
            this.tabPage2.Controls.Add(this.User);
            this.tabPage2.Controls.Add(this.btnRemoteCamera2);
            this.tabPage2.Controls.Add(this.btnRemoteCamera1);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.tbxEndpointV1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(492, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTTP Client";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnRemoteCamera2
            // 
            this.btnRemoteCamera2.Location = new System.Drawing.Point(29, 195);
            this.btnRemoteCamera2.Name = "btnRemoteCamera2";
            this.btnRemoteCamera2.Size = new System.Drawing.Size(142, 23);
            this.btnRemoteCamera2.TabIndex = 2;
            this.btnRemoteCamera2.Text = "Unicorn2.Method1()";
            this.btnRemoteCamera2.UseVisualStyleBackColor = true;
            // 
            // btnRemoteCamera1
            // 
            this.btnRemoteCamera1.Location = new System.Drawing.Point(29, 139);
            this.btnRemoteCamera1.Name = "btnRemoteCamera1";
            this.btnRemoteCamera1.Size = new System.Drawing.Size(142, 23);
            this.btnRemoteCamera1.TabIndex = 0;
            this.btnRemoteCamera1.Text = "Unicorn1.Method1()";
            this.btnRemoteCamera1.UseVisualStyleBackColor = true;
            this.btnRemoteCamera1.Click += new System.EventHandler(this.btnRemoteCamera1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Endpoint";
            // 
            // tbxEndpointV1
            // 
            this.tbxEndpointV1.Location = new System.Drawing.Point(82, 16);
            this.tbxEndpointV1.Name = "tbxEndpointV1";
            this.tbxEndpointV1.Size = new System.Drawing.Size(325, 20);
            this.tbxEndpointV1.TabIndex = 8;
            this.tbxEndpointV1.Text = "http://localhost:8777";
            // 
            // User
            // 
            this.User.AutoSize = true;
            this.User.Location = new System.Drawing.Point(26, 59);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(49, 13);
            this.User.TabIndex = 10;
            this.User.Text = "Endpoint";
            // 
            // tbxUser
            // 
            this.tbxUser.Location = new System.Drawing.Point(82, 56);
            this.tbxUser.Name = "tbxUser";
            this.tbxUser.Size = new System.Drawing.Size(118, 20);
            this.tbxUser.TabIndex = 11;
            this.tbxUser.Text = "TestUser";
            // 
            // tbxPassword
            // 
            this.tbxPassword.Location = new System.Drawing.Point(289, 56);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new System.Drawing.Size(118, 20);
            this.tbxPassword.TabIndex = 13;
            this.tbxPassword.Text = "Pass";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Password";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 388);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "Horus POC Client";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button btnLocalCamera1;
		private System.Windows.Forms.Button btnLocalCamera2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxEndpointV1;
		private System.Windows.Forms.Button btnRemoteCamera1;
        private System.Windows.Forms.Button btnRemoteCamera2;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxUser;
        private System.Windows.Forms.Label User;
	}
}
