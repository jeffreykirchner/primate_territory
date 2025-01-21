using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmInstructions : Form
    {
       
        //public int numberOfPages = 6;

        public bool startPressed = false;
        public bool[] pagesDone = new bool[101];
        public int currentInstruction = 1;


        public frmInstructions()
        {
            InitializeComponent();
        }

        private void frmInstructions_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i <= 100; i++)
                {
                    pagesDone[i] =false;
                }


                pagesDone[1] = true;
                startPressed = false;
                currentInstruction = 1;

                nextInstruction();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSubmitQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQuiz.Text.Trim())) return;

                checkQuiz();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            try
            {
                cmdNext.Visible = true;

                //previous page of instructions
                if (currentInstruction == 1)
                    return;

                currentInstruction -= 1;

                if (currentInstruction == 1) cmdBack.Visible = false;

                nextInstruction();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            try
            {
                cmdStart.Visible = false;
                startPressed = true;

                string outstr = "";

                Common.FrmClient.SC.sendMessage("FINSHED_INSTRUCTIONS", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            try
            {

                if (!pagesDone[currentInstruction])
                {
                    MessageBox.Show("Please take the requested action before continuing.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                cmdBack.Visible = true;

                if (currentInstruction == Common.instructionPages)
                    return;

                currentInstruction += 1;

                if (currentInstruction == Common.instructionPages & !startPressed)
                {
                    cmdStart.Visible = true;
                }

                if (currentInstruction == Common.instructionPages)
                {
                    cmdNext.Visible = false;
                }

                nextInstruction();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        void nextInstruction()
        {
            try
            {
                string folder = Common.instructionsFolder;

                RichTextBox1.LoadFile(Application.StartupPath + "\\instructions\\" + folder + "\\page" + currentInstruction + ".rtf");

                variables();

                RichTextBox1.SelectionStart = 1;
                RichTextBox1.ScrollToCaret();

                if (!startPressed) Common.FrmClient.SC.sendMessage("INSTRUCTION_PAGE", currentInstruction + ";");

                Text = "Instructions, Page " + currentInstruction + "/" + Common.instructionPages;

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        void variables()
        {
            try
            {
                Player partner;
                Player p = Common.playerlist[Common.myType];

                float blueRevenuePercent = Common.periods[1].treatment.blueRevenuePercent;
                float redRevenuePercent = Common.periods[1].treatment.redRevenuePercent;

                if (Common.myType == 1)
                {
                    RepRTBfield2("color", "blue");
                    RepRTBfield2("Color", "Blue");
                    RepRTBfield2("otherColor", "red");
                    RepRTBfield2("leftOrRight", "left");

                    RepRTBfield2("yourRevenuePercent", string.Format("{0:0.00}", Math.Round(blueRevenuePercent * 100, 2)));
                    RepRTBfield2("otherRevenuePercent", string.Format("{0:0.00}", Math.Round(redRevenuePercent * 100, 2)));

                    partner = Common.playerlist[2];
                }
                else
                {
                    RepRTBfield2("color", "red");
                    RepRTBfield2("Color", "Red");
                    RepRTBfield2("otherColor", "blue");
                    RepRTBfield2("leftOrRight", "right");

                    RepRTBfield2("yourRevenuePercent", string.Format("{0:0.00}", Math.Round(redRevenuePercent * 100, 2)));
                    RepRTBfield2("otherRevenuePercent", string.Format("{0:0.00}", Math.Round(blueRevenuePercent * 100, 2)));

                    partner = Common.playerlist[1];
                }

                RepRTBfield2Color(" blue", Common.Frm1.p1Color);
                RepRTBfield2Color(" Blue", Common.Frm1.p1Color);
                RepRTBfield2Color(" red", Common.Frm1.p2Color);
                RepRTBfield2Color(" Red", Common.Frm1.p2Color);
                RepRTBfield2("earningsMultiplier", Common.earningsMultiplier.ToString());

                RepRTBfield2("leftExample", string.Format("{0:0.00}", Math.Round(Common.instructionPlayerLeft, 2)));
                RepRTBfield2("rightExample", string.Format("{0:0.00}", Math.Round(Common.instructionPlayerRight, 2)));

                double s = (double)Common.periodLength / 1000d;

                RepRTBfield2("periodLength",s.ToString());

                if(currentInstruction == Common.instructionPageSliders)
                {
                    if (!pagesDone[currentInstruction])
                    {
                        Common.phase = "run";

                        partner.selectionLeft = Common.instructionPartnerLeft1;
                        partner.selectionRight = Common.instructionPartnerRight1;

                        //send calculation request

                        string str = "";

                        str += p.selectionLeft + ";";
                        str += p.selectionRight + ";";
                        str += partner.selectionLeft + ";";
                        str += partner.selectionRight + ";";

                        Common.FrmClient.SC.sendMessage("03", str);
                    }
                }
                else if (currentInstruction == Common.instructionPageTable)
                {

                }
                else if(currentInstruction == Common.instructionPageExample2)
                {
                    if (!pagesDone[currentInstruction])
                    {
                        pagesDone[currentInstruction] = true;

                        partner.selectionLeft = Common.instructionPartnerLeft2;
                        partner.selectionRight = Common.instructionPartnerRight2;

                        string str = "";

                        str += p.selectionLeft + ";";
                        str += p.selectionRight + ";";
                        str += partner.selectionLeft + ";";
                        str += partner.selectionRight + ";";

                        Common.FrmClient.SC.sendMessage("03", str);
                    }
                }
                else if (currentInstruction == Common.instructionPageChat)
                {

                }
                else
                {
                    if (!pagesDone[currentInstruction])
                    {
                        pagesDone[currentInstruction] = true;
                    }
                }

                //    switch (currentInstruction)
                //{
                //    case Common.instructionPageSliders:
                //        if (!pagesDone[currentInstruction])
                //        {
                           
                //        }
                //        break;
                //    case 2:
                //        if (!pagesDone[currentInstruction])
                //        {
                            
                //        }
                //        break;
                //    case 3:
                //        if (!pagesDone[currentInstruction])
                //        {
                //            Common.phase = "run";                           

                //            partner.selectionLeft = Common.instructionPartnerLeft1;
                //            partner.selectionRight = Common.instructionPartnerRight1;

                //            //send calculation request

                //            string str = "";

                //            str += p.selectionLeft + ";";
                //            str += p.selectionRight + ";";
                //            str += partner.selectionLeft + ";";
                //            str += partner.selectionRight + ";";

                //            Common.FrmClient.SC.sendMessage("03", str);
                //        }
                //        break;
                //    case 4:
                //        if (!pagesDone[currentInstruction])
                //        {
                //            pagesDone[currentInstruction] = true;
                //        }
                //        break;
                //    case 5:
                //        if (!pagesDone[currentInstruction])
                //        {
                //            pagesDone[currentInstruction] = true;

                //            partner.selectionLeft = Common.instructionPartnerLeft2;
                //            partner.selectionRight = Common.instructionPartnerRight2;

                //            string str = "";

                //            str += p.selectionLeft + ";";
                //            str += p.selectionRight + ";";
                //            str += partner.selectionLeft + ";";
                //            str += partner.selectionRight + ";";

                //            Common.FrmClient.SC.sendMessage("03", str);
                //        }
                //        break;
                //    case 6:
                //        if (!pagesDone[currentInstruction])
                //        {
                //            pagesDone[currentInstruction] = true;
                //        }
                //        break;
                //    case 7:
                //        if (!pagesDone[currentInstruction])
                //        {
                //            pagesDone[currentInstruction] = true;
                //        }
                //        break;
                //}
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        bool RepRTBfield(string sField, string sValue)
        {
            try
            {
                //when the instructions are loaded into the rich text box control this function will
                //replace the variable place holders with variables.

                if (RichTextBox1.Find("#" + sField + "#") == -1)
                {
                    RichTextBox1.DeselectAll();
                    return false;
                }

                RichTextBox1.SelectedText = sValue;

                return true;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return false;
            }
        }

        void RepRTBfield2(string sField, string sValue)
        {
            try
            {

                while ((RepRTBfield(sField, sValue)))
                {
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        int RepRTBfieldColor(string sField, Color c, int start)
        {
            try
            {
                //when the instructions are loaded into the rich text box control this function will
                //color the specified text the specified color

                if (RichTextBox1.Find(sField, start, RichTextBoxFinds.None) == -1)
                {
                    RichTextBox1.DeselectAll();
                    return 0;
                }

                RichTextBox1.SelectionColor = c;

                return RichTextBox1.SelectionStart;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        void RepRTBfield2Color(string sField, Color c)
        {

            try
            {
                int start = (RepRTBfieldColor(sField, c, 1));

                bool go = false;

                if (start == 0)
                {
                    go = false;
                }
                else
                {
                    go = true;
                    start += 1;
                }

                while (go)
                {
                    start = (RepRTBfieldColor(sField, c, start));

                    if (start == 0)
                    {
                        go = false;
                    }
                    else
                    {
                        start += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //quiz functions
        public void checkQuiz()
        {
            try
            {
                pagesDone[currentInstruction] = true;
                nextInstruction();
                gbQuiz.Visible = false;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void txtQuiz_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AcceptButton = cmdSubmitQuiz;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
