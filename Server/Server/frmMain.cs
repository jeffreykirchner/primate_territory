using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public partial class frmMain : Form
    {
        //network variables
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = default(IPAddress);
        IPEndPoint localEndPoint = default(IPEndPoint);

        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        bool resetPressed = false;

        //graphics
        public Screen mainScreen;

        public StringFormat fmt = new StringFormat(); //center alignment
        public StringFormat fmtL = new StringFormat(); //left alignment
        public StringFormat fmtR = new StringFormat(); //right alignment

        public Font f6 = new Font("Microsoft Sans Serif", 6, System.Drawing.FontStyle.Bold);
        public Font f8 = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
        public Font f10 = new Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);
        public Font f12 = new Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Bold);
        public Font f16 = new Font("Microsoft Sans Serif", 16, System.Drawing.FontStyle.Bold);
        public Font f18 = new Font("Microsoft Sans Serif", 18, System.Drawing.FontStyle.Bold);

        public float xMargin = 110;
        public float yMargin = 75;
        public float otherMargin = 15;
        public float graphHeight = 0;
        public float graphWidth = 0;
        public float xTickHeight = 20;
        public Matrix graphTransform;

        public Pen p3Black;
        public Pen p5Black;
        public Pen p5BlackDash;

        public Pen p3Red;
        public Pen p3Blue;

        public Color p1Color = Color.FromArgb(175, 0, 0, 255);
        public Color p2Color = Color.FromArgb(175, 255, 0, 0);

        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            Common.start();

            //find the IPv4 address
            for (int i = 1; i <= ipHostInfo.AddressList.Length; i++)
            {
                if (ipHostInfo.AddressList[i - 1].ToString().IndexOf(".") > -1)
                {
                    ipAddress = ipHostInfo.AddressList[i - 1];
                    break;
                }
            }

            //start network listener
            localEndPoint = new IPEndPoint(IPAddress.Any, Common.portNumber);

            listener.Bind(localEndPoint);
            listener.Listen(10);

            bwTakeSocketConnections.RunWorkerAsync();

            //display network info on form
            lblIpAddress.Text = ipAddress.ToString();
            lblLocalHost.Text = SystemInformation.ComputerName;
            lblConnectionCount.Text = "0";

            //graphics
            mainScreen = new Screen(pnlMain, new Rectangle(0, 0, pnlMain.Width, pnlMain.Height));

            fmt.Alignment = StringAlignment.Center;
            fmtL.Alignment = StringAlignment.Near;
            fmtR.Alignment = StringAlignment.Far;

            setupPen(ref p3Black, Color.Black, 3);
            setupPen(ref p3Red, Color.Black, 3);
            setupPen(ref p3Blue, p1Color, 3);
            setupPen(ref p3Red, p2Color, 3);

            setupPen(ref p5Black, Color.Black, 5);
            setupPen(ref p5BlackDash, Color.Black, 5);

            p5BlackDash.DashStyle = DashStyle.Dash;

            graphHeight = pnlMain.Height - xMargin - otherMargin;
            graphWidth = pnlMain.Width - yMargin - otherMargin;

            Graphics g = mainScreen.GetGraphics();
            g.TranslateTransform(yMargin + graphWidth / 2, pnlMain.Height - xMargin);
            graphTransform = g.Transform;
            g.ResetTransform();
        }

        private void cmdBegin_Click(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = "";
                TabControl1.TabPages[2].Text = "Messages";

                Common.loadParameters();
                Common.closing = false;

                //if the number of connections does not match the parameters then exit
                if (Common.numberOfPlayers != Common.clientCount)
                {
                    MessageBox.Show("Incorrect number of clients");
                    return;
                }             

                //data files               
                string tempTime = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour +
                                  "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;

                //store parameters
                string filename = "Parameters_" + tempTime + ".csv";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;
                INI.writeINI(Common.sfile, "GameSettings", "gameName", "ESI Software2");
                INI.writeINI(Common.sfile, "GameSettings", "gameName", "ESI Software");
                File.Copy(Common.sfile, filename);

                //recruiter payment file
                filename = "Recruiter_Payments_" + tempTime + ".csv";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;
                Common.recruiterDf = File.CreateText(filename);
                Common.recruiterDf.AutoFlush = true;

                //summary data file
                filename = "Summary_Data_" + tempTime + ".csv";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;

                Common.summaryDf = File.CreateText(filename);
                Common.summaryDf.AutoFlush = true;

                string str = "SessionType,Period,Treatment,BlueShare,RedShare,Player,Color,Partner,LeftLocation,LeftValue,RightLocation,RightValue,RangeTotal,RangeOverlap,Revenue,TotalCost,Revenue(cents),TotalCost(cents),Earnings(cents),GraphLeftX,GraphLeftY,GraphMiddleX,GraphMiddleY,GraphRightX,GraphRightY,BlueCost,RedCost";

                Common.summaryDf.WriteLine(str);

                //replay data file
                filename = "Replay_Data_" + tempTime + ".json";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;

                Common.replayDf = File.CreateText(filename);
                Common.replayDf.AutoFlush = true;

                //earnings datafile
                filename = "Earnings_Data_" + tempTime + ".csv";
                filename = Application.StartupPath.ToString() + "\\datafiles\\" + filename;

                Common.earningsDf = File.CreateText(filename);
                Common.earningsDf.AutoFlush = true;

                //summary table
                dgMain.RowCount = Common.numberOfPlayers;               

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    dgMain[0, i - 1].Value = i;
                    dgMain[1, i - 1].Value = Common.playerlist[i].sp.remoteComputerName;
                    dgMain[3, i - 1].Value = string.Format(Common.culture,"{0:C}", 0);

                    Common.playerlist[i].partner = -1;
                    Common.playerlist[i].earnings = 0;
                }

                dgMain.CurrentCell.Selected = false;

                //setup screen
                cmdBegin.Enabled = false;
                cmdEndEarly.Enabled = true;
                cmdExchange.Enabled = false;
                cmdExit.Enabled = false;
                cmdLoad.Enabled = false;
                cmdSetup1.Enabled = false;
                cmdSetup2.Enabled = false;
                cmdSetup3.Enabled = false;
                cmdSetup4.Enabled = false;
                cmdSetup5.Enabled = false;
                cmdReplay.Enabled = false;

                //send signal to begin to clients
                string outstr = "";

                setup();

                //setup player types
                if(Common.numberOfPlayers ==1)
                {
                    Common.playerlist[1].myType = 1;
                    Common.playerlist[1].partner = 1;
                    Common.playerlist[1].setupGraphics();
                }
                else
                {
                    for (int i = 1; i <= Common.numberOfPlayers; i++)
                    {
                        if (i <= Common.numberOfPlayers / 2)
                        {
                            Common.playerlist[i].myType = 1;
                        }
                        else
                        {
                            Common.playerlist[i].myType = 2;
                        }

                        Common.playerlist[i].setupGraphics();
                    }
                }
                
                //setup partners
                for(int i=1;i<=Common.numberOfPlayers;i++)
                {
                    if(Common.playerlist[i].partner == -1)
                    {
                        while(Common.playerlist[i].partner == -1)
                        {
                            int r = Rand.rand(Common.numberOfPlayers, 1);

                            if(Common.playerlist[r].myType != Common.playerlist[i].myType &&
                               Common.playerlist[r].partner == -1)
                            {
                                Common.playerlist[r].partner = i;
                                Common.playerlist[i].partner = r;
                            }
                        }
                    }
                }
                                
                //send begin
                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    Common.playerlist[i].sendBegin(outstr);
                    dgMain[4, i - 1].Value = Common.playerlist[i].partner;
                }

                Common.checkin = 0;
                Timer1.Enabled = true;
                Common.replayWritten = false;               
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }

        }

        public void setup()
        {
            try
            {
                Common.currentPeriod = 1;
                Common.selectedPeriod = 1;
                Common.selectedPlayer = 1;

                //setup treatments
                Common.treatmentCount = int.Parse(INI.getINI(Common.sfile, "treatments", "count"));
                for (int i = 1; i <= Common.treatmentCount; i++)
                {
                    Common.treaments[i] = new Treatment();
                    Common.treaments[i].fromString(INI.getINI(Common.sfile, "treatments", i.ToString()));
                }

                //setup periods
                for (int i = 1; i <= Common.numberOfPeriods; i++)
                {
                    Common.periods[i] = new Period();
                    string[] msgtokens = INI.getINI(Common.sfile, "periods", i.ToString()).Split(';');

                    Common.periods[i].setup(msgtokens);
                }

                cboPlayer.Items.Clear();

                //setup player graphics
                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    cboPlayer.Items.Add("Player " + i);
                }

                cboPlayer.SelectedIndex = 0;
               
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
       

        private void cmdReset_Click(object sender, EventArgs e)
        {
            try
            {               

                for (int i = 1; i <= Common.clientCount; i++)
                {
                    Common.playerlist[i].sendReset();
                }

                Application.DoEvents();
                Common.closing = true;

                Common.writeReplayJSON();

                if (Common.summaryDf != null) Common.summaryDf.Close();
                if (Common.replayDf != null) Common.replayDf.Close();
                if (Common.recruiterDf != null) Common.recruiterDf.Close();
                if (Common.earningsDf != null) Common.earningsDf.Close();

                bwTakeSocketConnections.CancelAsync();                
                listener.Close();

                cmdBegin.Enabled = true;
                cmdEndEarly.Enabled = false;
                cmdExchange.Enabled = true;
                cmdExit.Enabled = true;
                cmdLoad.Enabled = true;
                cmdSetup1.Enabled = true;
                cmdSetup2.Enabled = true;
                cmdSetup3.Enabled = true;
                cmdSetup4.Enabled = true;
                cmdSetup5.Enabled = true;
                cmdReplay.Enabled = true;

                dgMain.RowCount = 0;

                Timer1.Enabled = false;
                cboPlayer.Items.Clear();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                //save current parameters to a text file so they can be loaded at a later time                
                SaveFileDialog1.FileName = "";
                SaveFileDialog1.Filter = "Parameter Files (*.txt)|*.txt";
                SaveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                SaveFileDialog1.ShowDialog();

                if (string.IsNullOrEmpty(SaveFileDialog1.FileName))
                {
                    return;
                }

               File.Copy(Common.sfile, SaveFileDialog1.FileName,true);
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string tempS="";
                string sinstr="";

                //dispaly open file dialog to select file
                OpenFileDialog1.FileName = "";
                OpenFileDialog1.Filter = "Parameter Files (*.txt)|*.txt";
                OpenFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;

                OpenFileDialog1.ShowDialog();

                //if filename is not empty then continue with load
                if (string.IsNullOrEmpty(OpenFileDialog1.FileName))
                {
                    return;
                }

                tempS = OpenFileDialog1.FileName;

                sinstr = INI.getINI(tempS, "gameSettings", "gameName");

                //check that this is correct type of file to load
                if (sinstr != "ESI Software")
                {
                    MessageBox.Show("Invalid file","Error",MessageBoxButtons.OK ,MessageBoxIcon.Error);
                    return;
                }

                File.Copy(OpenFileDialog1.FileName, Common.sfile , true);

                //load new parameters into server
                Common.loadParameters();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmSetup1 != null)
                    if (Common.FrmSetup1.Visible)
                        return;

                Common.FrmSetup1 = new frmSetup1();
                Common.FrmSetup1.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (Common.FrmSetup2 != null)
                    if (Common.FrmSetup2.Visible)
                        return;

                Common.FrmSetup2 = new frmSetup2();
                Common.FrmSetup2.Show();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmSetup3 != null)
                    if (Common.FrmSetup3.Visible)
                        return;

                Common.FrmSetup3 = new frmSetup3();
                Common.FrmSetup3.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmSetup4 != null)
                    if (Common.FrmSetup4.Visible)
                        return;

                Common.FrmSetup4 = new frmSetup4();
                Common.FrmSetup4.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSetup5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmSetup5 != null)
                    if (Common.FrmSetup5.Visible)
                        return;

                Common.FrmSetup5 = new frmSetup5();
                Common.FrmSetup5.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdEndEarly_Click(object sender, EventArgs e)
        {
            try
            {
                cmdEndEarly.Enabled = false;

                Common.numberOfPeriods = Common.currentPeriod;

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {
                    Common.playerlist[i].sendEndEarly();
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdExchange_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                Common.closing = true;

                Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            EventLog.AppEventLog_Close();
        }

        

        //tread safe handling of incoming message from a client
        delegate void StringArgReturningVoidDelegate(object sender, ListEventArgs e);

        private void setTakeMessage(object sender, ListEventArgs e)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.txtMain.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(setTakeMessage);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                Common.takeMessage(e.Data);
            }
        }

        //thread safe update to number of clients
        delegate void StringArgReturningVoidDelegate2(string text);

        private void setConnectionsLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.lblConnectionCount.InvokeRequired)
            {
                StringArgReturningVoidDelegate2 d = new StringArgReturningVoidDelegate2(setConnectionsLabel);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblConnectionCount.Text = text;
            }
        }

        //tread safe handling txterror
        delegate void StringDelegateTxtError(string text);

        public void setTxtError(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.txtMain.InvokeRequired)
            {
                StringDelegateTxtError d = new StringDelegateTxtError(setTxtError);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                txtError.Text += text;
            }
        }

        //tread safe handling TabControl1 tab messages
        delegate void StringVoidDelegateTabMessages(string text);

        public void setTabMessages(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.txtMain.InvokeRequired)
            {
                StringVoidDelegateTabMessages d = new StringVoidDelegateTabMessages(setTabMessages);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                TabControl1.TabPages[2].Text = text;
            }
        }

        //upate the number of connected clients
        public void refreshConnectionsLabel()
        {
            try
            {
                setConnectionsLabel(Common.clientCount.ToString());
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //loop taking new network connections from the clients
        private void bwTakeSocketConnections_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool go = true;

                while (go)
                {
                    Socket tempSocket = listener.Accept();

                    Common.clientCount += 1;
                    Common.playerlist[Common.clientCount] = new player();

                    Common.playerlist[Common.clientCount].sp.socketHandler = tempSocket;

                    Common.playerlist[Common.clientCount].sp.startReceive();

                    Common.playerlist[Common.clientCount].sp.messageReceived += new EventHandler<ListEventArgs>(setTakeMessage);

                    Common.playerlist[Common.clientCount].inumber = Common.clientCount;
                    Common.playerlist[Common.clientCount].sp.inumber = Common.clientCount;

                    refreshConnectionsLabel();

                    if (cmdBegin.Enabled == false)
                    {
                        Common.playerlist[Common.clientCount].sendInvalidConnection();
                        Common.clientCount -= 1;
                    }                

                    if (resetPressed)
                        go = false;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //reset the network listeners
        private void bwTakeSocketConnections_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                resetPressed = false;

                for (int i = 1; i <= Common.clientCount; i++)
                {
                    Common.playerlist[i].sp.stopping = true;
                    Common.playerlist[i].sp = null;
                }

                Common.clientCount = 0;

                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(localEndPoint);
                listener.Listen(10);

                bwTakeSocketConnections.RunWorkerAsync();
                refreshConnectionsLabel();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (PrintDialog1.ShowDialog() == DialogResult.OK)
                {
                    PrintDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int i = 0;
                System.Drawing.Font f = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
                int tempN = 0;

                // e.Graphics.DrawString(filename, f, Brushes.Black, 10, 10)

                f = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);

                e.Graphics.DrawString("Name", f, System.Drawing.Brushes.Black, 10, 30);
                e.Graphics.DrawString("Earnings", f, System.Drawing.Brushes.Black, 400, 30);

                f = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

                tempN = 55;

                for (i = 1; i <= dgMain.RowCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        e.Graphics.FillRectangle(System.Drawing.Brushes.Aqua, 0, tempN, 500, 19);
                    }
                    e.Graphics.DrawString(dgMain.Rows[i - 1].Cells[1].Value.ToString(), f, System.Drawing.Brushes.Black, 10, tempN);
                    e.Graphics.DrawString(dgMain.Rows[i - 1].Cells[3].Value.ToString(), f, System.Drawing.Brushes.Black, 400, tempN);

                    tempN += 20;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void frmMain_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            EventLog.AppEventLog_Close();
        }        

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                refreshScreen();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void refreshScreen()
        {
            try
            {

                if (mainScreen == null) return;

                mainScreen.erase1();
                Graphics g = mainScreen.GetGraphics();

                g.SmoothingMode = SmoothingMode.AntiAlias;

                drawAxis(g);
                drawRevenue(g);
                drawCost(g);
                drawSelection(g);
                drawTriangle(g);
                drawKey(g);

                mainScreen.flip();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawKey(Graphics g)
        {
            try
            {

                player pLeft;
                player pRight;
                player p = Common.playerlist[Common.selectedPlayer];

                if (p.partner == -1) return;

                if (p.myType == 1)
                {
                    pLeft = p;
                    pRight = Common.playerlist[p.partner];
                }
                else
                {
                    pLeft =  Common.playerlist[p.partner];
                    pRight = p;
                }               

                Matrix mt = g.Transform;

                //left side key
                g.TranslateTransform(yMargin + 220, otherMargin);

                drawKey2(g, pLeft, new SolidBrush(p1Color), p3Blue);

                g.ResetTransform();

                //right side key
                g.TranslateTransform(pnlMain.Width - 105, otherMargin);

                drawKey2(g,pRight , new SolidBrush(p2Color), p3Red);

                g.ResetTransform();

                //bottom key left
                g.TranslateTransform(yMargin + 105, pnlMain.Height - 28);
                g.DrawString("Period:", f16, Brushes.Black, new PointF(0, 0), fmtR);
                g.DrawString(Common.selectedPeriod.ToString(), f16, Brushes.Black, new PointF(0, 0), fmtL);

                g.Transform = mt;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawKey2(Graphics g, player p, SolidBrush brush, Pen pen)
        {
            try
            {
                g.DrawString("Total Revenue(¢):", f18, brush, new PointF(0, 0), fmtR);
                g.DrawString(string.Format("{0:0.00}", Math.Round(p.revenue[Common.selectedPeriod], 2)), f18, brush, new PointF(0, 0), fmtL);

                g.DrawString("- Total Cost(¢):", f18, brush, new PointF(0, 25), fmtR);
                g.DrawString(string.Format("{0:0.00}", Math.Round(p.cost[Common.selectedPeriod], 2)), f18, brush, new PointF(0, 25), fmtL);

                //equals line 
                g.DrawString("P" + p.inumber + " Profit(¢):", f18, brush, new PointF(0, 60), fmtR);

                g.DrawLine(pen, new PointF(-185, 57), new PointF(75, 57));
                g.DrawString(string.Format("{0:0.00}", Math.Round(p.profit[Common.selectedPeriod], 2)), f18, brush, new PointF(0, 60), fmtL);
            
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawTriangle(Graphics g)
        {
            try
            {
                float xAxisWidth = (pnlMain.Width - xMargin - otherMargin);

                g.Transform = graphTransform;

                Period p = Common.periods[Common.selectedPeriod];

                player plr = Common.playerlist[Common.selectedPlayer];

                //full value line
                g.DrawLine(p5Black,
                           p.treatment.pt1,
                           p.treatment.pt3);

                g.DrawLine(p5Black,
                          p.treatment.pt2,
                          p.treatment.pt3);

                //half value line
                if (plr.myType == 1)
                {
                    g.DrawLine(p5BlackDash,
                               p.treatment.halfPt1Blue,
                               p.treatment.halfPt3Blue);

                    g.DrawLine(p5BlackDash,
                              p.treatment.halfPt2Blue,
                              p.treatment.halfPt3Blue);
                }
                else
                {
                    g.DrawLine(p5BlackDash,
                               p.treatment.halfPt1Red,
                               p.treatment.halfPt3Red);

                    g.DrawLine(p5BlackDash,
                              p.treatment.halfPt2Red,
                              p.treatment.halfPt3Red);
                }

                //closing lines
                g.DrawLine(p5Black,
                           p.treatment.pt1,
                           new PointF(p.treatment.pt1.X, 0));

                g.DrawLine(p5Black,
                          p.treatment.pt2,
                          new PointF(p.treatment.pt2.X, 0));

                g.ResetTransform();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawSelection(Graphics g)
        {
            try
            {
                player p = Common.playerlist[Common.selectedPlayer];

                if (p.partner == -1) return;

                g.Transform = graphTransform;

                if (p.myType == 1)
                {
                    //if (Common.numberOfPlayers > 1)
                    Common.playerlist[p.partner].draw(g);
                    p.draw(g);
                }
                else
                {
                    p.draw(g);
                    Common.playerlist[p.partner].draw(g);
                }

                g.ResetTransform();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawCost(Graphics g)
        {
            try
            {               
                player p = Common.playerlist[Common.selectedPlayer];

                if (p.cost[Common.selectedPeriod] == 0 || p.revenue[Common.selectedPeriod] == 0) return;

                Matrix mt = g.Transform;

                g.Transform = graphTransform;

                p.drawCost(g);

                g.Transform = mt;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawRevenue(Graphics g)
        {
            try
            {               
                player p = Common.playerlist[Common.selectedPlayer];

                if (p == null) return;

                if (p.cost[Common.selectedPeriod] == 0 || p.revenue[Common.selectedPeriod] == 0) return;

                Matrix mt = g.Transform;

                g.Transform = graphTransform;

                p.drawRevenue(g);

                g.Transform = mt;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawAxis(Graphics g)
        {
            try
            {
                if (Common.selectedPeriod == 0) return;
                g.TranslateTransform(yMargin, pnlMain.Height - xMargin);
                //axis lines
                g.DrawLine(p3Black, 0, 0, 0, -graphHeight);
                g.DrawLine(p3Black, 0, 0, graphWidth, 0);

                //axis labels
                g.DrawString("Locations", f12, Brushes.DimGray, new PointF(graphWidth / 2, xMargin - 30), fmt);

                Matrix gt = g.Transform;
                g.TranslateTransform(-yMargin + 10, -graphHeight / 2);
                g.RotateTransform(-90);
                g.DrawString($"Resource Value (1 point = {Common.earningsMultiplier}¢)", f12, Brushes.DimGray, new PointF(0, 0), fmt);
                g.Transform = gt;

                Treatment t = Common.periods[Common.selectedPeriod].treatment;

                //x ticks
                float tempX = 0;
                float tempY = 0;
                float tempXIncrement = graphWidth / (t.scaleRange * 2);
                float tempXValue = -t.scaleRange;

                for (int i = 1; i <= t.scaleRange * 2 + 1; i++)
                {
                    g.DrawLine(p3Black, new PointF(tempX, tempY), new PointF(tempX, tempY + xTickHeight));
                    g.DrawString(tempXValue.ToString(), f10, Brushes.Black, new PointF(tempX, tempY + 22), fmt);
                    tempX += tempXIncrement;
                    tempXValue++;
                }

                //y ticks
                tempX = 0;
                float tempYTickIncrement = graphHeight / 10;
                double tempYTickValue = 0;
                double tempYValueIncrement = t.scaleHeight / 10;

                for (int i = 1; i <= 11; i++)
                {
                    g.DrawLine(p3Black, new PointF(tempX, tempY), new PointF(tempX - 7, tempY));
                    g.DrawString(string.Format("{0:0.00}", Math.Round(tempYTickValue, 2)), f10, Brushes.Black, new PointF(tempX - 10, tempY - 8), fmtR);

                    tempY -= tempYTickIncrement;
                    tempYTickValue += tempYValueIncrement;
                }


                g.ResetTransform();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void TabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                TabControl1.Refresh();
            }
             catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        // thread safe return handle to panel2
        public delegate void getPanel2Callback(ref IntPtr handle);

        public void getPanel2(ref IntPtr handle)
        {
            if (this.pnlMain.InvokeRequired)
            {
                getPanel2Callback d = new getPanel2Callback(getPanel2);
                this.Invoke(d, new object[] { handle });
            }
            else
            {
                handle = pnlMain.Handle;
            }

        }

        private void cmdReplay_Click(object sender, EventArgs e)
        {
            try
            {
                if (Common.FrmReplay != null)
                    if (Common.FrmReplay.Visible)
                        return;

                Common.FrmReplay = new frmReplay();
                Common.FrmReplay.Show();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public float convertToY(float tempValue,float graphMax)
        {
            try
            {
                if (Common.currentPeriod == 0) return 0;
                //Treatment t = Common.periods[Common.currentPeriod].treatment;

                float graphMin = 0;
                //float graphMax = t.scaleHeight;

                float tempHeight = graphHeight;

                float unit = tempHeight / (graphMax - graphMin);

                return -(unit * tempValue);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }

        }

        public float convertToX(float tempValue,float range)
        {
            try
            {
                if (Common.currentPeriod == 0) return 0;
                //Treatment t = Common.periods[Common.currentPeriod].treatment;

                float tempWidth = graphWidth / 2;
                //float range = t.scaleRange;

                float tempT = tempWidth / range;

                return tempT * tempValue;
                // adjust for width of marker
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }

        }
        public PointF getLinePoint(float value)
        {
            try
            {

                Period p = Common.periods[Common.selectedPeriod];
                Treatment t = p.treatment;

                if (p == null) return new PointF(0, 0);

                if (value == p.treatment.middleX)
                {
                    //point in the middle
                    return p.treatment.pt3;
                }
                else if (value < p.treatment.middleX)
                {
                    //use left side line

                    return getPointIntersection(new PointF(convertToX(value,t.scaleRange),convertToY(0,t.scaleHeight)),
                                                new PointF(convertToX(value, t.scaleRange), convertToY(t.scaleHeight, t.scaleHeight)),
                                                p.treatment.pt1,
                                                p.treatment.pt3);
                }
                else
                {
                    //use right side line
                    return getPointIntersection(new PointF(convertToX(value, t.scaleRange), convertToY(0, t.scaleHeight)),
                                                new PointF(convertToX(value, t.scaleRange), convertToY(t.scaleHeight, t.scaleHeight)),
                                                p.treatment.pt2,
                                                p.treatment.pt3);
                }


            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return new Point(0, 0);
            }
        }

        public PointF getPointIntersection(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        {
            try
            {
                PointF pt = new PointF();

                pt.X = ((pt1.X * pt2.Y - pt1.Y * pt2.X) * (pt3.X - pt4.X) - (pt1.X - pt2.X) * (pt3.X * pt4.Y - pt3.Y * pt4.X)) /
                       ((pt1.X - pt2.X) * (pt3.Y - pt4.Y) - (pt1.Y - pt2.Y) * (pt3.X - pt4.X));

                pt.Y = ((pt1.X * pt2.Y - pt1.Y * pt2.X) * (pt3.Y - pt4.Y) - (pt1.Y - pt2.Y) * (pt3.X * pt4.Y - pt3.Y * pt4.X)) /
                       ((pt1.X - pt2.X) * (pt3.Y - pt4.Y) - (pt1.Y - pt2.Y) * (pt3.X - pt4.X));

                return pt;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return new Point(0, 0);
            }
        }

        public void setupPen(ref Pen p, Color c, int s)
        {
            try
            {
                p = new Pen(new SolidBrush(c), s);
                p.Alignment = PenAlignment.Center;
                p.EndCap = LineCap.Triangle;
                p.StartCap = LineCap.Triangle;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {

        }

        private void cboPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                Common.selectedPlayer = cboPlayer.SelectedIndex + 1;
                refreshScreen();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
    
}
