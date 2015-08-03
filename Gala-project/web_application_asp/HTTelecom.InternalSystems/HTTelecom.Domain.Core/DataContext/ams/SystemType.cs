//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HTTelecom.Domain.Core.DataContext.ams
{
    using System;
    using System.Collections.Generic;
    
    public partial class SystemType
    {
        public SystemType()
        {
            this.SystemTypePermissions = new HashSet<SystemTypePermission>();
        }
    
        public long SystemTypeId { get; set; }
        public string SystemName { get; set; }
        public string SystemCode { get; set; }
        public string URL { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<SystemTypePermission> SystemTypePermissions { get; set; }
    }
}
