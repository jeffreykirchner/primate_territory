namespace Server
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.dgMain = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbControl = new System.Windows.Forms.GroupBox();
            this.cmdSetup5 = new System.Windows.Forms.Button();
            this.cmdSetup4 = new System.Windows.Forms.Button();
            this.cmdSetup3 = new System.Windows.Forms.Button();
            this.cmdSetup2 = new System.Windows.Forms.Button();
            this.cmdExchange = new System.Windows.Forms.Button();
            this.cmdSetup1 = new System.Windows.Forms.Button();
            this.cmdEndEarly = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdBegin = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblConnectionCount = new System.Windows.Forms.Label();
            this.lblLocalHost = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.cboPlayer = new System.Windows.Forms.ComboBox();
            this.cmdReplay = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.txtError = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.bwTakeSocketConnections = new System.ComponentModel.BackgroundWorker();
            this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
            this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Timer2 = new System.Windows.Forms.Timer(this.components);
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMain)).BeginInit();
            this.gbControl.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.TabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage4);
            this.TabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl1.Location = new System.Drawing.Point(12, 12);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(1291, 962);
            this.TabControl1.TabIndex = 9;
            this.TabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabControl1_MouseClick);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TabPage1.Controls.Add(this.cmdPrint);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.lbl2);
            this.TabPage1.Controls.Add(this.dgMain);
            this.TabPage1.Controls.Add(this.gbControl);
            this.TabPage1.Controls.Add(this.GroupBox1);
            this.TabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabPage1.Location = new System.Drawing.Point(4, 25);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1283, 933);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Main";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPrint.Location = new System.Drawing.Point(615, 833);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(79, 33);
            this.cmdPrint.TabIndex = 13;
            this.cmdPrint.Text = "Print ";
            this.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(889, 830);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(324, 44);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "Designed By: Jordan Adamson, Jeffrey Kirchner and Bart Wilson";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(75, 841);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(234, 16);
            this.lbl2.TabIndex = 11;
            this.lbl2.Text = "Programmed By: Jeffrey Kirchner";
            // 
            // dgMain
            // 
            this.dgMain.AllowUserToAddRows = false;
            this.dgMain.AllowUserToDeleteRows = false;
            this.dgMain.AllowUserToResizeColumns = false;
            this.dgMain.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dgMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgMain.ColumnHeadersHeight = 26;
            this.dgMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column12});
            this.dgMain.Location = new System.Drawing.Point(74, 216);
            this.dgMain.Name = "dgMain";
            this.dgMain.ReadOnly = true;
            this.dgMain.RowHeadersVisible = false;
            this.dgMain.Size = new System.Drawing.Size(1142, 611);
            this.dgMain.TabIndex = 10;
            // 
            // Column1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.HeaderText = "Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 600;
            // 
            // Column3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column3.HeaderText = "Status";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 275;
            // 
            // Column4
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column4.HeaderText = "Earnings";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 110;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column12.HeaderText = "Partner";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 64;
            // 
            // gbControl
            // 
            this.gbControl.Controls.Add(this.cmdSetup5);
            this.gbControl.Controls.Add(this.cmdSetup4);
            this.gbControl.Controls.Add(this.cmdSetup3);
            this.gbControl.Controls.Add(this.cmdSetup2);
            this.gbControl.Controls.Add(this.cmdExchange);
            this.gbControl.Controls.Add(this.cmdSetup1);
            this.gbControl.Controls.Add(this.cmdEndEarly);
            this.gbControl.Controls.Add(this.cmdExit);
            this.gbControl.Controls.Add(this.cmdLoad);
            this.gbControl.Controls.Add(this.cmdSave);
            this.gbControl.Controls.Add(this.cmdReset);
            this.gbControl.Controls.Add(this.cmdBegin);
            this.gbControl.Location = new System.Drawing.Point(307, 57);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(909, 153);
            this.gbControl.TabIndex = 9;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "Control";
            // 
            // cmdSetup5
            // 
            this.cmdSetup5.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetup5.Image")));
            this.cmdSetup5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSetup5.Location = new System.Drawing.Point(610, 81);
            this.cmdSetup5.Name = "cmdSetup5";
            this.cmdSetup5.Size = new System.Drawing.Size(145, 59);
            this.cmdSetup5.TabIndex = 13;
            this.cmdSetup5.Text = "         Setup 5";
            this.cmdSetup5.UseVisualStyleBackColor = true;
            this.cmdSetup5.Visible = false;
            this.cmdSetup5.Click += new System.EventHandler(this.cmdSetup5_Click);
            // 
            // cmdSetup4
            // 
            this.cmdSetup4.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetup4.Image")));
            this.cmdSetup4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSetup4.Location = new System.Drawing.Point(459, 81);
            this.cmdSetup4.Name = "cmdSetup4";
            this.cmdSetup4.Size = new System.Drawing.Size(145, 59);
            this.cmdSetup4.TabIndex = 12;
            this.cmdSetup4.Text = "         Setup 4";
            this.cmdSetup4.UseVisualStyleBackColor = true;
            this.cmdSetup4.Visible = false;
            this.cmdSetup4.Click += new System.EventHandler(this.cmdSetup4_Click);
            // 
            // cmdSetup3
            // 
            this.cmdSetup3.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetup3.Image")));
            this.cmdSetup3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSetup3.Location = new System.Drawing.Point(459, 16);
            this.cmdSetup3.Name = "cmdSetup3";
            this.cmdSetup3.Size = new System.Drawing.Size(145, 59);
            this.cmdSetup3.TabIndex = 11;
            this.cmdSetup3.Text = "             Treatment \r\n             Setup";
            this.cmdSetup3.UseVisualStyleBackColor = true;
            this.cmdSetup3.Click += new System.EventHandler(this.cmdSetup3_Click);
            // 
            // cmdSetup2
            // 
            this.cmdSetup2.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetup2.Image")));
            this.cmdSetup2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSetup2.Location = new System.Drawing.Point(308, 81);
            this.cmdSetup2.Name = "cmdSetup2";
            this.cmdSetup2.Size = new System.Drawing.Size(145, 59);
            this.cmdSetup2.TabIndex = 10;
            this.cmdSetup2.Text = "            Period\r\n            Setup";
            this.cmdSetup2.UseVisualStyleBackColor = true;
            this.cmdSetup2.Click += new System.EventHandler(this.cmdSetup2_Click);
            // 
            // cmdExchange
            // 
            this.cmdExchange.Image = ((System.Drawing.Image)(resources.GetObject("cmdExchange.Image")));
            this.cmdExchange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExchange.Location = new System.Drawing.Point(610, 16);
            this.cmdExchange.Name = "cmdExchange";
            this.cmdExchange.Size = new System.Drawing.Size(145, 59);
            this.cmdExchange.TabIndex = 9;
            this.cmdExchange.Text = "             Exchange\r\n             Rate";
            this.cmdExchange.UseVisualStyleBackColor = true;
            this.cmdExchange.Visible = false;
            this.cmdExchange.Click += new System.EventHandler(this.cmdExchange_Click);
            // 
            // cmdSetup1
            // 
            this.cmdSetup1.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetup1.Image")));
            this.cmdSetup1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSetup1.Location = new System.Drawing.Point(308, 16);
            this.cmdSetup1.Name = "cmdSetup1";
            this.cmdSetup1.Size = new System.Drawing.Size(145, 59);
            this.cmdSetup1.TabIndex = 8;
            this.cmdSetup1.Text = "           Game\r\n           Setup";
            this.cmdSetup1.UseVisualStyleBackColor = true;
            this.cmdSetup1.Click += new System.EventHandler(this.cmdSetup1_Click);
            // 
            // cmdEndEarly
            // 
            this.cmdEndEarly.Enabled = false;
            this.cmdEndEarly.Image = ((System.Drawing.Image)(resources.GetObject("cmdEndEarly.Image")));
            this.cmdEndEarly.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdEndEarly.Location = new System.Drawing.Point(761, 16);
            this.cmdEndEarly.Name = "cmdEndEarly";
            this.cmdEndEarly.Size = new System.Drawing.Size(145, 59);
            this.cmdEndEarly.TabIndex = 7;
            this.cmdEndEarly.Text = "          End\r\n          Early";
            this.cmdEndEarly.UseVisualStyleBackColor = true;
            this.cmdEndEarly.Click += new System.EventHandler(this.cmdEndEarly_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExit.Location = new System.Drawing.Point(761, 81);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(145, 59);
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "       Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoad.Image")));
            this.cmdLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdLoad.Location = new System.Drawing.Point(157, 81);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(145, 59);
            this.cmdLoad.TabIndex = 5;
            this.cmdLoad.Text = "          Load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(157, 16);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(145, 59);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "           Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdReset
            // 
            this.cmdReset.Image = ((System.Drawing.Image)(resources.GetObject("cmdReset.Image")));
            this.cmdReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdReset.Location = new System.Drawing.Point(6, 81);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(145, 59);
            this.cmdReset.TabIndex = 1;
            this.cmdReset.Text = "          Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdBegin
            // 
            this.cmdBegin.Image = ((System.Drawing.Image)(resources.GetObject("cmdBegin.Image")));
            this.cmdBegin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdBegin.Location = new System.Drawing.Point(6, 16);
            this.cmdBegin.Name = "cmdBegin";
            this.cmdBegin.Size = new System.Drawing.Size(145, 59);
            this.cmdBegin.TabIndex = 2;
            this.cmdBegin.Text = "          Begin";
            this.cmdBegin.UseVisualStyleBackColor = true;
            this.cmdBegin.Click += new System.EventHandler(this.cmdBegin_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lblConnectionCount);
            this.GroupBox1.Controls.Add(this.lblLocalHost);
            this.GroupBox1.Controls.Add(this.lblIpAddress);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.lbl1);
            this.GroupBox1.Location = new System.Drawing.Point(74, 57);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(227, 153);
            this.GroupBox1.TabIndex = 8;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Status";
            // 
            // lblConnectionCount
            // 
            this.lblConnectionCount.BackColor = System.Drawing.Color.Transparent;
            this.lblConnectionCount.Location = new System.Drawing.Point(107, 100);
            this.lblConnectionCount.Name = "lblConnectionCount";
            this.lblConnectionCount.Size = new System.Drawing.Size(120, 18);
            this.lblConnectionCount.TabIndex = 5;
            this.lblConnectionCount.Text = "000";
            this.lblConnectionCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocalHost
            // 
            this.lblLocalHost.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalHost.Location = new System.Drawing.Point(107, 72);
            this.lblLocalHost.Name = "lblLocalHost";
            this.lblLocalHost.Size = new System.Drawing.Size(120, 18);
            this.lblLocalHost.TabIndex = 4;
            this.lblLocalHost.Text = "Localhost";
            this.lblLocalHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblIpAddress.Location = new System.Drawing.Point(107, 44);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(120, 18);
            this.lblIpAddress.TabIndex = 3;
            this.lblIpAddress.Text = "888.888.888.888";
            this.lblIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(7, 100);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(98, 18);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Connections:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(7, 72);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(98, 18);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "Host:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl1
            // 
            this.lbl1.Location = new System.Drawing.Point(7, 44);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(98, 18);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "IP Address:";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TabPage2.Controls.Add(this.label6);
            this.TabPage2.Controls.Add(this.cboPlayer);
            this.TabPage2.Controls.Add(this.cmdReplay);
            this.TabPage2.Controls.Add(this.pnlMain);
            this.TabPage2.Location = new System.Drawing.Point(4, 25);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1283, 933);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Status";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1053, 843);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 24);
            this.label6.TabIndex = 58;
            this.label6.Text = "Player";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboPlayer
            // 
            this.cboPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPlayer.FormattingEnabled = true;
            this.cboPlayer.Location = new System.Drawing.Point(1053, 870);
            this.cboPlayer.Name = "cboPlayer";
            this.cboPlayer.Size = new System.Drawing.Size(168, 33);
            this.cboPlayer.TabIndex = 57;
            this.cboPlayer.SelectedIndexChanged += new System.EventHandler(this.cboPlayer_SelectedIndexChanged);
            // 
            // cmdReplay
            // 
            this.cmdReplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReplay.Location = new System.Drawing.Point(580, 870);
            this.cmdReplay.Name = "cmdReplay";
            this.cmdReplay.Size = new System.Drawing.Size(99, 39);
            this.cmdReplay.TabIndex = 56;
            this.cmdReplay.Text = "Replay";
            this.cmdReplay.UseVisualStyleBackColor = true;
            this.cmdReplay.Click += new System.EventHandler(this.cmdReplay_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Location = new System.Drawing.Point(51, 57);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1186, 769);
            this.pnlMain.TabIndex = 50;
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage4.Controls.Add(this.txtError);
            this.TabPage4.Controls.Add(this.Label2);
            this.TabPage4.Controls.Add(this.txtMain);
            this.TabPage4.Controls.Add(this.Label1);
            this.TabPage4.Location = new System.Drawing.Point(4, 25);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage4.Size = new System.Drawing.Size(1283, 933);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Messages";
            // 
            // txtError
            // 
            this.txtError.BackColor = System.Drawing.Color.White;
            this.txtError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtError.Location = new System.Drawing.Point(644, 32);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtError.Size = new System.Drawing.Size(630, 790);
            this.txtError.TabIndex = 8;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(649, 14);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(50, 16);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Errors";
            // 
            // txtMain
            // 
            this.txtMain.BackColor = System.Drawing.Color.White;
            this.txtMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMain.Location = new System.Drawing.Point(6, 32);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.ReadOnly = true;
            this.txtMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMain.Size = new System.Drawing.Size(630, 790);
            this.txtMain.TabIndex = 7;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(80, 16);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Messages";
            // 
            // bwTakeSocketConnections
            // 
            this.bwTakeSocketConnections.WorkerSupportsCancellation = true;
            this.bwTakeSocketConnections.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwTakeSocketConnections_DoWork);
            this.bwTakeSocketConnections.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwTakeSocketConnections_RunWorkerCompleted);
            // 
            // PrintDialog1
            // 
            this.PrintDialog1.Document = this.PrintDocument1;
            this.PrintDialog1.UseEXDialog = true;
            // 
            // PrintDocument1
            // 
            this.PrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // Timer2
            // 
            this.Timer2.Interval = 1000;
            this.Timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 981);
            this.ControlBox = false;
            this.Controls.Add(this.TabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing_1);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMain)).EndInit();
            this.gbControl.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl TabControl1;
        internal System.Windows.Forms.TabPage TabPage1;
        internal System.Windows.Forms.Button cmdPrint;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label lbl2;
        internal System.Windows.Forms.DataGridView dgMain;
        internal System.Windows.Forms.GroupBox gbControl;
        internal System.Windows.Forms.Button cmdSetup5;
        internal System.Windows.Forms.Button cmdSetup4;
        internal System.Windows.Forms.Button cmdSetup3;
        internal System.Windows.Forms.Button cmdSetup2;
        internal System.Windows.Forms.Button cmdExchange;
        internal System.Windows.Forms.Button cmdSetup1;
        internal System.Windows.Forms.Button cmdEndEarly;
        internal System.Windows.Forms.Button cmdExit;
        internal System.Windows.Forms.Button cmdLoad;
        internal System.Windows.Forms.Button cmdSave;
        internal System.Windows.Forms.Button cmdReset;
        internal System.Windows.Forms.Button cmdBegin;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label lblConnectionCount;
        internal System.Windows.Forms.Label lblLocalHost;
        internal System.Windows.Forms.Label lblIpAddress;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label lbl1;
        internal System.Windows.Forms.TabPage TabPage2;
        internal System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.TabPage TabPage4;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtMain;
        internal System.Windows.Forms.Label Label1;
        internal System.ComponentModel.BackgroundWorker bwTakeSocketConnections;
        internal System.Windows.Forms.PrintDialog PrintDialog1;
        internal System.Drawing.Printing.PrintDocument PrintDocument1;
        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        internal System.Windows.Forms.Timer Timer2;
        private System.Windows.Forms.Button cmdReplay;
        public System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.ComboBox cboPlayer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}