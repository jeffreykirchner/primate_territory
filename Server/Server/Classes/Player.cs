using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Server
{
    public class player
    {
        public socketPlayer sp = new socketPlayer();
        public int inumber;
        public string name;
        public string studentID;
        public double earnings;
        public int myType;                              //1 or 2 (Blue , Red)
        public int partner = -1;

        public float[] selectionLeft = new float[10000];
        public float[] valueLeft = new float[10000];

        public float[] selectionRight = new float[10000];
        public float[] valueRight = new float[10000];

        public float[] profit = new float[10000];         //earnigs in points
        public float[] revenue = new float[10000];
        public float[] cost = new float[10000];

        public float[] range = new float[10000];
        public float[] rangeOverlap = new float[10000];

        public float[] maxRevenue = new float[10000];

        //drawing

        public System.Drawing.Color color;
        public Pen p2Dash;

        public RectangleF leftHandle;
        public RectangleF rightHandle;

        public void FromString(ref string[] msgtokens,ref int nextToken)
        {
            try
            {
            

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public override string ToString()
        {
            try
            {
                string s="";              

                return s;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

        //send message to the client
        public void sendMessage(string index, string message)
        {
            try
            {
                if (sp == null) return;
                if (Common.closing) return;

                sp.send(index, message);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //send begin message to client
        public void sendBegin(string str)
        {
            try
            {                
                string outstr = str;

                outstr += Common.numberOfPlayers + ";";
                outstr += Common.numberOfPeriods + ";";
                outstr += Common.instructionX + ";";
                outstr += Common.instructionY + ";";
                outstr += Common.windowX + ";";
                outstr += Common.windowY + ";";
                outstr += Common.showInstructions + ";";
                outstr += inumber + ";";
                outstr += Common.testMode + ";";
                outstr += Common.periodLength + ";";
                outstr += myType + ";";
                outstr += Common.locationIncrement + ";";
                outstr += Common.earningsMultiplier + ";";
                outstr += Common.showPartnerInfo + ";";
                outstr += Common.enableChat + ";";

                outstr += Common.instructionPlayerLeft + ";";
                outstr += Common.instructionPlayerRight + ";";
                outstr += Common.instructionPartnerLeft1 + ";";
                outstr += Common.instructionPartnerRight1 + ";";
                outstr += Common.instructionPartnerLeft2 + ";";
                outstr += Common.instructionPartnerRight2 + ";";

                outstr += Common.treatmentCount + ";";

                for (int i=1;i<=Common.treatmentCount;i++)
                {
                    outstr += Common.treaments[i].toString();
                }

                for (int i = 1; i <= Common.numberOfPeriods; i++)
                {
                    outstr += Common.periods[i].toString();
                }

                sendMessage("BEGIN", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void sendInvalidConnection()
        {
            try
            {
                sendMessage("INVALID_CONNECTION", "");
            }
            catch (Exception ex)
            {
               EventLog.appEventLog_Write("error :", ex);
            }
        }
        
        //kill the clients
        public void sendReset()
        {
            try
            {
                string outstr = "";
                sendMessage("RESET", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //kill the clients
        public void sendShowName()
        {
            try
            {
                if (Common.replayDf == null) return;   //replay

                string outstr = "";

                outstr = String.Format(Common.culture, "{0:C}", earnings / 100) + ";";

                sendMessage("SHOW_NAME", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //all subjects done with instructions, proceed to next phase
        public void sendFinishedInstructions()
        {
            try
            {

                string outstr = "";


                sendMessage("FINISHED_INSTRUCTIONS", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        //singal the clients that the current period is now that last period
        public void sendEndEarly()
        {
            try
            {
                string outstr = Common.numberOfPeriods + ";";

                sendMessage("END_EARLY", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void sendUpdateTarget(string str)
        {
            try
            {
                sendMessage("01", str);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void sendStartNextPeriod(string str)
        {
            try
            {
                if (Common.replayDf == null) return;   //replay, do not record data              

                string outstr = "";

                outstr += Common.currentPeriod.ToString() + ";";
                outstr += earnings.ToString() + ";";

                outstr += revenue[Common.currentPeriod-1] + ";";
                outstr += cost[Common.currentPeriod-1] + ";";
                outstr += profit[Common.currentPeriod-1] + ";";

                outstr += Common.convertToCents(revenue[Common.currentPeriod - 1]).ToString() + ";";
                outstr += Common.convertToCents(cost[Common.currentPeriod - 1]).ToString() + ";";

                outstr += Common.playerlist[partner].revenue[Common.currentPeriod-1] + ";";
                outstr += Common.playerlist[partner].cost[Common.currentPeriod-1] + ";";
                outstr += Common.playerlist[partner].profit[Common.currentPeriod-1] + ";";

                outstr += Common.convertToCents(Common.playerlist[partner].revenue[Common.currentPeriod - 1]).ToString() + ";";
                outstr += Common.convertToCents(Common.playerlist[partner].cost[Common.currentPeriod - 1]).ToString() + ";";

                outstr += Common.playerlist[partner].selectionLeft[Common.currentPeriod-1].ToString() + ";";
                outstr += Common.playerlist[partner].selectionRight[Common.currentPeriod-1].ToString() + ";";

                sendMessage("START_NEXT_PERIOD", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void sendCalculationRequest(string str)
        {
            try
            {
                if (Common.replayDf == null) return;   //replay, do not record data              

                string outstr = str;

                sendMessage("01", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void sendChat(string str)
        {
            try
            {
                if (Common.replayDf == null) return;   //replay, do not record data              
                string outstr = str;
                sendMessage("02", outstr);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void writeSummaryData()
        {
            try
            {
                //string str = "Period,Treatment,Player,Color,Partner,LeftLocation,LeftValue,RightLocation,RightValue,RangeTotal,RangeOverlap,Revenue,TotalCost,Profit,GraphLeftX,GraphLeftY,GraphMiddleX,GraphMiddleY,GraphRightX,GraphRightY,BlueCost,RedCost";

                if (Common.showInstructions) return;

                Period p = Common.periods[Common.currentPeriod];

                string str = "";

                str = Common.parmaterSetName + ",";
                str += Common.currentPeriod + ",";
                str += p.treatment.name + ",";
                str += Common.periods[Common.currentPeriod].treatment.blueRevenuePercent + ",";
                str += Common.periods[Common.currentPeriod].treatment.redRevenuePercent + ",";
                str += inumber + ",";

                if (myType == 1)
                    str += "Blue,";
                else
                    str += "Red,";

                str += partner + ",";
                str += selectionLeft[Common.currentPeriod] + ",";
                str += valueLeft[Common.currentPeriod] + ",";
                str += selectionRight[Common.currentPeriod] + ",";
                str += valueRight[Common.currentPeriod] + ",";
                str += range[Common.currentPeriod] + ",";
                str += rangeOverlap[Common.currentPeriod] + ",";

                str += revenue[Common.currentPeriod] + ",";
                str += cost[Common.currentPeriod] + ",";

                str += Common.convertToCents(revenue[Common.currentPeriod]).ToString() + ",";
                str += Common.convertToCents(cost[Common.currentPeriod]).ToString() + ",";
                str += profit[Common.currentPeriod] + ",";

                str += p.treatment.leftX + ",";
                str += p.treatment.leftY + ",";
                str += p.treatment.middleX + ",";
                str += p.treatment.middleY + ",";
                str += p.treatment.rightX + ",";
                str += p.treatment.rightY + ",";
                str += p.treatment.cost[1] + ",";
                str += p.treatment.cost[2];

                Common.summaryDf.WriteLine(str);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);               
            }
        }


        //public void writeReplayData()
        //{
        //    try
        //    {
        //        //str = "Period,Time,Player,LocationX,LocationY,TargetX,TargetY,Health,HealthTime,CoolingTime,Emote,EmoteTime,";

        //        if (Common.replayDf == null) return;   //replay, do not record data
        //        if (Common.showInstructions) return;

        //        string outstr = "";

        //        outstr = Common.currentPeriod + ",";
        //        outstr += Common.timeRemaining + ",";
        //        outstr += inumber + ",";

        //        Common.replayDf.WriteLine(outstr);
        //    }
        //     catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);                
        //    }
        //}

        //public void readReplayData(string s)
        //{
        //    try
        //    {
        //        string[] msgtokens = s.Split(',');
        //        //int nextToken = 3;
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLog.appEventLog_Write("error :", ex);
        //    }
        //}
        
        

        public void calcCurrentProfit()
        {
            try
            {
                player otherPlayer = Common.playerlist[partner];

                float revenueMultiplier = 0;
                if (myType == 1)
                    revenueMultiplier = Common.periods[Common.currentPeriod].treatment.blueRevenuePercent;
                else
                    revenueMultiplier = Common.periods[Common.currentPeriod].treatment.redRevenuePercent;

                Common.calcCurrentCost(selectionLeft[Common.currentPeriod],
                                       selectionRight[Common.currentPeriod],
                                       ref cost[Common.currentPeriod],
                                       ref range[Common.currentPeriod],
                                       myType);

                Common.calcCurrentRevenue(ref revenue[Common.currentPeriod],
                                          ref rangeOverlap[Common.currentPeriod],
                                          selectionLeft[Common.currentPeriod],
                                          selectionRight[Common.currentPeriod],
                                          otherPlayer.selectionLeft[Common.currentPeriod],
                                          otherPlayer.selectionRight[Common.currentPeriod],
                                          revenueMultiplier,
                                          range[Common.currentPeriod]);

                profit[Common.currentPeriod] = (float)Common.convertToCents(revenue[Common.currentPeriod]) - (float)Common.convertToCents(cost[Common.currentPeriod]);

                profit[Common.currentPeriod] = (float)Math.Round(profit[Common.currentPeriod], 2);

                earnings += profit[Common.currentPeriod];

                Common.FrmServer.dgMain[3, inumber - 1].Value = string.Format(Common.culture, "{0:C}", Math.Round(earnings/100,2));
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

                player otherPlayer = Common.playerlist[partner];

                float mySLeft = selectionLeft[Common.selectedPeriod];
                float mySRight = selectionRight[Common.selectedPeriod];
                float otherSLeft = otherPlayer.selectionLeft[Common.selectedPeriod];
                float otherSRight = otherPlayer.selectionRight[Common.selectedPeriod];

                Treatment t = Common.periods[Common.selectedPeriod].treatment;

                float sharedRevenuePercent = 0;

                if (myType == 1)
                {
                    sharedRevenuePercent = t.blueRevenuePercent;
                }
                else
                {
                    sharedRevenuePercent = t.redRevenuePercent;
                }

                PointF myPt1 = new PointF(Common.FrmServer.convertToX(mySLeft,t.scaleRange), 0);      //left side location
                PointF myPt2 = new PointF(Common.FrmServer.convertToX(mySRight, t.scaleRange), 0);     //right side location

                PointF myPt3 = Common.FrmServer.getLinePoint(mySLeft);       //left side value
                PointF myPt4 = Common.FrmServer.getLinePoint(mySRight);      //right side value

                PointF otherPt1 = new PointF(Common.FrmServer.convertToX(otherSLeft, t.scaleRange), 0);      //left side location
                PointF otherPt2 = new PointF(Common.FrmServer.convertToX(otherSRight, t.scaleRange), 0);     //right side location

                PointF otherPt3 = Common.FrmServer.getLinePoint(otherSLeft);       //left side value
                PointF otherPt4 = Common.FrmServer.getLinePoint(otherSRight);      //right side value

                GraphicsPath gp1 = new GraphicsPath();

                if (mySLeft <= otherSRight && mySLeft >= otherSLeft &&
                   mySRight <= otherSRight && mySRight >= otherSLeft)
                {
                    //fully within other player
                    gp1 = getFillPath(myPt1,
                                        myPt2,
                                        new PointF(myPt3.X, myPt3.Y * sharedRevenuePercent),
                                        new PointF(myPt4.X, myPt4.Y * sharedRevenuePercent),
                                        new PointF(t.pt3.X, t.pt3.Y * sharedRevenuePercent),
                                        mySLeft, mySRight);
                    g.FillPath(Brushes.LightYellow, gp1);
                }
                else if (otherSLeft >= mySRight || otherSRight <= mySLeft)
                {
                    //no overlap
                    gp1 = getFillPath(myPt1, myPt2, myPt3, myPt4, t.pt3, mySLeft, mySRight);
                    g.FillPath(Brushes.LightYellow, gp1);
                }
                else
                {

                    if (mySLeft < otherSLeft)
                    {
                        //left edge, no over lap
                        gp1 = getFillPath(myPt1, otherPt1, myPt3, otherPt3, t.pt3, mySLeft, otherSLeft);
                        g.FillPath(Brushes.LightYellow, gp1);
                    }
                    else if (mySLeft <= otherSRight && mySRight >= otherSRight)
                    {
                        //left edge over lap
                        gp1 = getFillPath(myPt1,
                                          otherPt2,
                                          new PointF(myPt3.X, myPt3.Y * sharedRevenuePercent),
                                          new PointF(otherPt4.X, otherPt4.Y * sharedRevenuePercent),
                                          new PointF(t.pt3.X, t.pt3.Y * sharedRevenuePercent),
                                          mySLeft, otherSRight);
                        g.FillPath(Brushes.LightYellow, gp1);
                    }



                    if (mySRight > otherSRight)
                    {
                        //right edge no over lap
                        gp1 = getFillPath(myPt2, otherPt2, myPt4, otherPt4, t.pt3, otherSRight, mySRight);
                        g.FillPath(Brushes.LightYellow, gp1);
                    }
                    else if (mySRight <= otherSRight && mySRight >= otherSLeft)
                    {
                        //right edge over lap
                        gp1 = getFillPath(otherPt1,
                                          myPt2,
                                          new PointF(otherPt3.X, otherPt3.Y * sharedRevenuePercent),
                                          new PointF(myPt4.X, myPt4.Y * sharedRevenuePercent),
                                          new PointF(t.pt3.X, t.pt3.Y * sharedRevenuePercent),
                                          otherSLeft, mySRight);
                        g.FillPath(Brushes.LightYellow, gp1);
                    }

                    //both edges no overlap
                    if (mySLeft < otherSLeft && mySRight > otherSRight)
                    {
                        //fully within other player
                        gp1 = getFillPath(otherPt1,
                                            otherPt2,
                                            new PointF(otherPt3.X, otherPt3.Y * sharedRevenuePercent),
                                            new PointF(otherPt4.X, otherPt4.Y * sharedRevenuePercent),
                                            new PointF(t.pt3.X, t.pt3.Y * sharedRevenuePercent),
                                            otherSLeft, otherSRight);
                        g.FillPath(Brushes.LightYellow, gp1);
                    }



                }

                Region r = new Region();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt1">Lower left</param>
        /// <param name="pt2">Lower right</param>
        /// <param name="pt3">Upper Left</param>
        /// <param name="pt4">Upper Right</param>
        /// <param name="pt5">Center Point</param>
        /// <param name="sLeft">Selection Left</param>
        /// <param name="sRight">Selection Right</param>
        /// <returns></returns>
        GraphicsPath getFillPath(PointF pt1, PointF pt2, PointF pt3, PointF pt4, PointF pt5, float sLeft, float sRight)
        {

            try
            {
                GraphicsPath gp = new GraphicsPath();

                gp.AddLine(pt1, pt3);

                if (sLeft < 0 && sRight > 0)
                {
                    gp.AddLine(pt3, pt5);
                    gp.AddLine(pt5, pt4);
                }
                else
                {
                    gp.AddLine(pt3, pt4);
                }

                gp.AddLine(pt4, pt2);
                gp.AddLine(pt2, pt1);

                return gp;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return new GraphicsPath();
            }
        }

        public void draw(Graphics g)
        {
            try
            {
                //player myPartner = Common.playerlist[partner];

                Treatment t = Common.periods[Common.selectedPeriod].treatment;

                float sLeft = selectionLeft[Common.selectedPeriod];
                float sRight = selectionRight[Common.selectedPeriod];

                PointF pt1 = new PointF(Common.FrmServer.convertToX(sLeft,t.scaleRange), 0);      //left side location
                PointF pt2 = new PointF(Common.FrmServer.convertToX(sRight,t.scaleRange), 0);     //right side location

                PointF pt3 = new PointF();      //left side value
                PointF pt4 = new PointF();      //right side value

                pt3 = Common.FrmServer.getLinePoint(sLeft);
                pt4 = Common.FrmServer.getLinePoint(sRight);

                //location bar
                g.FillRectangle(new SolidBrush(color),
                                new RectangleF(pt1.X, pt1.Y, pt2.X - pt1.X, Common.FrmServer.xTickHeight));

                //value intersections
                //if selection is same alternate showing them

                g.DrawLine(p2Dash, pt1, pt3);
                g.DrawLine(p2Dash, pt2, pt4);

                //control balls
                if (inumber == Common.selectedPlayer)
                {

                    //left handle
                    leftHandle = new RectangleF(Convert.ToInt32(Math.Round(pt1.X - 60)),
                                                                Convert.ToInt32(Math.Round(pt1.Y + Common.FrmServer.xTickHeight + 20)),
                                                                60,
                                                                25);

                    g.FillRectangle(Brushes.White, leftHandle.X, leftHandle.Y, leftHandle.Width, leftHandle.Height);
                    g.DrawLine(p2Dash, pt1.X, pt1.Y, pt1.X, pt1.Y + Common.FrmServer.xTickHeight + 20);
                    g.DrawRectangle(p2Dash, leftHandle.X, leftHandle.Y, leftHandle.Width, leftHandle.Height);
                    g.DrawString(string.Format("{0:0.00}", Math.Round(sLeft, 2)),
                                 Common.FrmServer.f12,
                                 Brushes.Black,
                                 new PointF(leftHandle.X + leftHandle.Width / 2, leftHandle.Y + leftHandle.Height / 2 - 10),
                                 Common.FrmServer.fmt);
                    g.DrawString("←A S→",
                                 Common.FrmServer.f10,
                                 Brushes.Black,
                                 new PointF(leftHandle.X + leftHandle.Width / 2, leftHandle.Y + leftHandle.Height + 2),
                                 Common.FrmServer.fmt);

                    //right hande
                    rightHandle = new RectangleF(Convert.ToInt32(Math.Round(pt2.X)),
                                                                 Convert.ToInt32(Math.Round(pt1.Y + Common.FrmServer.xTickHeight + 20)),
                                                                 60,
                                                                 25);

                    g.FillEllipse(Brushes.White, rightHandle.X, rightHandle.Y, rightHandle.Width, rightHandle.Height);
                    g.DrawLine(p2Dash, pt2.X, pt2.Y, pt2.X, pt2.Y + Common.FrmServer.xTickHeight + 20);
                    g.DrawRectangle(p2Dash, rightHandle.X, rightHandle.Y, rightHandle.Width, rightHandle.Height);
                    g.DrawString(string.Format("{0:0.00}", Math.Round(sRight, 2)),
                                Common.FrmServer.f12,
                                Brushes.Black,
                                new PointF(rightHandle.X + rightHandle.Width / 2, rightHandle.Y + rightHandle.Height / 2 - 10),
                                Common.FrmServer.fmt);
                    g.DrawString("←K L→",
                                Common.FrmServer.f10,
                                Brushes.Black,
                                new PointF(rightHandle.X + rightHandle.Width / 2, rightHandle.Y + rightHandle.Height + 2),
                                Common.FrmServer.fmt);
                }
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
                if (cost[Common.selectedPeriod] <= 0) return;
                if (Common.selectedPeriod <= 0) return;

                Treatment t = Common.periods[Common.selectedPeriod].treatment;

                float percent = cost[Common.selectedPeriod] / maxRevenue[Common.selectedPeriod];

                GraphicsPath gp = new GraphicsPath();

                PointF pt1 = new PointF(Common.FrmServer.convertToX(selectionLeft[Common.selectedPeriod],t.scaleRange), 0);      //left side location
                PointF pt2 = new PointF(Common.FrmServer.convertToX(selectionRight[Common.selectedPeriod],t.scaleRange), 0);     //right side location

                PointF pt3 = new PointF();      //left side value
                PointF pt4 = new PointF();      //right side value

                pt3 = Common.FrmServer.getLinePoint(selectionLeft[Common.selectedPeriod]);
                pt4 = Common.FrmServer.getLinePoint(selectionRight[Common.selectedPeriod]);

                pt3.Y *= percent;
                pt4.Y *= percent;

                gp = getFillPath(pt1, pt2, pt3, pt4, new PointF(t.pt3.X, t.pt3.Y * percent), selectionLeft[Common.selectedPeriod], selectionRight[Common.selectedPeriod]);

                g.FillPath(Brushes.LightGray, gp);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void setupGraphics()
        {
            try
            {
                if (myType == 1)
                {
                    color = Common.FrmServer.p1Color;
                }
                else
                {
                    color = Common.FrmServer.p2Color;
                }

                Common.FrmServer.setupPen(ref p2Dash, color, 2);
                p2Dash.DashStyle = DashStyle.Dash;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public JProperty getJson(int index)
        {
            try
            {
                JObject jo = new JObject();

                jo.Add(new JProperty("Number", inumber));
                jo.Add(new JProperty("Name", name));
                jo.Add(new JProperty("Student ID", studentID));
                jo.Add(new JProperty("Earnings", earnings));
                jo.Add(new JProperty("My Type", myType));
                jo.Add(new JProperty("Partner", partner));

                JObject periods = new JObject();

                for (int i = 1; i <= Common.numberOfPeriods; i++)
                {
                    JObject period = new JObject();

                    period.Add(new JProperty("Left Location", selectionLeft[i]));
                    period.Add(new JProperty("Left Value", valueLeft[i]));
                    period.Add(new JProperty("Max Revenue", maxRevenue[i]));

                    period.Add(new JProperty("Right Location", selectionRight[i]));
                    period.Add(new JProperty("Right Value", valueRight[i]));

                    period.Add(new JProperty("Profit", profit[i]));
                    period.Add(new JProperty("Revenue", revenue[i]));
                    period.Add(new JProperty("Cost", cost[i]));
                    period.Add(new JProperty("Revenue Cents", Common.convertToCents(revenue[i])));
                    period.Add(new JProperty("Cost Cents", Common.convertToCents(cost[i])));
                    period.Add(new JProperty("Location Range", range[i]));
                    period.Add(new JProperty("Location Range Overlap", rangeOverlap[i]));

                    periods.Add(new JProperty(i.ToString(), period));
                }

                jo.Add(new JProperty("Periods", periods));               

                return new JProperty(index.ToString(), jo);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return null;
            }
        }

        public void fromJSON(JProperty jp)
        {
            try
            {
                JObject jo = (JObject)jp.Value;

                name = (string)jo["Name"];
                studentID = (string)jo["Student ID"];
                earnings = (double)jo["Earnings"];
                myType = (int)jo["My Type"];
                partner = (int)jo["Partner"];

                //max value locations
                JObject periods = new JObject((JObject)jo["Periods"]);
                                
                for (int i = 1; i <= Common.numberOfPeriods; i++)
                {
                    JObject period = new JObject((JObject)periods[i.ToString()]);

                    selectionLeft[i] = (float)period["Left Location"];
                    valueLeft[i] = (float)period["Left Value"];

                    if (period["Max Revenue"] != null)
                        maxRevenue[i] = (float)period["Max Revenue"];
                    else
                        maxRevenue[i] = 100;                   

                    selectionRight[i] = (float)period["Right Location"];
                    valueRight[i] = (float)period["Right Value"];

                    profit[i] = (float)period["Profit"];
                    revenue[i] = (float)period["Revenue"];
                    cost[i] = (float)period["Cost"];

                    range[i] = (float)period["Location Range"];
                    rangeOverlap[i] = (float)period["Location Range Overlap"];
                }                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
