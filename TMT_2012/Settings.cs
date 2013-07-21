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
    public partial class frmSettings : Telerik.WinControls.UI.RadForm
    {

        Point moveStart;

        string cell_brand_name;
        string cell_size_name; 
        string cell_ply_rate;
        string cell_make;    

        string changed_brand;
        string changed_size;
        string changed_ply_rate;
        string changed_make; 
        int brand_id;
        int row_index;

        int brand_id_x;
        string thread_pattern_x;


        /// <summary>
        /// Initializes a new instance of the <see cref="frmSettings"/> class.
        /// </summary>
        public frmSettings()
        {
            InitializeComponent();
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void radButton10_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;  
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
         
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

            fill_brand_table();
            
           
        }

        public void fill_brand_table()// This method is use to fill brand name table.
        {
            string q1 = "SELECT B.brand_name AS Currently_Available_Brands FROM brand B";
            DataSet ds_brand_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_brand_table != null) // if data set is not null
            {

                radGridView2.DataSource = ds_brand_table.Tables[0].DefaultView; //fill table 
            }

            else
                radGridView2.DataSource = null;

        }

        public void fill_size_table()// This method is use to fill tyer size table.
        {
            string q1 = "SELECT S.tyer_size AS Currently_Available_Sizes FROM size S";
            DataSet ds_size_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_size_table != null) // if data set is not null
            {

                radGridView3.DataSource = ds_size_table.Tables[0]; //fill table 
            }

            else
                radGridView3.DataSource = null;

        }

        public void fill_thread_pattern()// This method is use to fill tyer size table.
        {
            string q1 = "SELECT TP.pattern_name AS Thread_Pattern, B.brand_name AS Brand FROM thread_pattern TP,brand B WHERE B.brand_id = TP.brand_id";
            DataSet ds_size_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_size_table != null) // if data set is not null
            {

                radGridView4.DataSource = ds_size_table.Tables[0].DefaultView; //fill table 
            }

            else
                radGridView4.DataSource = null;

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

        public void fill_ply_rate_table()
        {
            string q1 = "SELECT PL.rate AS Ply_Rates FROM ply_rate PL";
            DataSet ds_ply_arte_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_ply_arte_table != null) // if data set is not null
            {

                radGridView1.DataSource = ds_ply_arte_table.Tables[0].DefaultView; //fill table 
            }

            else
                radGridView1.DataSource = null;

        }

        /// <summary>
        /// Fill_make_tables this instance.
        /// </summary>
        public void fill_make_table()
        {
            string q1 = "SELECT M.made_by  AS Manufactured FROM make M";
            DataSet ds_make_table = middle_access.db_access.SelectData(q1); // fill data set with brands which stoed in brand table 
            if (ds_make_table != null) // if data set is not null
            {
                radGridView5.DataSource = ds_make_table.Tables[0].DefaultView; //fill table 
               
            }

            else
                radGridView5.DataSource = null;
           
        }


        private void btn_add_brand_Click(object sender, EventArgs e)
        {
            string brand = txt_brand.Text;
            if (txt_brand.Text != "") // if brand name text box is not empty
            {
                string q1 = "SELECT * FROM brand B WHERE B.brand_name = '"+ brand +"' ";
                DataSet ds_brand_table = middle_access.db_access.SelectData(q1);

                if(ds_brand_table == null)
                    {
                        string q2 = "INSERT INTO brand(brand_name)  VALUES ('"+ brand +"')";
                        bool status = middle_access.db_access.InsertData(q2);
                        if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        fill_brand_table();// fill brand name table.
                        int row_count = radGridView2.RowCount;
                        this.radGridView2.Rows[row_count - 1].IsCurrent = true;
                        txt_brand.Text = "";
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
                MessageBox.Show("Enter Brand Name!","Message",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

     
        private void radGridView2_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
          int val =  e.RowIndex;
          if (val >= 0)
          {
              cell_brand_name = radGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();//get cell value to the cell_brand_name variable
              txt_brand.Text = cell_brand_name;
              row_index = e.RowIndex;
          }
          else
          {

          }
            
        }

        private void tabItem1_Click(object sender, EventArgs e)
        {
            fill_brand_table();
        }

        private void radButton6_Click_1(object sender, EventArgs e)
        {
            if (txt_brand.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM brand B WHERE B.brand_name = '" + changed_brand + "' ";
                    DataSet ds_brand_table = middle_access.db_access.SelectData(q1);

                    if (ds_brand_table == null)
                    {
                        string q2 = "UPDATE brand SET brand_name ='" + txt_brand.Text + "' WHERE brand_name = '" + cell_brand_name + "'  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_brand_table();
                            this.radGridView2.Rows[row_index].IsCurrent = true;

                            txt_brand.Text = "";

                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_brand.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Brand name was not changed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_brand.Text = "";
                    }
                }
                else
                {
                    txt_brand.Text = "";
                }
            }
            else
                MessageBox.Show("Select a brand first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                
        }
              

        private void txt_brand_TextChanged(object sender, EventArgs e)
        {
            changed_brand = txt_brand.Text; 
        }

        private void radButton11_Click(object sender, EventArgs e)
        {
            if (txt_brand.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM brand B WHERE B.brand_name = '" + txt_brand.Text + "' ";
                    DataSet ds_brand_table = middle_access.db_access.SelectData(q1);

                    if (ds_brand_table != null)
                    {
                        string q2 = "DELETE FROM brand WHERE brand_name = '" + txt_brand.Text + "'  ";
                        bool status = middle_access.db_access.DeleteData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_brand_table();
                            txt_brand.Text = "";
                        }

                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_brand.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Brand name was not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_brand.Text = "";
                    }
                }
                else
                {
                    txt_brand.Text = "";
                }

            }
            else
                MessageBox.Show("Select a brand first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void tabItem1_ContentPanel_Paint(object sender, PaintEventArgs e)
        {
            fill_brand_table();
            txt_brand.Focus();
        }

        private void tabItem2_ContentPanel_Paint(object sender, PaintEventArgs e)
        {
            fill_size_table();
            txt_size.Focus();
        }

        private void btn_size_Click(object sender, EventArgs e)
        {
           
                string size = (txt_size.Text);
                if (txt_size.Text != "") // if size text box is not empty
                {
                    string q1 = "SELECT * FROM size S WHERE S.tyer_size = '" + size.Trim() + "' ";
                    DataSet ds_size_table = middle_access.db_access.SelectData(q1);

                    if (ds_size_table == null)
                    {
                        string q2 = "INSERT INTO size(tyer_size)  VALUES('" + size + "')";
                        bool status = middle_access.db_access.InsertData(q2);
                        if (status == true) // if data is insert
                        {
                            MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                            fill_size_table();// fill size name table.
                            int row_count = radGridView3.RowCount;
                            this.radGridView3.Rows[row_count - 1].IsCurrent = true;
                            txt_size.Text = "";
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
                    MessageBox.Show("Enter size!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
           
        }
        

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_size.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM size S WHERE S.tyer_size = '" + txt_size.Text + "' ";
                    DataSet ds_size_table = middle_access.db_access.SelectData(q1);

                    if (ds_size_table != null)
                    {
                        string q2 = "DELETE FROM size WHERE tyer_size = '" + txt_size.Text + "'  ";
                        bool status = middle_access.db_access.DeleteData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_size_table();
                            txt_size.Text = "";
                        }

                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_size.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Size was not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_size.Text = "";
                    }
                }
                else
                {
                    txt_size.Text = "";
                }

            }
            else
                MessageBox.Show("Select size!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radGridView3_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
                txt_size.Text = radGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();//get cell value to the cell_brand_name variable
                cell_size_name = txt_size.Text;
                row_index = e.RowIndex;
            }
            else
            {

            }
        }

        private void tabItem3_ContentPanel_Paint(object sender, PaintEventArgs e)
        {

            fill_thread_pattern();
            fill_drp_brand();
            com_brand_name.Text = "";
            txt_thread_pattern.Text = "";
            txt_thread_pattern.Focus();
            
            
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

        private void radButton13_Click(object sender, EventArgs e)
        {
            string thread_pattern = txt_thread_pattern.Text;

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
                string q1 = "SELECT * FROM thread_pattern TP,brand B  WHERE B.brand_name = '" + com_brand_name.Text + "' AND TP.pattern_name = '"+ txt_thread_pattern.Text +"' AND B.brand_id = TP.brand_id";
                DataSet ds_thread_pattern = middle_access.db_access.SelectData(q1);
                if (ds_thread_pattern == null )
                {
                    string q2 = "INSERT INTO thread_pattern(pattern_name,brand_id)  VALUES('" + thread_pattern + "'," + brand_id + ")";
                    bool status = middle_access.db_access.InsertData(q2);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        txt_thread_pattern.Focus();
                        fill_thread_pattern();
                        int row_count = radGridView4.RowCount;
                        this.radGridView4.Rows[row_count - 1].IsCurrent = true;
                        txt_thread_pattern.Text = "";
                    }
                    else
                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                                  

            }

        }

        private void radGridView4_Click(object sender, EventArgs e)
        {

        }

        private void radGridView4_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
                txt_thread_pattern.Text = radGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();//get cell value to the cell_brand_name variable
                com_brand_name.Text = radGridView4.Rows[e.RowIndex].Cells[1].Value.ToString();
                thread_pattern_x = radGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();//set globle variable
                row_index = e.RowIndex;
                string q1 = "SELECT B.brand_id FROM brand B  WHERE brand_name = '" + com_brand_name.Text + "'";
                DataSet ds_brand_id = middle_access.db_access.SelectData(q1);
                if (ds_brand_id != null)
                {
                    DataRow row_brand_id = ds_brand_id.Tables[0].Rows[0];
                    brand_id_x = (int)row_brand_id.ItemArray.GetValue(0);//set globle variable
                }
            }
            else
            {

            }
        }

        private void radButton2_Click_1(object sender, EventArgs e)
        {
            if (txt_thread_pattern.Text == "")
            {
                MessageBox.Show("Enter Thread pattern!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(com_brand_name.Text == "")
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


                        string q1 = "SELECT * FROM thread_pattern TP WHERE TP.brand_id = '" + brand_id_selected + "' AND TP.pattern_name = '" + txt_thread_pattern.Text + "' ";
                        DataSet ds_brand_table = middle_access.db_access.SelectData(q1);

                        if (ds_brand_table == null)
                        {
                            string q2 = "UPDATE thread_pattern SET brand_id = " + brand_id_selected + ", pattern_name = '"+ txt_thread_pattern.Text +"' WHERE brand_id = "+ brand_id_x +" AND pattern_name = '" + thread_pattern_x + "'  ";
                            bool status = middle_access.db_access.UpdateData(q2);
                            if (status == true)
                            {
                                MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                fill_thread_pattern();
                                this.radGridView4.Rows[row_index].IsCurrent = true;

                                txt_thread_pattern.Text = "";
                                com_brand_name.Text = "";
                            }

                            else
                            {

                                MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txt_thread_pattern.Text = "";
                                com_brand_name.Text = "";

                            }
                        }

                        else
                        {
                            MessageBox.Show("Information was not change!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txt_brand.Text = "";
                        }
                    }
                }
                else
                {
                    txt_brand.Text = "";
                }
            }
            

        }

    

        private void com_brand_name_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void radButton3_Click(object sender, EventArgs e)
        {
            if (txt_thread_pattern.Text == "")
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


                        string q1 = "SELECT * FROM thread_pattern TP WHERE TP.brand_id = '" + brand_id_selected + "' AND TP.pattern_name = '" + txt_thread_pattern.Text + "' ";
                        DataSet ds_thread_pattern_table = middle_access.db_access.SelectData(q1);

                    if (ds_thread_pattern_table != null)
                    {
                        string q2 = "DELETE FROM thread_pattern  WHERE brand_id = '" + brand_id_selected + "' AND pattern_name = '" + txt_thread_pattern.Text + "' ";
                        bool status = middle_access.db_access.DeleteData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_thread_pattern();
                            txt_thread_pattern.Text = "";
                            com_brand_name.Text = "";
                        }

                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_thread_pattern.Text = "";
                            com_brand_name.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Thread pattern was not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_thread_pattern.Text = "";
                        com_brand_name.Text = "";
                    }
                    }
                }
                
                
                else
                {
                    txt_thread_pattern.Text = "";
                    com_brand_name.Text = "";
                }

            }
            
        }

        private void tabItem4_ContentPanel_Paint(object sender, PaintEventArgs e)
        {
            fill_ply_rate_table();
            txt_ply_rate.Focus();
        }

        private void radButton7_Click(object sender, EventArgs e)
        {

                string ply_rate = txt_ply_rate.Text;
                if (txt_ply_rate.Text != "") // if ply rate text box is not empty
                {
                    string q1 = "SELECT * FROM ply_rate PL WHERE PL.rate = '" + ply_rate + "' ";
                    DataSet ds_ply_rate_table = middle_access.db_access.SelectData(q1);

                    if (ds_ply_rate_table == null)// if entered ply rate is not in data base
                    {
                        string q2 = "INSERT INTO ply_rate(rate)  VALUES('" + ply_rate + "')";
                        bool status = middle_access.db_access.InsertData(q2);
                        if (status == true) // if data is insert
                        {
                            MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                            fill_ply_rate_table();// fill ply rate table.
                            int row_count = radGridView1.RowCount;
                            this.radGridView1.Rows[row_count - 1].IsCurrent = true;
                            txt_ply_rate.Text = "";
                        }
                        else // if data is not insert
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_ply_rate.Text = "";
                        }
                    }
                    else
                        MessageBox.Show("Already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // if ply rate text box is empty
                    MessageBox.Show("Enter rate!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

        private void radGridView5_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
               cell_ply_rate = (radGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());//get cell value to the cell_brand_name variable
               txt_ply_rate.Text  = cell_ply_rate;
            }
            else
            {

            }
           

        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            if (txt_ply_rate.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM ply_rate PL WHERE PL.rate = '" + txt_ply_rate.Text + "' ";
                    DataSet ds_ply_rate_table = middle_access.db_access.SelectData(q1);

                    if (ds_ply_rate_table != null)
                    {
                        string q2 = "DELETE FROM ply_rate WHERE rate = '" + txt_ply_rate.Text + "'  ";
                        bool status = middle_access.db_access.DeleteData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_ply_rate_table();
                            txt_ply_rate.Text = "";
                        }

                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_ply_rate.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("ply rate was not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_ply_rate.Text = "";
                    }
                }
                else
                {
                    txt_ply_rate.Text = "";
                }
            }



        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
                cell_ply_rate = (radGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());//get cell value to the cell_brand_name variable
                txt_ply_rate.Text = cell_ply_rate.ToString();
                row_index = e.RowIndex;
            }
            else
            {

            }
        }

        private void radButton14_Click(object sender, EventArgs e)
        {
            string make = txt_make.Text;
            if (txt_make.Text != "") // if make text box is not empty
            {
                string q1 = "SELECT * FROM make M WHERE M.made_by = '" + make + "' ";
                DataSet ds_make_table = middle_access.db_access.SelectData(q1);

                if (ds_make_table == null)
                {
                    string q2 = "INSERT INTO make(made_by)  VALUES('" + make + "')";
                    bool status = middle_access.db_access.InsertData(q2);
                    if (status == true) // if data is insert
                    {
                        MessageBox.Show("OK!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);//show message box with ok button
                        fill_make_table();// fill make table.
                        int row_count = radGridView5.RowCount;
                        this.radGridView5.Rows[row_count - 1].IsCurrent = true;
                        txt_make.Text = "";
                    }
                    else // if data is not insert
                    {

                        MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Already exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // if make text box is empty
                MessageBox.Show("Enter Brand Name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tabItem5_ContentPanel_Paint(object sender, PaintEventArgs e)
        {
            fill_make_table();
            txt_make.Focus();
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            if (txt_make.Text != "")
            {
                if (MessageBox.Show("Do you want to delete ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM make M WHERE M.made_by = '" + txt_make.Text + "' ";
                    DataSet ds_make_table = middle_access.db_access.SelectData(q1);

                    if (ds_make_table != null)
                    {
                        string q2 = "DELETE FROM make WHERE made_by = '" + txt_make.Text + "'  ";
                        bool status = middle_access.db_access.DeleteData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_make_table();
                            txt_make.Text = "";
                        }

                        else
                        {
                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_make.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Make was not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_make.Text = "";
                    }
                }
                else
                {
                    txt_make.Text = "";
                }

            }
            else
                MessageBox.Show("Select a make first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void radGridView5_CellClick_1(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int val = e.RowIndex;
            if (val >= 0)
            {
                txt_make.Text = radGridView5.Rows[e.RowIndex].Cells[0].Value.ToString();//get cell value to the cell_brand_name variable
                cell_make = txt_make.Text;
                row_index = e.RowIndex;
            }
            else
            {

            }
        }

        /// <summary>
        /// Get_back_ups this instance.
        /// </summary>
        /// <returns></returns>
        public bool get_back_up()
        {         
           
            saveFileDialog1.Title = ("Set Back Up Path");
            saveFileDialog1.InitialDirectory = "C:\\";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "All File|*.*";
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;
            if (path != "")
            {               
                    bak_up.Backup("root", "123", "localhost", "accountingsystem", path);
                    return true;             
            }
            else
            {
                return false;
            }
      
        }

        /// <summary>
        /// Restors this instance.
        /// </summary>
        /// <returns></returns>
        public bool restor()
        {
            openFileDialog1.Title = ("Restor");
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "SQL File|*.sql";
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            if (path != "")
            {
                bak_up.restor("root", "123", "localhost", "accountingsystem", path);
                return true;
            }
            else
            {
                return false;
            }

        }

        private void radButton8_Click(object sender, EventArgs e)
        {
                       
            bool status = get_back_up();
            if(status == true)
            {
                MessageBox.Show("Backuping Process Completed Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void radButton9_Click(object sender, EventArgs e)
        {
            bool status = restor();
            if(status == true)
            {
                MessageBox.Show("Restoring Process Completed Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void frmSettings_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moveStart = new Point(e.X, e.Y);
            }
        }

        private void frmSettings_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Point deltaPos = new Point(e.X - moveStart.X, e.Y - moveStart.Y);
                this.Location = new Point(this.Location.X + deltaPos.X,
                  this.Location.Y + deltaPos.Y);
            }
        }

        private void tabItem6_ContentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radButton12_Click(object sender, EventArgs e)
        {
            if (txt_size.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM size S WHERE S.tyer_size = '" + changed_size + "' ";
                    DataSet ds_size_table = middle_access.db_access.SelectData(q1);

                    if (ds_size_table == null)
                    {
                        string q2 = "UPDATE size SET tyer_size ='" + txt_size.Text + "' WHERE tyer_size = '" + cell_size_name + "'  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_size_table();
                            this.radGridView3.Rows[row_index].IsCurrent = true;

                            txt_size.Text = "";
                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_size.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Size was not changed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_size.Text = "";
                    }
                }
                else
                {
                    txt_size.Text = "";
                }
            }
            else
                MessageBox.Show("Select a size first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txt_size_TextChanged(object sender, EventArgs e)
        {
            changed_size = txt_size.Text;
        }

        private void radButton15_Click(object sender, EventArgs e)
        {
            if (txt_ply_rate.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM ply_rate PR WHERE PR.rate = '" + changed_ply_rate + "' ";
                    DataSet ds_ply_rate_table = middle_access.db_access.SelectData(q1);

                    if (ds_ply_rate_table == null)
                    {
                        string q2 = "UPDATE ply_rate SET rate ='" + txt_ply_rate.Text + "' WHERE rate = '" + cell_ply_rate + "'  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_ply_rate_table();
                            this.radGridView1.Rows[row_index].IsCurrent = true;
                            txt_ply_rate.Text = "";
                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_ply_rate.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Ply rate was not changed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_ply_rate.Text = "";
                    }
                }
                else
                {
                    txt_ply_rate.Text = "";
                }
            }
            else
                MessageBox.Show("Select a Ply rate first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txt_ply_rate_TextChanged(object sender, EventArgs e)
        {
            changed_ply_rate = txt_ply_rate.Text;
        }

        private void radButton16_Click(object sender, EventArgs e)
        {
            if (txt_make.Text != "")
            {
                if (MessageBox.Show("Do you want to update ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string q1 = "SELECT * FROM make M WHERE M.made_by = '" + changed_make + "' ";
                    DataSet ds_make_table = middle_access.db_access.SelectData(q1);

                    if (ds_make_table == null)
                    {
                        string q2 = "UPDATE make SET made_by ='" + txt_make.Text + "' WHERE made_by = '" + cell_make + "'  ";
                        bool status = middle_access.db_access.UpdateData(q2);
                        if (status == true)
                        {
                            MessageBox.Show("Successfully updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fill_make_table();
                            this.radGridView5.Rows[row_index].IsCurrent = true;
                            txt_brand.Text = "";

                        }

                        else
                        {

                            MessageBox.Show("Error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_brand.Text = "";

                        }
                    }

                    else
                    {
                        MessageBox.Show("Brand name was not changed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_brand.Text = "";
                    }
                }
                else
                {
                    txt_brand.Text = "";
                }
            }
            else
                MessageBox.Show("Select a brand first!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void txt_make_TextChanged(object sender, EventArgs e)
        {
            changed_make = txt_make.Text;
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void radButton1_Click_2(object sender, EventArgs e)
        {
            //report_vehical_history h = new report_vehical_history();
            //h.Show();

        }

        private void radButton17_Click(object sender, EventArgs e)
        {
            //report_cycle_history c = new report_cycle_history();
            //c.Show();
        }

        private void radButton18_Click(object sender, EventArgs e)
        {
            //report_battery_history B = new report_battery_history();
            //B.Show();
        }

        private void radButton1_Click_3(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;  
        }

 
        }
       

     
    }

