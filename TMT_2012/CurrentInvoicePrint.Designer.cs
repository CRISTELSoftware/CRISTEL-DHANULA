﻿namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    partial class CurrentInvoicePrint
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.radButton8 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radButton8)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TMT_2012.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-1, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(676, 552);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // radButton8
            // 
            this.radButton8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radButton8.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton8.ForeColor = System.Drawing.Color.Crimson;
            this.radButton8.Location = new System.Drawing.Point(648, 0);
            this.radButton8.Name = "radButton8";
            // 
            // 
            // 
            this.radButton8.RootElement.ForeColor = System.Drawing.Color.Crimson;
            this.radButton8.Size = new System.Drawing.Size(27, 29);
            this.radButton8.TabIndex = 118;
            this.radButton8.Text = "X";
            this.radButton8.ThemeName = "Breeze";
            this.radButton8.Click += new System.EventHandler(this.radButton8_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton8.GetChildAt(0))).Text = "X";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(166)))), ((int)(((byte)(91)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(109)))), ((int)(((byte)(60)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(220)))), ((int)(((byte)(159)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(254)))), ((int)(((byte)(143)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(2))).InnerColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(247)))), ((int)(((byte)(91)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(2))).InnerColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(236)))), ((int)(((byte)(78)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(2))).InnerColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(242)))), ((int)(((byte)(14)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton8.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // CurrentInvoicePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(675, 552);
            this.Controls.Add(this.radButton8);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CurrentInvoicePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CurrentInvoicePrint";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CurrentInvoicePrint_FormClosing);
            this.Load += new System.EventHandler(this.CurrentInvoicePrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radButton8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private Telerik.WinControls.UI.RadButton radButton8;
    }
}