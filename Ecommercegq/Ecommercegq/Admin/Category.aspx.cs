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
            Session["breadCumbTitle"] = "Manage Category";
            Session["breadCumbPage"] = "Category";
            lblMsg.Visible = false;
            getCategories();
        }

        void getCategories()
        {
            con = new MySqlConnection(Utils.getConnection());
            cmd = new MySqlCommand("Category_Crud", con);
            
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("in_Action", "GETALL");
                cmd.Parameters.AddWithValue("in_CategoryId", DBNull.Value);
                cmd.Parameters.AddWithValue("in_CategoryName", DBNull.Value);
                cmd.Parameters.AddWithValue("in_CategoryImageUrl", DBNull.Value);
                cmd.Parameters.AddWithValue("in_IsActive", DBNull.Value);
                cmd.Parameters.AddWithValue("in_Sold", DBNull.Value);

            sda = new MySqlDataAdapter(cmd);
                
                    dt = new DataTable();
                    sda.Fill(dt);
                    rCategory.DataSource = dt;
                    rCategory.DataBind();
                            
        }
#pragma warning disable IDE1006 // Naming Styles
#pragma warning restore IDE1006 // Naming Styles
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int categoryId = Convert.ToInt32(hfCategoryId.Value);
            con = new MySqlConnection(Utils.getConnection());
            cmd = new MySqlCommand("Category_Crud", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("in_Action", categoryId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("in_CategoryId", categoryId);
            cmd.Parameters.AddWithValue("in_CategoryName", txtCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("in_IsActive", cbIsActive.Checked);

            // Always set imagePath, even if empty (for SP)
            imagePath = "";
            if (fuCategoryImage.HasFile)
            {
                if (Utils.isValidExtension(fuCategoryImage.FileName))
                {
                    string newImageName = Utils.getUniqueId();
                    fileExtension = Path.GetExtension(fuCategoryImage.FileName);
                    imagePath = "Images/Category/" + newImageName + fileExtension;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + newImageName + fileExtension);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please upload a valid image file (jpg, jpeg, png).";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                // For UPDATE, keep the old image if not uploading a new one
                if (categoryId != 0)
                {
                    imagePath = imagePreview.ImageUrl.Replace("../", ""); // Remove leading ../ if present
                }
                isValidToExecute = true;
            }

            cmd.Parameters.AddWithValue("in_CategoryImageUrl", string.IsNullOrEmpty(imagePath) ? DBNull.Value : (object)imagePath);

            cmd.Parameters.AddWithValue("in_Sold", DBNull.Value); // If your SP needs this

            if (isValidToExecute)
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = categoryId == 0 ? "inserted" : "updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category " + actionName + " successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getCategories();
                    clear();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            txtCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            hfCategoryId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imagePreview.ImageUrl = string.Empty;
        }

        
        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;

            if (e.CommandName == "edit")
            {
                con = new MySqlConnection(Utils.getConnection());
                cmd = new MySqlCommand("Category_Crud", con);

                cmd.Parameters.AddWithValue("in_Action", "GETBYID");
                cmd.Parameters.AddWithValue("in_CategoryId", Convert.ToInt32(e.CommandArgument));
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                cmd.Parameters.AddWithValue("in_CategoryName", DBNull.Value);
                cmd.Parameters.AddWithValue("in_Action", DBNull.Value);
                cmd.Parameters.AddWithValue("in_CategoryImageUrl", DBNull.Value);
                cmd.Parameters.AddWithValue("in_IsActive", DBNull.Value);
                txtCategoryName.Text = dt.Rows[0]["CategoryName"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                imagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["CategoryImageUrl"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["CategoryImageUrl"].ToString();
                imagePreview.Height = 200;
                imagePreview.Width = 200;
                hfCategoryId.Value = dt.Rows[0]["CategoryId"].ToString();
                btnAddOrUpdate.Text = "Update";
            }
            else if (e.CommandName == "delete")
            {
                con = new MySqlConnection(Utils.getConnection());
                cmd = new MySqlCommand("Category_Crud", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("in_Action", "DELETE");
                cmd.Parameters.AddWithValue("in_CategoryId", Convert.ToInt32(e.CommandArgument));
                cmd.Parameters.AddWithValue("in_CategoryName", DBNull.Value);
                cmd.Parameters.AddWithValue("in_CategoryImageUrl", DBNull.Value);
                cmd.Parameters.AddWithValue("in_IsActive", DBNull.Value);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.Text = "Category deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getCategories();
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
}