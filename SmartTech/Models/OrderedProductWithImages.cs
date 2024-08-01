using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTech.Models
{
    public class OrderedProductWithImages
    {
        public OrderedProductWithImages() { }
        public OrderedProductWithImages(string ProductName, decimal ProductPrice, string ProductImage, long Quantity)
        {
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            this.ProductImage = ProductImage;
            this.Quantity = Quantity;
        }

        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public long Quantity { get; set; }
    }
}