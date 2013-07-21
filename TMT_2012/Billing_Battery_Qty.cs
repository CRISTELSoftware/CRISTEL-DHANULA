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
    public partial class Billing_Battery_Qty : Form
    {
        float item_price;
        DataSet ds_price;
        public Billing_Battery_Qty()
        {
            InitializeComponent();
        }

        private void battery_qty_Load(object sender, EventArgs e)
        {
            check_din_normal();
            fill_details();
            
            panel1.Enabled = false;
            lbl_back.Visible = false;
            price_grid_fill();
            radGridView3.Visible = false;
            btn_clear.Visible = false;

            string[] currentPrice = lbl_current_prize.Text.ToString().Split(':');
            item_price = float.Parse(currentPrice[1].Trim());
        }


        /// <summary>
        /// Check_din_normals this instance.
        /// </summary>
        public void check_din_normal()
        {
            
            if (billing_battery_category_data.din_check_box_val == "DIN")
            {
                lbl_din_normal.Text = "DIN Battery";
            }
            else
            {
                lbl_din_normal.Text = "Normal Battery";
            }
        }

        /// <summary>
        /// Price_grid_fills this instance.
        /// </summary>
        public void price_grid_fill()
        {
            int catagory_id = billing_battery_category_data.get_battery_catagory_id();
            string q = "SELECT price AS Price,last_update AS Last_Update FROM battery_price WHERE category_id = " + catagory_id + " AND battery_type = '" + lbl_din_normal.Text + "' ";
            //ds_price = Data.DataAccess.GetData(q);
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

        public void fill_details()
        {
            lbl_itemno.Text = billing_battery_category_data.brand;
            lbl_size.Text = billing_battery_category_data.size; ;
            lbl_voltage.Text = billing_battery_category_data.voltage;            
            lbl_amps.Text = billing_battery_category_data.amps;
            lbl_type.Text = billing_battery_category_data.type;
            string types = billing_battery_category_data.type;
            if (types == "M")
            {
                lbl_type.Text = "Maintain";
            }
            else
            {
                lbl_type.Text = "Maintain Free";
            }

            int battery_qty = Convert.ToInt32(billing_battery_category_data.qty);
            
                if (battery_qty == 0)
                {
                    lbl_Aq.ForeColor = Color.Red;
                }
             
                lbl_Aq.Text = battery_qty.ToString();



                lbl_current_prize.Text = "Rs : " + billing_battery_category_data.price.ToString();

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

        private void radButton1_Click(object sender, EventArgs e)
        {
            billing_battery_category_data.invoiceQty = "";
            Form f = (Form)Application.OpenForms["Billing"];
            f.Enabled = true;
            this.Close();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            string[] val = lbl_current_prize.Text.Split(':');
            billing_battery_category_data.invoiceQty = txt_qty.Text;
            if (Convert.ToInt32(val[1]) != 0)
                billing_battery_category_data.unitPrice = val[1].ToString();
            else
                billing_battery_category_data.unitPrice = item_price.ToString();
            billing_battery_category_data.statusPass2Forms = true;
          
           // int catagory_id = battery_category_data.get_battery_catagory_id();

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

                float price = (float)(Convert.ToDouble(billing_battery_category_data.price));


                float discounted_price = (price - (price * ((float)(Convert.ToDouble(txt_discount.Text)) / 100)));
                MessageBox.Show("Customer Price : " + "Rs " + discounted_price + " /=", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_discount.Text = "";
             //   txtDiscountPrice.Text = discounted_price.ToString();

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
                    int cat_id = billing_battery_category_data.get_battery_catagory_id();
                    float unit_price = (float)(Convert.ToDouble(txt_price.Text));

                    string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
                    string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

                    string q1 = "INSERT INTO battery_price VALUES(" + cat_id + "," + (float)(Convert.ToDouble(txt_price.Text)) + ",'" + date + "','" + time + "','" + lbl_din_normal.Text + "')";
                    bool status =middle_access.db_access.InsertData(q1);
                    if (status == true)
                    {
                        if (lbl_din_normal.Text == "Normal Battery")
                        {
                            string q = "UPDATE add_battery SET b_prize = " + unit_price + "  WHERE b_stok_id = " + cat_id + " ";
                            middle_access.db_access.UpdateData(q);
                        }
                        else
                        {
                            string q = "UPDATE din SET price = " + unit_price + "  WHERE din_id = " + cat_id + " ";
                            middle_access.db_access.UpdateData(q);
                        }
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

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int cat_id = billing_battery_category_data.get_battery_catagory_id();
                string q = "DELETE FROM battery_price WHERE category_id =" + cat_id + " AND battery_type = '" + lbl_din_normal.Text + "'";
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

