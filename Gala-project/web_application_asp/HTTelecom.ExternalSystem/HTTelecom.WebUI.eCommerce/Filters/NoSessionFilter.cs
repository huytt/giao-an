using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HTTelecom.WebUI.eCommerce.Filters
{
    public class NoSessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["sessionGala"] != null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Home" },
                                          { "action", "Index" }
                                         });
            }
            base.OnActionExecuting(filterContext);
        }

    }
    public class RequreSecureConnectionFilter : RequireHttpsAttribute
    {
        public bool IsLocal
        {
            get
            {
                String remoteAddress = "www.galagala.vn:88";

                // if unknown, assume not local
                if (String.IsNullOrEmpty(remoteAddress))
                    return false;

                // check if localhost
                if (remoteAddress == "127.0.0.1" || remoteAddress == "::1")
                    return true;

                // compare with local address
                if (remoteAddress == "www.galagala.vn:88")
                    return true;

                return false;
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.HttpContext.Request.IsLocal)
            {
                // when connection to the application is local, don't do any HTTPS stuff
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}