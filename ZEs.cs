using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KraussMaffeiData;
using DevExpress.XtraGrid.Columns;

namespace TS_ECT2
{
    public partial class ZEs : Form
    {
        private GlobalVar.GlobalData kmGlobalData = new GlobalVar.GlobalData();
        KraussMaffeiFunctions kmFunctions = new KraussMaffeiFunctions();
        public List<string> oldaszZEs = new List<string>();
        public ZEs()
        {
            InitializeComponent();
        }

        public ZEs(GlobalVar.GlobalData kmGlobalData, KraussMaffeiFunctions kmFunctions)
        {
            InitializeComponent();
            this.kmGlobalData = kmGlobalData;
        }

        private void ZEs_Load(object sender, EventArgs e)
        {
            oldaszZEs.Clear();
            foreach (string ze in kmGlobalData.aszZEs)
            {
                oldaszZEs.Add(ze);
            }

            txtOrdernumber.Text = kmGlobalData.szOrderNumber;
            gridControlZEs.DataSource = kmGlobalData.aszZEs;
            TopMost = true;
            Invalidate();

        }

        private void btnAddZE_Click(object sender, EventArgs e)
        {
            if (txtEditZE.Text != "")
            {
                bool exists = false;
                foreach (string ze in kmGlobalData.aszZEs)
                {
                    if (ze == txtEditZE.Text)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    kmGlobalData.aszZEs.Add(txtEditZE.Text);
                    gridControlZEs.RefreshDataSource();
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string cellValue = gridViewZEs.GetFocusedDisplayText();
            kmGlobalData.aszZEs.Remove(cellValue);
            gridControlZEs.RefreshDataSource();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtOperator.Text == "")
                MessageBox.Show("No Operator selected");
            else
            {
                kmGlobalData.szOperator = txtOperator.Text;
                Close();
            }
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            kmGlobalData.aszZEs.Clear();
            foreach (string ze in oldaszZEs)
            {
                kmGlobalData.aszZEs.Add(ze);
            }
            Close();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
