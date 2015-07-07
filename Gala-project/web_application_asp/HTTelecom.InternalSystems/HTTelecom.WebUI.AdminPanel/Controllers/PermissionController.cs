using HTTelecom.Domain.Core.DataContext.ams;
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
    public class PermissionController : Controller
    {
        //#region Action Method (key:AM):
        //#region 1. Action Method: Index
        ////Description: // POST: /Index/--- index page
        //public ActionResult Index(int? page)
        //{
        //    int pageNum = (page ?? 1);
        //    ViewBag.page = pageNum;

        //    PermissionRepository _iPermissionService = new PermissionRepository();

        //    var lst_Permission = _iPermissionService.GetList_PermissionAll(pageNum, 10);

        //    ViewBag.SideBarMenu = "Permission";

        //    return View(lst_Permission);
        //}
        //#endregion

        //#region 1. Action Method: Create
        ////Description: // GET: /Create/--- create page
        //public ActionResult Create()
        //{
        //    this.LoadPermissionFormPage(0);

        //    ViewBag.SideBarMenu = "Permission";
        //    return View(new Permission());
        //}

        //[HttpPost, ValidateInput(false)]
        //public ActionResult Create(Permission PermissionCollection)
        //{
        //    PermissionRepository _iPermissionService = new PermissionRepository();

        //    if (this.ValidatePermissionFormPage(PermissionCollection) == true)
        //    {
        //        Account accOnline = (Account)Session["Account"];
        //        PermissionCollection.CreatedBy = accOnline.AccountId;

        //        long permissionId = _iPermissionService.Insert(PermissionCollection);
        //        if (permissionId != -1)
        //        {
        //            TempData["StatusMessage"] = "Success"; // Success
        //            return RedirectToAction("Index", "Permission");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("InsertStatus", "There was an error occurs !!");
        //            this.LoadPermissionFormPage(0);

        //            return View(PermissionCollection);
        //        }
        //    }
        //    else
        //    {
        //        this.LoadPermissionFormPage(0);

        //        ViewBag.SideBarMenu = "Permission";
        //        return View(new Permission());
        //    }

        //}
        //#endregion

        //#region 1. Action Method: Edit
        ////Description: // GET: /Edit/--- Edit page
        //public ActionResult Edit(long id)
        //{
        //    PermissionRepository _iPermissionService = new PermissionRepository();

        //    var permissionPage = _iPermissionService.Get_PermissionById(id);

        //    this.LoadPermissionFormPage(id);


        //    ViewBag.SideBarMenu = "Permission";
        //    return View(permissionPage);
        //}

        ////Description: // POST: /Edit/--- Edit page
        //[HttpPost, ValidateInput(false)]
        //public ActionResult Edit(Permission PermissionCollection)
        //{
        //    PermissionRepository _iPermissionService = new PermissionRepository();

        //    if (this.ValidatePermissionFormPage(PermissionCollection) == true)
        //    {
        //        Account accOnline = (Account)Session["Account"];
        //        PermissionCollection.ModifiedBy = accOnline.AccountId;

        //        bool updateStatus = _iPermissionService.Update(PermissionCollection);
        //        if (updateStatus == true)
        //        {
        //            TempData["StatusMessage"] = "Success"; // Success
        //            return RedirectToAction("Index", "Permission");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("UpdateStatus", "UpdateStatus, There was an error occurs !!");
        //            this.LoadPermissionFormPage(0);

        //            return View(PermissionCollection);
        //        }

        //    }
        //    else
        //    {
        //        this.LoadPermissionFormPage(0);

        //        ViewBag.SideBarMenu = "Permission";
        //        return View(PermissionCollection);
        //    }
        //}
        //#endregion

        //#region 1. Action Method: ChangeActive
        ////Description: // POST: /ChangeActive/
        //public JsonResult ChangeActive(List<long> listPermissionId, int sbool, int? page)
        //{
        //    PermissionRepository _iPermissionService = new PermissionRepository();

        //    int pageNum = (page ?? 1);

        //    bool updateStatus = false;
        //    foreach (var permissionId in listPermissionId)
        //    {
        //        var permissionUpdateIsActive = _iPermissionService.Get_PermissionById(permissionId);
        //        if (sbool == -1)
        //        {
        //            updateStatus = _iPermissionService.UpdateActive(permissionId, (permissionUpdateIsActive.IsActive == true ? false : true));
        //        }
        //        if (sbool == 0)
        //        {
        //            updateStatus = _iPermissionService.UpdateActive(permissionId, false);
        //        }
        //        if (sbool == 1)
        //        {
        //            updateStatus = _iPermissionService.UpdateActive(permissionId, true);
        //        }
        //    }
        //    if (updateStatus == true)
        //    {
        //        var lst_Permission = _iPermissionService.GetList_PermissionAll(pageNum, 10);

        //        return Json(new { _listPermission = lst_Permission, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //#endregion
        //#endregion

        //#region Common Method (key:CM):
        //#region 1. Common Method: LoadPermissionFormPage
        ////Description: Load form page/
        //private void LoadPermissionFormPage(long permissionId)
        //{
        //    AccountRepository _iAccountService = new AccountRepository();
        //    ViewBag.list_Account = _iAccountService.GetList_AccountAll_IsDeleted(false);
        //}
        //#endregion

        //#region 1. Common Method: ValidateSystemTypeFormPage
        ////Description: Load form page/
        //private bool ValidatePermissionFormPage(Permission PermissionCollection)
        //{
        //    bool valid = true;
            
        //    if (PermissionCollection.PermissionKey == null)
        //    {
        //        ModelState.AddModelError("PermissionKey", "PermissionKey is empty !!");
        //        valid = false;
        //    }
        //    if (PermissionCollection.PermissionCode == null)
        //    {
        //        ModelState.AddModelError("PermissionCode", "PermissionCode is empty or don't enough 3 character !!");
        //        valid = false;
        //    }
        //    if (PermissionCollection.PermissionName == null)
        //    {
        //        ModelState.AddModelError("PermissionName", "PermissionName is empty !!");
        //        valid = false;
        //    }
        //    return valid;
        //}
        //#endregion
        //#endregion
    }
}
