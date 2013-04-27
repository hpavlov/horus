namespace HorusClientApp
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;



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
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxUser = new System.Windows.Forms.TextBox();
            this.User = new System.Windows.Forms.Label();
            this.btnRemoteCamera2 = new System.Windows.Forms.Button();
            this.btnRemoteCamera1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxEndpointV1 = new System.Windows.Forms.TextBox();
            this.tabVideo = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picboxVideo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnAction = new System.Windows.Forms.Button();
            this.cbLogicalVideoDevices = new System.Windows.Forms.ComboBox();
            this.tabDome = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDomeAction = new System.Windows.Forms.Button();
            this.cbLogicalDomeDevices = new System.Windows.Forms.ComboBox();
            this.btnDomeConnect = new System.Windows.Forms.Button();
            this.btnDomeDisconnect = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabVideo.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxVideo)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabDome.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabVideo);
            this.tabControl1.Controls.Add(this.tabDome);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 538);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnLocalCamera2);
            this.tabPage1.Controls.Add(this.btnLocalCamera1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 512);
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
            this.tabPage2.Size = new System.Drawing.Size(752, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTTP Client";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // tbxUser
            // 
            this.tbxUser.Location = new System.Drawing.Point(82, 56);
            this.tbxUser.Name = "tbxUser";
            this.tbxUser.Size = new System.Drawing.Size(118, 20);
            this.tbxUser.TabIndex = 11;
            this.tbxUser.Text = "TestUser";
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
            // tabVideo
            // 
            this.tabVideo.Controls.Add(this.panel2);
            this.tabVideo.Controls.Add(this.panel1);
            this.tabVideo.Location = new System.Drawing.Point(4, 22);
            this.tabVideo.Name = "tabVideo";
            this.tabVideo.Padding = new System.Windows.Forms.Padding(3);
            this.tabVideo.Size = new System.Drawing.Size(752, 512);
            this.tabVideo.TabIndex = 2;
            this.tabVideo.Text = "Video Client";
            this.tabVideo.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picboxVideo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(746, 467);
            this.panel2.TabIndex = 1;
            // 
            // picboxVideo
            // 
            this.picboxVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picboxVideo.Location = new System.Drawing.Point(0, 0);
            this.picboxVideo.Name = "picboxVideo";
            this.picboxVideo.Size = new System.Drawing.Size(746, 467);
            this.picboxVideo.TabIndex = 0;
            this.picboxVideo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.btnAction);
            this.panel1.Controls.Add(this.cbLogicalVideoDevices);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(746, 39);
            this.panel1.TabIndex = 0;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Location = new System.Drawing.Point(418, 10);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(83, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Local HTTP";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(364, 10);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(51, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Local";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(7, 7);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(98, 23);
            this.btnAction.TabIndex = 1;
            this.btnAction.Text = "List Devices";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // cbLogicalVideoDevices
            // 
            this.cbLogicalVideoDevices.FormattingEnabled = true;
            this.cbLogicalVideoDevices.Location = new System.Drawing.Point(117, 8);
            this.cbLogicalVideoDevices.Name = "cbLogicalVideoDevices";
            this.cbLogicalVideoDevices.Size = new System.Drawing.Size(240, 21);
            this.cbLogicalVideoDevices.TabIndex = 0;
            // 
            // tabDome
            // 
            this.tabDome.Controls.Add(this.panel4);
            this.tabDome.Controls.Add(this.panel3);
            this.tabDome.Location = new System.Drawing.Point(4, 22);
            this.tabDome.Name = "tabDome";
            this.tabDome.Padding = new System.Windows.Forms.Padding(3);
            this.tabDome.Size = new System.Drawing.Size(752, 512);
            this.tabDome.TabIndex = 3;
            this.tabDome.Text = "Dome Client";
            this.tabDome.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 62);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(746, 447);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDomeDisconnect);
            this.panel3.Controls.Add(this.btnDomeConnect);
            this.panel3.Controls.Add(this.cbLogicalDomeDevices);
            this.panel3.Controls.Add(this.btnDomeAction);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(746, 59);
            this.panel3.TabIndex = 0;
            // 
            // btnDomeAction
            // 
            this.btnDomeAction.Location = new System.Drawing.Point(14, 15);
            this.btnDomeAction.Name = "btnDomeAction";
            this.btnDomeAction.Size = new System.Drawing.Size(122, 23);
            this.btnDomeAction.TabIndex = 0;
            this.btnDomeAction.Text = "Select Dome";
            this.btnDomeAction.UseVisualStyleBackColor = true;
            this.btnDomeAction.Click += new System.EventHandler(this.btnDomeAction_Click);
            // 
            // cbLogicalDomeDevices
            // 
            this.cbLogicalDomeDevices.FormattingEnabled = true;
            this.cbLogicalDomeDevices.Location = new System.Drawing.Point(143, 16);
            this.cbLogicalDomeDevices.Name = "cbLogicalDomeDevices";
            this.cbLogicalDomeDevices.Size = new System.Drawing.Size(290, 21);
            this.cbLogicalDomeDevices.TabIndex = 1;
            // 
            // btnDomeConnect
            // 
            this.btnDomeConnect.Location = new System.Drawing.Point(487, 14);
            this.btnDomeConnect.Name = "btnDomeConnect";
            this.btnDomeConnect.Size = new System.Drawing.Size(75, 23);
            this.btnDomeConnect.TabIndex = 2;
            this.btnDomeConnect.Text = "Connect";
            this.btnDomeConnect.UseVisualStyleBackColor = true;
            this.btnDomeConnect.Click += new System.EventHandler(this.btnDomeConnect_Click);
            // 
            // btnDomeDisconnect
            // 
            this.btnDomeDisconnect.Location = new System.Drawing.Point(578, 14);
            this.btnDomeDisconnect.Name = "btnDomeDisconnect";
            this.btnDomeDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDomeDisconnect.TabIndex = 2;
            this.btnDomeDisconnect.Text = "Disconnect";
            this.btnDomeDisconnect.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "Horus POC Client";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabVideo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picboxVideo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabDome.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabVideo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbLogicalVideoDevices;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        protected internal System.Windows.Forms.PictureBox picboxVideo;
        private System.Windows.Forms.TabPage tabDome;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDomeAction;
        private System.Windows.Forms.ComboBox cbLogicalDomeDevices;
        private System.Windows.Forms.Button btnDomeDisconnect;
        private System.Windows.Forms.Button btnDomeConnect;
	}
}

