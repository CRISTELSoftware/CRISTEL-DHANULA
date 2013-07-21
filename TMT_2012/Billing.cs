using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq.Expressions;


namespace TMT_2012
{
    public partial class Billing : Telerik.WinControls.UI.RadForm
    {


        public static string brand;
        string type;
        Point moveStart;
        DataSet ds_size;
        int row_index;
        int i = 0;
        int r;
        string refresh_vehicle = null;
        string refresh_cycle = null;
        private string p;
        string refresh_battery = null;
        string invoiceNumber;
        string refresh_tube = null;
        public Billing()
        {
            InitializeComponent();
        }

        public Billing(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        //*************** FORM CLOSE BUTTON**************************
        private void radButton10_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms[""];
            f.Enabled = true;  
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void AdvancedSearch_Load(object sender, EventArgs e)
        {
             loadBillNo();
            
            fill_advance_search_table();
            fill_advance_search_table_cycle();
            fill_com_brand();
            fill_size();
            fill_com_ply_rate();          
            fill_com_make();           
            radGridView2.Visible = false;          
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
            lblBilling.Text = "Billing (Tyre Search)";
            fill_grdOther_table();
            loadBatery();



        }







        private void loadBillNo()
        {

            string q1 = "SELECT maxno FROM autoincrem where tablename='I'";
            DataSet ds_InviceNo = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table           

            if (ds_InviceNo != null) // if data set is not null
            {
                invoiceNumber = ds_InviceNo.Tables[0].Rows[0][0].ToString();
                txtBillNo.Text = "I "+invoiceNumber+"";
            
            }
                else
                txtBillNo.Text = null; //fill table 
            
        }
        private void fill_grdOther_table()
        {

            string q = "SELECT itemno AS itemNo, itemname AS itemName, itemcatagory AS itemCatagory,itemqty AS itemQty,itemprice AS itemPrice FROM otheritems ";
            DataSet ds_other = middle_access.db_access.SelectData(q);
            if (ds_other != null)
            {
                grdOther.DataSource = ds_other.Tables[0].DefaultView;
            }
            else
                grdOther.DataSource = null;
            
        }
        //*************** Set total quantity ****************************
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
                    string searched_qty = row_searched_qty.ItemArray.GetValue(0).ToString();
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

        public void set_no_of_brands()
        {
            string q = null;
            if (chk_cycle_search.Checked != true)
            {
                q = "SELECT DISTINCT COUNT(qty) FROM add_vehical_tyre"; ;
            }
            else if (chk_cycle_search.Checked == true)
            {
                q = "SELECT DISTINCT COUNT(qty) FROM add_cycle_tyre";
            }

            DataSet ds_no_of_brands = middle_access.db_access.SelectData(q);
            DataRow row_no_of_brands = ds_no_of_brands.Tables[0].Rows[0];
            int total_qty = Convert.ToInt32(row_no_of_brands.ItemArray.GetValue(0).ToString());

        }


        /// <summary>
        /// Fill_advance_search_tables this instance.
        /// </summary>
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

        /// <summary>
        /// Fill_advance_search_table_cycles this instance.
        /// </summary>
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

        /// <summary>
        /// Fill_sizes this instance.
        /// </summary>
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

        /// <summary>
        /// Check_stocks this instance.
        /// </summary>
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

        /// <summary>
        /// Searches this instance.
        /// </summary>
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
            qq = set_search_query_with_side(qq);
            qq = set_search_query_with_tyre_pattern(qq);
            refresh_cycle = qq;
            set_searched_qty(qq);
            string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side , t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_tyre_pattern AS Tyre_Type ,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price FROM add_cycle_tyre WHERE " + qq + " ";
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

        /// <summary>
        /// Set_search_query_with_types the specified q_.
        /// </summary>
        /// <param name="q_">The q_.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Set_search_query_with_sides the specified q_.
        /// </summary>
        /// <param name="q_">The q_.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Set_search_query_with_tyre_patterns the specified q_.
        /// </summary>
        /// <param name="q_">The q_.</param>
        /// <returns></returns>
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
                                qq = qq + "AND" + " t_make = '" + make + "' ";
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
        /// <summary>
        /// ////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            ////////////////////////grid view 1 cell click
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

                //////////////////////////********************////////////////////////////////
                this.Enabled = false;
                Billing_qty q = new Billing_qty();
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
                radGridView1.Visible = true;
                radGridView2.Visible = false;

                String q = "SELECT SUM(qty) FROM add_vehical_tyre";
                DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
                DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
                int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());

