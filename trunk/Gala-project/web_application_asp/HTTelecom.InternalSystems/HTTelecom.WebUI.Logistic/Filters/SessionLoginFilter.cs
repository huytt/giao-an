using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HTTelecom.WebUI.Logistic.Filters
{
    public class SessionLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Account"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Home" },
                                          { "action", "Login" }
                                         });
            }

            base.OnActionExecuting(filterContext);
        }

    }

    public class PermissionFilter : ActionFilterAttribute
    {
        public long Permission_GroupId { get; set; } // 1 : Manager, 2: Call, 3: Exception
        public long[] Permission_ALLOW { get; set; }
        public long[] Permission_READ { get; set; }
        public long[] Permission_WRITE { get; set; }
        public long[] Permission_ACCESS { get; set; }
        public long[] Permission_DENY { get; set; }
        public DepartmentGroup[] Permission_GROUPDENY { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            // Check List Deny dau tien đẩy ra nếu trùng

            //Check Quyen
            if (HttpContext.Current.Session["Account"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
        new RouteValueDictionary{{ "controller", "Home" },
                                          { "action", "Login" }});
            }
            else
            {
                AccountRepository _AccountRepository = new AccountRepository();
                Account acc = (Account)HttpContext.Current.Session["Account"];
                var lstAdmin = new List<long?>();
                if (acc.GroupAccounts.Where(n => n.GroupId == Permission_GroupId).ToList().Count == 0)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Home" },
                                          { "action", "Login" }});
                }
            }

            base.OnActionExecuting(filterContext);
        }

    }
}