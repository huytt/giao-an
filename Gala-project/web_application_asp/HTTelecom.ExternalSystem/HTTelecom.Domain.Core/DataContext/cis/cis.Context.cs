﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HTTelecom.Domain.Core.DataContext.cis
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CIS_DBEntities : DbContext
    {
        public CIS_DBEntities()
            : base("name=CIS_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AdsCustomerCard> AdsCustomerCard { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<CardType> CardType { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<ContractType> ContractType { get; set; }
        public DbSet<CounterCard> CounterCard { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerCard> CustomerCard { get; set; }
        public DbSet<SecureAuthentication> SecureAuthentication { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VendorAddress> VendorAddress { get; set; }
        public DbSet<VendorCard> VendorCard { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<AdsCustomer> AdsCustomer { get; set; }
    }
}
