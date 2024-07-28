using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTech.Models
{
    public class ProductWithImages
    {
        public ProductWithImages() { }
        public product Product { get; set; }
        public List<string> ProductPhotos { get; set; }
    }
}