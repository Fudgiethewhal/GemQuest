using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Crypto;
using System.IO;

namespace Ecommercegq.Admin
{    
    public partial class Product : System.Web.UI.Page
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter sda;
        DataTable dt, dt1;
        string[] imagePath;
        ProductObj productObj;
        ProductDAL productDAL;
        List<ProductImageObj> productImages = new List<ProductImageObj>();
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
            try
            {
                string selectedColor = string.Empty;
                string selectedSize = string.Empty;
                bool isValid = false;
                List<string> list = new List<string>();
                bool isImageSaved = false;
                if (fuFirstImage.HasFile && fuSecondImage.HasFile && fuThirdImage.HasFile && fuFourthImage.HasFile)
                {
                    list.Add(fuFirstImage.FileName);
                    list.Add(fuSecondImage.FileName);
                    list.Add(fuThirdImage.FileName);
                    list.Add(fuFourthImage.FileName);
                    string[] fu = list.ToArray();

                    #region validate images
                    for (int i = 0; i < fu.Length; i++)
                    {
                        if (Utils.isValidExtension(fu[i])) // validating image type
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                            break;
                        }
                    }
                    #endregion
                    #region After image validation proceeding to add product
                    if (isValid)
                    {
                        imagePath = Utils.getImagesPath(fu); // getting unique image path
                        for (int i = 0; i <= imagePath.Length -1; i++)
                        {
                            for (int j = i; j <= rblDefaultImage.Items.Count -1;)
                            {
                                productImages.Add
                                (
                                    new ProductImageObj()
                                    {
                                        ImageUrl = imagePath[i],
                                        DefaultImage = Convert.ToBoolean(rblDefaultImage.Items[j].Selected)//getting default image 
                                    }
                                );
                                break;
                            }
                            #region saving images to folder
                            if (i == 0)
                            {
                                fuFirstImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                isImageSaved = true;
                            }
                            else if (i == 1)
                            {
                                fuSecondImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                isImageSaved = true;
                            }
                            else if (i == 2)
                            {
                                fuThirdImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                isImageSaved = true;
                            }
                            else if (i == 3)
                            {
                                fuFourthImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                isImageSaved = true;
                            }                            
                            #endregion
                        }

                        #region saving new product
                        if (isImageSaved)
                        {
                            selectedColor = Utils.getItemWithCommaSeparater(lboxColor);
                            selectedSize = Utils.getItemWithCommaSeparater(lboxSize);
                            productDAL = new ProductDAL();
                            productObj = new ProductObj()
                            {
                                ProductId = 0,
                                ProductName = txtProductName.Text.Trim(),
                                ShortDescription = txtShortDescription.Text.Trim(),
                                LongDescription = txtLongDescription.Text.Trim(),
                                AdditionalDescription = txtAdditionalDescription.Text.Trim(),
                                Price = Convert.ToDecimal(txtPrice.Text.Trim()),
                                Quantity = Convert.ToInt32(txtQuantity.Text.Trim()),
                                Size = selectedSize,
                                Color = selectedColor,
                                CompanyName = txtCompanyName.Text.Trim(),
                                CategoryId = Convert.ToInt32(ddlCategory.SelectedValue),
                                SubCategoryId = Convert.ToInt32(ddlSubCategory.SelectedValue),
                                IsCustomized = cbIsCustomized.Checked,
                                IsActive = cbIsActive.Checked,
                                ProductImages = productImages
                            };
                            int r = productDAL.AddUpdateProduct(productObj);
                            if (r > 0)
                            {
                                DisplayMessage("Product saved successfully.", "success");
                            }
                            else
                            {
                                DeleteFile(imagePath);
                                DisplayMessage("Cannot save record at this moment.", "warning");
                            }
                        }
                        else
                        {
                            DeleteFile(imagePath);
                        }
                        #endregion
                    }
                    else
                    {
                        DisplayMessage("Please upload valid image files (jpg, jpeg, png).", "warning");
                    }
                    #endregion
                }
                else
                {
                    DisplayMessage("Please select all product images", "warning");
                }
                    
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')<script>");
                
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void DeleteFile(string[] filePath)
        {
            for (int i = 0; i <= filePath.Length -1; i++)
            {
                if (File.Exists(Server.MapPath("~/" + filePath[i])))
                {
                    File.Delete(Server.MapPath("~/" + filePath[i]));
                }
                        
            }
        }

        void DisplayMessage(string message, string cssClass)
        {
            lblMsg.Visible = true;
            lblMsg.Text = message;
            lblMsg.CssClass = "alert alert-" + cssClass;
        }

        private void Clear()
        {
            txtProductName.Text = string.Empty;
            txtShortDescription.Text = string.Empty;
            txtLongDescription.Text = string.Empty;
            txtAdditionalDescription.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            lboxColor.ClearSelection();
            lboxSize.ClearSelection();
            ddlCategory.ClearSelection();            
            ddlSubCategory.ClearSelection();
            rblDefaultImage.ClearSelection();
            cbIsCustomized.Checked = false;
            cbIsActive.Checked = false;
            hfDefaultImagePos.Value = "0";
        }
    }
}