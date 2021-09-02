using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using System.Drawing;
using System.Drawing.Drawing2D;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public static class Common
    {
        public static string sfile;                               //location of server.ini       
        public static int clientCount = 0;                        //number of connected clients
        public static player[] playerlist = new player[1000];     //list of connected clients

        //forms
        public static frmMain FrmServer;
        public static frmSetup1 FrmSetup1;
        public static frmSetup2 FrmSetup2;
        public static frmSetup3 FrmSetup3;
        public static frmSetup4 FrmSetup4;
        public static frmSetup5 FrmSetup5;
        public static frmReplay FrmReplay;

        //data files
        public static StreamWriter summaryDf;                  //summary data file
        //public static StreamWriter eventsDf;                 //events data file
        public static StreamWriter replayDf;                   //events data file
        public static StreamWriter recruiterDf;                         //events data file

        //global parameters
        public static int numberOfPlayers;                     //number of players needed
        public static int numberOfPeriods;                     //number of periods in the experiment  
        public static int portNumber;                          //port for socket communication 
        public static int instructionX;                        //x location of intstruction window
        public static int instructionY;                        //y location of intstruction window
        public static int windowX;                             //x location of main window
        public static int windowY;                             //y location of main window
        public static bool showInstructions;                   //show instructions before experiment starts
        public static bool testMode;                           //turn on auto test system
        public static int checkin=0;                           //global counter for subject actions 

        public static int currentPeriod = 0;                   //current period      
        public static int timeRemaining = 0;                   //timer remaing in current period   
        public static int millisecondCounter = 0;              //milliseconds passed between timer ticks  
        public static bool closing = false;                    //true once the software is reset
        public static int periodLength = 0;                    //period length in seconds
        public static float locationIncrement = 0;             //smallest change in location 
        public static bool showPartnerInfo = true;             //show partner info subject on their screen   
        public static float earningsMultiplier = 1;             //multiple points by this to get us dollars

        public static Treatment[] treaments = new Treatment[100];    //list of each available treatment
        public static int treatmentCount = 0;                        //number of " 

        public static Period[] periods = new Period[1000];           //list of periods
        //currency
        public static System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

        public static int selectedPlayer = 1;                    //player selected to show on graph   
        public static int selectedPeriod = 1;                    //player selected to show on graph    

        public static bool replayWritten = false;                //true after replay file is written

        //instruction example parameters
        public static float instructionPlayerLeft;
        public static float instructionPlayerRight;
        public static float instructionPartnerLeft1;
        public static float instructionPartnerRight1;
        public static float instructionPartnerLeft2;
        public static float instructionPartnerRight2;

        public static void start()
        {
            sfile = Application.StartupPath + @"\server.ini";
            EventLog.AppEventLog_Init();

            loadParameters();

            culture.NumberFormat.CurrencyNegativePattern = 1;
        }

        public static void loadParameters()
        {
            numberOfPlayers =int.Parse(INI.getINI(sfile, "gameSettings", "numberOfPlayers"));
            numberOfPeriods = int.Parse(INI.getINI(sfile, "gameSettings", "numberOfPeriods"));
            portNumber = int.Parse(INI.getINI(sfile, "gameSettings", "port"));
            instructionX = int.Parse(INI.getINI(sfile, "gameSettings", "instructionX"));
            instructionY = int.Parse(INI.getINI(sfile, "gameSettings", "instructionY"));
            windowX = int.Parse(INI.getINI(sfile, "gameSettings", "windowX"));
            windowY = int.Parse(INI.getINI(sfile, "gameSettings", "windowY"));

            periodLength = int.Parse(INI.getINI(Common.sfile, "gameSettings", "periodLength"));
            locationIncrement = float.Parse(INI.getINI(Common.sfile, "gameSettings", "locationIncrement"));
            earningsMultiplier = float.Parse(INI.getINI(Common.sfile, "gameSettings", "earningsMultiplier"));

            showPartnerInfo = bool.Parse(INI.getINI(sfile, "gameSettings", "showPartnerInfo"));
            showInstructions = bool.Parse(INI.getINI(sfile, "gameSettings", "showInstructions"));
            testMode = bool.Parse(INI.getINI(sfile, "gameSettings", "testMode"));

            instructionPlayerLeft = float.Parse(INI.getINI(Common.sfile, "gameSettings", "instructionPlayerLeft"));
            instructionPlayerRight = float.Parse(INI.getINI(Common.sfile, "gameSettings", "instructionPlayerRight"));
            instructionPartnerLeft1 = float.Parse(INI.getINI(Common.sfile, "gameSettings", "instructionPartnerLeft1"));
            instructionPartnerRight1 = float.Parse(INI.getINI(Common.sfile, "gameSettings", "instructionPartnerRight1"));
            instructionPartnerLeft2 = float.Parse(INI.getINI(Common.sfile, "gameSettings", "instructionPartnerLeft2"));
            instructionPartnerRight2 = float.Parse(INI.getINI(Common.sfile, "gameSettings", "instructionPartnerRight2"));
    }

        //process incoming message from a client
        public static void takeMessage(List<string> sinstr)
        {
            try
            {

                int index = int.Parse(sinstr[0]);
                string str = sinstr[1];

                string[] tempa = new string[4];
                tempa[1] = "<SEP>";
                tempa[2] = "<EOF>";
                string[] msgtokens = str.Split(tempa, 3, StringSplitOptions.None);

                string id = msgtokens[0];
                string message = msgtokens[1];

                switch (id)
                {
                    case "COMPUTER_NAME":
                        takeRemoteComputerName(index, message);
                        break;
                    case "SUBJECT_NAME":
                        takeName(index, message);
                        break;
                    case "FINSHED_INSTRUCTIONS":
                        takeFinishedInstructions(index, message);
                        break;
                    case "INSTRUCTION_PAGE":
                        takeInstructionPage(index, message);
                        break;
                    case "CLIENT_ERROR":
                        takeClientError(index, message);
                        break;
                    case "01":
                        takeBegin(index, message);
                        break;
                    case "02":
                        takeSelection(index, message);
                        break;
                    case "03":
                        takeCalculationRequest(index, message);
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

        static void takeName(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                playerlist[index].name = msgtokens[nextToken++].Replace(",", "<COMMA>");
                playerlist[index].studentID = msgtokens[nextToken++].Replace(",", "<COMMA>");

                FrmServer.dgMain[1, index - 1].Value = playerlist[index].name;

                checkin += 1;

                if (checkin == numberOfPlayers)
                {
                    checkin = 0;

                    //summary price data

                    summaryDf.WriteLine(",");
                    summaryDf.WriteLine("Number,Name,Student ID,Earnings");

                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        string earnings = String.Format(culture, "{0:C}", playerlist[i].earnings / 100);
                        string outstr = "";

                        outstr = playerlist[i].inumber + ",";
                        outstr += playerlist[i].name + ",";
                        outstr += playerlist[i].studentID + ",";
                        outstr += earnings + ",";

                        summaryDf.WriteLine(outstr);

                        //recruiter earnings file
                        string outstr2 = playerlist[i].studentID + ",";
                        outstr2 += earnings + ",";

                        recruiterDf.WriteLine(outstr2);
                    }

                    Common.writeReplayJSON();

                    summaryDf.Close();                   
                    replayDf.Close();
                    recruiterDf.Close();
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeBegin(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                checkin += 1;

                FrmServer.dgMain[2, index - 1].Value = "Waiting";

                playerlist[index].selectionLeft[Common.currentPeriod] = float.Parse(msgtokens[nextToken++]);
                playerlist[index].selectionRight[Common.currentPeriod] = float.Parse(msgtokens[nextToken++]);

                if (checkin == numberOfPlayers)
                {
                    if(Common.currentPeriod == 1)
                        MessageBox.Show("Start?", "Start", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    checkin = 0;
                    showInstructions = false;

                    handleSelection();                    
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeSelection(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                checkin += 1;

                //FrmServer.dgMain[2, index - 1].Value = "Waiting";

                playerlist[index].selectionLeft[Common.currentPeriod] = float.Parse(msgtokens[nextToken++]);
                playerlist[index].selectionRight[Common.currentPeriod] = float.Parse(msgtokens[nextToken++]);

                //set values
                playerlist[index].valueLeft[Common.currentPeriod] = getLinePointRevenue(playerlist[index].selectionLeft[Common.currentPeriod]).Y;
                playerlist[index].valueRight[Common.currentPeriod] = getLinePointRevenue(playerlist[index].selectionRight[Common.currentPeriod]).Y;

                if (checkin == numberOfPlayers)
                {
                    //MessageBox.Show("Start?", "Start", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    checkin = 0;
                    handleSelection();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void handleSelection()
        {
            try
            {
                if (Common.currentPeriod == Common.numberOfPeriods)
                {
                    //end game
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerlist[i].calcCurrentProfit();
                        playerlist[i].writeSummaryData();
                        playerlist[i].sendShowName();
                    }
                    selectedPeriod = currentPeriod;
                }
                else
                {
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerlist[i].calcCurrentProfit();
                        playerlist[i].writeSummaryData();
                    }

                    Common.currentPeriod++;
                    selectedPeriod = currentPeriod - 1;

                    string str2 = "";

                    //start next treatment
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerlist[i].sendStartNextPeriod(str2);
                    }
                }
            }
             catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
        
        static void takeFinishedInstructions(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                //int nextToken = 0;

                checkin += 1;

                FrmServer.dgMain[2, index - 1].Value = "Waiting";

                if (checkin == numberOfPlayers)
                {
                    MessageBox.Show("Start Experiment?", "Start", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    checkin = 0;
                    showInstructions = false;                                        

                 

                    //reset players
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                      
                    }

                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerlist[i].sendFinishedInstructions();
                        FrmServer.dgMain[2, i - 1].Value = "Playing";
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeInstructionPage(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                int tempPage = int.Parse(msgtokens[nextToken++]);

                Common.FrmServer.dgMain[2, index - 1].Value = "Page " + tempPage;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeClientError(int index,string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                EventLog.appEventLog_Write("client " + index.ToString() + " error :" + msgtokens[nextToken]);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeRemoteComputerName(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                playerlist[index].sp.remoteComputerName = msgtokens[nextToken++];
                FrmServer.dgMain[1, index - 1].Value = playerlist[index].sp.remoteComputerName;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        static void takeCalculationRequest(int index, string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

                float selectionLeft = float.Parse(msgtokens[nextToken++]);
                float selectionRight = float.Parse(msgtokens[nextToken++]);
                float otherSelectionLeft = float.Parse(msgtokens[nextToken++]);
                float otherSelectionRight = float.Parse(msgtokens[nextToken++]);

                float revenue = 0;
                float cost = 0;
                float range = 0;
                float rangeOverlap = 0;

                float revenueMultiplier = 0;
                if (playerlist[index].myType == 1)
                    revenueMultiplier = Common.periods[Common.currentPeriod].treatment.blueRevenuePercent;
                else
                    revenueMultiplier = Common.periods[Common.currentPeriod].treatment.redRevenuePercent;

                Common.calcCurrentCost(selectionLeft,
                                      selectionRight,
                                      ref cost,
                                      ref range,
                                      playerlist[index].myType);

                Common.calcCurrentRevenue(ref revenue,
                                          ref rangeOverlap,
                                          selectionLeft,
                                          selectionRight,
                                          otherSelectionLeft,
                                          otherSelectionRight,
                                          revenueMultiplier,
                                          range);

                float profit = revenue - cost;

                string outstr = "";

                outstr = revenue + ";";
                outstr += cost + ";";
                outstr += profit + ";";

                playerlist[index].sendCalculationRequest(outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void startNextPeriod()
        {
            try
            {
                //record summary data
                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    playerlist[i].writeSummaryData();
                }

                //update displayed earnings
                if (Common.replayDf != null)  //replay
                { 
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        Common.FrmServer.dgMain[3, i - 1].Value = String.Format(culture, "{0:C}", playerlist[i].earnings / 100);
                    }
                }

                if (currentPeriod == numberOfPeriods)
                {

                    //end game
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerlist[i].sendShowName();
                    }
                }
                else
                {
                    if (!Common.showInstructions) currentPeriod += 1;
                    timeRemaining = periodLength;

                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerlist[i].sendStartNextPeriod("");
                    }                    
                }
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static int returnDistance(int x1, int y1, int x2, int y2)
        {
            return Convert.ToInt32(Math.Round(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2))));
        }

        public static string timeConversion(int sec)
        {
            try
            {
                // appEventLog_Write("time conversion :" & sec)
                return ((sec / 60).ToString("D2") + ":" + (sec % 60).ToString("D2"));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error timeConversion:", ex);
                return "";
            }

        }
               
        public static string dZero(double s)
        {
            try
            {
                return (string.Format("{0:0.00}", s));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "0.00";
            }
        }

        public static float calcRevenueArea(PointF pt1, PointF pt2 , float selectionLeft, float selectionRight)
        {
            try
            {
                float total = 0;

                PointF pt3 = new PointF(periods[currentPeriod].treatment.middleX, periods[currentPeriod].treatment.middleY);

                if (selectionLeft < 0 && selectionRight > 0)
                {
                    total = calcRevenueArea2(pt3.X, pt3.Y, pt1.X, pt1.Y);
                    total += calcRevenueArea2(pt3.X, pt3.Y, pt2.X, pt2.Y);
                }
                else
                {
                    total = calcRevenueArea2(pt1.X,pt1.Y,pt2.X,pt2.Y);
                }

                return total;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        public static float calcRevenueArea2(float x1, float y1, float x2,float y2)
        {
            try
            {
                float total = 0;
                //PointF pt1 = Common.Frm1.getLinePoint(x1);
                //PointF pt2 = Common.Frm1.getLinePoint(x2);
                //float y1 = Common.Frm1.convertFromY(pt1.Y);
                //float y2 = Common.Frm1.convertFromY(pt2.Y);

                //triangle
                total = ((y1 - y2) * (x2 - x1)) / 2;

                //sqaure
                total += y2 * (x2 - x1);
                return Math.Abs(total);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        //get point based on parameter values
        public static PointF getLinePointRevenue(float value)
        {
            try
            {
                

                Period p = Common.periods[Common.currentPeriod];
                Treatment t = p.treatment;

                PointF pt1 = new PointF(p.treatment.leftX, p.treatment.leftY);          // left selection point
                PointF pt2 = new PointF(p.treatment.rightX, p.treatment.rightY);        // right selection point
                PointF pt3 = new PointF(p.treatment.middleX, p.treatment.middleY);      // middle selection point

                if (value == p.treatment.middleX)
                {
                    //point in the middle
                    return pt3;
                }
                else if (value < p.treatment.middleX)
                {
                    //use left side line

                    return getPointIntersection(new PointF(value,0),
                                                new PointF(value, t.scaleHeight),
                                                pt1,
                                                pt3);
                }
                else
                {
                    //use right side line
                    return getPointIntersection(new PointF(value, 0),
                                                new PointF(value, t.scaleHeight),
                                                pt2,
                                                pt3);
                }


            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return new Point(0, 0);
            }
        }

        public static PointF getPointIntersection(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
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

        public static float getRange(float sStart,float sEnd)
        {
            try
            {
                float total = 0;

                if(sStart<=0)
                {
                    //start is less than zero    
                    if (sEnd<=0)
                    {
                        //end is less than zero
                        total = Math.Abs(sStart - sEnd);
                    }
                    else
                    {
                        //end is greater than zero
                        total = Math.Abs(sStart) + sEnd; 
                    }
                }
                else
                {
                    //start is greater than zero
                    total = sEnd - sStart;
                }

                return total;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        public static void writeReplayJSON()
        {
            try
            {
                if (replayWritten) return;
                if (Common.FrmServer.cmdBegin.Enabled) return;

                replayWritten = true;

                JObject jo = new JObject();

                for (int i = 1; i <= Common.numberOfPlayers; i++)
                {                    
                    jo.Add(playerlist[i].getJson(i));                    
                }

                Common.replayDf.WriteLine(jo.ToString());
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void calcCurrentCost(float selectionLeft,float selectionRight, ref float cost,ref float range,int myType)
        {
            try
            {

                Period p = Common.periods[Common.currentPeriod];

                float totalRange = 0;

                if (selectionLeft < 0)
                {
                    totalRange += Math.Abs(selectionLeft);
                }
                else if (selectionLeft > 0)
                {
                    totalRange -= Math.Abs(selectionLeft);
                }

                if (selectionRight > 0)
                {
                    totalRange += Math.Abs(selectionRight);
                }
                else if (selectionRight < 0)
                {
                    totalRange -= Math.Abs(selectionRight);
                }

                cost = (float)Math.Pow(totalRange, 2) * p.treatment.cost[myType] / 2;
                cost = (float)Math.Round(cost, 2);
                range = totalRange;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public static void calcCurrentRevenue(ref float revenue,
                                              ref float rangeOverlap,
                                              float mySelectionLeft,
                                              float mySelectionRight,
                                              float otherSelectionLeft,
                                              float otherSelectionRight,
                                              float revenueMultiplier,
                                              float range)
        {
            try
            {
                revenue = 0;
                rangeOverlap = 0;

                Treatment t = Common.periods[Common.currentPeriod].treatment;

                PointF myPt1 = new PointF(mySelectionLeft, 0);      //left side location
                PointF myPt2 = new PointF(mySelectionRight, 0);      //right side location

                PointF myPt3 = Common.getLinePointRevenue(mySelectionLeft);       //left side value
                PointF myPt4 = Common.getLinePointRevenue(mySelectionRight);      //right side value

                PointF otherPt1 = new PointF(otherSelectionLeft, 0);      //left side location
                PointF otherPt2 = new PointF(otherSelectionRight, 0);     //right side location

                PointF otherPt3 = Common.getLinePointRevenue(otherSelectionLeft);       //left side value
                PointF otherPt4 = Common.getLinePointRevenue(otherSelectionRight);      //right side value

                if (mySelectionLeft <= otherSelectionRight && mySelectionLeft >= otherSelectionLeft &&
                    mySelectionRight <= otherSelectionRight && mySelectionRight >= otherSelectionLeft)
                {
                    //fully within other player
                    revenue += Common.calcRevenueArea(myPt3, myPt4, mySelectionLeft, mySelectionRight) * revenueMultiplier;
                    rangeOverlap = range;
                }
                else if (otherSelectionLeft >= mySelectionRight || otherSelectionRight <= mySelectionLeft)
                {
                    //no overlap
                    revenue += Common.calcRevenueArea(myPt3, myPt4, mySelectionLeft, mySelectionRight);
                }
                else
                {
                    if (mySelectionLeft < otherSelectionLeft)
                    {
                        //left edge, no over lap
                        revenue += Common.calcRevenueArea(myPt3, otherPt3, mySelectionLeft, otherSelectionLeft);
                    }
                    else if (mySelectionLeft <= otherSelectionRight && mySelectionRight >= otherSelectionRight)
                    {
                        //left edge over lap
                        revenue += Common.calcRevenueArea(myPt3, otherPt4, mySelectionLeft, otherSelectionRight) * revenueMultiplier;
                        rangeOverlap += Common.getRange(mySelectionLeft, otherSelectionRight);
                    }

                    if (mySelectionRight > otherSelectionRight)
                    {
                        //right edge no over lap
                        revenue += Common.calcRevenueArea(myPt4, otherPt4, otherSelectionRight, mySelectionRight);
                    }
                    else if (mySelectionRight <= otherSelectionRight && mySelectionRight >= otherSelectionLeft)
                    {
                        //right edge over lap
                        revenue += Common.calcRevenueArea(otherPt3, myPt4, otherSelectionLeft, mySelectionRight) * revenueMultiplier;
                        rangeOverlap += Common.getRange(otherSelectionLeft, mySelectionRight);
                    }

                    //both edges no overlap
                    if (mySelectionLeft < otherSelectionLeft && mySelectionRight > otherSelectionRight)
                    {
                        //other player fully within player
                        revenue += Common.calcRevenueArea(otherPt3, otherPt4, otherSelectionLeft, otherSelectionRight) * revenueMultiplier;
                        rangeOverlap += Common.getRange(otherSelectionLeft, otherSelectionRight);
                    }

                    revenue = (float)Math.Round(revenue, 2);

                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

    }
}
