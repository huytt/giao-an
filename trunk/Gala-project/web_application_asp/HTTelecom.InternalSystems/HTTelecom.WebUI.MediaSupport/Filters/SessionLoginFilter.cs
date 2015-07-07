using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HTTelecom.WebUI.MediaSupport.Filters
{
    public class SessionLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var url = filterContext.HttpContext.Request.Url.ToString();
            if (HttpContext.Current.Session["Account"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Home" },
                                          { "action", "Login" }
                                          ,{"ur",HttpUtility.UrlEncode(url)}
                                         });
            }

            base.OnActionExecuting(filterContext);
        }

    }
}