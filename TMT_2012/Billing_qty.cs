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
    public partial class Billing_qty : Form
    {
        float item_price;
        DataSet ds_price;
        private string p;

        public Billing_qty()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Billing_qty"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        public Billing_qty(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            vehical_category_data.qty = "";
            Form f = (Form)Application.OpenForms["Billing"];
            f.Enabled = true;            
            this.Close();
            

        }

        private void qty_Load(object sender, EventArgs e)
        {
            fill_details();
            get_available_qty();
            get_current_price();            
            panel1.Enabled = false;
            lbl_back.Visible = false;
            price_grid_fill();
            radGridView3.Visible = false;
            btn_clear.Visible = false;
            txt_qty.Focus();

            string[] currentPrice = lbl_current_prize.Text.ToString().Split(':');
            item_price = float.Parse(currentPrice[1].Trim());

        }

        //**************** Price Grid Fill *********************************
        // in here it will show the price histry grid in the UI panel
        public void price_grid_fill()
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

        /// <summary>
        /// Get_current_prices this instance.
        /// </summary>
        public void get_current_price()
        {
            int catagory_id = vehical_category_data.get_catagory_id();
            string q = "SELECT * FROM add_vehical_tyre WHERE t_stok_id = " + catagory_id + "";
            DataSet ds_item_price = middle_access.db_access.SelectData(q);

            if (ds_item_price != null)
            {
                DataRow row_catagory_id = ds_item_price.Tables[0].Rows[0];
              //  float item_price = (float)(Convert.ToDouble(row_catagory_id.ItemArray.GetValue(10)));
                item_price=(float)(Convert.ToDouble(row_catagory_id.ItemArray.GetValue(10)));
                lbl_current_prize.Text = "Rs : " + item_price.ToString();
                
            }
            else
            {
                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }





        public void fill_details()
        {
            lbl_brand.Text = vehical_category_data.brand;
            lbl_make.Text = vehical_category_data.make;
            lbl_ply_rate.Text = vehical_category_data.ply_rate.ToString();
            lbl_size.Text = vehical_category_data.size;
            lbl_thread_pattern.Text = vehical_category_data.thread_pattern;
            lbl_tube_type.Text = vehical_category_data.tube;
            lbl_type.Text = vehical_category_data.type;

        }

        /// <summary>
        /// Get_available_qties this instance.
        /// </summary>
        public void get_available_qty()
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
                //else if (available_qty <= 4)
                //{
                //    radDesktopAlert1.ContentText = "Available Srock is low!";
                //    radDesktopAlert1.Show();
                //}
                lbl_Aq.Text = available_qty.ToString();
                
            }
            else
            {
                lbl_Aq.ForeColor = Color.Red;
                lbl_Aq.Text = "0";
            }

        }

    

        private void radButton2_Click(object sender, EventArgs e)
        {
          
                vehical_category_data.qty = txt_qty.Text;
                //  vehical_category_data.newPrice = txt_price.Text;
                vehical_category_data.unitPrice = item_price.ToString();
                vehical_category_data.statusPass2Forms = true;

              //  int catagory_id = vehical_category_data.get_catagory_id();
                bool check;
                int a;
                check = int.TryParse(txt_qty.Text, out a);

                if (txt_qty.Text == "" || Convert.ToInt32(txt_qty.Text) == 0)
                {
                    MessageBox.Show("Enter qty!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_qty.Text = "";
                }
                else if (!check)
                {
                    MessageBox.Show("Enter correct qty!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_qty.Text = "";

                }
                else if (Convert.ToInt32(txt_qty.Text) < 0)
                {
                    MessageBox.Show("Qty can not less than Zero!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_qty.Text = "";
                }


                else
                {

                    Form f = (Form)Application.OpenForms["Billing"];
                    f.Enabled = true;
                    this.Close();
                        
                   
                }



            
        }

        private void label12_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            lbl_back.Visible = true;
            lbl_front.Visible = false;
            if (ds_price != null)
            {
                radGridView3.Visible = true;
                btn_clear.Visible = true;
            }
        }

        private void lbl_back_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            lbl_back.Visible = false;
            lbl_front.Visible = true;
            radGridView3.Visible = false;
            btn_clear.Visible = false;
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            bool check;
            float a;
            check = float.TryParse(txt_price.Text, out a);

            if (txt_price.Text == "")
            {
                MessageBox.Show("Enter new price!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (!check)
            {
                MessageBox.Show("Enter correct price!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_price.Text = "";
            }

            else
            {
                if (MessageBox.Show("Are you sure you want to add price?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int cat_id = vehical_category_data.get_catagory_id();
                    float unit_price = (float)(Convert.ToDouble(txt_price.Text));

                    string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
                    string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

                    string q1 = "INSERT INTO price VALUES(" + cat_id + "," + (float)(Convert.ToDouble(txt_price.Text)) + ",'" + date + "','" + time + "')";
                    bool status = middle_access.db_access.InsertData(q1);
                    if (status == true)
                    {
                        string q = "UPDATE add_vehical_tyre SET unit_prize = " + unit_price + "  WHERE t_stok_id = " + cat_id + " ";
                        middle_access.db_access.UpdateData(q);
                        lbl_current_prize.Text = "Rs : " + String.Format("{0:0.00##}", txt_price.Text);
                        item_price = float.Parse(txt_price.Text);
                        txt_price.Text = "";
                        price_grid_fill();
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            bool check;
            float a;
            check = float.TryParse(txt_discount.Text, out a);

            if (txt_discount.Text == "")
            {
                MessageBox.Show("Enter discount!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!check)
            {
                MessageBox.Show("Enter correct value!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_discount.Text = "";
            }
            else
            {
                int cat_id = vehical_category_data.get_catagory_id();
                string q = "SELECT * FROM add_vehical_tyre WHERE t_stok_id =" + cat_id + "";
                DataSet ds_current_price = middle_access.db_access.SelectData(q);

                DataRow row_current_price = ds_current_price.Tables[0].Rows[0];
                float price = (float)(Convert.ToDouble(row_current_price.ItemArray.GetValue(10).ToString()));


                float discounted_price = (price - (price * ((float)(Convert.ToDouble(txt_discount.Text)) / 100)));
                MessageBox.Show("Customer Price : " + "Rs " + discounted_price + " /=", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_qty_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Aq_Click(object sender, EventArgs e)
        {

        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_tube_type_Click(object sender, EventArgs e)
        {

        }

        private void lbl_type_Click(object sender, EventArgs e)
        {

        }

        private void lbl_make_Click(object sender, EventArgs e)
        {

        }

        private void lbl_thread_pattern_Click(object sender, EventArgs e)
        {

        }

        private void lbl_ply_rate_Click(object sender, EventArgs e)
        {

        }

        private void lbl_size_Click(object sender, EventArgs e)
        {

        }

        private void lbl_brand_Click(object sender, EventArgs e)
        {

        }

        private void lbl_available_qty_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Are you sure you want to clear?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int cat_id = vehical_category_data.get_catagory_id();
                string q = "DELETE FROM price WHERE category_id =" + cat_id + "";
                bool status = middle_access.db_access.DeleteData(q);
                if (status == true)
                {
                    MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    radGridView3.DataSource = null;
                    radGridView3.Visible = false;
                    btn_clear.Visible = false;
                    price_grid_fill();
                }
                else
                {
                    MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    
    }

        }

    
    

