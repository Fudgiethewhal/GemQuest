using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommercegq.User
{
    public partial class Default : System.Web.UI.Page
    {
        MySqlConnection con;
        MySqlCommand cmd;
        DataTable dt;
        MySqlDataAdapter sda;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategories();
            }
        }

        private void getCategories()
        {
            con = new MySqlConnection(Utils.getConnection());
            cmd = new MySqlCommand("Category_Crud", con);
            cmd.Parameters.AddWithValue("in_Action", "ACTIVECATEGORY");
            cmd.Parameters.AddWithValue("in_CategoryId", DBNull.Value);
            cmd.Parameters.AddWithValue("in_CategoryName", DBNull.Value);
            cmd.Parameters.AddWithValue("in_CategoryImageUrl", DBNull.Value);
            cmd.Parameters.AddWithValue("in_IsActive", DBNull.Value);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();
        }
    }
}