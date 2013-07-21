using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;

namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Payments_Full_Report : Form
    {
        public Payments_Full_Report()
        {
            InitializeComponent();
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["CustomerSection"] != null)
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
        }

        private void Payments_Full_Report_Load(object sender, EventArgs e)
        {
            ReportDataSource ds = new ReportDataSource();
            ds.Name = "DataSet1";
            ds.Value = GeneratePaymentData();            

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = @"Report5.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(ds);


            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 0;
            pg.Landscape = true;
            pg.Margins.Bottom = 0;
            pg.Margins.Left = 50;
            pg.Margins.Right = 0;

            System.Drawing.Printing.PaperSize size = new PaperSize();
            size.RawKind = (int)PaperKind.A4;            
            pg.PaperSize = size;
            pg.Landscape = true;
            
            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();          
        }

        private DataTable GeneratePaymentData()
        {
            string q1 = "";
            //string q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,APA.customerNo,APA.customerName,APA.invoiceNo,I.invoicedate,APA.invoiceAmount,APA.enteredAmount,(APA.invoiceAmount - APA.enteredAmount) AS Balance,APA.paymentMethodTxt,C.givendate,C.duedate,I.invoicetotal,APA.paymentDate FROM addpaymentsaccount APA  LEFT JOIN cheque C ON C.payments=APA.paymentNo,invoice I   WHERE APA.invoiceNo=I.invoiceno AND APA.customerNo='" + GlobleAccess.cusID + "' ";
            if (GlobleAccess.PaymentReportType == "C")
            {
                q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,APA.customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,APA.enteredAmount,(APA.invoiceAmount - APA.enteredAmount) AS Balance,APA.paymentMethodTxt,C.givendate,C.duedate,I.invoicetotal,APA.paymentDate FROM (invoice I LEFT JOIN  (addpaymentsaccount APA LEFT JOIN cheque C ON (C.payments=APA.paymentNo))  ON (APA.invoiceNo=I.invoiceno)) WHERE  I.customer='" + GlobleAccess.cusID + "' AND I.invoiceno IN (SELECT invoiceno FROM addpaymentsaccount WHERE (invoiceAmount - enteredAmount)<'0.00' OR (invoiceAmount - enteredAmount)='0.00')  ORDER BY  I.invoiceno  ";
            }
            else if (GlobleAccess.PaymentReportType == "I")
            {
                q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,APA.customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,APA.enteredAmount,(APA.invoiceAmount - APA.enteredAmount) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo)) WHERE  I.customer='" + GlobleAccess.cusID + "' AND I.invoiceno IN (SELECT invoiceno FROM View3 WHERE customerNo='" + GlobleAccess.cusID + "' AND (invoicetotal<>enteredAmount OR enteredAmount IS NULL) GROUP BY invoiceno)";
            }
            
            
            //string q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,APA.customerNo,APA.customerName,APA.invoiceNo,IL.itemname,I.invoicedate,APA.invoiceAmount,APA.enteredAmount,(APA.invoiceAmount - APA.enteredAmount) AS Balance,APA.paymentMethodTxt,C.givendate,C.duedate FROM addpaymentsaccount APA  LEFT JOIN cheque C ON C.payments=APA.paymentNo,invoice I,invoicelines IL   WHERE APA.invoiceNo=I.invoiceno AND I.invoiceno=IL.invoiceno AND APA.invoiceNo=IL.invoiceno AND customerNo='" + GlobleAccess.cusID + "'";

            DataSet ds = middle_access.db_access.SelectData(q1);

            DataTable Table = new DataTable();
            if (ds != null)
            {
                Table = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("No report available!","Message",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            return Table;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void Payments_Full_Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            string q4 = "DELETE FROM temp_invoice_print";
            middle_access.db_access.DeleteData(q4);
        }
    }
}
