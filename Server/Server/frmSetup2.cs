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
    public partial class frmSetup2 : Form
    {

        public frmSetup2()
        {
            InitializeComponent();
        }

        private void frmSetup2_Load(object sender, EventArgs e)
        {
            try
            {
                
                dgView.RowCount = Common.numberOfPeriods;
                DataGridViewComboBoxColumn dgvcbc =(DataGridViewComboBoxColumn) dgView.Columns[1];
                dgvcbc.Items.Clear();

                int c = int.Parse(INI.getINI(Common.sfile, "treatments", "count"));

                for(int i=1;i<=c;i++)
                {
                    string[] msgtokens = INI.getINI(Common.sfile, "treatments", i.ToString()).Split(';');
                    dgvcbc.Items.Add(i + " - " + msgtokens[0]);
                }

                for (int i = 1; i <= dgView.RowCount; i++)
                {
                    dgView[0, i-1].Value = i;

                    string[] msgtokens = INI.getINI(Common.sfile, "periods", i.ToString()).Split(';');

                    if(msgtokens.Count()>1)
                    {
                        int index = int.Parse(msgtokens[0]);

                        if(dgvcbc.Items.Count>= index)
                        {
                            //DataGridViewComboBoxCell dgvcvc = (DataGridViewComboBoxCell)dgView[1, i-1];

                            dgView[1, i - 1].Value = dgvcbc.Items[index-1];
                        }
                    }

                    
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
                Cursor = Cursors.WaitCursor;

                for (int i=1;i<=dgView.RowCount;i++)
                {
                    string outstr = "";

                    string[] msgtokens = dgView[1, i - 1].Value.ToString().Split(new string[1] { " - " },StringSplitOptions.RemoveEmptyEntries);

                    outstr = msgtokens[0] + ";";

                    INI.writeINI(Common.sfile, "Periods", i.ToString(), outstr);
                }

                Cursor = Cursors.Default;

                this.Close();
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                int r = dgView.CurrentCell.RowIndex;

                for(int i=r+1;i<dgView.RowCount;i++)
                {
                    dgView[1, i].Value = dgView[1, r].Value;
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                EventLog.appEventLog_Write("error :", ex);
            }
        }
    }
}
