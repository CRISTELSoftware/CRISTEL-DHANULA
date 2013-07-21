using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq.Expressions;
using System.Drawing.Printing;
using System.Runtime.InteropServices;



namespace TMT_2012
{
    public partial class Invoice : Telerik.WinControls.UI.RadForm
    {

        string invoiceNumber; // used for the cus no auto generation purpose
        public static string brand;
        string type;
        Point moveStart;
        DataSet ds_size;
        int row_index;
       // int i = 0;
        int r;
        string refresh_vehicle = null;
        string refresh_cycle = null;
        private string p;
        string refresh_battery = null;
        DataSet ds_name;
        DataSet ds_ID;
        string refresh_tube = null;
        //double linePrice;

        public Invoice()
        {
            InitializeComponent();
        }

        public Invoice(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        //*************** FORM CLOSE BUTTON**************************
        private void radButton10_Click(object sender, EventArgs e)
        {
            this.Close();
            //    Form f = (Form)Application.OpenForms["main"];
            //    f.Enabled = true;  
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void AdvancedSearch_Load(object sender, EventArgs e)
        {
            //grdInvoice.Columns["Price"] = edit;
            loadInvoiceNo();//
            //fillCustomerName();//
            
            fill_advance_search_table();
            fill_advance_search_table_cycle();//put to cycle            
            fill_com_brand();
            fill_size();
            fill_com_ply_rate();
            fill_com_make();
            
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
            grdFillCustomer.Visible = false;
            pnlTube.Visible = false;

           set_total_qty();
           fill_grdOther_table();//put to other bbuttn
           loadBatery();//load to bateery
           lblInvoice.Text = "Invoice (Tyre Search)";
           string q = "SELECT * FROM customer";
           filter_customer_ID(q);
           txtCustomerName.Text = "";
           com_customer_id.Text = "";
           grdFillCustomer.Visible = false;
           btnAddPayment.Visible = false;

        }

        

        private void loadInvoiceNo()
        {
            string q1 = "SELECT maxno FROM autoincrem where tablename='I'";
            DataSet ds_InviceNo = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table           

            if (ds_InviceNo != null) // if data set is not null
            {
                invoiceNumber = ds_InviceNo.Tables[0].Rows[0][0].ToString();
                txtInvoiceNo.Text = "I " + invoiceNumber + "";


            }
            else
                txtInvoiceNo.Text = null; //fill table 
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





        //****************** fill Customer Name ****************************
        //private void fillCustomerName()
        //{

        //    string q1 = "SELECT customername,customerno FROM customer";
        //    DataSet ds_made1 = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table       

        //    if (ds_made1 != null) // if data set is not null
        //    {

        //        txtCustomerName.DataSource = ds_made1.Tables[0];       //fill table 
        //        cmbCusName.DisplayMember = "customername";
        //        cmbCusName.ValueMember = "customerno";
        //        cmbCusName.Text = " ";

        //    }
        //    else

        //        cmbCusName.DataSource = null; //fill table 
        //}

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

            //lbl_tot.Text = total_qty.ToString();
            //lbl_sh_qty.Text = "0";

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

           // lbl_tot.Text = total_qty.ToString();




        }

        public void set_searched_qty(string where_value)
        {
            string q = null;
            string value = where_value;
            if (value == null)
            {
               // lbl_sh_qty.Text = "0";
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
                        //lbl_sh_qty.Text = "0";
                    }//
                    else
                    {
                       // lbl_sh_qty.Text = searched_qty.ToString();
                    }
                }
                else
                {
                    //lbl_sh_qty.Text = "0";
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
                radGridView2.Visible = true;
                radGridView1.Visible = false;

                String q = "SELECT SUM(qty) FROM add_cycle_tyre";
                DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
                DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
                int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());

                //lbl_tot.Text = total_qty.ToString();
                
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

                //lbl_tot.Text = total_qty.ToString();



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

                //lbl_tot.Text = total_qty.ToString();
                //lbl_sh_qty.Text = "0";

            }
            else
            {
                fill_advance_search_table_cycle();
                String q = "SELECT SUM(qty) FROM add_cycle_tyre";
                DataSet ds_totat_qty = middle_access.db_access.SelectData(q);
                DataRow row_total_qty = ds_totat_qty.Tables[0].Rows[0];
                int total_qty = Convert.ToInt32(row_total_qty.ItemArray.GetValue(0).ToString());

                //lbl_tot.Text = total_qty.ToString();
                //lbl_sh_qty.Text = "0";


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


                ////////////////////////////////*********************/////////////////////////////////
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
            radGridView5.Visible = false;
            grdFillCustomer.Visible = false;
        }

        private void txt_size_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = true;
        }

        private void radPanel2_Click(object sender, EventArgs e)
        {
            radGridView3.Visible = false;
        }

        private void radButton3_Click(object sender, EventArgs e)
        {


        }

        private void chk_cycle_search_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            //if (chk_cycle_search.Checked == true)
            //{
            //    identify_stok.check = "C";
            //}
            //else
            //{
            //    identify_stok.check = "V";
            //}
        }

        private void radLabel1_Click(object sender, EventArgs e)
        {
            //dnt Know what this is

        }




        /// //////////////////////////////////////////



        public void showText(int catagory_id)
        {
            txt_size.Text = vehical_category_data.get_catagory_id().ToString();
        }


        public void showTextCycle(int catagory_id)
        {
            txt_size.Text = cycle_category_data.get_catagory_id().ToString();
        }

