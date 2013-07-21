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
    public partial class DHNAULA : Form
    {
        private int childFormNumber = 0;
        //int max;
        //Point moveStart;




        /// <summary>
        /// Initializes a new instance of the <see cref="DHNAULA"/> class.
        /// </summary>
        public DHNAULA()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void btnAddCusMdi_Click(object sender, EventArgs e)
        {
            Add_New_Customer addCustomer = new Add_New_Customer();
            addCustomer.MdiParent = this;
            addCustomer.Show();

        }

        private void btnAddCusMDI_Click_1(object sender, EventArgs e)
        {
        //    Add_New_Customer addCustomer = new Add_New_Customer();
        //    addCustomer.MdiParent = this;
        //    addCustomer.Show();
        }

        private void ribCustomer_Click(object sender, EventArgs e)
        {
            Add_New_Customer addCustomer = new Add_New_Customer();
            addCustomer.MdiParent = this;
            addCustomer.Show();
        }

        private void radRibbonBarGroup2_Click(object sender, EventArgs e)
        {
            Search_Customer srchCustomer = new Search_Customer();
            srchCustomer.MdiParent = this;
            srchCustomer.Show();
        }

        private void radRibbonBarGroup1_Click(object sender, EventArgs e)
        {
           Invoice inv = new Invoice();
            inv.MdiParent = this;
            inv.Show();
        }      
       
    

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["SearchCustomerGrid"] == null)
            {
                SearchCustomerGrid cs = new SearchCustomerGrid();
                cs.MdiParent = this;
                cs.Size = new System.Drawing.Size(760, 415);
                cs.Location = new System.Drawing.Point(150, 5);
                cs.Show();
            }
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
           /* toolStripProgressBar1.Increment(+1);

            if (toolStripProgressBar1.Value == max)
            {
                timer1.Enabled = false;

                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Maximum = max;
                toolStripProgressBar1.Minimum = 0;
                
               
            }
            */
        }
        // Advance Search Btn
      

        private void btnSettings_Click(object sender, EventArgs e)
        {
           frmSettings set = new frmSettings();
            set.MdiParent = this;
            set.Size = new System.Drawing.Size(657, 543);
            set.Location = new System.Drawing.Point(150, 5);
           set.Show();
        }

        private void btnMdiAddCustomer_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Add_New_Customer"] == null)
            {
                Add_New_Customer adcus = new Add_New_Customer();
                adcus.MdiParent = this;
                adcus.Size = new System.Drawing.Size(525, 620);
                adcus.Location = new System.Drawing.Point(150, 5);
                adcus.Show();
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AddNewStock"] == null)
            {
                AddNewStock adcus = new AddNewStock();
                adcus.MdiParent = this;
                adcus.Size = new System.Drawing.Size(1004, 564);
                adcus.Location = new System.Drawing.Point(150, 5);
                adcus.Show();
            }

        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AddMoterCycleTyre"] == null)
            {
                AddMoterCycleTyre adcus = new AddMoterCycleTyre();
                adcus.MdiParent = this;
                adcus.Size = new System.Drawing.Size(1004, 564);
                adcus.Location = new System.Drawing.Point(150, 5);
                adcus.Show();
            }
        }

       

        private void radButton9_Click(object sender, EventArgs e)
        {
          
        }

        private void btnVehReorder_Click(object sender, EventArgs e)
        {
            if (pnlAlert.Visible == false)
            {
                pnlAlert.Visible = true;
                lblRoder.Text = "Vehile Tyre Reorder";
                grdAlertGrid.DataSource = null;
                string q = "SELECT t_brand AS Brand, t_size AS Size, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_type AS Type,t_tube AS Tube_Type,qty AS Qty FROM add_vehical_tyre HAVING Qty < 4";// WHERE " + qq + " ";
                DataSet ds_alert_vehicleTyre = middle_access.db_access.SelectData(q);
                if (ds_alert_vehicleTyre != null)
                {
                    grdAlertGrid.DataSource = ds_alert_vehicleTyre.Tables[0].DefaultView;
                }
                else
                    MessageBox.Show("No Items Remainig Low Qantity Level", "message", MessageBoxButtons.OK);

            }
            else 
            {
                lblRoder.Text = "Vehile Tyre Reorder";
                grdAlertGrid.DataSource = null;
                string q = "SELECT t_brand AS Brand, t_size AS Size, t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_type AS Type,t_tube AS Tube_Type,qty AS Qty FROM add_vehical_tyre HAVING Qty < 4";// WHERE " + qq + " ";
                DataSet ds_alert_vehicleTyre = middle_access.db_access.SelectData(q);
                if (ds_alert_vehicleTyre != null)
                {
                    grdAlertGrid.DataSource = ds_alert_vehicleTyre.Tables[0].DefaultView;
                }
                else
                    MessageBox.Show("No Items Remainig Low Qantity Level", "message", MessageBoxButtons.OK);              
            }
        }

        // butten Click motercycle Stock Reorder
        private void radButton11_Click(object sender, EventArgs e)
        {
            if (pnlAlert.Visible == false)
            {
                pnlAlert.Visible = true;
                lblRoder.Text = "Motercycle Tyre Reorder";
                grdAlertGrid.DataSource = null;
                string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side , t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_tyre_pattern AS Tyre_Type ,t_tube AS Tube_Type,qty AS Qty FROM add_cycle_tyre  HAVING Qty < 4";
                DataSet ds_alert_MotercycleTyre = middle_access.db_access.SelectData(q);
                if (ds_alert_MotercycleTyre != null)
                {
                    grdAlertGrid.DataSource = ds_alert_MotercycleTyre.Tables[0].DefaultView;
                }
                else
                    MessageBox.Show("No Items Remainig Low Qantity Level", "message", MessageBoxButtons.OK);

            }
            else
            {
                lblRoder.Text = "Motercycle Tyre Reorder";
                grdAlertGrid.DataSource = null;
                string q = "SELECT t_brand AS Brand, t_size AS Size,t_side AS Side , t_ply_rate AS Ply_rate, t_thread_pattern AS Thread_Pattern,t_make AS Make,t_tyre_pattern AS Tyre_Type ,t_tube AS Tube_Type,qty AS Qty FROM add_cycle_tyre  HAVING qty  < 4";
                DataSet ds_alert_MotercycleTyre = middle_access.db_access.SelectData(q);
                if (ds_alert_MotercycleTyre != null)
                {
                    grdAlertGrid.DataSource = ds_alert_MotercycleTyre.Tables[0].DefaultView;
                }
                else
                    MessageBox.Show("No Items Remainig Low Qantity Level", "message", MessageBoxButtons.OK);
            }
        }
        
        // reoder battery btnclick
        private void radButton12_Click(object sender, EventArgs e)
        {
            if (pnlAlert.Visible == false)
            {
                pnlAlert.Visible = true;
                lblRoder.Text = "Battery Reorder";
                grdAlertGrid.DataSource = null;
                string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type,b_qty AS Battary_qty FROM add_battery HAVING b_qty< 4";
                DataSet ds_alert_Battry = middle_access.db_access.SelectData(q);
                if (ds_alert_Battry != null)
                {
                    grdAlertGrid.DataSource = ds_alert_Battry.Tables[0].DefaultView;
                }

                else
                {
                    MessageBox.Show("No Items Remainig Low Qantity Level", "message", MessageBoxButtons.OK);
                }

            }
            else
            {
                lblRoder.Text = "Battery Reorder";
                grdAlertGrid.DataSource = null;
                string q = "SELECT b_brand AS Brand, b_size AS Size, b_voltage AS Voltage, b_amps AS Amps, b_type AS Battery_Type,b_qty as Battary_qty FROM add_battery HAVING b_qty< 4";
                DataSet ds_alert_Battry = middle_access.db_access.SelectData(q);
                if (ds_alert_Battry != null)
                {
                    grdAlertGrid.DataSource = ds_alert_Battry.Tables[0].DefaultView;
                }
                else
                    MessageBox.Show("No Items Remainig Low Qantity Level", "message", MessageBoxButtons.OK);
            }
          
        }

        private void radButton14_Click(object sender, EventArgs e)
        {            
            grdAlertGrid.DataSource = null;           
            pnlAlert.Visible =false;
        }     
      

    

    

        private void radButton2_Click_1(object sender, EventArgs e)
        {
            if (Application.OpenForms["managePayments"] == null)
            {
                managePayments mp = new managePayments();
                mp.MdiParent = this;
                mp.Size = new System.Drawing.Size(820, 400);
                mp.Location = new System.Drawing.Point(330, 50);
                mp.Show();
            }
        }

    


        private void DHNAULA_Load(object sender, EventArgs e)
        {
            
        }

        private void DHNAULA_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AdvancedSearch"] == null)
            {
                CustomerSection c = new CustomerSection();
                c.MdiParent = this;
                c.Size = new System.Drawing.Size(1100, 670);
                c.Location = new System.Drawing.Point(5, 5);
                c.Show();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AdvancedSearch"] == null)
            {
                TyreSection adv = new TyreSection();
                adv.MdiParent = this;
                adv.Size = new System.Drawing.Size(1100, 670);
                adv.Location = new System.Drawing.Point(5, 5);
                adv.Show();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["batary"] == null)
            {
                batary bt = new batary();
                bt.MdiParent = this;
                bt.Size = new System.Drawing.Size(1100, 670);
                bt.Location = new System.Drawing.Point(5, 5);
                bt.Show();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Tube"] == null)
            {
                Tube bt = new Tube();
                bt.MdiParent = this;
                bt.Size = new System.Drawing.Size(1100, 690);
                bt.Location = new System.Drawing.Point(5, 5);
                bt.Show();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // toolStripProgressBar1.Maximum = max;
            // timer1.Start();
            //   timer1.Enabled = true;
            if (Application.OpenForms["Billing"] != null)
            {
                MessageBox.Show("Please close the Billing interface!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Application.OpenForms["Invoice"] == null)
            {
                Invoice inv = new Invoice();
                inv.MdiParent = this;
                inv.Size = new System.Drawing.Size(1100, 690);
                inv.Location = new System.Drawing.Point(5, 5);
                inv.Show();
            }

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Invoice"] != null)
            {
                MessageBox.Show("Please close the invoice!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Application.OpenForms["Billing"] == null)
            {
                Billing bill = new Billing();
                bill.MdiParent = this;
                bill.Size = new System.Drawing.Size(1100, 670);
                bill.Location = new System.Drawing.Point(5, 5);
                bill.Show();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AddPayments"] == null)
            {
                GlobleAccess.openType = "M";
                AddPayments a = new AddPayments();
                a.MdiParent = this;
                a.Size = new System.Drawing.Size(1100, 1000);
                a.Location = new System.Drawing.Point(5, 5);
                a.Show();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["BackUps"] == null)
            {
                BackUps b = new BackUps();
                b.MdiParent = this;
                b.Location = new System.Drawing.Point(400, 200);
                b.Show();
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Barcode"] == null)
            {
                Barcode b = new Barcode();
                b.MdiParent = this;
                b.Size = new System.Drawing.Size(1100, 670);
                b.Location = new System.Drawing.Point(5, 5);
                b.Show();
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pnlAlert.Visible = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_CustomerSection.ForeColor = Color.Yellow;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            lbl_CustomerSection.ForeColor = Color.White;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_TyreSection.ForeColor = Color.Yellow;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            lbl_TyreSection.ForeColor = Color.White;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_BatterySection.ForeColor = Color.Yellow;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            lbl_BatterySection.ForeColor = Color.White;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_TubeSection.ForeColor = Color.Yellow;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            lbl_TubeSection.ForeColor = Color.White;
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_Invoice.ForeColor = Color.Lime;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            lbl_Invoice.ForeColor = Color.White;
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_Bill.ForeColor = Color.Lime;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
              lbl_Bill.ForeColor = Color.White;
        
        }

        private void pictureBox7_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_Payments.ForeColor = Color.Lime;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            lbl_Payments.ForeColor = Color.White;
        }

        private void pictureBox8_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_Backups.ForeColor = Color.Magenta;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            lbl_Backups.ForeColor = Color.White;
        }

        private void pictureBox9_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_BarcodeReport.ForeColor = Color.Magenta;
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            lbl_BarcodeReport.ForeColor = Color.White;
        }

        private void pictureBox10_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_ReorderInformation.ForeColor = Color.Magenta;
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            lbl_ReorderInformation.ForeColor = Color.White;
        }

     
    }
}
