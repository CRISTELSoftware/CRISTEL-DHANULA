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
    public partial class tube_qty : Form
    {
        float item_price;
        DataSet ds_price;

        /// <summary>
        /// Initializes a new instance of the <see cref="tube_qty"/> class.
        /// </summary>
        public tube_qty()
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

            if (Application.OpenForms["Invoice"] != null)
            {
                addToInvoiceTube.Visible = true;
                btn_Add.Visible = false;
                btn_reduce.Visible = false;
                txt_qty.Focus();

            }
            else if (Application.OpenForms["Tube"] != null)
            {
                addToInvoiceTube.Visible = false;
                btn_Add.Visible = true;
                btn_reduce.Visible = true;
                txt_qty.Focus();

            }
            else if (Application.OpenForms["Billing"] != null)
            {
                addToInvoiceTube.Text = "Add To Bill";
                addToInvoiceTube.Visible = true;
                btn_Add.Visible = false;
                btn_reduce.Visible = false;
                txt_qty.Focus();

            }
            
        }

        public void check_din_normal()
        {
            
            //if (tube_category_data.din_check_box_val == "DIN")
            //{
            //   // lbl_din_normal.Text = "DIN Battery";
            //}
            //else
            //{
            //    lbl_din_normal.Text = "Normal Tube";
            //}
        }

        /// <summary>
        /// Price_grid_fills this instance.
        /// </summary>
        public void price_grid_fill()
        {
            int catagory_id = tube_category_data.get_battery_catagory_id();
            string q = "SELECT t_price AS Price,t_last_update AS Last_Update FROM tube_price WHERE t_category_id = " + catagory_id + " AND t_type = '" + lbl_din_normal.Text + "' ";            
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
            lbl_itemno.Text = tube_category_data.brand;
            lbl_size.Text = tube_category_data.size; ;
           // lbl_voltage.Text = tube_category_data.voltage;            
            lbl_amps.Text = tube_category_data.amps;
            lbl_type.Text = tube_category_data.type;
            string types = tube_category_data.type;
            if (types == "Cycle")
            {
                lbl_type.Text = "Cycle tube";
            }
            else
            {
                lbl_type.Text = "Vehicle tube";
            }

            int battery_qty = Convert.ToInt32(tube_category_data.qty);
            
                if (battery_qty == 0)
                {
                    lbl_Aq.ForeColor = Color.Red;
                }
             
                lbl_Aq.Text = battery_qty.ToString();

         

            lbl_current_prize.Text =  "Rs : "+tube_category_data.price.ToString();

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
            tube_category_data.invoiceQty = "";

            if (Application.OpenForms["Invoice"] != null)
            {
                Form f = (Form)Application.OpenForms["Invoice"];
                f.Enabled = true;
                this.Close();
            }
            else if (Application.OpenForms["Tube"] != null)
            {
                Form f = (Form)Application.OpenForms["Tube"];
                f.Enabled = true;
                this.Close();
            }
            else if (Application.OpenForms["Billing"] != null)
            {
                Form f = (Form)Application.OpenForms["Billing"];
                f.Enabled = true;
                this.Close();
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            string[] val = lbl_current_prize.Text.Split(':');
           
            tube_category_data.invoiceQty = txt_qty.Text;
            if (Convert.ToInt32(val[1]) != 0)
                tube_category_data.unitPrice = val[1].ToString();
            else
                tube_category_data.unitPrice = item_price.ToString();
            tube_category_data.statusPass2Forms = true;
            int catagory_id = tube_category_data.get_battery_catagory_id();
            int new_qty = 0;


                bool check;
                int a;
                check = int.TryParse(txt_qty.Text, out a);

                if (addToInvoiceTube.Text == "Add To Invoice"){
                    //new_qty = Convert.ToInt32(lbl_Aq.Text) - Convert.ToInt32(txt_qty.Text);
                //if (MessageBox.Show("Are you sure you want to add invoice?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{        

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
                    int available_qty = Convert.ToInt32(lbl_Aq.Text.ToString());
                    if (available_qty < Convert.ToInt32(txt_qty.Text))
                    {
                        MessageBox.Show("Available stock is insufficient!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);//show message box with ok button
                        txt_qty.Text = "";
                    }
                    else
                    {
                         if (Application.OpenForms["Invoice"] != null)
                         {
                             Form f = (Form)Application.OpenForms["Invoice"];
                             f.Enabled = true;
                             this.Close();
                         }
                         else if (Application.OpenForms["Tube"] != null)
                         {
                             Form f = (Form)Application.OpenForms["Tube"];
                             f.Enabled = true;
                             this.Close();
                         }

                    }
                }

                //}
                }

                else//(addToInvoiceTube.Text == "Add To Bill")                   
                {
                    new_qty = Convert.ToInt32(lbl_Aq.Text) + Convert.ToInt32(txt_qty.Text);
                    if (MessageBox.Show("Are you sure you want to add bill?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

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

                float price = (float)(Convert.ToDouble(tube_category_data.price));


                float discounted_price = (price - (price * ((float)(Convert.ToDouble(txt_discount.Text)) / 100)));
                MessageBox.Show("Customer Price : " + "Rs " + discounted_price + " /=", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_discount.Text = "";

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
                    int cat_id = tube_category_data.get_battery_catagory_id();
                    float unit_price = (float)(Convert.ToDouble(txt_price.Text));

                    string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
                    string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

                    string q1 = "INSERT INTO tube_price VALUES(" + cat_id + "," + (float)(Convert.ToDouble(txt_price.Text)) + ",'" + date + "','" + time + "','" + lbl_din_normal.Text + "')";
                    bool status = middle_access.db_access.InsertData(q1);
                    if (status == true)
                    {
                       // if (lbl_din_normal.Text == "Normal Tube")
                       // {
                            string q = "UPDATE tube_add SET t_prize = " + unit_price + "  WHERE t_stok_id = " + cat_id + " ";
                            middle_access.db_access.UpdateData(q);
                       // }
                       // else
                      //  {
                            //string q = "UPDATE din SET price = " + unit_price + "  WHERE din_id = " + cat_id + " ";
                            //middle_access.db_access.UpdateData(q);
                      //  }
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
                int cat_id = tube_category_data.get_battery_catagory_id();
                string q = "DELETE FROM tube_price WHERE t_category_id =" + cat_id + " AND t_type = '" + lbl_din_normal.Text + "'";
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

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add stock?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int catagory_id = tube_category_data.get_battery_catagory_id();

                bool check;
                int a;
                check = int.TryParse(txt_qty.Text, out a);

                if (txt_qty.Text == "")
                {
                    MessageBox.Show("Enter qty!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    int cat_id = tube_category_data.get_battery_catagory_id();                    
                    int new_qty = Convert.ToInt32(lbl_Aq.Text) + Convert.ToInt32(txt_qty.Text);


                    string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
                    string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
                    string action = "+";

                    string q1 = "INSERT INTO tube_qty VALUES(" + cat_id + "," + Convert.ToInt32(lbl_Aq.Text) + ",'" + action + "'," + Convert.ToInt32(txt_qty.Text) + ",'" + date + "','" + time + "','" + lbl_din_normal.Text + "')";
                    bool status = middle_access.db_access.InsertData(q1);
                    if (status == true)
                    {
                        //if (lbl_din_normal.Text == "Normal Tube")
                        //{
                            string q = "UPDATE tube_add SET t_qty = " + new_qty + "  WHERE t_stok_id = " + cat_id + " ";
                            middle_access.db_access.UpdateData(q);
                       // }
                        //else
                       // {
                            //string q = "UPDATE din SET qty = " + new_qty + "  WHERE din_id = " + cat_id + " ";
                            //middle_access.db_access.UpdateData(q);
                      //  }

                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        lbl_Aq.ForeColor = Color.Blue;
                        lbl_Aq.Text = new_qty.ToString();
                        txt_qty.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }


        }

        private void btn_reduce_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add stock?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int catagory_id = tube_category_data.get_battery_catagory_id();

                bool check;
                int a;
                check = int.TryParse(txt_qty.Text, out a);

                if (txt_qty.Text == "")
                {
                    MessageBox.Show("Enter qty!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    int cat_id = tube_category_data.get_battery_catagory_id();

                    int new_qty = Convert.ToInt32(lbl_Aq.Text) - Convert.ToInt32(txt_qty.Text);
                    if (Convert.ToInt32(lbl_Aq.Text) < Convert.ToInt32(txt_qty.Text))
                    {

                        MessageBox.Show("Available stock is insufficient!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);//show message box with ok button
                        txt_qty.Text = "";
                    }
                    else
                    {
                        string date = DateTime.Today.Date.ToString("yyyy-MM-dd");
                        string time = DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
                        string action = "-";

                        string q1 = "INSERT INTO tube_qty VALUES(" + cat_id + "," + Convert.ToInt32(lbl_Aq.Text) + ",'" + action + "'," + Convert.ToInt32(txt_qty.Text) + ",'" + date + "','" + time + "','" + lbl_din_normal.Text + "')";
                        bool status = middle_access.db_access.InsertData(q1);
                        if (status == true)
                        {
                            //if (lbl_din_normal.Text == "Normal Tube")
                            //{
                                string q = "UPDATE tube_add SET t_qty = " + new_qty + "  WHERE t_stok_id = " + cat_id + " ";
                                middle_access.db_access.UpdateData(q);
                           // }
                            //else
                           // {
                                //string q = "UPDATE din SET qty = " + new_qty + "  WHERE din_id = " + cat_id + " ";
                                //middle_access.db_access.UpdateData(q);
                          //  }

                            MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                            if (new_qty == 0)
                            {
                                lbl_Aq.ForeColor = Color.Red;
                            }

                            lbl_Aq.Text = new_qty.ToString();
                            txt_qty.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }


        }

      
       

        }
    }

