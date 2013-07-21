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
    public partial class BillingQty : Telerik.WinControls.UI.RadForm
    {
        float item_price;
        DataSet ds_price;
        private string p;

        public BillingQty()
        {
            InitializeComponent();
        }

        public BillingQty(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        private void billingQty_Load(object sender, EventArgs e)
        {
            fill_details();
            get_available_qty();
            get_current_price();
            panel1.Enabled = false;
            lbl_back.Visible = false;
            price_grid_fill();
            radGridView3.Visible = false;
            btn_clear.Visible = false;

        }

        private void price_grid_fill()
        {
            int catagory_id = vehical_category_data.get_catagory_id();
            string q = "SELECT price AS Price,last_update AS Last_Update FROM price WHERE category_id = " + catagory_id + "";
            ds_price = middle_access.db_access.SelectData(q);
            if (ds_price != null)
            {
                radGridView3.DataSource = ds_price.Tables[0].DefaultView;
                radGridView3.Visible = true;
                btn_clear.Visible = true;
            }
            else
            {
                radGridView3.DataSource = null;
            }

        }

        private void get_current_price()
        {
            int catagory_id = vehical_category_data.get_catagory_id();
            string q = "SELECT * FROM add_vehical_tyre WHERE t_stok_id = " + catagory_id + "";
            DataSet ds_item_price = middle_access.db_access.SelectData(q);

            if (ds_item_price != null)
            {
                DataRow row_catagory_id = ds_item_price.Tables[0].Rows[0];
                //  float item_price = (float)(Convert.ToDouble(row_catagory_id.ItemArray.GetValue(10)));
                item_price = (float)(Convert.ToDouble(row_catagory_id.ItemArray.GetValue(10)));
                lbl_current_prize.Text = "Rs : " + item_price.ToString();

            }
            else
            {
                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //******** get_available_qty ***************
        private void get_available_qty()
        {
            int catagory_id = vehical_category_data.get_catagory_id();
            string q = "SELECT * FROM add_vehical_tyre WHERE t_stok_id = " + catagory_id + "";
            DataSet ds_catagory_id = middle_access.db_access.SelectData(q);

            if (ds_catagory_id != null)
            {
                DataRow row_catagory_id = ds_catagory_id.Tables[0].Rows[0];
                int available_qty = Convert.ToInt32(row_catagory_id.ItemArray.GetValue(9).ToString());
                if (available_qty == 0)
                {
                    lbl_Aq.ForeColor = Color.Red;
                }
            
                lbl_Aq.Text = available_qty.ToString();

            }
            else
            {
                lbl_Aq.ForeColor = Color.Red;
                lbl_Aq.Text = "0";
            }
        }


        //******** fill_details() ***************
        private void fill_details()
        {
            //lbl_brand.Text = vehical_category_data.brand;
            //lbl_make.Text = vehical_category_data.make;
            //lbl_ply_rate.Text = vehical_category_data.ply_rate.ToString();
            //lbl_size.Text = vehical_category_data.size;
            //lbl_thread_pattern.Text = vehical_category_data.thread_pattern;
            //lbl_tube_type.Text = vehical_category_data.tube;
            //lbl_type.Text = vehical_category_data.type;
        }

        private void addToBill_Click(object sender, EventArgs e)
        {
            Form f = (Form)Application.OpenForms["Billing"];
            f.Enabled = true;
            this.Close();
            
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

        }

    }
}
