//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HTTelecom.Domain.Core.DataContext.mss
{
    using System;
    
    public partial class spSearchProductPriority_Result
    {
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public long StoreId { get; set; }
        public Nullable<long> BrandId { get; set; }
        public string ProductStatusCode { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductStockCode { get; set; }
        public string ProductName { get; set; }
        public string ProductComplexName { get; set; }
        public string Alias { get; set; }
        public string Keywords { get; set; }
        public Nullable<double> RetailPrice { get; set; }
        public Nullable<double> PromotePrice { get; set; }
        public Nullable<double> MobileOnlinePrice { get; set; }
        public string ProductOutLine { get; set; }
        public string ProductSpecification { get; set; }
        public string ProductDetail { get; set; }
        public string ProductTermService { get; set; }
        public Nullable<long> VisitCount { get; set; }
        public Nullable<bool> ShowInStorePage { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<System.DateTime> DateVerified { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> OnlineDate { get; set; }
        public Nullable<System.DateTime> OfflineDate { get; set; }
        public Nullable<int> PPriority { get; set; }
    }
}