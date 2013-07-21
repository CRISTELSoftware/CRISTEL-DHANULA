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
    public partial class AddMoterCycleTyre : Telerik.WinControls.UI.RadForm
    {
        Point moveStart;
        string radio_val_tyre_type = null;
        string radio_val_tube = null;
        string radio_val_side = null;
        string check_val_side = null;
        int val;
        int row_index;
        int row_count_update;

        DataSet ds_size;
        DataSet ds_set_add_cycle_tyre_cell_click;

        //string front;
        string rear;
        string tube;
        string tube_less;
        string trail;
        string non_trail;

        int category_id;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMoterCycleTyre"/> class.
        /// </summary>
        public AddMoterCycleTyre()
        {
            InitializeComponent();
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true; 
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void AddMoterCycleTyre_Load(object sender, EventArgs e)
        {
            fill_add_tyre_table();
            fill_com_brand();
            fill_size();
            fill_com_ply_rate();
            fill_com_make();
            fill_com_thread_pattern_load();
            dtp_new_stock.Value = DateTime.Today.Date;
            radGridView2.Visible = false;
        }


        /// <summary>
        /// Fill_add_tyre_tables this instance.
        /// </summary>
        public void fill_add_tyre_table()
        {

            string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side, t_make AS Make, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern, t_stok_date AS Date,t_tyre_pattern AS Tyer_Pattern , t_tube AS Tube_type FROM add_cycle_tyre ";
            DataSet ds_add_vehical_tyre = middle_access.db_access.SelectData(q);
            if (ds_add_vehical_tyre != null)
            {
                radGridView1.DataSource = ds_add_vehical_tyre.Tables[0].DefaultView;
            }
            else
                radGridView1.DataSource = null;
        }


        /// <summary>
        /// Fill_com_brands this instance.
        /// </summary>
        public void fill_com_brand()
        {
            string q1 = "SELECT * FROM brand B";
            DataSet ds_brand = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand != null) // if data set is not null
            {

                com_brand.DataSource = ds_brand.Tables[0]; //fill table 
                com_brand.DisplayMember = "brand_name";
                com_brand.ValueMember = "brand_name";
                com_brand.Text = "";


            }
            else
                com_brand.DataSource = null; //fill table 
        }

        /// <summary>
        /// Fill_com_thread_patterns this instance.
        /// </summary>
        public void fill_com_thread_pattern()
        {
            string brand_name = com_brand.Text;
            string q1 = "SELECT TP.pattern_name FROM thread_pattern TP,brand B WHERE TP.brand_id = B.brand_id AND B.brand_name = '" + brand_name + "' ";
            DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with patterns which stoed in thread pattern table 
            if (ds_thread_pattern != null) // if data set is not null
            {

                com_t_pattern.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                com_t_pattern.DisplayMember = "pattern_name";
                com_t_pattern.ValueMember = "pattern_name";
                com_t_pattern.Text = "";

            }
            else
                com_t_pattern.DataSource = null; //fill table 
        }

        public void fill_size()
        {

            string q1 = "SELECT S.tyer_size FROM size S";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                radGridView2.DataSource = ds_size.Tables[0]; //fill table 
                txt_size.Text = "";

            }
            else
                radGridView2.DataSource = null; //fill table 
        }

        /// <summary>
        /// Fill_com_ply_rates this instance.
        /// </summary>
        public void fill_com_ply_rate()
        {
            string q1 = "SELECT * FROM ply_rate ";
            DataSet ds_ply_rate = middle_access.db_access.SelectData(q1); // fill data set with rates which stoed in pl rate table 
            if (ds_ply_rate != null) // if data set is not null
            {

                com_ply_rate.DataSource = ds_ply_rate.Tables[0]; //fill table 
                com_ply_rate.DisplayMember = "rate";
                com_ply_rate.ValueMember = "rate";
                com_ply_rate.Text = "";

            }
            else
                com_ply_rate.DataSource = null; //fill table 

        }

        /// <summary>
        /// Fill_com_makes this instance.
        /// </summary>
        public void fill_com_make()
        {
            string q1 = "SELECT * FROM make ";
            DataSet ds_made = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_made != null) // if data set is not null
            {

                com_made.DataSource = ds_made.Tables[0]; //fill table 
                com_made.DisplayMember = "made_by";
                com_made.ValueMember = "made_by";
                com_made.Text = "";

            }
            else
                com_made.DataSource = null; //fill table 

        }

        public void fill_com_thread_pattern_load()
        {
            string q1 = "SELECT * FROM thread_pattern";
            DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_thread_pattern != null) // if data set is not null
            {

                com_t_pattern.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                com_t_pattern.DisplayMember = "pattern_name";
                com_t_pattern.ValueMember = "pattern_name";
                com_t_pattern.Text = "";

            }
            else
                com_t_pattern.DataSource = null; //fill table 
        }

        private void com_brand_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_com_thread_pattern();
        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            val = e.RowIndex;
            if (val >= 0)
            {
                dtp_new_stock.Value = Convert.ToDateTime(radGridView1.Rows[e.RowIndex].Cells[6].Value);
                com_brand.Text = radGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_size.Text = radGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_size.Text = radGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                radGridView2.DataSource = null;
                radGridView2.Visible = false;
                com_made.Text = radGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                com_ply_rate.Text = radGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                com_t_pattern.Text = radGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                string tp = com_t_pattern.Text;

                string tyre_pattern = radGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                string tube = radGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                string side = radGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();


                cycle_category_data.brand = com_brand.Text;
                cycle_category_data.size = txt_size.Text;
                cycle_category_data.ply_rate = com_ply_rate.Text;
                cycle_category_data.thread_pattern = com_t_pattern.Text;
                cycle_category_data.make = com_made.Text;
                cycle_category_data.tyre_pattern = tyre_pattern;
                cycle_category_data.tube = tube;
                cycle_category_data.side = side;


                category_id = cycle_category_data.get_catagory_id();

                if (tyre_pattern == "Trail")
                {
                    radio_trail.IsChecked = true;

                    if (tube == "Tube")
                    {
                        radio_tube.IsChecked = true;

                        if (side == "Front/Rear")
                        {
                            check_front.IsChecked = true;
                            check_rear.IsChecked = true;
                            set_add_cycle_tyre_cell_click();

                        }
                        else if (side == "Front")
                        {
                            check_front.IsChecked = true;
                            check_rear.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                        else
                        {
                            check_rear.IsChecked = true;
                            check_front.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                    }
                    else
                    {
                        radio_tubeless.IsChecked = true;
                        if (side == "Front/Rear")
                        {
                            check_front.IsChecked = true;
                            check_rear.IsChecked = true;
                            set_add_cycle_tyre_cell_click();

                        }
                        else if (side == "Front")
                        {
                            check_front.IsChecked = true;
                            check_rear.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                        else
                        {
                            check_rear.IsChecked = true;
                            check_front.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                    }
                }

                else if (tyre_pattern == "N_Trail")
                {
                    radio_non_trail.IsChecked = true;
                    if (tube == "Tube")
                    {
                        radio_tube.IsChecked = true;

                        if (side == "Front/Rear")
                        {
                            check_front.IsChecked = true;
                            check_rear.IsChecked = true;
                            set_add_cycle_tyre_cell_click();

                        }
                        else if (side == "Front")
                        {
                            check_front.IsChecked = true;
                            check_rear.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                        else
                        {
                            check_rear.IsChecked = true;
                            check_front.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                    }
                    else
                    {
                        radio_tubeless.IsChecked = true;
                        if (side == "Front/Rear")
                        {
                            check_front.IsChecked = true;
                            check_rear.IsChecked = true;
                            set_add_cycle_tyre_cell_click();

                        }
                        else if (side == "Front")
                        {
                            check_front.IsChecked = true;
                            check_rear.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                        else
                        {
                            check_rear.IsChecked = true;
                            check_front.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                    }

                }
                row_count_update = e.RowIndex;
                string brand_name = com_brand.Text;
                string q1 = "SELECT TP.pattern_name FROM thread_pattern TP,brand B WHERE TP.brand_id = B.brand_id AND B.brand_name = '" + brand_name + "' ";
                DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with patterns which stoed in thread pattern table 
                if (ds_thread_pattern != null) // if data set is not null
                {

                    com_t_pattern.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                    com_t_pattern.DisplayMember = "pattern_name";
                    com_t_pattern.ValueMember = "pattern_name";
                    com_t_pattern.Text = tp;

                }
                else
                    com_t_pattern.DataSource = null; //fill table 
            }


        }

        private void dtp_new_stock_ValueChanged(object sender, EventArgs e)
        {

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            press_enter();
        }

        public void press_enter()
        {
            if (((check_front.IsChecked == true) || (check_rear.IsChecked == true)) && ((radio_trail.IsChecked == true) || (radio_non_trail.IsChecked == true)) && ((radio_tube.IsChecked == true) || (radio_tubeless.IsChecked == true)) && com_brand.Text != "" && com_made.Text != "" && com_ply_rate.Text != "" && txt_size.Text != "" && com_t_pattern.Text != "")
            {
                set_tyep_tube_val();
                string q = "SELECT * FROM add_cycle_tyre A WHERE A.t_brand = '" + com_brand.Text + "' AND A.t_size = '" + txt_size.Text + "' AND A.t_ply_rate = '" + com_ply_rate.Text + "' AND t_make = '" + com_made.Text + "' AND t_thread_pattern = '" + com_t_pattern.Text + "' AND t_side = '" + check_val_side + "' AND t_tube = '" + radio_val_tube + "' AND t_tyre_pattern = '" + radio_val_tyre_type + "'";
                DataSet ds_add_vahical_tyre = middle_access.db_access.SelectData(q);

                if (ds_add_vahical_tyre == null)
                {
                    string date = dtp_new_stock.Value.Date.ToString("yyyy-MM-dd");
                    string q1 = "INSERT INTO add_cycle_tyre(t_stok_date,t_tyre_pattern,t_tube,t_side,t_brand,t_size,t_ply_rate,t_make,t_thread_pattern)  VALUES('" + date + "','" + radio_val_tyre_type + "', '" + radio_val_tube + "','" + check_val_side + "','" + com_brand.Text + "', '" + txt_size.Text + "', '" + com_ply_rate.Text + "','" + com_made.Text + "','" + com_t_pattern.Text + "')";
                    bool status = middle_access.db_access.InsertData(q1);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        fill_add_tyre_table();// fill brand name table.
                        radGridView1.Focus();
                        int row_count = radGridView1.RowCount;
                        this.radGridView1.Rows[row_count - 1].IsCurrent = true;

                        dtp_new_stock.Value = DateTime.Today.Date;
                        com_ply_rate.Text = "";
                        com_t_pattern.Text = "";
                        //com_ply_rate.Focus();

                    }
                    else // if data is not insert
                    {

                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Pattern is already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtp_new_stock.Value = DateTime.Today.Date;
                    check_front.IsChecked = false;
                    check_rear.IsChecked = false;
                    radio_trail.IsChecked = false;
                    radio_non_trail.IsChecked = false;
                    radio_tube.IsChecked = false;
                    radio_tubeless.IsChecked = false;

                    com_brand.Text = "";
                    com_made.Text = "";
                    com_ply_rate.Text = "";
                    txt_size.Text = "";
                    com_t_pattern.Text = "";
                }


            }
            else
                MessageBox.Show("Enter detalils correctly!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void set_tyep_tube_val()
        {
            if (radio_trail.IsChecked == true)
            {
                radio_val_tyre_type = "Trail";
                if (radio_tube.IsChecked == true)
                {
                    radio_val_tube = "Tube";
                    if (check_front.IsChecked == true && check_rear.IsChecked == true)
                    {
                        //radio_val_side = "Front";
                        check_val_side = "Front/Rear";
                    }
                    else if (check_front.IsChecked == true)
                    {
                        //radio_val_side = "Rear";
                        check_val_side = "Front";
                    }
                    else
                    {
                        check_val_side = "Rear";
                    }
                }
                else
                {
                    radio_val_tube = "Tubeless";
                    if (check_front.IsChecked == true && check_rear.IsChecked == true)
                    {
                        //radio_val_side = "Front";
                        check_val_side = "Front/Rear";
                    }
                    else if (check_front.IsChecked == true)
                    {
                        //radio_val_side = "Rear";
                        check_val_side = "Front";
                    }
                    else
                    {
                        check_val_side = "Rear";
                    }
                }
            }

            else if (radio_non_trail.IsChecked == true)
            {
                radio_val_tyre_type = "N_Trail";
                if (radio_tube.IsChecked == true)
                {
                    radio_val_tube = "Tube";
                    if (check_front.IsChecked == true && check_rear.IsChecked == true)
                    {
                        //radio_val_side = "Front";
                        check_val_side = "Front/Rear";
                    }
                    else if (check_front.IsChecked == true)
                    {
                        //radio_val_side = "Rear";
                        check_val_side = "Front";
                    }
                    else
                    {
                        check_val_side = "Rear";
                    }
                }
                else
                {
                    radio_val_tube = "Tubeless";
                    if (check_front.IsChecked == true && check_rear.IsChecked == true)
                    {
                        //radio_val_side = "Front";
                        check_val_side = "Front/Rear";
                    }
                    else if (check_front.IsChecked == true)
                    {
                        //radio_val_side = "Rear";
                        check_val_side = "Front";
                    }
                    else
                    {
                        check_val_side = "Rear";
                    }
                }

            }




        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            dtp_new_stock.Value = DateTime.Today.Date;
            check_front.IsChecked = false;
            check_rear.IsChecked = false;
            radio_trail.IsChecked = false;
            radio_non_trail.IsChecked = false;
            radio_tube.IsChecked = false;
            radio_tubeless.IsChecked = false;

            com_brand.Text = "";
            com_made.Text = "";
            com_ply_rate.Text = "";
            txt_size.Text = "";
            com_t_pattern.Text = "";
        }

        public void set_add_cycle_tyre_cell_click()
        {
            set_tyep_tube_val();
            string q = "SELECT * FROM add_cycle_tyre A WHERE A.t_brand = '" + com_brand.Text + "' AND A.t_size = '" + txt_size.Text + "' AND A.t_ply_rate = '" + com_ply_rate.Text + "' AND t_make = '" + com_made.Text + "' AND t_thread_pattern = '" + com_t_pattern.Text + "' AND t_side = '" + check_val_side + "' AND t_tube = '" + radio_val_tube + "' AND t_tyre_pattern = '" + radio_val_tyre_type + "'";
            ds_set_add_cycle_tyre_cell_click = middle_access.db_access.SelectData(q);
        }


        private void radButton3_Click(object sender, EventArgs e)
        {
            if (((check_front.IsChecked == true) || (check_rear.IsChecked == true)) && ((radio_trail.IsChecked == true) || (radio_non_trail.IsChecked == true)) && ((radio_tube.IsChecked == true) || (radio_tubeless.IsChecked == true)) && com_brand.Text != "" && com_made.Text != "" && com_ply_rate.Text != "" && txt_size.Text != "" && com_t_pattern.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    set_tyep_tube_val();
                    string q = "SELECT * FROM add_cycle_tyre A WHERE A.t_brand = '" + com_brand.Text + "' AND A.t_size = '" + txt_size.Text + "' AND A.t_ply_rate = '" + com_ply_rate.Text + "' AND t_make = '" + com_made.Text + "' AND t_thread_pattern = '" + com_t_pattern.Text + "' AND t_side = '" + check_val_side + "' AND t_tube = '" + radio_val_tube + "' AND t_tyre_pattern = '" + radio_val_tyre_type + "'";
                    DataSet ds_add_cycle_tyre = middle_access.db_access.SelectData(q);

                    if (ds_add_cycle_tyre == null)
                    {
                        DataRow row_add_cycle_tyre = ds_set_add_cycle_tyre_cell_click.Tables[0].Rows[0];

                        string tyre_pattern = row_add_cycle_tyre.ItemArray.GetValue(2).ToString();
                        string tube = row_add_cycle_tyre.ItemArray.GetValue(3).ToString();
                        string side = row_add_cycle_tyre.ItemArray.GetValue(4).ToString();
                        string brand = row_add_cycle_tyre.ItemArray.GetValue(5).ToString();
                        string size = row_add_cycle_tyre.ItemArray.GetValue(6).ToString();
                        string ply_rate = row_add_cycle_tyre.ItemArray.GetValue(7).ToString();
                        string make = row_add_cycle_tyre.ItemArray.GetValue(8).ToString();
                        string thread_pattern = row_add_cycle_tyre.ItemArray.GetValue(9).ToString();

                        cycle_category_data.brand = brand;
                        cycle_category_data.size = size;
                        cycle_category_data.ply_rate = ply_rate;
                        cycle_category_data.thread_pattern = thread_pattern;
                        cycle_category_data.make = make;
                        cycle_category_data.tyre_pattern = tyre_pattern;
                        cycle_category_data.tube = tube;
                        cycle_category_data.side = side;

                        set_tyep_tube_val();
                        string date = dtp_new_stock.Value.Date.ToString("yyyy-MM-dd");

                        string q2 = "UPDATE add_cycle_tyre SET t_stok_date = '" + date + "',t_tyre_pattern = '" + radio_val_tyre_type + "',t_tube = '" + radio_val_tube + "',t_side = '" + check_val_side + "',t_brand = '" + com_brand.Text + "',t_size = '" + txt_size.Text + "',t_ply_rate = '" + com_ply_rate.Text + "',t_make = '" + com_made.Text + "',t_thread_pattern = '" + com_t_pattern.Text + "' WHERE t_stok_id = " + category_id + " ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            int row_index = row_count_update; 
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_add_tyre_table();
                            radGridView1.Focus();
                            this.radGridView1.Rows[row_index].IsCurrent = true;

                            dtp_new_stock.Value = DateTime.Today.Date;
                            check_front.IsChecked = false;
                            check_rear.IsChecked = false;
                            radio_trail.IsChecked = false;
                            radio_non_trail.IsChecked = false;
                            radio_tube.IsChecked = false;
                            radio_tubeless.IsChecked = false;

                            com_brand.Text = "";
                            com_made.Text = "";
                            com_ply_rate.Text = "";
                            txt_size.Text = "";
                            com_t_pattern.Text = "";
                           

                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dtp_new_stock.Value = DateTime.Today.Date;
                            check_front.IsChecked = false;
                            check_rear.IsChecked = false;
                            radio_trail.IsChecked = false;
                            radio_non_trail.IsChecked = false;
                            radio_tube.IsChecked = false;
                            radio_tubeless.IsChecked = false;

                            com_brand.Text = "";
                            com_made.Text = "";
                            com_ply_rate.Text = "";
                            txt_size.Text = "";
                            com_t_pattern.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Alrady exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtp_new_stock.Value = DateTime.Today.Date;
                        check_front.IsChecked = false;
                        check_rear.IsChecked = false;
                        radio_trail.IsChecked = false;
                        radio_non_trail.IsChecked = false;
                        radio_tube.IsChecked = false;
                        radio_tubeless.IsChecked = false;

                        com_brand.Text = "";
                        com_made.Text = "";
                        com_ply_rate.Text = "";
                        txt_size.Text = "";
                        com_t_pattern.Text = "";
                    }
                }
                else
                {
                    dtp_new_stock.Value = DateTime.Today.Date;
                    check_front.IsChecked = false;
                    check_rear.IsChecked = false;
                    radio_trail.IsChecked = false;
                    radio_non_trail.IsChecked = false;
                    radio_tube.IsChecked = false;
                    radio_tubeless.IsChecked = false;

                    com_brand.Text = "";
                    com_made.Text = "";
                    com_ply_rate.Text = "";
                    txt_size.Text = "";
                    com_t_pattern.Text = "";
                }
            }
            else
                MessageBox.Show("Select a record first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radButton9_Click(object sender, EventArgs e)
        {
            if (((check_front.IsChecked == true) || (check_rear.IsChecked == true)) && ((radio_trail.IsChecked == true) || (radio_non_trail.IsChecked == true)) && ((radio_tube.IsChecked == true) || (radio_tubeless.IsChecked == true)) && com_brand.Text != "" && com_made.Text != "" && com_ply_rate.Text != "" && txt_size.Text != "" && com_t_pattern.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    set_tyep_tube_val();


                    string q2 = "DELETE FROM add_cycle_tyre  WHERE t_stok_id=" + category_id + "";
                    bool status = middle_access.db_access.DeleteData(q2);
                    if (status == true)
                    {
                        string q3 = "DELETE FROM qty_cycle  WHERE category_id = " + category_id + " ";
                        middle_access.db_access.DeleteData(q3);

                        MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fill_add_tyre_table();
                        radGridView1.Focus();
                        dtp_new_stock.Value = DateTime.Today.Date;
                        check_front.IsChecked = false;
                        check_rear.IsChecked = false;
                        radio_trail.IsChecked = false;
                        radio_non_trail.IsChecked = false;
                        radio_tube.IsChecked = false;
                        radio_tubeless.IsChecked = false;

                        com_brand.Text = "";
                        com_made.Text = "";
                        com_ply_rate.Text = "";
                        txt_size.Text = "";
                        com_t_pattern.Text = "";

                    }

                    else
                    {
                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {

                }

            }
            else
                MessageBox.Show("Select a record first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void AddMoterCycleTyre_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                moveStart = new Point(e.X, e.Y);
            }
        }

        private void AddMoterCycleTyre_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Point deltaPos = new Point(e.X - moveStart.X, e.Y - moveStart.Y);
                this.Location = new Point(this.Location.X + deltaPos.X,
                  this.Location.Y + deltaPos.Y);
            }
        }

        private void com_brand_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        private void com_size_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        private void com_ply_rate_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        private void com_made_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        private void com_t_pattern_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        private void radGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (val == -1)
                {

                }
                else
                {
                    if (val >= 0)
                    {
                        dtp_new_stock.Value = Convert.ToDateTime(radGridView1.Rows[val].Cells[6].Value);
                        com_brand.Text = radGridView1.Rows[val].Cells[0].Value.ToString();
                        txt_size.Text = radGridView1.Rows[val].Cells[1].Value.ToString();
                        com_made.Text = radGridView1.Rows[val].Cells[3].Value.ToString();
                        com_ply_rate.Text = radGridView1.Rows[val].Cells[4].Value.ToString();
                        com_t_pattern.Text = radGridView1.Rows[val].Cells[5].Value.ToString();
                        string tp = com_t_pattern.Text;

                        string tyre_pattern = radGridView1.Rows[val].Cells[7].Value.ToString();
                        string tube = radGridView1.Rows[val].Cells[8].Value.ToString();
                        string side = radGridView1.Rows[val].Cells[2].Value.ToString();


                        cycle_category_data.brand = com_brand.Text;
                        cycle_category_data.size = txt_size.Text;
                        cycle_category_data.ply_rate = com_ply_rate.Text;
                        cycle_category_data.thread_pattern = com_t_pattern.Text;
                        cycle_category_data.make = com_made.Text;
                        cycle_category_data.tyre_pattern = tyre_pattern;
                        cycle_category_data.tube = tube;
                        cycle_category_data.side = side;


                        category_id = cycle_category_data.get_catagory_id();

                        if (tyre_pattern == "Trail")
                        {
                            radio_trail.IsChecked = true;

                            if (tube == "Tube")
                            {
                                radio_tube.IsChecked = true;

                                if (side == "Front/Rear")
                                {
                                    check_front.IsChecked = true;
                                    check_rear.IsChecked = true;
                                    set_add_cycle_tyre_cell_click();

                                }
                                else if (side == "Front")
                                {
                                    check_front.IsChecked = true;
                                    check_rear.IsChecked = false;
                                    set_add_cycle_tyre_cell_click();
                                }
                                else
                                {
                                    check_rear.IsChecked = true;
                                    check_front.IsChecked = false;
                                    set_add_cycle_tyre_cell_click();
                                }
                            }
                            else
                            {
                                radio_tubeless.IsChecked = true;
                                if (side == "Front/Rear")
                                {
                                    check_front.IsChecked = true;
                                    check_rear.IsChecked = true;
                                    set_add_cycle_tyre_cell_click();

                                }
                                else if (side == "Front")
                                {
                                    check_front.IsChecked = true;
                                    check_rear.IsChecked = false;
                                    set_add_cycle_tyre_cell_click();
                                }
                                else
                                {
                                    check_rear.IsChecked = true;
                                    check_front.IsChecked = false;
                                    set_add_cycle_tyre_cell_click();
                                }
                            }
                        }

                        else if (tyre_pattern == "N_Trail")
                        {
                            radio_non_trail.IsChecked = true;
                            if (tube == "Tube")
                            {
                                radio_tube.IsChecked = true;

                                if (side == "Front/Rear")
                                {
                                    check_front.IsChecked = true;
                                    check_rear.IsChecked = true;
                                    set_add_cycle_tyre_cell_click();

                                }
                                else if (side == "Front")
                                {
                                    check_front.IsChecked = true;
                                    check_rear.IsChecked = false;
                                    set_add_cycle_tyre_cell_click();
                                }
                                else
                                {
                                    check_rear.IsChecked = true;
                                    check_front.IsChecked = false;
                                    set_add_cycle_tyre_cell_click();
                                }
                            }
                            else
                            {
                                radio_tubeless.IsChecked = true;
                                if (side == "Front/Rear")
                                {
                                    check_front.IsChecked = true;
                                    check_rear.IsChecked = true;
                                    set_add_cycle_tyre_cell_click();

                                }
                                else if (side == "Front")
                                {
                                    check_front.IsChecked = true;
                                    check_rear.IsChecked = false;
                                    set_add_cycle_tyre_cell_click();
                                }
                                else
                                {
                                    check_rear.IsChecked = true;
                                    check_front.IsChecked = false;
                                    set_add_cycle_tyre_cell_click();
                                }
                            }

                        }

                        row_count_update = radGridView1.RowCount; ;
                        string brand_name = com_brand.Text;
                        string q1 = "SELECT TP.pattern_name FROM thread_pattern TP,brand B WHERE TP.brand_id = B.brand_id AND B.brand_name = '" + brand_name + "' ";
                        DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with patterns which stoed in thread pattern table 
                        if (ds_thread_pattern != null) // if data set is not null
                        {

                            com_t_pattern.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                            com_t_pattern.DisplayMember = "pattern_name";
                            com_t_pattern.ValueMember = "pattern_name";
                            com_t_pattern.Text = tp;

                        }
                        else
                            com_t_pattern.DataSource = null; //fill table 

                    }
                  
                }
            }
        }

        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            val = radGridView1.CurrentRow.Index;
            row_count_update = radGridView1.CurrentRow.Index;
        }

        private void com_brand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                press_enter();
            }
        }

        private void com_size_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                press_enter();
            }
        }

        private void com_ply_rate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                press_enter();
            }
        }

        private void com_made_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                press_enter();
            }
        }

        private void com_t_pattern_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                press_enter();
            }
        }

        private void txt_size_Enter(object sender, EventArgs e)
        {
            string a = txt_size.Text;
            if (a == "")
            {
                fill_size();

            }
            else
            {
                filter_text_box_text_size(a);

            }
            radGridView2.Visible = true;
        }

        public void filter_text_box_text_size(string text_text)
        {
            string q1 = "SELECT tyer_size FROM size WHERE tyer_size  LIKE  '" + text_text + "%' ";
            ds_size = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_size != null) // if data set is not null
            {
                radGridView2.Visible = false;
                radGridView2.Visible = true;
                radGridView2.DataSource = ds_size.Tables[0];


            }
            else
            {
                radGridView2.DataSource = null;
                radGridView2.Visible = false;
            }

        }

        private void txt_size_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Down)
            {
                if (ds_size != null)
                {
                    radGridView2.Focus();
                    this.radGridView2.Rows[0].IsCurrent = true;
                }
            }

            else if (e.KeyData == Keys.Enter)
            {
                radGridView2.Visible = false;
            }
            
        }

        private void txt_size_TextChanged(object sender, EventArgs e)
        {
            if (txt_size.Text == "")
            {
                radGridView2.DataSource = null;
                radGridView2.Refresh();
                radGridView2.Visible = false;


            }
            else
            {

                string a = txt_size.Text; ;
                filter_text_box_text_size(a);
                if (ds_size != null)
                {
                    radGridView2.Visible = true;
                    radGridView2.Refresh();
                    // radGridView2.Focus();
                    //this.radGridView2.Rows[1].IsCurrent = true;
                    //txt_size.Focus();
                }
            }
        }

        private void radGridView2_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_size.Text = radGridView2.Rows[row_index].Cells[0].Value.ToString();
            txt_size.Focus();
            radGridView2.Visible = false;
        }

        private void radGridView2_SelectionChanged(object sender, EventArgs e)
        {
            row_index = radGridView2.CurrentRow.Index;
        }

        private void radGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txt_size.Text = radGridView2.Rows[row_index].Cells[0].Value.ToString();
                txt_size.Focus();
                radGridView2.Visible = false;

            }

            else if (e.KeyData == Keys.U)
            {
                if (row_index == 0)
                {
                    txt_size.Focus();
                }
            }
               

            
        }

        private void AddMoterCycleTyre_Click(object sender, EventArgs e)
        {
            radGridView2.Visible = false;
        }

        private void radButton10_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true; 
        }
    }
}
