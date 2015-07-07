using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.WebUI.SaleMedia.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HTTelecom.WebUI.SaleMedia.Filters
{
    public class AccessRoleManagerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Account accOnline = (Account)HttpContext.Current.Session["Account"];
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

            SystemType st = _iSystemTypeService.Get_SystemTypeByCode(GlobalVariables.SystemCode);
            SystemTypePermission stp = _iSystemTypePermissionService.Get_SystemTypePermissionIsSecurityRole(accOnline.AccountId, st.SystemTypeId);
            SecurityRole sr = _iSecurityRoleService.Get_SecurityRoleById(stp.SecurityRoleId);
            int levelRoleAdmin = (int)_iSecurityRoleService.Get_SecurityRoleByCode("ADM").LevelRole;
            if (stp == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Home" },
                                          { "action", "Login" }
                                         });
            }
            if (sr.LevelRole > levelRoleAdmin)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Errors" },
                                          { "action", "ErrorAccess" }
                                         });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}