namespace Client
{
    partial class frm1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pnlMain = new System.Windows.Forms.Panel();
            this.cmdSubmit = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.gbChat = new System.Windows.Forms.GroupBox();
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.cmdChat = new System.Windows.Forms.Button();
            this.gbChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtMessages
            // 
            this.txtMessages.BackColor = System.Drawing.Color.White;
            this.txtMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessages.Location = new System.Drawing.Point(12, 790);
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.Size = new System.Drawing.Size(932, 31);
            this.txtMessages.TabIndex = 38;
            this.txtMessages.TabStop = false;
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Location = new System.Drawing.Point(12, 15);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1186, 769);
            this.pnlMain.TabIndex = 40;
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            this.pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseMove);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseUp);
            // 
            // cmdSubmit
            // 
            this.cmdSubmit.AutoSize = true;
            this.cmdSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSubmit.Location = new System.Drawing.Point(950, 789);
            this.cmdSubmit.Name = "cmdSubmit";
            this.cmdSubmit.Size = new System.Drawing.Size(248, 34);
            this.cmdSubmit.TabIndex = 41;
            this.cmdSubmit.TabStop = false;
            this.cmdSubmit.Text = "Start";
            this.cmdSubmit.UseVisualStyleBackColor = false;
            this.cmdSubmit.Click += new System.EventHandler(this.cmdSubmit_Click);
            // 
            // timer3
            // 
            this.timer3.Interval = 500;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // gbChat
            // 
            this.gbChat.Controls.Add(this.rtbChat);
            this.gbChat.Controls.Add(this.txtChat);
            this.gbChat.Controls.Add(this.cmdChat);
            this.gbChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbChat.Location = new System.Drawing.Point(1204, 9);
            this.gbChat.Name = "gbChat";
            this.gbChat.Size = new System.Drawing.Size(306, 814);
            this.gbChat.TabIndex = 42;
            this.gbChat.TabStop = false;
            this.gbChat.Text = "Chat";
            // 
            // rtbChat
            // 
            this.rtbChat.BackColor = System.Drawing.Color.White;
            this.rtbChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbChat.Location = new System.Drawing.Point(6, 21);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbChat.Size = new System.Drawing.Size(294, 745);
            this.rtbChat.TabIndex = 44;
            this.rtbChat.Text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
            // 
            // txtChat
            // 
            this.txtChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChat.Location = new System.Drawing.Point(6, 776);
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(250, 24);
            this.txtChat.TabIndex = 43;
            this.txtChat.TabStop = false;
            this.txtChat.Text = "Type here to chat ...";
            this.txtChat.Click += new System.EventHandler(this.txtChat_Click);
            this.txtChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChat_KeyDown);
            // 
            // cmdChat
            // 
            this.cmdChat.AutoSize = true;
            this.cmdChat.BackColor = System.Drawing.SystemColors.Control;
            this.cmdChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChat.Image = ((System.Drawing.Image)(resources.GetObject("cmdChat.Image")));
            this.cmdChat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdChat.Location = new System.Drawing.Point(262, 772);
            this.cmdChat.Name = "cmdChat";
            this.cmdChat.Size = new System.Drawing.Size(38, 34);
            this.cmdChat.TabIndex = 42;
            this.cmdChat.TabStop = false;
            this.cmdChat.UseVisualStyleBackColor = false;
            this.cmdChat.Click += new System.EventHandler(this.cmdChat_Click);
            // 
            // frm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1522, 831);
            this.ControlBox = false;
            this.Controls.Add(this.gbChat);
            this.Controls.Add(this.cmdSubmit);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.txtMessages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frm1";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm1_FormClosing);
            this.Load += new System.EventHandler(this.frm1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm1_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm1_MouseMove);
            this.gbChat.ResumeLayout(false);
            this.gbChat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.TextBox txtMessages;
        public System.Windows.Forms.Timer timer2;
        public System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Timer timer3;
        public System.Windows.Forms.Timer timer4;
        public System.Windows.Forms.Button cmdSubmit;
        private System.Windows.Forms.GroupBox gbChat;
        public System.Windows.Forms.Button cmdChat;
        private System.Windows.Forms.TextBox txtChat;
        public System.Windows.Forms.RichTextBox rtbChat;
    }
}