using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using MySql.Data.MySqlClient;

namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Add_New_Customer : Telerik.WinControls.UI.RadForm
    {
        string customerNumber;//use for the Customer No auto genaration 

        /// <summary>
        /// Initializes a new instance of the <see cref="Add_New_Customer"/> class.
        /// </summary>
        public Add_New_Customer()
        {
            InitializeComponent();
        }

        private void btn_Add_Customer_Click(object sender, EventArgs e)
        {
           
            
                string name = txb_Cus_Name.Text;
                string Cusno = txb_Cus_No.Text;
                string telMobile = txb_tel_mobile.Text;
                string telOffice = txb_tel_office.Text;
                string telHome = txb_tel_home.Text;
                string telOther = txb_tel_other.Text;
                string address = txb_Cus_Addrs.Text;
                string notes = txb_cus_Note.Text;
                string vehicle1 = txb_Veh_No1.Text;
                string vehicle2 = txb_Veh_No2.Text;
                string vehicle3 = txb_Veh_No3.Text;
                string vehicle4 = txb_Veh_No4.Text;
        
                    string creditlimit = DDL_crd_Limit.SelectedItem.ToString();
                    string customercat = drwCustomerCat.SelectedItem.ToString();
                



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
                                string q6 = "INSERT INTO customertel (customerNo,tel_home,tel_mobile,tel_office,tel_other)  VALUES('" + Cusno + "','" + telHome + "','" + telMobile+ "','" + telOffice + "','" + telOther + "')";
                                bool status2 = middle_access.db_access.InsertData(q6);

                                // insert into customer vehicle 
                                string q7 = "INSERT INTO customervehicles (customerNo,vehicleno1,vehicleno2,vehicleno3,vehicleno4)  VALUES('" + Cusno + "','" + vehicle1 + "','" + vehicle2 + "','" + vehicle3 + "','" + vehicle4 + "')";
                                bool status3 = middle_access.db_access.InsertData(q7);


                                if (status1 == true && status2 == true && status3 == true) // if data is insert
                                {
                                    MessageBox.Show("New Customer Successfully Added !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button  

                                    //to increment the customer no
                                    int nextCusNo = (Convert.ToInt32(customerNumber)) + 1;
                                    string q = "UPDATE autoincrem SET maxno = " + nextCusNo + "  WHERE tablename = 'C'";
                                    bool status = middle_access.db_access.UpdateData(q);
                                }

                                    // if data is not insert
                                
                                else 
                                {
                                    MessageBox.Show("Customer Already Exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Add_New_Customer_Load(object sender, EventArgs e)
        {
            loadCusNo();
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

        private void radLabel3_Click(object sender, EventArgs e)
        {

        }

        private void txb_tele_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void radButton10_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }
    }
}
