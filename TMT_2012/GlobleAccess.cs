using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TMT_2012
{
    class GlobleAccess
    {
        public static string openType = "";
        public static string customerNo="";
        public static string customerName="";
        public static string amount="";
        public static string invoiceNo = "";
        public static string cusID="";
        public static string PaymentReportType = "";
        public static string ReportRangeType = "";
        public static string FromDate = "";
        public static string ToDate = "";

        public static string Get_Payments_Sum(){
             string q = "SELECT SUM(enteredAmount) AS sum FROM addpaymentsaccount WHERE invoiceNo='" + GlobleAccess.invoiceNo + "'";
             DataSet ds = middle_access.db_access.SelectData(q);
             if (ds != null)
                 return ds.Tables[0].Rows[0][0].ToString();
             else
                 return "";

        }
    }
}
