//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartTech.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cart
    {
        public cart() { }
        public cart(long product_id, long user_id, long qnt, decimal price)
        {
            this.product_id = product_id;
            this.user_id = user_id;
            this.qnt = qnt;
            this.price = price;
        }
        public long id { get; set; }
        public long product_id { get; set; }
        public long user_id { get; set; }
        public long qnt { get; set; }
        public decimal price { get; set; }
    
        public virtual product product { get; set; }
        public virtual user user { get; set; }
    }
}
