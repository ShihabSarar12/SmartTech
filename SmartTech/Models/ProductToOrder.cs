using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTech.Models
{
    public class ProductToOrder
    {
        public ProductToOrder(long ProductId, decimal Price, long Quantity) 
        {
            this.ProductId = ProductId;
            this.Price = Price;
            this.Quantity = Quantity;
        }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
    }
}