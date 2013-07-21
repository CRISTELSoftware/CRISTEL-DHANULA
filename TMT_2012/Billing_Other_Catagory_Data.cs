using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TMT_2012
{
    class Billing_Other_Catagory_Data
    {
        public static string itemno = "";
        public static string itemname="";      
        public static string type=""; 
        public static string qty = null;
        public static string AvailableQty="0";
        public static string unitPrice = "0";
        public static string catagory = "0";
       // public static string invoiceqty = null;
        // public static double discount = 0 ;

        public static bool statusPass2Forms = false; // to check wther passing data between two forms 
        /// //////////////////////////////////////////////////

        public static string newPrice = null; // for billing Purpose
        public static bool billing = false;
        /// <summary>
        /// ////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public static int get_catagory_id()
        {
            string q = "SELECT itemno FROM otheritems WHERE itemname = '" + itemname + "' ";
            DataSet ds_other_id = middle_access.db_access.SelectData(q);
            DataRow row_cat_id = ds_other_id.Tables[0].Rows[0];

            int catagory_id = Convert.ToInt32(row_cat_id.ItemArray.GetValue(0).ToString());

            return catagory_id;
        }



    }
}
