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
    
    public partial class AdsCategory
    {
        public long AdsCategoryId { get; set; }
        public string AdsCategoryName { get; set; }
        public string ChargeRate { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
