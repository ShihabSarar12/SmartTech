using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTech.Models
{
    public class TopCategories
    {
        public TopCategories() { }
        public TopCategories(product_categories Category, decimal? TotalSales) 
        {
            this.Category = Category;
            this.TotalSales = TotalSales;
        }
        public product_categories Category { get; set; }
        public decimal? TotalSales { get; set; }
    }
}