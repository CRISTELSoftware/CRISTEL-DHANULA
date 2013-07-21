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
    /// <summary>
    /// 
    /// </summary>
    public partial class AddNewStock : Telerik.WinControls.UI.RadForm
    {
        Point moveStart;

        string radial;
        string canvas;
        string tube;
        string tube_less;

        int category_id;
        int val;
        int row_count_update;
        int row_index;
        string radio_val_type = null;
        string radio_val_tube = null;

        DataSet ds_add_vehicel_tyre_cell_click;
        DataSet ds_size;

        string brand_x;

        public AddNewStock()
        {
            InitializeComponent();
        }

        private void AddNewStock_Load(object sender, EventArgs e)
        {
            fill_add_tyre_table();
            fill_com_brand();
            fill_size();
            fill_com_ply_rate();
            fill_com_make();
            fill_com_thread_pattern_load();
            dtp_stok_date.Value = DateTime.Today.Date;
            radGridView2.Visible = false;
              
        }

        public void fill_add_tyre_table()
        {
            
            string q = "SELECT t_brand AS Brand, t_size AS Size, t_make AS Make, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern, t_stok_date AS Date, t_type AS Type, t_tube AS Tube_type FROM add_vehical_tyre ";
            DataSet ds_add_vehical_tyre = middle_access.db_access.SelectData(q);
            if (ds_add_vehical_tyre != null)
            {
                radGridView1.DataSource = ds_add_vehical_tyre.Tables[0].DefaultView;
            }
            else
                radGridView1.DataSource = null;
        }

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

        public void fill_com_thread_pattern()
        {
            string brand_name = com_brand.Text;
            string q1 = "SELECT TP.pattern_name FROM thread_pattern TP,brand B WHERE TP.brand_id = B.brand_id AND B.brand_name = '"+ brand_name +"' ";
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

            string q1 = "SELECT tyer_size FROM size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                radGridView2.DataSource = ds_size.Tables[0]; //fill table 
                

            }
            else
                radGridView2.DataSource = null; //fill table 
        }

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

        

        private void radButton8_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHANULA"];
            f.Enabled = true;  
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void radRadioButton4_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void com_brand_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_com_thread_pattern();

                        
        }

        private void radRadioButton6_Click(object sender, EventArgs e)
        {
            radial = "Radial";
            canvas = "";
           
        }

        private void radRadioButton5_Click(object sender, EventArgs e)
        {
            radial = "";
            canvas = "Canvas";
           
        }

        private void radRadioButton4_Click(object sender, EventArgs e)
        {
           
            tube = "Tube";
            tube_less = "";
        }

        private void radRadioButton3_Click(object sender, EventArgs e)
        {
            
            tube = "";
            tube_less = "Tubeless";
        }

        /// <summary>
        /// Set_tyep_tube_vals this instance.
        /// </summary>
        public void set_tyep_tube_val()
        {
            if (rad_radial.IsChecked == true)
            {
                radio_val_type = "Radial";
                if (rad_tube.IsChecked == true)
                {
                    radio_val_tube = "Tube";
                }
                else
                    radio_val_tube = "Tubeless";
            }

            else if (rad_canvas.IsChecked == true)
            {
                radio_val_type = "Canvas";
                if (rad_tube.IsChecked == true)
                {
                    radio_val_tube = "Tube";
                }
                else
                    radio_val_tube = "Tubeless";

            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            press_enter();
        }

        public void press_enter()
        {
            if (((rad_canvas.IsChecked == true) || (rad_radial.IsChecked == true)) && ((rad_tube.IsChecked == true) || (rad_tube_less.IsChecked == true)) && com_brand.Text != "" && com_made.Text != "" && com_ply_rate.Text != "" && txt_size.Text != "" && com_t_pattern.Text != "")
            {


                set_tyep_tube_val();
                string q = "SELECT * FROM add_vehical_tyre A WHERE A.t_brand = '" + com_brand.Text + "' AND A.t_size = '" + txt_size.Text + "' AND A.t_ply_rate = '" + com_ply_rate.Text + "' AND t_make = '" + com_made.Text + "' AND t_thread_pattern = '" + com_t_pattern.Text + "' AND t_type = '" + radio_val_type + "' AND t_tube = '" + radio_val_tube + "'";
                DataSet ds_add_vahical_tyre = middle_access.db_access.SelectData(q);

                if (ds_add_vahical_tyre == null)
                {
                    string date = dtp_stok_date.Value.Date.ToString("yyyy-MM-dd");
                    string q1 = "INSERT INTO add_vehical_tyre(t_stok_date,t_type,t_tube,t_brand,t_size,t_ply_rate,t_make,t_thread_pattern)  VALUES('" + date + "','" + radio_val_type + "', '" + radio_val_tube + "','" + com_brand.Text + "', '" + txt_size.Text + "', '" + com_ply_rate.Text + "','" + com_made.Text + "','" + com_t_pattern.Text + "')";
                    bool status = middle_access.db_access.InsertData(q1);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        fill_add_tyre_table();// fill brand name table.
                        radGridView1.Focus();
                        int row_count = radGridView1.RowCount;
                        this.radGridView1.Rows[row_count-1].IsCurrent = true;
                        //this.radGridView1.MasterGridViewInfo.CurrentRow = this.radGridView1.Rows[row_count-1];               
                        //radGridView1.CurrentRow = radGridView1.Rows[row_count]; 
                       // radGridView1.CurrentCell = radGridView1.Rows[row_count].Cells[1];
                       // radGridView1.Rows[row_count].IsSelected = true;

                        dtp_stok_date.Value = DateTime.Today.Date;
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
                    MessageBox.Show("Pattern is already exiat!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtp_stok_date.Value = DateTime.Today.Date;
                    rad_canvas.IsChecked = false;
                    rad_radial.IsChecked = false;
                    rad_tube.IsChecked = false;
                    rad_tube_less.IsChecked = false;
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
        

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
                dtp_stok_date.Value = Convert.ToDateTime(radGridView1.Rows[e.RowIndex].Cells[5].Value);
                com_brand.Text = radGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

               // com_size.Text = radGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_size.Text = radGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                radGridView2.DataSource = null;
                radGridView2.Visible = false;

                com_made.Text = radGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                com_ply_rate.Text = radGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                com_t_pattern.Text = radGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                string tp = com_t_pattern.Text;
                string type = radGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                string tube = radGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                vehical_category_data.brand = com_brand.Text;
                vehical_category_data.size = txt_size.Text;
                vehical_category_data.ply_rate = com_ply_rate.Text;
                vehical_category_data.thread_pattern = com_t_pattern.Text;
                vehical_category_data.make = com_made.Text;
                vehical_category_data.type = type;
                vehical_category_data.tube = tube;

                category_id = vehical_category_data.get_catagory_id();

                if (type == "Radial")
                {
                    rad_radial.IsChecked = true;
                    if (tube == "Tube")
                    {
                        rad_tube.IsChecked = true;
                        set_add_vehicel_tyre_cell_click();
                    }
                    else
                    {
                        rad_tube_less.IsChecked = true;
                        set_add_vehicel_tyre_cell_click();
                    }
                }

                else if (type == "Canvas")
                {
                    rad_canvas.IsChecked = true;
                    if (tube == "Tube")
                    {
                        rad_tube.IsChecked = true;
                        set_add_vehicel_tyre_cell_click();
                    }
                    else
                    {
                        rad_tube_less.IsChecked = true;
                        set_add_vehicel_tyre_cell_click();
                    }


                }

                
                else
                {

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

        public void set_add_vehicel_tyre_cell_click()
        {
            set_tyep_tube_val();
            string q1 = "SELECT * FROM add_vehical_tyre A WHERE A.t_brand = '" + com_brand.Text + "' AND A.t_size = '" + txt_size.Text + "' AND A.t_ply_rate = '" + com_ply_rate.Text + "' AND t_make = '" + com_made.Text + "' AND t_thread_pattern = '" + com_t_pattern.Text + "' AND t_type = '" + radio_val_type + "' AND t_tube = '" + radio_val_tube + "'";
            ds_add_vehicel_tyre_cell_click = middle_access.db_access.SelectData(q1);
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            if (((rad_canvas.IsChecked == true) || (rad_radial.IsChecked == true)) && ((rad_tube.IsChecked == true) || (rad_tube_less.IsChecked == true)) && com_brand.Text != "" && com_made.Text != "" && com_ply_rate.Text != "" && txt_size.Text != "" && com_t_pattern.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    set_tyep_tube_val();
                    string q1 = "SELECT * FROM add_vehical_tyre A WHERE A.t_brand = '" + com_brand.Text + "' AND A.t_size = '" + txt_size.Text + "' AND A.t_ply_rate = '" + com_ply_rate.Text + "' AND t_make = '" + com_made.Text + "' AND t_thread_pattern = '" + com_t_pattern.Text + "' AND t_type = '" + radio_val_type + "' AND t_tube = '" + radio_val_tube + "' ";
                    DataSet ds_add_vehicel_tyre = middle_access.db_access.SelectData(q1);

                    if (ds_add_vehicel_tyre == null)
                    {
                        DataRow row_add_vehical_tyre = ds_add_vehicel_tyre_cell_click.Tables[0].Rows[0];

                        string type = row_add_vehical_tyre.ItemArray.GetValue(2).ToString();
                        string tube = row_add_vehical_tyre.ItemArray.GetValue(3).ToString();
                        string brand = row_add_vehical_tyre.ItemArray.GetValue(4).ToString();
                        string size = row_add_vehical_tyre.ItemArray.GetValue(5).ToString();
                        string ply_rate = row_add_vehical_tyre.ItemArray.GetValue(6).ToString() ;
                        string make = row_add_vehical_tyre.ItemArray.GetValue(7).ToString();
                        string thread_pttern = row_add_vehical_tyre.ItemArray.GetValue(8).ToString();

                        vehical_category_data.brand = brand;
                        vehical_category_data.size = size;
                        vehical_category_data.ply_rate = ply_rate;
                        vehical_category_data.thread_pattern = thread_pttern;
                        vehical_category_data.make = make;
                        vehical_category_data.type = type;
                        vehical_category_data.tube = tube;

                        set_tyep_tube_val();
                        string date = dtp_stok_date.Value.Date.ToString("yyyy-MM-dd");
                        string q2 = "UPDATE add_vehical_tyre SET t_stok_date = '" + date + "',t_type = '" + radio_val_type + "',t_tube = '" + radio_val_tube + "',t_brand = '" + com_brand.Text + "',t_size = '" + txt_size.Text + "',t_ply_rate = '" + com_ply_rate.Text + "',t_make = '" + com_made.Text + "',t_thread_pattern = '" + com_t_pattern.Text + "' WHERE t_type = '" + type + "' AND t_tube = '" + tube + "' AND t_brand = '" + brand + "' AND t_size = '" + size + "' AND t_ply_rate = " + ply_rate + " AND t_make = '" + make + "' AND t_thread_pattern = '" + thread_pttern + "'  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            int row_index = row_count_update; 
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_add_tyre_table();
                            radGridView1.Focus();
                            this.radGridView1.Rows[row_index].IsCurrent = true;

                            dtp_stok_date.Value = DateTime.Today.Date;
                            rad_canvas.IsChecked = false;
                            rad_radial.IsChecked = false;
                            rad_tube.IsChecked = false;
                            rad_tube_less.IsChecked = false;
                            com_brand.Text = "";
                            com_made.Text = "";
                            com_ply_rate.Text = "";
                            txt_size.Text = "";
                            com_t_pattern.Text = "";
                            
                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dtp_stok_date.Value = DateTime.Today.Date;
                            rad_canvas.IsChecked = false;
                            rad_radial.IsChecked = false;
                            rad_tube.IsChecked = false;
                            rad_tube_less.IsChecked = false;
                            com_brand.Text = "";
                            com_made.Text = "";
                            com_ply_rate.Text = "";
                            txt_size.Text = "";
                            com_t_pattern.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Already exist!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtp_stok_date.Value = DateTime.Today.Date;
                        rad_canvas.IsChecked = false;
                        rad_radial.IsChecked = false;
                        rad_tube.IsChecked = false;
                        rad_tube_less.IsChecked = false;
                        com_brand.Text = "";
                        com_made.Text = "";
                        com_ply_rate.Text = "";
                        txt_size.Text = "";
                        com_t_pattern.Text = "";
                    }
                }
                else
                {
                    dtp_stok_date.Value = DateTime.Today.Date;
                    rad_canvas.IsChecked = false;
                    rad_radial.IsChecked = false;
                    rad_tube.IsChecked = false;
                    rad_tube_less.IsChecked = false;
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
            if (((rad_canvas.IsChecked == true) || (rad_radial.IsChecked == true)) && ((rad_tube.IsChecked == true) || (rad_tube_less.IsChecked == true)) && com_brand.Text != "" && com_made.Text != "" && com_ply_rate.Text != "" && txt_size.Text != "" && com_t_pattern.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    set_tyep_tube_val();
                    int catagory_id = vehical_category_data.get_catagory_id();

                    string q2 = "DELETE FROM add_vehical_tyre  WHERE t_brand = '" + com_brand.Text + "' AND t_size = '" + txt_size.Text + "' AND t_ply_rate = '" + com_ply_rate.Text + "' AND t_make = '" + com_made.Text + "' AND t_thread_pattern = '" + com_t_pattern.Text + "' AND t_type = '" + radio_val_type + "' AND t_tube = '" + radio_val_tube + "'"  ;
                    bool status = middle_access.db_access.DeleteData(q2);
                    if (status == true)
                    {
                        string q3 = "DELETE FROM qty  WHERE category_id = " + catagory_id + " ";
                        middle_access.db_access.DeleteData(q3);

                        MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fill_add_tyre_table();
                        radGridView1.Focus();
                        dtp_stok_date.Value = DateTime.Today.Date;
                        rad_canvas.IsChecked = false;
                        rad_radial.IsChecked = false;
                        rad_tube.IsChecked = false;
                        rad_tube_less.IsChecked = false;
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
                MessageBox.Show("Select a record!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            dtp_stok_date.Value = DateTime.Today.Date;
            rad_canvas.IsChecked = false;
            rad_radial.IsChecked = false;
            rad_tube.IsChecked = false;
            rad_tube_less.IsChecked = false;
            com_brand.Text = "";
            com_made.Text = "";
            com_ply_rate.Text = "";
            txt_size.Text = "";
            com_t_pattern.Text = "";

        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void AddNewStock_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moveStart = new Point(e.X, e.Y);
            }
        }

        private void AddNewStock_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Point deltaPos = new Point(e.X - moveStart.X, e.Y - moveStart.Y);
                this.Location = new Point(this.Location.X + deltaPos.X,
                this.Location.Y + deltaPos.Y);
            }
        }

        private void rad_tube_less_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

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
                        dtp_stok_date.Value = Convert.ToDateTime(radGridView1.Rows[val].Cells[5].Value);
                        com_brand.Text = radGridView1.Rows[val].Cells[0].Value.ToString();
                        txt_size.Text = radGridView1.Rows[val].Cells[1].Value.ToString();
                        com_made.Text = radGridView1.Rows[val].Cells[2].Value.ToString();
                        com_ply_rate.Text = radGridView1.Rows[val].Cells[3].Value.ToString();
                        com_t_pattern.Text = radGridView1.Rows[val].Cells[4].Value.ToString();
                        string tp = com_t_pattern.Text;
                        string type = radGridView1.Rows[val].Cells[6].Value.ToString();
                        string tube = radGridView1.Rows[val].Cells[7].Value.ToString();

                        vehical_category_data.brand = com_brand.Text;
                        vehical_category_data.size = txt_size.Text;
                        vehical_category_data.ply_rate = com_ply_rate.Text;
                        vehical_category_data.thread_pattern = com_t_pattern.Text;
                        vehical_category_data.make = com_made.Text;
                        vehical_category_data.type = type;
                        vehical_category_data.tube = tube;

                        category_id = vehical_category_data.get_catagory_id();

                        if (type == "Radial")
                        {
                            rad_radial.IsChecked = true;
                            if (tube == "Tube")
                            {
                                rad_tube.IsChecked = true;
                                set_add_vehicel_tyre_cell_click();
                            }
                            else
                            {
                                rad_tube_less.IsChecked = true;
                                set_add_vehicel_tyre_cell_click();
                            }
                        }

                        else if (type == "Canvas")
                        {
                            rad_canvas.IsChecked = true;
                            if (tube == "Tube")
                            {
                                rad_tube.IsChecked = true;
                                set_add_vehicel_tyre_cell_click();
                            }
                            else
                            {
                                rad_tube_less.IsChecked = true;
                                set_add_vehicel_tyre_cell_click();
                            }


                        }

                        else
                        {

                        }

                        row_count_update = radGridView1.RowCount - 1; 
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
                    //            this.radGridView1.Rows[val].IsCurrent = true;

                }
              
               

            }
        }

        private void com_brand_TextChanged(object sender, EventArgs e)
        {
           // string a = com_brand.Text;

            //if (a != "System.Data.DataRowView")
           // {
             //   if (a == "")
               // {
            
             //   }
               // else
               // {
                 //   filter_combo_box_text_brand(a);
               // }
                     
          //  }
 
        //   SendKeys.Send("{F4}");
        }

        private void com_t_pattern_KeyDown(object sender, KeyEventArgs e)
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

        private void com_ply_rate_KeyDown(object sender, KeyEventArgs e)
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

        private void com_brand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                press_enter();
            }
         
        }

        private void dtp_stok_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                press_enter();
            }
        }

        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            val = radGridView1.CurrentRow.Index;
            row_count_update = radGridView1.CurrentRow.Index;

        }

        private void com_brand_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
            radGridView2.Visible = false;                      

        }

        private void com_size_Enter(object sender, EventArgs e)
        {
            
            SendKeys.Send("{F4}");
        }

        private void com_ply_rate_Enter(object sender, EventArgs e)
        {
            
            SendKeys.Send("{F4}");
            radGridView2.Visible = false;
        }

        private void com_made_Enter(object sender, EventArgs e)
        {
                   
          
            SendKeys.Send("{F4}");
        }

        private void com_t_pattern_Enter(object sender, EventArgs e)
        {
            
            SendKeys.Send("{F4}");
        }

        private void com_size_TextChanged(object sender, EventArgs e)
        {
           // SendKeys.Send("{F4}");
            
           // string a = com_size.Text;
          //  filter_combo_box_text_size(a);
        }

        private void com_ply_rate_TextChanged(object sender, EventArgs e)
        {
           // SendKeys.Send("{F4}");
        }

        private void com_made_TextChanged(object sender, EventArgs e)
        {
         //   SendKeys.Send("{F4}");
        }

        private void com_t_pattern_TextChanged(object sender, EventArgs e)
        {
          //  SendKeys.Send("{F4}");
        }

        private void radGridView1_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
          
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

     

        private void txt_size_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData ==  Keys.Down)
            {
                if(ds_size != null)
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

        private void radGridView2_SelectionChanged(object sender, EventArgs e)
        {
            row_index = radGridView2.CurrentRow.Index;
            
            
        }

       

        private void radGridView2_CellClick_1(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_size.Text = radGridView2.Rows[row_index].Cells[0].Value.ToString();
            txt_size.Focus();
            radGridView2.Visible = false;

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

        private void com_brand_Click(object sender, EventArgs e)
        {
            radGridView2.Visible = false;
        }

        private void AddNewStock_Click(object sender, EventArgs e)
        {
            radGridView2.Visible = false;
        }

        private void radGridView2_SelectionChanged_1(object sender, EventArgs e)
        {
            row_index = radGridView2.CurrentRow.Index;
        }

        private void radGridView2_Click(object sender, EventArgs e)
        {

        }

        private void radButton7_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;  
        }

      
     

        

      

        

   

        

       
    }
}
