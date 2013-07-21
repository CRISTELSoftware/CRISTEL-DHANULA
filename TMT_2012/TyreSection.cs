using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace TMT_2012
{//currntly editing
    public partial class TyreSection : Telerik.WinControls.UI.RadForm
    {
        Point moveStart;
        DataSet ds_size;
        int row_index;
        int category_id;
        int row_count_update;
        int row_index_settings;
        int val;
        string thread_pattern_x;
        int brand_id_x;
        int brand_id;
        string changed_type;
        string cell_type_name;
        string refresh_vehicle = null;
        string refresh_cycle = null;
        string radio_val_type = null;
        string radio_val_tube = null;

        string radio_val_tyre_type = null;      
        string radio_val_side = null;
        string check_val_side = null;

        DataSet ds_add_vehicel_tyre_cell_click;
        DataSet ds_set_add_cycle_tyre_cell_click;

        

        string front;
        //string rear;
        string tube;
        //string tube_less;
        string trail;
        string non_trail;

        /// <summary>
        /// Initializes a new instance of the <see cref="TyreSection"/> class.
        /// </summary>
        public TyreSection()
        {
            InitializeComponent();
        }

        //AdvancedSearch  search close btn
        private void radButton10_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;  
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }



        private void AdvancedSearch_Load(object sender, EventArgs e)
        {
            lbl_tyre_section_header.Text = "Tyre Section (Search)";
            fill_advance_search_table();
            fill_advance_search_table_cycle();
            fill_com_brand();
            fill_size();
            fill_com_ply_rate();
            fill_com_make();
            label2.Text = "Vehicles";
            radGridView2.Visible = false;
            //check_stock();
            radGridView3.Visible = false;

            com_brand.Text = "";
            txt_size.Text = "";
            com_ply_rate.Text = "";
            com_made.Text = "";

            com_brand.Enabled = false;
            txt_size.Enabled = false;
            txt_size.Enabled = false;
            com_ply_rate.Enabled = false;
            com_made.Enabled = false;
            com_t_pattern.Enabled = false;
            panal_cycle.Enabled = false;

            set_total_qty();
        }

        public void set_total_qty()
        {
            string q = null;
            if (chk_cycle_search.Checked != true)
            {          
                q = "SELECT SUM(qty) FROM add_vehical_tyre";        
            }
            else if (chk_cycle_search.Checked == true)
            {
                q = "SELECT SUM(qty) FROM add_cycle_tyre";        
            }
          

           
            DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
            DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
            int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());
            
            lbl_tot.Text = total_qty.ToString();
            lbl_sh_qty.Text = "0";

        }


        public void set_total_qty_after_changed()
        {
            string q = null;
            if (chk_cycle_search.Checked != true)
            {
                q = "SELECT SUM(qty) FROM add_vehical_tyre";
            }
            else if (chk_cycle_search.Checked == true)
            {
                q = "SELECT SUM(qty) FROM add_cycle_tyre";
            }



            DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
            DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
            int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());

            lbl_tot.Text = total_qty.ToString();
            
        }

        public void set_searched_qty(string where_value)
        {
            string q = null;
            string value = where_value;
            if (value == null)
            {
                lbl_sh_qty.Text = "0";
            }
            else
            {
                if (chk_cycle_search.Checked != true)
                {
                    q = "SELECT SUM(qty) FROM add_vehical_tyre WHERE " + value + "";
                }
                else if (chk_cycle_search.Checked == true)
                {
                    q = "SELECT SUM(qty) FROM add_cycle_tyre WHERE " + value + "";
                }




                DataSet ds_searched_qty = middle_access.db_access.SelectData(q);
                if (ds_searched_qty != null)
                {
                    DataRow row_searched_qty = ds_searched_qty.Tables[0].Rows[0];
                    string  searched_qty = row_searched_qty.ItemArray.GetValue(0).ToString();
                    if (searched_qty == "")
                    {
                        lbl_sh_qty.Text = "0";
                    }
                    else
                    {
                        lbl_sh_qty.Text = searched_qty.ToString();
                    }
                }
                else
                {
                    lbl_sh_qty.Text = "0";
                }
            }
        }

        //public void set_no_of_brands()
        //{
        //    string q = null;
        //    if (chk_cycle_search.Checked != true)
        //    {
        //        q = "SELECT DISTINCT COUNT(qty) FROM add_vehical_tyre"; ;
        //    }
        //    else if (chk_cycle_search.Checked == true)
        //    {
        //        q = "SELECT DISTINCT COUNT(qty) FROM add_cycle_tyre";
        //    }

        //    DataSet ds_no_of_brands = Data.DataAccess.GetData(q);
        //    DataRow row_no_of_brands = ds_no_of_brands.Tables[0].Rows[0];
        //    int total_qty = Convert.ToInt32(row_no_of_brands.ItemArray.GetValue(0).ToString());

        //    lbl_no_of_brands.Text = total_qty.ToString(); 
                 
        //}


        public void fill_advance_search_table()
        {
            string q = "SELECT t_brand AS Brand, t_size AS Size, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_type AS Type,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price FROM add_vehical_tyre ";
            DataSet ds_add_vehical_tyre = middle_access.db_access.SelectData(q);
            if (ds_add_vehical_tyre != null)
            {
                radGridView1.DataSource = ds_add_vehical_tyre.Tables[0].DefaultView;
            }
            else
                radGridView1.DataSource = null;
        }


        public void fill_advance_search_table_cycle()
        {
            string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side , t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_tyre_pattern AS Tyre_Type ,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price FROM add_cycle_tyre ";
            DataSet ds_add_cycle_tyre = middle_access.db_access.SelectData(q);
            if (ds_add_cycle_tyre != null)
            {
                radGridView2.DataSource = ds_add_cycle_tyre.Tables[0].DefaultView;
            }
            else
                radGridView2.DataSource = null;
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

        //public void fill_com_size()
        //{

        //    string q1 = "SELECT * FROM size S";
        //    DataSet ds_size = Data.DataAccess.GetData(q1); // fill data set with sizes which stoed in size table 
        //    if (ds_size != null) // if data set is not null
        //    {

        //        com_size.DataSource = ds_size.Tables[0]; //fill table 
        //        com_size.DisplayMember = "tyer_size";
        //        com_size.ValueMember = "tyer_size";
        //        com_size.Text = "";
        //    }
        //    else
        //        com_t_pattern.DataSource = null; //fill table 
        //}


        public void fill_size()
        {

            string q1 = "SELECT tyer_size FROM size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                radGridView3.DataSource = ds_size.Tables[0]; //fill table 


            }
            else
                radGridView3.DataSource = null; //fill table 
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

        public void check_stock()
        {
            int row_count = radGridView1.RowCount;


            for (int row = 0; row <= row_count; row++)
            {
                int catagory_id = vehical_category_data.get_catagory_id();
                string q = "SELECT * FROM qty WHERE category_id = " + catagory_id + "";
                DataSet ds_catagory_id = middle_access.db_access.SelectData(q);

                if (ds_catagory_id != null)
                {
                    DataRow row_catagory_id = ds_catagory_id.Tables[0].Rows[0];
                    int available_qty = Convert.ToInt32(row_catagory_id.ItemArray.GetValue(1).ToString());
                    if (available_qty == 0)
                    {
                        //set color
                    }

                }
            }

        }


        private void com_brand_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_com_thread_pattern();
        }

        private void radCheckBox2_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = false;
            if (chk_brand.Checked == false)
            {
                com_brand.Enabled = true;
            }
            else
            {
                com_brand.Enabled = false;
                com_brand.Text = "";
            }
        }

        private void chk_size_Click(object sender, EventArgs e)
        {
            if (chk_size.Checked == false)
            {
                txt_size.Enabled = true;
            }
            else
            {
                txt_size.Enabled = false;
                txt_size.Text = "";
                radGridView3.Visible = false;
                
            }
        }

        private void chk_ply_rate_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = false;
            if (chk_ply_rate.Checked == false)
            {
                com_ply_rate.Enabled = true;
            }
            else
            {
                com_ply_rate.Enabled = false;
                com_ply_rate.Text = "";
            }
        }

        private void chk_t_pattern_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = false;
            if (chk_t_pattern.Checked == false)
            {
                com_t_pattern.Enabled = true;
            }
            else
            {
                com_t_pattern.Enabled = false;
                com_t_pattern.Text = "";
            }
        }

        private void chk_make_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = false;
            if (chk_make.Checked == false)
            {
                com_made.Enabled = true;
            }
            else
            {
                com_made.Enabled = false;
                com_made.Text = "";
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

            if (chk_cycle_search.Checked != true)
            {
                search();
            }
            else
            {
                search_cycle_stock();
            }

        }

        public void search()
        {
            string qq = set_search_query();
            qq = set_search_query_with_type(qq);
            refresh_vehicle = qq;

            set_searched_qty(qq);
            string q = "SELECT t_brand AS Brand, t_size AS Size, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_type AS Type,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price   FROM add_vehical_tyre WHERE " + qq + " ";
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

        public void search_cycle_stock()
        {
            string qq = set_search_query();
            //qq = set_search_query_with_type(qq);
            qq = set_search_query_with_side(qq);
            qq = set_search_query_with_tyre_pattern(qq);
            refresh_cycle = qq;
            set_searched_qty(qq);
            string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side , t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_tyre_pattern AS Tyre_Type ,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price FROM add_cycle_tyre WHERE "+ qq +" ";
            DataSet ds_advance_search = middle_access.db_access.SelectData(q);
            if (ds_advance_search != null)
            {
                radGridView2.DataSource = ds_advance_search.Tables[0].DefaultView;
            }
            else
            {
                radGridView2.DataSource = null;
            }

        }

        public string set_search_query_with_type(string q_)
        {
            string qq = q_;
            if (qq != null)
            {
                if (radio_radial.IsChecked == true)
                {
                    qq = qq + "AND " + "t_type = 'Radial'";
                }
                else if (radio_canvas.IsChecked == true)
                {
                    qq = qq + "AND " + "t_type = 'Canvas'";
                }
            }
            else
            {
                if (radio_radial.IsChecked == true)
                {
                    qq = "t_type = 'Radial'";
                }
                else if (radio_canvas.IsChecked == true)
                {
                    qq = "t_type = 'Canvas'";
                }
            }

            return qq;
        }


        public string set_search_query_with_side(string q_)
        {
            string qq = q_;
            if (qq != null)
            {
                if (check_front.IsChecked && check_rear.IsChecked)
                {
                    qq = qq + "AND " + "t_side = 'Front/Rear'";
                }
                else if (check_front.IsChecked)
                {
                    qq = qq + "AND " + "t_side = 'Front'";
                }
                else if (check_rear.IsChecked)
                {
                    qq = qq + "AND " + "t_side = 'Rear'";
                }
            }
            else
            {
                if (check_front.IsChecked && check_rear.IsChecked)
                {
                    qq = "t_side = 'Front/Rear'";
                }
                else if (check_front.IsChecked)
                {
                    qq = "t_side = 'Front'";
                }
                else if (check_rear.IsChecked)
                {
                    qq = "t_side = 'Rear'";
                }
            }

            return qq;
        }


        public string set_search_query_with_tyre_pattern(string q_)
        {
            string qq = q_;
            if (qq != null)
            {
                if (radio_trial.IsChecked == true)
                {
                    qq = qq + "AND " + "t_tyre_pattern = 'Trail'";
                }
                else if (radio_non_trial.IsChecked == true)
                {
                    qq = qq + "AND " + "t_tyre_pattern = 'N_Trail'";
                }
            }
            else
            {
                if (radio_trial.IsChecked == true)
                {
                    qq = "t_tyre_pattern = 'Trail'";
                }
                else if (radio_non_trial.IsChecked == true)
                {
                    qq = "t_tyre_pattern = 'N_Trail'";
                }
            }

            return qq;
        }



        public string set_search_query()
        {
            string brand = com_brand.Text;
            string size = txt_size.Text;
            string ply_rate = com_ply_rate.Text;
            string thread_pattern = com_t_pattern.Text;
            string make = com_made.Text;

            string qq = null;
            if (chk_brand.Checked == true)
            {
                qq = " t_brand ='" + brand + "' ";
                if (chk_size.Checked == true)
                {
                    qq = qq + "AND" + " t_size = '" + size + "' ";
                    if (chk_ply_rate.Checked == true)
                    {
                        qq = qq + "AND" + " t_ply_rate = '" + ply_rate + "' ";
                        if (chk_t_pattern.Checked == true)
                        {
                            qq = qq + "AND" + " t_thread_pattern = '" + thread_pattern + "' ";
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }

                        }
                        else
                        {
                            if (chk_make.Checked == true)
                            {
                                qq = " t_make = '" + make + "' ";
                            }

                        }
                    }
                    else
                    {
                        if (chk_t_pattern.Checked == true)
                        {
                            qq = " t_thread_pattern = '" + thread_pattern + "' ";
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }

                        }
                        else
                        {
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }
                        }
                    }
                }
                else
                {
                    if (chk_ply_rate.Checked == true)
                    {
                        qq = qq + "AND" + " t_ply_rate = '" + ply_rate + "' ";
                        if (chk_t_pattern.Checked == true)
                        {
                            qq = qq + "AND" + " t_thread_pattern = '" + thread_pattern + "' ";
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }
                        }
                        else
                        {
                            if (chk_make.Checked == true)
                            {
                                qq = " t_make = '" + make + "' ";
                            }
                        }
                    }
                    else
                    {
                        if (chk_t_pattern.Checked == true)
                        {
                            qq = qq + "AND " + " t_thread_pattern = '" + thread_pattern + "' ";
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }
                        }
                        else
                        {
                            if (chk_make.Checked == true)
                            {
                                qq = qq + " AND " + " t_make = '" + make + "' ";
                            }
                        }
                    }
                }
            }




            else
            {

                if (chk_size.Checked == true)
                {
                    qq = " t_size = '" + size + "' ";
                    if (chk_ply_rate.Checked == true)
                    {
                        qq = qq + "AND" + " t_ply_rate = '" + ply_rate + "' ";
                        if (chk_t_pattern.Checked == true)
                        {
                            qq = qq + "AND" + " t_thread_pattern = '" + thread_pattern + "' ";
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }
                        }
                        else
                        {
                            if (chk_make.Checked == true)
                            {
                                qq = " t_make = '" + make + "' ";
                            }
                        }
                    }
                    else
                    {
                        if (chk_t_pattern.Checked == true)
                        {
                            qq = " t_thread_pattern = '" + thread_pattern + "' ";
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }
                        }
                        else
                        {
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" +" t_make = '" + make + "' ";
                            }
                        }
                    }
                }





                else
                {
                    if (chk_ply_rate.Checked == true)
                    {
                        qq = " t_ply_rate = '" + ply_rate + "' ";
                        if (chk_t_pattern.Checked == true)
                        {
                            qq = qq + "AND" + " t_thread_pattern = '" + thread_pattern + "' ";
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }

                        }
                        else
                        {
                            if (chk_make.Checked == true)
                            {
                                qq = " t_make = '" + make + "' ";
                            }
                        }

                    }
                    else
                    {
                        if (chk_t_pattern.Checked == true)
                        {
                            qq = " t_thread_pattern = '" + thread_pattern + "' ";
                            if (chk_make.Checked == true)
                            {
                                qq = qq + "AND" + " t_make = '" + make + "' ";
                            }

                        }
                        else
                        {
                            if (chk_make.Checked == true)
                            {
                                qq = " t_make = '" + make + "' ";
                            }
                        }
                    }
                }
            }

            return qq;
        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {

                string brand = radGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string size = radGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string ply_rate = radGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string thread_pattern = radGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string make = radGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                string type = radGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                string tube = radGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

                vehical_category_data.brand = brand;
                vehical_category_data.size = size;
                vehical_category_data.ply_rate = ply_rate;
                vehical_category_data.thread_pattern = thread_pattern;
                vehical_category_data.make = make;
                vehical_category_data.type = type;
                vehical_category_data.tube = tube;

                this.Enabled = false;
                qty q = new qty();
                q.MdiParent = DHNAULA.ActiveForm;
                q.Visible = true;



            }



        }

        private void chk_cycle_search_Click(object sender, EventArgs e)
        {
            if (chk_cycle_search.Checked != true)
            {
                panal_cycle.Enabled = true;
                grp_tyre_type.Enabled = false;
                label2.Text = "Motor Cycle";
                radGridView2.Visible = true;
                radGridView1.Visible = false;

                String q = "SELECT SUM(qty) FROM add_cycle_tyre";
                DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
                DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
                int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());

                lbl_tot.Text = total_qty.ToString();
                lbl_sh_qty.Text = "0"; 
            }
            else
            {                
                panal_cycle.Enabled = false;
                grp_tyre_type.Enabled = true;
                label2.Text = "Vehical";
                radGridView1.Visible = true;
                radGridView2.Visible = false;

                String q = "SELECT SUM(qty) FROM add_vehical_tyre";
                DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
                DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
                int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());

                lbl_tot.Text = total_qty.ToString(); 
               
                

            }
        }

        public  void reset()
        {
            fill_advance_search_table();
            fill_advance_search_table_cycle();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (chk_cycle_search.Checked != true)
            {
               
                fill_advance_search_table();
                String q = "SELECT SUM(qty) FROM add_vehical_tyre";
                DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
                DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
                int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());

                lbl_tot.Text = total_qty.ToString();
                lbl_sh_qty.Text = "0";
                
            }
            else
            {
                fill_advance_search_table_cycle();
                String q = "SELECT SUM(qty) FROM add_cycle_tyre";
                DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
                DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
                int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());

                lbl_tot.Text = total_qty.ToString();
                lbl_sh_qty.Text = "0";

                
            }
        }

        private void radGridView2_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            int r_count = radGridView2.RowCount;
            if (val >= 0)
            {
                string brand = radGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                string size = radGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                string side = radGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                string ply_rate = radGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                string thread_pattern = radGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                string make = radGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                string tyre_pattern = radGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                string tube = radGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();

                cycle_category_data.brand = brand;
                cycle_category_data.size = size;
                cycle_category_data.ply_rate = ply_rate;
                cycle_category_data.thread_pattern = thread_pattern;
                cycle_category_data.make = make;
                cycle_category_data.tyre_pattern = tyre_pattern;
                cycle_category_data.tube = tube;
                cycle_category_data.side = side;



                this.Enabled = false;
                qty_cycle q = new qty_cycle();
                q.MdiParent = DHNAULA.ActiveForm;
                q.Visible = true;
            }
        }

        private void AdvancedSearch_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moveStart = new Point(e.X, e.Y);
            }
        }

        private void AdvancedSearch_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Point deltaPos = new Point(e.X - moveStart.X, e.Y - moveStart.Y);
                this.Location = new Point(this.Location.X + deltaPos.X,
                  this.Location.Y + deltaPos.Y);
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
            radGridView3.Visible = true;
        }


        public void filter_text_box_text_size(string text_text)
        {
            string q1 = "SELECT tyer_size FROM size WHERE tyer_size  LIKE  '" + text_text + "%' ";
            ds_size = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_size != null) // if data set is not null
            {
                radGridView3.Visible = false;
                radGridView3.Visible = true;
                radGridView3.DataSource = ds_size.Tables[0];


            }
            else
            {
                radGridView3.DataSource = null;
                radGridView3.Visible = false;
            }

        }

        private void radGridView3_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_size.Text = radGridView3.Rows[row_index].Cells[0].Value.ToString();
            txt_size.Focus();
            radGridView3.Visible = false;
        }

        private void radGridView3_SelectionChanged(object sender, EventArgs e)
        {
            row_index = radGridView3.CurrentRow.Index;
        }

        private void txt_size_TextChanged(object sender, EventArgs e)
        {
            string text_text = txt_size.Text;
            filter_text_box_text_size(text_text);
        }

        private void AdvancedSearch_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = false;
        }

        private void txt_size_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = true;
        }

        private void radPanel2_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = false;
        }

        private void chk_cycle_search_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chk_cycle_search.Checked == true)
            {
                identify_stok.check = "C";
            }
            else
            {
                identify_stok.check = "V";
            }
        }

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }

        private void AdvancedSearch_EnabledChanged(object sender, EventArgs e)
        {
            if (chk_cycle_search.Checked == true)
            {
                if(refresh_cycle == null)
                {
                    fill_advance_search_table_cycle();
                    
                }
                else
                {
                    string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side , t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_tyre_pattern AS Tyre_Type ,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price FROM add_cycle_tyre WHERE "+ refresh_cycle +"";
                    DataSet ds_add_cycle_tyre = middle_access.db_access.SelectData(q);
                    if (ds_add_cycle_tyre != null)
                    {
                        radGridView2.DataSource = ds_add_cycle_tyre.Tables[0].DefaultView;
                    }
                    else
                        radGridView2.DataSource = null;
                }
                set_searched_qty(refresh_cycle);
                set_total_qty_after_changed();
            }
            else
            {
                if (refresh_vehicle == null)
                {
                    fill_advance_search_table();
                }
                else
                {
                    string q = "SELECT t_brand AS Brand, t_size AS Size, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_type AS Type,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price FROM add_vehical_tyre WHERE "+ refresh_vehicle +"";
                    DataSet ds_add_vehical_tyre = middle_access.db_access.SelectData(q);
                    if (ds_add_vehical_tyre != null)
                    {
                        radGridView1.DataSource = ds_add_vehical_tyre.Tables[0].DefaultView;
                    }
                    else
                        radGridView1.DataSource = null;
                }

                set_searched_qty(refresh_vehicle);
                set_total_qty_after_changed();
            }


           
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            //report_vehical_history h = new report_vehical_history();
            //h.Show();
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            //report_cycle_history c = new report_cycle_history();
            //c.Show();
        }

       

        private void radButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true; 
        }

        private void radButton7_Click(object sender, EventArgs e)
        {
            panel_search.Visible = true;
            panel_settings.Visible = false;
            panel_vehicale_category.Visible = false;
            panel_cycle_category.Visible = false;
            lbl_tyre_section_header.Text = "Tyre Section (Search)";
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            panel_search.Visible = false;
            panel_settings.Visible = false;
            panel_vehicale_category.Visible = true;
            panel_cycle_category.Visible = false;
            lbl_tyre_section_header.Text = "Tyre Section (Add Vehical Category)";

            fill_add_tyre_table();
            fill_com_brand_vehicale();
            fill_size_vehicale();
            fill_com_ply_rate_vehicale();
            fill_com_make_vehicale();
            fill_com_thread_pattern_load();
            dtp_new_stock.Value = DateTime.Today.Date;
            radGrid_vehicale_tyre_size.Visible = false;
            reset_add_new_vehical();


        }

        private void fill_add_tyre_table()
        {
            string q = "SELECT t_brand AS Brand, t_size AS Size, t_make AS Make, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern, t_stok_date AS Date, t_type AS Type, t_tube AS Tube_type FROM add_vehical_tyre ";
            DataSet ds_add_vehical_tyre = middle_access.db_access.SelectData(q);
            if (ds_add_vehical_tyre != null)
            {
                radGrid_add_vehicale_tyre.DataSource = ds_add_vehical_tyre.Tables[0].DefaultView;
            }
            else
                radGrid_add_vehicale_tyre.DataSource = null;
        }

        private void fill_com_brand_vehicale()
        {
            string q1 = "SELECT * FROM brand B";
            DataSet ds_brand = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand != null) // if data set is not null
            {

                com_brand_vehicale.DataSource = ds_brand.Tables[0]; //fill table 
                com_brand_vehicale.DisplayMember = "brand_name";
                com_brand_vehicale.ValueMember = "brand_name";
                com_brand_vehicale.Text = "";

            }
            else
                com_brand_vehicale.DataSource = null; //fill table 
        }

        private void fill_size_vehicale()
        {
            string q1 = "SELECT tyer_size FROM size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                radGrid_vehicale_tyre_size.DataSource = ds_size.Tables[0]; //fill table 


            }
            else
                radGrid_vehicale_tyre_size.DataSource = null; //fill table 
        }

        private void fill_com_ply_rate_vehicale()
        {
            string q1 = "SELECT * FROM ply_rate ";
            DataSet ds_ply_rate = middle_access.db_access.SelectData(q1); // fill data set with rates which stoed in pl rate table 
            if (ds_ply_rate != null) // if data set is not null
            {

                com_ply_rate_vehicale.DataSource = ds_ply_rate.Tables[0]; //fill table 
                com_ply_rate_vehicale.DisplayMember = "rate";
                com_ply_rate_vehicale.ValueMember = "rate";
                com_ply_rate_vehicale.Text = "";

            }
            else
                com_ply_rate_vehicale.DataSource = null; //fill table 
        }

        private void fill_com_make_vehicale()
        {
            string q1 = "SELECT * FROM make ";
            DataSet ds_made = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_made != null) // if data set is not null
            {

                com_make_vehicale.DataSource = ds_made.Tables[0]; //fill table 
                com_make_vehicale.DisplayMember = "made_by";
                com_make_vehicale.ValueMember = "made_by";
                com_make_vehicale.Text = "";

            }
            else
                com_make_vehicale.DataSource = null; //fill table 
        }

        private void fill_com_thread_pattern_load()
        {
            string q1 = "SELECT * FROM thread_pattern";
            DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_thread_pattern != null) // if data set is not null
            {

                com_thread_pattern_vehicale.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                com_thread_pattern_vehicale.DisplayMember = "pattern_name";
                com_thread_pattern_vehicale.ValueMember = "pattern_name";
                com_thread_pattern_vehicale.Text = "";

            }
            else
                com_thread_pattern_vehicale.DataSource = null; //fill table 
        }

        private void radButton9_Click(object sender, EventArgs e)
        {
            panel_cycle_category.Visible = true;
            panel_search.Visible = false;
            panel_settings.Visible = false;
            panel_vehicale_category.Visible = false;
            lbl_tyre_section_header.Text = "Tyre Section (Add Cycle Category)";


            fill_add_cycle_tyre_table();
            fill_com_brand_cycle();
            fill_size_cycle();
            fill_com_ply_rate_cycle();
            fill_com_make_cycle();
            fill_com_thread_pattern_load_cycle();
            dtp_new_stock.Value = DateTime.Today.Date;
            radGrid_cycle_tyre_size.Visible = false;
            reset_add_new_cycle();




        }

        private void fill_com_thread_pattern_load_cycle()
        {
            string q1 = "SELECT * FROM thread_pattern";
            DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_thread_pattern != null) // if data set is not null
            {

                com_thread_pattern_cycle.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                com_thread_pattern_cycle.DisplayMember = "pattern_name";
                com_thread_pattern_cycle.ValueMember = "pattern_name";
                com_thread_pattern_cycle.Text = "";

            }
            else
                com_thread_pattern_cycle.DataSource = null; //fill table 
        }

        private void fill_com_make_cycle()
        {
            string q1 = "SELECT * FROM make ";
            DataSet ds_made = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_made != null) // if data set is not null
            {

                com_make_cycle.DataSource = ds_made.Tables[0]; //fill table 
                com_make_cycle.DisplayMember = "made_by";
                com_make_cycle.ValueMember = "made_by";
                com_make_cycle.Text = "";

            }
            else
                com_make_cycle.DataSource = null; //fill table 
        }

        private void fill_com_ply_rate_cycle()
        {
            string q1 = "SELECT * FROM ply_rate ";
            DataSet ds_ply_rate = middle_access.db_access.SelectData(q1); // fill data set with rates which stoed in pl rate table 
            if (ds_ply_rate != null) // if data set is not null
            {

                com_ply_rate_cycle.DataSource = ds_ply_rate.Tables[0]; //fill table 
                com_ply_rate_cycle.DisplayMember = "rate";
                com_ply_rate_cycle.ValueMember = "rate";
                com_ply_rate_cycle.Text = "";

            }
            else
                com_ply_rate_cycle.DataSource = null; //fill table 
        }

        private void fill_size_cycle()
        {
            string q1 = "SELECT S.tyer_size FROM size S";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                radGrid_cycle_tyre_size.DataSource = ds_size.Tables[0]; //fill table 
                txt_size.Text = "";

            }
            else
                radGrid_cycle_tyre_size.DataSource = null; //fill table 
        }

        private void fill_com_brand_cycle()
        {
            string q1 = "SELECT * FROM brand B";
            DataSet ds_brand = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand != null) // if data set is not null
            {

                com_brand_cycle.DataSource = ds_brand.Tables[0]; //fill table 
                com_brand_cycle.DisplayMember = "brand_name";
                com_brand_cycle.ValueMember = "brand_name";
                com_brand_cycle.Text = "";


            }
            else
                com_brand_cycle.DataSource = null; //fill table 
        }

        private void fill_add_cycle_tyre_table()
        {
            string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side, t_make AS Make, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern, t_stok_date AS Date,t_tyre_pattern AS Tyer_Pattern , t_tube AS Tube_type FROM add_cycle_tyre ";
            DataSet ds_add_vehical_tyre = middle_access.db_access.SelectData(q);
            if (ds_add_vehical_tyre != null)
            {
                radGrid_add_cycle_tyre.DataSource = ds_add_vehical_tyre.Tables[0].DefaultView;
            }
            else
                radGrid_add_cycle_tyre.DataSource = null;
        }

        private void radButton3_Click_1(object sender, EventArgs e)
        {
            panel_search.Visible = false;
            panel_settings.Visible = true;
            panel_vehicale_category.Visible = false;
            panel_cycle_category.Visible = false;

            lbl_tyre_section_header.Text = "Tyre Section (Settings)";
            radio_brand.Checked = true;
            com_brand_name.Visible = false;
            lbl_brand.Visible = false;
            lbl_type.Text = "Brand";
            fill_settings_table("brand_name", "brand", "Currently_Available_Brands");
            txt_type.Focus();
            
        }

        private void fill_settings_table(string column,string table,string header)
        {
            string q1 = "SELECT X."+column+" AS "+header+" FROM "+table+" X";
            DataSet ds_brand_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand_table != null) // if data set is not null
            {

                radGrid_setting_items.DataSource = ds_brand_table.Tables[0].DefaultView; //fill table 
            }

            else
                radGrid_setting_items.DataSource = null;
        }

        private void radio_brand_Click(object sender, EventArgs e)
        {
            com_brand_name.Visible = false;
            lbl_brand.Visible = false;
            lbl_type.Text = "Brand";
            fill_settings_table("brand_name", "brand", "Currently_Available_Brands");
            txt_type.Clear();
            txt_type.Focus();
        }

      

        private void radio_size_Click(object sender, EventArgs e)
        {
            com_brand_name.Visible = false;
            lbl_brand.Visible = false;
            lbl_type.Text = "Size";
            fill_settings_table("tyer_size", "size", "Currently_Available_Sizes");
            txt_type.Clear();
            txt_type.Focus();
        }

     

        private void radio_make_Click(object sender, EventArgs e)
        {
            com_brand_name.Visible = false;
            lbl_brand.Visible = false;
            lbl_type.Text = "Make";
            fill_settings_table("made_by", "make", "Manufactured");
            txt_type.Clear();
            txt_type.Focus();
        }

        private void radio_ply_rate_Click(object sender, EventArgs e)
        {
            com_brand_name.Visible = false;
            lbl_brand.Visible = false;
            lbl_type.Text = "Ply Rate";
            fill_settings_table("rate", "ply_rate", "Ply_Rates");
            txt_type.Clear();
            txt_type.Focus();
        }

        private void radio_thred_pattern_Click(object sender, EventArgs e)
        {
            com_brand_name.Visible = true;
            lbl_brand.Visible = true;
            lbl_type.Text = "Thred pattern";
            fill_thread_pattern();
            fill_drp_brand();
            com_brand_name.Text = "";
            txt_type.Clear();
            txt_type.Focus();
        }

        public void fill_thread_pattern()// This method is use to fill tyer size table.
        {
            string q1 = "SELECT TP.pattern_name AS Thread_Pattern, B.brand_name AS Brand FROM thread_pattern TP,brand B WHERE B.brand_id = TP.brand_id";
            DataSet ds_size_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_size_table != null) // if data set is not null
            {

                radGrid_setting_items.DataSource = ds_size_table.Tables[0].DefaultView; //fill table 
            }

            else
                radGrid_setting_items.DataSource = null;

        }

        public void fill_drp_brand()//fill brand dop down list in thread pattern tab 
        {
            string q1 = "SELECT * FROM brand B";
            DataSet ds_brand = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand != null) // if data set is not null
            {

                com_brand_name.DataSource = ds_brand.Tables[0]; //fill table 
                com_brand_name.DisplayMember = "brand_name";
                com_brand_name.ValueMember = "brand_id";
            }
            else
                com_brand_name.DataSource = null; //fill table 
        }

        private void radGrid_setting_items_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
                if (radio_thred_pattern.Checked != true)
                {
                    txt_type.Text = radGrid_setting_items.Rows[e.RowIndex].Cells[0].Value.ToString();
                    cell_type_name = txt_type.Text;
                    row_index_settings = e.RowIndex;
                }
                else
                {

                    txt_type.Text = radGrid_setting_items.Rows[e.RowIndex].Cells[0].Value.ToString();//get cell value to the cell_brand_name variable
                    com_brand_name.Text = radGrid_setting_items.Rows[e.RowIndex].Cells[1].Value.ToString();
                    thread_pattern_x = radGrid_setting_items.Rows[e.RowIndex].Cells[0].Value.ToString();//set globle variable
                    row_index = e.RowIndex;
                    string q1 = "SELECT B.brand_id FROM brand B  WHERE brand_name = '" + com_brand_name.Text + "'";
                    DataSet ds_brand_id = middle_access.db_access.SelectData(q1);
                    if (ds_brand_id != null)
                    {
                        DataRow row_brand_id = ds_brand_id.Tables[0].Rows[0];
                        brand_id_x = (int)row_brand_id.ItemArray.GetValue(0);//set globle variable
                    }

                }
            }
            else
            {

            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if(radio_brand.Checked == true)
            {
                insert_settings_data("brand", "brand_name");
                fill_settings_table("brand_name", "brand", "Currently_Available_Brands");
                set_current_row();
            }
            else if (radio_size.Checked == true)
            {
                insert_settings_data("size", "tyer_size");
                fill_settings_table("tyer_size", "size", "Currently_Available_Sizes");
                set_current_row();
            }
            else if (radio_thred_pattern.Checked == true)
            {
                insert_THRED_PATTERN_settings_data();
            }
            else if (radio_ply_rate.Checked == true)
            {
                insert_settings_data("ply_rate", "rate");
                fill_settings_table("rate", "ply_rate", "Ply_Rates");
                set_current_row();
            }
            else if (radio_make.Checked == true)
            {
                insert_settings_data("make", "made_by");
                fill_settings_table("made_by", "make", "Manufactured");
                set_current_row();
            }
            
        }

        private void set_current_row()
        {
            int row_count = radGrid_setting_items.RowCount;
            this.radGrid_setting_items.Rows[row_count - 1].IsCurrent = true;
        }

        private void insert_settings_data(string table, string column)
        {
            string type = txt_type.Text;
            if (txt_type.Text != "") // if brand name text box is not empty
            {
                string q1 = "SELECT * FROM "+table+" B WHERE B."+column+" = '" + type + "' ";
                DataSet ds_brand_table = middle_access.db_access.SelectData(q1);

                if (ds_brand_table == null)
                {
                    string q2 = "INSERT INTO "+table+"("+column+")  VALUES ('" + type + "')";
                    bool status = middle_access.db_access.InsertData(q2);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button                     
                        txt_type.Text = "";
                    }
                    else // if data is not insert
                    {

                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // if brand name text box is empty
                MessageBox.Show("Enter Brand Name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void insert_THRED_PATTERN_settings_data()
        {
            string thread_pattern = txt_type.Text;

            if (thread_pattern == "")
            {
                MessageBox.Show("Enter thread pattern!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button

            }
            else if (com_brand_name.Text == "")
            {
                MessageBox.Show("Select a brand first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button

            }
            else
            {
                string q1 = "SELECT * FROM thread_pattern TP,brand B  WHERE B.brand_name = '" + com_brand_name.Text + "' AND TP.pattern_name = '" + txt_type.Text + "' AND B.brand_id = TP.brand_id";
                DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1);
                if (ds_thread_pattern == null)
                {
                    string q2 = "INSERT INTO thread_pattern(pattern_name,brand_id)  VALUES('" + thread_pattern + "'," + brand_id + ")";
                    bool status = middle_access.db_access.InsertData(q2);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        txt_type.Focus();
                        fill_thread_pattern();
                      //  int row_count = radGrid_setting_items.RowCount;
                      //  this.radGrid_setting_items.Rows[row_count - 1].IsCurrent = true;
                        txt_type.Text = "";
                    }
                    else
                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button


            }
        }

        private void com_brand_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            string brand = com_brand_name.SelectedText.ToString();

            string q1 = "SELECT B.brand_id FROM brand B  WHERE brand_name = '" + brand + "'";
            DataSet ds_brand_id = middle_access.db_access.SelectData(q1);
            if (ds_brand_id != null)
            {
                DataRow row_brand_id = ds_brand_id.Tables[0].Rows[0];
                brand_id = (int)row_brand_id.ItemArray.GetValue(0);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (radio_brand.Checked == true)
            {
                delete_settings_data("brand", "brand_name");                
                fill_settings_table("brand_name", "brand", "Currently_Available_Brands");
            }
            else if (radio_size.Checked == true)
            {
                delete_settings_data("size", "tyer_size");
                fill_settings_table("tyer_size", "size", "Currently_Available_Sizes");
            }
            else if (radio_thred_pattern.Checked == true)
            {
                delete_THRED_PATTERN_settings_data();
            }
            else if (radio_ply_rate.Checked == true)
            {
                delete_settings_data("ply_rate", "rate");
                fill_settings_table("rate", "ply_rate", "Ply_Rates");
            }
            else if (radio_make.Checked == true)
            {
                delete_settings_data("make", "made_by");
                fill_settings_table("made_by", "make", "Manufactured");
            }
        }

        private void delete_settings_data(string table, string column)
        {
            if (txt_type.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM "+table+" B WHERE B."+column+" = '" + txt_type.Text + "' ";
                    DataSet ds_brand_table = middle_access.db_access.SelectData(q1);

                    if (ds_brand_table != null)
                    {
                        string q2 = "DELETE FROM "+table+" WHERE "+column+" = '" + txt_type.Text + "'  ";
                        bool status = middle_access.db_access.DeleteData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            txt_type.Text = "";
                        }

                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_type.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show(lbl_type.Text + " was not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_type.Text = "";
                    }
                }
                else
                {
                    txt_type.Text = "";
                }

            }
            else
                MessageBox.Show("Select a " + lbl_type.Text + " first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void delete_THRED_PATTERN_settings_data()
        {
            if (txt_type.Text == "")
            {
                MessageBox.Show("Select a Thread pattern!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (com_brand_name.Text == "")
            {
                MessageBox.Show("Select a brand first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q = "SELECT * FROM brand  WHERE brand_name = '" + com_brand_name.Text + "' ";
                    DataSet ds_brand_id = middle_access.db_access.SelectData(q);
                    if (ds_brand_id == null)
                    {
                        MessageBox.Show("Brand is not available!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        com_brand_name.Text = "";
                    }
                    else
                    {

                        DataRow row_brand_id = ds_brand_id.Tables[0].Rows[0];
                        string brand_id_selected = row_brand_id.ItemArray.GetValue(0).ToString();


                        string q1 = "SELECT * FROM thread_pattern TP WHERE TP.brand_id = '" + brand_id_selected + "' AND TP.pattern_name = '" + txt_type.Text + "' ";
                        DataSet ds_thread_pattern_table = middle_access.db_access.SelectData(q1);

                        if (ds_thread_pattern_table != null)
                        {
                            string q2 = "DELETE FROM thread_pattern  WHERE brand_id = '" + brand_id_selected + "' AND pattern_name = '" + txt_type.Text + "' ";
                            bool status = middle_access.db_access.DeleteData(q2);
                            if (status == true)
                            {
                                MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                fill_thread_pattern();
                                txt_type.Text = "";
                                com_brand_name.Text = "";
                            }

                            else
                            {
                                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txt_type.Text = "";
                                com_brand_name.Text = "";

                            }
                        }

                        else
                        {
                            MessageBox.Show("Thread pattern was not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txt_type.Text = "";
                            com_brand_name.Text = "";
                        }
                    }
                }


                else
                {
                    txt_type.Text = "";
                    com_brand_name.Text = "";
                }

            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (radio_brand.Checked == true)
            {
                update_settings_data("brand", "brand_name");
                fill_settings_table("brand_name", "brand", "Currently_Available_Brands");
                this.radGrid_setting_items.Rows[row_index].IsCurrent = true;
            }
            else if (radio_size.Checked == true)
            {
                update_settings_data("size", "tyer_size");
                fill_settings_table("tyer_size", "size", "Currently_Available_Sizes");
                this.radGrid_setting_items.Rows[row_index].IsCurrent = true;
            }
            else if (radio_thred_pattern.Checked == true)
            {
                update_THRED_PATTERN_settings_data();
            }
            else if (radio_ply_rate.Checked == true)
            {
                update_settings_data("ply_rate", "rate");
                fill_settings_table("rate", "ply_rate", "Ply_Rates");
                this.radGrid_setting_items.Rows[row_index].IsCurrent = true;
            }
            else if (radio_make.Checked == true)
            {
                update_settings_data("make", "made_by");
                fill_settings_table("made_by", "make", "Manufactured");
                this.radGrid_setting_items.Rows[row_index].IsCurrent = true;
            }
        }


        private void update_settings_data(string table, string column)
        {
            if (txt_type.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM "+table+" B WHERE B."+column+" = '" + changed_type + "' ";
                    DataSet ds_brand_table = middle_access.db_access.SelectData(q1);

                    if (ds_brand_table == null)
                    {
                        string q2 = "UPDATE "+table+" SET "+column+" ='" + txt_type.Text + "' WHERE "+column+" = '" + cell_type_name + "'  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                            txt_type.Text = "";

                        }

                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_type.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show(lbl_type.Text + " was not changed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_type.Text = "";
                    }
                }
                else
                {
                    txt_type.Text = "";
                }
            }
            else
                MessageBox.Show("Select a "+ lbl_type.Text +" first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void update_THRED_PATTERN_settings_data()
        {
            if (txt_type.Text == "")
            {
                MessageBox.Show("Enter Thread pattern!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (com_brand_name.Text == "")
            {
                MessageBox.Show("Select a brand!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string q = "SELECT * FROM brand  WHERE brand_name = '" + com_brand_name.Text + "' ";
                    DataSet ds_brand_id = middle_access.db_access.SelectData(q);
                    if (ds_brand_id == null)
                    {
                        MessageBox.Show("Brand is not available!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        com_brand_name.Text = "";
                    }
                    else
                    {

                        DataRow row_brand_id = ds_brand_id.Tables[0].Rows[0];
                        string brand_id_selected = row_brand_id.ItemArray.GetValue(0).ToString();


                        string q1 = "SELECT * FROM thread_pattern TP WHERE TP.brand_id = '" + brand_id_selected + "' AND TP.pattern_name = '" + txt_type.Text + "' ";
                        DataSet ds_brand_table = middle_access.db_access.SelectData(q1);

                        if (ds_brand_table == null)
                        {
                            string q2 = "UPDATE thread_pattern SET brand_id = " + brand_id_selected + ", pattern_name = '" + txt_type.Text + "' WHERE brand_id = " + brand_id_x + " AND pattern_name = '" + thread_pattern_x + "'  ";
                            bool status = middle_access.db_access.UpdateData(q2);
                            if (status == true)
                            {
                                MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                fill_thread_pattern();
                                this.radGrid_setting_items.Rows[row_index].IsCurrent = true;

                                txt_type.Text = "";
                                com_brand_name.Text = "";
                            }

                            else
                            {

                                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txt_type.Text = "";
                                com_brand_name.Text = "";

                            }
                        }

                        else
                        {
                            MessageBox.Show("Information was not change!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txt_type.Text = "";
                            com_brand_name.Text = "";
                        }
                    }
                }
                else
                {
                    txt_type.Text = "";
                    com_brand_name.Text = "";
                }
            }
        }

        private void txt_type_TextChanged(object sender, EventArgs e)
        {
            changed_type = txt_type.Text; 
        }

        private void radButton11_Click(object sender, EventArgs e)
        {
            reset_add_new_vehical();
        }

        /// <summary>
        /// Reset_add_new_vehicals this instance.
        /// </summary>
        private void reset_add_new_vehical()
        {
            dtp_stok_date.Value = DateTime.Today.Date;
            rad_canvas.IsChecked = false;
            rad_radial.IsChecked = false;
            rad_tube.IsChecked = false;
            rad_tube_less.IsChecked = false;
            com_brand_vehicale.Text = "";
            com_make_vehicale.Text = "";
            com_ply_rate_vehicale.Text = "";
            txt_size_vehicale.Text = "";
            com_thread_pattern_vehicale.Text = "";
        }

        private void radGrid_add_vehicale_tyre_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
                dtp_stok_date.Value = Convert.ToDateTime(radGrid_add_vehicale_tyre.Rows[e.RowIndex].Cells[5].Value);                
                com_brand_vehicale.Text = radGrid_add_vehicale_tyre.Rows[e.RowIndex].Cells[0].Value.ToString();
               
                // com_size.Text = radGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_size_vehicale.Text = radGrid_add_vehicale_tyre.Rows[e.RowIndex].Cells[1].Value.ToString();
                radGrid_vehicale_tyre_size.DataSource = null;
                radGrid_vehicale_tyre_size.Visible = false;

                com_make_vehicale.Text = radGrid_add_vehicale_tyre.Rows[e.RowIndex].Cells[2].Value.ToString();
                com_ply_rate_vehicale.Text = radGrid_add_vehicale_tyre.Rows[e.RowIndex].Cells[3].Value.ToString();
                com_thread_pattern_vehicale.Text = radGrid_add_vehicale_tyre.Rows[e.RowIndex].Cells[4].Value.ToString();
                string tp = com_thread_pattern_vehicale.Text;
                string type = radGrid_add_vehicale_tyre.Rows[e.RowIndex].Cells[6].Value.ToString();
                string tube = radGrid_add_vehicale_tyre.Rows[e.RowIndex].Cells[7].Value.ToString();

                vehical_category_data.brand = com_brand_vehicale.Text;
                vehical_category_data.size = txt_size_vehicale.Text;
                vehical_category_data.ply_rate = com_ply_rate_vehicale.Text;
                vehical_category_data.thread_pattern = com_thread_pattern_vehicale.Text;
                vehical_category_data.make = com_make_vehicale.Text;
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
                string brand_name = com_brand_vehicale.Text;
                string q1 = "SELECT TP.pattern_name FROM thread_pattern TP,brand B WHERE TP.brand_id = B.brand_id AND B.brand_name = '" + brand_name + "' ";
                DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with patterns which stoed in thread pattern table 
                if (ds_thread_pattern != null) // if data set is not null
                {

                    com_thread_pattern_vehicale.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                    com_thread_pattern_vehicale.DisplayMember = "pattern_name";
                    com_thread_pattern_vehicale.ValueMember = "pattern_name";
                    com_thread_pattern_vehicale.Text = tp;

                }
                else
                    com_thread_pattern_vehicale.DataSource = null; //fill table 
            }

        }

        /// <summary>
        /// Set_add_vehicel_tyre_cell_clicks this instance.
        /// </summary>
        public void set_add_vehicel_tyre_cell_click()
        {
            set_tyep_tube_val();
            string q1 = "SELECT * FROM add_vehical_tyre A WHERE A.t_brand = '" + com_brand_vehicale.Text + "' AND A.t_size = '" + txt_size_vehicale.Text + "' AND A.t_ply_rate = '" + com_ply_rate_vehicale.Text + "' AND t_make = '" + com_make_vehicale.Text + "' AND t_thread_pattern = '" + com_thread_pattern_vehicale.Text + "' AND t_type = '" + radio_val_type + "' AND t_tube = '" + radio_val_tube + "'";
            ds_add_vehicel_tyre_cell_click = middle_access.db_access.SelectData(q1);
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

        private void panel_vehicale_category_Click(object sender, EventArgs e)
        {
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void dtp_stok_date_Click(object sender, EventArgs e)
        {
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void rad_radial_Click(object sender, EventArgs e)
        {
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void rad_canvas_Click(object sender, EventArgs e)
        {
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void rad_tube_Click(object sender, EventArgs e)
        {
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void rad_tube_less_Click(object sender, EventArgs e)
        {
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void radGrid_add_vehicale_tyre_Click(object sender, EventArgs e)
        {
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void com_brand_vehicale_Click(object sender, EventArgs e)
        {
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void txt_size_vehicale_TextChanged(object sender, EventArgs e)
        {
            if (txt_size_vehicale.Text == "")
            {
                radGrid_vehicale_tyre_size.DataSource = null;
                radGrid_vehicale_tyre_size.Refresh();
                radGrid_vehicale_tyre_size.Visible = false;


            }
            else
            {

                string a = txt_size_vehicale.Text; ;
                filter_text_box_text_vehicale_tyre_size(a);
                if (ds_size != null)
                {
                    radGrid_vehicale_tyre_size.Visible = true;
                    radGrid_vehicale_tyre_size.Refresh();
                    // radGridView2.Focus();
                    //this.radGridView2.Rows[1].IsCurrent = true;
                    //txt_size.Focus();
                }
            }
        }

        private void filter_text_box_text_vehicale_tyre_size(string a)
        {
            string q1 = "SELECT tyer_size FROM size WHERE tyer_size  LIKE  '" + a + "%' ";
            ds_size = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_size != null) // if data set is not null
            {
                radGrid_vehicale_tyre_size.Visible = false;
                radGrid_vehicale_tyre_size.Visible = true;
                radGrid_vehicale_tyre_size.DataSource = ds_size.Tables[0];


            }
            else
            {
                radGrid_vehicale_tyre_size.DataSource = null;
                radGrid_vehicale_tyre_size.Visible = false;
            }
        }

        private void radGrid_vehicale_tyre_size_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_size_vehicale.Text = radGrid_vehicale_tyre_size.Rows[row_index].Cells[0].Value.ToString();
            txt_size_vehicale.Focus();
            radGrid_vehicale_tyre_size.Visible = false;
        }

        private void com_brand_vehicale_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_vehicale_com_thread_pattern();
        }

        /// <summary>
        /// Fill_vehicale_com_thread_patterns this instance.
        /// </summary>
        private void fill_vehicale_com_thread_pattern()
        {
            string brand_name = com_brand_vehicale.Text;
            string q1 = "SELECT TP.pattern_name FROM thread_pattern TP,brand B WHERE TP.brand_id = B.brand_id AND B.brand_name = '" + brand_name + "' ";
            DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with patterns which stoed in thread pattern table 
            if (ds_thread_pattern != null) // if data set is not null
            {

                com_thread_pattern_vehicale.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                com_thread_pattern_vehicale.DisplayMember = "pattern_name";
                com_thread_pattern_vehicale.ValueMember = "pattern_name";
             //  com_thread_pattern_vehicale.Text = "";

            }
            else
                com_thread_pattern_vehicale.DataSource = null; //fill table 
        }

        private void com_brand_cycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_com_thread_pattern_cycle();
        }

        /// <summary>
        /// Fill_com_thread_pattern_cycles this instance.
        /// </summary>
        public void fill_com_thread_pattern_cycle()
        {
            string brand_name = com_brand_cycle.Text;
            string q1 = "SELECT TP.pattern_name FROM thread_pattern TP,brand B WHERE TP.brand_id = B.brand_id AND B.brand_name = '" + brand_name + "' ";
            DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with patterns which stoed in thread pattern table 
            if (ds_thread_pattern != null) // if data set is not null
            {

                com_thread_pattern_cycle.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                com_thread_pattern_cycle.DisplayMember = "pattern_name";
                com_thread_pattern_cycle.ValueMember = "pattern_name";
                com_thread_pattern_cycle.Text = "";

            }
            else
                com_thread_pattern_cycle.DataSource = null; //fill table 
        }

        private void radGrid_cycle_tyre_size_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_size_cycle.Text = radGrid_cycle_tyre_size.Rows[row_index].Cells[0].Value.ToString();
            txt_size_cycle.Focus();
            radGrid_cycle_tyre_size.Visible = false;
        }

        private void txt_size_cycle_TextChanged(object sender, EventArgs e)
        {
            if (txt_size_cycle.Text == "")
            {
                radGrid_cycle_tyre_size.DataSource = null;
                radGrid_cycle_tyre_size.Refresh();
                radGrid_cycle_tyre_size.Visible = false;


            }
            else
            {

                string a = txt_size_cycle.Text; ;
                filter_text_box_text_size_cycle(a);
                if (ds_size != null)
                {
                    radGrid_cycle_tyre_size.Visible = true;
                    radGrid_cycle_tyre_size.Refresh();
                    // radGridView2.Focus();
                    //this.radGridView2.Rows[1].IsCurrent = true;
                    //txt_size.Focus();
                }
            }
        }

        private void filter_text_box_text_size_cycle(string a)
        {
            string q1 = "SELECT tyer_size FROM size WHERE tyer_size  LIKE  '" + a + "%' ";
            ds_size = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_size != null) // if data set is not null
            {
                radGrid_cycle_tyre_size.Visible = false;
                radGrid_cycle_tyre_size.Visible = true;
                radGrid_cycle_tyre_size.DataSource = ds_size.Tables[0];


            }
            else
            {
                radGrid_cycle_tyre_size.DataSource = null;
                radGrid_cycle_tyre_size.Visible = false;
            }
        }

        private void radGrid_add_cycle_tyre_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            val = e.RowIndex;
            if (val >= 0)
            {
                dtp_new_stock.Value = Convert.ToDateTime(radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[6].Value);
                com_brand_cycle.Text = radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_size_cycle.Text = radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[1].Value.ToString();              
                radGrid_cycle_tyre_size.DataSource = null;
                radGrid_cycle_tyre_size.Visible = false;
                com_make_cycle.Text = radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[3].Value.ToString();
                com_ply_rate_cycle.Text = radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[4].Value.ToString();
                com_thread_pattern_cycle.Text = radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[5].Value.ToString();
                string tp = com_thread_pattern_cycle.Text;

                string tyre_pattern = radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[7].Value.ToString();
                string tube = radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[8].Value.ToString();
                string side = radGrid_add_cycle_tyre.Rows[e.RowIndex].Cells[2].Value.ToString();


                cycle_category_data.brand = com_brand_cycle.Text;
                cycle_category_data.size = txt_size_cycle.Text;
                cycle_category_data.ply_rate = com_ply_rate_cycle.Text;
                cycle_category_data.thread_pattern = com_thread_pattern_cycle.Text;
                cycle_category_data.make = com_make_cycle.Text;
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
                            ck_front.IsChecked = true;
                            ck_rear.IsChecked = true;
                            set_add_cycle_tyre_cell_click();

                        }
                        else if (side == "Front")
                        {
                            ck_front.IsChecked = true;
                            ck_rear.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                        else
                        {
                            ck_rear.IsChecked = true;
                            ck_front.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                    }
                    else
                    {
                        radio_tubeless.IsChecked = true;
                        if (side == "Front/Rear")
                        {
                            ck_front.IsChecked = true;
                            ck_rear.IsChecked = true;
                            set_add_cycle_tyre_cell_click();

                        }
                        else if (side == "Front")
                        {
                            ck_front.IsChecked = true;
                            ck_rear.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                        else
                        {
                            ck_rear.IsChecked = true;
                            ck_front.IsChecked = false;
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
                            ck_front.IsChecked = true;
                            ck_rear.IsChecked = true;
                            set_add_cycle_tyre_cell_click();

                        }
                        else if (side == "Front")
                        {
                            ck_front.IsChecked = true;
                            ck_rear.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                        else
                        {
                            ck_rear.IsChecked = true;
                            ck_front.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                    }
                    else
                    {
                        radio_tubeless.IsChecked = true;
                        if (side == "Front/Rear")
                        {
                            ck_front.IsChecked = true;
                            ck_rear.IsChecked = true;
                            set_add_cycle_tyre_cell_click();

                        }
                        else if (side == "Front")
                        {
                            ck_front.IsChecked = true;
                            ck_rear.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                        else
                        {
                            ck_rear.IsChecked = true;
                            ck_front.IsChecked = false;
                            set_add_cycle_tyre_cell_click();
                        }
                    }

                }
                row_count_update = e.RowIndex;
                string brand_name = com_brand_cycle.Text;
                string q1 = "SELECT TP.pattern_name FROM thread_pattern TP,brand B WHERE TP.brand_id = B.brand_id AND B.brand_name = '" + brand_name + "' ";
                DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1); // fill data set with patterns which stoed in thread pattern table 
                if (ds_thread_pattern != null) // if data set is not null
                {

                    com_thread_pattern_cycle.DataSource = ds_thread_pattern.Tables[0]; //fill table 
                    com_thread_pattern_cycle.DisplayMember = "pattern_name";
                    com_thread_pattern_cycle.ValueMember = "pattern_name";
                    com_thread_pattern_cycle.Text = tp;

                }
                else
                    com_thread_pattern_cycle.DataSource = null; //fill table 
            }
        }

        private void set_add_cycle_tyre_cell_click()
        {
            set_tyep_tube_val_cycle();
            string q = "SELECT * FROM add_cycle_tyre A WHERE A.t_brand = '" + com_brand_cycle.Text + "' AND A.t_size = '" + txt_size_cycle.Text + "' AND A.t_ply_rate = '" + com_ply_rate_cycle.Text + "' AND t_make = '" + com_make_cycle.Text + "' AND t_thread_pattern = '" + com_thread_pattern_cycle.Text + "' AND t_side = '" + check_val_side + "' AND t_tube = '" + radio_val_tube + "' AND t_tyre_pattern = '" + radio_val_tyre_type + "'";
            ds_set_add_cycle_tyre_cell_click = middle_access.db_access.SelectData(q);
        }

        /// <summary>
        /// Set_tyep_tube_val_cycles this instance.
        /// </summary>
        public void set_tyep_tube_val_cycle()
        {
            if (radio_trail.IsChecked == true)
            {
                radio_val_tyre_type = "Trail";
                if (radio_tube.IsChecked == true)
                {
                    radio_val_tube = "Tube";
                    if (ck_front.IsChecked == true && ck_rear.IsChecked == true)
                    {
                        //radio_val_side = "Front";
                        check_val_side = "Front/Rear";
                    }
                    else if (ck_front.IsChecked == true)
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
                    if (ck_front.IsChecked == true && ck_rear.IsChecked == true)
                    {
                        //radio_val_side = "Front";
                        check_val_side = "Front/Rear";
                    }
                    else if (ck_front.IsChecked == true)
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
                    if (ck_front.IsChecked == true && ck_rear.IsChecked == true)
                    {
                        //radio_val_side = "Front";
                        check_val_side = "Front/Rear";
                    }
                    else if (ck_front.IsChecked == true)
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
                    if (ck_front.IsChecked == true && ck_rear.IsChecked == true)
                    {
                        //radio_val_side = "Front";
                        check_val_side = "Front/Rear";
                    }
                    else if (ck_front.IsChecked == true)
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

        private void radButton14_Click(object sender, EventArgs e)
        {
            reset_add_new_cycle();
        }

        private void reset_add_new_cycle()
        {
            dtp_new_stock.Value = DateTime.Today.Date;
            ck_front.IsChecked = false;
            ck_rear.IsChecked = false;
            radio_trail.IsChecked = false;
            radio_non_trail.IsChecked = false;
            radio_tube.IsChecked = false;
            radio_tubeless.IsChecked = false;

            com_brand_cycle.Text = "";
            com_make_cycle.Text = "";
            com_ply_rate_cycle.Text = "";
            txt_size_cycle.Text = "";
            com_thread_pattern_cycle.Text = "";
        }

        private void radButton4_Click_1(object sender, EventArgs e)
        {
            insert_cycle_data();
        }

        private void insert_cycle_data()
        {
            if (((ck_front.IsChecked == true) || (ck_rear.IsChecked == true)) && ((radio_trail.IsChecked == true) || (radio_non_trail.IsChecked == true)) && ((radio_tube.IsChecked == true) || (radio_tubeless.IsChecked == true)) && com_brand_cycle.Text != "" && com_make_cycle.Text != "" && com_ply_rate_cycle.Text != "" && txt_size_cycle.Text != "" && com_thread_pattern_cycle.Text != "")
            {
                set_tyep_tube_val_cycle();
                string q = "SELECT * FROM add_cycle_tyre A WHERE A.t_brand = '" + com_brand_cycle.Text + "' AND A.t_size = '" + txt_size_cycle.Text + "' AND A.t_ply_rate = '" + com_ply_rate_cycle.Text + "' AND t_make = '" + com_make_cycle.Text + "' AND t_thread_pattern = '" + com_thread_pattern_cycle.Text + "' AND t_side = '" + check_val_side + "' AND t_tube = '" + radio_val_tube + "' AND t_tyre_pattern = '" + radio_val_tyre_type + "'";
                DataSet ds_add_vahical_tyre = middle_access.db_access.SelectData(q);

                if (ds_add_vahical_tyre == null)
                {
                    string date = dtp_new_stock.Value.Date.ToString("yyyy-MM-dd");
                    string q1 = "INSERT INTO add_cycle_tyre(t_stok_date,t_tyre_pattern,t_tube,t_side,t_brand,t_size,t_ply_rate,t_make,t_thread_pattern)  VALUES('" + date + "','" + radio_val_tyre_type + "', '" + radio_val_tube + "','" + check_val_side + "','" + com_brand_cycle.Text + "', '" + txt_size_cycle.Text + "', '" + com_ply_rate_cycle.Text + "','" + com_make_cycle.Text + "','" + com_thread_pattern_cycle.Text + "')";
                    bool status = middle_access.db_access.InsertData(q1);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        fill_add_cycle_tyre_table();// fill brand name table.
                        radGrid_add_cycle_tyre.Focus();
                        int row_count = radGrid_add_cycle_tyre.RowCount;
                        this.radGrid_add_cycle_tyre.Rows[row_count - 1].IsCurrent = true;

                        dtp_new_stock.Value = DateTime.Today.Date;
                        com_ply_rate_cycle.Text = "";
                        com_thread_pattern_cycle.Text = "";
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
                    reset_add_new_cycle();
                }


            }
            else
                MessageBox.Show("Enter detalils correctly!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radButton15_Click(object sender, EventArgs e)
        {
            update_cycle_data();
        }

        private void update_cycle_data()
        {
            if (((ck_front.IsChecked == true) || (ck_rear.IsChecked == true)) && ((radio_trail.IsChecked == true) || (radio_non_trail.IsChecked == true)) && ((radio_tube.IsChecked == true) || (radio_tubeless.IsChecked == true)) && com_brand_cycle.Text != "" && com_make_cycle.Text != "" && com_ply_rate_cycle.Text != "" && txt_size_cycle.Text != "" && com_thread_pattern_cycle.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    set_tyep_tube_val_cycle();
                    string q = "SELECT * FROM add_cycle_tyre A WHERE A.t_brand = '" + com_brand_cycle.Text + "' AND A.t_size = '" + txt_size_cycle.Text + "' AND A.t_ply_rate = '" + com_ply_rate_cycle.Text + "' AND t_make = '" + com_make_cycle.Text + "' AND t_thread_pattern = '" + com_thread_pattern_cycle.Text + "' AND t_side = '" + check_val_side + "' AND t_tube = '" + radio_val_tube + "' AND t_tyre_pattern = '" + radio_val_tyre_type + "'";
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

                        set_tyep_tube_val_cycle();
                        string date = dtp_new_stock.Value.Date.ToString("yyyy-MM-dd");

                        string q2 = "UPDATE add_cycle_tyre SET t_stok_date = '" + date + "',t_tyre_pattern = '" + radio_val_tyre_type + "',t_tube = '" + radio_val_tube + "',t_side = '" + check_val_side + "',t_brand = '" + com_brand_cycle.Text + "',t_size = '" + txt_size_cycle.Text + "',t_ply_rate = '" + com_ply_rate_cycle.Text + "',t_make = '" + com_make_cycle.Text + "',t_thread_pattern = '" + com_thread_pattern_cycle.Text + "' WHERE t_stok_id = " + category_id + " ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            int row_index = row_count_update;
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_add_cycle_tyre_table();
                            radGrid_add_cycle_tyre.Focus();
                            this.radGrid_add_cycle_tyre.Rows[row_index].IsCurrent = true;

                            reset_add_new_cycle();


                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reset_add_new_cycle();

                        }
                    }

                    else
                    {
                        MessageBox.Show("Alrady exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset_add_new_cycle();
                    }
                }
                else
                {
                    reset_add_new_cycle();
                }
            }
            else
                MessageBox.Show("Select a record first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radButton13_Click(object sender, EventArgs e)
        {
            delete_cycle_data();
        }

        private void delete_cycle_data()
        {
            if (((ck_front.IsChecked == true) || (ck_rear.IsChecked == true)) && ((radio_trail.IsChecked == true) || (radio_non_trail.IsChecked == true)) && ((radio_tube.IsChecked == true) || (radio_tubeless.IsChecked == true)) && com_brand_cycle.Text != "" && com_make_cycle.Text != "" && com_ply_rate_cycle.Text != "" && txt_size_cycle.Text != "" && com_thread_pattern_cycle.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    set_tyep_tube_val_cycle();


                    string q2 = "DELETE FROM add_cycle_tyre  WHERE t_stok_id=" + category_id + "";
                    bool status = middle_access.db_access.DeleteData(q2);
                    if (status == true)
                    {
                        string q3 = "DELETE FROM qty_cycle  WHERE category_id = " + category_id + " ";
                        middle_access.db_access.DeleteData(q3);

                        MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fill_add_cycle_tyre_table();
                        radGrid_add_cycle_tyre.Focus();
                        reset_add_new_cycle();

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

        private void radButton12_Click(object sender, EventArgs e)
        {
            insert_vehicale_data();
        }

        private void insert_vehicale_data()
        {
            if (((rad_canvas.IsChecked == true) || (rad_radial.IsChecked == true)) && ((rad_tube.IsChecked == true) || (rad_tube_less.IsChecked == true)) && com_brand_vehicale.Text != "" && com_make_vehicale.Text != "" && com_ply_rate_vehicale.Text != "" && txt_size_vehicale.Text != "" && com_thread_pattern_vehicale.Text != "")
            {
                set_tyep_tube_val();
                string q = "SELECT * FROM add_vehical_tyre A WHERE A.t_brand = '" + com_brand_vehicale.Text + "' AND A.t_size = '" + txt_size_vehicale.Text + "' AND A.t_ply_rate = '" + com_ply_rate_vehicale.Text + "' AND t_make = '" + com_make_vehicale.Text + "' AND t_thread_pattern = '" + com_thread_pattern_vehicale.Text + "' AND t_type = '" + radio_val_type + "' AND t_tube = '" + radio_val_tube + "'";
                DataSet ds_add_vahical_tyre = middle_access.db_access.SelectData(q);

                if (ds_add_vahical_tyre == null)
                {
                    string date = dtp_stok_date.Value.Date.ToString("yyyy-MM-dd");
                    string q1 = "INSERT INTO add_vehical_tyre(t_stok_date,t_type,t_tube,t_brand,t_size,t_ply_rate,t_make,t_thread_pattern)  VALUES('" + date + "','" + radio_val_type + "', '" + radio_val_tube + "','" + com_brand_vehicale.Text + "', '" + txt_size_vehicale.Text + "', '" + com_ply_rate_vehicale.Text + "','" + com_make_vehicale.Text + "','" + com_thread_pattern_vehicale.Text + "')";
                    bool status = middle_access.db_access.InsertData(q1);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        fill_add_tyre_table();// fill brand name table.
                        radGrid_add_vehicale_tyre.Focus();
                        int row_count = radGrid_add_vehicale_tyre.RowCount;
                        this.radGrid_add_vehicale_tyre.Rows[row_count - 1].IsCurrent = true;
                        

                        dtp_stok_date.Value = DateTime.Today.Date;
                        com_ply_rate_vehicale.Text = "";
                        com_thread_pattern_vehicale.Text = "";
                    }
                    else // if data is not insert
                    {

                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Pattern is already exiat!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    reset_add_new_vehical();
                }


            }
            else
                MessageBox.Show("Enter detalils correctly!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radButton5_Click_1(object sender, EventArgs e)
        {
            update_vehicale_data();
        }

        private void update_vehicale_data()
        {
            if (((rad_canvas.IsChecked == true) || (rad_radial.IsChecked == true)) && ((rad_tube.IsChecked == true) || (rad_tube_less.IsChecked == true)) && com_brand_vehicale.Text != "" && com_make_vehicale.Text != "" && com_ply_rate_vehicale.Text != "" && txt_size_vehicale.Text != "" && com_thread_pattern_vehicale.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    set_tyep_tube_val();
                    string q1 = "SELECT * FROM add_vehical_tyre A WHERE A.t_brand = '" + com_brand_vehicale.Text + "' AND A.t_size = '" + txt_size_vehicale.Text + "' AND A.t_ply_rate = '" + com_ply_rate_vehicale.Text + "' AND t_make = '" + com_make_vehicale.Text + "' AND t_thread_pattern = '" + com_thread_pattern_vehicale.Text + "' AND t_type = '" + radio_val_type + "' AND t_tube = '" + radio_val_tube + "' ";
                    DataSet ds_add_vehicel_tyre = middle_access.db_access.SelectData(q1);

                    if (ds_add_vehicel_tyre == null)
                    {
                        DataRow row_add_vehical_tyre = ds_add_vehicel_tyre_cell_click.Tables[0].Rows[0];

                        string type = row_add_vehical_tyre.ItemArray.GetValue(2).ToString();
                        string tube = row_add_vehical_tyre.ItemArray.GetValue(3).ToString();
                        string brand = row_add_vehical_tyre.ItemArray.GetValue(4).ToString();
                        string size = row_add_vehical_tyre.ItemArray.GetValue(5).ToString();
                        string ply_rate = row_add_vehical_tyre.ItemArray.GetValue(6).ToString();
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
                        string q2 = "UPDATE add_vehical_tyre SET t_stok_date = '" + date + "',t_type = '" + radio_val_type + "',t_tube = '" + radio_val_tube + "',t_brand = '" + com_brand_vehicale.Text + "',t_size = '" + txt_size_vehicale.Text + "',t_ply_rate = '" + com_ply_rate_vehicale.Text + "',t_make = '" + com_make_vehicale.Text + "',t_thread_pattern = '" + com_thread_pattern_vehicale.Text + "' WHERE t_type = '" + type + "' AND t_tube = '" + tube + "' AND t_brand = '" + brand + "' AND t_size = '" + size + "' AND t_ply_rate = " + ply_rate + " AND t_make = '" + make + "' AND t_thread_pattern = '" + thread_pttern + "'  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            int row_index = row_count_update;
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_add_tyre_table();
                            radGrid_add_vehicale_tyre.Focus();
                            this.radGrid_add_vehicale_tyre.Rows[row_index].IsCurrent = true;

                            reset_add_new_vehical();

                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            reset_add_new_vehical();

                        }
                    }

                    else
                    {
                        MessageBox.Show("Already exist!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset_add_new_vehical();
                    }
                }
                else
                {
                    reset_add_new_vehical();
                }
            }
            else
                MessageBox.Show("Select a record first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radButton10_Click_1(object sender, EventArgs e)
        {
            delete_vehicale_data();
 
        }

        private void delete_vehicale_data()
        {
            if (((rad_canvas.IsChecked == true) || (rad_radial.IsChecked == true)) && ((rad_tube.IsChecked == true) || (rad_tube_less.IsChecked == true)) && com_brand_vehicale.Text != "" && com_make_vehicale.Text != "" && com_ply_rate_vehicale.Text != "" && txt_size_vehicale.Text != "" && com_thread_pattern_vehicale.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    set_tyep_tube_val();
                    int catagory_id = vehical_category_data.get_catagory_id();

                    string q2 = "DELETE FROM add_vehical_tyre  WHERE t_brand = '" + com_brand_vehicale.Text + "' AND t_size = '" + txt_size_vehicale.Text + "' AND t_ply_rate = '" + com_ply_rate_vehicale.Text + "' AND t_make = '" + com_make_vehicale.Text + "' AND t_thread_pattern = '" + com_thread_pattern_vehicale.Text + "' AND t_type = '" + radio_val_type + "' AND t_tube = '" + radio_val_tube + "'";
                    bool status = middle_access.db_access.DeleteData(q2);
                    if (status == true)
                    {
                        string q3 = "DELETE FROM qty  WHERE category_id = " + catagory_id + " ";
                        middle_access.db_access.DeleteData(q3);

                        MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fill_add_tyre_table();
                        radGrid_add_vehicale_tyre.Focus();
                        reset_add_new_vehical();

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

     
    }
}
    
                    

