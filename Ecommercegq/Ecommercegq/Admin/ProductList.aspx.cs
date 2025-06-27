using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommercegq.Admin
{
    public partial class ProductList : System.Web.UI.Page
    {
        MySqlConnection con;
        MySqlCommand cmd;
        DataTable dt;
        ProductDAL productDAL;


        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Product List";
            Session["breadCumbPage"] = "ProductList";
            if (!IsPostBack)
            {
                getProducts();
            }
            lblMsg.Visible = false;
        }

        private void getProducts()
        {
            productDAL = new ProductDAL();
            dt = new DataTable();
            dt = productDAL.ProductWithDefaultImg();
            rProductList.DataSource = dt;
            rProductList.DataBind();
        }

        protected void rProductList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;

            if (e.CommandName == "edit")
            {
                Response.Redirect("Product.aspx?id=" + e.CommandArgument);
            }
            else if (e.CommandName == "delete")
            {
                con = new MySqlConnection(Utils.getConnection());
                cmd = new MySqlCommand("Product_Crud", con);
                cmd.Parameters.AddWithValue("?in_Action", "DELETE");
                cmd.Parameters.AddWithValue("?in_ProductId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Product deleted successfully.";
                    lblMsg.CssClass = "alert alert-success";
                    getProducts();

                }
                catch(Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error : " + ex.Message;
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