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
    public partial class Payments_Report : Form
    {
        public Payments_Report()
        {
            InitializeComponent();
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
           // Form f = (Form)Application.OpenForms["CustomerSection"];
           // f.Enabled = true;
            this.Close();
        }

        private void Payments_Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            string q4 = "DELETE FROM temp_invoice_print_all";
            middle_access.db_access.DeleteData(q4);

           
        }

        private void Payments_Report_Load(object sender, EventArgs e)
        {
            ReportDataSource ds = new ReportDataSource();
            ds.Name = "DataSet1";
            ds.Value = GenerateData();

           /* ReportDataSource ds1 = new ReportDataSource();
            ds1.Name = "DataSet2";
            ds1.Value = GeneratePaymentData();*/

          /*  ReportDataSource ds2 = new ReportDataSource();
            ds2.Name = "DataSet3";
            ds2.Value = GenerateData();*/

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = "Report4.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(ds);
            //this.reportViewer1.LocalReport.DataSources.Add(ds1);
           // this.reportViewer1.LocalReport.DataSources.Add(ds2);
           

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
         
        }

        private static DataTable GeneratePaymentData()
        {
            string q1 = "SELECT paymentMethodTxt,enteredAmount,invoiceNo,customerName FROM addpaymentsaccount WHERE customerNo='" + GlobleAccess.cusID + "'";
            DataSet ds = middle_access.db_access.SelectData(q1);

            DataTable Table = new DataTable();
            if (ds != null)
            {
                Table = ds.Tables[0];
            }
            return Table;
        }

        private static DataTable GenerateData()
        {
            //string q1 = "SELECT T.invoiceNo, T.invoiceDate, T.invoiceLinesText,A.paymentMethodTxt,A.enteredAmount,A.customerName,T.total FROM addpaymentsaccount A RIGHT OUTER JOIN temp_invoice_print_all T ON T.invoiceNo=A.invoiceNo ORDER BY T.invoiceNo";
            //string q1 = "SELECT T.invoiceNo, T.invoiceDate, T.invoiceLinesText,A.paymentMethodTxt,A.enteredAmount,A.customerName,T.total FROM temp_invoice_print_all T LEFT OUTER JOIN addpaymentsaccount A  ON T.invoiceNo=A.invoiceNo WHERE  A.customerNo='" + GlobleAccess.cusID + "' GROUP BY A.paymentMethodTxt,A.enteredAmount";
            //string q1 = "SELECT * FROM View2";
            string q1 = "";
            string q2 = "";
            DataSet ds1 = null;
            DataSet ds2 = null;

            q1 = "select `invoiceNo`,`invoiceDate`,`invoiceLinesText`,`paymentMethodTxt`,`enteredAmount`,`customerName`,`total`  from view2 WHERE paymentMethodTxt<>'' group by paymentMethodTxt";
            ds1 = middle_access.db_access.SelectData(q1);

            if (ds1 != null)
            {
                q2 = "select `invoiceNo`,`invoiceDate`,`invoiceLinesText`,`paymentMethodTxt`,`enteredAmount`,`customerName`,`total`  from view2 WHERE paymentMethodTxt IS NULL group by invoiceNo";
                ds2 = middle_access.db_access.SelectData(q2);
                if (ds2 != null)
                {
                    ds1.Merge(ds2);
                }
            }
            else
            {
                q2 = "select `invoiceNo`,`invoiceDate`,`invoiceLinesText`,`paymentMethodTxt`,`enteredAmount`,`customerName`,`total`  from view2 WHERE paymentMethodTxt IS NULL group by invoiceNo";
                ds1 = middle_access.db_access.SelectData(q2);
            }
            

            DataTable Table = new DataTable();
            if (ds1 != null)
            {
                Table = ds1.Tables[0];
            }
            return Table;
        }

    }
}
