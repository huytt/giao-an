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
    using System.Collections.Generic;
    
    public partial class SearchKeyword
    {
        public long SearchKeywordId { get; set; }
        public string Keyword { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}