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
    
    public partial class OrgRole
    {
        public OrgRole()
        {
            this.Accounts = new HashSet<Account>();
        }
    
        public long OrgRoleId { get; set; }
        public string OrgRoleName { get; set; }
        public Nullable<int> LevelRole { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<Account> Accounts { get; set; }
    }
}