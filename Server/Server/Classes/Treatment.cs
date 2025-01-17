﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Server
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

        public float[] cost = new float[3];

        public float blueRevenuePercent;         //percent of revenue going to blue
        public float redRevenuePercent;          //percent of revenue going to red

        public float scaleRange = 0;                      //left and right side scale
        public float scaleHeight = 0;                     //height of the scale

        //drawing points
        public PointF pt1;   //left point
        public PointF pt2;   //right point
        public PointF pt3;   //middle point, full value

        public PointF halfPt1Blue;   //middle point blue
        public PointF halfPt2Blue;   //middle point blue
        public PointF halfPt3Blue;   //middle point blue

        public PointF halfPt1Red;   //middle point red
        public PointF halfPt2Red;   //middle point red
        public PointF halfPt3Red;   //middle point red

        public void fromString(string str)
        {
            try
            {
                string[] msgtokens = str.Split(';');
                int nextToken = 0;

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

                pt1 = new PointF(Common.FrmServer.convertToX(leftX, scaleRange),
                                 Common.FrmServer.convertToY(leftY, scaleHeight));

                pt2 = new PointF(Common.FrmServer.convertToX(rightX, scaleRange),
                                 Common.FrmServer.convertToY(rightY, scaleHeight));

                pt3 = new PointF(Common.FrmServer.convertToX(middleX,scaleRange),
                                 Common.FrmServer.convertToY(middleY, scaleHeight));

                halfPt1Blue = new PointF(Common.FrmServer.convertToX(leftX, scaleRange),
                                    Common.FrmServer.convertToY(leftY * blueRevenuePercent, scaleHeight));

                halfPt2Blue = new PointF(Common.FrmServer.convertToX(rightX, scaleRange),
                                    Common.FrmServer.convertToY(rightY * blueRevenuePercent, scaleHeight));

                halfPt3Blue = new PointF(Common.FrmServer.convertToX(middleX, scaleRange),
                                     Common.FrmServer.convertToY(middleY * blueRevenuePercent, scaleHeight));

                halfPt1Red = new PointF(Common.FrmServer.convertToX(leftX, scaleRange),
                                    Common.FrmServer.convertToY(leftY * redRevenuePercent, scaleHeight));

                halfPt2Red = new PointF(Common.FrmServer.convertToX(rightX, scaleRange),
                                    Common.FrmServer.convertToY(rightY * redRevenuePercent, scaleHeight));

                halfPt3Red = new PointF(Common.FrmServer.convertToX(middleX, scaleRange),
                                     Common.FrmServer.convertToY(middleY * redRevenuePercent, scaleHeight));
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        public string toString()
        {
            try
            {
                string str = "";

                str = name + ";";
                str += leftX.ToString() + ";";
                str += leftY.ToString() + ";";
                str += rightX.ToString() + ";";
                str += rightY.ToString() + ";";
                str += middleX.ToString() + ";";
                str += middleY.ToString() + ";";
                str += cost[1].ToString() + ";";
                str += cost[2].ToString() + ";";
                str += blueRevenuePercent.ToString() + ";";
                str += redRevenuePercent.ToString() + ";";
                str += scaleRange.ToString() + ";";
                str += scaleHeight.ToString() + ";";

                return str;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
                return "";
            }
        }

    }
}
