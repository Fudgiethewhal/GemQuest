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
    public partial class Shop : System.Web.UI.Page
    {
        MySqlConnection con;
        MySqlCommand cmd;
        DataTable dt;
        MySqlDataAdapter sda;
        Utils utils;
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cid"] != null) //get products by category
                {
                    getProductByCategory();
                }
                else if (Request.QueryString["sid"] != null) // get products by sub-category
                {
                    getProductBySubCategory();
                }
                else
                {
                    getAllProducts();

                }
            }
        }

        void getAllProducts()
        {
            try 
            {
                using(con = new MySqlConnection(Utils.getConnection()))
                {
                    con.Open();                
                    cmd = new MySqlCommand("Product_Crud", con);
                    cmd.Parameters.AddWithValue("in_Action", "ACTIVEPRODUCT");
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rProducts.DataSource = dt;
                        
                    }
                    else
                    {
                        rProducts.DataSource = dt;
                        rProducts.FooterTemplate = null;
                        rProducts.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                    rProducts.DataBind();
                    Session["product"] = dt;
                }
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "')</script>");
            }
        }

        void getProductByCategory()
        {
            try
            {
                using (con = new MySqlConnection(Utils.getConnection()))
                {
                    int categoryId = Convert.ToInt32(Request.QueryString["cid"]);
                    cmd = new MySqlCommand("Product_Crud", con);
                    cmd.Parameters.AddWithValue("in_Action", "PRDTBYCATEGORY");
                    cmd.Parameters.AddWithValue("in_CategoryId", categoryId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rProducts.DataSource = dt;

                    }
                    else
                    {
                        rProducts.DataSource = dt;
                        rProducts.FooterTemplate = null;
                        rProducts.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                    rProducts.DataBind();
                    Session["product"] = dt;
                }
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "')</script>");
            }
        }

        void getProductBySubCategory()
        {
            try
            {
                using (con = new MySqlConnection(Utils.getConnection()))
                {
                    int subCategoryId = Convert.ToInt32(Request.QueryString["sid"]);
                    cmd = new MySqlCommand("Product_Crud", con);
                    cmd.Parameters.AddWithValue("in_Action", "PRDTBYSUBCATEGORY");
                    cmd.Parameters.AddWithValue("in_SubCategoryId", subCategoryId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rProducts.DataSource = dt;

                    }
                    else
                    {
                        rProducts.DataSource = dt;
                        rProducts.FooterTemplate = null;
                        rProducts.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                    rProducts.DataBind();
                    Session["product"] = dt;
                }
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "')</script>");
            }
        }
        //Custom Template class to add controls to the repeater's header, item and footer sections.
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            public CustomTemplate(ListItemType listItemType)
            {
                ListItemType = ListItemType;
            }

            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<b>No Product to display.</b>");
                    container.Controls.Add(footer);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["product"];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dv = new DataView(dt);
                    dv.RowFilter = "ProductName LIKE '%" + txtSearchInput.Value.Trim().Replace("'", "''") + "%' ";
                    if (dv.Count > 0)
                    {
                        rProducts.DataSource = dv;
                    }
                    else
                    {
                        rProducts.DataSource = dv;
                        rProducts.FooterTemplate = null;
                        rProducts.FooterTemplate = new CustomTemplate(ListItemType.Footer);

                    }
                }
                else
                {
                    rProducts.DataSource = dt;
                    rProducts.FooterTemplate = null;
                    rProducts.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                }
                rProducts.DataBind();
            }
        }      


        protected void btnReset_Click(object sender, EventArgs e)
        {
            rProducts.DataSource = null;
            rProducts.DataSource = (DataTable)Session["product"];
            rProducts.DataBind();
            txtSearchInput.Value = string.Empty;
        }

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSortBy.SelectedValue != "0")
            {
                dt = (DataTable)Session["product"];
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dv = new DataView(dt);
                        if (ddlSortBy.SelectedIndex == 1)
                        {
                            dv.Sort = "CreatedDate ASC";
                        }
                        else if (ddlSortBy.SelectedIndex == 2)
                        {
                            dv.Sort = "ProductName ASC";
                        }
                        else
                        {
                            dv.Sort = "Price ASC";
                        }
                        if (dv.Count > 0)
                        {
                            rProducts.DataSource = dv;
                        }
                        else
                        {
                            rProducts.DataSource = dv;
                            rProducts.FooterTemplate = null;
                            rProducts.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                        }
                        rProducts.DataBind();
                    }
                    else
                    {
                        rProducts.DataSource = dt;
                        rProducts.FooterTemplate = null;
                        rProducts.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                }
            }
        }
        protected void btnSortReset_Click(object sender, EventArgs e)
        {
            rProducts.DataSource = null;
            rProducts.DataSource = (DataTable)Session["product"];
            rProducts.DataBind();
            ddlSortBy.ClearSelection();
        }
    }
}