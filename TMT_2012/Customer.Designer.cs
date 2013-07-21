namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    partial class SearchCustomerGrid
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
            this.accountingsystemDataSet = new TMT_2012.accountingsystemDataSet();
            this.customerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customerTableAdapter = new TMT_2012.accountingsystemDataSetTableAdapters.customerTableAdapter();
            this.invoicecustomerfkBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.invoiceTableAdapter = new TMT_2012.accountingsystemDataSetTableAdapters.invoiceTableAdapter();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.btnPhoneBook = new Telerik.WinControls.UI.RadButton();
            this.lblTyreSearch = new Telerik.WinControls.UI.RadLabel();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.grdSearchCustomer = new Telerik.WinControls.UI.RadGridView();
            this.pnlCusTelephone = new System.Windows.Forms.Panel();
            this.lblCusName = new Telerik.WinControls.UI.RadLabel();
            this.lblCusOther = new Telerik.WinControls.UI.RadLabel();
            this.lblCusOffice = new Telerik.WinControls.UI.RadLabel();
            this.lblCusMobile = new Telerik.WinControls.UI.RadLabel();
            this.lblCusHome = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton14 = new Telerik.WinControls.UI.RadButton();
            this.lblRoder = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.accountingsystemDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoicecustomerfkBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPhoneBook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTyreSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSearchCustomer)).BeginInit();
            this.grdSearchCustomer.SuspendLayout();
            this.pnlCusTelephone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusOther)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusOffice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusMobile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRoder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // accountingsystemDataSet
            // 
            this.accountingsystemDataSet.DataSetName = "accountingsystemDataSet";
            this.accountingsystemDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customerBindingSource
            // 
            this.customerBindingSource.DataMember = "customer";
            this.customerBindingSource.DataSource = this.accountingsystemDataSet;
            // 
            // customerTableAdapter
            // 
            this.customerTableAdapter.ClearBeforeFill = true;
            // 
            // invoicecustomerfkBindingSource
            // 
            this.invoicecustomerfkBindingSource.DataMember = "invoice_customer_fk";
            this.invoicecustomerfkBindingSource.DataSource = this.customerBindingSource;
            // 
            // invoiceTableAdapter
            // 
            this.invoiceTableAdapter.ClearBeforeFill = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(560, 52);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(47, 17);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Refresh";
            this.btnClear.ThemeName = "Breeze";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPhoneBook
            // 
            this.btnPhoneBook.Location = new System.Drawing.Point(632, 104);
            this.btnPhoneBook.Name = "btnPhoneBook";
            this.btnPhoneBook.Size = new System.Drawing.Size(108, 44);
            this.btnPhoneBook.TabIndex = 2;
            this.btnPhoneBook.Text = "Phone Book";
            this.btnPhoneBook.ThemeName = "Breeze";
            this.btnPhoneBook.Click += new System.EventHandler(this.btnPhoneBook_Click);
            // 
            // lblTyreSearch
            // 
            this.lblTyreSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblTyreSearch.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTyreSearch.ForeColor = System.Drawing.Color.Gold;
            this.lblTyreSearch.Location = new System.Drawing.Point(27, 12);
            this.lblTyreSearch.Name = "lblTyreSearch";
            // 
            // 
            // 
            this.lblTyreSearch.RootElement.ForeColor = System.Drawing.Color.Gold;
            this.lblTyreSearch.Size = new System.Drawing.Size(229, 41);
            this.lblTyreSearch.TabIndex = 69;
            this.lblTyreSearch.Text = "Search Customer";
            // 
            // radButton3
            // 
            this.radButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radButton3.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton3.ForeColor = System.Drawing.Color.Crimson;
            this.radButton3.Location = new System.Drawing.Point(704, 12);
            this.radButton3.Name = "radButton3";
            // 
            // 
            // 
            this.radButton3.RootElement.ForeColor = System.Drawing.Color.Crimson;
            this.radButton3.Size = new System.Drawing.Size(40, 40);
            this.radButton3.TabIndex = 117;
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
            // grdSearchCustomer
            // 
            this.grdSearchCustomer.Controls.Add(this.pnlCusTelephone);
            this.grdSearchCustomer.ForeColor = System.Drawing.Color.Black;
            this.grdSearchCustomer.Location = new System.Drawing.Point(27, 77);
            // 
            // grdSearchCustomer
            // 
            this.grdSearchCustomer.MasterTemplate.AllowAddNewRow = false;
            this.grdSearchCustomer.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.grdSearchCustomer.Name = "grdSearchCustomer";
            this.grdSearchCustomer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.grdSearchCustomer.ReadOnly = true;
            // 
            // 
            // 
            this.grdSearchCustomer.RootElement.ForeColor = System.Drawing.Color.Black;
            this.grdSearchCustomer.RootElement.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.grdSearchCustomer.Size = new System.Drawing.Size(580, 303);
            this.grdSearchCustomer.TabIndex = 118;
            this.grdSearchCustomer.Text = "radGridView1";
            this.grdSearchCustomer.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdSearchCustomer_CellClick);
            this.grdSearchCustomer.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdSearchCustomer_CellDoubleClick);
            // 
            // pnlCusTelephone
            // 
            this.pnlCusTelephone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlCusTelephone.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCusTelephone.Controls.Add(this.lblCusName);
            this.pnlCusTelephone.Controls.Add(this.lblCusOther);
            this.pnlCusTelephone.Controls.Add(this.lblCusOffice);
            this.pnlCusTelephone.Controls.Add(this.lblCusMobile);
            this.pnlCusTelephone.Controls.Add(this.lblCusHome);
            this.pnlCusTelephone.Controls.Add(this.radLabel5);
            this.pnlCusTelephone.Controls.Add(this.radLabel4);
            this.pnlCusTelephone.Controls.Add(this.radLabel2);
            this.pnlCusTelephone.Controls.Add(this.radLabel3);
            this.pnlCusTelephone.Controls.Add(this.radLabel1);
            this.pnlCusTelephone.Controls.Add(this.radButton1);
            this.pnlCusTelephone.Controls.Add(this.radButton14);
            this.pnlCusTelephone.Controls.Add(this.lblRoder);
            this.pnlCusTelephone.Location = new System.Drawing.Point(151, 16);
            this.pnlCusTelephone.Name = "pnlCusTelephone";
            this.pnlCusTelephone.Size = new System.Drawing.Size(379, 259);
            this.pnlCusTelephone.TabIndex = 119;
            this.pnlCusTelephone.Visible = false;
            // 
            // lblCusName
            // 
            this.lblCusName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusName.ForeColor = System.Drawing.Color.Gold;
            this.lblCusName.Location = new System.Drawing.Point(162, 78);
            this.lblCusName.Name = "lblCusName";
            // 
            // 
            // 
            this.lblCusName.RootElement.ForeColor = System.Drawing.Color.Gold;
            this.lblCusName.Size = new System.Drawing.Size(117, 24);
            this.lblCusName.TabIndex = 120;
            this.lblCusName.Text = "Customer Name";
            // 
            // lblCusOther
            // 
            this.lblCusOther.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusOther.ForeColor = System.Drawing.Color.Gold;
            this.lblCusOther.Location = new System.Drawing.Point(163, 194);
            this.lblCusOther.Name = "lblCusOther";
            // 
            // 
            // 
            this.lblCusOther.RootElement.ForeColor = System.Drawing.Color.Gold;
            this.lblCusOther.Size = new System.Drawing.Size(46, 24);
            this.lblCusOther.TabIndex = 124;
            this.lblCusOther.Text = "Other";
            // 
            // lblCusOffice
            // 
            this.lblCusOffice.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusOffice.ForeColor = System.Drawing.Color.Gold;
            this.lblCusOffice.Location = new System.Drawing.Point(163, 168);
            this.lblCusOffice.Name = "lblCusOffice";
            // 
            // 
            // 
            this.lblCusOffice.RootElement.ForeColor = System.Drawing.Color.Gold;
            this.lblCusOffice.Size = new System.Drawing.Size(47, 24);
            this.lblCusOffice.TabIndex = 125;
            this.lblCusOffice.Text = "Office";
            // 
            // lblCusMobile
            // 
            this.lblCusMobile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusMobile.ForeColor = System.Drawing.Color.Gold;
            this.lblCusMobile.Location = new System.Drawing.Point(162, 112);
            this.lblCusMobile.Name = "lblCusMobile";
            // 
            // 
            // 
            this.lblCusMobile.RootElement.ForeColor = System.Drawing.Color.Gold;
            this.lblCusMobile.Size = new System.Drawing.Size(52, 24);
            this.lblCusMobile.TabIndex = 122;
            this.lblCusMobile.Text = "Mobile";
            // 
            // lblCusHome
            // 
            this.lblCusHome.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusHome.ForeColor = System.Drawing.Color.Gold;
            this.lblCusHome.Location = new System.Drawing.Point(162, 141);
            this.lblCusHome.Name = "lblCusHome";
            // 
            // 
            // 
            this.lblCusHome.RootElement.ForeColor = System.Drawing.Color.Gold;
            this.lblCusHome.Size = new System.Drawing.Size(47, 24);
            this.lblCusHome.TabIndex = 123;
            this.lblCusHome.Text = "Home";
            // 
            // radLabel5
            // 
            this.radLabel5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.ForeColor = System.Drawing.Color.White;
            this.radLabel5.Location = new System.Drawing.Point(28, 194);
            this.radLabel5.Name = "radLabel5";
            // 
            // 
            // 
            this.radLabel5.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel5.Size = new System.Drawing.Size(41, 21);
            this.radLabel5.TabIndex = 121;
            this.radLabel5.Text = "Other";
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.ForeColor = System.Drawing.Color.White;
            this.radLabel4.Location = new System.Drawing.Point(28, 168);
            this.radLabel4.Name = "radLabel4";
            // 
            // 
            // 
            this.radLabel4.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel4.Size = new System.Drawing.Size(42, 21);
            this.radLabel4.TabIndex = 121;
            this.radLabel4.Text = "Office";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.ForeColor = System.Drawing.Color.White;
            this.radLabel2.Location = new System.Drawing.Point(27, 112);
            this.radLabel2.Name = "radLabel2";
            // 
            // 
            // 
            this.radLabel2.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel2.Size = new System.Drawing.Size(48, 21);
            this.radLabel2.TabIndex = 120;
            this.radLabel2.Text = "Mobile";
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.ForeColor = System.Drawing.Color.White;
            this.radLabel3.Location = new System.Drawing.Point(27, 141);
            this.radLabel3.Name = "radLabel3";
            // 
            // 
            // 
            this.radLabel3.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel3.Size = new System.Drawing.Size(43, 21);
            this.radLabel3.TabIndex = 121;
            this.radLabel3.Text = "Home";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(27, 78);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(103, 21);
            this.radLabel1.TabIndex = 119;
            this.radLabel1.Text = "Customer Name";
            // 
            // radButton1
            // 
            this.radButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radButton1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton1.ForeColor = System.Drawing.Color.Crimson;
            this.radButton1.Location = new System.Drawing.Point(326, 9);
            this.radButton1.Name = "radButton1";
            // 
            // 
            // 
            this.radButton1.RootElement.ForeColor = System.Drawing.Color.Crimson;
            this.radButton1.Size = new System.Drawing.Size(40, 40);
            this.radButton1.TabIndex = 118;
            this.radButton1.Text = "X";
            this.radButton1.ThemeName = "Breeze";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton1.GetChildAt(0))).Text = "X";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(166)))), ((int)(((byte)(91)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(109)))), ((int)(((byte)(60)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(220)))), ((int)(((byte)(159)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(254)))), ((int)(((byte)(143)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(2))).InnerColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(247)))), ((int)(((byte)(91)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(2))).InnerColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(236)))), ((int)(((byte)(78)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(2))).InnerColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(242)))), ((int)(((byte)(14)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // radButton14
            // 
            this.radButton14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radButton14.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton14.ForeColor = System.Drawing.Color.Crimson;
            this.radButton14.Location = new System.Drawing.Point(702, 10);
            this.radButton14.Name = "radButton14";
            // 
            // 
            // 
            this.radButton14.RootElement.ForeColor = System.Drawing.Color.Crimson;
            this.radButton14.Size = new System.Drawing.Size(40, 40);
            this.radButton14.TabIndex = 117;
            this.radButton14.Text = "X";
            this.radButton14.ThemeName = "Breeze";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton14.GetChildAt(0))).Text = "X";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(166)))), ((int)(((byte)(91)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(109)))), ((int)(((byte)(60)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(220)))), ((int)(((byte)(159)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(254)))), ((int)(((byte)(143)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(2))).InnerColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(247)))), ((int)(((byte)(91)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(2))).InnerColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(236)))), ((int)(((byte)(78)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(2))).InnerColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(242)))), ((int)(((byte)(14)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radButton14.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // lblRoder
            // 
            this.lblRoder.BackColor = System.Drawing.Color.Transparent;
            this.lblRoder.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblRoder.ForeColor = System.Drawing.Color.Gold;
            this.lblRoder.Location = new System.Drawing.Point(11, 9);
            this.lblRoder.Name = "lblRoder";
            // 
            // 
            // 
            this.lblRoder.RootElement.ForeColor = System.Drawing.Color.Gold;
            this.lblRoder.Size = new System.Drawing.Size(279, 41);
            this.lblRoder.TabIndex = 69;
            this.lblRoder.Text = "Customer Telephone";
            // 
            // SearchCustomerGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(784, 529);
            this.Controls.Add(this.grdSearchCustomer);
            this.Controls.Add(this.radButton3);
            this.Controls.Add(this.lblTyreSearch);
            this.Controls.Add(this.btnPhoneBook);
            this.Controls.Add(this.btnClear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SearchCustomerGrid";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SearchCustomerGrid";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.SearchCustomerGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accountingsystemDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoicecustomerfkBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPhoneBook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTyreSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSearchCustomer)).EndInit();
            this.grdSearchCustomer.ResumeLayout(false);
            this.pnlCusTelephone.ResumeLayout(false);
            this.pnlCusTelephone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusOther)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusOffice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusMobile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCusHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRoder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private accountingsystemDataSet accountingsystemDataSet;
        private System.Windows.Forms.BindingSource customerBindingSource;
        private accountingsystemDataSetTableAdapters.customerTableAdapter customerTableAdapter;
        private System.Windows.Forms.BindingSource invoicecustomerfkBindingSource;
        private accountingsystemDataSetTableAdapters.invoiceTableAdapter invoiceTableAdapter;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnPhoneBook;
        private Telerik.WinControls.UI.RadLabel lblTyreSearch;
        private Telerik.WinControls.UI.RadButton radButton3;
        public Telerik.WinControls.UI.RadGridView grdSearchCustomer;
        private System.Windows.Forms.Panel pnlCusTelephone;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton14;
        private Telerik.WinControls.UI.RadLabel lblRoder;
        private Telerik.WinControls.UI.RadLabel lblCusName;
        private Telerik.WinControls.UI.RadLabel lblCusOther;
        private Telerik.WinControls.UI.RadLabel lblCusOffice;
        private Telerik.WinControls.UI.RadLabel lblCusMobile;
        private Telerik.WinControls.UI.RadLabel lblCusHome;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}

