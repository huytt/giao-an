using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.WebUI.AdminPanel.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.AdminPanel.Controllers
{
    [SessionLoginFilter]
    public class RolePermissionController : Controller
    {
        //#region Action Method (key:AM):
        //#region 1. Action Method: Index
        //public ActionResult Index(int? page)
        //{
        //    int pageNum = (page ?? 1);
        //    ViewBag.page = pageNum;

        //    this.LoadRolePermissionFormPage(0);


        //    RolePermissionRepository _iRolePermissionService = new RolePermissionRepository();

        //    var list_RolePermission = _iRolePermissionService.GetList_RolePermissionAll(pageNum, 10);

        //    ViewBag.SideBarMenu = "RolePermission";
        //    return View(list_RolePermission);
        //}
        //#endregion
        //#endregion

        //#region Common Method (key:CM):
        //#region 1. Common Method: LoadRolePermissionFormPage
        ////Description: Load form page/
        //private void LoadRolePermissionFormPage(long rolePermissionId)
        //{
        //    PermissionRepository _iPermissionService = new PermissionRepository();
        //    SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

        //    ViewBag.list_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll();
        //    ViewBag.list_Permission = _iPermissionService.GetList_PermissionAll();
        //}
        //#endregion
        //#endregion
    }
}
