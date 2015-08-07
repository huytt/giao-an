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
    
    public partial class Group
    {
        public Group()
        {
            this.ActionTypePermissions = new HashSet<ActionTypePermission>();
            this.GroupAccounts = new HashSet<GroupAccount>();
        }
    
        public long GroupId { get; set; }
        public Nullable<long> OrgRoleId { get; set; }
        public Nullable<int> GroupLevel { get; set; }
        public Nullable<long> GroupParentId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<ActionTypePermission> ActionTypePermissions { get; set; }
        public virtual ICollection<GroupAccount> GroupAccounts { get; set; }
        public virtual OrgRole OrgRole { get; set; }
    }
}
