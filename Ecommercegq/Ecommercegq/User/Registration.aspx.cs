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
    public partial class Registration : System.Web.UI.Page
    {
        MySqlConnection con;
        MySqlCommand cmd;   
        MySqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if( !IsPostBack)
            {
                
            }
        }

        protected void btnRegisterOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName =string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            con = new MySqlConnection(Utils.getConnection());
            cmd = new MySqlCommand("User_Crud", con);
            cmd.Parameters.AddWithValue("?in_Action", userId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("?in_UserId", userId);
            cmd.Parameters.AddWithValue("?in_Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("?in_Username", txtUserName.Text.Trim());
            cmd.Parameters.AddWithValue("?in_Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("?in_Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("?in_Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("?in_PostCode", txtPostCode.Text.Trim());
            cmd.Parameters.AddWithValue("?in_Password", txtPassword.Text.Trim());            
            cmd.CommandType = CommandType.StoredProcedure;
        }
    }
}