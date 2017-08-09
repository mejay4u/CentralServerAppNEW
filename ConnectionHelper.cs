using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
namespace CentralServerApp
{
    public static class ConnectionHelper
    {
        public static SqlConnection mycon1()
        {
            string str = ConfigurationManager.ConnectionStrings["connect1"].ToString();
            SqlConnection conn= new SqlConnection(str);
            return conn;
        }

        public static SqlConnection mycon2()
        {
            string str1 = ConfigurationManager.ConnectionStrings["connect2"].ToString();
            SqlConnection conn1 = new SqlConnection(str1);
            return conn1;
        }
    }
}