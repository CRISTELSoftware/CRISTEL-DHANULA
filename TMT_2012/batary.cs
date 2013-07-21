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
    public partial class batary : Form
    {

        string cell_value;
        int row_index;
        string type;
        int battery_category_id;
        int val;
        int row_count_update;
        DataSet ds_add_battery_cell_click = null;
        DataSet ds_size;
        string refresh_battery = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="batary"/> class.
        /// </summary>
        public batary()
        {
            InitializeComponent();
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

        

        private void label1_Click(object sender, EventArgs e)
        {
            panel_batary_stock.Visible = true;
            panel_add_new_category.Visible = false;
            panel_settings.Visible = false;
            fill_battery_search_table();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel_batary_stock.Visible = false;
            panel_add_new_category.Visible = true;
            panel_settings.Visible = false;
            radGridView4.Visible = false;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel_batary_stock.Visible = false;
            panel_add_new_category.Visible = false;
            panel_settings.Visible = true;
            radio_size.Checked = true;
            lbl_radio_selected_type.Text = "Size";
        }

         

       
        private void batary_Load(object sender, EventArgs e)
        {
            lbl_battery_section.Text = " Battery Section (Search)";
            panel_batary_stock.Visible = true;
            panel_add_new_category.Visible = false;
            panel_settings.Visible = false;


            fill_com_brand();
            fill_size();
            fill_com_voltage();
            fill_com_amps();
            fill_com_amp();
            fill_add_battery_table();
            fill_battery_search_table();


            radGridView5.Visible = false;
            com_search_brand.Enabled = false;
            txt_search_size.Enabled = false;
            groupBox2.Enabled = false;
            com_amp.Enabled = false;
            set_total_qty();

        }

        public void set_total_qty()
        {
            string q = null;
            if (chk_din_battery.Checked != true)
            {
                q = "SELECT SUM(b_qty) FROM add_battery";
            }
            else if (chk_din_battery.Checked == true)
            {
                q = "SELECT SUM(qty) FROM din";
            }


            
            DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
            DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
            string total_qty = row_total_qty.ItemArray.GetValue(0).ToString();
            if(total_qty != null)
            {
                lbl_tot_qty.Text = total_qty.ToString();
                lbl_search_qty.Text = "0";
            }
            else
           {
                lbl_tot_qty.Text = "0";
                lbl_search_qty.Text = "0";
           }
        }


        public void set_total_qty_after_changed()
        {
            string q = null;
            if (chk_din_battery.Checked != true)
            {
                q = "SELECT SUM(b_qty) FROM add_battery";
            }
            else if (chk_din_battery.Checked == true)
            {
                q = "SELECT SUM(qty) FROM din";
            }



            DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
            DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
            string total_qty = row_total_qty.ItemArray.GetValue(0).ToString();
            if (total_qty != null)
            {
                lbl_tot_qty.Text = total_qty.ToString();
                
            }
            else
            {
                lbl_tot_qty.Text = "0";
                
            }
        }

        public void set_searched_qty(string where_value)
        {
            string q = null;
            string value = where_value;
            if (value == null)
            {
                lbl_search_qty.Text = "0";
            }
            else
            {
                if (chk_din_battery.Checked != true)
                {
                    q = "SELECT SUM(b_qty) FROM add_battery WHERE " + value + "";
                }
                else if (chk_din_battery.Checked == true)
                {
                    q = "SELECT SUM(qty) FROM din D,add_battery B  WHERE B." + value + " AND B.b_stok_id = D.din_id ";
                }




                DataSet ds_searched_qty = middle_access.db_access.SelectData(q);
                if (ds_searched_qty != null)
                {
                    DataRow row_searched_qty = ds_searched_qty.Tables[0].Rows[0];
                    string searched_qty = row_searched_qty.ItemArray.GetValue(0).ToString();
                    if (searched_qty == "")
                    {
                        lbl_search_qty.Text = "0";
                    }
                    else
                    {
                        lbl_search_qty.Text = searched_qty.ToString();
                    }
                }
                else
                {
                    lbl_search_qty.Text = "0";
                }
            }
        }


        public void fill_battery_search_table()
        {

            string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type, b_qty AS Qty, b_prize AS Price FROM add_battery";
            DataSet ds_battery = middle_access.db_access.SelectData(q);
            if (ds_battery != null)
            {
                radGridView1.DataSource = ds_battery.Tables[0].DefaultView;
            }
            else
                radGridView1.DataSource = null;
        }

        /// <summary>
        /// Fill_din_battery_search_tables this instance.
        /// </summary>
        public void fill_din_battery_search_table()
        {

            string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type, D.qty AS Qty, D.price AS Price FROM add_battery B, din D WHERE D.din_id = B.b_stok_id";
            DataSet ds_battery = middle_access.db_access.SelectData(q);
            if (ds_battery != null)
            {
                radGridView1.DataSource = ds_battery.Tables[0].DefaultView;
            }
            else
                radGridView1.DataSource = null;
        }



        public void fill_com_brand()
        {
            string q1 = "SELECT * FROM battery_brand ";
            DataSet ds_brand = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand != null) // if data set is not null
            {
                com_brand.DataSource = ds_brand.Tables[0]; //fill table 
                com_brand.DisplayMember = "battery_brand";
                com_brand.ValueMember = "battery_brand";
                com_brand.Text = "";

                com_search_brand.DataSource = ds_brand.Tables[0]; //fill table 
                com_search_brand.DisplayMember = "battery_brand";
                com_search_brand.ValueMember = "battery_brand";
                com_search_brand.Text = "";

            }
            else
                com_brand.DataSource = null; //fill table 
        }

        public void fill_size()
        {

            string q1 = "SELECT battery_size FROM battery_size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                radGridView4.DataSource = ds_size.Tables[0]; //fill table 
                //radGridView5.DataSource = ds_size.Tables[0];


            }
            else
                radGridView4.DataSource = null; //fill table 
            //radGridView5.DataSource = null;
        }

        public void fill_com_voltage()
        {
            string q1 = "SELECT * FROM battery_voltage";
            DataSet ds_voltage = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_voltage != null) // if data set is not null
            {
                com_voltage.DataSource = ds_voltage.Tables[0]; //fill table 
                com_voltage.DisplayMember = "battery_voltage";
                com_voltage.ValueMember = "battery_voltage";
                com_voltage.Text = "";

            }
            else
                com_voltage.DataSource = null; //fill table 
        }

        public void fill_com_amps()
        {
            string q1 = "SELECT * FROM battery_amps";
            DataSet ds_amps = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_amps != null) // if data set is not null
            {
                com_amps.DataSource = ds_amps.Tables[0]; //fill table 
                com_amps.DisplayMember = "battery_amps";
                com_amps.ValueMember = "battery_amps";
                com_amps.Text = "";


            }
            else
                com_voltage.DataSource = null; //fill table 
        }

        public void fill_com_amp()
        {
            string q1 = "SELECT * FROM battery_amps";
            DataSet ds_amps = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_amps != null) // if data set is not null
            {
                
                com_amp.DataSource = ds_amps.Tables[0]; //fill table 
                com_amp.DisplayMember = "battery_amps";
                com_amp.ValueMember = "battery_amps";
                com_amp.Text = "";

            }
            else
                com_voltage.DataSource = null; //fill table 
        }




        private void radio_size_Click(object sender, EventArgs e)
        {
            lbl_radio_selected_type.Text = "Size";
            radio_size.Checked = true;
            txt_value.Focus();
            txt_value.Text = "";
        }

        private void radio_brand_Click(object sender, EventArgs e)
        {
            lbl_radio_selected_type.Text = "Brand";
            radio_brand.Checked = true;
            fill_battery_brand();
            txt_value.Focus();
            txt_value.Text = "";
        }

        private void radio_volts_Click(object sender, EventArgs e)
        {
            lbl_radio_selected_type.Text = "Voltage";
            radio_volts.Checked = true;
            fill_battery_voltage();
            txt_value.Focus();
            txt_value.Text = "";
        }

        private void radio_amps_Click(object sender, EventArgs e)
        {
            lbl_radio_selected_type.Text = "Amps";
            radio_amps.Checked = true;
            fill_battery_amps();
            txt_value.Focus();
            txt_value.Text = "";
        }

        public void fill_battery_size()
        {
            string q1 = "SELECT S.battery_size AS Batter_Size FROM battery_size S";
            DataSet ds_size_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_size_table != null) // if data set is not null
            {

                radGridView3.DataSource = ds_size_table.Tables[0]; //fill table 
            }

            else
                radGridView3.DataSource = null;
        }

        public void fill_battery_brand()
        {
            string q1 = "SELECT B.battery_brand AS Battery_Brands FROM battery_brand B";
            DataSet ds_brand_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand_table != null) // if data set is not null
            {

                radGridView3.DataSource = ds_brand_table.Tables[0]; //fill table 
            }

            else
                radGridView3.DataSource = null;
        }

        public void fill_battery_voltage()
        {
            string q1 = "SELECT V.battery_voltage AS Battery_Voltage FROM battery_voltage V";
            DataSet ds_voltage_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_voltage_table != null) // if data set is not null
            {

                radGridView3.DataSource = ds_voltage_table.Tables[0]; //fill table 
            }

            else
                radGridView3.DataSource = null;
        }


        /// <summary>
        /// Fill_battery_ampses this instance.
        /// </summary>
        public void fill_battery_amps()
        {
            string q1 = "SELECT A.battery_amps AS Battery_Amps FROM battery_amps A";
            DataSet ds_amps_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_amps_table != null) // if data set is not null
            {

                radGridView3.DataSource = ds_amps_table.Tables[0]; //fill table 
            }

            else
                radGridView3.DataSource = null;
        }


        private void radio_size_CheckedChanged(object sender, EventArgs e)
        {
            fill_battery_size();
            txt_value.Focus();
        }

        private void radGridView3_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
                txt_value.Text = radGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();//get cell value to the cell_brand_name variable
                cell_value = txt_value.Text;
                row_index = e.RowIndex;
            }
            else
            {

            }
        }

        private void btn_size_Click(object sender, EventArgs e)
        {
            if (radio_size.Checked == true)
            {
                insert_values("battery_size", "battery_size");
                fill_size();
            }
            else if (radio_brand.Checked == true)
            {
                insert_values("battery_brand", "battery_brand");
                fill_com_brand();
            }
            else if (radio_volts.Checked == true)
            {
                insert_values("battery_voltage", "battery_voltage");
                fill_com_voltage();
            }
            else if (radio_amps.Checked == true)
            {
                insert_values("battery_amps", "battery_amps");
                fill_com_amps();
            }

        }

        public void insert_values(string table, string column)
        {

            string value = (txt_value.Text);
            if (txt_value.Text != "") // if size text box is not empty
            {
                string q1 = "SELECT * FROM " + table + "  WHERE " + column + " = '" + value.Trim() + "' ";
                DataSet ds_value_table = middle_access.db_access.SelectData(q1);

                if (ds_value_table == null)
                {
                    string q2 = "INSERT INTO " + table + "(" + column + ")  VALUES('" + value + "')";
                    bool status = middle_access.db_access.InsertData(q2);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        if (radio_size.Checked == true)
                        {
                            fill_battery_size();// fill size name table.
                        }
                        else if (radio_brand.Checked == true)
                        {
                            fill_battery_brand();
                        }
                        else if (radio_volts.Checked == true)
                        {
                            fill_battery_voltage();
                        }
                        else if (radio_amps.Checked == true)
                        {
                            fill_battery_amps();
                        }
                        int row_count = radGridView3.RowCount;
                        this.radGridView3.Rows[row_count - 1].IsCurrent = true;
                        txt_value.Text = "";
                    }
                    else // if data is not insert
                    {

                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // if size text box is empty
                MessageBox.Show("Enter a Value!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void update_value(string table, string column)
        {
            string value = (txt_value.Text);
            if (txt_value.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string q1 = "SELECT * FROM " + table + "  WHERE " + column + " = '" + value.Trim() + "' ";
                    DataSet ds_value_table = middle_access.db_access.SelectData(q1);

                    if (ds_value_table == null)
                    {
                        string q2 = "UPDATE " + table + " SET " + column + " ='" + txt_value.Text + "' WHERE " + column + " = '" + cell_value + "'  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (radio_size.Checked == true)
                            {
                                fill_battery_size();// fill size name table.
                            }
                            else if (radio_brand.Checked == true)
                            {
                                fill_battery_brand();
                            }
                            else if (radio_volts.Checked == true)
                            {
                                fill_battery_voltage();
                            }
                            else if (radio_amps.Checked == true)
                            {
                                fill_battery_amps();
                            }

                            this.radGridView3.Rows[row_index].IsCurrent = true;
                            txt_value.Text = "";
                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_value.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Not changed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_value.Text = "";
                    }
                }
                else
                {
                    txt_value.Text = "";
                }
            }
            else
                MessageBox.Show("Select a value first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        public void delete_value(string table, string column)
        {
            string value = (txt_value.Text);
            if (txt_value.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM " + table + "  WHERE " + column + " = '" + value.Trim() + "' ";
                    DataSet ds_value_table = middle_access.db_access.SelectData(q1);

                    if (ds_value_table != null)
                    {
                        string q2 = "DELETE FROM " + table + " WHERE " + column + " = '" + txt_value.Text + "'  ";
                        bool status = middle_access.db_access.DeleteData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (radio_size.Checked == true)
                            {
                                fill_battery_size();// fill size name table.
                            }
                            else if (radio_brand.Checked == true)
                            {
                                fill_battery_brand();
                            }
                            else if (radio_volts.Checked == true)
                            {
                                fill_battery_voltage();
                            }
                            else if (radio_amps.Checked == true)
                            {
                                fill_battery_amps();
                            }

                            txt_value.Text = "";
                        }

                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_value.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Value was not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_value.Text = "";
                    }
                }
                else
                {
                    txt_value.Text = "";
                }

            }
            else
                MessageBox.Show("Select a value!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void btn_update_Click(object sender, EventArgs e)
        {
            if (radio_size.Checked == true)
            {
                update_value("battery_size", "battery_size");
                fill_size();
            }
            else if (radio_brand.Checked == true)
            {
                update_value("battery_brand", "battery_brand");
                fill_com_brand();
            }
            else if (radio_volts.Checked == true)
            {
                update_value("battery_voltage", "battery_voltage");
                fill_com_voltage();
            }
            else if (radio_amps.Checked == true)
            {
                update_value("battery_amps", "battery_amps");
                fill_com_amps();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (radio_size.Checked == true)
            {
                delete_value("battery_size", "battery_size");
                fill_size();
            }
            else if (radio_brand.Checked == true)
            {
                delete_value("battery_brand", "battery_brand");
                fill_com_brand();
            }
            else if (radio_volts.Checked == true)
            {
                delete_value("battery_voltage", "battery_voltage");
                fill_com_voltage();
            }
            else if (radio_amps.Checked == true)
            {
                delete_value("battery_amps", "battery_amps");
                fill_com_amps();
            }

        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            com_amps.Text = "";
            com_brand.Text = "";
            com_voltage.Text = "";
            txt_size.Text = "";
            radio_M.Checked = false;
            radio_MF.Checked = false;

        }

        private void txt_size_Enter(object sender, EventArgs e)
        {

            radGridView4.Visible = true;
        }

        private void panel_add_new_category_Click(object sender, EventArgs e)
        {
            radGridView4.Visible = false;
        }

        private void com_brand_Click(object sender, EventArgs e)
        {
            radGridView4.Visible = false;
        }

        private void com_voltage_Click(object sender, EventArgs e)
        {
            radGridView4.Visible = false;
        }

        private void com_amps_Click(object sender, EventArgs e)
        {
            radGridView4.Visible = false;
        }

        private void batary_Click(object sender, EventArgs e)
        {
            radGridView4.Visible = false;
            radGridView5.Visible = false;
        }

        private void txt_size_Click(object sender, EventArgs e)
        {
            // radGridView4.Visible = true;
        }

        private void radGridView4_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_size.Text = radGridView4.Rows[row_index].Cells[0].Value.ToString();
            txt_size.Focus();
            radGridView4.Visible = false;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            press_enter();
        }














        /// <summary>
        /// Press_enters this instance.
        /// </summary>
        public void press_enter()
        {
            if (((radio_M.Checked == true) || (radio_MF.Checked == true)) && com_brand.Text != "" && com_voltage.Text != "" && com_amps.Text != "" && txt_size.Text != "")
            {
                string q = "SELECT * FROM add_battery B WHERE B.b_brand = '" + com_brand.Text + "' AND B.b_size = '" + txt_size.Text + "' AND B.b_type = '" + type + "' AND B.b_voltage = '" + com_voltage.Text + "' AND B.b_amps = '" + com_amps.Text + "'";
                DataSet ds_add_battery = middle_access.db_access.SelectData(q);

                if (ds_add_battery == null)
                {
                    if (chk_DIN.Checked == true)
                    {
                        string q1 = "INSERT INTO add_battery (b_brand,b_size,b_voltage,b_amps,b_type)  VALUES('" + com_brand.Text + "','" + txt_size.Text + "', '" + com_voltage.Text + "','" + com_amps.Text + "', '" + type + "')";
                        bool status = middle_access.db_access.InsertData(q1);
                        if (status == true) // if data is insert
                        {
                            string q5 = "SELECT * FROM add_battery B WHERE B.b_brand = '" + com_brand.Text + "' AND B.b_size = '" + txt_size.Text + "' AND B.b_type = '" + type + "' AND B.b_voltage = '" + com_voltage.Text + "' AND B.b_amps = '" + com_amps.Text + "'";
                            ds_add_battery = middle_access.db_access.SelectData(q5);
                            DataRow row_add_battery = ds_add_battery.Tables[0].Rows[0];
                            int battery_category_id = Convert.ToInt32(row_add_battery.ItemArray.GetValue(0).ToString());

                            string q2 = "INSERT INTO din (din_id,din)  VALUES(" + battery_category_id + ",'DIN')";
                            bool status1 = middle_access.db_access.InsertData(q2);
                            if (status1 == true)
                            {
                                MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                                fill_add_battery_table_din();// fill brand name table.
                                radGridView2.Focus();
                                int row_count = radGridView2.RowCount;
                                this.radGridView2.Rows[row_count - 1].IsCurrent = true;
                            }
                            else
                            {
                                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Pattern is already exiat!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            com_amps.Text = "";
                            com_brand.Text = "";
                            com_voltage.Text = "";
                            txt_size.Text = "";
                            radio_M.Checked = false;
                            radio_MF.Checked = false;

                        }


                    }

                    else // if data is not insert
                    {
                        string q1 = "INSERT INTO add_battery (b_brand,b_size,b_voltage,b_amps,b_type)  VALUES('" + com_brand.Text + "','" + txt_size.Text + "', '" + com_voltage.Text + "','" + com_amps.Text + "', '" + type + "')";
                        bool status = middle_access.db_access.InsertData(q1);
                        if (status == true) // if data is insert
                        {
                            MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                            fill_add_battery_table();// fill brand name table.
                            radGridView2.Focus();
                            int row_count = radGridView2.RowCount;
                            this.radGridView2.Rows[row_count - 1].IsCurrent = true;
                        }
                        else
                        {
                            MessageBox.Show("Pattern is already exiat!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            com_amps.Text = "";
                            com_brand.Text = "";
                            com_voltage.Text = "";
                            txt_size.Text = "";
                            radio_M.Checked = false;
                            radio_MF.Checked = false;


                        }

                    }

                }
                else
                {
                    DataRow row_add_battery = ds_add_battery.Tables[0].Rows[0];
                    int battery_category_id = Convert.ToInt32(row_add_battery.ItemArray.GetValue(0).ToString());
                    if (chk_DIN.Checked == true)
                    {
                        string q3 = "SELECT * FROM din WHERE din_id = '" + battery_category_id + "'";
                        bool status = middle_access.db_access.InsertData(q3);
                        if (status != true) // if data is insert
                        {
                            string q2 = "INSERT INTO din (din_id,din)  VALUES(" + battery_category_id + ",'DIN')";
                            bool status1 = middle_access.db_access.InsertData(q2);
                            if (status1 == true)
                            {
                                MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                                fill_add_battery_table_din();// fill brand name table.
                                radGridView2.Focus();
                                int row_count = radGridView2.RowCount;
                                this.radGridView2.Rows[row_count - 1].IsCurrent = true;
                            }
                            else
                            {
                                MessageBox.Show("Pattern is already exiat!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                com_amps.Text = "";
                                com_brand.Text = "";
                                com_voltage.Text = "";
                                txt_size.Text = "";
                                radio_M.Checked = false;
                                radio_MF.Checked = false;

                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Pattern is already exiat!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        com_amps.Text = "";
                        com_brand.Text = "";
                        com_voltage.Text = "";
                        txt_size.Text = "";
                        radio_M.Checked = false;
                        radio_MF.Checked = false;

                    }
                }


            }
            else
                MessageBox.Show("Enter detalils correctly!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }


        /// <summary>
        /// Fill_add_battery_table_dins this instance.
        /// </summary>
        public void fill_add_battery_table_din()
        {
            // int battery_category_id = battery_category_data.get_battery_catagory_id();
            string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type FROM add_battery B,din D WHERE B.b_stok_id = D.din_id";
            DataSet ds_add_battery = middle_access.db_access.SelectData(q);
            if (ds_add_battery != null)
            {
                radGridView2.DataSource = ds_add_battery.Tables[0].DefaultView;

            }
            else
            {
                radGridView2.DataSource = null;

            }
        }

        /// <summary>
        /// Fill_add_battery_tables this instance.
        /// </summary>
        public void fill_add_battery_table()
        {

            string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type FROM add_battery";
            DataSet ds_add_battery = middle_access.db_access.SelectData(q);
            if (ds_add_battery != null)
            {
                radGridView2.DataSource = ds_add_battery.Tables[0].DefaultView;

            }
            else
            {
                radGridView2.DataSource = null;

            }
        }


        private void radio_M_Click(object sender, EventArgs e)
        {
            type = "M";
        }

        private void radio_MF_Click(object sender, EventArgs e)
        {
            type = "MF";
        }

        private void txt_search_size_Enter(object sender, EventArgs e)
        {
            string q1 = "SELECT battery_size FROM battery_size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {
                radGridView5.DataSource = ds_size.Tables[0];
                radGridView5.Visible = true;
            }
            else
            {
                radGridView5.DataSource = null; //fill table 
            }
        }

        private void panel_batary_stock_Click(object sender, EventArgs e)
        {
            radGridView5.Visible = false;
        }

        private void chk_brand_Click(object sender, EventArgs e)
        {
            if (chk_brand.Checked == false)
            {
                com_search_brand.Enabled = true;
            }
            else
            {
                com_search_brand.Enabled = false;
                com_search_brand.Text = "";
            }
        }

        private void chk_size_Click(object sender, EventArgs e)
        {
            if (chk_size.Checked == false)
            {
                txt_search_size.Enabled = true;
                //  fill_size();

            }
            else
            {
                txt_search_size.Enabled = false;
                txt_search_size.Text = "";
                radGridView5.Visible = false;

            }
        }

        private void chk_type_Click(object sender, EventArgs e)
        {
            if (chk_type.Checked == false)
            {
                groupBox2.Enabled = true;
            }
            else
            {
                groupBox2.Enabled = false;

            }
        }

        private void radGridView5_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_search_size.Text = radGridView5.Rows[row_index].Cells[0].Value.ToString();
            txt_size.Focus();
            radGridView5.Visible = false;
        }

        private void radGridView4_SelectionChanged(object sender, EventArgs e)
        {
            row_index = radGridView4.CurrentRow.Index;
        }

        private void radGridView5_SelectionChanged(object sender, EventArgs e)
        {
            row_index = radGridView5.CurrentRow.Index;
        }

        private void txt_search_size_Click(object sender, EventArgs e)
        {
            txt_size.Text = radGridView5.Rows[row_index].Cells[0].Value.ToString();
            txt_size.Focus();
            radGridView5.Visible = false;
        }

        private void radGridView2_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {

                com_brand.Text = radGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_size.Text = radGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                radGridView4.DataSource = null;
                radGridView4.Visible = false;
                com_voltage.Text = radGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                com_amps.Text = radGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                type = radGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();

                set_add_battery_cell_click();
                battery_category_data.brand = com_brand.Text;
                battery_category_data.size = txt_size.Text;
                battery_category_data.type = type;
                battery_category_data.voltage = com_voltage.Text;
                battery_category_data.amps = com_amps.Text;

                battery_category_id = battery_category_data.get_battery_catagory_id();

                if (type == "M")
                {
                    radio_M.Checked = true;
                }

                else if (type == "MF")
                {
                    radio_MF.Checked = true;
                }

                fill_size();

                //string q = "SELECT * FROM din WHERE din_id = '" + battery_category_id + "'";
                //DataSet ds_din = Data.DataAccess.GetData(q);

                //if (ds_din != null)
                //{
                //    chk_DIN.Checked = true;
                //}

            }

        }

        private void radGridView2_SelectionChanged(object sender, EventArgs e)
        {
            val = radGridView2.CurrentRow.Index;
            row_count_update = radGridView2.CurrentRow.Index;
        }

        private void chk_DIN_Click(object sender, EventArgs e)
        {
            if (chk_DIN.Checked == true)
            {
                fill_add_battery_table_din();
            }
            else
            {
                fill_add_battery_table();
                radGridView2.Focus();
            }

        }

        private void radio_M_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_M.Checked == true)
            {
                type = "M";
            }
        }

        private void radio_MF_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_MF.Checked == true)
            {
                type = "MF";
            }
        }

        private void chk_DIN_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void set_add_battery_cell_click()
        {
            string q = "SELECT * FROM add_battery B WHERE B.b_brand = '" + com_brand.Text + "' AND B.b_size = '" + txt_size.Text + "' AND B.b_type = '" + type + "' AND B.b_voltage = '" + com_voltage.Text + "' AND B.b_amps = '" + com_amps.Text + "'";
            ds_add_battery_cell_click = middle_access.db_access.SelectData(q);
        }


        private void radButton3_Click(object sender, EventArgs e)
        {
            if (((radio_M.Checked == true) || (radio_MF.Checked == true)) && com_brand.Text != "" && com_voltage.Text != "" && com_amps.Text != "" && txt_size.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string q = "SELECT * FROM add_battery B WHERE B.b_brand = '" + com_brand.Text + "' AND B.b_size = '" + txt_size.Text + "' AND B.b_type = '" + type + "' AND B.b_voltage = '" + com_voltage.Text + "' AND B.b_amps = '" + com_amps.Text + "'";
                    DataSet ds_add_battery = middle_access.db_access.SelectData(q);
                    if (ds_add_battery == null)
                    {
                        DataRow row_add_battery = ds_add_battery_cell_click.Tables[0].Rows[0];

                        string brand = row_add_battery.ItemArray.GetValue(1).ToString();
                        string size = row_add_battery.ItemArray.GetValue(2).ToString();
                        string voltage = row_add_battery.ItemArray.GetValue(3).ToString();
                        string amps = row_add_battery.ItemArray.GetValue(4).ToString();
                        string type1 = row_add_battery.ItemArray.GetValue(5).ToString();

                        battery_category_data.brand = brand;
                        battery_category_data.size = size;
                        battery_category_data.voltage = voltage;
                        battery_category_data.amps = amps;
                        battery_category_data.type = type1;
                        int battery_category_id = battery_category_data.get_battery_catagory_id();

                        string q2 = "UPDATE add_battery SET b_brand = '" + com_brand.Text + "',b_size = '" + txt_size.Text + "',b_voltage = '" + com_voltage.Text + "',b_amps = '" + com_amps.Text + "',b_type = '" + type + "' WHERE b_stok_id = " + battery_category_id + "  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            int row_index = row_count_update;
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (chk_DIN.Checked == true)
                            {
                                fill_add_battery_table_din();
                            }
                            else
                            {

                                fill_add_battery_table();
                            }
                            radGridView2.Focus();
                            this.radGridView2.Rows[row_index].IsCurrent = true;

                            com_amps.Text = "";
                            com_brand.Text = "";
                            com_voltage.Text = "";
                            txt_size.Text = "";
                            radio_M.Checked = false;
                            radio_MF.Checked = false;

                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            com_amps.Text = "";
                            com_brand.Text = "";
                            com_voltage.Text = "";
                            txt_size.Text = "";
                            radio_M.Checked = false;
                            radio_MF.Checked = false;

                        }
                    }

                    else
                    {
                        MessageBox.Show("Already exist!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        com_amps.Text = "";
                        com_brand.Text = "";
                        com_voltage.Text = "";
                        txt_size.Text = "";
                        radio_M.Checked = false;
                        radio_MF.Checked = false;
                    }
                }
                else
                {
                    com_amps.Text = "";
                    com_brand.Text = "";
                    com_voltage.Text = "";
                    txt_size.Text = "";
                    radio_M.Checked = false;
                    radio_MF.Checked = false;
                }
            }
            else
                MessageBox.Show("Select a record first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radButton9_Click(object sender, EventArgs e)
        {
            if (((radio_M.Checked == true) || (radio_MF.Checked == true)) && com_brand.Text != "" && com_voltage.Text != "" && com_amps.Text != "" && txt_size.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    int catagory_id = battery_category_data.get_battery_catagory_id();

                    string q2 = "DELETE FROM din  WHERE din_id = " + catagory_id + " ";
                    bool status = middle_access.db_access.DeleteData(q2);
                    if (status == true)
                    {
                        string q3 = "DELETE FROM add_battery  WHERE b_stok_id = " + catagory_id + "";
                        middle_access.db_access.DeleteData(q3);

                        MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (chk_DIN.Checked == true)
                        {
                            fill_add_battery_table_din();
                        }
                        else
                        {
                            fill_add_battery_table();
                        }
                        com_amps.Text = "";
                        com_brand.Text = "";
                        com_voltage.Text = "";
                        txt_size.Text = "";
                        radio_M.Checked = false;
                        radio_MF.Checked = false;
                    }

                    else
                    {
                        string q3 = "DELETE FROM add_battery  WHERE b_stok_id = " + catagory_id + "";
                        middle_access.db_access.DeleteData(q3);

                        MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (chk_DIN.Checked == true)
                        {
                            fill_add_battery_table_din();
                        }
                        else
                        {
                            fill_add_battery_table();
                        }
                        com_amps.Text = "";
                        com_brand.Text = "";
                        com_voltage.Text = "";
                        txt_size.Text = "";
                        radio_M.Checked = false;
                        radio_MF.Checked = false;

                    }
                }
                else
                {

                }

            }
            else
                MessageBox.Show("Select a record!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void chk_din_battery_Click(object sender, EventArgs e)
        {
            //if (chk_din_battery.Checked != true)
            //{
            //    fill_battery_search_table();
            //    battery_category_data.din_check_box_val = "DIN";
            //}
            //else
            //{
            //    fill_din_battery_search_table();

            //    battery_category_data.din_check_box_val = "NORMAL";
            //}

            //set_total_qty();
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            if (chk_din_battery.Checked != true)
            {
                search_normal_battery();
            }
            else
            {
                search_din_battery();
            }
        }


        /// <summary>
        /// Search_din_batteries this instance.
        /// </summary>
        public void search_din_battery()
        {
            string qq = set_search_query();
            refresh_battery = qq;
            set_searched_qty(qq);
            if (qq == null)
            {

            }
            else
            {

                string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type, D.qty AS Qty, D.price AS Price FROM add_battery B, din D WHERE " + qq + " AND B.b_stok_id = D.din_id";
                DataSet ds_advance_search = middle_access.db_access.SelectData(q);
                if (ds_advance_search != null)
                {
                    radGridView1.DataSource = ds_advance_search.Tables[0].DefaultView;
                }
                else
                {
                    radGridView1.DataSource = null;
                }
            }
        }

        public void search_normal_battery()
        {
            string qq = set_search_query();
            string qqq = set_amp();
            if (qqq == null)
            {
                refresh_battery = qq;
                set_searched_qty(qq);
                if (qq == null)
                {

                }
                else
                {

                    string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type, b_qty AS Qty, b_prize AS Price FROM add_battery WHERE " + qq + "";
                    DataSet ds_advance_search = middle_access.db_access.SelectData(q);
                    if (ds_advance_search != null)
                    {
                        radGridView1.DataSource = ds_advance_search.Tables[0].DefaultView;
                    }
                    else
                    {
                        radGridView1.DataSource = null;
                    }
                }
            }
            else
            {
                string q1;
                if(qq != null)
                {
                     q1 = qq + "AND " + qqq; 
                }else
                {
                     q1 =  qqq; 
                }
                
                refresh_battery = q1;
                set_searched_qty(q1);
                if (q1 == null)
                {

                }
                else
                {

                    string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type, b_qty AS Qty, b_prize AS Price FROM add_battery WHERE " + q1 + "";
                    DataSet ds_advance_search = middle_access.db_access.SelectData(q);
                    if (ds_advance_search != null)
                    {
                        radGridView1.DataSource = ds_advance_search.Tables[0].DefaultView;
                    }
                    else
                    {
                        radGridView1.DataSource = null;
                    }
                }
            }
            
        }

        public string set_amp()
        {
            string amp = com_amp.Text;
            string qq = null;
            if (chk_amp.Checked == true)
            {
                qq = "b_amps = '" + amp + "'";
            }

            else
            {

            }

            return qq;
        }

        public string set_search_query()
        {

            string brand = com_search_brand.Text;
            string size = txt_search_size.Text;
            string battery_type = type;
            string qq = null;

            if (chk_brand.Checked == true)
            {
                qq = " b_brand ='" + brand + "' ";
                if (chk_size.Checked == true)
                {
                    qq = qq + "AND" + " b_size ='" + size + "' ";
                    if (chk_type.Checked == true)
                    {
                        qq = qq + "AND" + " b_type ='" + battery_type + "' ";
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (chk_type.Checked == true)
                    {
                        qq = qq + "AND" + " b_type ='" + battery_type + "' ";
                    }
                }
            }
            else
            {
                if (chk_size.Checked == true)
                {
                    qq = " b_size ='" + size + "' ";
                    if (chk_type.Checked == true)
                    {
                        qq = qq + "AND" + " b_type ='" + battery_type + "' ";
                    }
                }
                else
                {
                    if (chk_type.Checked == true)
                    {
                        qq = " b_type ='" + battery_type + "' ";
                    }
                }
            }


            return qq;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_M.Checked == true)
            {
                type = "M";
            }
            if (rad_MF.Checked == true)
            {
                type = "MF";
            }
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            if (chk_din_battery.Checked != true)
            {
                fill_battery_search_table();
                
            }
            else
            {
                fill_din_battery_search_table();
            }
            set_total_qty();
        }

        private void rad_MF_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_M.Checked == true)
            {
                type = "M";
            }
            if (rad_MF.Checked == true)
            {
                type = "MF";
            }
        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            int r_count = radGridView1.RowCount;
            if (val >= 0)
            {
                string brand = radGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string size = radGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string voltage = radGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string amp = radGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string type = radGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                

                battery_category_data.brand = brand;
                battery_category_data.size = size;
                battery_category_data.voltage = voltage;
                battery_category_data.amps = amp;
                battery_category_data.type = type;
                battery_category_data.qty = radGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                battery_category_data.price = radGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                


                this.Enabled = false;
                battery_qty q = new battery_qty();
                q.MdiParent = DHNAULA.ActiveForm;
                q.Visible = true;
            }




        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            //if (battery_category_data.din_check_box_val == "NORMAL")
            //{
            //    report_battery_stok b = new report_battery_stok();
            //    b.Show();
            //}
            //else
            //{
            //    report_din_battery_stok db = new report_din_battery_stok();
            //    db.Show();
            //}
        }

        private void chk_din_battery_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chk_din_battery.Checked != true)
            {
                fill_battery_search_table();
                battery_category_data.din_check_box_val = "NORMAL";
            }
            else
            {
                fill_din_battery_search_table();

                battery_category_data.din_check_box_val = "DIN";
            }

            set_total_qty();
        }

        private void txt_size_TextChanged(object sender, EventArgs e)
        {
            string text_text = txt_size.Text;
            filter_text_box_text_size_add(text_text);
        }

        public void filter_text_box_text_size_add(string text_text)
        {
            if(txt_size.Text == "")
            {
                radGridView4.Visible = false;
            }
            else
            {
            string q1 = "SELECT battery_size FROM battery_size WHERE battery_size  LIKE  '" + text_text + "%' ";
            ds_size = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_size != null) // if data set is not null
            {
               // radGridView4.Visible = false;
                radGridView4.Visible = true;
                radGridView4.DataSource = ds_size.Tables[0];


            }
            else
            {
                radGridView4.DataSource = null;
                radGridView4.Visible = false;
            }
            }

        }

        /// <summary>
        /// Filter_text_box_text_size_searches the specified text_text.
        /// </summary>
        /// <param name="text_text">The text_text.</param>
        public void filter_text_box_text_size_search(string text_text)
        {
            string q1 = "SELECT battery_size FROM battery_size WHERE battery_size  LIKE  '" + text_text + "%' ";
            ds_size = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_size != null) // if data set is not null
            {
                radGridView5.Visible = false;
                radGridView5.Visible = true;
                radGridView5.DataSource = ds_size.Tables[0];


            }
            else
            {
                radGridView5.DataSource = null;
                radGridView5.Visible = false;
            }

        }

        private void txt_search_size_TextChanged(object sender, EventArgs e)
        {
            string text_text = txt_search_size.Text;
            filter_text_box_text_size_search(text_text);
        }

        private void batary_EnabledChanged(object sender, EventArgs e)
        {
            if (chk_din_battery.Checked == true)
            {
                if (refresh_battery == null)
                {
                    fill_din_battery_search_table();

                }
                else
                {
                    string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type, D.qty AS Qty, D.price AS Price FROM add_battery B, din D WHERE D.din_id = B.b_stok_id AND "+ refresh_battery +"";
                    DataSet ds_battery = middle_access.db_access.SelectData(q);
                    if (ds_battery != null)
                    {
                        radGridView1.DataSource = ds_battery.Tables[0].DefaultView;
                    }
                    else
                        radGridView1.DataSource = null;
                }
                set_searched_qty(refresh_battery);
                set_total_qty_after_changed();
            }
            else
            {
                if (refresh_battery == null)
                {
                    fill_battery_search_table();
                }
                else
                {
                    string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type, b_qty AS Qty, b_prize AS Price FROM add_battery WHERE "+ refresh_battery +"";
                    DataSet ds_battery = middle_access.db_access.SelectData(q);
                    if (ds_battery != null)
                    {
                        radGridView1.DataSource = ds_battery.Tables[0].DefaultView;
                    }
                    else
                        radGridView1.DataSource = null;
            
                }

                set_searched_qty(refresh_battery);
                set_total_qty_after_changed();
            }
        }

        private void chk_amp_Click(object sender, EventArgs e)
        {
            if (chk_amp.Checked == false)
            {
                com_amp.Enabled = true;
            }
            else
            {
                com_amp.Enabled = false;
                com_amp.Text = "";
            }
        }

        private void radButton7_Click(object sender, EventArgs e)
        {
            //report_battery_history B = new report_battery_history();
            //B.Show();
        }

        private void btnBattarySearch_Click(object sender, EventArgs e)
        {
            lbl_battery_section.Text = " Battery Section (Search)";
            panel_batary_stock.Visible = true;
            panel_add_new_category.Visible = false;
            panel_settings.Visible = false;
            fill_battery_search_table();
        }

        private void btnAddNewBattaryCat_Click(object sender, EventArgs e)
        {
            lbl_battery_section.Text = " Battery Section (Add Battery Category)";
            panel_batary_stock.Visible = false;
            panel_add_new_category.Visible = true;
            panel_settings.Visible = false;
            radGridView4.Visible = false;
        }

        private void btnBattrySettings_Click(object sender, EventArgs e)
        {
            lbl_battery_section.Text = " Battery Section (Settings)";
            panel_batary_stock.Visible = false;
            panel_add_new_category.Visible = false;
            panel_settings.Visible = true;
            radio_size.Checked = true;
            lbl_radio_selected_type.Text = "Size";
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            radGridView5.Visible = false;
        }

        
    }
}
