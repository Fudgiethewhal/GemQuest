using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Ecommercegq
{
    public class ProductObj
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string AdditionalDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string CompanyName { get; set; }
        //public string Tags { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsCustomized { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ProductImageObj> ProductImages { get; set; } = new List<ProductImageObj>();
        public int DefaultImagePosition { get; set; }
    }

    public class ProductImageObj
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public bool DefaultImage { get; set; }
    }

    public class ProductDAL
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader sdr;
        MySqlDataAdapter sda;
        DataTable dt;
        MySqlTransaction transaction = null;

        public int AddUpdateProduct(ProductObj productBO)
        {
            int result = 0;
            int productId = 0;
            using (con = new MySqlConnection(Utils.getConnection()))
            {
                try
                {
                    var productImages = productBO.ProductImages;
                    #region insert product
                    con.Open();
                    transaction = con.BeginTransaction();

                    cmd = new MySqlCommand("Product_Crud", con, transaction);
                    cmd.Parameters.AddWithValue("?in_Action", "INSERT");
                    cmd.Parameters.AddWithValue("?in_ProductName", productBO.ProductName);
                    cmd.Parameters.AddWithValue("?in_ShortDescription", productBO.ShortDescription);
                    cmd.Parameters.AddWithValue("?in_LongDescription", productBO.LongDescription);
                    cmd.Parameters.AddWithValue("?in_AdditionalDescription", productBO.AdditionalDescription);
                    cmd.Parameters.AddWithValue("?in_Price", productBO.Price);
                    cmd.Parameters.AddWithValue("?in_Quantity", productBO.Quantity);
                    cmd.Parameters.AddWithValue("?in_Size", productBO.Size);
                    cmd.Parameters.AddWithValue("?in_Color", productBO.Color);
                    cmd.Parameters.AddWithValue("?in_CompanyName", productBO.CompanyName);
                    cmd.Parameters.AddWithValue("?in_CategoryId", productBO.CategoryId);
                    cmd.Parameters.AddWithValue("?in_SubCategoryId", productBO.SubCategoryId);
                    cmd.Parameters.AddWithValue("?in_IsCustomized", productBO.IsCustomized);
                    cmd.Parameters.AddWithValue("?in_IsActive", productBO.IsActive);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    if (productId == 0)
                    {
                        cmd = new MySqlCommand("Product_Crud", con, transaction);
                        cmd.Parameters.AddWithValue("?in_Action", "RECENT_PRODUCT");
                        cmd.CommandType = CommandType.StoredProcedure;
                        sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            productId = (int)sdr["ProductId"];
                        }
                        sdr.Close();  
                    }
                    #endregion

                    #region insert Product Images
                    if (productId > 0)
                    {
                        foreach (var image in productImages)
                        {
                            cmd = new MySqlCommand("Product_Crud", con, transaction);
                            cmd.Parameters.AddWithValue("?in_Action", "INSERT_PROD_IMG");
                            cmd.Parameters.AddWithValue("?in_ImageUrl", image.ImageUrl);
                            cmd.Parameters.AddWithValue("?in_ProductId", productId);
                            cmd.Parameters.AddWithValue("?in_DefaultImage", image.DefaultImage);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            result = 1;
                        }
                    }
                    #endregion

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        result = 0;
                    }
                    catch(Exception e) 
                    {
                        throw;

                    }
                }
            }
            return result;

        }
    }
}
   