                lbl_tot.Text = total_qty.ToString();



            }
        }

        public void reset()
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
               
                Billing_Cycle_Category_Data.brand = brand;
                Billing_Cycle_Category_Data.size = size;
                Billing_Cycle_Category_Data.ply_rate = ply_rate;
                Billing_Cycle_Category_Data.thread_pattern = thread_pattern;
                Billing_Cycle_Category_Data.make = make;
                Billing_Cycle_Category_Data.tyre_pattern = tyre_pattern;
                Billing_Cycle_Category_Data.tube = tube;
                Billing_Cycle_Category_Data.side = side;



                this.Enabled = false;
                Billing_Qty_Cycle q = new Billing_Qty_Cycle();
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
            radGridView5.Visible = false;
        }

        private void txt_size_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = true;
        }

        private void radPanel2_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = false;
        }


        public void showText(int catagory_id)
        {
            txt_size.Text = vehical_category_data.get_catagory_id().ToString();
        }


        public void showTextCycle(int catagory_id)
        {
            txt_size.Text = Billing_Cycle_Category_Data.get_catagory_id().ToString();
        }

      
        private void AdvancedSearch_EnabledChanged(object sender, EventArgs e)
        {
            if (chk_cycle_search.Checked == true)
            {
                if (refresh_cycle == null)
                {
                    fill_advance_search_table_cycle();

                }
                else
                {
                    string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side , t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_tyre_pattern AS Tyre_Type ,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price FROM add_cycle_tyre WHERE " + refresh_cycle + "";
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
                    string q = "SELECT t_brand AS Brand, t_size AS Size, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_type AS Type,t_tube AS Tube_Type,qty AS Qty,unit_prize AS Unit_Price FROM add_vehical_tyre WHERE " + refresh_vehicle + "";
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

                //////////////////////////////////////////////////////////for batery

                if (chk_din_battery.Checked == true)
                {
                    if (refresh_battery == null)
                    {
                       fill_din_battery_search_table();

                    }
                    else
                    {
                        string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type, D.qty AS Qty, D.price AS Price FROM add_battery B, din D WHERE D.din_id = B.b_stok_id AND " + refresh_battery + "";
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
                        string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type, b_qty AS Qty, b_prize AS Price FROM add_battery WHERE " + refresh_battery + "";
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

                //////////////////////////////////////////////////////////
            }

            if (pnlTube.Visible == true)
            {
                fill_tube_search_table();
            }

            // ****************** to show the data in the invoice grid view *************************
            if (vehical_category_data.statusPass2Forms == true)
            {
                FillInvoiceGrid();
                vehical_category_data.statusPass2Forms = false;

            }

            if (Billing_Cycle_Category_Data.statusPass2Forms == true)
            {
                FillInvoiceGridForCycle();
                Billing_Cycle_Category_Data.statusPass2Forms = false;

            }

            if (billing_battery_category_data.statusPass2Forms == true)
            {
                FillInvoiceGridForBattry();
                billing_battery_category_data.statusPass2Forms = false;
            }

            if (Billing_Other_Catagory_Data.statusPass2Forms == true)
            {
                FillInvoiceGridForOther();
                Billing_Other_Catagory_Data.statusPass2Forms = false;
            }
            if (tube_category_data.statusPass2Forms == true)
            {
                FillInvoiceGridForTube();
                tube_category_data.statusPass2Forms = false;
            }



        }


        private void FillInvoiceGridForTube()
        {

            if (tube_category_data.invoiceQty != "")
            {
                DataGridViewRow NewRow = new DataGridViewRow();
                grdBill.Rows.Add(NewRow);
                int a = grdBill.RowCount;
                int catID = tube_category_data.get_battery_catagory_id();
                string brand = tube_category_data.brand;
                string size = tube_category_data.size;
                string qty = tube_category_data.invoiceQty;
                string unitPrice = tube_category_data.unitPrice;
                double linePrice = Convert.ToDouble(tube_category_data.unitPrice) * Convert.ToDouble(tube_category_data.invoiceQty);


                //string discount = "" ;    

                grdBill.Rows[a - 1].Cells[0].Value = catID.ToString();
                grdBill.Rows[a - 1].Cells[1].Value = brand + "   /   " + size;
                grdBill.Rows[a - 1].Cells[2].Value = qty;
                grdBill.Rows[a - 1].Cells[3].Value = unitPrice;
                grdBill.Rows[a - 1].Cells[4].Value = linePrice.ToString();
                grdBill.Rows[a - 1].Cells[5].Value = "T";

                //to calculate invoice total
                //uses total at vehical_category_data class
                vehical_category_data.total = linePrice + vehical_category_data.total;
                txtTotal.Text = vehical_category_data.total.ToString();
                tube_category_data.statusPass2Forms = false;

                //   i++;// i is a int type globle variable
            }

        }

        private void FillInvoiceGridForOther()
        {

            DataGridViewRow NewRow = new DataGridViewRow();
            grdBill.Rows.Add(NewRow);

            int a = grdBill.RowCount;
            string otherItemNo = Billing_Other_Catagory_Data.itemno;
            string itemName = Billing_Other_Catagory_Data.itemname;
            string qty = Billing_Other_Catagory_Data.qty;
            string unitPrice = Billing_Other_Catagory_Data.unitPrice;
            double linePrice = Convert.ToDouble(Billing_Other_Catagory_Data.unitPrice) * Convert.ToDouble(Billing_Other_Catagory_Data.qty);
             

            grdBill.Rows[a - 1].Cells[0].Value = otherItemNo;
            grdBill.Rows[a - 1].Cells[1].Value = itemName;
            grdBill.Rows[a - 1].Cells[2].Value = qty;
            grdBill.Rows[a - 1].Cells[3].Value = unitPrice;
            grdBill.Rows[a - 1].Cells[4].Value = linePrice.ToString();
            grdBill.Rows[a - 1].Cells[5].Value = "O";

            //to calculate invoice total
            //uses total at vehical_category_data class
            vehical_category_data.total = linePrice + vehical_category_data.total;
            txtTotal.Text = vehical_category_data.total.ToString();
            Billing_Other_Catagory_Data.statusPass2Forms = false;
        
        }



        private void FillInvoiceGridForBattry()
        {
            if (billing_battery_category_data.invoiceQty != "")
            {
            DataGridViewRow NewRow = new DataGridViewRow();
            grdBill.Rows.Add(NewRow);

            int a = grdBill.RowCount;
            int catID = billing_battery_category_data.get_battery_catagory_id();
            string brand = billing_battery_category_data.brand;
            string size = billing_battery_category_data.size;
            string voltage = billing_battery_category_data.voltage;
            string amp = billing_battery_category_data.amps;
            string qty = billing_battery_category_data.invoiceQty;
            string unitPrice = billing_battery_category_data.unitPrice;
            double linePrice = Convert.ToDouble(billing_battery_category_data.unitPrice) * Convert.ToDouble(billing_battery_category_data.invoiceQty);
          
            grdBill.Rows[a - 1].Cells[0].Value = catID.ToString();
            grdBill.Rows[a - 1].Cells[1].Value = brand + "   /   " + voltage + "   /   " + amp;
            grdBill.Rows[a - 1].Cells[2].Value = qty;
            grdBill.Rows[a - 1].Cells[3].Value = unitPrice;
            grdBill.Rows[a - 1].Cells[4].Value = linePrice.ToString();
            grdBill.Rows[a - 1].Cells[5].Value = "B";

            //to calculate invoice total
            //uses total at vehical_category_data class
            vehical_category_data.total = linePrice + vehical_category_data.total;
            txtTotal.Text = vehical_category_data.total.ToString();
            billing_battery_category_data.statusPass2Forms = false;

            }
        }

        private void FillInvoiceGridForCycle()
        {
            if (Billing_Cycle_Category_Data.qty != "")
            {
            DataGridViewRow NewRow = new DataGridViewRow();
            grdBill.Rows.Add(NewRow);

            int a = grdBill.RowCount;
            int catID = Billing_Cycle_Category_Data.get_catagory_id();
            string brand = Billing_Cycle_Category_Data.brand;
            string size = Billing_Cycle_Category_Data.size;
            string qty = Billing_Cycle_Category_Data.qty;
            string unitPrice = Billing_Cycle_Category_Data.unitPricee;
            double linePrice = Convert.ToDouble(Billing_Cycle_Category_Data.unitPricee) * Convert.ToDouble(Billing_Cycle_Category_Data.qty);
            

            grdBill.Rows[a - 1].Cells[0].Value = catID.ToString();
            grdBill.Rows[a - 1].Cells[1].Value = brand + "   /   " + size;
            grdBill.Rows[a - 1].Cells[2].Value = qty;
            grdBill.Rows[a - 1].Cells[3].Value = unitPrice;
            grdBill.Rows[a - 1].Cells[4].Value = linePrice.ToString();
            grdBill.Rows[a - 1].Cells[5].Value = "C";

            //to calculate invoice total
            //uses total at vehical_category_data class
            vehical_category_data.total = linePrice + vehical_category_data.total;
            txtTotal.Text = vehical_category_data.total.ToString();
            vehical_category_data.statusPass2Forms = false;          
            }
        }

        // *********** function to fill tha INVOICE GRID VIEW ***************
        public void FillInvoiceGrid()
        {
            if (vehical_category_data.qty != "")
            {
                DataGridViewRow NewRow = new DataGridViewRow();
                grdBill.Rows.Add(NewRow);

                int a = grdBill.RowCount;
                int catID = vehical_category_data.get_catagory_id();
                string brand = vehical_category_data.brand;
                string size = vehical_category_data.size;
                string qty = vehical_category_data.qty;
                string unitPrice = vehical_category_data.unitPrice;
                double linePrice = Convert.ToDouble(vehical_category_data.unitPrice) * Convert.ToDouble(vehical_category_data.qty);


                grdBill.Rows[a - 1].Cells[0].Value = catID.ToString();
                grdBill.Rows[a - 1].Cells[1].Value = brand + "   /   " + size;
                grdBill.Rows[a - 1].Cells[2].Value = qty;
                grdBill.Rows[a - 1].Cells[3].Value = unitPrice;
                grdBill.Rows[a - 1].Cells[4].Value = linePrice.ToString();
                grdBill.Rows[a - 1].Cells[5].Value = "V"; ;

                //to calculate invoice total
                //uses total at vehical_category_data class
                vehical_category_data.total = linePrice + vehical_category_data.total;
                txtTotal.Text = vehical_category_data.total.ToString();
                vehical_category_data.statusPass2Forms = false;
               
            }
           
        }
     
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBillNo.Text != "" && grdBill.RowCount != 0)
            {//if customerNo is not null
                if (MessageBox.Show("Do you want to add Bill?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string billNo = txtBillNo.Text;
                    string billDate = dtpBillingDate.Value.Date.ToString("yyyy-MM-dd");
                    //string customerNo = "50";
                    double billTotal = Convert.ToDouble(txtTotal.Text);
                    //double invoiceDiscount = Convert.ToDouble(txtDiscount.Text);
                    string billNote = txtBillNote.Text;
                    string salesRef = txtSalesRef.Text;


                    if (txtBillNo.Text != "")//if customerNo is not null
                    {
                        //insert into  invoice infomation customer table 
                        string q1 = "INSERT INTO companybills(billno,billnote,billtotal,billdate,salesref) VALUES ('" + billNo + "','" + billNote + "'," + billTotal + ",'" + billDate + "','" + salesRef + "')";
                        bool status1 = middle_access.db_access.InsertData(q1);


                        if (status1 == true) // if Invoice Data are successfully added into the Invoice table
                        {

                            for (int i = 0; i < grdBill.Rows.Count; i++)
                            {


                                //invoiceno is defined above
                                int billlLineNo = i;
                                // double discount = 0;
                                //working
                                int lineQuantity = Convert.ToInt32(grdBill.Rows[i].Cells[2].Value.ToString());
                                double itemPrice = Convert.ToDouble(grdBill.Rows[i].Cells[3].Value.ToString());
                                int itemNo = Convert.ToInt32(grdBill.Rows[i].Cells[0].Value.ToString());

                                string q2 = "INSERT INTO billlines(billno,billlineno,itemno,billlineqty,itemprice) VALUES ('" + billNo + "'," + billlLineNo + "," + itemNo + "," + lineQuantity + "," + itemPrice + ")";
                                bool status2 = middle_access.db_access.InsertData(q2);
                               

                            }

                        }


                        bool status3 = invoiceItemsAdd();
                        if (status1 == true) // if data is insert
                        {
                            MessageBox.Show("Successfully Inserted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button  
                            //for the auto increment
                            int nextInvoiceNo = (Convert.ToInt32(invoiceNumber)) + 1;
                            string q = "UPDATE autoincrem SET maxno = " + nextInvoiceNo + "  WHERE tablename = 'I'";
                            bool status = middle_access.db_access.UpdateData(q);
                            clear();

                        }

                    }

                    else // if  invoice number Exists
                    {
                        MessageBox.Show("Please Check Inputs !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
              

               
            }
            else // if bill is empty
            {
                MessageBox.Show("Invalid operation!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clear()
        {
            dtpBillingDate.Value = DateTime.Today.Date;
            txtBillNote.Text = "";
            txtSalesRef.Text = "";
            vehical_category_data.total = 0;            
            txtTotal.Text = "0";
            txtDiscount.Text = "0";
            grdBill.Rows.Clear();
            loadBillNo();            
            fill_advance_search_table();
            fill_advance_search_table_cycle();
            fill_battery_search_table();
            fill_din_battery_search_table();
            fill_tube_search_table();
           
        }

        private bool invoiceItemsAdd()
        {
            for (int i = 0; i < grdBill.Rows.Count; i++)
            {


                //invoiceno is defined above
                int invoiceLineNo = i;


                int quantity = Convert.ToInt32(grdBill.Rows[i].Cells[2].Value.ToString());
                string itemCat = grdBill.Rows[i].Cells[4].Value.ToString();
                double retailPrice = Convert.ToDouble(grdBill.Rows[i].Cells[3].Value.ToString());
                int itemNo = Convert.ToInt32(grdBill.Rows[i].Cells[0].Value.ToString());

               

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (grdBill.Rows[i].Cells[5].Value == "V")
                {

                    string q = "SELECT qty FROM add_vehical_tyre WHERE t_stok_id = " + itemNo + " ";
                    DataSet ds_battery_Oty = middle_access.db_access.SelectData(q);

                    string existingQty = ds_battery_Oty.Tables[0].Rows[0][0].ToString();
                    int newQty = Convert.ToInt32(existingQty) + quantity;

                    string q1 = "UPDATE add_vehical_tyre SET qty = " + newQty + "  WHERE t_stok_id = " + itemNo + " ";
                    middle_access.db_access.UpdateData(q1);

                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (grdBill.Rows[i].Cells[5].Value == "C")
                {

                    string q = "SELECT qty FROM add_cycle_tyre WHERE t_stok_id = " + itemNo + " ";
                    DataSet ds_battery_Oty = middle_access.db_access.SelectData(q);

                    string existingQty = ds_battery_Oty.Tables[0].Rows[0][0].ToString();
                    int newQty = Convert.ToInt32(existingQty) + quantity;

                    string q1 = "UPDATE add_cycle_tyre SET qty = " + newQty + "  WHERE t_stok_id = " + itemNo + " ";
                    middle_access.db_access.UpdateData(q1);
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
                //batery done

                else if (grdBill.Rows[i].Cells[5].Value == "B")
                {
                    string q = "SELECT b_qty FROM add_battery WHERE b_stok_id = " + itemNo + " ";
                    DataSet ds_battery_Oty = middle_access.db_access.SelectData(q);

                    string existingQty = ds_battery_Oty.Tables[0].Rows[0][0].ToString();
                    int newQty = Convert.ToInt32(existingQty) + quantity;

                    string q1 = "UPDATE add_battery SET b_qty = " + newQty + "  WHERE b_stok_id = " + itemNo + " ";
                    middle_access.db_access.UpdateData(q1);

                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                else if (grdBill.Rows[i].Cells[5].Value == "T")
                {
                    string q = "SELECT t_qty FROM tube_add WHERE t_stok_id = " + itemNo + " ";
                    DataSet ds_tube_Oty = middle_access.db_access.SelectData(q);

                    string existingQty = ds_tube_Oty.Tables[0].Rows[0][0].ToString();
                    int newQty = Convert.ToInt32(existingQty) + quantity;

                    string q1 = "UPDATE tube_add SET t_qty = " + newQty + "  WHERE t_stok_id = " + itemNo + " ";
                    middle_access.db_access.UpdateData(q1);

                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (itemCat == "O") { }



                ///////////////////////////////////////////////////////////////////////////////////////////////////////////


            }
            return true;
        }
       //////////////////////////////////////////////////////////////////////////////////////////batery

        //for the form load
        public void loadBatery()
        {
            panel_batary_stock.Visible = false;          
            radGridView5.Visible = false;
            fill_com_brandBatery();
            fill_sizeBatery();
            fill_com_voltage();
            fill_com_amps();
            fill_com_amp();         
            fill_battery_search_table();


            radGridView5.Visible = false;
            com_search_brand.Enabled = false;
            txt_search_size.Enabled = false;
            groupBox3.Enabled = false;
            com_amp.Enabled = false;
            set_total_qty_batery();
        }



        public void set_total_qty_batery()
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
                lbl_search_qty.Text = "0";
            }
            else
            {
                lbl_tot_qty.Text = "0";
                lbl_search_qty.Text = "0";
            }
        }


        /// <summary>
        /// Fill_com_brands the batery.
        /// </summary>
        public void fill_com_brandBatery()
        {
            string q1 = "SELECT * FROM battery_brand ";
            DataSet ds_brand = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand != null) // if data set is not null
            {
              
                com_search_brand.DataSource = ds_brand.Tables[0]; //fill table 
                com_search_brand.DisplayMember = "battery_brand";
                com_search_brand.ValueMember = "battery_brand";
                com_search_brand.Text = "";

            }           
        }
        /// <summary>
        /// Fill_sizes the batery.
        /// </summary>
        public void fill_sizeBatery()
        {

            string q1 = "SELECT battery_size FROM battery_size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {               
                radGridView5.DataSource = ds_size.Tables[0];

            }          
            radGridView5.DataSource = null;
        }

        /// <summary>
        /// Fill_com_voltages this instance.
        /// </summary>
        public void fill_com_voltage()
        {
            string q1 = "SELECT * FROM battery_voltage";
            DataSet ds_voltage = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_voltage != null) // if data set is not null
            {
                

            }          
        }

        public void fill_com_amps()
        {
            string q1 = "SELECT * FROM battery_amps";
            DataSet ds_amps = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_amps != null) // if data set is not null
            {
               
            }
          
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
           
        }


        public void fill_battery_search_table()
        {

            string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type, b_qty AS Qty, b_prize AS Price FROM add_battery";
            DataSet ds_battery = middle_access.db_access.SelectData(q);
            if (ds_battery != null)
            {
                grdBattry.DataSource = ds_battery.Tables[0].DefaultView;
            }
            else
                grdBattry.DataSource = null;
        }

        public void fill_add_battery_table_din()
        {            
            string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type FROM add_battery B,din D WHERE B.b_stok_id = D.din_id";
            DataSet ds_add_battery = middle_access.db_access.SelectData(q);
            if (ds_add_battery != null)
            {
               

            }
            else
            {
              

            }
        }

        public void fill_add_battery_table()
        {

            string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type FROM add_battery";
            DataSet ds_add_battery = middle_access.db_access.SelectData(q);
            if (ds_add_battery != null)
            {
               
            }
            else
            {
               

            }
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




        private string set_amp()
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
            set_total_qty_batery();
        }

        public void radButton5_Click(object sender, EventArgs e)
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
        private void radButton3_Click_1(object sender, EventArgs e)
        {
            pnlTyre.Visible = false;
            panel_other_search.Visible = false;  
            panel_batary_stock.Visible = true;
            pnlTube.Visible = false;
            lblBilling.Text = "Billing (Batery Search)";

           
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            pnlTyre.Visible = true;
            panel_batary_stock.Visible = false;
            panel_other_search.Visible = false;
            pnlTube.Visible = false;
            lblBilling.Text = "Billing (Tyre Search)";
            
        }

        /// <summary>
        /// Handles the Click event of the chk_type control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chk_type_Click(object sender, EventArgs e)
        {
            if (chk_type.Checked == false)
            {
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox3.Enabled = false;

            }
        }

        /// <summary>
        /// Handles the Click event of the chk_brandd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chk_brandd_Click(object sender, EventArgs e)
        {
            if (chk_brandd.Checked == false)
            {
                com_search_brand.Enabled = true;
            }
            else
            {
                com_search_brand.Enabled = false;
                com_search_brand.Text = "";
            }
        }

        /// <summary>
        /// Handles the Click event of the chk_din_battery control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chk_din_battery_Click(object sender, EventArgs e)
        {
            if (chk_din_battery.Checked != true)
            {
                fill_battery_search_table();
                billing_battery_category_data.din_check_box_val = "DIN";
            }
            else
            {
                fill_din_battery_search_table();

                billing_battery_category_data.din_check_box_val = "NORMAL";
            }

            set_total_qty_batery();
        }

        public void fill_din_battery_search_table()
        {

            string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type, D.qty AS Qty, D.price AS Price FROM add_battery B, din D WHERE D.din_id = B.b_stok_id";
            DataSet ds_battery = middle_access.db_access.SelectData(q);
            if (ds_battery != null)
            {
                grdBattry.DataSource = ds_battery.Tables[0].DefaultView;
            }
            else
                grdBattry.DataSource = null;
        }


        /// <summary>
        /// Search_normal_batteries this instance.
        /// </summary>
        public void search_normal_battery()
        {
            string qq = set_search_query_batery();
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
                        grdBattry.DataSource = ds_advance_search.Tables[0].DefaultView;
                    }
                    else
                    {
                        grdBattry.DataSource = null;
                    }
                }
            }
            else
            {
                string q1;
                if (qq != null)
                {
                    q1 = qq + "AND " + qqq;
                }
                else
                {
                    q1 = qqq;
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
                        grdBattry.DataSource = ds_advance_search.Tables[0].DefaultView;
                    }
                    else
                    {
                        grdBattry.DataSource = null;
                    }
                }
            }

        }

        /// <summary>
        /// Set_search_query_bateries this instance.
        /// </summary>
        /// <returns></returns>
        public string set_search_query_batery()
        {

            string brand = com_search_brand.Text;
            string size = txt_search_size.Text;
            string battery_type = type;
            string qq = null;

            if (chk_brandd.Checked == true)
            {
                qq = " b_brand ='" + brand + "' ";
                if (chk_size_batery.Checked == true)
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
                if (chk_size_batery.Checked == true)
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
        /// <summary>
        /// Search_din_batteries this instance.
        /// </summary>
        public void search_din_battery()
        {
            string qq = set_search_query();
            refresh_battery = qq;
            set_searched_qty_batery(qq);
            if (qq == null)
            {

            }
            else
            {

                string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type, D.qty AS Qty, D.price AS Price FROM add_battery B, din D WHERE " + qq + " AND B.b_stok_id = D.din_id";
                DataSet ds_advance_search = middle_access.db_access.SelectData(q);
                if (ds_advance_search != null)
                {
                    grdBattry.DataSource = ds_advance_search.Tables[0].DefaultView;
                }
                else
                {
                    grdBattry.DataSource = null;
                }
            }
        }


        /// <summary>
        /// Set_searched_qty_bateries the specified where_value.
        /// </summary>
        /// <param name="where_value">The where_value.</param>
        public void set_searched_qty_batery(string where_value)
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


        private void chk_brandd_Click_1(object sender, EventArgs e)
        {
            if (chk_brandd.Checked == false)
            {
                com_search_brand.Enabled = true;
            }
            else
            {
                com_search_brand.Enabled = false;
                com_search_brand.Text = "";
            }
        }

        private void chk_amp_Click_1(object sender, EventArgs e)
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

        private void txt_search_size_Click(object sender, EventArgs e)
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

        private void radGridView5_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_search_size.Text = radGridView5.Rows[row_index].Cells[0].Value.ToString();
            // txt_size.Focus();
            radGridView5.Visible = false;
        }

        private void radGridView4_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            int r_count = grdBattry.RowCount;
            if (val >= 0)
            {
                string brand = grdBattry.Rows[e.RowIndex].Cells[0].Value.ToString();
                string size = grdBattry.Rows[e.RowIndex].Cells[1].Value.ToString();
                string voltage = grdBattry.Rows[e.RowIndex].Cells[2].Value.ToString();
                string amp = grdBattry.Rows[e.RowIndex].Cells[3].Value.ToString();
                string type = grdBattry.Rows[e.RowIndex].Cells[4].Value.ToString();


                billing_battery_category_data.brand = brand;
                billing_battery_category_data.size = size;
                billing_battery_category_data.voltage = voltage;
                billing_battery_category_data.amps = amp;
                billing_battery_category_data.type = type;
                billing_battery_category_data.qty = grdBattry.Rows[e.RowIndex].Cells[5].Value.ToString();
                billing_battery_category_data.price = grdBattry.Rows[e.RowIndex].Cells[6].Value.ToString();



                this.Enabled = false;
                Billing_Battery_Qty q = new Billing_Battery_Qty();
                q.MdiParent = DHNAULA.ActiveForm;
                q.Visible = true;
            }
        }

        private void chk_size_batery_Click(object sender, EventArgs e)
        {
            if (chk_size_batery.Checked == false)
            {
                txt_search_size.Enabled = true;
               
            }
            else
            {
                txt_search_size.Enabled = false;
                txt_search_size.Text = "";
                radGridView5.Visible = false;

            }
        }

        private void radGridView5_SelectionChanged(object sender, EventArgs e)
        {

            row_index = radGridView5.CurrentRow.Index;

        }

        private void chk_din_battery_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chk_din_battery.Checked != true)
            {
                fill_battery_search_table();
                billing_battery_category_data.din_check_box_val = "NORMAL";
            }
            else
            {
                fill_din_battery_search_table();

                billing_battery_category_data.din_check_box_val = "DIN";
            }

            set_total_qty_batery();
        }

        private void rad_M_CheckedChanged(object sender, EventArgs e)
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
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////
        /// Other Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButton7_Click(object sender, EventArgs e)
        {
            panel_other_search.Visible = true;
            panel_batary_stock.Visible = false;
            pnlTyre.Visible = false;
            pnlTube.Visible = false;
            lblBilling.Text = "Billing (Other Search)";
  
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {      
            clear();

        }

        private void grdOther_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //cmdSearchCat.

            int val = e.RowIndex;
            int r_count = grdOther.RowCount;
            if (val >= 0)
            {
                string itemNo = grdOther.Rows[e.RowIndex].Cells[0].Value.ToString();
                string itemName = grdOther.Rows[e.RowIndex].Cells[1].Value.ToString();
                string itemCatagory = grdOther.Rows[e.RowIndex].Cells[2].Value.ToString();
                string itemQty = grdOther.Rows[e.RowIndex].Cells[3].Value.ToString();
                string itemPrice = grdOther.Rows[e.RowIndex].Cells[4].Value.ToString();


                ////////////////////////////////*********************/////////////////////////////////
                Billing_Other_Catagory_Data.itemno = itemNo;
                Billing_Other_Catagory_Data.itemname = itemName;
                Billing_Other_Catagory_Data.catagory = itemCatagory;
                Billing_Other_Catagory_Data.qty = itemQty;
                Billing_Other_Catagory_Data.unitPrice = itemPrice;



                this.Enabled = false;
                Billing_Other_Qty q = new Billing_Other_Qty();
                q.Visible = true;
            }



            ////////////////////////////////////////////////////////////////////////////////////////////
        }    

      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

        private void radButton9_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

        private void radButton6_Click_1(object sender, EventArgs e)
        {
            pnlTube.Visible = true;
            pnlTyre.Visible = false;
            panel_batary_stock.Visible = false;
            panel_other_search.Visible = false;
            lblBilling.Text = "Billing (Tube Search)";

            fill_tube_search_table();
            fill_com_Tube_brand();
            fill_Tube_size();
            fill_com_Tube_make();
            grdTubeSize.Visible = false;
            txt_barcode.Focus();
            
        }

        /// <summary>
        /// Fill_tube_search_tables this instance.
        /// </summary>
        public void fill_tube_search_table()
        {

            string q = "SELECT t_brand AS Brand, t_size AS Size, t_amps AS Make, t_type AS Tube_Type, t_qty AS Qty, t_prize AS Price FROM tube_add";
            DataSet ds_tube = middle_access.db_access.SelectData(q);
            if (ds_tube != null)
            {
                grdTubesearch.DataSource = ds_tube.Tables[0].DefaultView;
            }
            else
                grdTubesearch.DataSource = null;
        }

        /// <summary>
        /// Fill_com_s the tube_brand.
        /// </summary>
        public void fill_com_Tube_brand()
        {
            string q1 = "SELECT * FROM tube_brand ";
            DataSet ds_brand = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand != null) // if data set is not null
            {
                comTubeBrand.DataSource = ds_brand.Tables[0]; //fill table 
                comTubeBrand.DisplayMember = "t_brand";
                comTubeBrand.ValueMember = "t_brand";
                comTubeBrand.Text = "";

            }
            else
                comTubeBrand.DataSource = null; //fill table 
        }

        /// <summary>
        /// Fill_s the tube_size.
        /// </summary>
        public void fill_Tube_size()
        {

            string q1 = "SELECT t_size FROM tube_size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                grdTubeSize.DataSource = ds_size.Tables[0]; //fill table 
                //radGridView5.DataSource = ds_size.Tables[0];


            }
            else
                grdTubeSize.DataSource = null; //fill table 
            //radGridView5.DataSource = null;
        }

        /// <summary>
        /// Fill_com_s the tube_make.
        /// </summary>
        public void fill_com_Tube_make()
        {
            string q1 = "SELECT * FROM tube_amps";
            DataSet ds_amps = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_amps != null) // if data set is not null
            {
                comTubeMake.DataSource = ds_amps.Tables[0]; //fill table 
                comTubeMake.DisplayMember = "tube_amps";
                comTubeMake.ValueMember = "tube_amps";
                comTubeMake.Text = "";



            }
            else
                comTubeMake.DataSource = null; //fill table 
        }

        private void chkTubeBrand_Click(object sender, EventArgs e)
        {
            if (chkTubeBrand.Checked == false)
            {
                comTubeBrand.Enabled = true;
            }
            else
            {
                comTubeBrand.Enabled = false;
                comTubeBrand.Text = "";
            }
        }

        private void chkTubeMake_Click(object sender, EventArgs e)
        {
            if (chkTubeMake.Checked == false)
            {
                comTubeMake.Enabled = true;
            }
            else
            {
                comTubeMake.Enabled = false;
                comTubeMake.Text = "";
            }
        }

        private void chkTubeSize_Click(object sender, EventArgs e)
        {
            if (chkTubeSize.Checked == false)
            {
                txtTubeSize.Enabled = true;
                //  fill_size();

            }
            else
            {
                txtTubeSize.Enabled = false;
                txtTubeSize.Text = "";
                grdTubeSize.Visible = false;

            }
        }

        private void chkTubeType_Click(object sender, EventArgs e)
        {
            if (chkTubeType.Checked == false)
            {
                groupBox5.Enabled = true;
            }
            else
            {
                groupBox5.Enabled = false;

            }
        }

        private void rad_tube_v_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_tube_v.Checked == true)
            {
                type = "Vehicle";
            }
            if (rad_tube_c.Checked == true)
            {
                type = "Cycle";
            }
        }

        private void rad_tube_c_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_tube_v.Checked == true)
            {
                type = "Vehicle";
            }
            if (rad_tube_c.Checked == true)
            {
                type = "Cycle";
            }
        }

        private void txtTubeSize_TextChanged(object sender, EventArgs e)
        {
            string text_text = txtTubeSize.Text;
            filter_text_box_text_size_search(text_text);
        }

        public void filter_text_box_text_size_search(string text_text)
        {
            string q1 = "SELECT t_size FROM tube_size WHERE t_size  LIKE  '" + text_text + "%' ";
            ds_size = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_size != null) // if data set is not null
            {
                // grdTubeSize.Visible = false;              
                grdTubeSize.DataSource = ds_size.Tables[0];
                grdTubeSize.Visible = true;


            }
            else
            {
                grdTubeSize.DataSource = null;
                grdTubeSize.Visible = false;
            }

        }

        private void txtTubeSize_Click(object sender, EventArgs e)
        {
            fill_Tube_size();
            grdTubeSize.Visible = true;
        }

        private void grdTubeSize_SelectionChanged(object sender, EventArgs e)
        {
            row_index = grdTubeSize.CurrentRow.Index;
        }

        private void grdTubeSize_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txtTubeSize.Text = grdTubeSize.Rows[row_index].Cells[0].Value.ToString();
            txtTubeSize.Focus();
            grdTubeSize.Visible = false;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            grdTubeSize.Visible = false;
        }

        private void txt_barcode_TextChanged(object sender, EventArgs e)
        {
            int count = 0;
            int tubeIntId = 0;
            foreach (char c in txt_barcode.Text)
            {
                count++;
            }

            if (count == 9)
            {
                try
                {
                    tubeIntId = Convert.ToInt32(txt_barcode.Text.Substring(3));
                }
                catch (Exception E)
                {

                }

                string q = "SELECT t_brand AS Brand, t_size AS Size, t_amps AS Make, t_type AS Tube_Type, t_qty AS Qty, t_prize AS Price FROM tube_add WHERE t_stok_id = '" + tubeIntId + "'";
                DataSet ds_tube = middle_access.db_access.SelectData(q);
                if (ds_tube != null)
                {
                    DataRow dr_tube = ds_tube.Tables[0].Rows[0];

                    tube_category_data.brand = dr_tube.ItemArray.GetValue(0).ToString();
                    tube_category_data.size = dr_tube.ItemArray.GetValue(1).ToString();
                    // tube_category_data.voltage = voltage;
                    tube_category_data.amps = dr_tube.ItemArray.GetValue(2).ToString();
                    tube_category_data.type = dr_tube.ItemArray.GetValue(3).ToString();
                    tube_category_data.qty = dr_tube.ItemArray.GetValue(4).ToString();
                    tube_category_data.price = dr_tube.ItemArray.GetValue(5).ToString();


                    this.Enabled = false;
                    tube_qty q1 = new tube_qty();
                    q1.MdiParent = DHNAULA.ActiveForm;
                    q1.Visible = true;
                }
                else
                {
                    MessageBox.Show("No shuch a tube in the database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_barcode.Clear();
                }
                txt_barcode.Clear();
            }
        }

        private void radButton10_Click_1(object sender, EventArgs e)
        {
            search_normal_tube();
        }

        private void radButton11_Click(object sender, EventArgs e)
        {
            fill_tube_search_table();
        }

        /// <summary>
        /// Search_normal_tubes this instance.
        /// </summary>
        public void search_normal_tube()
        {
            string qq = set_tube_search_query();
            string qqq = set_make();
            if (qqq == null)
            {
                refresh_tube = qq;
                //set_tube_searched_qty(qq);
                if (qq == null)
                {
                    grdTubesearch.DataSource = null;
                }
                else
                {

                    string q = "SELECT t_brand AS Brand, t_size AS Size, t_amps AS Make, t_type AS Tube_Type, t_qty AS Qty, t_prize AS Price FROM tube_add WHERE " + qq + "";
                    DataSet ds_advance_search = middle_access.db_access.SelectData(q);
                    if (ds_advance_search != null)
                    {
                        grdTubesearch.DataSource = ds_advance_search.Tables[0].DefaultView;
                    }
                    else
                    {
                        grdTubesearch.DataSource = null;
                    }
                }
            }
            else
            {
                string q1;
                if (qq != null)
                {
                    q1 = qq + "AND " + qqq;
                }
                else
                {
                    q1 = qqq;
                }

                refresh_tube = q1;
                set_tube_searched_qty(q1);
                if (q1 == null)
                {

                }
                else
                {

                    string q = "SELECT t_brand AS Brand, t_size AS Size, t_amps AS Make, t_type AS Tube_Type, t_qty AS Qty, t_prize AS Price FROM tube_add WHERE " + q1 + "";
                    DataSet ds_advance_search = middle_access.db_access.SelectData(q);
                    if (ds_advance_search != null)
                    {
                        grdTubesearch.DataSource = ds_advance_search.Tables[0].DefaultView;
                    }
                    else
                    {
                        grdTubesearch.DataSource = null;
                    }
                }
            }

        }


        public string set_tube_search_query()
        {

            string brand = comTubeBrand.Text;
            string size = txtTubeSize.Text;
            string tube_type = type;
            string qq = null;

            if (chkTubeBrand.Checked == true)
            {
                qq = " t_brand ='" + brand + "' ";
                if (chkTubeSize.Checked == true)
                {
                    qq = qq + "AND" + " t_size ='" + size + "' ";
                    if (chkTubeType.Checked == true)
                    {
                        qq = qq + "AND" + " t_type ='" + tube_type + "' ";
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (chkTubeType.Checked == true)
                    {
                        qq = qq + "AND" + " t_type ='" + tube_type + "' ";
                    }
                }
            }
            else
            {
                if (chkTubeSize.Checked == true)
                {
                    qq = " t_size ='" + size + "' ";
                    if (chkTubeType.Checked == true)
                    {
                        qq = qq + "AND" + " t_type ='" + tube_type + "' ";
                    }
                }
                else
                {
                    if (chkTubeType.Checked == true)
                    {
                        qq = " t_type ='" + tube_type + "' ";
                    }
                }
            }


            return qq;
        }


        /// <summary>
        /// Set_makes this instance.
        /// </summary>
        /// <returns></returns>
        public string set_make()
        {
            string make = comTubeMake.Text;
            string qq = null;
            if (chkTubeMake.Checked == true)
            {
                qq = "t_amps = '" + make + "'";
            }

            else
            {

            }

            return qq;
        }

        public void set_tube_searched_qty(string where_value)
        {
            string q = null;
            string value = where_value;
            if (value == null)
            {
                // lbl_search_qty.Text = "0";
            }
            else
            {
                //TODO : Add  add_tube table
                if (chk_din_tube.Checked != true)
                {
                    q = "SELECT SUM(t_qty) FROM tube_add WHERE " + value + "";
                }
                else if (chk_din_tube.Checked == true)
                {
                    q = "SELECT SUM(qty) FROM din D,tube_add B  WHERE B." + value + " AND B.t_stok_id = D.din_id ";
                }




                DataSet ds_searched_qty = middle_access.db_access.SelectData(q);
                if (ds_searched_qty != null)
                {
                    DataRow row_searched_qty = ds_searched_qty.Tables[0].Rows[0];
                    string searched_qty = row_searched_qty.ItemArray.GetValue(0).ToString();
                    if (searched_qty == "")
                    {
                        // lbl_search_qty.Text = "0";
                    }
                    else
                    {
                        // lbl_search_qty.Text = searched_qty.ToString();
                    }
                }
                else
                {
                    // lbl_search_qty.Text = "0";
                }
            }
        }

        private void grdTubesearch_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            int r_count = grdTubesearch.RowCount;
            if (val >= 0)
            {
                string brand = grdTubesearch.Rows[e.RowIndex].Cells[0].Value.ToString();
                string size = grdTubesearch.Rows[e.RowIndex].Cells[1].Value.ToString();
                string amp = grdTubesearch.Rows[e.RowIndex].Cells[2].Value.ToString();
                string type = grdTubesearch.Rows[e.RowIndex].Cells[3].Value.ToString();

                tube_category_data.brand = brand;
                tube_category_data.size = size;
                tube_category_data.amps = amp;
                tube_category_data.type = type;
                tube_category_data.qty = grdTubesearch.Rows[e.RowIndex].Cells[4].Value.ToString();
                tube_category_data.price = grdTubesearch.Rows[e.RowIndex].Cells[5].Value.ToString();


                this.Enabled = false;
                tube_qty q = new tube_qty();
                q.MdiParent = DHNAULA.ActiveForm;
                q.Visible = true;
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0" + ".00";
                    double discount = Convert.ToDouble(txtDiscount.Text.ToString());
                    double price = Convert.ToDouble(txtTotal.Text.ToString());

                    txtFinalTotal.Text = (price - discount).ToString();
                }
                else
                {
                    double discount = Convert.ToDouble(txtDiscount.Text.ToString());
                    double price = Convert.ToDouble(txtTotal.Text.ToString());
                    txtFinalTotal.Text = (price - discount).ToString() + ".00";
                }


            }
            catch
            {

            }
        }

        private void grdBill_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;

            if (val >= 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grdBill.Rows[e.RowIndex].Delete();
                    double total = 0.0f;
                    txtTotal.Clear();
                    txtFinalTotal.Clear();
                    int r_count = grdBill.RowCount;
                    for (int i = 0; i < r_count; i++)
                    {
                        int qty = Convert.ToInt32(grdBill.Rows[i].Cells[2].Value.ToString());
                        double unitPrice = Convert.ToDouble((grdBill.Rows[i].Cells[3].Value.ToString()));

                        total += qty * unitPrice;
                    }
                    txtTotal.Text = total.ToString();
                    vehical_category_data.total = total;
                    //   txtFinalTotal.Text = (total - Convert.ToDouble());
                }

                else
                {


                }
            }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtFinalTotal.Text = txtTotal.Text.ToString();
        }


    }
}
    