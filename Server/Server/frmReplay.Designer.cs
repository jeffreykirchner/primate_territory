namespace Server
{
    partial class frmReplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReplay));
            this.cmdPauseData = new System.Windows.Forms.Button();
            this.cmdLoadData = new System.Windows.Forms.Button();
            this.tbData = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cmdPlayData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbData)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdPauseData
            // 
            this.cmdPauseData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPauseData.Image = ((System.Drawing.Image)(resources.GetObject("cmdPauseData.Image")));
            this.cmdPauseData.Location = new System.Drawing.Point(58, 6);
            this.cmdPauseData.Name = "cmdPauseData";
            this.cmdPauseData.Size = new System.Drawing.Size(44, 39);
            this.cmdPauseData.TabIndex = 62;
            this.cmdPauseData.UseVisualStyleBackColor = true;
            this.cmdPauseData.Click += new System.EventHandler(this.cmdPauseData_Click);
            // 
            // cmdLoadData
            // 
            this.cmdLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLoadData.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoadData.Image")));
            this.cmdLoadData.Location = new System.Drawing.Point(8, 7);
            this.cmdLoadData.Name = "cmdLoadData";
            this.cmdLoadData.Size = new System.Drawing.Size(44, 38);
            this.cmdLoadData.TabIndex = 60;
            this.cmdLoadData.UseVisualStyleBackColor = true;
            this.cmdLoadData.Click += new System.EventHandler(this.cmdLoadData_Click);
            // 
            // tbData
            // 
            this.tbData.LargeChange = 1;
            this.tbData.Location = new System.Drawing.Point(108, 3);
            this.tbData.Minimum = 1;
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(525, 45);
            this.tbData.TabIndex = 61;
            this.tbData.TickFrequency = 10;
            this.tbData.Value = 1;
            this.tbData.Scroll += new System.EventHandler(this.tbData_Scroll);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // cmdPlayData
            // 
            this.cmdPlayData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPlayData.Image = ((System.Drawing.Image)(resources.GetObject("cmdPlayData.Image")));
            this.cmdPlayData.Location = new System.Drawing.Point(68, 12);
            this.cmdPlayData.Name = "cmdPlayData";
            this.cmdPlayData.Size = new System.Drawing.Size(44, 38);
            this.cmdPlayData.TabIndex = 63;
            this.cmdPlayData.UseVisualStyleBackColor = true;
            this.cmdPlayData.Visible = false;
            this.cmdPlayData.Click += new System.EventHandler(this.cmdPlayData_Click);
            // 
            // frmReplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 51);
            this.Controls.Add(this.cmdPauseData);
            this.Controls.Add(this.cmdPlayData);
            this.Controls.Add(this.cmdLoadData);
            this.Controls.Add(this.tbData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReplay";
            this.Text = "Replay";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReplay_FormClosing);
            this.Load += new System.EventHandler(this.frmReplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button cmdPauseData;
        internal System.Windows.Forms.Button cmdLoadData;
        internal System.Windows.Forms.TrackBar tbData;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.Button cmdPlayData;
    }
}