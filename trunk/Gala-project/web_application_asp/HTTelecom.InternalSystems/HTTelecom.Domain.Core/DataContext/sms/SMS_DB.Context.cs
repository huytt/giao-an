﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HTTelecom.Domain.Core.DataContext.sms
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SMS_DBEntities : DbContext
    {
        public SMS_DBEntities()
            : base("name=SMS_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BrandLog> BrandLog { get; set; }
        public DbSet<BrandLog_mss> BrandLog_mss { get; set; }
        public DbSet<BrandStatistic> BrandStatistic { get; set; }
        public DbSet<HomePageLog> HomePageLog { get; set; }
        public DbSet<HomePageStatistic> HomePageStatistic { get; set; }
        public DbSet<OrderLog> OrderLog { get; set; }
        public DbSet<OrderStatistic> OrderStatistic { get; set; }
        public DbSet<ProductBuyStatistic> ProductBuyStatistic { get; set; }
        public DbSet<ProductLog> ProductLog { get; set; }
        public DbSet<ProductLog_mss> ProductLog_mss { get; set; }
        public DbSet<ProductStatistic> ProductStatistic { get; set; }
        public DbSet<SearchStatistic> SearchStatistic { get; set; }
        public DbSet<StatusLog> StatusLog { get; set; }
        public DbSet<StoreLog> StoreLog { get; set; }
        public DbSet<StoreLog_mss> StoreLog_mss { get; set; }
        public DbSet<StoreStatistic> StoreStatistic { get; set; }
        public DbSet<VendorLog> VendorLog { get; set; }
    }
}
