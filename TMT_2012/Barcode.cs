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
    public partial class Barcode : Form
    {
        DataSet ds_size;
        int row_index;
        string type;
        string refresh_tube = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode"/> class.
        /// </summary>
        public Barcode()
        {
            InitializeComponent();
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

        private void Barcode_Load(object sender, EventArgs e)
        {
            radioTube.Checked = true;
            if (radioTube.Checked)
            {
                fill_com_brand();
                fill_size();              
                fill_com_make();                
                fill_tube_search_table();


                radGridView5.Visible = false;
                com_search_brand.Enabled = false;
                txt_search_size.Enabled = false;
                groupBox2.Enabled = false;
                com_make.Enabled = false;
                
               
            }
            radButton3.Focus();
        }


        public void fill_com_make()
        {
            string q1 = "SELECT * FROM tube_amps";
            DataSet ds_amps = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_amps != null) // if data set is not null
            {
                

                com_make.DataSource = ds_amps.Tables[0]; //fill table 
                com_make.DisplayMember = "tube_amps";
                com_make.ValueMember = "tube_amps";
                com_make.Text = "";


            }
            else
                com_make.DataSource = null; //fill table 
        }



        public void fill_size()
        {

            string q1 = "SELECT t_size FROM tube_size";
            DataSet ds_size = middle_access.db_access.SelectData(q1); // fill data set with sizes which stoed in size table 
            if (ds_size != null) // if data set is not null
            {

                radGridView5.DataSource = ds_size.Tables[0]; //fill table 
                //radGridView5.DataSource = ds_size.Tables[0];


            }
            else
                radGridView5.DataSource = null; //fill table 
            //radGridView5.DataSource = null;
        }


        public void fill_com_brand()
        {
            string q1 = "SELECT * FROM tube_brand ";
            DataSet ds_brand = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand != null) // if data set is not null
            {
                com_search_brand.DataSource = ds_brand.Tables[0]; //fill table 
                com_search_brand.DisplayMember = "t_brand";
                com_search_brand.ValueMember = "t_brand";
                com_search_brand.Text = "";

            }
            else
                com_search_brand.DataSource = null; //fill table 
        }

        public void fill_tube_search_table()
        {
                string q = "SELECT t_brand AS Brand, t_size AS Size, t_amps AS Make, t_type AS Tube_Type FROM tube_add";
                DataSet ds_barcodes = middle_access.db_access.SelectData(q);
                if (ds_barcodes != null)
                {
                    grdBarcodeDetails.DataSource = ds_barcodes.Tables[0].DefaultView;
                }
                else
                {
                    grdBarcodeDetails.DataSource = null;
                }
            
            
               
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            fill_tube_search_table();
            /*string q = "SELECT t_stok_id,t_brand AS Brand, t_size AS Size, t_amps AS Make, t_type AS Tube_Type, t_qty AS Qty, t_prize AS Price FROM tube_add";
            DataSet ds_tube = middle_access.db_access.SelectData(q);
            if (ds_tube != null)
            {
                foreach (DataRow dr_tube in ds_tube.Tables[0].Rows)
                {
                    string id = dr_tube.ItemArray.GetValue(0).ToString();
                    char[] array = id.ToCharArray();
                    int no_of_array = 3 + array.Length;
                    int zeros = 9 - no_of_array;
                    string barcordID = "TUB";
                    for (int i = 0; i < zeros; i++)
                    {
                        barcordID += "0";
                    }


                    barcordID = barcordID + id;
                    string brand = dr_tube.ItemArray.GetValue(1).ToString();
                    string size = dr_tube.ItemArray.GetValue(2).ToString();
                    string make = dr_tube.ItemArray.GetValue(3).ToString();
                    string type = dr_tube.ItemArray.GetValue(4).ToString();

                   // string q2 = "DELETE FROM barcodeinformation";
                   // bool status1 = middle_access.db_access.InsertData(q2);
                    string q1 = "INSERT INTO barcodeinformation(BID,Brand,Size,Make,Type)VALUES('" + barcordID + "','" + brand + "','" + size + "','" + make + "','" + type + "')";
                    bool status2 = middle_access.db_access.InsertData(q1);

                }
                fill_tube_search_table();

            }*/
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

        private void chk_amp_Click(object sender, EventArgs e)
        {
            if (chk_amp.Checked == false)
            {
                com_make.Enabled = true;
            }
            else
            {
                com_make.Enabled = false;
                com_make.Text = "";
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

        

        private void txt_search_size_TextChanged(object sender, EventArgs e)
        {
            string text_text = txt_search_size.Text;
            filter_text_box_text_size_search(text_text);
        }

        public void filter_text_box_text_size_search(string text_text)
        {
            string q1 = "SELECT t_size FROM tube_size WHERE tube_size  LIKE  '" + text_text + "%' ";
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

        private void radGridView5_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            txt_search_size.Text = radGridView5.Rows[row_index].Cells[0].Value.ToString();
            txt_search_size.Focus();
            radGridView5.Visible = false;
        }

        private void radGridView5_SelectionChanged(object sender, EventArgs e)
        {
            row_index = radGridView5.CurrentRow.Index;
        }

        private void txt_search_size_Enter(object sender, EventArgs e)
        {
            string q1 = "SELECT t_size FROM tube_size";
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

        private void rad_M_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_M.Checked == true)
            {
                type = "Vehicle";
            }
            if (rad_MF.Checked == true)
            {
                type = "Cycle";
            }
        }

        private void rad_MF_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_M.Checked == true)
            {
                type = "Vehicle";
            }
            if (rad_MF.Checked == true)
            {
                type = "Cycle";
            }
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            if (chk_din_tube.Checked != true)
            {
                search_normal_tube();
            }
            else
            {
                // search_din_tube();
            }
        }

        public void search_normal_tube()
        {
            string qq = set_search_query();
            string qqq = set_amp();
            if (qqq == null)
            {
                refresh_tube = qq;
                set_searched_qty(qq);
                if (qq == null)
                {

                }
                else
                {

                    string q = "SELECT t_brand AS Brand, t_size AS Size, t_voltage AS Voltage, t_amps AS Amps, t_type AS Tube_Type, t_qty AS Qty, t_prize AS Price FROM tube_add WHERE " + qq + "";
                    DataSet ds_advance_search = middle_access.db_access.SelectData(q);
                    if (ds_advance_search != null)
                    {
                        grdBarcodeDetails.DataSource = ds_advance_search.Tables[0].DefaultView;
                    }
                    else
                    {
                        grdBarcodeDetails.DataSource = null;
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
                set_searched_qty(q1);
                if (q1 == null)
                {

                }
                else
                {

                    string q = "SELECT t_brand AS Brand, t_size AS Size, t_voltage AS Voltage, t_amps AS Amps, t_type AS Tube_Type, t_qty AS Qty, t_prize AS Price FROM tube_add WHERE " + q1 + "";
                    DataSet ds_advance_search = middle_access.db_access.SelectData(q);
                    if (ds_advance_search != null)
                    {
                        grdBarcodeDetails.DataSource = ds_advance_search.Tables[0].DefaultView;
                    }
                    else
                    {
                        grdBarcodeDetails.DataSource = null;
                    }
                }
            }

        }

        public string set_search_query()
        {

            string brand = com_search_brand.Text;
            string size = txt_search_size.Text;
            string tube_type = type;
            string qq = null;

            if (chk_brand.Checked == true)
            {
                qq = " t_brand ='" + brand + "' ";
                if (chk_size.Checked == true)
                {
                    qq = qq + "AND" + " t_size ='" + size + "' ";
                    if (chk_type.Checked == true)
                    {
                        qq = qq + "AND" + " t_type ='" + tube_type + "' ";
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (chk_type.Checked == true)
                    {
                        qq = qq + "AND" + " t_type ='" + tube_type + "' ";
                    }
                }
            }
            else
            {
                if (chk_size.Checked == true)
                {
                    qq = " t_size ='" + size + "' ";
                    if (chk_type.Checked == true)
                    {
                        qq = qq + "AND" + " t_type ='" + tube_type + "' ";
                    }
                }
                else
                {
                    if (chk_type.Checked == true)
                    {
                        qq = " t_type ='" + tube_type + "' ";
                    }
                }
            }


            return qq;
        }


        public string set_amp()
        {
            string amp = com_make.Text;
            string qq = null;
            if (chk_amp.Checked == true)
            {
                qq = "t_amps = '" + amp + "'";
            }

            else
            {

            }

            return qq;
        }


        public void set_searched_qty(string where_value)
        {
            string q = null;
            string value = where_value;
            if (value == null)
            {
              //  lbl_search_qty.Text = "0";
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

        private void grdBarcodeDetails_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (MessageBox.Show("Do you want to add this item?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int val = e.RowIndex;
                int r_count = grdBarcodeDetails.RowCount;
                string barcordID = "TUB";
                if (val >= 0)
                {
                    tube_category_data.brand = grdBarcodeDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                    tube_category_data.size = grdBarcodeDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                    tube_category_data.amps = grdBarcodeDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                    tube_category_data.type = grdBarcodeDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string id = tube_category_data.get_battery_catagory_id().ToString();
                    char[] array = id.ToCharArray();
                    int no_of_array = 3 + array.Length;
                    int zeros = 9 - no_of_array;                    
                    for (int i = 0; i < zeros; i++)
                    {
                        barcordID += "0";
                    }
                    barcordID = barcordID + id;

                }              
                  

                
                DataGridViewRow NewRow = new DataGridViewRow();               
                grdbarcodeinfor.Rows.Add(NewRow);
                int a = grdbarcodeinfor.RowCount;
                if (a == 1)
                {
                    grdbarcodeinfor.Rows[a - 1].Cells[0].Value = barcordID.ToString();
                }
                else
                {
                    for (int i = 0; i < a-1; i++)
                    {
                        string value = grdbarcodeinfor.Rows[i].Cells[0].Value.ToString();
                        if (value != barcordID)
                        {
                            grdbarcodeinfor.Rows[a - 1].Cells[0].Value = barcordID.ToString();
                        }
                        else
                        {
                            grdbarcodeinfor.Rows[a - 1].Delete();
                        }
                    }
                }
               
                    


               
                
            }
        }

        private void grdbarcodeinfor_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
                int val = e.RowIndex;                
                if (val >= 0)
                {
                    if (MessageBox.Show("Do you want delete this item?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        grdbarcodeinfor.Rows[val].Delete();
                    }
                }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            int val = grdbarcodeinfor.RowCount;       
            for (int i = 0; i < val; i++)
            {
                string code = grdbarcodeinfor.Rows[i].Cells[0].Value.ToString();
                string q1 = "INSERT INTO barcodeinformation(BID)VALUES('" + code + "')";
                bool status2 = middle_access.db_access.InsertData(q1);
            }

            this.Enabled = false;
            BarcodeReport b = new BarcodeReport();
            b.MdiParent = DHNAULA.ActiveForm;
            b.Show();
          
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            grdbarcodeinfor.Rows.Clear();
        }


    }
}
