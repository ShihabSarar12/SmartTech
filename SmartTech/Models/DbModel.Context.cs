﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SmartTechEntities : DbContext
    {
        public SmartTechEntities()
            : base("name=SmartTechEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<banner> banners { get; set; }
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<order_products> order_products { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product_categories> product_categories { get; set; }
        public virtual DbSet<product_photos> product_photos { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<shipping> shippings { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
