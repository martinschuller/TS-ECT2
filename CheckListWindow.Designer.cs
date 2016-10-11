namespace TS_ECT2
{
    partial class CheckListWindow
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
            this.checklListGridControl = new DevExpress.XtraGrid.GridControl();
            this.CheckListGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.checklListGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckListGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checklListGridControl
            // 
            this.checklListGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checklListGridControl.Location = new System.Drawing.Point(0, 0);
            this.checklListGridControl.MainView = this.CheckListGridView;
            this.checklListGridControl.Name = "checklListGridControl";
            this.checklListGridControl.Size = new System.Drawing.Size(420, 427);
            this.checklListGridControl.TabIndex = 0;
            this.checklListGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.CheckListGridView});
            // 
            // CheckListGridView
            // 
            this.CheckListGridView.GridControl = this.checklListGridControl;
            this.CheckListGridView.Name = "CheckListGridView";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.checklListGridControl);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.label1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(420, 481);
            this.splitContainerControl1.SplitterPosition = 427;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // CheckListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 481);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "CheckListWindow";
            this.Text = "CheckListWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CheckListWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.checklListGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckListGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl checklListGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView CheckListGridView;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.Label label1;
    }
}