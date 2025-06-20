using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace Ecommercegq
{
    public class Utils
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter sda;
        MySqlDataReader sdr;
        DataTable dt;

        public static string getConnection()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        public static bool isValidExtension(string fileName)
        {
            bool isValid = false;
            string[] fileExtension = { ".jpg", ".jpeg", ".png" };
            foreach (string file in fileExtension)
            {
                if (fileName.Contains(file))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        public static string getUniqueId()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}