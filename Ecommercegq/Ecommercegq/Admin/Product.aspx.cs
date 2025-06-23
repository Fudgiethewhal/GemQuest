using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommercegq.Admin
{    
    public partial class Product : System.Web.UI.Page
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter sda;
        DataTable dt, dt1;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Product";
            Session["breadCumbPage"] = "Product";
            if (!IsPostBack)
            {
                getCategories();
                
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
            ddlSubCategory.Items.Clear();
            ddlSubCategory.DataSource = dt;
            ddlSubCategory.DataTextField = "CategoryName";
            ddlSubCategory.DataValueField = "CategoryId";
            ddlSubCategory.DataBind();

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSubCategories(Convert.ToInt32(ddlCategory.SelectedValue));
        }
        void getSubCategories(int categoryId)
        {
            con = new MySqlConnection(Utils.getConnection());
            cmd = new MySqlCommand("SubCategory_Crud", con);
            cmd.Parameters.AddWithValue("?in_Action", "SUBCATEGORYBYID");
            cmd.Parameters.AddWithValue("?in_CategoryId", "categoryId");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?in_CategoryName", DBNull.Value);
            cmd.Parameters.AddWithValue("?in_CategoryImageUrl", DBNull.Value);
            cmd.Parameters.AddWithValue("?in_IsActive", DBNull.Value);
            sda = new MySqlDataAdapter(cmd);
            dt1 = new DataTable();
            sda.Fill(dt1);
            ddlSubCategory.Items.Clear();
            ddlCategory.DataSource = dt1;
            ddlCategory.DataTextField = "SubCategoryName";
            ddlCategory.DataValueField = "SubCategoryId";            
            ddlCategory.DataBind();
            ddlSubCategory.Items.Insert(0,"Select SubCategory");    

        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {

        }


    }
}