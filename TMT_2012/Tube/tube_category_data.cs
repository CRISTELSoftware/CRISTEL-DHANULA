using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TMT_2012
{
    class tube_category_data
    {
        public static string brand;
        public static string size;
        public static string type;
    //    public static string voltage;
        public static string amps;

        public static string qty;
        public static string price;

     //   public static string din_check_box_val = "NORMAL";

        public static string invoiceQty = "";
        public static string unitPrice = "";
        public static bool statusPass2Forms = false;
        public static int get_battery_catagory_id()
        {
            string q = "SELECT t_stok_id FROM tube_add WHERE t_brand = '" + brand + "' AND t_size = '" + size + "' AND t_type = '" + type + "' AND t_amps = '" + amps + "'";
            DataSet ds_battery_ctagory_id = middle_access.db_access.SelectData(q);
            DataRow row_cat_id = ds_battery_ctagory_id.Tables[0].Rows[0];
            int catagory_id = Convert.ToInt32(row_cat_id.ItemArray.GetValue(0).ToString());
            return catagory_id;
        }
    }
}
