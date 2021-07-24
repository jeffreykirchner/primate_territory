using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Period
    {
        public Treatment treatment = new Treatment();
        public int treatmentIndex;

        public void fromString(ref string[] msgtokens,ref int nextToken)
        {
            try
            {
                treatmentIndex = int.Parse(msgtokens[nextToken++]);

                treatment = Common.treaments[treatmentIndex];
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
