﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HTTelecom.Domain.Core.DataContext.acs
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ACS_DBEntities : DbContext
    {
        public ACS_DBEntities()
            : base("name=ACS_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdsCategory> AdsCategories { get; set; }
        public DbSet<AdsChannel> AdsChannels { get; set; }
        public DbSet<AdsType> AdsTypes { get; set; }
        public DbSet<DbTable> DbTables { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogDetail> LogDetails { get; set; }
        public DbSet<LogType> LogTypes { get; set; }
        public DbSet<AdsContent> AdsContent { get; set; }
    }
}