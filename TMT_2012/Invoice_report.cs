using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Invoice_report : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Invoice_report"/> class.
        /// </summary>
        public Invoice_report()
        {
            InitializeComponent();
        }

        private void Invoice_report_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'accountingsystemDataSet_invoice_report.temp_invoice_print' table. You can move, or remove it, as needed.
            //this.temp_invoice_printTableAdapter.Fill(this.accountingsystemDataSet_invoice_report.temp_invoice_print);
            // TODO: This line of code loads data into the 'accountingsystemDataSet_currentInvoice.temp_invoice_print' table. You can move, or remove it, as needed.
          //  this.temp_invoice_printTableAdapter.Fill(this.accountingsystemDataSet_currentInvoice.temp_invoice_print);

           // this.reportViewer1.RefreshReport();

            ReportDataSource ds = new ReportDataSource();
            ds.Name = "DataSet1";
            ds.Value = GenerateData();

            ReportDataSource ds1 = new ReportDataSource();
            ds1.Name = "DataSet2";
            ds1.Value = GeneratePaymentData();

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = "Report3.rdlc"; 
            this.reportViewer1.LocalReport.DataSources.Add(ds);
            this.reportViewer1.LocalReport.DataSources.Add(ds1);

            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 0;
            pg.Landscape = true;
            pg.Margins.Bottom = 0;
            // pg.Margins.Left = 1;
            // pg.Margins.Right = 1;

            //System.Drawing.Printing.PaperSize size = new PaperSize();
            //size.RawKind = (int)PaperKind.A5;
            //pg.PaperSize = size;
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
        }

        private static DataTable GenerateData()
        {
            string q1 = "SELECT T.invoice_no, T.invoice_date, T.customer_name, T.item_no, T.item_name, T.qty, T.unit_price, T.price, T.total_price, T.discount,T.refNo,T.vehicleno FROM temp_invoice_print T";
            //string q1 = "SELECT T.invoice_no, T.invoice_date, T.customer_name, T.item_no, T.item_name, T.qty, T.unit_price, T.price, T.total_price, T.discount,T.refNo,T.vehicleno,A.paymentMethodTxt,A.enteredAmount FROM temp_invoice_print T,addpaymentsaccount A,payments P WHERE T.invoice_no=P.invoiceno AND T.invoice_no=A.invoiceNo AND A.customerNo=P.customer AND P.paymentno=A.paymentNo AND P.customer='"+ GlobleAccess.cusID +"' AND A.paymentMethodTxt!=''";
            DataSet ds = middle_access.db_access.SelectData(q1);


            DataTable Table = new DataTable();
            Table = ds.Tables[0];
            return Table;
        }

        private static DataTable GeneratePaymentData()
        {
            string q1 = "SELECT paymentMethodTxt,enteredAmount,invoiceNo FROM addpaymentsaccount WHERE customerNo='" + GlobleAccess.cusID + "'";
            DataSet ds = middle_access.db_access.SelectData(q1);

            DataTable Table = new DataTable();
            if (ds != null)
            {
                Table = ds.Tables[0];
            }
            return Table;
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            Form f = (Form)Application.OpenForms["CustomerSection"];
            f.Enabled = true;
            this.Close();
        }

        private void Invoice_report_FormClosing(object sender, FormClosingEventArgs e)
        {
            string q4 = "DELETE FROM temp_invoice_print";
            middle_access.db_access.DeleteData(q4);
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
