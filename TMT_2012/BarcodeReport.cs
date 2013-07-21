using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Xml;

namespace TMT_2012
{
    public partial class BarcodeReport : Form
    {
        public BarcodeReport()
        {
            InitializeComponent();
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            Form f = (Form)Application.OpenForms["Barcode"];
            f.Enabled = true;
            this.Close();
        }

        private void BarcodeReport_Load(object sender, EventArgs e)
        {

         
            ReportDataSource ds = new ReportDataSource();
            // The dataset name must be the same as the report's dataset
            ds.Name = "DataSet1";
            ds.Value =GenerateData();
 
           
          
           
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";      
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(ds);           
            this.reportViewer1.LocalReport.Refresh();
           
            
          
           
            // TODO: This line of code loads data into the 'accountingsystemDataSet1.barcodeinformation' table. You can move, or remove it, as needed.
         //  this.barcodeinformationTableAdapter.Fill(this.accountingsystemDataSet1.barcodeinformation);
        //    this.reportViewer1.RefreshReport();
        }

        private static DataTable GenerateData()
        {
            string q1 = "SELECT BID FROM barcodeinformation";          
            DataSet ds = middle_access.db_access.SelectData(q1);


            DataTable table = new DataTable();
            table = ds.Tables[0];
            return table;
        }

     
        private void BarcodeReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            string q = "DELETE FROM barcodeinformation";
            middle_access.db_access.DeleteData(q);

        }

      

        
    }
}
