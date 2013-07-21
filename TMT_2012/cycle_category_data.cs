using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TMT_2012
{
    class cycle_category_data
    {
        public static string brand;
        public static string size;
        public static string ply_rate;
        public static string thread_pattern;
        public static string make;
        public static string tyre_pattern;
        public static string tube;
        public static string side;
        public static string qty = null;
        public static string unitPricee="0";
        public static double total = 0;


        public static string newPriceCycle = null; // for billing Purpose
        public static bool statusPass2Forms = false; // to check wther passing data between two forms 

        public static int get_catagory_id()
        {
           // int catagory_id = -1;
            string q = "SELECT t_stok_id FROM add_cycle_tyre WHERE t_brand = '" + brand + "' AND t_size = '" + size + "' AND t_ply_rate = '" + ply_rate + "' AND t_make = '" + make + "' AND t_thread_pattern = '" + thread_pattern + "' AND t_side = '" + side + "' AND t_tube = '" + tube + "' AND t_tyre_pattern = '" + tyre_pattern + "' ";
            DataSet ds_ctagory_id = middle_access.db_access.SelectData(q);           
            DataRow row_cat_id = ds_ctagory_id.Tables[0].Rows[0];
            int catagory_id = Convert.ToInt32(row_cat_id.ItemArray.GetValue(0).ToString());
            

            return catagory_id;
        }
    }
}
