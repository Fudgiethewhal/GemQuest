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
            string type = "insert";
            using (con = new MySqlConnection(Utils.getConnection()))
            {
                try
                {
                    var productImages = productBO.ProductImages;
                    #region insert product
                    con.Open();
                    transaction = con.BeginTransaction();
                    productId = productBO.ProductId;



                    cmd = new MySqlCommand("Product_Crud", con, transaction);
                    cmd.Parameters.AddWithValue("?in_Action", productId == 0 ? "INSERT" : "UPDATE");
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
                    if (productId > 0)
                    {
                        cmd.Parameters.AddWithValue("?in_ProductId", productBO.ProductId);
                        type = "update";
                    }

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
                        if (type == "insert")
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
                        else //update images
                        {
                            bool isTrue = false;
                            if (productImages.Count != 0)
                            {
                                cmd = new MySqlCommand("Product_Crud", con, transaction);
                                cmd.Parameters.AddWithValue("?in_Action", "DELETE_PROD_IMG");
                                cmd.Parameters.AddWithValue("?in_ProductId", productId);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                isTrue = true;
                            }
                            else 
                            {
                                int defaultImagePos = productBO.DefaultImagePosition;
                                if (defaultImagePos > 0)
                                {
                                    cmd = new MySqlCommand("Product_Crud", con, transaction);
                                    cmd.Parameters.AddWithValue("?in_Action", "UPDATE_IMG_POS");                                    
                                    cmd.Parameters.AddWithValue("?in_ProductId", productId);
                                    cmd.Parameters.AddWithValue("?in_DefaultImagePos", defaultImagePos);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.ExecuteNonQuery();
                                    result = 1;
                                }
                                else
                                {
                                    result = 1; 
                                }
                            }

                            if (isTrue)
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

        public DataTable ProductByIdWithImages(int productId)
        {
            try
            {
                DataTable dt = ProductById(productId);
                dt.Columns.Add("Image2");
                dt.Columns.Add("Image3");
                dt.Columns.Add("Image4");
                dt.Columns.Add("DefaultImage");
                DataRow dr = dt.NewRow();
                string images = dt.Rows[0]["Image1"].ToString();
                string[] imgArr = images.Split(';');
                string imag;
                int rb = 0;
                foreach (string img in imgArr)
                {
                    imag = img.Substring(img.IndexOf(": ") + 1);
                    if (imag.Trim() == "1")
                    {
                        break;
                    }
                    else 
                    {
                        rb++;
                    }
                }
                foreach (DataRow dataRow in dt.Rows)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        dataRow["image" + (i + 1)] = imgArr[i].Trim();
                    }
                    dataRow["DefaultImage"] = rb;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable ProductById(int pId)
        {
            try
            {
                using(con = new MySqlConnection(Utils.getConnection()))
                {
                    con.Open();
                    cmd = new MySqlCommand("Product_Crud", con);
                    cmd.Parameters.AddWithValue("?in_Action", "GETBYID");
                    cmd.Parameters.AddWithValue("?in_ProductId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ProductWithDefaultImg()
        {
            try
            {
                using (con = new MySqlConnection(Utils.getConnection()))
                {
                    con.Open();
                    cmd = new MySqlCommand("Product_Crud", con);
                    cmd.Parameters.AddWithValue("?in_Action", "SELECT");                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new MySqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
   