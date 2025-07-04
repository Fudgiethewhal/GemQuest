using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            cmd.Parameters.AddWithValue("in_Action", userId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("in_UserId", userId);
            cmd.Parameters.AddWithValue("in_Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("in_Username", txtUserName.Text.Trim());
            cmd.Parameters.AddWithValue("in_Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("in_Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("in_Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("in_PostCode", txtPostCode.Text.Trim());
            cmd.Parameters.AddWithValue("in_Password", txtPassword.Text.Trim());
            if (fuUserImage.HasFile)
            {
                if (Utils.isValidExtension(fuUserImage.FileName))
                {
                    string newImageName = Utils.getUniqueId();
                    fileExtension = Path.GetExtension(fuUserImage.FileName);
                    imagePath = "Images/User/" + newImageName.ToString() + fileExtension;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User/") + newImageName.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("in_ImageUrl", imagePath);
                    cmd.Parameters.AddWithValue("in_Action", DBNull.Value);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please select .jpg, .jpeg or .png image";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }
            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = userId == 0 ?
                        "registration is successful! <b><a href='Login.aspx'>Click here to login</a></b>" :
                        "details updated successfully! <b><a href='Profile.aspx'>Click here to view profile</a></b>";
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUserName.Text.Trim() + "</b>, " + actionName;
                    lblMsg.CssClass = "alert alert-success";
                    if (userId != 0)
                    {
                        Response.AddHeader("REFRESH", "3;URL=Profile.aspx");
                    }
                    clear();
                }
                catch (MySqlException ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtUserName.Text.Trim() + "</b> username already exists, try a new one!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error- " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }                
            }
        }

        void clear()
        {
            txtName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPostCode.Text = string.Empty;
            txtPassword.Text = string.Empty;
            
        }
    }
}