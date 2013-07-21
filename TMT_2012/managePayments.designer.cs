namespace TMT_2012
{
    partial class managePayments
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
            this.label12 = new System.Windows.Forms.Label();
            this.radCashChqPayments = new System.Windows.Forms.RadioButton();
            this.radChequePayment = new System.Windows.Forms.RadioButton();
            this.radCashPayment = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.grdPayments = new Telerik.WinControls.UI.RadGridView();
            this.pnlChangeStatus = new System.Windows.Forms.Panel();
            this.btnPending = new System.Windows.Forms.Button();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReturned = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCloseStatus = new System.Windows.Forms.Button();
            this.lblBank = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblChequeNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radCreditcardPayments = new System.Windows.Forms.RadioButton();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.radButton4 = new Telerik.WinControls.UI.RadButton();
            this.label3 = new System.Windows.Forms.Label();
            this.grdFillCustomer = new System.Windows.Forms.DataGridView();
            this.txtSearchCustomer = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).BeginInit();
            this.grdPayments.SuspendLayout();
            this.pnlChangeStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFillCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(26, 79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 19);
            this.label12.TabIndex = 19;
            this.label12.Text = "Paymnet Type";
            // 
            // radCashChqPayments
            // 
            this.radCashChqPayments.AutoSize = true;
            this.radCashChqPayments.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radCashChqPayments.ForeColor = System.Drawing.Color.White;
            this.radCashChqPayments.Location = new System.Drawing.Point(505, 76);
            this.radCashChqPayments.Name = "radCashChqPayments";
            this.radCashChqPayments.Size = new System.Drawing.Size(172, 23);
            this.radCashChqPayments.TabIndex = 18;
            this.radCashChqPayments.TabStop = true;
            this.radCashChqPayments.Text = "Cash Cheque Payments";
            this.radCashChqPayments.UseVisualStyleBackColor = true;
            this.radCashChqPayments.CheckedChanged += new System.EventHandler(this.radCashChqPayments_CheckedChanged);
            // 
            // radChequePayment
            // 
            this.radChequePayment.AutoSize = true;
            this.radChequePayment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radChequePayment.ForeColor = System.Drawing.Color.White;
            this.radChequePayment.Location = new System.Drawing.Point(335, 76);
            this.radChequePayment.Name = "radChequePayment";
            this.radChequePayment.Size = new System.Drawing.Size(138, 23);
            this.radChequePayment.TabIndex = 17;
            this.radChequePayment.TabStop = true;
            this.radChequePayment.Text = "Cheque Payments";
            this.radChequePayment.UseVisualStyleBackColor = true;
            this.radChequePayment.CheckedChanged += new System.EventHandler(this.radChequePayment_CheckedChanged);
            // 
            // radCashPayment
            // 
            this.radCashPayment.AutoSize = true;
            this.radCashPayment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radCashPayment.ForeColor = System.Drawing.Color.White;
            this.radCashPayment.Location = new System.Drawing.Point(186, 76);
            this.radCashPayment.Name = "radCashPayment";
            this.radCashPayment.Size = new System.Drawing.Size(121, 23);
            this.radCashPayment.TabIndex = 16;
            this.radCashPayment.TabStop = true;
            this.radCashPayment.Text = "Cash Payments";
            this.radCashPayment.UseVisualStyleBackColor = true;
            this.radCashPayment.CheckedChanged += new System.EventHandler(this.radCashPayment_CheckedChanged);
            this.radCashPayment.Click += new System.EventHandler(this.radCashPayment_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 32);
            this.label1.TabIndex = 20;
            this.label1.Text = "Manage Payments";
            // 
            // grdPayments
            // 
            this.grdPayments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.grdPayments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.grdPayments.Controls.Add(this.pnlChangeStatus);
            this.grdPayments.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdPayments.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grdPayments.ForeColor = System.Drawing.Color.Black;
            this.grdPayments.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdPayments.Location = new System.Drawing.Point(12, 143);
            // 
            // grdPayments
            // 
            this.grdPayments.MasterTemplate.AllowAddNewRow = false;
            this.grdPayments.MasterTemplate.AllowColumnChooser = false;
            this.grdPayments.MasterTemplate.AllowDeleteRow = false;
            this.grdPayments.MasterTemplate.AllowEditRow = false;
            this.grdPayments.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.grdPayments.Name = "grdPayments";
            this.grdPayments.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.grdPayments.ReadOnly = true;
            this.grdPayments.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.grdPayments.RootElement.ForeColor = System.Drawing.Color.Black;
            this.grdPayments.RootElement.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.grdPayments.Size = new System.Drawing.Size(1069, 507);
            this.grdPayments.TabIndex = 58;
            this.grdPayments.Text = "grdPayments";
            this.grdPayments.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.grdPayments_RowFormatting);
            this.grdPayments.EditorRequired += new Telerik.WinControls.UI.EditorRequiredEventHandler(this.grdPayments_EditorRequired);
            this.grdPayments.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.grdPayments_CurrentRowChanged);
            this.grdPayments.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdPayments_CellDoubleClick);
            this.grdPayments.BackColorChanged += new System.EventHandler(this.grdPayments_BackColorChanged);
            // 
            // pnlChangeStatus
            // 
            this.pnlChangeStatus.BackColor = System.Drawing.Color.DimGray;
            this.pnlChangeStatus.Controls.Add(this.btnPending);
            this.pnlChangeStatus.Controls.Add(this.lblCurrentStatus);
            this.pnlChangeStatus.Controls.Add(this.label9);
            this.pnlChangeStatus.Controls.Add(this.lblNote);
            this.pnlChangeStatus.Controls.Add(this.label8);
            this.pnlChangeStatus.Controls.Add(this.lblAmount);
            this.pnlChangeStatus.Controls.Add(this.label7);
            this.pnlChangeStatus.Controls.Add(this.lblCustomerName);
            this.pnlChangeStatus.Controls.Add(this.label6);
            this.pnlChangeStatus.Controls.Add(this.btnReturned);
            this.pnlChangeStatus.Controls.Add(this.btnOK);
            this.pnlChangeStatus.Controls.Add(this.lblDueDate);
            this.pnlChangeStatus.Controls.Add(this.label4);
            this.pnlChangeStatus.Controls.Add(this.btnCloseStatus);
            this.pnlChangeStatus.Controls.Add(this.lblBank);
            this.pnlChangeStatus.Controls.Add(this.label5);
            this.pnlChangeStatus.Controls.Add(this.lblChequeNo);
            this.pnlChangeStatus.Controls.Add(this.label2);
            this.pnlChangeStatus.Location = new System.Drawing.Point(363, 21);
            this.pnlChangeStatus.Name = "pnlChangeStatus";
            this.pnlChangeStatus.Size = new System.Drawing.Size(414, 286);
            this.pnlChangeStatus.TabIndex = 0;
            // 
            // btnPending
            // 
            this.btnPending.Location = new System.Drawing.Point(29, 247);
            this.btnPending.Name = "btnPending";
            this.btnPending.Size = new System.Drawing.Size(101, 23);
            this.btnPending.TabIndex = 17;
            this.btnPending.Text = "Mark as Pending";
            this.btnPending.UseVisualStyleBackColor = true;
            this.btnPending.Click += new System.EventHandler(this.btnPending_Click);
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentStatus.ForeColor = System.Drawing.Color.Orange;
            this.lblCurrentStatus.Location = new System.Drawing.Point(161, 217);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(96, 17);
            this.lblCurrentStatus.TabIndex = 16;
            this.lblCurrentStatus.Text = "Current Status";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(34, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Current Status";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.Khaki;
            this.lblNote.Location = new System.Drawing.Point(161, 187);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(96, 17);
            this.lblNote.TabIndex = 14;
            this.lblNote.Text = "Payment Note";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(34, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "Payment Note";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.Khaki;
            this.lblAmount.Location = new System.Drawing.Point(161, 152);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(58, 17);
            this.lblAmount.TabIndex = 12;
            this.lblAmount.Text = "Amount";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(34, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Amount";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.ForeColor = System.Drawing.Color.Khaki;
            this.lblCustomerName.Location = new System.Drawing.Point(161, 14);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(107, 17);
            this.lblCustomerName.TabIndex = 10;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(34, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Customer Name";
            // 
            // btnReturned
            // 
            this.btnReturned.Location = new System.Drawing.Point(152, 247);
            this.btnReturned.Name = "btnReturned";
            this.btnReturned.Size = new System.Drawing.Size(106, 23);
            this.btnReturned.TabIndex = 8;
            this.btnReturned.Text = "Mark as Returned";
            this.btnReturned.UseVisualStyleBackColor = true;
            this.btnReturned.Click += new System.EventHandler(this.btnReturned_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(275, 247);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Mark as Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDueDate.ForeColor = System.Drawing.Color.Khaki;
            this.lblDueDate.Location = new System.Drawing.Point(161, 116);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(66, 17);
            this.lblDueDate.TabIndex = 6;
            this.lblDueDate.Text = "Due Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(34, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Due Date";
            // 
            // btnCloseStatus
            // 
            this.btnCloseStatus.Location = new System.Drawing.Point(364, 3);
            this.btnCloseStatus.Name = "btnCloseStatus";
            this.btnCloseStatus.Size = new System.Drawing.Size(43, 37);
            this.btnCloseStatus.TabIndex = 4;
            this.btnCloseStatus.Text = "Close";
            this.btnCloseStatus.UseVisualStyleBackColor = true;
            this.btnCloseStatus.Click += new System.EventHandler(this.btnCloseStatus_Click);
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBank.ForeColor = System.Drawing.Color.Khaki;
            this.lblBank.Location = new System.Drawing.Point(161, 80);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(38, 17);
            this.lblBank.TabIndex = 3;
            this.lblBank.Text = "Bank";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(34, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Bank";
            // 
            // lblChequeNo
            // 
            this.lblChequeNo.AutoSize = true;
            this.lblChequeNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChequeNo.ForeColor = System.Drawing.Color.Khaki;
            this.lblChequeNo.Location = new System.Drawing.Point(161, 49);
            this.lblChequeNo.Name = "lblChequeNo";
            this.lblChequeNo.Size = new System.Drawing.Size(78, 17);
            this.lblChequeNo.TabIndex = 1;
            this.lblChequeNo.Text = "Cheque NO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(34, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cheque Number";
            // 
            // radCreditcardPayments
            // 
            this.radCreditcardPayments.AutoSize = true;
            this.radCreditcardPayments.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radCreditcardPayments.ForeColor = System.Drawing.Color.White;
            this.radCreditcardPayments.Location = new System.Drawing.Point(703, 76);
            this.radCreditcardPayments.Name = "radCreditcardPayments";
            this.radCreditcardPayments.Size = new System.Drawing.Size(161, 23);
            this.radCreditcardPayments.TabIndex = 59;
            this.radCreditcardPayments.TabStop = true;
            this.radCreditcardPayments.Text = "Credit Card Payments";
            this.radCreditcardPayments.UseVisualStyleBackColor = true;
            this.radCreditcardPayments.CheckedChanged += new System.EventHandler(this.radCreditcardPayments_CheckedChanged);
            // 
            // radButton3
            // 
            this.radButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radButton3.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton3.ForeColor = System.Drawing.Color.Crimson;
            this.radButton3.Location = new System.Drawing.Point(1065, 6);
            this.radButton3.Name = "radButton3";
            // 
            // 
            // 
            this.radButton3.RootElement.ForeColor = System.Drawing.Color.Crimson;
            this.radButton3.Size = new System.Drawing.Size(30, 30);
            this.radButton3.TabIndex = 135;
            this.radButton3.Text = "X";
            this.radButton3.ThemeName = "Breeze";
            this.radButton3.Click += new System.EventHandler(this.radButton3_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton3.GetChildAt(0))).Text = "X";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(166)))), ((int)(((byte)(91)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(109)))), ((int)(((byte)(60)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(220)))), ((int)(((byte)(159)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(254)))), ((int)(((byte)(143)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(2))).InnerColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(247)))), ((int)(((byte)(91)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(2))).InnerColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(236)))), ((int)(((byte)(78)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(2))).InnerColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(242)))), ((int)(((byte)(14)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton3.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // radButton4
            // 
            this.radButton4.Location = new System.Drawing.Point(892, 12);
            this.radButton4.Name = "radButton4";
            this.radButton4.Size = new System.Drawing.Size(130, 24);
            this.radButton4.TabIndex = 143;
            this.radButton4.Text = "Add Payments";
            this.radButton4.ThemeName = "Breeze";
            this.radButton4.Click += new System.EventHandler(this.radButton4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(26, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 21);
            this.label3.TabIndex = 152;
            this.label3.Text = "Search Customer";
            // 
            // grdFillCustomer
            // 
            this.grdFillCustomer.AllowUserToAddRows = false;
            this.grdFillCustomer.AllowUserToDeleteRows = false;
            this.grdFillCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdFillCustomer.BackgroundColor = System.Drawing.Color.White;
            this.grdFillCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdFillCustomer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdFillCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFillCustomer.ColumnHeadersVisible = false;
            this.grdFillCustomer.Location = new System.Drawing.Point(177, 131);
            this.grdFillCustomer.Name = "grdFillCustomer";
            this.grdFillCustomer.ReadOnly = true;
            this.grdFillCustomer.RowHeadersVisible = false;
            this.grdFillCustomer.Size = new System.Drawing.Size(229, 143);
            this.grdFillCustomer.TabIndex = 151;
            this.grdFillCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFillCustomer_CellClick);
            // 
            // txtSearchCustomer
            // 
            this.txtSearchCustomer.Location = new System.Drawing.Point(177, 111);
            this.txtSearchCustomer.Name = "txtSearchCustomer";
            this.txtSearchCustomer.Size = new System.Drawing.Size(229, 20);
            this.txtSearchCustomer.TabIndex = 150;
            this.txtSearchCustomer.TextChanged += new System.EventHandler(this.txtSearchCustomer_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(441, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 36);
            this.button1.TabIndex = 149;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // managePayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1370, 750);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grdFillCustomer);
            this.Controls.Add(this.txtSearchCustomer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radButton4);
            this.Controls.Add(this.radButton3);
            this.Controls.Add(this.radCreditcardPayments);
            this.Controls.Add(this.grdPayments);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.radCashChqPayments);
            this.Controls.Add(this.radChequePayment);
            this.Controls.Add(this.radCashPayment);
            this.FormBehavior = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "managePayments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Manage Payments";
            this.ThemeName = "ControlDefault";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.managePayments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).EndInit();
            this.grdPayments.ResumeLayout(false);
            this.pnlChangeStatus.ResumeLayout(false);
            this.pnlChangeStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFillCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radCashChqPayments;
        private System.Windows.Forms.RadioButton radChequePayment;
        private System.Windows.Forms.RadioButton radCashPayment;
        private System.Windows.Forms.Label label1;
        public Telerik.WinControls.UI.RadGridView grdPayments;
        private System.Windows.Forms.Panel pnlChangeStatus;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblChequeNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCloseStatus;
        private System.Windows.Forms.Button btnReturned;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPending;
        private System.Windows.Forms.RadioButton radCreditcardPayments;
        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadButton radButton4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView grdFillCustomer;
        private System.Windows.Forms.TextBox txtSearchCustomer;
        private System.Windows.Forms.Button button1;
    }
}