        ////////////////////////////////////////////////////
        private void AdvancedSearch_EnabledChanged(object sender, EventArgs e)
        {
            //testing for the customer no
           // fillCustomerNo(Search_Customer.customerNoo);

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
                string  unitPrice = vehical_category_data.unitPrice;
                if (unitPrice == "0")
                {
                    MessageBox.Show("Unit price can't be zero! please change the price.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    vehical_category_data.unitPrice = "0.0";
                }
                else
                {
                    if (unitPrice != "0.0")
                    {
                        FillInvoiceGrid();
                        vehical_category_data.statusPass2Forms = false;
                    }
                }

            }

            if (cycle_category_data.statusPass2Forms == true)
            {
                string  unitPrice = cycle_category_data.unitPricee;
                if (unitPrice == "0")
                {
                    MessageBox.Show("Unit price can't be zero! please change the price.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cycle_category_data.unitPricee = "0.0";
                }
                else
                {
                    if (unitPrice != "0.0")
                    {
                        FillInvoiceGridForCycle();
                        cycle_category_data.statusPass2Forms = false;
                    }
                }

            }

            if (battery_category_data.statusPass2Forms == true)
            {
                string  unitPrice =battery_category_data.unitPrice;
                if (unitPrice == "0")
                {
                    MessageBox.Show("Unit price can't be zero! please change the price.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    battery_category_data.unitPrice = "0.0";
                }
                else
                {
                    if (unitPrice != "0.0")
                    {
                        FillInvoiceGridForBattry();
                        battery_category_data.statusPass2Forms = false;
                    }
                }
            }

            if (other_catagory_data.statusPass2Forms == true)
            {
                string  unitPrice = other_catagory_data.unitPrice;
                if (unitPrice == "0")
                {
                    MessageBox.Show("Unit price can't be zero! please change the price.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    other_catagory_data.unitPrice = "0.0";
                }
                else
                {
                    if (unitPrice != "0.0")
                    {
                        FillInvoiceGridForOther();
                        other_catagory_data.statusPass2Forms = false;
                    }
                }
            }
            if (tube_category_data.statusPass2Forms == true)
            {
                string unitPrice = tube_category_data.unitPrice;
                if (unitPrice == "0")
                {
                    MessageBox.Show("Unit price can't be zero! please change the price.","Message",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    tube_category_data.unitPrice = "0.0";
                }
                else
                {
                    if (unitPrice != "0.0")
                    {
                        FillInvoiceGridForTube();
                        tube_category_data.statusPass2Forms = false;
                    }
                }
            }



        }
        // OTHER 
        private void FillInvoiceGridForTube()
        {
            if (tube_category_data.invoiceQty != "")
            {
               /* DataGridViewRow NewRow = new DataGridViewRow();
                grdInvoice.Rows.Add(NewRow);
                int a = grdInvoice.RowCount;*/
                int catID = tube_category_data.get_battery_catagory_id();
                string brand = tube_category_data.brand;
                string size = tube_category_data.size;
                string qty = tube_category_data.invoiceQty;
                double unitPrice = Convert.ToDouble(tube_category_data.unitPrice);
                double linePrice = Convert.ToDouble(tube_category_data.unitPrice) * Convert.ToDouble(tube_category_data.invoiceQty);
                bool state = true;

                for (int i = 0; i < grdInvoice.RowCount;i++ )
                {
                    if(grdInvoice.Rows[i].Cells[1].Value.ToString() == (brand + "   /   " + size))
                    {
                       
                        double discount = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                        tube_category_data.unitPrice = grdInvoice.Rows[i].Cells[3].Value.ToString();
                        double availableQty = Convert.ToDouble(grdInvoice.Rows[i].Cells[2].Value);                        
                        grdInvoice.Rows[i].Cells[2].Value = (availableQty + Convert.ToDouble(qty));
                        linePrice = ((Convert.ToDouble(tube_category_data.unitPrice) * Convert.ToDouble((Convert.ToDouble(qty)))) / 100) * (100 - discount);
                        grdInvoice.Rows[i].Cells[4].Value = String.Format("{0:0.00##}", Convert.ToDouble(tube_category_data.unitPrice) * Convert.ToDouble((availableQty +Convert.ToDouble(qty))));
                       // grdInvoice.Rows[i].Cells[5].Value = discount;
                        grdInvoice.Refresh();

                        vehical_category_data.total = linePrice +vehical_category_data.total;
                        txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));
                        state = false;
                        break;
                    }
                    else
                    {       
                    }

                }
                if (state)
                {
                    DataGridViewRow NewRow = new DataGridViewRow();
                    grdInvoice.Rows.Add(NewRow);
                    int a = grdInvoice.RowCount;
                    grdInvoice.Rows[a - 1].Cells[0].Value = catID.ToString();
                    grdInvoice.Rows[a - 1].Cells[1].Value = brand + "   /   " + size;
                    grdInvoice.Rows[a - 1].Cells[1].ReadOnly = true;
                    grdInvoice.Rows[a - 1].Cells[2].Value = qty;
                    grdInvoice.Rows[a - 1].Cells[2].ReadOnly = true;
                    grdInvoice.Rows[a - 1].Cells[3].Value = String.Format("{0:0.00##}", unitPrice);
                    grdInvoice.Rows[a - 1].Cells[4].Value = String.Format("{0:0.00##}", linePrice);
                    grdInvoice.Rows[a - 1].Cells[5].Value = "0";
                    grdInvoice.Rows[a - 1].Cells[6].Value = "T";
                    vehical_category_data.total = linePrice + vehical_category_data.total;
                    txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));
                    
                }
                tube_category_data.statusPass2Forms = false;
            }

        }

