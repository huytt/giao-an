using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.eCommerce.Models
{
    public class CartViewModel
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public long Size { get; set; }
    }
}