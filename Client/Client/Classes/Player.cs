using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public class Player
    {


        public string colorName="";                     //string color name
        public int myID=0;                              //id number 

        public float selectionLeft = 0;
        public float selectionRight = 0;

        public System.Drawing.Color color;
        public Pen p2Dash;

        public RectangleF leftHandle;
        public RectangleF rightHandle;

        public float currentMaxProfit = 0;            //max possible profit given current selection
        public float currentProfit = 0;               //realized profit from last period
        public float currentMinProfit = 0;            //min posssible profit given current selection

        public float currentMaxRevenue = 0;           //max possible revenue given selection
        public float currentRevenue = 0;              //realized revenue from last period given other player's selection 
        public float currentMinRevenue = 0;           //min possible revenue given selection

        public float currentSelectionCost = 0;        //cost set locally
        public float currentCost = 0;                 //cost from last period 

        public float costPercent = 0;                 //percent of revenue cost is consuming  

        public void FromString(ref string[] msgtokens, ref int nextToken,int myID)
        {
            try
            {
                this.myID = myID;

                if(myID == 1)
                {
                    color = Common.Frm1.p1Color;
                }
                else
                {
                    color = Common.Frm1.p2Color;
                }

                Common.Frm1.setupPen(ref p2Dash, color, 2);
                p2Dash.DashStyle = DashStyle.Dash;

                resetLocationSelection();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void resetLocationSelection()
        {
            try
            {
                selectionLeft = 0;
                selectionRight = 0;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void draw(Graphics g)
        {
            try
            {
                Player partner;
                if (Common.myType == 1)
                {
                    partner = Common.playerlist[2];
                }
                else
                {
                    partner = Common.playerlist[1];
                }
                Treatment t = Common.periods[Common.currentPeriod].treatment;

                PointF pt1 = new PointF(Common.Frm1.convertToX(selectionLeft,t.scaleRange), 0);      //left side location
                PointF pt2 = new PointF(Common.Frm1.convertToX(selectionRight, t.scaleRange), 0);     //right side location

                PointF pt3 = new PointF();      //left side value
                PointF pt4 = new PointF();      //right side value

                pt3 = Common.Frm1.getLinePointValue(selectionLeft);
                pt4 = Common.Frm1.getLinePointValue(selectionRight);

                //location bar
                g.FillRectangle(new SolidBrush(color),
                                new RectangleF(pt1.X,pt1.Y,pt2.X-pt1.X,Common.Frm1.xTickHeight));

                //value intersections
                //if selection is same alternate showing them

                g.DrawLine(p2Dash, pt1, pt3);
                g.DrawLine(p2Dash, pt2, pt4);          
                
                //control balls
                if(myID == Common.myType)
                {

                    //left handle
                    leftHandle = new RectangleF(Convert.ToInt32(Math.Round(pt1.X - 60)),
                                                                  Convert.ToInt32(Math.Round(pt1.Y + Common.Frm1.xTickHeight + 20)),
                                                                  60,
                                                                  25);

                    g.FillRectangle(Brushes.White, leftHandle.X, leftHandle.Y, leftHandle.Width, leftHandle.Height);
                    g.DrawLine(p2Dash, pt1.X, pt1.Y, pt1.X, pt1.Y + Common.Frm1.xTickHeight + 20);
                    g.DrawRectangle(p2Dash, leftHandle.X,leftHandle.Y,leftHandle.Width,leftHandle.Height);
                    g.DrawString(string.Format("{0:0.00}", Math.Round(selectionLeft, 2)),
                                 Common.Frm1.f12,
                                 Brushes.Black,
                                 new PointF(leftHandle.X+ leftHandle.Width/2, leftHandle.Y + leftHandle.Height/2-10),
                                 Common.Frm1.fmt);
                    g.DrawString("←A S→",
                                 Common.Frm1.f10,
                                 Brushes.Black,
                                 new PointF(leftHandle.X + leftHandle.Width / 2, leftHandle.Y + leftHandle.Height +2),
                                 Common.Frm1.fmt);

                    //right hande
                    rightHandle = new RectangleF(Convert.ToInt32(Math.Round(pt2.X)),
                                                                 Convert.ToInt32(Math.Round(pt1.Y + Common.Frm1.xTickHeight + 20)),
                                                                 60,
                                                                 25);

                    g.FillEllipse(Brushes.White, rightHandle.X, rightHandle.Y, rightHandle.Width, rightHandle.Height);
                    g.DrawLine(p2Dash, pt2.X, pt2.Y, pt2.X, pt2.Y + Common.Frm1.xTickHeight + 20);
                    g.DrawRectangle(p2Dash, rightHandle.X, rightHandle.Y, rightHandle.Width, rightHandle.Height);
                    g.DrawString(string.Format("{0:0.00}", Math.Round(selectionRight, 2)),
                                Common.Frm1.f12,
                                Brushes.Black,
                                new PointF(rightHandle.X + rightHandle.Width / 2, rightHandle.Y + rightHandle.Height / 2 -10),
                                Common.Frm1.fmt);
                    g.DrawString("←K L→",
                                Common.Frm1.f10,
                                Brushes.Black,
                                new PointF(rightHandle.X + rightHandle.Width / 2, rightHandle.Y + rightHandle.Height + 2),
                                Common.Frm1.fmt);
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
                if (currentSelectionCost <= 0) return;
                if (Common.currentPeriod <= 0) return;

                Treatment t = Common.periods[Common.currentPeriod].treatment;

                costPercent = currentSelectionCost/ currentMaxRevenue;
                
                GraphicsPath gp = new GraphicsPath();

                PointF pt1 = new PointF(Common.Frm1.convertToX(selectionLeft, t.scaleRange), 0);      //left side location
                PointF pt2 = new PointF(Common.Frm1.convertToX(selectionRight, t.scaleRange), 0);     //right side location

                PointF pt3 = new PointF();      //left side value
                PointF pt4 = new PointF();      //right side value

                pt3 = Common.Frm1.getLinePointValue(selectionLeft);
                pt4 = Common.Frm1.getLinePointValue(selectionRight);

                pt3.Y *= costPercent;
                pt4.Y *= costPercent;

                gp = getFillPath(pt1, pt2, pt3, pt4, new PointF(t.pt3.X, t.pt3.Y * costPercent), selectionLeft, selectionRight);
                
                g.FillPath(Brushes.LightGray, gp);
                Common.Frm1.costPath = gp;

                
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
        GraphicsPath getFillPath(PointF pt1,PointF pt2, PointF pt3,PointF pt4,PointF pt5, float sLeft,float sRight)
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

        public void drawRevenue(Graphics g)
        {
            try
            {
                if (currentSelectionCost <= 0) return;
                if (Common.currentPeriod <= 0) return;

                float sharedRevenuePercent=0;

                Treatment t = Common.periods[Common.currentPeriod].treatment;

                Player otherPlayer;
                if (myID == 1)
                {
                    otherPlayer = Common.playerlist[2];
                    sharedRevenuePercent = t.blueRevenuePercent;
                }
                else
                {
                    otherPlayer = Common.playerlist[1];
                    sharedRevenuePercent = t.redRevenuePercent;
                }

                
                PointF myPt1 = new PointF(Common.Frm1.convertToX(selectionLeft, t.scaleRange), 0);      //left side location
                PointF myPt2 = new PointF(Common.Frm1.convertToX(selectionRight, t.scaleRange), 0);     //right side location

                PointF myPt3 = Common.Frm1.getLinePointValue(selectionLeft);       //left side value
                PointF myPt4 = Common.Frm1.getLinePointValue(selectionRight);      //right side value

                PointF otherPt1 = new PointF(Common.Frm1.convertToX(otherPlayer.selectionLeft, t.scaleRange), 0);      //left side location
                PointF otherPt2 = new PointF(Common.Frm1.convertToX(otherPlayer.selectionRight, t.scaleRange), 0);     //right side location

                PointF otherPt3 = Common.Frm1.getLinePointValue(otherPlayer.selectionLeft);       //left side value
                PointF otherPt4 = Common.Frm1.getLinePointValue(otherPlayer.selectionRight);      //right side value

                GraphicsPath[] gp1 = new GraphicsPath[3];
                int gp1Count = 0;      

                if (selectionLeft<=otherPlayer.selectionRight && selectionLeft>=otherPlayer.selectionLeft &&
                   selectionRight<=otherPlayer.selectionRight && selectionRight>=otherPlayer.selectionLeft)
                {
                    //fully within other player
                    gp1[gp1Count++] = getFillPath(myPt1,
                                        myPt2,
                                        new PointF(myPt3.X, myPt3.Y * sharedRevenuePercent),
                                        new PointF(myPt4.X, myPt4.Y * sharedRevenuePercent),
                                        new PointF(t.pt3.X, t.pt3.Y * sharedRevenuePercent),
                                        selectionLeft, selectionRight);
                }
                else if (otherPlayer.selectionLeft>=selectionRight || otherPlayer.selectionRight<=selectionLeft)
                {
                    //no overlap
                    gp1[gp1Count++] = getFillPath(myPt1, myPt2, myPt3, myPt4, t.pt3, selectionLeft, selectionRight);
                }
                else
                {
                    
                    if(selectionLeft < otherPlayer.selectionLeft)
                    {
                        //left edge, no over lap
                        gp1[gp1Count++] = getFillPath(myPt1, otherPt1, myPt3, otherPt3, t.pt3, selectionLeft, otherPlayer.selectionLeft);
                    }
                    else if(selectionLeft <= otherPlayer.selectionRight && selectionRight >= otherPlayer.selectionRight)
                    {
                        //left edge over lap
                        gp1[gp1Count++] = getFillPath(myPt1,
                                          otherPt2,
                                          new PointF(myPt3.X,myPt3.Y * sharedRevenuePercent),
                                          new PointF(otherPt4.X, otherPt4.Y * sharedRevenuePercent),
                                          new PointF(t.pt3.X,t.pt3.Y * sharedRevenuePercent),
                                          selectionLeft, otherPlayer.selectionRight);
                    }                   

                    //both edges no overlap
                    if(selectionLeft<otherPlayer.selectionLeft && selectionRight> otherPlayer.selectionRight)
                    {
                        //fully within other player
                        gp1[gp1Count++] = getFillPath(otherPt1,
                                            otherPt2,
                                            new PointF(otherPt3.X, otherPt3.Y * sharedRevenuePercent),
                                            new PointF(otherPt4.X, otherPt4.Y * sharedRevenuePercent),
                                            new PointF(t.pt3.X, t.pt3.Y * sharedRevenuePercent),
                                            otherPlayer.selectionLeft,otherPlayer.selectionRight);                       
                    }

                    if (selectionRight > otherPlayer.selectionRight)
                    {
                        //right edge no over lap
                        gp1[gp1Count++] = getFillPath(otherPt2, myPt2, otherPt4, myPt4, t.pt3, otherPlayer.selectionRight, selectionRight);
                    }
                    else if (selectionRight <= otherPlayer.selectionRight && selectionRight >= otherPlayer.selectionLeft)
                    {
                        //right edge over lap
                        gp1[gp1Count++] = getFillPath(otherPt1,
                                          myPt2,
                                          new PointF(otherPt3.X, otherPt3.Y * sharedRevenuePercent),
                                          new PointF(myPt4.X, myPt4.Y * sharedRevenuePercent),
                                          new PointF(t.pt3.X, t.pt3.Y * sharedRevenuePercent),
                                          otherPlayer.selectionLeft, selectionRight);
                    }

                }

                //add paths to final path.

                GraphicsPath gp2 = new GraphicsPath();

                //add first path
                for (int i = 1; i < gp1[0].PointCount-2; i++)
                {
                    gp2.AddLine(gp1[0].PathPoints[i-1], gp1[0].PathPoints[i]);
                }

                //add other paths
                for (int j = 1; j < gp1Count; j++)
                {
                    gp2.AddLine(gp2.PathPoints[gp2.PointCount - 1], gp1[j].PathPoints[1]);

                    for (int i = 2; i < gp1[j].PointCount - 2; i++)
                    {
                        gp2.AddLine(gp1[j].PathPoints[i - 1], gp1[j].PathPoints[i]);
                    }
                }

                gp2.AddLine(gp2.PathPoints[gp2.PointCount - 1], new PointF(gp2.PathPoints[gp2.PointCount - 1].X,0));

                gp2.AddLine(gp2.PathPoints[gp2.PointCount - 1], gp2.PathPoints[0]);

                //gp2.CloseFigure();

                g.FillPath(Brushes.LightYellow, gp2);
                Common.Frm1.revenuePath = gp2;

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public bool isOverLeft(PointF pt)
        {
            try
            {
                if (isOverHandle(leftHandle, pt))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return false;
            }
        }

        public bool isOverRight(PointF pt)
        {
            try
            {
                if (isOverHandle(rightHandle, pt))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return false;
            }
        }

        public bool isOverHandle(RectangleF tempHandle,PointF pt)
        {
            try
            {
                PointF[] pts = new PointF[1];
                pts[0] = pt;

                Graphics g = Common.Frm1.mainScreen.GetGraphics();
                Matrix mt = g.Transform;

                g.Transform = Common.Frm1.graphTransform;
                g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);

                g.Transform = mt;

                if (tempHandle.Contains(pts[0]))
                {
                    return true;
                }
                else
                {
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return false;
            }
        }

        public void moveLeftHandleLeft()
        {
            try
            {
                selectionLeft -= Common.locationIncrement;

                validateSelectionLeft();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void moveLeftHandleRight()
        {
            try
            {
                
                selectionLeft += Common.locationIncrement;
                validateSelectionLeft();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void validateSelectionLeft()
        {
            try
            {
                Treatment t = Common.periods[Common.currentPeriod].treatment;

                if (selectionLeft < t.leftX) selectionLeft = t.leftX;
                if (selectionLeft > t.rightX) selectionLeft = t.rightX;

                if (selectionLeft >= selectionRight) selectionLeft = selectionRight - Common.locationIncrement;

                selectionLeft = (float) Math.Round(selectionLeft, 2);

                Common.Frm1.refreshScreen();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void moveRightHandleLeft()
        {
            try
            {
                selectionRight -= Common.locationIncrement;
                validateSelectionRight();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void moveRightHandleRight()
        {
            try
            {
                selectionRight += Common.locationIncrement;
                validateSelectionRight();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void validateSelectionRight()
        {
            try
            {
                Treatment t = Common.periods[Common.currentPeriod].treatment;

                if (selectionRight < t.leftX) selectionRight = t.leftX;
                if (selectionRight > t.rightX) selectionRight = t.rightX;

                if (selectionRight <= selectionLeft) selectionRight = selectionLeft + Common.locationIncrement;

                selectionRight = (float)Math.Round(selectionRight, 2);

                Common.Frm1.refreshScreen();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void calcProfit()
        {
            try
            {
                calcCost();
                calcRevenue();

                currentMinProfit = currentMinRevenue - currentSelectionCost;
                currentMaxProfit = currentMaxRevenue - currentSelectionCost;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void calcRevenue()
        {
            try
            {
                if (Common.myType != 1 && Common.myType != 2) return;
                //Player p = Common.playerlist[Common.myType];

                if (selectionRight <= 0)
                {
                    //left side only
                    currentMaxRevenue = calcRevenue2(selectionLeft, selectionRight);
                }
                else if (selectionLeft >= 0)
                {
                    //right side only
                    currentMaxRevenue = calcRevenue2(selectionLeft, selectionRight);
                }
                else
                {
                    //both sides
                    currentMaxRevenue = calcRevenue2(selectionLeft, 0);
                    currentMaxRevenue += calcRevenue2(0, selectionRight);
                }

                if (Common.myType == 1)
                    currentMinRevenue = currentMaxRevenue * Common.periods[Common.currentPeriod].treatment.blueRevenuePercent;
                else
                    currentMinRevenue = currentMaxRevenue * Common.periods[Common.currentPeriod].treatment.redRevenuePercent;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public float calcRevenue2(float x1, float x2)
        {
            try
            {
                float total = 0;
                PointF pt1 = Common.Frm1.getLinePointValue(x1);
                PointF pt2 = Common.Frm1.getLinePointValue(x2);
                float y1 = Common.Frm1.convertFromY(pt1.Y);
                float y2 = Common.Frm1.convertFromY(pt2.Y);

                //triangle
                total = ((y1 - y2) * (x2 - x1)) / 2;

                //sqaure
                total += y2 * (x2 - x1);
                return total;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        public void calcCost()
        {
            try
            {
                if (Common.myType != 1 && Common.myType != 2) return;
                //Player p = Common.playerlist[Common.myType];

                currentSelectionCost = calcCost2(selectionLeft, selectionRight);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public float calcCost2(float selectionLeft, float selectionRight)
        {
            try
            {
                if (Common.currentPeriod < 1) return 0;

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

                return (float)Math.Pow(totalRange, 2) * p.treatment.cost[Common.myType] / 2;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        public void takePeriodResults(ref string[] msgtokens, ref int nextToken)
        {
            try
            {
                currentRevenue = float.Parse(msgtokens[nextToken++]);
                currentCost = float.Parse(msgtokens[nextToken++]);
                currentProfit = float.Parse(msgtokens[nextToken++]);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);                
            }
        }

        public void takePeriodSelection(ref string[] msgtokens, ref int nextToken)
        {
            try
            {
                selectionLeft = float.Parse(msgtokens[nextToken++]);
                selectionRight = float.Parse(msgtokens[nextToken++]);                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
