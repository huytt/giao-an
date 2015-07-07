//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Log
    {
        public Log()
        {
            this.LogDetails = new HashSet<LogDetail>();
        }
    
        public long LogId { get; set; }
        public long LogTypeId { get; set; }
        public Nullable<long> CTSId { get; set; }
        public Nullable<long> ActionId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<LogDetail> LogDetails { get; set; }
    }
}
