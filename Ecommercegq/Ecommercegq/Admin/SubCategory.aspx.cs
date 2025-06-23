using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommercegq.Admin
{
    public partial class SubCategory : System.Web.UI.Page
    {

        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Manage Sub-Category";
            Session["breadCumbPage"] = "Sub-Category";
            if (!IsPostBack)
            {
                getCategories();
                getSubCategories();
                
            }
            lblMsg.Visible = false;
        }

        void getCategories()
        {
            con = new MySqlConnection(Utils.getConnection());
            cmd = new MySqlCommand("Category_Crud", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?in_Action", "GETALL");
            cmd.Parameters.AddWithValue("?in_CategoryId", DBNull.Value);
            cmd.Parameters.AddWithValue("?in_CategoryName", DBNull.Value);
            cmd.Parameters.AddWithValue("?in_CategoryImageUrl", DBNull.Value);
            cmd.Parameters.AddWithValue("?in_IsActive", DBNull.Value);

            sda = new MySqlDataAdapter(cmd);

            dt = new DataTable();
            sda.Fill(dt);
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataTextField = "CategoryId";
            ddlCategory.DataBind();

        }

        void getSubCategories()
        {
            con = new MySqlConnection(Utils.getConnection());
            cmd = new MySqlCommand("Category_Crud", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?in_Action", "GETALL");
            cmd.Parameters.AddWithValue("?in_CategoryId", DBNull.Value);
            cmd.Parameters.AddWithValue("?in_CategoryName", DBNull.Value);
            cmd.Parameters.AddWithValue("?in_CategoryImageUrl", DBNull.Value);
            cmd.Parameters.AddWithValue("?in_IsActive", DBNull.Value);
            sda = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rSubCategory.DataSource = dt;            
            rSubCategory.DataBind();

        }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning restore IDE1006 // Naming Styles
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty;
            int subCategoryId = Convert.ToInt32(hfSubCategoryId.Value);
            con = new MySqlConnection(Utils.getConnection());
            cmd = new MySqlCommand("SubCategory_Crud", con);
            cmd.Parameters.AddWithValue("?in_Action", subCategoryId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("?in_SubCategoryId", subCategoryId);
            cmd.Parameters.AddWithValue("?in_SubCategoryName", txtSubCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("?in_CategoryId", Convert.ToInt32( ddlCategory.SelectedValue ));
            cmd.Parameters.AddWithValue("?in_IsActive", cbIsActive.Checked);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                actionName = subCategoryId == 0 ? "inserted" : "updated";
                lblMsg.Visible = true;
                lblMsg.Text = "Sub-Category " + actionName + " successfully!";
                lblMsg.CssClass = "alert alert-success";
                getSubCategories();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            txtSubCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            hfSubCategoryId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            ddlCategory.ClearSelection();
        }

        protected void rSubCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "edit")
            {
                con = new MySqlConnection(Utils.getConnection());
                cmd = new MySqlCommand("SubCategory_Crud", con);

                cmd.Parameters.AddWithValue("?in_Action", "GETBYID");
                cmd.Parameters.AddWithValue("?in_SubCategoryId", Convert.ToInt32(e.CommandArgument));
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                cmd.Parameters.AddWithValue("?in_CategoryName", DBNull.Value);
                cmd.Parameters.AddWithValue("?in_CategoryImageUrl", DBNull.Value);
                cmd.Parameters.AddWithValue("?in_IsActive", DBNull.Value);
                txtSubCategoryName.Text = dt.Rows[0]["SubCategoryName"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                ddlCategory.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
                hfSubCategoryId.Value = dt.Rows[0]["CategoryId"].ToString();
                btnAddOrUpdate.Text = "Update";
            }
            else if (e.CommandName == "delete")
            {
                con = new MySqlConnection(Utils.getConnection());
                cmd = new MySqlCommand("SubCategory_Crud", con);
                cmd.Parameters.AddWithValue("?in_Action", "DELETE");
                cmd.Parameters.AddWithValue("?in_SubCategoryId", Convert.ToInt32(e.CommandArgument));
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.Text = "SubCategory deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    getSubCategories();

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
