using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CustomerSection : Form
    {
        string cusid;
        string CusName;
        DataSet ds_name;
        DataSet ds_ID;
        string customerNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSection"/> class.
        /// </summary>
        public CustomerSection()
        {
            InitializeComponent();
        }

        private void btnAddNewBattaryCat_Click(object sender, EventArgs e)
        {
            string q = "SELECT * FROM customer";
            lbl_customer_section.Text = "Customer Section (Search Customer)";
            lbl_data_msg.Text = "";
            panel_search_customer.Visible = true;
            panel_add_customer.Visible = false;
            pnlCusTelephone.Visible = false;
            fillCustomerGrid();
            filter_customer_ID(q);
            txtCustomerName.Text = "Defult";
            grdFillCustomer.Visible = false;
            panel_invoice.Enabled = false;
            panel_Reports.Visible = false;
            
        }

        private void fillCustomerGrid()
        {
            string q1 = "SELECT customerno As Customer_No,customername AS Customer_Name,address AS Address,notes AS Notes,creditlimit AS Credit_Limit,customertype AS Customer_Type FROM customer";
            DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
            grdSearchCustomer.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
        }

        private void grdSearchCustomer_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            int r_count = grdSearchCustomer.RowCount;
            if (val >= 0)
            {
                string customerid = grdSearchCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                cusid = customerid;
                string customerName = grdSearchCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                CusName = customerName;
                
                show_customer_data();
                txtCustomerName.Text = CusName;
                com_customer_id.Text = cusid;
                grdFillCustomer.Visible = false;
                fill_invoice_no();
                
            }

        }

        private void fill_invoice_no()
        {
            string q = "SELECT invoiceno FROM invoice WHERE customer='"+ com_customer_id.Text +"'";
            DataSet ds_customer_id = middle_access.db_access.SelectData(q);
            if (ds_customer_id != null)
            {
                com_invoice_no.DataSource = ds_customer_id.Tables[0];
                com_invoice_no.DisplayMember = "invoiceno";
                com_invoice_no.ValueMember = "invoiceno";
                com_invoice_no.Text = "";
                panel_invoice.Enabled = true;

            }
            else
            {
                com_invoice_no.DataSource = null;
                panel_invoice.Enabled = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            fillCustomerGrid();
        }

        private void btnPhoneBook_Click(object sender, EventArgs e)
        {
            show_customer_data();
        }

        private void show_customer_data()
        {
            try
            {

                string q1 = "SELECT * FROM customertel WHERE customerNo ='" + cusid + "'";
                DataSet ds_customer_TelePhone = middle_access.db_access.SelectData(q1);
                DataRow row_cus_telephone; 
                if (ds_customer_TelePhone == null)
                {
                   // MessageBox.Show("No contact numbers!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbl_data_msg.Text = "No Contacts...";
                    pnlCusTelephone.Visible = false;
                }             


                else
                {
                    row_cus_telephone = ds_customer_TelePhone.Tables[0].Rows[0];
                    if (row_cus_telephone.ItemArray.GetValue(2).ToString() == "" && row_cus_telephone.ItemArray.GetValue(1).ToString() == "" && row_cus_telephone.ItemArray.GetValue(3).ToString() == "" && row_cus_telephone.ItemArray.GetValue(4).ToString() == "")
                    {
                        lbl_data_msg.Text = "No Contacts...";
                        pnlCusTelephone.Visible = false;
                    }
                    else
                    {
                        string cusTelephone = row_cus_telephone.ItemArray.GetValue(0).ToString();

                        //string cuteomerName = grdSearchCustomer.Rows[0].Cells[1].ToString();

                        pnlCusTelephone.Visible = true;

                        lblCusName.Text = CusName;
                        lblCusMobile.Text = row_cus_telephone.ItemArray.GetValue(2).ToString();
                        lblCusHome.Text = row_cus_telephone.ItemArray.GetValue(1).ToString();
                        lblCusOffice.Text = row_cus_telephone.ItemArray.GetValue(3).ToString();
                        lblCusOther.Text = row_cus_telephone.ItemArray.GetValue(4).ToString();
                        lblfax.Text = row_cus_telephone.ItemArray.GetValue(5).ToString();
                    }
                }
                fill_invoice_no();
               // txtCustomerName.Text = "";
               // com_customer_id.Text = "";
                grdFillCustomer.Visible = false;
                
            }
            catch (NullReferenceException)
            {
                throw new Exception();
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            string q = "SELECT * FROM customer";
            filter_customer_ID(q);
            pnlCusTelephone.Visible = false;
            lbl_data_msg.Text = "";
            txtCustomerName.Text = "";
            grdFillCustomer.Visible = false;
            
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

        private void CustomerSection_Load(object sender, EventArgs e)
        {
            txtCustomerName.Text = "Defult";
            string q = "SELECT * FROM customer";
            lbl_customer_section.Text = "Customer Section (Search Customer)";
            grdFillCustomer.Visible = false;
            fillCustomerGrid();
            filter_customer_ID(q);
            
            panel_invoice.Enabled = false;
            
        }

        private void btnBattrySettings_Click(object sender, EventArgs e)
        {
            lbl_customer_section.Text = "Customer Section (Add Customer)";
            panel_search_customer.Visible = false;
            panel_add_customer.Visible = true;
            btn_Add_Customer.Enabled = true;
            btn_upate_customer.Enabled = false;
            grdCustomer.Enabled = false;
            fill_add_customer_grid();
            loadCusNo();
            clear_all();
            DDL_crd_Limit.Text = "0";
            grdFillCustomer_.Visible = false;
            panel_Reports.Visible = false;
        }

        private void fill_add_customer_grid()
        {
            try
            {
                string q = "SELECT  C.customerno,C.customername,C.address,C.customertype,C.notes,C.creditlimit,CT.tel_home,CT.tel_mobile,CT.tel_office,CT.tel_other,CT.fax,CV.vehiclenos FROM customer C,customertel CT, customervehicles CV WHERE C.customerno=CT.customerNo AND CV.customerNo = C.customerno";
                DataSet ds_add_customer_data = middle_access.db_access.SelectData(q);
                grdCustomer.DataSource = ds_add_customer_data.Tables[0].DefaultView;
            }
            catch (Exception)
            {
                
                
            }
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            string name = txtCustomerName.Text;
            filter_customer_name(name,grdFillCustomer);
        }

        private void filter_customer_name(string name,DataGridView dg)
        {
            string q1 = "SELECT customername FROM customer WHERE customername  LIKE  '" + name + "%' ";
            ds_name = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_name != null) // if data set is not null
            {
                dg.Visible = false;
                dg.Visible = true;
                dg.DataSource = ds_name.Tables[0];


            }
            else
            {
                dg.DataSource = null;
                dg.Visible = false;
            }

        }

        private void grdFillCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {   
           
            txtCustomerName.Text = grdFillCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCustomerName.Focus();
            grdFillCustomer.Visible = false;
            string q = "SELECT * FROM customer WHERE customername='" + txtCustomerName.Text + "'";
            filter_customer_ID(q);
        }


        private void filter_customer_ID(string q)
        {
            string q1 = q;
            ds_ID = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_ID != null) // if data set is not null
            {
                com_customer_id.DataSource = ds_ID.Tables[0]; //fill table 
                com_customer_id.DisplayMember = "customerno";
                com_customer_id.ValueMember = "customerno";
                if (txb_Cus_Name.Text == "")
                {
                    //MessageBox.Show("awa");
                    com_customer_id.SelectedText = "";
                }


            }
            else
            {
                com_customer_id.DataSource = null;
            }

        }

        private void com_customer_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customer_ID = com_customer_id.Text;
            string q = "SELECT customername FROM customer WHERE customerno='" + customer_ID + "'";
            ds_name = middle_access.db_access.SelectData(q);
            if (ds_name != null)
            {
            DataRow dr_name = ds_name.Tables[0].Rows[0];
            txtCustomerName.Text = dr_name.ItemArray.GetValue(0).ToString();
            }
            grdFillCustomer.Visible = false;
            fill_invoice_no();
           
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            cusid = com_customer_id.Text;
            CusName = txtCustomerName.Text;
            if (cusid == "" || CusName == "")
            {
                MessageBox.Show("Enter detalils correctly!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                show_customer_data();
            }
           
        }


        private void loadCusNo()
        {
            string q1 = "SELECT maxno FROM autoincrem where tablename='C'";
            DataSet ds_InviceNo = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table           

            if (ds_InviceNo != null) // if data set is not null
            {
                customerNumber = ds_InviceNo.Tables[0].Rows[0][0].ToString();
                txb_Cus_No.Text = "C " + customerNumber + "";


            }
            else
                txb_Cus_No.Text = null; //fill table 

        }

        private void btn_Add_Customer_Click(object sender, EventArgs e)
        {
            string name = txb_Cus_Name.Text;
            string Cusno = txb_Cus_No.Text;
            string telMobile = txb_tel_mobile.Text;
            string telOffice = txb_tel_office.Text;
            string telHome = txb_tel_home.Text;
            string telOther = txb_tel_other.Text;
            string fax = txb_fax.Text;
            string address = txb_Cus_Addrs.Text;
            string notes = txb_cus_Note.Text;

       
            string creditlimit = DDL_crd_Limit.Text.ToString();
            string customercat = drwCustomerCat.Text.ToString();

      

            int r = grd_vehicle_no.RowCount;
            string no = "";           
            
            for (int i = 1; i < r; i++ )
            {
                try
                {
                    string row_item = grd_vehicle_no.Rows[i - 1].Cells[0].Value.ToString();
                    if (i != r - 1)
                    {

                        no += grd_vehicle_no.Rows[i - 1].Cells[0].Value.ToString() + ",";

                    }
                    else
                    {
                        no += grd_vehicle_no.Rows[i - 1].Cells[0].Value.ToString();
                    }
                }

                catch (Exception x)
                {
                }
                    
            }


            //****************************************************************************

            //if customerNo is not null
            if (txb_Cus_No.Text != "")
            {
                //  if customer name is not null
                if (name != "")
                {



                    //insert into  customer infomation customer table 
                    string q5 = "INSERT INTO customer(customerno,customername,address,notes,creditlimit,customertype) VALUES ('" + Cusno + "','" + name + "','" + address + "','" + notes + "','" + creditlimit + "','" + customercat + "')";
                    bool status1 = middle_access.db_access.InsertData(q5);

                    //insert into customer telephone number 
                    string q6 = "INSERT INTO customertel (customerNo,tel_home,tel_mobile,tel_office,tel_other,fax)  VALUES('" + Cusno + "','" + telHome + "','" + telMobile + "','" + telOffice + "','" + telOther + "','"+ fax +"')";
                    bool status2 = middle_access.db_access.InsertData(q6);

                    // insert into customer vehicle 
                    string q7 = "INSERT INTO customervehicles (customerNo,vehiclenos)  VALUES('" + Cusno + "','" + no + "')";
                    bool status3 = middle_access.db_access.InsertData(q7);


                    if (status1 == true && status2 == true && status3 == true) // if data is insert
                    {
                        MessageBox.Show("New Customer Successfully Added !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button  

                        //to increment the customer no
                        int nextCusNo = (Convert.ToInt32(customerNumber)) + 1;
                        string q = "UPDATE autoincrem SET maxno = " + nextCusNo + "  WHERE tablename = 'C'";
                        bool status = middle_access.db_access.UpdateData(q);
                        fill_add_customer_grid();
                        DDL_crd_Limit.Text = "0";
                        loadCusNo();
                        clear_all();
                        

                    }

                        // if data is not insert

                    else
                    {
                        MessageBox.Show("Customer not added. Please check inputs!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       // clear_all();
                    }


                }

                //  if customer name is null
                else
                {
                    MessageBox.Show("Please Enter a Customer Name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                }
            }


            //if customerNo is null                                             
            else
            {
                MessageBox.Show("Please Check Customer Number!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //*******************************************************************************
        }

        private void clear_all()
        {
           txb_Cus_Name.Text="";
           txb_tel_mobile.Text = "";
           txb_tel_office.Text = "";
           txb_tel_home.Text = "";
           txb_tel_other.Text = "";
           txb_fax.Text = "";
           txb_Cus_Addrs.Text = "";
           txb_cus_Note.Text = "";
           DDL_crd_Limit.Text = "0";
           drwCustomerCat.Text = "";

           grd_vehicle_no.Rows.Clear();
           
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            lbl_customer_section.Text = "Customer Section (Update Customer)";
            panel_search_customer.Visible = false;
            panel_add_customer.Visible = true;
            btn_Add_Customer.Enabled = false;
            btn_upate_customer.Enabled = true;
            grdCustomer.Enabled = true;
            fill_add_customer_grid();
            txb_Cus_No.Text = ""; 
            clear_all();
            panel_Reports.Visible = false;
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            if (grdCustomer.Enabled == true)
            {
                txb_Cus_No.Text = "";
            }
            clear_all();
        }

        private void grdCustomer_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            grd_vehicle_no.Rows.Clear();
            int val = e.RowIndex;
           // int r_count = grdCustomer.RowCount;
            //if (val >= 0)
            //{
            //    string vehicle_nos = null;
            //    vehicle_nos = grdCustomer.Rows[e.RowIndex].Cells[10].Value.ToString();
            //    if (vehicle_nos != "")
            //    {
            //    string[] strArr = null;
            //    char[] splitchar = { ',' };
            //    strArr = vehicle_nos.Split(splitchar);
            //    for (int count = 0; count <= strArr.Length - 1; count++)
            //    {
            //        grd_vehicle_no.Rows.Add();
            //        grd_vehicle_no.Rows[count].Cells[0].Value = strArr[count];
                   
            //    }
            //    }

              
            //}
            try
            {
                set_values(val);               
            }
            catch (Exception)
            {
                
                
            }

        }

        private void set_values(int i)
        {
            txb_Cus_No.Text = grdCustomer.Rows[i].Cells[0].Value.ToString();
            txb_Cus_Name.Text = grdCustomer.Rows[i].Cells[1].Value.ToString();
            txb_Cus_Addrs.Text = grdCustomer.Rows[i].Cells[2].Value.ToString();
            drwCustomerCat.Text = grdCustomer.Rows[i].Cells[3].Value.ToString();
            txb_cus_Note.Text = grdCustomer.Rows[i].Cells[4].Value.ToString();
            DDL_crd_Limit.Text = grdCustomer.Rows[i].Cells[5].Value.ToString();
            txb_tel_home.Text = grdCustomer.Rows[i].Cells[6].Value.ToString();
            txb_tel_mobile.Text = grdCustomer.Rows[i].Cells[7].Value.ToString();
            txb_tel_office.Text = grdCustomer.Rows[i].Cells[8].Value.ToString();
            txb_tel_other.Text = grdCustomer.Rows[i].Cells[9].Value.ToString();
            txb_fax.Text = grdCustomer.Rows[i].Cells[10].Value.ToString();

            if (i >= 0)
            {
                string vehicle_nos = null;
                vehicle_nos = grdCustomer.Rows[i].Cells[11].Value.ToString();
                if (vehicle_nos != "")
                {
                    string[] strArr = null;
                    char[] splitchar = { ',' };
                    strArr = vehicle_nos.Split(splitchar);
                    for (int count = 0; count <= strArr.Length - 1; count++)
                    {
                        grd_vehicle_no.Rows.Add();
                        grd_vehicle_no.Rows[count].Cells[0].Value = strArr[count];

                    }
                }

               
            }

        }

        private void btn_upate_customer_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("Not implimented!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string name = txb_Cus_Name.Text;
            string Cusno = txb_Cus_No.Text;
            string telMobile = txb_tel_mobile.Text;
            string telOffice = txb_tel_office.Text;
            string telHome = txb_tel_home.Text;
            string telOther = txb_tel_other.Text;
            string fax = txb_fax.Text;
            string address = txb_Cus_Addrs.Text;
            string notes = txb_cus_Note.Text;


            string creditlimit = DDL_crd_Limit.Text.ToString();
            string customercat = drwCustomerCat.Text.ToString();



            int r = grd_vehicle_no.RowCount;
            string no = "";

            for (int i = 1; i < r; i++)
            {
                try
                {
                    string row_item = grd_vehicle_no.Rows[i - 1].Cells[0].Value.ToString();
                    if (i != r - 1)
                    {

                        no += grd_vehicle_no.Rows[i - 1].Cells[0].Value.ToString() + ",";

                    }
                    else
                    {
                        no += grd_vehicle_no.Rows[i - 1].Cells[0].Value.ToString();
                    }
                }

                catch (Exception x)
                {
                }

            }
            //****************************************************************************

            //if customerNo is not null
            if (txb_Cus_No.Text != "")
            {
                //  if customer name is not null
                if (name != "")
                {



                    //update customer infomation customer table 
                    string q5 = "update customer set customername='" + name + "',address='" + address + "',notes='" + notes + "',creditlimit='" + creditlimit + "',customertype='" + customercat + "' where customerno='" + Cusno + "'";
                  
                    bool status1 = middle_access.db_access.UpdateData(q5);


                    //update customer telephone number 
                    string q6 = "update customertel set tel_home='" + telHome + "',tel_mobile='" + telMobile + "',tel_office='" + telOffice + "',tel_other='" + telOther + "',fax='"+ fax +"'where customerNo='" + Cusno + "'";
                    bool status2 = middle_access.db_access.UpdateData(q6);

                    // update customer vehicle 
                    string q7 = "update  customervehicles set vehiclenos='" + no + "' where customerNo='" + Cusno + "'";
                    bool status3 = middle_access.db_access.UpdateData(q7);


                    if (status1 == true && status2 == true && status3 == true) // if data is updated
                    {
                        MessageBox.Show("Customer Successfully Updated !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button  


                        fill_add_customer_grid();
                        DDL_crd_Limit.Text = "0";
                        loadCusNo();
                        clear_all();


                    }

                        // if data is not updated

                    else
                    {
                        MessageBox.Show("Customer not Updated. Please check inputs!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clear_all();
                    }


                }

                //  if customer name is null
                else
                {
                    MessageBox.Show("Please Enter a Customer Name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }


            //if customerNo is null                                             
            else
            {
                MessageBox.Show("Please Check Customer Number!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            cusid = com_customer_id.Text;
            CusName = txtCustomerName.Text;
            if (cusid == "" || CusName == "")
            {
                MessageBox.Show("Enter detalils correctly!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                lbl_customer_section.Text = "Customer Section (Update Customer)";
                panel_search_customer.Visible = false;
                panel_add_customer.Visible = true;
                btn_Add_Customer.Enabled = false;
                btn_upate_customer.Enabled = true;
                grdCustomer.Enabled = true;
                fill_add_customer_grid();
                txb_Cus_No.Text = "";
                clear_all();

                //must call a function in hear to fill upate values in update interface
                for (int i = 0; i < grdCustomer.RowCount; i++)
                {
                    string c_id = grdCustomer.Rows[i].Cells[0].Value.ToString();
                    if (c_id == cusid)
                    {
                        grdCustomer.Rows[i].IsCurrent = true;
                        set_values(i);
                        break;
                    }
                    
                }
            }
            grdFillCustomer_.Visible = false;
        }

        private void btn_invoice_Click(object sender, EventArgs e)
        {
            if (cmb_ReportType.SelectedIndex == 0)
            {
                string invoice_no = com_invoice_no.Text;
                GlobleAccess.invoiceNo = invoice_no;
                string q = "SELECT I.invoiceno,I.invoicedate,C.customername,IL.itemno,IL.itemname,IL.soldqty,IL.retailprice,IL.soldqty*IL.retailprice,I.invoicetotal,IL.discount,I.invoicenote,I.vehicleno FROM invoice I,invoicelines IL, customer C WHERE C.customerno = I.customer AND I.invoiceno = IL.invoiceno AND I.invoiceno = '" + invoice_no + "'";
                DataSet ds_invoice = middle_access.db_access.SelectData(q);
                if (ds_invoice != null)
                {
                    // int i = 0;
                    for (int i = 0; i < ds_invoice.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr_invoice = ds_invoice.Tables[0].Rows[i];

                        string invoice_date = dr_invoice.ItemArray.GetValue(1).ToString();
                        string customer_name = dr_invoice.ItemArray.GetValue(2).ToString();
                        string item_no = dr_invoice.ItemArray.GetValue(3).ToString();
                        string item_name = dr_invoice.ItemArray.GetValue(4).ToString();
                        string qty = dr_invoice.ItemArray.GetValue(5).ToString();
                        string unit_price = dr_invoice.ItemArray.GetValue(6).ToString();
                        string price = dr_invoice.ItemArray.GetValue(7).ToString();
                        string total_price = dr_invoice.ItemArray.GetValue(8).ToString();
                        string discount = dr_invoice.ItemArray.GetValue(9).ToString();
                        string refNo = dr_invoice.ItemArray.GetValue(10).ToString();
                        string vehicleNo = dr_invoice.ItemArray.GetValue(11).ToString();

                        if (discount == "")
                        {
                            discount = " ";
                            price = ((Convert.ToDouble(qty) * Convert.ToDouble(unit_price)) / 100 * (100 - 0)).ToString();
                        }
                        else if (discount != " ")
                        {
                            price = ((Convert.ToDouble(qty) * Convert.ToDouble(unit_price)) / 100 * (100 - Convert.ToDouble(discount))).ToString();
                            discount = discount + " %";

                        }

                        string q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleNo)VALUES('" + invoice_no + "','" + invoice_date + "','" + customer_name + "','" + item_no + "','" + item_name + "','" + qty + "','" + unit_price + ".00','" + price + ".00','" + total_price + ".00','" + discount + "','" + refNo + "','" + vehicleNo + "')";
                        bool status = middle_access.db_access.InsertData(q3);
                        //  i++;
                        if (status != true)
                        {
                            MessageBox.Show("Error1!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }

                    this.Enabled = false;
                    CurrentInvoicePrint ci = new CurrentInvoicePrint();
                    ci.MdiParent = DHNAULA.ActiveForm;
                    ci.Show();


                }
                else
                {
                    MessageBox.Show("Incorrect item No!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (cmb_ReportType.SelectedIndex == 1)
            {
                GlobleAccess.PaymentReportType = "C";
                com_invoice_no.Text = "";
                GlobleAccess.cusID = com_customer_id.Text;             

                this.Enabled = false;               
                Payments_Full_Report pr = new Payments_Full_Report();
                pr.MdiParent = DHNAULA.ActiveForm;
                pr.Show();
            }
            else if (cmb_ReportType.SelectedIndex == 2)
            {
                GlobleAccess.PaymentReportType = "I";
                com_invoice_no.Text = "";
                GlobleAccess.cusID = com_customer_id.Text;
                this.Enabled = false;
                Payments_Full_Report pr = new Payments_Full_Report();
                pr.MdiParent = DHNAULA.ActiveForm;
                pr.Show();
            }
           
        }

        private void btn_repot_Click(object sender, EventArgs e)
        {
            GlobleAccess.PaymentReportType = "C";
            com_invoice_no.Text = "";
            GlobleAccess.cusID = com_customer_id.Text;
            //string q = "SELECT I.invoiceno,I.invoicedate,C.customername,IL.itemno,IL.itemname,IL.soldqty,IL.retailprice,IL.soldqty*IL.retailprice,I.invoicetotal,IL.discount,I.invoicenote,I.vehicleno FROM invoice I,invoicelines IL, customer C WHERE C.customerno = I.customer AND I.invoiceno = IL.invoiceno AND C.customerno='" + com_customer_id.Text + "'";
           // DataSet ds_invoice = middle_access.db_access.SelectData(q);
           // if (ds_invoice != null)
           // {

            //}
                // int i = 0;
                /*    for (int i = 0; i < ds_invoice.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr_invoice = ds_invoice.Tables[0].Rows[i];

                        string invoice_no = dr_invoice.ItemArray.GetValue(0).ToString();
                        string invoice_date = dr_invoice.ItemArray.GetValue(1).ToString();
                        string customer_name = dr_invoice.ItemArray.GetValue(2).ToString();
                        string item_no = dr_invoice.ItemArray.GetValue(3).ToString();
                        string item_name = dr_invoice.ItemArray.GetValue(4).ToString();
                        string qty = dr_invoice.ItemArray.GetValue(5).ToString();
                        string unit_price = dr_invoice.ItemArray.GetValue(6).ToString();
                        string price = dr_invoice.ItemArray.GetValue(7).ToString();                    
                        string total_price = dr_invoice.ItemArray.GetValue(8).ToString();
                        string discount = dr_invoice.ItemArray.GetValue(9).ToString();
                        string refNo = dr_invoice.ItemArray.GetValue(10).ToString();
                        string vehicleNo = dr_invoice.ItemArray.GetValue(11).ToString();
                    
                        if (discount == "")
                        {
                            discount = " ";
                            price = ((Convert.ToDouble(qty) * Convert.ToDouble(unit_price)) / 100 * (100 - 0)).ToString();
                        }
                        else if (discount != " ")
                        {
                            price = ((Convert.ToDouble(qty) * Convert.ToDouble(unit_price)) / 100 * (100 - Convert.ToDouble(discount))).ToString();
                            discount = discount + " %";

                        }

                        string q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleno)VALUES('" + invoice_no + "','" + invoice_date + "','" + customer_name + "','" + item_no + "','" + item_name + "'," + qty + ",'" + unit_price + ".00','" + price + ".00','" + total_price + ".00','" + discount + "','" + refNo + "','" + vehicleNo + "')";
                        bool status = middle_access.db_access.InsertData(q3);
                        //  i++;
                        if (status == true)
                        {
                        
                        }
                        else
                        {
                            MessageBox.Show("Error2!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }*/
          
                this.Enabled = false;
               // Invoice_report ir = new Invoice_report();
               // ir.MdiParent = DHNAULA.ActiveForm;              
                //ir.Show();
                Payments_Full_Report pr = new Payments_Full_Report();
                pr.MdiParent = DHNAULA.ActiveForm;
                pr.Show();
            
           /* else
            {
                MessageBox.Show("Incorrect item No!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (grdCustomer.Visible == true) 
            {
                if (txb_Cus_No.Text == "C 100")
                {
                    txtCustomerName.Text = "Defult";
                }
                grdFillCustomer.Visible = false;
 
            }
        }

        private void txb_Cus_Name_TextChanged(object sender, EventArgs e)
        {
            string name = txb_Cus_Name.Text;
            filter_customer_name(name, grdFillCustomer_);
        }

        private void grdFillCustomer__CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txb_Cus_Name.Text = grdFillCustomer_.Rows[e.RowIndex].Cells[0].Value.ToString();
            txb_Cus_Name.Focus();
            grdFillCustomer_.Visible = false;
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            grdFillCustomer_.Visible = false;
        }

        private void radButton7_Click(object sender, EventArgs e)
        {
            GlobleAccess.PaymentReportType = "I";           
            com_invoice_no.Text = "";
            GlobleAccess.cusID = com_customer_id.Text;
            this.Enabled = false;           
            Payments_Full_Report pr = new Payments_Full_Report();
            pr.MdiParent = DHNAULA.ActiveForm;
            pr.Show();
        }

        private void cmb_ReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_ReportType.SelectedIndex == 0)
            {
                com_invoice_no.Enabled = true;
            }
            else if (cmb_ReportType.SelectedIndex == 1)
            {
                com_invoice_no.Enabled = false;
            }
            else if (cmb_ReportType.SelectedIndex == 2)
            {
                com_invoice_no.Enabled = false;
            }
        }

        private void lbl_More_Reports_Click(object sender, EventArgs e)
        {
            panel_search_customer.Visible = false;
            panel_add_customer.Visible = false;
            panel_Reports.Visible = true;
            radioButton1.Checked = true;
        }

        private void btn_Show_Report_Click(object sender, EventArgs e)
        {
            if (cmb_Report_Type.Text == "")
            {
                MessageBox.Show("Please select a report type!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (radioButton2.Checked && GlobleAccess.FromDate == GlobleAccess.ToDate)
            {
                MessageBox.Show("Please select the date range!","Message",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
                if (cmb_Report_Type.SelectedIndex == 0)
                {
                    GlobleAccess.PaymentReportType = "I";
                    GlobleAccess.FromDate = dateTime_From.Value.Date.ToString("yyyy-MM-dd");
                    GlobleAccess.ToDate = dateTime_To.Value.Date.ToString("yyyy-MM-dd");

                    com_invoice_no.Text = "";
                    GlobleAccess.cusID = com_customer_id.Text;
                    this.Enabled = false;
                    Payments_Full_Credit_Report pr = new Payments_Full_Credit_Report();
                    pr.MdiParent = DHNAULA.ActiveForm;
                    pr.Show();
                }
                else if (cmb_Report_Type.SelectedIndex == 1)
                {
                    GlobleAccess.PaymentReportType = "C";
                    GlobleAccess.FromDate = dateTime_From.Value.Date.ToString("yyyy-MM-dd");
                    GlobleAccess.ToDate = dateTime_To.Value.Date.ToString("yyyy-MM-dd");

                    com_invoice_no.Text = "";
                    GlobleAccess.cusID = com_customer_id.Text;
                    this.Enabled = false;
                    Payments_Full_Credit_Report pr = new Payments_Full_Credit_Report();
                    pr.MdiParent = DHNAULA.ActiveForm;
                    pr.Show();
                }

                else if (cmb_Report_Type.SelectedIndex == 2)
                {
                    GlobleAccess.PaymentReportType = "F";
                    GlobleAccess.FromDate = dateTime_From.Value.Date.ToString("yyyy-MM-dd");
                    GlobleAccess.ToDate = dateTime_To.Value.Date.ToString("yyyy-MM-dd");

                    com_invoice_no.Text = "";
                    GlobleAccess.cusID = com_customer_id.Text;
                    this.Enabled = false;
                    Payments_Full_Credit_Report pr = new Payments_Full_Credit_Report();
                    pr.MdiParent = DHNAULA.ActiveForm;
                    pr.Show();
                }
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            GlobleAccess.ReportRangeType = "A";
            panel_Dates.Enabled = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            GlobleAccess.ReportRangeType = "D";
            panel_Dates.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GlobleAccess.ReportRangeType = "A";
            panel_Dates.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GlobleAccess.ReportRangeType = "D";
            panel_Dates.Enabled = true;
        }

        private void dateTime_From_ValueChanged(object sender, EventArgs e)
        {
            GlobleAccess.FromDate = dateTime_From.Value.Date.ToString("yyyy-MM-dd");
            GlobleAccess.ToDate = dateTime_To.Value.Date.ToString("yyyy-MM-dd");
        }

        private void dateTime_To_ValueChanged(object sender, EventArgs e)
        {
            GlobleAccess.FromDate = dateTime_From.Value.Date.ToString("yyyy-MM-dd");
            GlobleAccess.ToDate = dateTime_To.Value.Date.ToString("yyyy-MM-dd");
        }

      


    }
}
