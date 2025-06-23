using System;
using System.Collections.Generic;
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
}