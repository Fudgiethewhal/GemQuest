using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommercegq.Admin
{
    public partial class Category : System.Web.UI.Page
    {
        MySqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnection con;
            MySqlCommand cmd;
            MySqlDataAdapter sda;
            DataTable dt;
        }

#pragma warning disable IDE1006 // Naming Styles
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            string actionName= string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int categoryId = Convert.ToInt32(hfCategoryId.Value);
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=yourpassword;database=ECommerce");

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}