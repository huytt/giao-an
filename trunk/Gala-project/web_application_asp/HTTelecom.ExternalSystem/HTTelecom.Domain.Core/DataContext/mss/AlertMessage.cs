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
    
    public partial class AlertMessage
    {
        public long AlertMessageId { get; set; }
        public string AlertMessageName { get; set; }
        public string AlertMessageContent { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
