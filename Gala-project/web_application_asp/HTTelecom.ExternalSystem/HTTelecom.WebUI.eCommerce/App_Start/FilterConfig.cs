using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.eCommerce
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new HTTelecom.WebUI.eCommerce.Filters.RequreSecureConnectionFilter());
        }
    }
}