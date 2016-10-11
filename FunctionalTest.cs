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
    public partial class FunctionalTest : Form
    {
        GlobalVar.GlobalData kmGlobalData = new GlobalVar.GlobalData();
        Timer functionalTestTimer = new Timer();

        private CellColorHelper _CellColorHelper;

        public FunctionalTest()
        {
            InitializeComponent();
            _CellColorHelper = new CellColorHelper(gridViewTestFunctions);
        }

        public FunctionalTest(GlobalVar.GlobalData kmGlobalData)
        {
            InitializeComponent();
            this.kmGlobalData = kmGlobalData;
            _CellColorHelper = new CellColorHelper(gridViewTestFunctions);
        }

        private void FunctionalTest_Load(object sender, EventArgs e)
        {
            gridTestFunctions.DataSource = kmGlobalData.liTeststepList;
            gridViewTestFunctions.Columns["Activated"].FilterInfo = new ColumnFilterInfo("[Activated] LIKE TRUE");
            functionalTestTimer.Interval = 200;
            functionalTestTimer.Tick += new EventHandler(FunctionalTestTimer_Tick);
            functionalTestTimer.Start();
        }
        private void FunctionalTestTimer_Tick(object sender, EventArgs e)
        {
            gridTestFunctions.RefreshDataSource();
        }

        public void UpdateGridColors()
        {
            for (int i = 0; i <= gridViewTestFunctions.RowCount - 1; i++)
            {
                string rowValue = gridViewTestFunctions.GetRowCellValue(i, "Testfunction").ToString();
                if (rowValue == kmGlobalData.liTeststepList[kmGlobalData.actTestProcedure].Testfunction)
                {
                    if ((!kmGlobalData.breakTestProgram) && (!kmGlobalData.repeatTestFunction) && (!kmGlobalData.ignoreTestFailure))
                    {
                        kmGlobalData.liTeststepList[kmGlobalData.actTestProcedure].Testresult = 1;
                        _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.LightGreen);
                    }
                    else
                    {
                        kmGlobalData.liTeststepList[kmGlobalData.actTestProcedure].Testresult = 2;
                        _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.OrangeRed);
                    }
                }
            }
        }

        private void gridTestFunctions_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= gridViewTestFunctions.RowCount - 1; i++)
            {
                foreach (GlobalVar.Teststep teststep in kmGlobalData.liTeststepList)
                {
                    string rowValue = gridViewTestFunctions.GetRowCellValue(i, "Testfunction").ToString();
                    if (rowValue == teststep.Testfunction)
                    {
                        if (teststep.Testresult == 1)
                            _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.LightGreen);
                        else if (teststep.Testresult == 2)
                            _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.OrangeRed);
                    }
                }
            }
        }
    }
}
