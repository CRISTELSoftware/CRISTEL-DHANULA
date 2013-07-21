using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;
 
 


namespace TMT_2012
{
    public partial class Search_Customer : Telerik.WinControls.UI.RadForm
    {
       int status = 0;


       /// <summary>
       /// Initializes a new instance of the <see cref="Search_Customer"/> class.
       /// </summary>
        public Search_Customer()
        {
            InitializeComponent();
        }



        private void getCustomerDetails(string no)
        {
           
                string q = "SELECT * FROM customer WHERE customerno = '"+no+"'";
                DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q);

                string q1 = "SELECT * FROM customertel WHERE customerNo = '" + no + "'";
                DataSet ds_add_customer_tel = middle_access.db_access.SelectData(q1);


                //if (ds_add_customer_tyre == null)
                //{
                //    MessageBox.Show(" Name Not Found !");
                //}
                //else
                //{
                    string cusno = ds_add_customer_tyre.Tables[0].Rows[0][0].ToString();
                    string cusaddress = ds_add_customer_tyre.Tables[0].Rows[0][2].ToString();
                    string cusnote = ds_add_customer_tyre.Tables[0].Rows[0][3].ToString();
                    string creditlimit = ds_add_customer_tyre.Tables[0].Rows[0][4].ToString();
                    string customertype = ds_add_customer_tyre.Tables[0].Rows[0][5].ToString();

                   
                    string home      = ds_add_customer_tel.Tables[0].Rows[0][1].ToString();
                    string mobile    = ds_add_customer_tel.Tables[0].Rows[0][2].ToString();
                    string office    = ds_add_customer_tel.Tables[0].Rows[0][3].ToString();
                    string other     = ds_add_customer_tel.Tables[0].Rows[0][4].ToString();

                  


                    txbCusNo.Text = cusno;
                    txb_Cus_Addrs.Text = cusaddress;
                    txb_cus_Note.Text = cusnote;
                    cmdCreditLimit.Text = creditlimit;
                    drwCustomerCat.Text = customertype;

                    txb_tel_home.Text = home;
                    txb_tel_mobile.Text = mobile;
                    txb_tel_office.Text = office;
                    txb_tel_other.Text = other;

              //  }
        }

        /// <summary>
        /// clear text boxes
        /// </summary>
        public void clear()
        {
            cmbCusName.SelectedText = null;
            txbCusNo.Text = "";
            txb_tel_other.Text = "";
            txb_tel_office.Text = "";
            txb_tel_mobile.Text = "";
            txb_tel_home.Text = "";
            txb_cus_Note.Text = "";
            txb_Cus_Addrs.Text = "";
            cmdCreditLimit.Text = "";

        }

        //**************** update Butten Click ******************************
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////



            string cusno = txbCusNo.Text ;
            string cusaddress=txb_Cus_Addrs.Text ;
            string cusnote = txb_cus_Note.Text;
            string creditlimit = cmdCreditLimit.Text ;
            string customertype=drwCustomerCat.Text ;

            string home=txb_tel_home.Text ;
            string mobile=txb_tel_mobile.Text ;
            string office=txb_tel_office.Text;
            string other=txb_tel_other.Text ;





            ///////////////////////////////////////////////////////////////

            // check name is null
            if (cusno != "")
            {
                //**************Update Quary****************************
                string q1 = "UPDATE customer SET address = '" + cusaddress + "',notes = '" + cusnote + "',creditlimit = '" + creditlimit + "',customertype = '" + customertype + "' where customerno = '" + cusno + "'";
                bool status1 = middle_access.db_access.UpdateData(q1);

                string q2 = "UPDATE customertel SET tel_home = '" + home + "',tel_mobile = '" + mobile + "',tel_office = '" + office + "',tel_other = '" + other + "' where customerno = '" + cusno + "'";
                bool status2 = middle_access.db_access.UpdateData(q2);


                //UPDATE `customertel` SET `tel_home`='00000',
                //`tel_mobile`='8888', `tel_office`='999', `tel_other`='8888' WHERE `customerNo`='1' LIMIT 1;

                // error handling..................
                if (status1 == true)
                {
                    MessageBox.Show("updated Successfully");
                }
            }
            else
            {
                MessageBox.Show("please Search a Customer first!");
            }

        }

        // ***************** Delete Customer ****************************
        private void btnDelete_Click(object sender, EventArgs e)
        {

            string name = cmbCusName.SelectedText.ToString();
            string cusNo = txbCusNo.Text;
           // string telephone = txb_tele_no.Text;
            string address = txb_Cus_Addrs.Text;
            string notes = txb_cus_Note.Text;
            //cmbCusName.SelectedText = " ";
            //string creditlimit = DDL_crd_Limit.SelectedItem.ToString();

            if (name != "")
            {
                // message shows to  yes or no.............................        
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {


                    // delete quary......................

                    string q1= "DELETE FROM customertel WHERE customerNo='" + cusNo + "'";
                    bool status1 = middle_access.db_access.DeleteData(q1);
                    string q2 = "DELETE FROM customervehicles WHERE customerno ='" + cusNo + "'";
                    bool status2 = middle_access.db_access.DeleteData(q2);
                    string q3 = "DELETE FROM customer  WHERE customerno ='" + cusNo + "'";
                    bool status3 = middle_access.db_access.DeleteData(q3);

                    //.....if status true show successfully delete messages..........
                    if (status1 == true && status2 == true && status2 == true)
                    {
                        MessageBox.Show("Successfully Deleted", "Message", MessageBoxButtons.OK);
                        clear();
                        fillCusName();
                    }
                }

            }
            else
            {
                MessageBox.Show("Please Search the Name first", "Message", MessageBoxButtons.OK);
            }

            //clear();
        }

        private void Search_Customer_Load(object sender, EventArgs e)
        {
           
            fillCusName();

        }


        private void fillCusName()
        {

            string q1 = "SELECT customername,customerno FROM customer";
            DataSet ds_made1 = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table       

            if (ds_made1 != null) // if data set is not null
            {

                cmbCusName.DataSource = ds_made1.Tables[0];       //fill table 
                cmbCusName.DisplayMember = "customername";
                cmbCusName.ValueMember = "customerno";
                

            }
            else

                cmbCusName.DataSource = null; //fill table 
        }
 
        private void cmbCusName_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

          

   
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

        
        }

        private void cmbCusName_SelectedValueChanged(object sender, EventArgs e)
        {
          if (status == 0)
            {
               status = 1;
            }
         else
            {

                string a = cmbCusName.Text; 
                if(a != null)
                {
                    string b = cmbCusName.SelectedValue.ToString();
                    getCustomerDetails(b);                
                }
            }

        }

        private void radButton10_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }



    }     
}


            




        

        
    

