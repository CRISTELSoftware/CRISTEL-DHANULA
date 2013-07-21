using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TMT_2012
{
    class battery_category_data
    {
        public static string brand;
        public static string size;
        public static string type;
        public static string voltage;
        public static string amps;

        public static string qty;
        public static string price;

        public static string din_check_box_val = "NORMAL";

        public static string invoiceQty = "0";
        public static string unitPrice = "0";

        public static bool statusPass2Forms = false;

        public static int get_battery_catagory_id()
        {
            string q = "SELECT b_stok_id FROM add_battery WHERE b_brand = '" + brand + "' AND b_size = '" + size + "' AND b_type = '" + type + "' AND b_voltage = '" + voltage + "' AND b_amps = '" + amps + "'";
            DataSet ds_battery_ctagory_id = middle_access.db_access.SelectData(q);
            DataRow row_cat_id = ds_battery_ctagory_id.Tables[0].Rows[0];
            int catagory_id = Convert.ToInt32(row_cat_id.ItemArray.GetValue(0).ToString());
            return catagory_id;
        }
    }
}
