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
    
    public partial class AdsContent
    {
        public AdsContent()
        {
            this.Ads = new HashSet<Ads>();
        }
    
        public long AdsContentId { get; set; }
        public string AdsHeader { get; set; }
        public string ImageFilePath { get; set; }
        public string VideoFilePath { get; set; }
        public string LogoFilePath { get; set; }
        public string AdsTitle { get; set; }
        public string Description { get; set; }
        public string LinkSite { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    
        public virtual ICollection<Ads> Ads { get; set; }
    }
}