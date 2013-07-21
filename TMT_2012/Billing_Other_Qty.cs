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
    public partial class Billing_Other_Qty : Form
    {
        float item_price;
        //DataSet ds_price;
        /// <summary>
        /// Initializes a new instance of the <see cref="Billing_Other_Qty"/> class.
        /// </summary>
        public Billing_Other_Qty()
        {
            InitializeComponent();
        }

        private void battery_qty_Load(object sender, EventArgs e)
        {
            //check_din_normal();
            fill_details();
            string[] currentPrice = lbl_current_prize.Text.ToString().Split(':');
            item_price = float.Parse(currentPrice[1].Trim());
            //panel1.Enabled = false;
            //lbl_back.Visible = false;
            //price_grid_fill();
            //radGridView3.Visible = false;
            //btn_clear.Visible = false;
        }


        public void check_din_normal()
        {
            
            //if (battery_category_data.din_check_box_val == "DIN")
            //{
            //    lbl_din_normal.Text = "DIN Battery";
            //}
            //else
            //{
            //    lbl_din_normal.Text = "Normal Battery";
            //}
        }

        /// <summary>
        /// Price_grid_fills this instance.
        /// </summary>
        public void price_grid_fill()
        {
            //int catagory_id = battery_category_data.get_battery_catagory_id();
            //string q = "SELECT price AS Price,last_update AS Last_Update FROM battery_price WHERE category_id = " + catagory_id + " AND battery_type = '" + lbl_din_normal.Text + "' ";
            //ds_price = Data.DataAccess.GetData(q);
            //if (ds_price != null)
            //{
            //    radGridView3.DataSource = ds_price.Tables[0].DefaultView;
            //    radGridView3.Visible = true;
            //    btn_clear.Visible = true;
            //}
            //else
            //{
            //    radGridView3.DataSource = null;
            //}

        }

        public void fill_details()
        {
            lbl_itemNo.Text = Billing_Other_Catagory_Data.itemno;
            lbl_itemName.Text = Billing_Other_Catagory_Data.itemname;
            lbl_itemCat.Text = Billing_Other_Catagory_Data.catagory;
            lbl_AvailableQty.Text = Billing_Other_Catagory_Data.AvailableQty;
            lbl_itemCat.Text = Billing_Other_Catagory_Data.catagory;
            lbl_current_prize.Text = Billing_Other_Catagory_Data.unitPrice;

            //lbl_type.Text = battery_category_data.type;
            //string types = battery_category_data.type;
            //if (types == "M")
            //{
            //    lbl_type.Text = "Maintain";
            //}
            //else.te
            //{
            //    lbl_type.Text = "Maintain Free";
            //}

            //int battery_qty = Convert.ToInt32(battery_category_data.qty);
            
            //    if (battery_qty == 0)
            //    {
            //        lbl_Aq.ForeColor = Color.Red;
            //    }
             
            //    lbl_Aq.Text = battery_qty.ToString();

         

            //lbl_current_prize.Text = "Rs : " + battery_category_data.price.ToString();

        }

        private void lbl_back_Click(object sender, EventArgs e)
        {
            //panel1.Enabled = false;
            //lbl_back.Visible = false;
            //lbl_front.Visible = true;
            //radGridView3.Visible = false;
            //btn_clear.Visible = false;
        }

        private void lbl_front_Click(object sender, EventArgs e)
        {
            //panel1.Enabled = true;
            //lbl_back.Visible = true;
            //lbl_front.Visible = false;
            //if (ds_price != null)
            //{
            //    radGridView3.Visible = true;
            //    btn_clear.Visible = true;
            //}
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Form f = (Form)Application.OpenForms["Billing"];
            f.Enabled = true;
            this.Close();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            Billing_Other_Catagory_Data.qty = txt_qty.Text;

            Billing_Other_Catagory_Data.newPrice = txt_price.Text;

           // other_catagory_data.unitPrice = item_price.ToString();

            Billing_Other_Catagory_Data.statusPass2Forms = true;

            Form f = (Form)Application.OpenForms["Billing"];
            f.Enabled = true;
            this.Close();

            //if (MessageBox.Show("Are you sure you want to add stock?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    int catagory_id = battery_category_data.get_battery_catagory_id();

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
            //         int cat_id = battery_category_data.get_battery_catagory_id();
            //        // string q0 = "SELECT * FROM add_battery WHERE b_stok_id =" + cat_id + "";
            //       //  DataSet ds_qty_table = Data.DataAccess.GetData(q0);

            //        // DataRow row_available_qty = ds_qty_table.Tables[0].Rows[0];
            //         int new_qty = Convert.ToInt32(/*row_available_qty.ItemArray.GetValue(6).ToString()*/lbl_Aq.Text) + Convert.ToInt32(txt_qty.Text);

                   
            //            string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            //            string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            //            string action = "+";

            //            string q1 = "INSERT INTO battery_qty VALUES(" + cat_id + "," + Convert.ToInt32(lbl_Aq.Text) + ",'" + action + "'," + Convert.ToInt32(txt_qty.Text) + ",'" + date + "','" + time + "','"+ lbl_din_normal.Text +"')";
            //            bool status = Data.DataAccess.ExecuteData(q1);
            //            if (status == true)
            //            {
            //                if (lbl_din_normal.Text == "Normal Battery")
            //                {
            //                    string q = "UPDATE add_battery SET b_qty = " + new_qty + "  WHERE b_stok_id = " + cat_id + " ";
            //                    Data.DataAccess.ExecuteData(q);
            //                }
            //                else
            //                {
            //                    string q = "UPDATE din SET qty = " + new_qty + "  WHERE din_id = " + cat_id + " ";
            //                    Data.DataAccess.ExecuteData(q);
            //                }

            //                MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
            //                lbl_Aq.ForeColor = Color.Blue;
            //                lbl_Aq.Text = new_qty.ToString();
            //                txt_qty.Text = "";
                            
            //            }
            //            else
            //            {
            //                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }

            //    }



            }

        private void radButton2_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you sure you want to add stock?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    int catagory_id = battery_category_data.get_battery_catagory_id();

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
            //        int cat_id = battery_category_data.get_battery_catagory_id();

            //    //    string q0 = "SELECT * FROM add_battery WHERE b_stok_id =" + cat_id + "";
            //     //   DataSet ds_qty_table = Data.DataAccess.GetData(q0);

            //      //  DataRow row_available_qty = ds_qty_table.Tables[0].Rows[0];

            //        int new_qty = Convert.ToInt32(/*row_available_qty.ItemArray.GetValue(6).ToString()*/lbl_Aq.Text) - Convert.ToInt32(txt_qty.Text);
            //        if (Convert.ToInt32(/*row_available_qty.ItemArray.GetValue(6).ToString()*/lbl_Aq.Text) < Convert.ToInt32(txt_qty.Text))
            //        {

            //            MessageBox.Show("Available stock is insufficient!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);//show message box with ok button
            //            txt_qty.Text = "";
            //        }
            //        else
            //        {
            //            string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            //            string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            //            string action = "-";

            //            string q1 = "INSERT INTO battery_qty VALUES(" + cat_id + "," + Convert.ToInt32(lbl_Aq.Text) + ",'" + action + "'," + Convert.ToInt32(txt_qty.Text) + ",'" + date + "','" + time + "','" + lbl_din_normal.Text + "')";
            //            bool status = Data.DataAccess.ExecuteData(q1);
            //            if (status == true)
            //            {
            //                if (lbl_din_normal.Text == "Normal Battery")
            //                {
            //                    string q = "UPDATE add_battery SET b_qty = " + new_qty + "  WHERE b_stok_id = " + cat_id + " ";
            //                    Data.DataAccess.ExecuteData(q);
            //                }
            //                else
            //                {
            //                    string q = "UPDATE din SET qty = " + new_qty + "  WHERE din_id = " + cat_id + " ";
            //                    Data.DataAccess.ExecuteData(q);
            //                }

            //                MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
            //                if (new_qty == 0)
            //                {
            //                    lbl_Aq.ForeColor = Color.Red;
            //                }

            //                lbl_Aq.Text = new_qty.ToString();
            //                txt_qty.Text = "";
                           
            //            }
            //            else
            //            {
            //                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //    }

            //}



        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            //bool check;
            //float a;
            //check = float.TryParse(txt_discount.Text, out a);

            //if (txt_discount.Text == "")
            //{
            //    MessageBox.Show("Enter discount!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else if (!check)
            //{
            //    MessageBox.Show("Enter correct value!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txt_discount.Text = "";
            //}
            //else
            //{
                
            //    float price = (float)(Convert.ToDouble(battery_category_data.price));


            //    float discounted_price = (price - (price * ((float)(Convert.ToDouble(txt_discount.Text)) / 100)));
            //    MessageBox.Show("Customer Price : " + "Rs " + discounted_price + " /=", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txt_discount.Text = "";

            //}
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            //bool check;
            //float a;
            //check = float.TryParse(txt_price.Text, out a);

            //if (txt_price.Text == "")
            //{
            //    MessageBox.Show("Enter new price!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}

            //else if (!check)
            //{
            //    MessageBox.Show("Enter correct price!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txt_price.Text = "";
            //}

            //else
            //{
            //    if (MessageBox.Show("Are you sure you want to add price?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        int cat_id = battery_category_data.get_battery_catagory_id();
            //        float unit_price = (float)(Convert.ToDouble(txt_price.Text));

            //        string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            //        string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

            //        string q1 = "INSERT INTO battery_price VALUES(" + cat_id + "," + (float)(Convert.ToDouble(txt_price.Text)) + ",'" + date + "','" + time + "','"+ lbl_din_normal.Text +"')";
            //        bool status = Data.DataAccess.ExecuteData(q1);
            //        if (status == true)
            //        {
            //            if (lbl_din_normal.Text == "Normal Battery")
            //            {
            //                string q = "UPDATE add_battery SET b_prize = " + unit_price + "  WHERE b_stok_id = " + cat_id + " ";
            //                Data.DataAccess.ExecuteData(q);
            //            }
            //            else
            //            {
            //                string q = "UPDATE din SET price = " + unit_price + "  WHERE din_id = " + cat_id + " ";
            //                Data.DataAccess.ExecuteData(q);
            //            }
            //            lbl_current_prize.Text = "Rs" + txt_price.Text;
            //            txt_price.Text = "";
            //            price_grid_fill();
            //            MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }

            //    }
            //}
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you sure you want to clear?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //{
            //    int cat_id = battery_category_data.get_battery_catagory_id();
            //    string q = "DELETE FROM battery_price WHERE category_id =" + cat_id + " AND battery_type = '"+ lbl_din_normal.Text +"'";
            //    bool status = Data.DataAccess.ExecuteData(q);
            //    if (status == true)
            //    {
            //        MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        radGridView3.DataSource = null;
            //        radGridView3.Visible = false;
            //        btn_clear.Visible = false;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
        }
    }

