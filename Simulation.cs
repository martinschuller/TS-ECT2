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
    public partial class Simulation : Form
    {

        private GlobalVar.GlobalData kmGlobalData = new GlobalVar.GlobalData();
        
        Timer simulationTimer = new Timer();
        private int group;

        public Simulation()
        {
            InitializeComponent();
        }

        public GlobalVar.GlobalData KMGlobalData
        {
            get { return kmGlobalData; }
            set { kmGlobalData = value; }
        }

        public int SimulationGroup
        {
            get { return group; }
            set
            {
                group = value;
                gridViewSimulationFunctions.Columns["Group"].FilterInfo = new ColumnFilterInfo("[Group] LIKE " + group.ToString());
            }
        }

        public Simulation(GlobalVar.GlobalData kmGlobalData)
        {
            InitializeComponent();
            this.kmGlobalData = kmGlobalData;
        }

        private void Simulation_Load(object sender, EventArgs e)
        {
            gridSimulationFunctions.DataSource = kmGlobalData.checkList;
            gridViewSimulationFunctions.Columns["OKVar"].FilterInfo = new ColumnFilterInfo("[OKVar] LIKE FALSE");
            gridViewSimulationFunctions.Columns["Group"].FilterInfo = new ColumnFilterInfo("[Group] LIKE " + group.ToString());
            simulationTimer.Interval = 200;
            simulationTimer.Tick += new EventHandler(SimulationTimer_Tick);
            simulationTimer.Start();
        }

        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            gridSimulationFunctions.RefreshDataSource();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            kmGlobalData.breakTestProgram = true;
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            kmGlobalData.ignoreTestFailure = true;
        }
    }
}
