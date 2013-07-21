namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    partial class paymentsMenu
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
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAddPayment = new System.Windows.Forms.Label();
            this.lblManagePayments = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(485, 52);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(130, 24);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "Add Payments";
            this.radButton1.ThemeName = "ControlDefault";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(642, 52);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(130, 24);
            this.radButton2.TabIndex = 1;
            this.radButton2.Text = "Manage Payments";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // radButton3
            // 
            this.radButton3.Location = new System.Drawing.Point(796, 52);
            this.radButton3.Name = "radButton3";
            this.radButton3.Size = new System.Drawing.Size(130, 24);
            this.radButton3.TabIndex = 1;
            this.radButton3.Text = "Payment Reports";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 32);
            this.label1.TabIndex = 21;
            this.label1.Text = "Payments Section";
            // 
            // lblAddPayment
            // 
            this.lblAddPayment.AutoSize = true;
            this.lblAddPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblAddPayment.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddPayment.ForeColor = System.Drawing.Color.Gold;
            this.lblAddPayment.Location = new System.Drawing.Point(235, 14);
            this.lblAddPayment.Name = "lblAddPayment";
            this.lblAddPayment.Size = new System.Drawing.Size(200, 32);
            this.lblAddPayment.TabIndex = 22;
            this.lblAddPayment.Text = "(Add Payament)";
            // 
            // lblManagePayments
            // 
            this.lblManagePayments.AutoSize = true;
            this.lblManagePayments.BackColor = System.Drawing.Color.Transparent;
            this.lblManagePayments.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManagePayments.ForeColor = System.Drawing.Color.Gold;
            this.lblManagePayments.Location = new System.Drawing.Point(236, 14);
            this.lblManagePayments.Name = "lblManagePayments";
            this.lblManagePayments.Size = new System.Drawing.Size(256, 32);
            this.lblManagePayments.TabIndex = 23;
            this.lblManagePayments.Text = "(Manage Payaments)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(237, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 32);
            this.label2.TabIndex = 24;
            this.label2.Text = "(Payament Reports)";
            // 
            // paymentsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(943, 700);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblManagePayments);
            this.Controls.Add(this.lblAddPayment);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton3);
            this.Controls.Add(this.radButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "paymentsMenu";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Payments Menu";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAddPayment;
        private System.Windows.Forms.Label lblManagePayments;
        private System.Windows.Forms.Label label2;
    }
}

