using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Client
{
    public class Treatment
    {
        public string name;
        public float leftX;
        public float leftY;
        public float rightX;
        public float rightY;
        public float middleX;
        public float middleY;

        public float blueRevenuePercent;         //percent of revenue going to blue
        public float redRevenuePercent;          //percent of revenue going to red

        public float[] cost = new float[3];

        //drawing points
        public PointF pt1;   //left point
        public PointF pt2;   //right point
        public PointF pt3;   //middle point, full value

        public PointF halfPt1;   //middle point, half value
        public PointF halfPt2;   //middle point, half value
        public PointF halfPt3;   //middle point, half value

        public float scaleRange = 0;                      //left and right side scale
        public float scaleHeight = 0;                     //height of the scale

        public void fromString(ref string[] msgtokens,ref int nextToken, int myType)
        {
            try
            {
                name = msgtokens[nextToken++];
                leftX = float.Parse(msgtokens[nextToken++]);
                leftY = float.Parse(msgtokens[nextToken++]);
                rightX = float.Parse(msgtokens[nextToken++]);
                rightY = float.Parse(msgtokens[nextToken++]);
                middleX = float.Parse(msgtokens[nextToken++]);
                middleY = float.Parse(msgtokens[nextToken++]);

                cost[1] = float.Parse(msgtokens[nextToken++]);
                cost[2] = float.Parse(msgtokens[nextToken++]);

                blueRevenuePercent = float.Parse(msgtokens[nextToken++]);
                redRevenuePercent = float.Parse(msgtokens[nextToken++]);

                scaleRange = float.Parse(msgtokens[nextToken++]);
                scaleHeight = float.Parse(msgtokens[nextToken++]);

                pt1 = new PointF(Common.Frm1.convertToX(leftX, scaleRange),
                                 Common.Frm1.convertToY(leftY, scaleHeight));

                pt2 = new PointF(Common.Frm1.convertToX(rightX, scaleRange),
                                 Common.Frm1.convertToY(rightY, scaleHeight));

                pt3 = new PointF(Common.Frm1.convertToX(middleX, scaleRange),
                                 Common.Frm1.convertToY(middleY, scaleHeight));

                halfPt1 = new PointF(Common.Frm1.convertToX(leftX, scaleRange),
                                    Common.Frm1.convertToY(leftY / 2, scaleHeight));

                halfPt2 = new PointF(Common.Frm1.convertToX(rightX, scaleRange),
                                    Common.Frm1.convertToY(rightY / 2, scaleHeight));

                if(myType == 1)
                {
                    //blue
                    halfPt3 = new PointF(Common.Frm1.convertToX(middleX, scaleRange),
                                         Common.Frm1.convertToY(middleY * blueRevenuePercent, scaleHeight));
                }
                else
                {
                    //red
                    halfPt3 = new PointF(Common.Frm1.convertToX(middleX, scaleRange),
                                         Common.Frm1.convertToY(middleY * redRevenuePercent, scaleHeight));
                }
                

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
