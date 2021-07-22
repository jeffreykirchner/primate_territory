using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace Server
{
    public partial class frmReplay : Form
    {
        //string[] replayEventsDf;
        //string[] replayReplayDf;
        //string[] replaySummaryDf;


        public frmReplay()
        {
            InitializeComponent();
        }

        private void cmdLoadData_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog1.FileName = "";
                OpenFileDialog1.Filter = "Data Files (*.csv)|*.csv| Data Files (*.json)|*.json";
                OpenFileDialog1.InitialDirectory = Application.StartupPath + "\\DataFiles";

                OpenFileDialog1.ShowDialog();

                if (OpenFileDialog1.FileName == "") return;

                string[] d = new string[5];

                d[1] = "Replay_Data_";
                d[2] = "Summary_Data_";
                d[0] = "Parameters_";
                d[3] = ".csv";
                d[4] = ".json";

                string[] d2 = new string[1];
                d2[0] = "\r\n";

                string[] msgtokens2 = OpenFileDialog1.FileName.Split(d, StringSplitOptions.RemoveEmptyEntries);

                string tempFileName = "";

                //tempFileName = msgtokens2[0] + "Replay_Data_" + msgtokens2[1];

                //tempFileName = msgtokens2[0] + "Summary_Data_" + msgtokens2[1];
                //replaySummaryDf = File.ReadAllText(tempFileName).Split(d2, StringSplitOptions.RemoveEmptyEntries);

                tempFileName = msgtokens2[0] + "Parameters_" + msgtokens2[1] + ".csv";
                File.Copy(tempFileName, Common.sfile, true);

                tempFileName = msgtokens2[0] + "Replay_Data_" + msgtokens2[1] + ".json";
                JObject jo = JObject.Parse(File.ReadAllText(tempFileName));

                Common.loadParameters();

                Common.showInstructions = false;

                for(int i=1;i<=Common.numberOfPlayers;i++)
                {
                    Common.playerlist[i] = new player();
                    Common.playerlist[i].inumber = i;

                    Common.playerlist[i].fromJSON(jo.Property(i.ToString()));
                    Common.playerlist[i].setupGraphics();
                }

                Common.FrmServer.setup();                

                tbData.Maximum = Common.numberOfPeriods;
                tbData.Minimum = 1;
                tbData.Value = 1;

                Common.FrmServer.Timer1.Enabled = true;
                timer1.Enabled = true;
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdPauseData_Click(object sender, EventArgs e)
        {
            try
            {
                cmdPlayData.Visible = true;
                cmdPauseData.Visible = false;

                timer1.Enabled = false;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdPlayData_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;

                cmdPlayData.Visible = false;
                cmdPauseData.Visible = true;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void tbData_Scroll(object sender, EventArgs e)
        {
            try
            {
                cmdPlayData.Visible = true;
                cmdPauseData.Visible = false;

                Common.selectedPeriod = tbData.Value;
                Common.FrmServer.refreshScreen();

                timer1.Enabled = false;

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void frmReplay_Load(object sender, EventArgs e)
        {
            try
            {
                cmdPlayData.Location = cmdPauseData.Location;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void frmReplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if(tbData.Value == tbData.Maximum)
                {
                    timer1.Enabled = false;
                    cmdPlayData.Visible = true;
                    cmdPauseData.Visible = false;
                }
                else
                {
                    tbData.Value++;
                    Common.selectedPeriod = tbData.Value;
                    Common.FrmServer.refreshScreen();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
