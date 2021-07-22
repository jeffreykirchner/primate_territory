using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmSetup1 : Form
    {
        public frmSetup1()
        {
            InitializeComponent();
        }

        private void frmSetup1_Load(object sender, EventArgs e)
        {
            try
            {
                txtNumberOfPlayers.Text = INI.getINI(Common.sfile, "gameSettings", "numberOfPlayers");
                txtNumberOfPeriods.Text = INI.getINI(Common.sfile, "gameSettings", "numberOfPeriods");
                txtPortNumber.Text = INI.getINI(Common.sfile, "gameSettings", "port");
                txtInstructionX.Text = INI.getINI(Common.sfile, "gameSettings", "instructionX");
                txtInstructionY.Text = INI.getINI(Common.sfile, "gameSettings", "instructionY");
                txtWindowX.Text = INI.getINI(Common.sfile, "gameSettings", "windowX");
                txtWindowY.Text = INI.getINI(Common.sfile, "gameSettings", "windowY");

                txtPeriodLength.Text = INI.getINI(Common.sfile, "gameSettings", "periodLength");
                txtLocationIncrement.Text = INI.getINI(Common.sfile, "gameSettings", "locationIncrement");

                cbShowPartnerInfo.Checked = bool.Parse(INI.getINI(Common.sfile, "gameSettings", "showPartnerInfo"));
                cbShowInstructions.Checked =bool.Parse(INI.getINI(Common.sfile, "gameSettings", "showInstructions"));
                cbTestMode.Checked = bool.Parse(INI.getINI(Common.sfile, "gameSettings", "testMode"));

                txtInstructionPlayerLeft.Text = INI.getINI(Common.sfile, "gameSettings", "instructionPlayerLeft");
                txtInstructionPlayerRight.Text = INI.getINI(Common.sfile, "gameSettings", "instructionPlayerRight");
                txtInstructionPartnerLeft1.Text = INI.getINI(Common.sfile, "gameSettings", "instructionPartnerLeft1");
                txtInstructionPartnerRight1.Text = INI.getINI(Common.sfile, "gameSettings", "instructionPartnerRight1");
                txtInstructionPartnerLeft2.Text = INI.getINI(Common.sfile, "gameSettings", "instructionPartnerLeft2");
                txtInstructionPartnerRight2.Text = INI.getINI(Common.sfile, "gameSettings", "instructionPartnerRight2");
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {

                INI.writeINI(Common.sfile, "gameSettings", "numberOfPlayers", txtNumberOfPlayers.Text);
                INI.writeINI(Common.sfile, "gameSettings", "numberOfPeriods", txtNumberOfPeriods.Text);
                INI.writeINI(Common.sfile, "gameSettings", "port", txtPortNumber.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionX", txtInstructionX.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionY", txtInstructionY.Text);
                INI.writeINI(Common.sfile, "gameSettings", "windowX", txtWindowX.Text);
                INI.writeINI(Common.sfile, "gameSettings", "windowY", txtWindowY.Text);

                INI.writeINI(Common.sfile, "gameSettings", "periodLength", txtPeriodLength.Text);
                INI.writeINI(Common.sfile, "gameSettings", "locationIncrement", txtLocationIncrement.Text);

                INI.writeINI(Common.sfile, "gameSettings", "showPartnerInfo", cbShowPartnerInfo.Checked.ToString());
                INI.writeINI(Common.sfile, "gameSettings", "showInstructions", cbShowInstructions.Checked.ToString());
                INI.writeINI(Common.sfile, "gameSettings", "testMode", cbTestMode.Checked.ToString());

                INI.writeINI(Common.sfile, "gameSettings", "instructionPlayerLeft", txtInstructionPlayerLeft.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionPlayerRight", txtInstructionPlayerRight.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionPartnerLeft1", txtInstructionPartnerLeft1.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionPartnerRight1", txtInstructionPartnerRight1.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionPartnerLeft2", txtInstructionPartnerLeft2.Text);
                INI.writeINI(Common.sfile, "gameSettings", "instructionPartnerRight2", txtInstructionPartnerRight2.Text);

                Common.loadParameters();
                this.Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
