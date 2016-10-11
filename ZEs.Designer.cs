namespace TS_ECT2
{
    partial class ZEs
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
            this.txtEditZE = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnAddZE = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtOperator = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrdernumber = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControlZEs = new DevExpress.XtraGrid.GridControl();
            this.gridViewZEs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAbort = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditZE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrdernumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlZEs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewZEs)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEditZE
            // 
            this.txtEditZE.Location = new System.Drawing.Point(66, 8);
            this.txtEditZE.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEditZE.Name = "txtEditZE";
            this.txtEditZE.Size = new System.Drawing.Size(323, 26);
            this.txtEditZE.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 11);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(18, 19);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "ZE";
            // 
            // btnAddZE
            // 
            this.btnAddZE.Location = new System.Drawing.Point(13, 54);
            this.btnAddZE.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddZE.Name = "btnAddZE";
            this.btnAddZE.Size = new System.Drawing.Size(176, 62);
            this.btnAddZE.TabIndex = 3;
            this.btnAddZE.Text = "Hinzufügen";
            this.btnAddZE.Click += new System.EventHandler(this.btnAddZE_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(213, 54);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(176, 62);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Löschen";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.AlwaysScrollActiveControlIntoView = false;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.txtEditZE);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnAddZE);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnAbort);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnDelete);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnOK);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(625, 679);
            this.splitContainerControl1.SplitterPosition = 435;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtOperator);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtOrdernumber);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridControlZEs);
            this.splitContainer1.Size = new System.Drawing.Size(625, 435);
            this.splitContainer1.SplitterDistance = 149;
            this.splitContainer1.TabIndex = 2;
            // 
            // txtOperator
            // 
            this.txtOperator.Location = new System.Drawing.Point(26, 111);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(318, 26);
            this.txtOperator.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Operator";
            // 
            // txtOrdernumber
            // 
            this.txtOrdernumber.Enabled = false;
            this.txtOrdernumber.Location = new System.Drawing.Point(26, 35);
            this.txtOrdernumber.Name = "txtOrdernumber";
            this.txtOrdernumber.Size = new System.Drawing.Size(318, 26);
            this.txtOrdernumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order Number";
            // 
            // gridControlZEs
            // 
            this.gridControlZEs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlZEs.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControlZEs.Location = new System.Drawing.Point(0, 0);
            this.gridControlZEs.MainView = this.gridViewZEs;
            this.gridControlZEs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControlZEs.Name = "gridControlZEs";
            this.gridControlZEs.Size = new System.Drawing.Size(625, 282);
            this.gridControlZEs.TabIndex = 1;
            this.gridControlZEs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewZEs});
            // 
            // gridViewZEs
            // 
            this.gridViewZEs.GridControl = this.gridControlZEs;
            this.gridViewZEs.Name = "gridViewZEs";
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(213, 135);
            this.btnAbort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(176, 62);
            this.btnAbort.TabIndex = 1;
            this.btnAbort.Text = "Abbrechen";
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(410, 135);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(183, 62);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ZEs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 679);
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ZEs";
            this.Text = "ZEs";
            this.Load += new System.EventHandler(this.ZEs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtEditZE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrdernumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlZEs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewZEs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtEditZE;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAddZE;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnAbort;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl gridControlZEs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewZEs;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.TextEdit txtOperator;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtOrdernumber;
        private System.Windows.Forms.Label label1;
    }
}