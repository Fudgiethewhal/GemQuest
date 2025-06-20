using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ecommercegq
{
    public class Utils
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader sdr;
        DataTable dt;

        public static string getConnection()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }
    }
}