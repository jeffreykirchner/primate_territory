using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;


namespace Client
{
    public partial class frm1 : Form
    {
        public Screen mainScreen;

        public Font f6 = new Font("Microsoft Sans Serif", 6, System.Drawing.FontStyle.Bold);
        public Font f8 = new Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold);
        public Font f10 = new Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);
        public Font f12 = new Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Bold);
        public Font f16 = new Font("Microsoft Sans Serif", 16, System.Drawing.FontStyle.Bold);
        public Font f18 = new Font("Microsoft Sans Serif", 18, System.Drawing.FontStyle.Bold);

        public StringFormat fmt = new StringFormat(); //center alignment
        public StringFormat fmtL = new StringFormat(); //left alignment
        public StringFormat fmtR = new StringFormat(); //right alignment

        public float xMargin = 110;
        public float yMargin = 75;
        public float otherMargin = 15;
        public float graphHeight = 0;
        public float graphWidth = 0;
        public float xTickHeight = 20;

        public Matrix graphTransform;
        public Matrix leftKeyTransform;
        public Matrix rightKeyTransform;
        public Matrix rightBeginKeyTransform;

        public Pen p3Black;
        public Pen p5Black;
        public Pen p5BlackDash;

        public Pen p3Red;
        public Pen p3Blue;
        public Pen p8Yellow;

        public Color p1Color = Color.FromArgb(175, 0, 0, 255);
        public Color p2Color = Color.FromArgb(175, 255, 0, 0);

        public bool mouseIsDown = false;
        public string currentHandle = "none";
        public int mouseDownStartX = 0;

        //show heighlight around roll over 
        public bool heighlightRevenue = false;
        public bool heighlightCost = false;
        public bool heighlightProfit = false;

        //key roll over bounding boxes
        public RectangleF revenueTextRectangle;
        public RectangleF costTextRectangle;
        public RectangleF profitTextRectangle;

        //heighleight graphics paths
        public GraphicsPath revenuePath;
        //public int revenuePathCount =0;
        public GraphicsPath costPath = null;
        public GraphicsPath profitPath = null;

        //key locations
        public PointF keyLocationLeft;
        public PointF keyLocationRightBegin;
        public PointF keyLocationRight;
        public PointF keyRevenueLocation;
        public PointF keyCostLocation;
        public PointF keyProfitLocation;


        public frm1()
        {
            InitializeComponent();
        }

        private void frm1_Load(object sender, EventArgs e)
        {
            try
            {
                mainScreen = new Screen(pnlMain, new Rectangle(0, 0, pnlMain.Width, pnlMain.Height));

                fmt.Alignment = StringAlignment.Center;
                fmtL.Alignment = StringAlignment.Near;
                fmtR.Alignment = StringAlignment.Far;

                setupPen(ref p3Black, Color.Black,3);
                setupPen(ref p3Red, Color.Black, 3);
                setupPen(ref p3Blue, p1Color, 3);
                setupPen(ref p3Red, p2Color, 3);
                setupPen(ref p8Yellow, Color.Yellow, 8);

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
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void setupPen(ref Pen p,Color c,int s)
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

        /// <summary>Start game loop</summary> 
        private void timer1_Tick(object sender, EventArgs e)
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

        ///<summary>If ALT+K are pressed kill the client,if ALT+Q are pressed bring up connection box</summary>          
        private void frm1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 

                if (e.Alt == true)
                {
                    if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.K))
                    {
                        if (MessageBox.Show("Close Program?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;

                        Common.closeClient();
                    }
                    else if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.Q))
                    {
                        Common.FrmConnect.Show();
                    }
                }
                else
                {
                    if (Common.myType != 1 && Common.myType != 2) return;
                    Player p = Common.playerlist[Common.myType];

                    if (Common.phase == "begin" && cmdSubmit.Visible == false) return;

                    if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.A))
                    {
                        p.moveLeftHandleLeft();
                        Common.calcSelectionProfit();
                    }
                    else if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.S))
                    {
                        p.moveLeftHandleRight();
                        Common.calcSelectionProfit();
                    }
                    else if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.K))
                    {
                        p.moveRightHandleLeft();
                        Common.calcSelectionProfit();
                    }
                    else if (Convert.ToInt32(e.KeyValue) == Convert.ToInt32(Keys.L))
                    {
                        p.moveRightHandleRight();
                        Common.calcSelectionProfit();
                    }
                }

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {  
                if(cmdSubmit.Visible)
                {
                    if(cmdSubmit.BackColor==SystemColors.Control)
                    {
                        cmdSubmit.BackColor = Color.FromArgb(192, 255, 192);
                    }
                    else
                    {
                        cmdSubmit.BackColor = SystemColors.Control;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void frm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!Common.clientClosing) e.Cancel = true;
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
                drawRevenue(g);   //if(Common.phase=="run") 
                drawCost(g);
                drawSelection(g);               
                drawTriangle(g);
                drawKey(g);
                drawHighlights(g);
                
                mainScreen.flip();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawHighlights(Graphics g)
        {
            try
            {
                Matrix mt = g.Transform;

                g.Transform = graphTransform;

                if (heighlightRevenue)
                {
                    if (revenuePath != null)
                        g.DrawPath(p8Yellow, revenuePath);
                }
                else if (heighlightCost)
                {
                    if (costPath != null)
                        g.DrawPath(p8Yellow, costPath);
                }
                else if (heighlightProfit)
                {
                    if (profitPath != null)
                        g.DrawPath(p8Yellow, profitPath);
                }

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
                if (Common.myType != 1 && Common.myType != 2) return;
                Player p = Common.playerlist[Common.myType];

                //if (p.currentSelectionCost == 0 || p.currentMaxRevenue == 0) return;
                
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

        public void drawCost(Graphics g)
        {
            try
            {
                if (Common.myType != 1 && Common.myType != 2) return;
                Player p = Common.playerlist[Common.myType];

                if (p.currentSelectionCost == 0 || p.currentMaxRevenue == 0) return;

                Matrix mt = g.Transform;

                g.Transform = graphTransform;

                p.drawCost(g);
                
                g.Transform = mt;

                //calc profit path
                profitPath = new GraphicsPath();

                //revenue line
                profitPath.AddLine(drawCostPoint(costPath.PathPoints[1]),
                                   drawCostPoint(revenuePath.PathPoints[1]));

                for(int i=2;i<=revenuePath.PointCount-4;i++)
                {
                    PointF pt1 = drawCostPoint(revenuePath.PathPoints[i]);
                    PointF pt2 = drawCostPoint(revenuePath.PathPoints[i+1]);

                    profitPath.AddLine(pt1,pt2);
                }

                //cost line(
                if(costPath.PathPoints.Length == 6)
                {
                    profitPath.AddLine(drawCostPoint(revenuePath.PathPoints[revenuePath.PointCount - 3]),
                                       drawCostPoint(costPath.PathPoints[3]));

                    profitPath.AddLine(drawCostPoint(costPath.PathPoints[3]),
                                       drawCostPoint(costPath.PathPoints[2]));

                    profitPath.AddLine(drawCostPoint(costPath.PathPoints[2]),
                                       drawCostPoint(costPath.PathPoints[1]));
                }
                else
                {
                    profitPath.AddLine(drawCostPoint(revenuePath.PathPoints[revenuePath.PointCount - 3]),
                                       drawCostPoint(costPath.PathPoints[2]));
                    profitPath.AddLine(drawCostPoint(costPath.PathPoints[2]),
                                       drawCostPoint(costPath.PathPoints[1]));
                }
               
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public PointF drawCostPoint(PointF pt1)
        {
            try
            {
                return new PointF(pt1.X,
                                  Math.Min(pt1.Y,getLinePointCost(convertFromX(pt1.X)).Y));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return new PointF(0, 0);
            }
        }

        public void drawKey(Graphics g)
        {
            try
            {
                if (Common.myType != 1 && Common.myType != 2) return;
                Player p = Common.playerlist[Common.myType];

                Matrix mt = g.Transform;

                //left side key
                g.Transform = leftKeyTransform;

                if((Common.phase != "begin" && Common.showPartnerInfo) || p.myID ==1)
                    drawKey2(g, Common.playerlist[1], new SolidBrush(p1Color), p3Blue);

                g.ResetTransform();

                //right side key
                if (Common.phase == "begin")
                    g.Transform = rightBeginKeyTransform;
                else
                    g.Transform = rightKeyTransform;

                if ((Common.phase != "begin" && Common.showPartnerInfo) || p.myID == 2)
                    drawKey2(g, Common.playerlist[2], new SolidBrush(p2Color), p3Red);

                g.ResetTransform();

                //bottom key left
                g.TranslateTransform(yMargin+105,  pnlMain.Height - 28);
                g.DrawString("Period:", f16, Brushes.Black, new PointF(0, 0), fmtR);
                g.DrawString(Common.currentPeriod.ToString(), f16, Brushes.Black, new PointF(0, 0), fmtL);

                g.ResetTransform();

                //bottom key right
                g.TranslateTransform(pnlMain.Width - 105, pnlMain.Height - 28);
                g.DrawString("Total Profit (¢):", f16, Brushes.Black, new PointF(0, 0), fmtR);
                g.DrawString(string.Format("{0:0.00}", Math.Round(Common.earnings, 2)), f16, Brushes.Black, new PointF(0, 0), fmtL);

                g.Transform = mt;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void drawKey2(Graphics g, Player p,SolidBrush brush,Pen pen)
        {
            try
            {
                g.DrawString("Total Revenue:", f18, brush, keyRevenueLocation, fmtR);
                if (Common.phase == "begin")
                    g.DrawString(string.Format("{0:0.00}", p.currentMinRevenue) + " to " + string.Format("{0:0.00}", Math.Round(p.currentMaxRevenue, 2)),
                                 f18,
                                 brush,
                                 keyRevenueLocation,
                                 fmtL);
                else
                    g.DrawString(string.Format("{0:0.00}", p.currentRevenue), f18, brush, keyRevenueLocation, fmtL);

                g.DrawString("- Total Cost:", f18, brush, keyCostLocation, fmtR);
                if (Common.phase == "begin")
                    g.DrawString(string.Format("{0:0.00}", p.currentSelectionCost), f18, brush, keyCostLocation, fmtL);
                else
                    g.DrawString(string.Format("{0:0.00}",p.currentCost), f18, brush, keyCostLocation, fmtL);

                //equals line 
                if(p.myID == Common.myType)
                    g.DrawString("Your Profit:", f18, brush, keyProfitLocation, fmtR);
                else
                    g.DrawString("Other's Profit:", f18, brush, keyProfitLocation, fmtR);

                if (Common.phase == "begin")
                {
                    g.DrawLine(pen, new PointF(-185, 57), new PointF(150, 57));
                    g.DrawString(string.Format("{0:0.00}",p.currentMinProfit) + " to " + string.Format("{0:0.00}", Math.Round(p.currentMaxProfit, 2)),
                                f18,
                                brush,
                                keyProfitLocation,
                                fmtL);
                }
                else
                {
                    g.DrawLine(pen, new PointF(-185, 57), new PointF(75, 57));
                    g.DrawString(string.Format("{0:0.00}", p.currentProfit), f18, brush, keyProfitLocation, fmtL);
                }
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
                g.Transform = graphTransform;

                if (Common.myType == 1)
                {
                    //if (Common.numberOfPlayers > 1)
                    if (Common.phase == "run") Common.playerlist[2].draw(g);
                    Common.playerlist[1].draw(g);
                }
                else
                {
                    Common.playerlist[2].draw(g);
                    if (Common.phase == "run") Common.playerlist[1].draw(g);                    
                }

                g.ResetTransform();
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

                Period p = Common.periods[Common.currentPeriod];
                Treatment t = p.treatment;
                
                //full value line
                g.DrawLine(p5Black,
                           t.pt1,
                           t.pt3);

                g.DrawLine(p5Black,
                          t.pt2,
                          t.pt3);

                //half value line
                g.DrawLine(p5BlackDash,
                           t.halfPt1,
                           t.halfPt3);

                g.DrawLine(p5BlackDash,
                          t.halfPt2,
                          t.halfPt3);

                //closing lines
                g.DrawLine(p5Black,
                           t.pt1,
                           new PointF(t.pt1.X,0));

                g.DrawLine(p5Black,
                          t.pt2,
                          new PointF(t.pt2.X, 0));

                g.ResetTransform();
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
                g.TranslateTransform(yMargin, pnlMain.Height - xMargin);
                //axis lines
                g.DrawLine(p3Black, 0, 0, 0, -graphHeight);
                g.DrawLine(p3Black, 0, 0, graphWidth, 0);

                //axis labels
                g.DrawString("Locations", f12, Brushes.DimGray, new PointF(graphWidth / 2, xMargin - 30), fmt);

                Matrix gt = g.Transform;
                g.TranslateTransform(-yMargin+10, -graphHeight/2);
                g.RotateTransform(-90);
                g.DrawString("Resource Value (¢)", f12, Brushes.DimGray, new PointF(0,0), fmt);
                g.Transform = gt;

                Treatment t = Common.periods[Common.currentPeriod].treatment;

                //x ticks
                float tempX = 0;
                float tempY = 0;
                float tempXIncrement = graphWidth / 10;
                float tempXValue = -t.scaleRange;
                float tempXValueIncrement = t.scaleRange / 5;

                for (int i=1;i<=11;i++)
                {
                    StringFormat temp_fmt;
                    if (i == 11)
                        temp_fmt = fmtR;
                    else if (i == 1)
                        temp_fmt = fmtL;
                    else
                        temp_fmt = fmt;

                    g.DrawLine(p3Black, new PointF(tempX, tempY), new PointF(tempX, tempY + xTickHeight));
                    g.DrawString(string.Format("{0:0.00}", Math.Round(tempXValue, 2)), f10, Brushes.Black, new PointF(tempX, tempY + 22), temp_fmt);
                    tempX += tempXIncrement;
                    tempXValue += tempXValueIncrement;
                }                              

                //y ticks
                tempX = 0;
                float tempYTickIncrement = graphHeight / 10;
                double tempYTickValue = 0;
                double tempYValueIncrement = t.scaleHeight / 10;
         

                for(int i=1;i<=11;i++)
                {
                    g.DrawLine(p3Black, new PointF(tempX, tempY), new PointF(tempX-7, tempY));
                    g.DrawString(string.Format("{0:0.00}", Math.Round(tempYTickValue, 2)), f10, Brushes.Black, new PointF(tempX - 10, tempY-8 ), fmtR);

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

        //return point on line where value hits
        public PointF getLinePointValue(float value)
        {
            try
            {
                
                Period p = Common.periods[Common.currentPeriod];
                Treatment t = p.treatment;

                if (value == p.treatment.middleX)
                {
                    //point in the middle
                    return p.treatment.pt3;
                }
                else if(value < p.treatment.middleX)
                {
                    //use left side line

                    return getPointIntersection(new PointF(convertToX(value,t.scaleRange), convertToY(0,t.scaleHeight)),
                                                new PointF(convertToX(value, t.scaleRange), convertToY(t.scaleHeight, t.scaleHeight)),
                                                p.treatment.pt1,
                                                p.treatment.pt3);
                }
                else
                {
                    //use right side line
                    return getPointIntersection(new PointF(convertToX(value, t.scaleRange),convertToY(0, t.scaleHeight)),
                                                new PointF(convertToX(value, t.scaleRange),convertToY(t.scaleHeight, t.scaleHeight)),
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

        //get point on cost line given value
        public PointF getLinePointCost(float value)
        {
            try
            {

                Period p = Common.periods[Common.currentPeriod];
                Treatment t = p.treatment;

                float costPercent = Common.playerlist[Common.myType].costPercent;

                if (value == p.treatment.middleX)
                {
                    //point in the middle
                    return new PointF(p.treatment.pt3.X, p.treatment.pt3.Y*costPercent);
                }
                else if (value < p.treatment.middleX)
                {
                    //use left side line
                    PointF pt1 = getPointIntersection(new PointF(convertToX(value, t.scaleRange), convertToY(0, t.scaleHeight)),
                                                new PointF(convertToX(value, t.scaleRange), convertToY(t.scaleHeight, t.scaleHeight)),
                                                p.treatment.pt1,
                                                p.treatment.pt3);

                    return new PointF(pt1.X, pt1.Y * costPercent);
                }
                else
                {
                    //use right side line
                    PointF pt1 = getPointIntersection(new PointF(convertToX(value, t.scaleRange), convertToY(0, t.scaleHeight)),
                                                new PointF(convertToX(value, t.scaleRange), convertToY(t.scaleHeight, t.scaleHeight)),
                                                p.treatment.pt2,
                                                p.treatment.pt3);
                    
                    return new PointF(pt1.X, pt1.Y * costPercent);
                }


            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return new Point(0, 0);
            }
        }

        public PointF getPointIntersection(PointF pt1,PointF pt2,PointF pt3, PointF pt4)
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

        public float convertToY(float tempValue,float graphMax)
        {
            try
            {
                if (Common.currentPeriod == 0) return 0f;
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

        public float convertToX(float tempValue, float range)
        { 
            try
            {
                if (Common.currentPeriod == 0) return 0;
                //Treatment t = Common.periods[Common.currentPeriod].treatment;

                float tempWidth = graphWidth/2;
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

        public float convertFromX(float tempX)
        {
            try
            {
                Treatment t = Common.periods[Common.currentPeriod].treatment;

                float tempWidth = graphWidth / 2;
                float range = t.scaleRange;

                float tempT = tempWidth / range;

                return tempX / tempT;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }
        public float convertFromY(float tempY)
        {
            try
            {
                Treatment t = Common.periods[Common.currentPeriod].treatment;

                float graphMin = 0;
                float graphMax = t.scaleHeight;

                float tempHeight = graphHeight;

                float unit = tempHeight / (graphMax - graphMin);                

                return tempY / -unit;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return 0;
            }
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if(mouseIsDown)
                {
                    if (Common.showInstructions && !Common.Frm1.cmdSubmit.Visible) return;

                    handle_pnlMain_MouseMove(e.Location);
                }
                else
                {
                    checkIsOverKey(e.Location);
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);                
            }
        }

        public void handle_pnlMain_MouseMove(PointF mousePt)
        {
            try
            {
                if (Common.myType != 1 && Common.myType != 2) return;
                Player p = Common.playerlist[Common.myType];

                PointF[] pts = new PointF[1];
                pts[0] = mousePt;

                Graphics g = Common.Frm1.mainScreen.GetGraphics();
                Matrix mt = g.Transform;

                g.Transform = Common.Frm1.graphTransform;
                g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);

                g.Transform = mt;

                if (currentHandle == "left")
                {
                    p.selectionLeft = convertFromX(pts[0].X);
                    p.validateSelectionLeft();

                    Common.calcSelectionProfit();
                }
                else if (currentHandle == "right")
                {
                    p.selectionRight = convertFromX(pts[0].X);
                    p.validateSelectionRight();

                    Common.calcSelectionProfit();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }


        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Common.myType != 1 && Common.myType != 2) return;

                if (Common.phase == "begin" && cmdSubmit.Visible == false) return;

                Player p = Common.playerlist[Common.myType];
                if (p.isOverLeft(e.Location))
                {
                    mouseIsDown = true;
                    Cursor = Cursors.VSplit;
                    currentHandle = "left";
                    mouseDownStartX = e.X;
                }
                else if (p.isOverRight(e.Location))
                {
                    mouseIsDown = true;
                    Cursor = Cursors.VSplit;
                    currentHandle = "right";
                    mouseDownStartX = e.X;
                }
                else
                {
                    mouseIsDown = false;
                    Cursor = Cursors.Default;
                    currentHandle = "none";
                    mouseDownStartX = 0;
                }                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                mouseIsDown = false;
                Cursor = Cursors.Default;
                currentHandle = "none";
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void frm1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                mouseIsDown = false;
                Cursor = Cursors.Default;
                currentHandle = "none";
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cmdSubmit.Visible) return;

                Player p = Common.playerlist[Common.myType];

                if (Common.showInstructions)
                {
                    if(p.selectionLeft == Common.instructionPlayerLeft && p.selectionRight== Common.instructionPlayerRight)
                    {
                        //pnlMain.Enabled = false;
                        cmdSubmit.Visible = false;
                        Common.FrmInstructions.pagesDone[2] = true;
                        txtMessages.Text = "";
                    }
                }
                else
                {
                    pnlMain.Enabled = false;

                    string str = "";                    

                    str = p.selectionLeft.ToString() + ";";
                    str += p.selectionRight.ToString() + ";";

                    txtMessages.Text = "Waiting for others.";
                    cmdSubmit.Visible = false;

                    Common.FrmClient.SC.sendMessage("01", str);
                }
                
                
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                timer3.Enabled = false;

                string str = "";

                Player p = Common.playerlist[Common.myType];

                str = p.selectionLeft.ToString() + ";";
                str += p.selectionRight.ToString() + ";";

                Common.FrmClient.SC.sendMessage("02", str);
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {
                Player partner;
                Player p = Common.playerlist[Common.myType];

                if (Common.myType == 1)
                {
                    partner = Common.playerlist[2];
                }
                else
                {
                    partner = Common.playerlist[1];
                }

                if (Common.FrmNames != null)
                {
                    if (Common.FrmNames.Visible)
                    {
                        int tempN = Rand.rand(20, 5);
                        string s = "";
                        for (int i = 1; (i <= tempN); i++)
                        {

                            s += (char)Rand.rand(122, 60);
                        }

                        Common.FrmNames.txtName.Text = s;

                        Common.FrmNames.txtIDNumber.Text = Rand.rand(100000, 1).ToString();

                        Common.FrmNames.cmdSubmit.PerformClick();

                        Common.testMode = false;
                        timer2.Enabled = false;
                        return;
                    }
                }


                if (Common.FrmInstructions != null && Common.FrmInstructions.Visible)
                {
                    if(Common.FrmInstructions.cmdStart.Visible)
                    {
                        Common.FrmInstructions.cmdStart.PerformClick();
                    }
                    else if (Common.FrmInstructions.pagesDone[Common.FrmInstructions.currentInstruction])
                    {
                        Common.FrmInstructions.cmdNext.PerformClick();
                        return;
                    }
                    else
                    {
                        switch (Common.FrmInstructions.currentInstruction)
                        {
                            case 1:
                                break;
                            case 2:
                                if (p.selectionLeft != Common.instructionPlayerLeft)
                                {
                                    p.selectionLeft = Common.instructionPlayerLeft;
                                    p.selectionRight = Common.instructionPlayerRight;
                                }
                                else
                                {
                                    cmdSubmit.PerformClick();
                                }
                               
                                break;
                            case 3:
                                if (Common.myType == 1)
                                    checkIsOverKey(keyLocationLeft);
                                else
                                    checkIsOverKey(keyLocationRight);
                                break;
                            case 4:
                                
                                break;
                            case 5:
                               
                                break;
                            case 6:
                                
                                break;
                            case 7:
                                
                                break;
                            case 8:
                                
                                break;

                        }
                    }

                    return;
                }

                //move handle
                if (Rand.rand(2, 1) == 1)
                    currentHandle = "left";
                else
                    currentHandle = "right";

                handle_pnlMain_MouseMove(new PointF(Rand.rand(pnlMain.Width, 0),0));

                currentHandle = "none";

                if(cmdSubmit.Visible == true)
                {
                    cmdSubmit.PerformClick();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void setupHeighlightBoundingBoxes()
        {
            try
            {
               
                keyLocationLeft = new PointF(yMargin + 220, otherMargin);
                keyLocationRightBegin = new PointF(pnlMain.Width - 175, otherMargin);
                keyLocationRight = new PointF(pnlMain.Width - 105, otherMargin);

                keyRevenueLocation = new PointF(0, 0);
                keyCostLocation = new PointF(0, 25);
                keyProfitLocation = new PointF(0, 60);

                int w = 350;
                int h = 20;

                revenueTextRectangle = new RectangleF(keyRevenueLocation.X - w/2, keyRevenueLocation.Y, w, h);
                costTextRectangle = new RectangleF(keyCostLocation.X - w / 2, keyCostLocation.Y, w, h);
                profitTextRectangle = new RectangleF(keyProfitLocation.X - w / 2, keyProfitLocation.Y, w, h);

                Graphics g = mainScreen.GetGraphics();
                g.TranslateTransform(keyLocationLeft.X, keyLocationLeft.Y);
                leftKeyTransform = g.Transform;
                g.ResetTransform();

                g.TranslateTransform(keyLocationRightBegin.X, keyLocationRightBegin.Y);
                rightBeginKeyTransform = g.Transform;
                g.ResetTransform();

                g.TranslateTransform(keyLocationRight.X, keyLocationRight.Y);
                rightKeyTransform = g.Transform;
                g.ResetTransform();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public void checkIsOverKey(PointF pt)
        {           
            try
            {
                heighlightRevenue = false;
                heighlightCost = false;
                heighlightProfit = false;

                PointF[] pts = new PointF[1];
                pts[0] = pt;

                Graphics g = Common.Frm1.mainScreen.GetGraphics();
                Matrix mt = g.Transform;

                if (Common.myType == 1)
                {
                    g.Transform = Common.Frm1.leftKeyTransform;
                }
                else
                {
                    if(Common.phase=="begin")
                    {
                        g.Transform = Common.Frm1.rightBeginKeyTransform;
                    }
                    else
                    {
                        g.Transform = Common.Frm1.rightKeyTransform;
                    }
                }

                g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);

                if (revenueTextRectangle.Contains(pts[0]))
                    heighlightRevenue = true;
                else if (costTextRectangle.Contains(pts[0]))
                    heighlightCost = true;
                else if (profitTextRectangle.Contains(pts[0]))
                    heighlightProfit = true;                

                g.Transform = mt;

                if (heighlightRevenue || heighlightCost || heighlightProfit)
                {
                    if (Common.showInstructions && Common.FrmInstructions.currentInstruction == 3)
                    {
                        Common.FrmInstructions.pagesDone[3] = true;
                    }

                    refreshScreen();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

    }
}
