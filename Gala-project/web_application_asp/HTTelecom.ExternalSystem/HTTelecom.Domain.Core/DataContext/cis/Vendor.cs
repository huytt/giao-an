//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Vendor
    {
        public Vendor()
        {
            this.VendorAddress = new HashSet<VendorAddress>();
            this.VendorCard = new HashSet<VendorCard>();
        }
    
        public long VendorId { get; set; }
        public Nullable<long> ContractId { get; set; }
        public string VendorEmail { get; set; }
        public string VendorFullName { get; set; }
        public string CompanyName { get; set; }
        public string LinkWebsite { get; set; }
        public string LogoFile { get; set; }
        public string Description { get; set; }
        public Nullable<bool> CommonService { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Nullable<long> SecureAuthenticationId { get; set; }
    
        public virtual ICollection<VendorAddress> VendorAddress { get; set; }
        public virtual ICollection<VendorCard> VendorCard { get; set; }
    }
}
