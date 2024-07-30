using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTech.Models
{
    public class CartToOrder
    {
        public decimal TotalPrice { get; set; }
        public string OrderID { get; set; }
        public string Address { get; set; }
    }
}