using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HTTelecom.WebUI.XoneAds.Filters
{
    public class SessionLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["sessionXoneAds"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "AdsCustomer" },
                                          { "action", "Login" }
                                         });
            }
            base.OnActionExecuting(filterContext);
        }

    }
}