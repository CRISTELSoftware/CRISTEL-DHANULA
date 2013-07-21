﻿using System;
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
    public partial class Billing_Qty_Cycle : Form
    {

        float item_price;
        DataSet ds_price;
        /// <summary>
        /// Initializes a new instance of the <see cref="Billing_Qty_Cycle"/> class.
        /// </summary>
        public Billing_Qty_Cycle()
        {
            InitializeComponent();
        }

        private void qty_cycle_Load(object sender, EventArgs e)
        {
            fill_details();
            get_available_qty();
            get_current_price();
            panel1.Enabled = false;
            lbl_back.Visible = false;
            price_grid_fill();
            radGridView3.Visible = false;
            btn_clear.Visible = false;

            string[] currentPrice = lbl_current_prize.Text.ToString().Split(':');
            item_price = float.Parse(currentPrice[1].Trim());
        }

        /// <summary>
        /// Price_grid_fills this instance.
        /// </summary>
        public void price_grid_fill()
        {
            int catagory_id = Billing_Cycle_Category_Data.get_catagory_id();
            string q = "SELECT price AS Price,last_update AS Last_Update FROM price_cycle WHERE category_id = " + catagory_id + "";
            ds_price = middle_access.db_access.SelectData(q);
            if (ds_price != null)
            {
                radGridView3.DataSource = ds_price.Tables[0].DefaultView;
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
            int catagory_id = Billing_Cycle_Category_Data.get_catagory_id();
            string q = "SELECT * FROM add_cycle_tyre WHERE t_stok_id = " + catagory_id + "";
            DataSet ds_item_pricee = middle_access.db_access.SelectData(q);

            if (ds_item_pricee != null)
            {
                DataRow row_catagory_id = ds_item_pricee.Tables[0].Rows[0];
                double item_pricee = (double)(Convert.ToDouble(row_catagory_id.ItemArray.GetValue(11)));

               // lbl_current_prize.Text = "Rs : " + item_pricee.ToString();
                lbl_current_prize.Text =item_pricee.ToString();

            }
            else
            {
                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// Fill_detailses this instance.
        /// </summary>
        public void fill_details()
        {
            lbl_brand.Text = Billing_Cycle_Category_Data.brand;
            lbl_make.Text = Billing_Cycle_Category_Data.make;
            lbl_ply_rate.Text = Billing_Cycle_Category_Data.ply_rate;
            lbl_size.Text = Billing_Cycle_Category_Data.size;
            lbl_thread_pattern.Text = Billing_Cycle_Category_Data.thread_pattern;
            lbl_tube_type.Text = Billing_Cycle_Category_Data.tube;
            lbl_tyre_pattern.Text = Billing_Cycle_Category_Data.tyre_pattern;
            lbl_side.Text = Billing_Cycle_Category_Data.side;
    

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Billing_Cycle_Category_Data.qty = "";
            Form f = (Form)Application.OpenForms["Billing"];
            f.Enabled = true;
            this.Close();
        }

        /// <summary>
        /// Get_available_qties this instance.
        /// </summary>
        public void get_available_qty()
        {
            int catagory_id = Billing_Cycle_Category_Data.get_catagory_id();
            string q = "SELECT * FROM add_cycle_tyre WHERE t_stok_id = " + catagory_id + "";
            DataSet ds_catagory_id = middle_access.db_access.SelectData(q);

            if (ds_catagory_id != null)
            {
                DataRow row_catagory_id = ds_catagory_id.Tables[0].Rows[0];
                int available_qty = Convert.ToInt32(row_catagory_id.ItemArray.GetValue(10).ToString());
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

    

        private void radButton2_Click(object sender, EventArgs e)
        {




            //if (MessageBox.Show("Are you sure you want to reduce stock?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    int catagory_id = cycle_category_data.get_catagory_id();
            //    bool check;
            //    int a;
            //    check = int.TryParse(txt_qty.Text, out a);

            //    if (txt_qty.Text == "")
            //    {
            //        MessageBox.Show("Enter qty!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //    else if (!check)
            //    {
            //        MessageBox.Show("Enter correct qty!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        txt_qty.Text = "";

            //    }
            //    else if (Convert.ToInt32(txt_qty.Text) < 0)
            //    {
            //        MessageBox.Show("Qty can not less than Zero!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        txt_qty.Text = "";
            //    }


            //    else
            //    {
            //        int cat_id = cycle_category_data.get_catagory_id();

            //        string q0 = "SELECT * FROM add_cycle_tyre WHERE t_stok_id =" + cat_id + "";
            //        DataSet ds_qty_table = Data.DataAccess.GetData(q0);
            //        if (ds_qty_table == null)
            //        {
            //            MessageBox.Show("Can not reduce!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);//show message box with ok button

            //        }
            //        else
            //        {
            //            DataRow row_catagory_id = ds_qty_table.Tables[0].Rows[0];
            //            int available_qty = Convert.ToInt32(row_catagory_id.ItemArray.GetValue(10).ToString());

            //            if (available_qty < Convert.ToInt32(txt_qty.Text))
            //            {

            //                MessageBox.Show("Available stock is insufficient!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);//show message box with ok button
            //                txt_qty.Text = "";
            //            }
            //            else
            //            {
            //                int new_qty = available_qty - Convert.ToInt32(txt_qty.Text);


            //                string q = "SELECT * FROM qty_cycle WHERE category_id = " + catagory_id + "";
            //                DataSet ds_catagory_id = Data.DataAccess.GetData(q);

            //                string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            //                string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            //                string action = "-";

            //                string q1 = "INSERT INTO qty_cycle VALUES(" + cat_id + "," + Convert.ToInt32(lbl_Aq.Text) + ",'" + action + "'," + Convert.ToInt32(txt_qty.Text) + ", '" + date + "','" + time + "')";

            //                bool status = Data.DataAccess.ExecuteData(q1);
            //                if (status == true)
            //                {
            //                    string q2 = "UPDATE add_cycle_tyre SET qty = " + new_qty + "  WHERE t_stok_id = " + cat_id + " ";
            //                    Data.DataAccess.ExecuteData(q2);
            //                    MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
            //                    if (new_qty == 0)
            //                    {
            //                        lbl_Aq.ForeColor = Color.Red;
            //                    }

            //                    lbl_Aq.Text = new_qty.ToString();
            //                    txt_qty.Text = "";
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }




            //            }
            //        }
            //    }

            //}
        }

        private void lbl_back_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            lbl_back.Visible = false;
            lbl_front.Visible = true;
            radGridView3.Visible = false;
            btn_clear.Visible = false;
        }

        private void lbl_front_Click(object sender, EventArgs e)
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
                    int cat_id = Billing_Cycle_Category_Data.get_catagory_id();
                    float unit_price = (float)(Convert.ToDouble(txt_price.Text));

                    string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
                    string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

                    string q1 = "INSERT INTO price_cycle VALUES(" + cat_id + "," + (float)(Convert.ToDouble(txt_price.Text)) + ",'" + date + "','" + time + "')";
                    bool status = middle_access.db_access.InsertData(q1);
                    if (status == true)
                    {
                        string q = "UPDATE add_cycle_tyre SET unit_prize = " + unit_price + "  WHERE t_stok_id = " + cat_id + " ";
                        middle_access.db_access.UpdateData(q);
                        lbl_current_prize.Text = "Rs : " + String.Format("{0:0.00##}", txt_price.Text);
                        item_price = float.Parse(txt_price.Text);
                        txt_price.Text = "";
                        price_grid_fill();
                        radGridView3.Visible = true;
                        btn_clear.Visible = true;
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
                int cat_id = Billing_Cycle_Category_Data.get_catagory_id();
                string q = "SELECT * FROM add_cycle_tyre WHERE t_stok_id =" + cat_id + "";
                DataSet ds_current_price = middle_access.db_access.SelectData(q);

                DataRow row_current_price = ds_current_price.Tables[0].Rows[0];
                float price = (float)(Convert.ToDouble(row_current_price.ItemArray.GetValue(11).ToString()));


                float discounted_price = (price - (price * ((float)(Convert.ToDouble(txt_discount.Text)) / 100)));
                MessageBox.Show("Customer Price : " + "Rs " + discounted_price + " /=", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_discount.Text = "";

            }

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int cat_id = Billing_Cycle_Category_Data.get_catagory_id();
                string q = "DELETE FROM price_cycle WHERE category_id =" + cat_id + "";
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

        private void btnAddCycleCategory_Click(object sender, EventArgs e)
        {
         
            Billing_Cycle_Category_Data.qty = txt_qty.Text;
            Billing_Cycle_Category_Data.unitPricee = item_price.ToString();
            Billing_Cycle_Category_Data.statusPass2Forms = true;

         //   int catagory_id = cycle_category_data.get_catagory_id();
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
    }
}