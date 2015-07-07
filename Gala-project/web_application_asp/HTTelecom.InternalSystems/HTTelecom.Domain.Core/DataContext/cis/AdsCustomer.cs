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
    
    public partial class AdsCustomer
    {
        public AdsCustomer()
        {
            this.AdsCustomerCards = new HashSet<AdsCustomerCard>();
        }
    
        public long AdsCustomerId { get; set; }
        public string RepresentativeName { get; set; }
        public string CompanyName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<long> SecureAuthenticationId { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<AdsCustomerCard> AdsCustomerCards { get; set; }
    }
}
