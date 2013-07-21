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
    public partial class Payments_Full_Credit_Report : Form
    {
        public Payments_Full_Credit_Report()
        {
            InitializeComponent();
        }

        private void Payments_Full_Credit_Report_Load(object sender, EventArgs e)
        {

            ReportDataSource ds = new ReportDataSource();
            ds.Name = "DataSet1";
            ds.Value = GeneratePaymentData();

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = @"Report6.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(ds);


            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 0;
           // pg.Landscape = true;
            pg.Margins.Bottom = 0;
            pg.Margins.Left = 0;
            pg.Margins.Right = 0;

            System.Drawing.Printing.PaperSize size = new PaperSize();
            size.RawKind = (int)PaperKind.A4;
            pg.PaperSize = size;
          //  pg.Landscape = true;

            this.reportViewer1.SetPageSettings(pg);
            this.reportViewer1.RefreshReport();    
        }

        private DataTable GeneratePaymentData()
        {
            string q1 = "";
            //string q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,APA.customerNo,APA.customerName,APA.invoiceNo,I.invoicedate,APA.invoiceAmount,APA.enteredAmount,(APA.invoiceAmount - APA.enteredAmount) AS Balance,APA.paymentMethodTxt,C.givendate,C.duedate,I.invoicetotal,APA.paymentDate FROM addpaymentsaccount APA  LEFT JOIN cheque C ON C.payments=APA.paymentNo,invoice I   WHERE APA.invoiceNo=I.invoiceno AND APA.customerNo='" + GlobleAccess.cusID + "' ";
            if (GlobleAccess.PaymentReportType == "C" && GlobleAccess.ReportRangeType == "D")
            {
                q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,CS.customername AS customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(I.invoicetotal - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate,C.tel_home,C.tel_mobile,C.tel_office,C.tel_other,C.fax FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo),customertel C,customer CS) WHERE CS.customerno=I.customer AND C.customerNo=I.customer AND I.invoiceno IN (SELECT invoiceno FROM View3 WHERE ((invoicetotal - enteredAmount)<'0.00' OR (invoicetotal - enteredAmount)='0.00')GROUP BY invoiceno) AND (I.invoicedate BETWEEN '" + GlobleAccess.FromDate + "' AND '" + GlobleAccess.ToDate + "') GROUP BY invoiceno";
            }
            else if (GlobleAccess.PaymentReportType == "C" && GlobleAccess.ReportRangeType == "A")
            {
                q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,CS.customername AS customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(I.invoicetotal - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate,C.tel_home,C.tel_mobile,C.tel_office,C.tel_other,C.fax FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo),customertel C,customer CS) WHERE CS.customerno=I.customer AND C.customerNo=I.customer AND I.invoiceno IN (SELECT invoiceno FROM View3 WHERE ((invoicetotal - enteredAmount)<'0.00' OR (invoicetotal - enteredAmount)='0.00')GROUP BY invoiceno)  GROUP BY invoiceno"; 
            }
            else if (GlobleAccess.PaymentReportType == "I" && GlobleAccess.ReportRangeType == "D")
            {
               // q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,APA.customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(APA.invoiceAmount - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo)) WHERE  I.invoiceno IN (SELECT invoiceno FROM View3 WHERE (invoicetotal<>enteredAmount OR enteredAmount IS NULL) GROUP BY invoiceno)GROUP BY invoiceno";
                q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,CS.customername AS customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(APA.invoiceAmount - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate,C.tel_home,C.tel_mobile,C.tel_office,C.tel_other,C.fax FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo),customertel C,customer CS) WHERE CS.customerno=I.customer AND C.customerNo=I.customer AND I.invoiceno IN (SELECT invoiceno FROM View3 WHERE  (invoicetotal<>enteredAmount OR enteredAmount IS NULL) GROUP BY invoiceno) AND (I.invoicedate BETWEEN '"+ GlobleAccess.FromDate +"' AND '"+ GlobleAccess.ToDate +"') GROUP BY invoiceno ";
            }
            else if (GlobleAccess.PaymentReportType == "I" && GlobleAccess.ReportRangeType == "A")
            {
                // q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,APA.customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(APA.invoiceAmount - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo)) WHERE  I.invoiceno IN (SELECT invoiceno FROM View3 WHERE (invoicetotal<>enteredAmount OR enteredAmount IS NULL) GROUP BY invoiceno)GROUP BY invoiceno";
                q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,CS.customername AS customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(APA.invoiceAmount - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate,C.tel_home,C.tel_mobile,C.tel_office,C.tel_other,C.fax FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo),customertel C,customer CS) WHERE CS.customerno=I.customer AND C.customerNo=I.customer AND I.invoiceno IN (SELECT invoiceno FROM View3 WHERE  (invoicetotal<>enteredAmount OR enteredAmount IS NULL) GROUP BY invoiceno) GROUP BY invoiceno ";
            }

            else if (GlobleAccess.PaymentReportType == "F" && GlobleAccess.ReportRangeType == "A")
            {
                // q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,APA.customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(APA.invoiceAmount - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo)) WHERE  I.invoiceno IN (SELECT invoiceno FROM View3 WHERE (invoicetotal<>enteredAmount OR enteredAmount IS NULL) GROUP BY invoiceno)GROUP BY invoiceno";
                q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,CS.customername AS customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(I.invoicetotal - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate,C.tel_home,C.tel_mobile,C.tel_office,C.tel_other,C.fax FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo),customertel C,customer CS) WHERE CS.customerno=I.customer AND C.customerNo=I.customer AND I.invoiceno IN (SELECT invoiceno FROM View3 WHERE  ((invoicetotal<>enteredAmount OR enteredAmount IS NULL) OR ((invoicetotal - enteredAmount)<'0.00' OR (invoicetotal - enteredAmount)='0.00')))  GROUP BY invoiceno";
            }
            else if (GlobleAccess.PaymentReportType == "F" && GlobleAccess.ReportRangeType == "D")
            {
                // q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,APA.customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(APA.invoiceAmount - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo)) WHERE  I.invoiceno IN (SELECT invoiceno FROM View3 WHERE (invoicetotal<>enteredAmount OR enteredAmount IS NULL) GROUP BY invoiceno)GROUP BY invoiceno";
                q1 = "SELECT APA.paymentNo,I.invoicenote AS Ref,I.customer AS customerNo ,CS.customername AS customerName,I.invoiceno,I.invoicedate,APA.invoiceAmount,SUM(APA.enteredAmount) AS enteredAmount,(I.invoicetotal - SUM(APA.enteredAmount)) AS Balance,APA.paymentMethodTxt,I.invoicetotal,APA.paymentDate,C.tel_home,C.tel_mobile,C.tel_office,C.tel_other,C.fax FROM (invoice I LEFT JOIN  addpaymentsaccount APA  ON (I.invoiceno=APA.invoiceNo),customertel C,customer CS) WHERE CS.customerno=I.customer AND C.customerNo=I.customer AND I.invoiceno IN (SELECT invoiceno FROM View3 WHERE  ((invoicetotal<>enteredAmount OR enteredAmount IS NULL) OR ((invoicetotal - enteredAmount)<'0.00' OR (invoicetotal - enteredAmount)='0.00'))) AND (I.invoicedate BETWEEN '" + GlobleAccess.FromDate + "' AND '" + GlobleAccess.ToDate + "')  GROUP BY invoiceno";
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
                MessageBox.Show("No report available!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return Table;
        }

        private void Payments_Full_Credit_Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            string q4 = "DELETE FROM temp_invoice_print";
            middle_access.db_access.DeleteData(q4);
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
    }
}
