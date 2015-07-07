using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using HTTelecom.Domain.Core.DataContext.mss;

namespace HTTelecom.WebUI.MediaSupport.ViewModels
{
    public class SearchForm
    {
        public Store store { get; set; }
        public Product product { get; set; }
        public Article article { get; set; }
    }


}