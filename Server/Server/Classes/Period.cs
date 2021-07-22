﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Period
    {
        public Treatment treatment = new Treatment();
        public int treatmentIndex;

        public void setup(string[] msgtokens)
        {
            try
            {
                int nextToken = 0;
                this.treatmentIndex = int.Parse(msgtokens[nextToken++]);
                treatment = Common.treaments[treatmentIndex];
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

                str = treatmentIndex + ";";

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
