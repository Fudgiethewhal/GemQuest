using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommercegq.Admin
{
    public partial class Category : System.Web.UI.Page
    {
           MySqlConnection con;
           MySqlCommand cmd;
           MySqlDataAdapter sda;
           DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
        }

#pragma warning disable IDE1006 // Naming Styles
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            string actionName= string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int categoryId = Convert.ToInt32(hfCategoryId.Value);
            MySqlConnection con = new MySqlConnection(Utils.getConnection());
            MySqlCommand cmd = new MySqlCommand("Category_Crud", con);
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters: use ?paramName for MySQL
                cmd.Parameters.AddWithValue("?in_Action", categoryId == 0 ? "INSERT" : "UPDATE");
                cmd.Parameters.AddWithValue("?in_CategoryId", categoryId);
                cmd.Parameters.AddWithValue("?in_CategoryName", txtCategoryName.Text.Trim());                
                cmd.Parameters.AddWithValue("?in_IsActive", cbIsActive.Checked);
                if (fuCategoryImage.HasFile)
                {
                    if(Utils.isValidExtension(fuCategoryImage.FileName))
                    {
                        string newImageName = Utils.getUniqueId();
                        fileExtension = Path.GetExtension(fuCategoryImage.FileName);
                        imagePath = "Images/Category/" + newImageName.ToString() + fileExtension;
                        fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + newImageName.ToString() + fileExtension);
                        cmd.Parameters.AddWithValue("?in_CategoryImageUrl", imagePath);
                        isValidToExecute = true;
                    }
                    else
                    {
                        lblMsg.Visible = false;
                        lblMsg.Text = "Please upload a valid image file (jpg, jpeg, png).";
                        lblMsg.CssClass = "alert alert-danger";
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
                        actionName = categoryId == 0 ? " inserted " : " updated ";
                        lblMsg.Visible = true;
                        lblMsg.Text = " Category " + actionName + " successfully!";
                        lblMsg.CssClass = "alert alert-success";
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

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            txtCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            hfCategoryId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imagePreview.ImageUrl = string.Empty;
        }

    }
}