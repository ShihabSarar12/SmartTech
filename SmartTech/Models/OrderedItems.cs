using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTech.Models
{
    public class OrderedItems
    {
        public OrderedItems() { }
        public OrderedItems(string OrderID, string OrderTime, string OrderStatus, decimal OrderPrice, List<OrderedProductWithImages> Products)
        {
            this.OrderID = OrderID;
            this.OrderTime = OrderTime;
            this.OrderStatus = OrderStatus;
            this.OrderPrice = OrderPrice;
            this.Products = Products;
        }

        public string OrderID { get; set; }
        public string OrderTime { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderPrice { get; set; }
        public List<OrderedProductWithImages> Products { get; set; }
    }
}