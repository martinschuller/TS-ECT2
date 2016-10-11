namespace TS_ECT2
{
    partial class Simulation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridSimulationFunctions = new DevExpress.XtraGrid.GridControl();
            this.gridViewSimulationFunctions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.FunctionName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OKVar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Group = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnIgnore = new DevExpress.XtraEditors.SimpleButton();
            this.btnAbort = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulationFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSimulationFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridSimulationFunctions
            // 
            this.gridSimulationFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSimulationFunctions.Location = new System.Drawing.Point(0, 0);
            this.gridSimulationFunctions.MainView = this.gridViewSimulationFunctions;
            this.gridSimulationFunctions.Name = "gridSimulationFunctions";
            this.gridSimulationFunctions.Size = new System.Drawing.Size(369, 346);
            this.gridSimulationFunctions.TabIndex = 1;
            this.gridSimulationFunctions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSimulationFunctions});
            // 
            // gridViewSimulationFunctions
            // 
            this.gridViewSimulationFunctions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.FunctionName,
            this.OKVar,
            this.Group});
            this.gridViewSimulationFunctions.GridControl = this.gridSimulationFunctions;
            this.gridViewSimulationFunctions.Name = "gridViewSimulationFunctions";
            // 
            // FunctionName
            // 
            this.FunctionName.Caption = "Name";
            this.FunctionName.FieldName = "Name";
            this.FunctionName.Name = "FunctionName";
            this.FunctionName.OptionsColumn.AllowEdit = false;
            this.FunctionName.Visible = true;
            this.FunctionName.VisibleIndex = 0;
            // 
            // OKVar
            // 
            this.OKVar.Caption = "gridColumn1";
            this.OKVar.FieldName = "OKVar";
            this.OKVar.Name = "OKVar";
            // 
            // Group
            // 
            this.Group.Caption = "gridColumn1";
            this.Group.FieldName = "Group";
            this.Group.Name = "Group";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridSimulationFunctions);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnIgnore);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnAbort);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(369, 423);
            this.splitContainerControl1.SplitterPosition = 346;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(207, 10);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(150, 50);
            this.btnIgnore.TabIndex = 4;
            this.btnIgnore.Text = "Ignore";
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(51, 10);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(150, 50);
            this.btnAbort.TabIndex = 5;
            this.btnAbort.Text = "Abort";
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 423);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "Simulation";
            this.Text = "Simulation";
            this.Load += new System.EventHandler(this.Simulation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSimulationFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSimulationFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSimulationFunctions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSimulationFunctions;
        private DevExpress.XtraGrid.Columns.GridColumn FunctionName;
        private DevExpress.XtraGrid.Columns.GridColumn OKVar;
        private DevExpress.XtraGrid.Columns.GridColumn Group;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnIgnore;
        private DevExpress.XtraEditors.SimpleButton btnAbort;
    }
}