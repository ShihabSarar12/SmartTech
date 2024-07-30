using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTech.Models
{
    public class CartWithImages
    {
        public CartWithImages() { }
        public CartWithImages(long CartId, long Quantity, long ProductId, string Name, decimal Price, string Image)
        {
            this.CartId = CartId;
            this.Quantity = Quantity;
            this.ProductId = ProductId;
            this.Name = Name;
            this.Price = Price;
            this.Image = Image;
        }

        public long CartId;
        public long Quantity;
        public long ProductId;
        public string Name;
        public decimal Price;
        public string Image;
    }
}