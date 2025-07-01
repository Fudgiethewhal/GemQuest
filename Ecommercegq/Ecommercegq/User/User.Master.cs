using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommercegq.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        MySqlConnection con;
        MySqlCommand cmd;
        DataTable dt;
        MySqlDataAdapter sda;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                //load control
                Control sliderUserControl = (Control)Page.LoadControl("SliderUserControl.ascx");
                pnlSliderUC.Controls.Add(sliderUserControl);
            }
            if (!IsPostBack)
            {
                getNestedCategories();
            }
        }
        private void getNestedCategories()
        {
            con = new MySqlConnection(Utils.getConnection( ));
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
        protected void rCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField categoryId = e.Item.FindControl("hfCategoryId") as HiddenField;
                Repeater repSubcategory = e.Item.FindControl("rSubCategoryId") as Repeater;
                con = new MySqlConnection(Utils.getConnection());
                cmd = new MySqlCommand("SubCategory_Crud", con);
                cmd.Parameters.AddWithValue("in_Action", "ACTIVEBYID");
                cmd.Parameters.AddWithValue("in_CategoryId", Convert.ToInt32(categoryId.Value));
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    repSubcategory.DataSource = dt;
                    repSubcategory.DataBind();
                }                
            }
        }
    }
}