//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HTTelecom.Domain.Core.DataContext.sms
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreStatistic
    {
        public long StoreStatisticId { get; set; }
        public Nullable<long> StoreId { get; set; }
        public string Content { get; set; }
        public Nullable<int> Counter { get; set; }
        public Nullable<int> CounterMember { get; set; }
        public string Url { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Year { get; set; }
    }
}
