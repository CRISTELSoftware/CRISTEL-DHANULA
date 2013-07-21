using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Linq.Expressions;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Xml;

namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CurrentInvoicePrint : Form
    {
        public static string paymenCount = "0";

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentInvoicePrint"/> class.
        /// </summary>
        public CurrentInvoicePrint()
        {
            InitializeComponent();
        }

        private void CurrentInvoicePrint_Load(object sender, EventArgs e)
        {
            ReportDataSource ds = new ReportDataSource();            
            ds.Name = "DataSet1";            
            ds.Value = GenerateData();

            ReportDataSource ds1 = new ReportDataSource();
            ds1.Name = "DataSet2";
            ds1.Value = GeneratePaymentData();


           // string s_path = System.Windows.Forms.Application.StartupPath.Substring(0, System.Windows.Forms.Application.StartupPath.Substring(0, System.Windows.Forms.Application.StartupPath.Substring(0, System.Windows.Forms.Application.StartupPath.LastIndexOf("\\")).LastIndexOf("\\")).LastIndexOf("\\"));
                        
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath =@"Report2.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(ds);
            this.reportViewer1.LocalReport.DataSources.Add(ds1);

            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 0;
           // pg.Margins.Bottom = 1;
           // pg.Margins.Left = 1;
           // pg.Margins.Right = 1;
           
            //System.Drawing.Printing.PaperSize size = new PaperSize();
            //size.RawKind = (int)PaperKind.A5;
            //pg.PaperSize = size;
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();
        }

        private static DataTable GenerateData()
        {
            string q1 = "SELECT `invoice_no`, `invoice_date`, `customer_name`, `item_no`, `item_name`, `qty`, `unit_price`, `price`, `total_price`, `discount`,`refNo`,`vehicleno` FROM temp_invoice_print";
            DataSet ds = middle_access.db_access.SelectData(q1);

            DataTable Table = new DataTable();
            if (ds != null)
            {
                Table = ds.Tables[0];
            }
            return Table;
        }

        private static DataTable GeneratePaymentData()
        {
            string q1 = "SELECT paymentMethodTxt,enteredAmount FROM addpaymentsaccount WHERE invoiceNo='" + GlobleAccess.invoiceNo + "'";
            DataSet ds = middle_access.db_access.SelectData(q1);

            DataTable Table = new DataTable();
            if (ds!=null)
            {
                Table = ds.Tables[0];
            }
            return Table;
        }

        private void CurrentInvoicePrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            string q4 = "DELETE  FROM temp_invoice_print";
            middle_access.db_access.DeleteData(q4);
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Invoice"] != null)
            {
                Form f = (Form)Application.OpenForms["Invoice"];
                f.Enabled = true;
                this.Close();
            }
            else if (Application.OpenForms["CustomerSection"] != null)
            {
                Form f = (Form)Application.OpenForms["CustomerSection"];
                f.Enabled = true;
                this.Close();
            }
            else if (Application.OpenForms["AddPayments"] != null)
            {
                Form f = (Form)Application.OpenForms["AddPayments"];
                f.Enabled = true;
                this.Close();
            }
            else
            {
                this.Close();
            }
           
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //PrintDialog pd = new PrintDialog();
            //pd.PrinterSettings = new PrinterSettings();
           
            //if (DialogResult.OK == pd.ShowDialog(this))
            //{     
               
            //      printer.print.SendFileToPrinter(pd.PrinterSettings.PrinterName, @".\Report1.rdlc");
            //}
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            
        }
        private void print(PrintPageEventArgs ev)
        {
            
        }
    }
}
