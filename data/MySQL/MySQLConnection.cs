using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace data.MySQL
{
    class MySQLConnection
    {

        public static MySqlConnection conn = null;
        public static MySqlCommand comm = null;


        //Get Method for the Connection --- SINGLETON FUNCTION
        public static MySqlConnection GetConnection(string str)
        {
            try
            {
                if (conn == null)
                {
                    conn = new MySqlConnection(str);

                    return conn;
                }
                else
                {
                    return conn;
                }

            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                conn.Open();
            }
        }

        //Get Connection String
        public static MySqlConnectionStringBuilder GetQueryString(string userName, string passWord, string host, string db)
        {
            try
            {
                MySqlConnectionStringBuilder str = new MySqlConnectionStringBuilder();
                str.Server = host;
                str.UserID = userName;
                str.Password = passWord;
                str.Database = db;

                return str;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static MySqlCommand GetCommand(string userName, string passWord, string host, string db)
        {
            if (comm == null)
            {
                comm = new MySqlCommand();
                comm.Connection = MySQL.MySQLConnection.GetConnection(MySQL.MySQLConnection.GetQueryString(userName, passWord, host, db).ConnectionString);

                return comm;
            }
            else
            {
                return comm;
            }
        }


    }
}
