using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.MediaSupport.ViewModels
{
    public class ProductPriorityViewModel
    {
        public long groupPriorityId { get; set; }
        public IList<GroupPriority> list_GroupPriority { get; set; } 
    }
    public class ProductPriorityPatrialViewModel
    {
        public IList<ProductInPriority> list_ProductInPriority { get; set; }
        public IList<Product> list_Product { get; set; }
        public IList<GroupPriority> list_GroupPriority { get; set; }
    }
    public class CreateProductPriorityViewModel
    {
        public ProductInPriority ProductInPriorityModel { get; set; }
        public IList<Product> list_Product { get; set; }
        public IList<GroupPriority> list_GroupPriority { get; set; }
        public int orderNumberDefault { get; set; }
    }
}