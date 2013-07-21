using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace TMT_2012
{
    public partial class SearchCustomerGrid : Telerik.WinControls.UI.RadForm
    {

        string cusid = " ";
        string CusName = " ";

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCustomerGrid"/> class.
        /// </summary>
        public SearchCustomerGrid()
        {
            InitializeComponent();
        }

        private void SearchCustomerGrid_Load(object sender, EventArgs e)
        {
            fillCustomerGrid();
        }

        //********************* fillcustomer grid **************************
        private void fillCustomerGrid()
        {
            string q1 = "SELECT * FROM customer";
            DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
            grdSearchCustomer.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
        }

        /// <summary>
        /// to clear the grdSearchCustomer
        /// ds_add_customer_tyre set to null
        /// </summary>
        public void clearGrid()
        {
            grdSearchCustomer.DataSource = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearGrid();
            fillCustomerGrid();
        }

        private void btnPhoneBook_Click(object sender, EventArgs e)
        {
            try
            {

                string q1 = "SELECT * FROM customertel WHERE customerNo ='" + cusid + "'";
                DataSet ds_customer_TelePhone = middle_access.db_access.SelectData(q1);
                if (ds_customer_TelePhone == null)
                    MessageBox.Show("No contact numbers!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else
                {
                    DataRow row_cus_telephone = ds_customer_TelePhone.Tables[0].Rows[0];
                    string cusTelephone = row_cus_telephone.ItemArray.GetValue(0).ToString();

                    //string cuteomerName = grdSearchCustomer.Rows[0].Cells[1].ToString();

                    pnlCusTelephone.Visible = true;

                    lblCusName.Text = CusName;
                    lblCusMobile.Text = row_cus_telephone.ItemArray.GetValue(2).ToString();
                    lblCusHome.Text = row_cus_telephone.ItemArray.GetValue(1).ToString();
                    lblCusOffice.Text = row_cus_telephone.ItemArray.GetValue(3).ToString();
                    lblCusOther.Text = row_cus_telephone.ItemArray.GetValue(4).ToString();
                }
            }
            catch(NullReferenceException)
            {
                throw new Exception();
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
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
            }




        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            pnlCusTelephone.Visible = false;
        }

        private void grdSearchCustomer_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //Search_Customer sc = new Search_Customer();
            //sc.StartPosition = FormStartPosition.CenterParent;
            //sc.ShowDialog();
        }

    }


}


