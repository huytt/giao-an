﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HTTelecom.Domain.Core.DataContext.tts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TTS_DBEntities : DbContext
    {
        public TTS_DBEntities()
            : base("name=TTS_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<MainRecord> MainRecord { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<StatusDirection> StatusDirection { get; set; }
        public DbSet<StatusProcess> StatusProcess { get; set; }
        public DbSet<SubRecord> SubRecord { get; set; }
        public DbSet<TaskDirection> TaskDirection { get; set; }
        public DbSet<TaskForm> TaskForm { get; set; }
    }
}