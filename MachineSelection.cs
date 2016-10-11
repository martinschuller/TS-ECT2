using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KraussMaffeiData;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Resources;

namespace TS_ECT2
{
        
    public partial class MachineSelection : DevExpress.XtraEditors.XtraForm
    {
        private GlobalVar.GlobalData kmGlobalData;
        private int selectedMachine;
        private ResourceManager rm;

        public GlobalVar.GlobalData KMGlobalData
        {
            get { return kmGlobalData ; }
            set { kmGlobalData = value; }
        }

        public int SelectedMachine
        {
            get { return selectedMachine; }
            set { selectedMachine = value; }
        }

        public MachineSelection(GlobalVar.GlobalData kmGlobalData, ResourceManager rm)
        { 
            this.kmGlobalData = kmGlobalData;
            this.rm = rm;
            InitializeComponent();
            gridControlMachineSelection.DataSource = kmGlobalData.listTestModules;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        private void DoRowClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                SelectedMachine = info.RowHandle;
                
            }
            else
            {
                SelectedMachine = -1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedMachine = -1;
            Close();
        }

        private void gridMachineSelection_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowClick(view, pt);

        }

        private void MachineSelection_Load(object sender, EventArgs e)
        {   
            this.Text =rm.GetString("TitleMachineSelectionString");
            btnCancel.Text = kmGlobalData.abortDialogString;
            gridMachineSelection.Columns[0].Caption  = rm.GetString("MachineString");
        }

        private void gridMachineSelection_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt);
        }

        private  void DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                SelectedMachine = info.RowHandle;

            }
            else
            {
                SelectedMachine = -1;
            }
            Close();
        }
    }
}