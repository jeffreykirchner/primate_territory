using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmSetup3 : Form
    {
        public frmSetup3()
        {
            InitializeComponent();
        }

        private void frmSetup3_Load(object sender, EventArgs e)
        {
            try
            {
                dgView.RowCount = int.Parse(INI.getINI(Common.sfile, "treatments", "count"));

                for(int i=1;i<=dgView.RowCount; i++)
                {
                    loadRow(i - 1);
                }

                txtCount.Text = dgView.RowCount.ToString();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void loadRow(int rowIndex)
        {
            try
            {
                string[] msgtokens = INI.getINI(Common.sfile, "treatments", (rowIndex+1).ToString()).Split(';');

                if (msgtokens.Length < 5) return;

                for (int j = 1; j <= dgView.ColumnCount; j++)
                {
                    dgView[j - 1, rowIndex].Value = msgtokens[j - 1].ToString();
                }
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                INI.writeINI(Common.sfile, "treatments", "count", dgView.RowCount.ToString());

                for (int i = 1; i <= dgView.RowCount; i++)
                {
                    string str = "";

                    for (int j = 1; j <= dgView.ColumnCount; j++)
                    {
                        str += dgView[j - 1, i - 1].Value + ";";
                    }

                    INI.writeINI(Common.sfile, "treatments", i.ToString(), str);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgView.RowCount == 1) return;

                dgView.RowCount--;

                txtCount.Text = dgView.RowCount.ToString();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdPlus_Click(object sender, EventArgs e)
        {
            try
            {
                dgView.RowCount++;
                txtCount.Text = dgView.RowCount.ToString();

                loadRow(dgView.RowCount - 1);

            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
