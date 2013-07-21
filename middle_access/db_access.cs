using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using data;

namespace middle_access
{
   public class db_access
    {
        public static bool InsertData(string q)
        {
            return data.DataAccess.ExecuteData(q);

        }

        public static bool UpdateData(string q)
        {
            return data.DataAccess.ExecuteData(q);

        }

        public static bool DeleteData(string q)
        {
            return data.DataAccess.ExecuteData(q);

        }

        public static DataSet SelectData(string q)
        {
            return data.DataAccess.GetData(q);

        }


    }
}
