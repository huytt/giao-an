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
    
    public partial class District
    {
        public District()
        {
            this.Ward = new HashSet<Ward>();
        }
    
        public long DistrictId { get; set; }
        public Nullable<long> ProvinceId { get; set; }
        public string DistrictName { get; set; }
        public string PostalCode { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual Province Province { get; set; }
        public virtual ICollection<Ward> Ward { get; set; }
    }
}
