using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public static class Common
    {
        //globals
        public static string sfile;
        public static string myIPAddress = "localhost";
        public static int myPortNumber = 12345;
        public static int inumber;
        public static bool clientClosing = false;

        //forms
        public static frmConnect FrmConnect;
        public static frm1 Frm1;
        public static frmMain FrmClient;
        public static frmNames FrmNames;
        public static frmInstructions FrmInstructions;
        public static frmTestMode FrmTestMode;

        //keeps track of message count
        public static int messageCounter = 0;
        public static int[] playerMessageCounter = new int[101];

        //global parameters
        public static int numberOfPlayers;                     //number of players needed
        public static int numberOfPeriods;                     //number of periods in the experiment         
        public static int instructionX;                        //x location of intstruction window
        public static int instructionY;                        //y location of intstruction window
        public static int windowX;                             //x location of main window
        public static int windowY;                             //y location of main window
        public static bool showInstructions;                   //show instructions before experiment starts
        public static bool testMode;                           //turn on auto test system
                     
        public static int periodLength;                        //length of period in seconds        
        public static int timeRemaining;                       //seconds remaining in period  
        public static int currentPeriod;                       //current period
        public static float locationIncrement = 0;             //smallest change in location 
        public static bool showPartnerInfo = true;             //show partner info subject on their screen

        public static int myType;                              //1 or 2 (Red , Blue) 

        public static Treatment[] treaments = new Treatment[100];    //list of each available treatment
        public static int treatmentCount = 0;                        //number of " 

        public static Period[] periods = new Period[1000];           //list of periods

        public static Player[] playerlist = new Player[100];

        public static double earnings=0;                              //total earnings in cents
         
        public static string phase = "begin";                         //current phase of the game 
        
        public static string[] commandLineArgs;

        //instruction example parameters
        public static float instructionPlayerLeft;
        public static float instructionPlayerRight;
        public static float instructionPartnerLeft1;
        public static float instructionPartnerRight1;
        public static float instructionPartnerLeft2;
        public static float instructionPartnerRight2;

        public static void start()
        {

            sfile = Application.StartupPath + @"\client.ini";

            commandLineArgs = Environment.GetCommandLineArgs();

            if (commandLineArgs.Length>1)
            {
                INI.writeINI(sfile, "Settings", "ip", commandLineArgs[1]);
            }                       

            myIPAddress = INI.getINI(sfile, "Settings", "ip").ToString();
            myPortNumber = int.Parse(INI.getINI(sfile, "Settings", "port"));
            
            EventLog.AppEventLog_Init();
        }

        public static void takeMessage(List<string> sinstr)
        {

            try
            {
                string str = sinstr[0];

                string[] tempa = { "<SEP>" };
                string[] msgtokens = str.Split(tempa, StringSplitOptions.None);

                string id = msgtokens[0];
                string message = msgtokens[1];

                switch (id)
                {
                    case "SHOW_NAME":
                        takeShowName(message);
                        break;
                    case "RESET":
                        takeReset(message);
                        break;
                    case "END_EARLY":
                        takeEndEarly(message);
                        break;
                    case "FINISHED_INSTRUCTIONS":
                        takeFinishedInstructions(message);
                        break;
                    case "INVALID_CONNECTION":
                        takeInvalidConnection(message);
                        break;
                    case "BEGIN":                        
                        takeBegin(message);
                        break;
                    case "REQUEST_COMPUTER_NAME":
                        takeRequestComputerName(message);
                        break;
                    case "START_NEXT_PERIOD":
                        takeStartNextPeriod(message);
                        break;
                    case "01":
                        takeCalculationRequest(message);
                        break;
                    case "02":
                        
                        break;
                    case "03":
                        
                        break;
                    case "04":
                        
                        break;
                    case "05":
                        
                        break;
                    case "06":
                        
                        break;
                    case "07":
                        
                        break;
                    case "08":
                        
                        break;
                    case "09":
                        
                        break;
                    case "10":

                        break;
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeBegin(string str)
        {
            try
            {
                //initialize forms
                Frm1 = new frm1();
                Frm1.Show();

                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                Common.currentPeriod = 1;

                //take parameters
                numberOfPlayers = int.Parse(msgtokens[nextToken++]);
                numberOfPeriods = int.Parse(msgtokens[nextToken++]);
                instructionX = int.Parse(msgtokens[nextToken++]);
                instructionY = int.Parse(msgtokens[nextToken++]);
                windowX = int.Parse(msgtokens[nextToken++]);
                windowY = int.Parse(msgtokens[nextToken++]);
                showInstructions = bool.Parse(msgtokens[nextToken++]);
                inumber = int.Parse(msgtokens[nextToken++]);
                testMode = bool.Parse(msgtokens[nextToken++]);      
                
                periodLength = int.Parse(msgtokens[nextToken++]);

                myType = int.Parse(msgtokens[nextToken++]);
                locationIncrement = float.Parse(msgtokens[nextToken++]);
                showPartnerInfo = bool.Parse(msgtokens[nextToken++]);

                instructionPlayerLeft = float.Parse(msgtokens[nextToken++]);
                instructionPlayerRight = float.Parse(msgtokens[nextToken++]);
                instructionPartnerLeft1 = float.Parse(msgtokens[nextToken++]);
                instructionPartnerRight1 = float.Parse(msgtokens[nextToken++]);
                instructionPartnerLeft2 = float.Parse(msgtokens[nextToken++]);
                instructionPartnerRight2 = float.Parse(msgtokens[nextToken++]);

                Common.treatmentCount = int.Parse(msgtokens[nextToken++]);
                for (int i = 1; i <= Common.treatmentCount; i++)
                {
                    Common.treaments[i] = new Treatment();
                    Common.treaments[i].fromString(ref msgtokens,ref nextToken, myType);
                }

                for (int i = 1; i <= Common.numberOfPeriods; i++)
                {
                    Common.periods[i] = new Period();
                    Common.periods[i].fromString(ref msgtokens, ref nextToken);
                }

                //initialize players
                for (int i = 1; i <= 2; i++)
                {
                    playerlist[i] = new Player();
                    playerlist[i].FromString(ref msgtokens, ref nextToken, i);
                }

                Common.Frm1.setupHeighlightBoundingBoxes();

                Common.FrmClient.Hide();
                Frm1.Text = "Client " + inumber;

                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    playerMessageCounter[i] = 1;
                }

                Common.Frm1.timer3.Interval = periodLength;

                FrmClient.SC.sendMessage("COMPUTER_NAME", Environment.MachineName + ";");

                if (showInstructions)
                {
                    FrmInstructions = new frmInstructions();
                    FrmInstructions.Show();
                    FrmInstructions.Location = new System.Drawing.Point(instructionX, instructionY);

                    Frm1.Location = new System.Drawing.Point(windowX, windowY);                  
                }

                if(testMode)
                {
                    Frm1.timer4.Interval = Rand.rand(1500, 500);
                    Frm1.timer4.Enabled = true;

                    FrmTestMode = new frmTestMode();
                    FrmTestMode.Show();
                }                               

                Frm1.timer1.Enabled = true;

                takeBegin2();

                EventLog.appEventLog_Write("Client: " + inumber);              

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeBegin2()
        {
            try
            {
                phase = "begin";

                Frm1.txtMessages.Text = "Set your initial locations then press the Start button.";

                if (myType == 1)
                {
                    playerlist[1].selectionLeft = 0;
                    playerlist[1].selectionRight = 0;

                    playerlist[2].selectionLeft = Common.periods[Common.currentPeriod].treatment.leftX;
                    playerlist[2].selectionRight = Common.periods[Common.currentPeriod].treatment.leftX;
                }
                else
                {
                    playerlist[1].selectionLeft = Common.periods[Common.currentPeriod].treatment.leftX;
                    playerlist[1].selectionRight = Common.periods[Common.currentPeriod].treatment.leftX;

                    playerlist[2].selectionLeft = 0;
                    playerlist[2].selectionRight = 0;
                }

                Common.Frm1.cmdSubmit.Visible = true;

                calcSelectionProfit();
            }
             catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeShowName(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                FrmNames = new frmNames();
                FrmNames.Show();

                FrmNames.lblEarnings.Text = "Your Earnings Are: " +  msgtokens[nextToken++];

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeCalculationRequest(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                playerlist[myType].currentRevenue = float.Parse(msgtokens[nextToken++]);
                playerlist[myType].currentCost = float.Parse(msgtokens[nextToken++]);
                playerlist[myType].currentProfit = float.Parse(msgtokens[nextToken++]);

                Common.Frm1.refreshScreen();

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeEndEarly(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                numberOfPeriods = int.Parse(msgtokens[nextToken++]);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeFinishedInstructions(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                //int nextToken = 0;
                
               
                FrmInstructions.Close();
                showInstructions = false;

                takeBegin2();

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeRequestComputerName(string str)
        {
            try
            {
                str = "a;";
                // Thread.Sleep(200);
                //Program.frmClient.SC.sendMessage("COMPUTER_NAME","a");
                FrmClient.SC.sendMessage("COMPUTER_NAME", Environment.MachineName + ";");
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeReset(string str)
        {
            try
            {
                //string[] msgtokens = str.Split(';');
                //int nextToken = 0;

                closeClient();
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }


        public static void closeClient()
        {
            try
            {

               // FrmClient.Timer1.Enabled = false;
                clientClosing = true;
                if(Frm1 != null) Frm1.timer1.Enabled = false;
                

                FrmClient.bwSocket.CancelAsync();
                FrmClient.SC.close();

                //.Close()
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                FrmClient.Close();
            }
        }

        public static void takeInvalidConnection(string str)
        {
            try
            {
               List<string> list = new List<string>();

               FrmClient.SC_ConnectionError(null, new ListEventArgs(list));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void takeClientMessage(string message)
        {
            try
            {
                string[] msgtokens = message.Split(';');
                int nextToken = 0;

                string tempId = msgtokens[nextToken++];
                string tempMessage = msgtokens[nextToken++];                

                playerMessageCounter[int.Parse(tempId)] = int.Parse(tempMessage)+1;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static int returnDistance(int x1,int y1,int x2, int y2)
        {
            return Convert.ToInt32(Math.Round(Math.Sqrt(Math.Pow((x2 - x1),2) + Math.Pow((y2 - y1),2))));
        }

        public static void takeUpdateTime(string message)
        {
            try
            {
                string[] msgtokens = message.Split(';');
                int nextToken = 0;

                timeRemaining = int.Parse(msgtokens[nextToken++]);     
                
                earnings = double.Parse(msgtokens[nextToken++]);
                             
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }        


        public static string timeConversion(int sec)
        {
            try
            {
                // appEventLog_Write("time conversion :" & sec)
                return((sec / 60).ToString("D2")+ ":" + (sec % 60).ToString("D2"));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error timeConversion:", ex);
                return  "";
            }

        }

        public static void takeStartNextPeriod(string message)
        {
            try
            {
                string[] msgtokens = message.Split(';');
                int nextToken = 0;

                Player partner;
                if(myType == 1)
                {
                    partner = playerlist[2];
                }
                else
                {
                    partner = playerlist[1];
                }

                currentPeriod = int.Parse(msgtokens[nextToken++]);
                earnings = double.Parse(msgtokens[nextToken++]);

                playerlist[myType].takePeriodResults(ref msgtokens,ref nextToken);

                partner.takePeriodResults(ref msgtokens, ref nextToken);
                partner.takePeriodSelection(ref msgtokens, ref nextToken);

                
                if(currentPeriod>1 && periods[currentPeriod].treatment != periods[currentPeriod-1].treatment)
                {
                    //treatment has changed
                    phase = "begin";
                    Frm1.txtMessages.Text = "The graph has changed, set your Locations then press the Start button.";
                    Common.Frm1.timer3.Enabled = false;

                    playerlist[myType].resetLocationSelection();
                    partner.selectionLeft = periods[Common.currentPeriod].treatment.leftX;
                    partner.selectionRight = periods[Common.currentPeriod].treatment.rightX;

                    Common.Frm1.cmdSubmit.Visible = true;
                }
                else
                {
                    phase = "run";
                    Frm1.txtMessages.Text = "Update your locations at any time, they will be submitted automatically.";
                    Common.Frm1.timer3.Enabled = true;
                }
                

                Common.Frm1.refreshScreen();
                Common.Frm1.pnlMain.Enabled = true;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //public static void calcRevenue()
        //{
        //    try
        //    {
        //        if (Common.myType != 1 && Common.myType != 2) return;
        //        Player p = Common.playerlist[Common.myType];

        //        p.calcRevenue();
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}

        //public static void calcCost()
        //{
        //    try
        //    {
        //        if (Common.myType != 1 && Common.myType != 2) return;
        //        Player p = Common.playerlist[Common.myType];

        //        p.calcCost();
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}

        public static void calcSelectionProfit()
        {
            try
            {
                if (Common.myType != 1 && Common.myType != 2) return;
                Player p = Common.playerlist[Common.myType];

                p.calcProfit();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

    }
}
