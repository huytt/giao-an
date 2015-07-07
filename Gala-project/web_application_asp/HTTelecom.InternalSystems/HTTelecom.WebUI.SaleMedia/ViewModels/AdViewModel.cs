using HTTelecom.Domain.Core.DataContext.acs;
using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.SaleMedia.ViewModels
{
    public class AdViewModel
    {
        public Ad ad { get; set; }
        public AdsContent AdsContent { get; set; }
        public AdsCustomer AdsCustomer { get; set; }
        public AdsCustomerCard AdsCustomerCard { get; set; }
        public Contract Contract { get; set; }
        public CounterCard CounterCard { get; set; }
    }
}