        private void FillInvoiceGridForOther()
        {

            DataGridViewRow NewRow = new DataGridViewRow();
            grdInvoice.Rows.Add(NewRow);
            int a = grdInvoice.RowCount;
            string otherItemNo = other_catagory_data.itemno;
            string itemName = other_catagory_data.itemname;
            string qty = other_catagory_data.qty;
            double unitPrice = Convert.ToDouble(other_catagory_data.unitPrice);
            double linePrice = Convert.ToDouble(other_catagory_data.unitPrice) * Convert.ToDouble(other_catagory_data.qty);
            
      

            grdInvoice.Rows[a - 1].Cells[0].Value = otherItemNo;
            grdInvoice.Rows[a - 1].Cells[1].Value = itemName;
            grdInvoice.Rows[a - 1].Cells[1].ReadOnly = true;
            grdInvoice.Rows[a - 1].Cells[2].Value = qty;
            grdInvoice.Rows[a - 1].Cells[2].ReadOnly = true;
            grdInvoice.Rows[a - 1].Cells[3].Value = String.Format("{0:0.00##}", unitPrice);
            grdInvoice.Rows[a - 1].Cells[4].Value = String.Format("{0:0.00##}", linePrice);
            grdInvoice.Rows[a - 1].Cells[5].Value = "0";
            grdInvoice.Rows[a - 1].Cells[6].Value = "O";            
            vehical_category_data.total = linePrice + vehical_category_data.total;
            txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));

            other_catagory_data.statusPass2Forms = false;

       
        }


        // BATTERY
        private void FillInvoiceGridForBattry()
        {
           if (battery_category_data.invoiceQty != "")
            {
            /*DataGridViewRow NewRow = new DataGridViewRow();
            grdInvoice.Rows.Add(NewRow);
            int a = grdInvoice.RowCount;*/
            int catID = battery_category_data.get_battery_catagory_id();
            string brand = battery_category_data.brand;
            string size = battery_category_data.size;
            string voltage = battery_category_data.voltage;
            string amp = battery_category_data.amps;
            string qty = battery_category_data.invoiceQty;
            double unitPrice = Convert.ToDouble(battery_category_data.unitPrice);
            double linePrice = Convert.ToDouble(battery_category_data.unitPrice) * Convert.ToDouble(battery_category_data.invoiceQty);
            bool state = true;

            for (int i = 0; i < grdInvoice.RowCount; i++)
            {
                if (grdInvoice.Rows[i].Cells[1].Value.ToString() == (brand + "   /   " + voltage + "   /   " + amp))
                {
                    double discount = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                    battery_category_data.unitPrice = grdInvoice.Rows[i].Cells[3].Value.ToString();
                    double availableQty = Convert.ToDouble(grdInvoice.Rows[i].Cells[2].Value);
                    grdInvoice.Rows[i].Cells[2].Value = (availableQty + Convert.ToDouble(qty));
                    linePrice = ((Convert.ToDouble(battery_category_data.unitPrice) * Convert.ToDouble((Convert.ToDouble(qty)))) / 100) * (100 - discount);;
                    grdInvoice.Rows[i].Cells[4].Value = String.Format("{0:0.00##}", Convert.ToDouble(battery_category_data.unitPrice) * Convert.ToDouble((availableQty + Convert.ToDouble(qty))));
                    grdInvoice.Refresh();
                    vehical_category_data.total = linePrice + vehical_category_data.total;
                    txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));
                    state = false;
                    break;
                }
                else
                {
                }
            }


            if (state)
            {
                DataGridViewRow NewRow = new DataGridViewRow();
                grdInvoice.Rows.Add(NewRow);
                int a = grdInvoice.RowCount;
                grdInvoice.Rows[a - 1].Cells[0].Value = catID.ToString();
                grdInvoice.Rows[a - 1].Cells[1].Value = brand + "   /   " + voltage + "   /   " + amp;
                grdInvoice.Rows[a - 1].Cells[1].ReadOnly = true;
                grdInvoice.Rows[a - 1].Cells[2].Value = qty;
                grdInvoice.Rows[a - 1].Cells[2].ReadOnly = true;
                grdInvoice.Rows[a - 1].Cells[3].Value = String.Format("{0:0.00##}", unitPrice);
                grdInvoice.Rows[a - 1].Cells[4].Value = String.Format("{0:0.00##}", linePrice);
                grdInvoice.Rows[a - 1].Cells[5].Value = "0";
                grdInvoice.Rows[a - 1].Cells[6].Value = "B";                
                vehical_category_data.total = linePrice + vehical_category_data.total;
                txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));
            }
            battery_category_data.statusPass2Forms = false;     
            }
        }

        // MOTOR CYCLE
        private void FillInvoiceGridForCycle()
        {
            if (cycle_category_data.qty != "")
            {
                /*DataGridViewRow NewRow = new DataGridViewRow();
                grdInvoice.Rows.Add(NewRow);
                int a = grdInvoice.RowCount;*/
                int catID = cycle_category_data.get_catagory_id();
                string brand = cycle_category_data.brand;
                string size = cycle_category_data.size;
                string qty = cycle_category_data.qty;
                string plyRate = cycle_category_data.ply_rate;
                double unitPrice = Convert.ToDouble(cycle_category_data.unitPricee);
                double linePrice = Convert.ToDouble(cycle_category_data.unitPricee) * Convert.ToDouble(cycle_category_data.qty);
                bool state = true;

                for (int i = 0; i < grdInvoice.RowCount; i++)
                {
                    if (grdInvoice.Rows[i].Cells[1].Value.ToString() == (brand + "   /   " + size + "   /   " + plyRate))
                    {
                        double discount = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                        cycle_category_data.unitPricee = grdInvoice.Rows[i].Cells[3].Value.ToString();
                        double availableQty = Convert.ToDouble(grdInvoice.Rows[i].Cells[2].Value);
                        grdInvoice.Rows[i].Cells[2].Value = (availableQty + Convert.ToDouble(qty));
                        linePrice = ((Convert.ToDouble(cycle_category_data.unitPricee) * Convert.ToDouble((Convert.ToDouble(qty)))) / 100) * (100 - discount);;
                        grdInvoice.Rows[i].Cells[4].Value = String.Format("{0:0.00##}", Convert.ToDouble(cycle_category_data.unitPricee) * Convert.ToDouble((availableQty + Convert.ToDouble(qty))));
                        grdInvoice.Refresh();
                        vehical_category_data.total = linePrice + vehical_category_data.total;
                        txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));
                        state = false;
                        break;
                    }
                    else
                    {
                    }
                }

                if (state)
                {
                    DataGridViewRow NewRow = new DataGridViewRow();
                    grdInvoice.Rows.Add(NewRow);
                    int a = grdInvoice.RowCount;
                    grdInvoice.Rows[a - 1].Cells[0].Value = catID.ToString();
                    grdInvoice.Rows[a - 1].Cells[1].Value = brand + "   /   " + size + "   /   " + plyRate;
                    grdInvoice.Rows[a - 1].Cells[1].ReadOnly = true;
                    grdInvoice.Rows[a - 1].Cells[2].Value = qty;
                    grdInvoice.Rows[a - 1].Cells[2].ReadOnly = true;
                    grdInvoice.Rows[a - 1].Cells[3].Value = String.Format("{0:0.00##}", unitPrice);
                    grdInvoice.Rows[a - 1].Cells[4].Value = String.Format("{0:0.00##}", linePrice);
                    grdInvoice.Rows[a - 1].Cells[5].Value = "0";
                    grdInvoice.Rows[a - 1].Cells[6].Value = "C";
                    vehical_category_data.total = linePrice + vehical_category_data.total;
                    txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));
                }
                vehical_category_data.statusPass2Forms = false;
            }
        }

        // *********** function to fill tha INVOICE GRID VIEW ***************
        public void FillInvoiceGrid()
        {
            if (vehical_category_data.qty != "")
            {
                /*DataGridViewRow NewRow = new DataGridViewRow();
                grdInvoice.Rows.Add(NewRow);
                int a = grdInvoice.RowCount;*/
                int catID = vehical_category_data.get_catagory_id();
                string brand = vehical_category_data.brand;
                string size = vehical_category_data.size;
                string qty = vehical_category_data.qty;
                double unitPrice = Convert.ToDouble(vehical_category_data.unitPrice);
                string plyRate = vehical_category_data.ply_rate;
                double linePrice = Convert.ToDouble(vehical_category_data.unitPrice) * Convert.ToDouble(vehical_category_data.qty);
                bool state = true;

                for (int i = 0; i < grdInvoice.RowCount; i++)
                {
                    if (grdInvoice.Rows[i].Cells[1].Value.ToString() == (brand + "   /   " + size + "   /   " + plyRate))
                    {
                        double discount = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                        vehical_category_data.unitPrice = grdInvoice.Rows[i].Cells[3].Value.ToString();
                        double availableQty = Convert.ToDouble(grdInvoice.Rows[i].Cells[2].Value);
                        grdInvoice.Rows[i].Cells[2].Value = (availableQty + Convert.ToDouble(qty));
                        linePrice = ((Convert.ToDouble(vehical_category_data.unitPrice) * Convert.ToDouble((Convert.ToDouble(qty)))) / 100) * (100 - discount);;
                        grdInvoice.Rows[i].Cells[4].Value = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.unitPrice) * Convert.ToDouble((availableQty + Convert.ToDouble(qty))));
                        grdInvoice.Refresh();
                        vehical_category_data.total = linePrice + vehical_category_data.total;
                        txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));
                        state = false;
                        break;
                    }
                    else
                    {
                    }
                }
                if (state)
                {
                    DataGridViewRow NewRow = new DataGridViewRow();
                    grdInvoice.Rows.Add(NewRow);
                    int a = grdInvoice.RowCount;
                    grdInvoice.Rows[a - 1].Cells[0].Value = catID.ToString();
                    grdInvoice.Rows[a - 1].Cells[1].Value = brand + "   /   " + size + "   /   " + plyRate;
                    grdInvoice.Rows[a - 1].Cells[1].ReadOnly = true;
                    grdInvoice.Rows[a - 1].Cells[2].Value = qty;
                    grdInvoice.Rows[a - 1].Cells[2].ReadOnly = true;
                    grdInvoice.Rows[a - 1].Cells[3].Value = String.Format("{0:0.00##}", unitPrice);
                    grdInvoice.Rows[a - 1].Cells[4].Value = String.Format("{0:0.00##}", linePrice);
                    grdInvoice.Rows[a - 1].Cells[5].Value = "0";
                    grdInvoice.Rows[a - 1].Cells[6].Value = "V";                   
                    vehical_category_data.total = linePrice + vehical_category_data.total;
                    txtTotal.Text = String.Format("{0:0.00##}", Convert.ToDouble(vehical_category_data.total));
                }
                vehical_category_data.statusPass2Forms = false;           
            }         
        }
     

        private void btn_Search_cus_Click(object sender, EventArgs e)
        {
            //Search_Customer cs = new Search_Customer();

            //cs.Size = new System.Drawing.Size(559, 629);
            //cs.Location = new System.Drawing.Point(150, 5);
            //cs.Show();
           //Search_Customer c = new Search_Customer();

           // c.Vi sible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txtInvoiceNo.Text != "" && txtCustomerName.Text != "" && grdInvoice.RowCount != 0 && txtInvoiceNote.Text !="")//if customerNo is not null
                {
                    if (MessageBox.Show("Do you want to add invoice?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                      //  if (MessageBox.Show("Do you want to print invoice?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                       // {

                        setUserDetailsInGloble();
                        add();
                        clear_all();
                        vehical_category_data.total = 0;
                        this.Enabled = false;

                        GlobleAccess.openType = "I";
                        AddPayments a = new AddPayments();
                        //a.MdiParent = DHNAULA.ActiveForm;
                        a.Size = new System.Drawing.Size(1100, 670);
                        a.Location = new System.Drawing.Point(152, 30);
                        a.Show();

                           
                      
                          //  print();
                           // btnAddPayment.Visible = true;

                           // AddPayments a = new AddPayments();                           
                           // a.Size = new System.Drawing.Size(820, 400);
                            //a.Location = new System.Drawing.Point(330, 50);
                            //a.ShowDialog();

                          



                            //loadInvoiceNo();
                            
                       // }
                       // else
                       // {
                            /*setUserDetailsInGloble();
                            add();
                            clear_all();
                            vehical_category_data.total = 0;
                            this.Enabled = false;
                            CurrentInvoicePrint ci = new CurrentInvoicePrint();
                            ci.Show();
                            //btnAddPayment.Visible = true;

                            GlobleAccess.openType = "I";
                            AddPayments a = new AddPayments();
                           // a.MdiParent = DHNAULA.ActiveForm;
                            a.Size = new System.Drawing.Size(1100, 670);
                            a.Location = new System.Drawing.Point(152, 30);
                            a.ShowDialog();*/
                           // loadInvoiceNo();
                        //}

                    }
                    else
                    {
                        
                    }

                
                

            }
                else // if  invoice number Exists
                {
                    MessageBox.Show("Invalid operation!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            
           
        }

        private void add()
        {
            string invoiceNo = txtInvoiceNo.Text;
            string invoiceDate = dtpInvoiceDate.Value.Date.ToString("yyyy-MM-dd");
            string customerNo = com_customer_id.Text;
            double invoiceTotal = Convert.ToDouble(txtTotal.Text);
                //- Convert.ToDouble(txtDiscount.Text));
            //double invoiceDiscount = Convert.ToDouble(txtDiscount.Text);
            
            string invoiceNote = txtInvoiceNote.Text;
            string receivedBy = txt_ReceivedBy.Text;
           // double discount = Convert.ToDouble(txtDiscount.Text);
            double nettotal = Convert.ToDouble(txtTotal.Text);

            string vehicleNo = "";
            if(txtVehicleNo.Text == "")
                 vehicleNo = "-";
            else
                vehicleNo = txtVehicleNo.Text;

                        //insert into  invoice infomation invoice table 
            string q1 = "INSERT INTO invoice(invoiceno,invoicenote,invoicetotal,nettotal,invoicedate,customer,receivedby,vehicleno) VALUES ('" + invoiceNo + "','" + invoiceNote + "'," + invoiceTotal + "," + nettotal + ",'" + invoiceDate + "','" + customerNo + "','" + receivedBy + "','"+ vehicleNo +"')";
                        bool status1 = middle_access.db_access.InsertData(q1);

                        bool Status3 = invoiceItemsAdd();//to decrement values from db

                        if (status1 == true && Status3 == true) // if Invoice Data are successfully added into the Invoice table
                        {
                            for (int i = 0; i < grdInvoice.Rows.Count; i++)
                            {
                                //invoiceno is defined above
                                int invoiceLineNo = i;
                                

                                int quantity = Convert.ToInt32(grdInvoice.Rows[i].Cells[2].Value.ToString());
                                double retailPrice = Convert.ToDouble(grdInvoice.Rows[i].Cells[3].Value.ToString());
                                int itemNo = Convert.ToInt32(grdInvoice.Rows[i].Cells[0].Value.ToString());
                                string item_name = grdInvoice.Rows[i].Cells[1].Value.ToString();
                                string discount = " ";
                                if (grdInvoice.Rows[i].Cells[5].Value.ToString() != "0")
                                {
                                    discount = grdInvoice.Rows[i].Cells[5].Value.ToString();
                                }
                                string q2 = "INSERT INTO invoicelines(invoiceno,invoicelineno,soldqty,retailprice,itemno,itemname,discount) VALUES ('" + invoiceNo + "'," + invoiceLineNo + "," + quantity + "," + retailPrice + "," + itemNo + ",'" + item_name + "','" + discount + "')";

                                bool status2 = middle_access.db_access.InsertData(q2);
                                if (status2 != true)
                                {
                                    MessageBox.Show("Error in inserting Invoice lines !");
                                }
                            }

                        }


                       // if (status1 == true) // if data is insert
                        //    MessageBox.Show("Successfully Inserted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        
                        int nextInvNo = (Convert.ToInt32(invoiceNumber)) + 1;
                        string q = "UPDATE autoincrem SET maxno = " + nextInvNo + "  WHERE tablename = 'I'";
                        bool status = middle_access.db_access.UpdateData(q);
                        fill_advance_search_table();


                        for (int i = 0; i < grdInvoice.Rows.Count; i++)
                        {
                            double discount = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                            double discountedPrice = ((Convert.ToDouble(grdInvoice.Rows[i].Cells[2].Value) * Convert.ToDouble(grdInvoice.Rows[i].Cells[3].Value)) / 100) * (100 - discount);
                            string q3 = "";
                            if (discount == 0.00)
                            {
                                q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleno)VALUES('" + txtInvoiceNo.Text + "','" + dtpInvoiceDate.Value.Date.ToString("yyyy-MM-dd") + "','" + txtCustomerName.Text + "','" + grdInvoice.Rows[i].Cells[0].Value.ToString() + "','" + grdInvoice.Rows[i].Cells[1].Value.ToString() + "'," + grdInvoice.Rows[i].Cells[2].Value.ToString() + ",'" + grdInvoice.Rows[i].Cells[3].Value.ToString() + "','" + discountedPrice.ToString("0.00") + "','" + txtTotal.Text + "',' ','" + txtInvoiceNote.Text + "','"+ vehicleNo +"')";
                            }
                            else
                            {
                                q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleno)VALUES('" + txtInvoiceNo.Text + "','" + dtpInvoiceDate.Value.Date.ToString("yyyy-MM-dd") + "','" + txtCustomerName.Text + "','" + grdInvoice.Rows[i].Cells[0].Value.ToString() + "','" + grdInvoice.Rows[i].Cells[1].Value.ToString() + "'," + grdInvoice.Rows[i].Cells[2].Value.ToString() + ",'" + grdInvoice.Rows[i].Cells[3].Value.ToString() + "','" + discountedPrice.ToString("0.00") + "','" + txtTotal.Text + "','" + discount.ToString() + " %" + "','" + txtInvoiceNote.Text + "','" + vehicleNo + "')";
                            }
                            bool status3 = middle_access.db_access.InsertData(q3);
                            if (status3 != true)
                            {
                                MessageBox.Show("Error!");
                                string q4 = "DELETE FROM temp_invoice_print";
                                middle_access.db_access.DeleteData(q4);
                                break;
                            }
                        }
                    
        }

        private void clear_all()
        {
            txtCustomerName.Text = "";
            com_customer_id.Text = "";
            loadInvoiceNo();
       
            grdFillCustomer.Visible = false;
            
            dtpInvoiceDate.Value = DateTime.Today.Date;
            txtInvoiceNote.Text = "";
            txtVehicleNo.Text = "";
            txtTotal.Text = "0";
           // txtDiscount.Text = "0";
            grdInvoice.Rows.Clear();
            vehical_category_data.total = 0;
 

            
            
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        private bool invoiceItemsAdd()
        {
            for (int i = 0; i < grdInvoice.Rows.Count; i++)
            {


                //invoiceno is defined above
                int invoiceLineNo = i;
               
       
                int quantity = Convert.ToInt32(grdInvoice.Rows[i].Cells[2].Value.ToString());
                string itemCat = grdInvoice.Rows[i].Cells[6].Value.ToString();
                double retailPrice = Convert.ToDouble(grdInvoice.Rows[i].Cells[3].Value.ToString());
                int itemNo = Convert.ToInt32(grdInvoice.Rows[i].Cells[0].Value.ToString());

             //   grdInvoice.Rows[i].Cells[5].Value

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (grdInvoice.Rows[i].Cells[6].Value == "V")
                {

                    string q = "SELECT qty FROM add_vehical_tyre WHERE t_stok_id = " + itemNo + " ";
                    DataSet ds_battery_Oty = middle_access.db_access.SelectData(q);

                    string existingQty = ds_battery_Oty.Tables[0].Rows[0][0].ToString();
                    int newQty = Convert.ToInt32(existingQty) - quantity;

                    string q1 = "UPDATE add_vehical_tyre SET qty = " + newQty + "  WHERE t_stok_id = " + itemNo + " ";
                    middle_access.db_access.UpdateData(q1);
                
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (grdInvoice.Rows[i].Cells[6].Value == "C")
                {

                    string q = "SELECT qty FROM add_cycle_tyre WHERE t_stok_id = " + itemNo + " ";
                    DataSet ds_battery_Oty = middle_access.db_access.SelectData(q);

                    string existingQty = ds_battery_Oty.Tables[0].Rows[0][0].ToString();
                    int newQty = Convert.ToInt32(existingQty) - quantity;

                    string q1 = "UPDATE add_cycle_tyre SET qty = " + newQty + "  WHERE t_stok_id = " + itemNo + " ";
                    middle_access.db_access.UpdateData(q1);
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
                //batery done

                else if (grdInvoice.Rows[i].Cells[6].Value == "B") {
                    string q = "SELECT b_qty FROM add_battery WHERE b_stok_id = " + itemNo + " ";
                    DataSet ds_battery_Oty = middle_access.db_access.SelectData(q);

                    string existingQty = ds_battery_Oty.Tables[0].Rows[0][0].ToString();
                    int newQty = Convert.ToInt32(existingQty) - quantity;

                    string q1 = "UPDATE add_battery SET b_qty = " + newQty + "  WHERE b_stok_id = " + itemNo + " ";
                    middle_access.db_access.UpdateData(q1);
                
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                else if (grdInvoice.Rows[i].Cells[6].Value == "T")
                {
                    string q = "SELECT t_qty FROM tube_add WHERE t_stok_id = " + itemNo + " ";
                    DataSet ds_tube_Oty = middle_access.db_access.SelectData(q);

                    string existingQty = ds_tube_Oty.Tables[0].Rows[0][0].ToString();
                    int newQty = Convert.ToInt32(existingQty) - quantity;

                    string q1 = "UPDATE tube_add SET t_qty = " + newQty + "  WHERE t_stok_id = " + itemNo + " ";
                    middle_access.db_access.UpdateData(q1);
                
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                else  if (itemCat == "O") { }



                ///////////////////////////////////////////////////////////////////////////////////////////////////////////


            }
            return true;
        }



       //////////////////////////////////////////////////////////////////////////////////////////batery

        //for the form load
        public void loadBatery()
        {
            panel_batary_stock.Visible = false;
            // panel_add_new_category.Visible = false;
            //panel_settings.Visible = false;

            radGridView5.Visible = false;
            fill_com_brandBatery();
            fill_sizeBatery();
            fill_com_voltage();
            fill_com_amps();
            fill_com_amp();
             fill_add_battery_table();
           // fill_battery_search_table();


            radGridView5.Visible = false;
            com_search_brand.Enabled = false;
            txt_search_size.Enabled = false;
            groupBox3.Enabled = false;
            com_amp.Enabled = false;
            set_total_qty_batery();
        }



        public void set_total_qty_batery()
        {
            /*string q = null;
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
             * */
        }


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
            //  else
            //com_brand.DataSource = null; //fill table 
        }
        public void fill_sizeBatery()
        {

            string q1 = "SELECT battery_size FROM battery_size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                // radGridView4.DataSource = ds_size.Tables[0]; //fill table 
                radGridView5.DataSource = ds_size.Tables[0];


            }
            //  else
            //  radGridView4.DataSource = null; //fill table 
            radGridView5.DataSource = null;
        }

        public void fill_com_voltage()
        {
            string q1 = "SELECT * FROM battery_voltage";
            DataSet ds_voltage = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_voltage != null) // if data set is not null
            {
                

            }
            // else
            // com_voltage.DataSource = null; //fill table 
        }

        public void fill_com_amps()
        {
            string q1 = "SELECT * FROM battery_amps";
            DataSet ds_amps = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_amps != null) // if data set is not null
            {
                // com_amps.DataSource = ds_amps.Tables[0]; //fill table 
                // com_amps.DisplayMember = "battery_amps";
                // com_amps.ValueMember = "battery_amps";
                // com_amps.Text = "";


            }
            //else
            // com_voltage.DataSource = null; //fill table 
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
            //else
            //    com_voltage.DataSource = null; //fill table 
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
            // int battery_category_id = battery_category_data.get_battery_catagory_id();
            string q = "SELECT B.b_brand AS Brand, B.b_size AS Size, B.b_voltage AS Voltage, B.b_amps AS Amps, B.b_type AS Battery_Type FROM add_battery B,din D WHERE B.b_stok_id = D.din_id";
            DataSet ds_add_battery = middle_access.db_access.SelectData(q);
            if (ds_add_battery != null)
            {
                //   radGridView2.DataSource = ds_add_battery.Tables[0].DefaultView;

            }
            else
            {
                //  radGridView2.DataSource = null;

            }
        }

        public void fill_add_battery_table()
        {

            string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type FROM add_battery";
            DataSet ds_add_battery = middle_access.db_access.SelectData(q);
            if (ds_add_battery != null)
            {
                //   radGridView2.DataSource = ds_add_battery.Tables[0].DefaultView;

            }
            else
            {
                //  radGridView2.DataSource = null;

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
            fill_refrsh_batary();
        }

        private void fill_refrsh_batary()
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
        
        // battry  panel
        private void btnBattrySearch_Click_1(object sender, EventArgs e)
        {
            pnlTyre.Visible = false;
            panel_other_search.Visible = false;
            pnlTube.Visible = false;
            panel_batary_stock.Visible = true;
            lblInvoice.Text = "Invoice (Batery Search)";
            fill_refrsh_batary();
            /*if (panel_batary_stock.Visible = true)
            {
                btnBattrySearch.ForeColor = Color.Red;
                btnTyreSearch.ForeColor = Color.DimGray;
                btnOtherSearch.ForeColor = Color.DimGray;

            }*/
           
        }

        private void btnTyreSearch_Click(object sender, EventArgs e)
        {
            pnlTyre.Visible = true;
            panel_batary_stock.Visible = false;
            panel_other_search.Visible = false;
            pnlTube.Visible = false;
            
            lblInvoice.Text = "Invoice (Tyre Search)";
            string q = "SELECT * FROM customer";
            filter_customer_ID(q);
            txtCustomerName.Text = "Defult";
            com_customer_id.Text = "C 100";
            
            grdFillCustomer.Visible = false;
           /* if (pnlTyre.Visible = true)
            {
                btnTyreSearch.ForeColor = Color.Red;
                btnBattrySearch.ForeColor = Color.DimGray;
                //btnTyreSearch.ForeColor = Color.DimGray;
                btnOtherSearch.ForeColor = Color.DimGray;
                //btnTyreSearch.BackColor = Color.DarkOrange;
                
            }*/
            
        }

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

        private void chk_din_battery_Click(object sender, EventArgs e)
        {
            if (chk_din_battery.Checked != true)
            {
                fill_battery_search_table();
                battery_category_data.din_check_box_val = "DIN";
            }
            else
            {
                fill_din_battery_search_table();

                battery_category_data.din_check_box_val = "NORMAL";
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


        public void set_searched_qty_batery(string where_value)
        {
         
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


                battery_category_data.brand = brand;
                battery_category_data.size = size;
                battery_category_data.voltage = voltage;
                battery_category_data.amps = amp;
                battery_category_data.type = type;
                battery_category_data.qty = grdBattry.Rows[e.RowIndex].Cells[5].Value.ToString();
                battery_category_data.price = grdBattry.Rows[e.RowIndex].Cells[6].Value.ToString();



                this.Enabled = false;
                battery_qty q = new battery_qty();
                q.MdiParent = DHNAULA.ActiveForm;
                q.Visible = true;
            }
        }

        private void chk_size_batery_Click(object sender, EventArgs e)
        {
            if (chk_size_batery.Checked == false)
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

        private void radGridView5_SelectionChanged(object sender, EventArgs e)
        {

            row_index = radGridView5.CurrentRow.Index;

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
            pnlTube.Visible = false;
            pnlTyre.Visible = false;
            lblInvoice.Text = "Invoice (Other Search)";
            /*if (panel_other_search.Visible = true)
            {
                btnOtherSearch.ForeColor = Color.Red;
                btnBattrySearch.ForeColor = Color.DimGray;
                btnTyreSearch.ForeColor = Color.DimGray;
                //btnOtherSearch.ForeColor = Color.DimGray;
            }*/
  
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            clear_all();
            
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
                other_catagory_data.itemno = itemNo;
                other_catagory_data.itemname = itemName;
                other_catagory_data.catagory = itemCatagory;
                other_catagory_data.qty = itemQty;
                other_catagory_data.unitPrice = itemPrice;



                this.Enabled = false;
                other_qty q = new other_qty();
                q.Visible = true;
            }



            ////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void radButton6_Click_1(object sender, EventArgs e)
        {
            //not implemented yet
            pnlTyre.Visible = false;
            panel_batary_stock.Visible = false;
            panel_other_search.Visible = false;
            pnlTube.Visible = true;
            lblInvoice.Text = "Invoice (Tube Search)";
            
            fill_tube_search_table();
            fill_com_Tube_brand();
            fill_Tube_size();
            fill_com_Tube_make();

            grdTubeSize.Visible = false;
          //  com_search_brand.Enabled = false;
          //  txt_search_size.Enabled = false;
          //  groupBox2.Enabled = false;
          //  com_make.Enabled = false;
         //   set_total_qty();
           





            txt_barcode.Focus();
        }

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

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void radButton3_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            if (grdInvoice.RowCount < 1)
            {
                MessageBox.Show("Empty invoice!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (txtInvoiceNo.Text != "" && txtCustomerName.Text != "" && grdInvoice.RowCount != 0)//if customerNo is not null
                {
                    for (int i = 0; i < grdInvoice.Rows.Count; i++)
                    {
                        double discount = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                        double discountedPrice = ((Convert.ToDouble(grdInvoice.Rows[i].Cells[2].Value) * Convert.ToDouble(grdInvoice.Rows[i].Cells[3].Value)) / 100) * (100 - discount);
                        string vehicleNo = txtVehicleNo.Text;
                        string q3 = "";
                        if (discount == 0.00)
                        {
                            q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleno)VALUES('" + txtInvoiceNo.Text + "','" + dtpInvoiceDate.Value.Date.ToString("yyyy-MM-dd") + "','" + txtCustomerName.Text + "','" + grdInvoice.Rows[i].Cells[0].Value.ToString() + "','" + grdInvoice.Rows[i].Cells[1].Value.ToString() + "'," + grdInvoice.Rows[i].Cells[2].Value.ToString() + ",'" + grdInvoice.Rows[i].Cells[3].Value.ToString() + "','" + discountedPrice.ToString("0.00") + "','" + txtTotal.Text + "',' ','" + txtInvoiceNote.Text + "','" + vehicleNo + "')";
                        }
                        else
                        {
                            q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleno)VALUES('" + txtInvoiceNo.Text + "','" + dtpInvoiceDate.Value.Date.ToString("yyyy-MM-dd") + "','" + txtCustomerName.Text + "','" + grdInvoice.Rows[i].Cells[0].Value.ToString() + "','" + grdInvoice.Rows[i].Cells[1].Value.ToString() + "'," + grdInvoice.Rows[i].Cells[2].Value.ToString() + ",'" + grdInvoice.Rows[i].Cells[3].Value.ToString() + "','" + discountedPrice.ToString("0.00") + "','" + txtTotal.Text + "','" + discount.ToString() + " %" + "','" + txtInvoiceNote.Text + "','" + vehicleNo + "')";
                        }
                        bool status3 = middle_access.db_access.InsertData(q3);
                        if (status3 != true)
                        {
                            MessageBox.Show("Error!");
                            string q4 = "DELETE FROM temp_invoice_print";
                            middle_access.db_access.DeleteData(q4);
                            break;
                        }
                    }




                    this.Enabled = false;
                    CurrentInvoicePrint ci = new CurrentInvoicePrint();
                    ci.Show();
                    print();
                }
                else
                {
                    MessageBox.Show("Invalid operation!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void print()
        {         
            // Allow the user to select a printer.
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {               
                printer.print.SendFileToPrinter(pd.PrinterSettings.PrinterName, @"..\..\Report2.rdlc");
            }
        }

        private void lblTyreSearch_Click(object sender, EventArgs e)
        {

        }

        private void radTextBox1_TextChanged(object sender, EventArgs e)
        {
            string name = txtCustomerName.Text;
            filter_customer_name(name);
        }

        private void filter_customer_name(string name)
        {
            string q1 = "SELECT customername FROM customer WHERE customername  LIKE  '" + name + "%' ";
            ds_name = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_name != null) // if data set is not null
            {
                grdFillCustomer.Visible = false;
                grdFillCustomer.Visible = true;
                grdFillCustomer.DataSource = ds_name.Tables[0];


            }
            else
            {
                grdFillCustomer.DataSource = null;
                grdFillCustomer.Visible = false;
            }

        }

        private void grdFillCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomerName.Text = grdFillCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCustomerName.Focus();
            grdFillCustomer.Visible = false;
            string q = "SELECT * FROM customer WHERE customername='" + txtCustomerName.Text + "'";
            filter_customer_ID(q);
        }

        private void filter_customer_ID(string q)
        {
            string q1 = q;
            ds_ID = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_ID != null) // if data set is not null
            {
                com_customer_id.DataSource = ds_ID.Tables[0]; //fill table 
                com_customer_id.DisplayMember = "customerno";
                com_customer_id.ValueMember = "customerno";
               


            }
            else
            {
                com_customer_id.DataSource = null;
            }
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

        private void com_customer_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customer_ID = com_customer_id.Text;
            string q = "SELECT customername FROM customer WHERE customerno='" + customer_ID + "'";
            ds_name = middle_access.db_access.SelectData(q);
            if (ds_name != null)
            {
                DataRow dr_name = ds_name.Tables[0].Rows[0];
                txtCustomerName.Text = dr_name.ItemArray.GetValue(0).ToString();
            }
            grdFillCustomer.Visible = false;
        }

        private void txtCustomerName_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtCustomerName_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotal.Text = txtTotal.Text.ToString();
       
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //  txtFinalTotal.Text = Convert.ToDouble(txtTotal.Text. - txtDiscount.Text.ToString());
            
            //if (txtDiscount.Text != null)
            //{
          /*  try
            {
                if (txtDiscount.Text =="")
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
                    txtFinalTotal.Text = (price - discount).ToString()+".00"; 
                }


            }
            catch
            {

            }*/          
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            //if (Application.OpenForms["AddPayments"] == null)
            //{
                
            //    //to fill add payments interface
            //    AddPayments a = new AddPayments(txtCustomerName.Text,com_customer_id.Text);
            //    a.MdiParent = DHNAULA.ActiveForm;
            //    a.Size = new System.Drawing.Size(1100, 670);
            //    a.Location = new System.Drawing.Point(152, 30);
            //    a.Show();

            //}
        }

        private void setUserDetailsInGloble()
        {
            GlobleAccess.openType = "I";
            GlobleAccess.customerNo = com_customer_id.Text;
            GlobleAccess.customerName = txtCustomerName.Text;
            GlobleAccess.amount = txtTotal.Text;
            GlobleAccess.invoiceNo = txtInvoiceNo.Text;
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
                catch (Exception k)
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

        private void radCheckBox1_Click(object sender, EventArgs e)
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

        private void radCheckBox3_Click(object sender, EventArgs e)
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

        private void radCheckBox2_Click_1(object sender, EventArgs e)
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

        private void radCheckBox4_Click(object sender, EventArgs e)
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

      

        private void grdTubeSize_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txtTubeSize.Text = radGridView5.Rows[row_index].Cells[0].Value.ToString();
            txtTubeSize.Focus();
            grdTubeSize.Visible = false;
        }

        private void txtTubeSize_Click(object sender, EventArgs e)
        {
            string q1 = "SELECT t_size FROM tube_size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {
                grdTubeSize.DataSource = ds_size.Tables[0];
                grdTubeSize.Visible = true;
            }
            else
            {
                grdTubeSize.DataSource = null; //fill table 
            }
        }

        private void grdTubeSize_CellClick_1(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txtTubeSize.Text = grdTubeSize.Rows[row_index].Cells[0].Value.ToString();
            grdTubeSize.Focus();
            grdTubeSize.Visible = false;
        }

        private void txtTubeSize_Click_1(object sender, EventArgs e)
        {
            fill_Tube_size();
            grdTubeSize.Visible = true;
        }

        private void txtTubeSize_TextChanged_1(object sender, EventArgs e)
        {
            string text_text = txtTubeSize.Text;
            filter_text_box_text_size_search(text_text);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           // grdTubeSize.Visible = false;
        }

       

      
        private void grdTubeSize_SelectionChanged_1(object sender, EventArgs e)
        {
            row_index = grdTubeSize.CurrentRow.Index;
        }

        private void grdTubeSize_CellClick_3(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txtTubeSize.Text = grdTubeSize.Rows[row_index].Cells[0].Value.ToString();
            txtTubeSize.Focus();
            grdTubeSize.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
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

        private void radButton3_Click_2(object sender, EventArgs e)
        {
            fill_tube_search_table();
        }

        private void radButton7_Click_1(object sender, EventArgs e)
        {
            search_normal_tube();
        }



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

        private void pnlTyre_Paint(object sender, PaintEventArgs e)
        {

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


        int pre = 0;
      
      

        private void grdInvoice_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            
            /*  int val = e.RowIndex;
            int r_count = grdInvoice.RowCount;
            string Item_catogory = null;
            if (val >= 0)
            {
                Item_catogory = grdInvoice.Rows[e.RowIndex].Cells[5].Value.ToString();

            }
            if (Item_catogory == "V")
                FillInvoiceGrid();
            else if (Item_catogory == "C")
                FillInvoiceGridForCycle();
            else if (Item_catogory == "B")
                FillInvoiceGridForBattry();
            else if (Item_catogory == "O")
                FillInvoiceGridForOther();
            else if (Item_catogory == "T")
                FillInvoiceGridForTube();*/
        }

        private void grdInvoice_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            
            if (val >= 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grdInvoice.Rows[val].Delete();
                    double total = 0.0f;
                    txtTotal.Clear();
                    txtTotal.Clear();
                    int r_count = grdInvoice.RowCount;
                    for (int i = 0; i < r_count; i++)
                    {
                        double qty = Convert.ToDouble(grdInvoice.Rows[i].Cells[2].Value);
                        double unitPrice = Convert.ToDouble((grdInvoice.Rows[i].Cells[3].Value));

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

        private void grdInvoice_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 2)
            {
                double qty = Convert.ToDouble(grdInvoice.Rows[e.RowIndex].Cells[2].Value);
                double unitPrice = Convert.ToDouble(grdInvoice.Rows[e.RowIndex].Cells[3].Value);
                double linePrice = (qty * unitPrice);
                grdInvoice.Rows[e.RowIndex].Cells[4].Value = "";
                grdInvoice.Rows[e.RowIndex].Cells[4].Value = String.Format("{0:0.00##}", linePrice);
                grdInvoice.Rows[e.RowIndex].Cells[3].Value = String.Format("{0:0.00##}", unitPrice);
                grdInvoice.Refresh();


                double total = 0;
                for (int i = 0; i < grdInvoice.RowCount; i++)
                {
                    double price = Convert.ToDouble(grdInvoice.Rows[i].Cells[4].Value);
                    double percentage = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                    if (percentage == 0)
                    {
                        total += price;
                    }
                    else
                    {
                        total += price * ((100 - percentage) / 100);

                    }

                }
                vehical_category_data.total = total;
                txtTotal.Text = String.Format("{0:0.00##}", total);
                
            }


            if (e.ColumnIndex == 5)
            {
                if (Convert.ToDouble(grdInvoice.Rows[e.RowIndex].Cells[5].Value) < 0 || Convert.ToDouble(grdInvoice.Rows[e.RowIndex].Cells[5].Value) > 100)
                {
                    // MessageBox.Show("Invaied discount value!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    grdInvoice.Rows[e.RowIndex].Cells[5].Value = "0";

                }
                else
                {
                    double qty = Convert.ToDouble(grdInvoice.Rows[e.RowIndex].Cells[2].Value);
                    double unitPrice = Convert.ToDouble(grdInvoice.Rows[e.RowIndex].Cells[3].Value);
                    double linePrice = (qty * unitPrice);
                    grdInvoice.Rows[e.RowIndex].Cells[4].Value = "";
                    grdInvoice.Rows[e.RowIndex].Cells[4].Value = String.Format("{0:0.00##}", linePrice);
                    grdInvoice.Rows[e.RowIndex].Cells[3].Value = String.Format("{0:0.00##}", unitPrice);
                    grdInvoice.Refresh();


                    double total = 0;
                    for (int i = 0; i < grdInvoice.RowCount; i++)
                    {
                        double price = Convert.ToDouble(grdInvoice.Rows[i].Cells[4].Value);
                        double percentage = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                        if (percentage == 0)
                        {
                            total += price;
                        }
                        else
                        {
                            total += price * ((100 - percentage) / 100);

                        }

                    }
                    vehical_category_data.total = total;
                    txtTotal.Text = String.Format("{0:0.00##}", total);
                }
            }
        }

        private void grdInvoice_UserDeletedRow(object sender, Telerik.WinControls.UI.GridViewRowEventArgs e)
        {   

                double total = 0;
                for (int i = 0; i < grdInvoice.RowCount; i++)
                {
                    double price = Convert.ToDouble(grdInvoice.Rows[i].Cells[4].Value);
                    double percentage = Convert.ToDouble(grdInvoice.Rows[i].Cells[5].Value);
                    if (percentage == 0)
                    {
                        total += price;
                    }
                    else
                    {
                        total += price * ((100 - percentage) / 100);

                    }

                }
                vehical_category_data.total = total;
                txtTotal.Text = String.Format("{0:0.00##}", total);
          
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGridViewRow NewRow = new DataGridViewRow();
            grdInvoice.Rows.Add(NewRow);

            int index = grdInvoice.RowCount-1;
            grdInvoice.Rows[index].Cells[0].Value = index+1;
            grdInvoice.Rows[index].Cells[1].Value="-";            
            grdInvoice.Rows[index].Cells[2].Value = "1";
            grdInvoice.Rows[index].Cells[3].Value = "0.00";
            grdInvoice.Rows[index].Cells[4].Value = "0.00";
            grdInvoice.Rows[index].Cells[5].Value = "0";
            grdInvoice.Rows[index].Cells[6].Value = "O";
          
          
        }
      

    }
}
    