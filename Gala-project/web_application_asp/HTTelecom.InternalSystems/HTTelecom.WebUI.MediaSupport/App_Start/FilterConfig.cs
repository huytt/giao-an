using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.MediaSupport
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}