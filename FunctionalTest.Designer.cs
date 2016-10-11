namespace TS_ECT2
{
    partial class FunctionalTest
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridTestFunctions = new DevExpress.XtraGrid.GridControl();
            this.gridViewTestFunctions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Activated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTestFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTestFunctions)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridTestFunctions);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(886, 509);
            this.splitContainerControl1.SplitterPosition = 417;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridTestFunctions
            // 
            this.gridTestFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gridTestFunctions.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridTestFunctions.Location = new System.Drawing.Point(0, 0);
            this.gridTestFunctions.MainView = this.gridViewTestFunctions;
            this.gridTestFunctions.Name = "gridTestFunctions";
            this.gridTestFunctions.Size = new System.Drawing.Size(886, 417);
            this.gridTestFunctions.TabIndex = 28;
            this.gridTestFunctions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTestFunctions});
            this.gridTestFunctions.Load += new System.EventHandler(this.gridTestFunctions_Load);
            // 
            // gridViewTestFunctions
            // 
            this.gridViewTestFunctions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn12,
            this.Activated,
            this.gridColumn13});
            this.gridViewTestFunctions.GridControl = this.gridTestFunctions;
            this.gridViewTestFunctions.Name = "gridViewTestFunctions";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Funktion";
            this.gridColumn11.FieldName = "Testfunction";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Beschreibung";
            this.gridColumn12.FieldName = "Testdescription";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 1;
            // 
            // Activated
            // 
            this.Activated.Caption = "Activated";
            this.Activated.FieldName = "Activated";
            this.Activated.Name = "Activated";
            this.Activated.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Ausführen";
            this.gridColumn13.FieldName = "Perform";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            // 
            // FunctionalTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 509);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FunctionalTest";
            this.Text = "FunctionalTest";
            this.Load += new System.EventHandler(this.FunctionalTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTestFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTestFunctions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridTestFunctions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTestFunctions;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn Activated;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
    }
}