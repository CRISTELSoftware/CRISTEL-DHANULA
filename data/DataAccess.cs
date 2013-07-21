using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;


namespace data
{
    public class DataAccess
    {
        public static MySqlCommand comm = MySQL.MySQLConnection.GetCommand("root", "123", "localhost", "accountingsystem");

        //For INSERT, UPDATE, DELETE
        public static bool ExecuteData(string qry)
        {

            try
            {
                comm.CommandText = qry;
                //int x = comm.ExecuteScalar();
                int x = comm.ExecuteNonQuery();

                if (x > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }


            finally
            {
                //  comm.Cancel();

            }


        }

        //For SELECT
        public static DataSet GetData(string qry)
        {
            try
            {
                comm.CommandText = qry;
                MySqlDataAdapter da = new MySqlDataAdapter(comm);
                DataSet ds = new DataSet();
                int res = da.Fill(ds);

                if (res > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                // comm.Cancel();
            }


        }
    }
}
