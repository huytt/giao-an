using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.WebUI.AdminPanel.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.AdminPanel.Controllers
{
    [SessionLoginFilter]
    public class SystemTypePermissionController : Controller
    {
        #region Action Method (key:AM):
        #region 1. Action Method: Index
        //Description: // /Index/
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 1. Json Method: Create
        //Description: // POST: /Create/--- create page
        [HttpPost, ValidateInput(false)]
        public JsonResult Create(long systemTypeId, long accountId, long securityRoleId)
        {
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();
            SystemTypePermission SystemTypePermissionUpdate = new SystemTypePermission();

            Account accOnline = (Account)Session["Account"];

            SystemTypePermissionUpdate.SystemTypeId = systemTypeId;
            SystemTypePermissionUpdate.AccountId = accountId;
            SystemTypePermissionUpdate.SecurityRoleId = securityRoleId;
            SystemTypePermissionUpdate.CreatedBy = accOnline.AccountId;

            if (this.ValidateSystemTypePermissionFormPage(SystemTypePermissionUpdate) == true)
            {
                long systemTypePermissionId = _iSystemTypePermissionService.Insert(SystemTypePermissionUpdate);

                var lst_SystemTypePermission = _iSystemTypePermissionService.GetList_SystemTypePermissionAll_SystemTypeId(systemTypeId, 1, 10);
                if (systemTypePermissionId != -1)
                {
                    return Json(new { _listSystemTypePermission = lst_SystemTypePermission, Success = true, Message = "OK!!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError("InsertStatus", "There was an error occurs !!");

                    var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToList();
                    return Json(new { errors = validationErrors, Success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                                                 .SelectMany(E => E.Errors)
                                                 .Select(E => E.ErrorMessage)
                                                 .ToList();
                return Json(new { errors = validationErrors, Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 1. Action Method: ChangeAllow
        //Description: // POST: /ChangeAllow/
        public JsonResult ChangeAllow(long systemTypeId, List<long> listSystemPermissionId, int sbool, int? page)
        {
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();

            int pageNum = (page ?? 1);
            Account accOnline = (Account)Session["Account"];

            bool updateStatus = false;
            foreach (var systemPermissionId in listSystemPermissionId)
            {
                var productUpdateIsAllowed = _iSystemTypePermissionService.Get_SystemTypePermissionById(systemPermissionId);
                if (sbool == -1)
                {
                    updateStatus = _iSystemTypePermissionService.UpdateIsAllowed(systemPermissionId, (productUpdateIsAllowed.IsAllowed == true ? false : true), accOnline.AccountId);
                }
                if (sbool == 0)
                {
                    updateStatus = _iSystemTypePermissionService.UpdateIsAllowed(systemPermissionId, false, accOnline.AccountId);
                }
                if (sbool == 1)
                {
                    updateStatus = _iSystemTypePermissionService.UpdateIsAllowed(systemPermissionId, true, accOnline.AccountId);
                }
            }
            if (updateStatus == true)
            {
                var lst_SystemTypePermission = _iSystemTypePermissionService.GetList_SystemTypePermissionAll_SystemTypeId(systemTypeId, pageNum, 10);

                return Json(new { _listSystemTypePermission = lst_SystemTypePermission, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion

        #region Common Method (key:CM):
        #region 1. Common Method: LoadSystemTypePermissionFormPage
        //Description: Load form page/
        private void LoadSystemTypePermissionFormPage(long systemTypePermissionId)
        {
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
            AccountRepository _iAccountService = new AccountRepository();
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

            ViewBag.list_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll_IsActive_IsDeleted(true, false);
            ViewBag.list_Account = _iAccountService.GetList_AccountAll_IsDeleted(false);
            ViewBag.list_SystemType = _iSystemTypeService.GetList_SystemTypeAll();
        }
        #endregion

        #region 1. Common Method: ValidateSystemTypeFormPage
        //Description: Load form page/
        private bool ValidateSystemTypePermissionFormPage(SystemTypePermission SystemTypePermissionCollection)
        {
            AccountRepository _iAccountService = new AccountRepository();
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();

            SystemTypePermission systemTypePermissionExist = _iSystemTypePermissionService.Get_SystemTypePermission_SystemTypeId_AccountId(SystemTypePermissionCollection.SystemTypeId, SystemTypePermissionCollection.AccountId);

            bool valid = true;
            if (SystemTypePermissionCollection.SecurityRoleId == 0)
            {
                ModelState.AddModelError("SecurityRoleId", "SecurityRoleId is empty !!");
                valid = false;
            }
            if (SystemTypePermissionCollection.AccountId == 0)
            {
                ModelState.AddModelError("AccountId", "Account is empty !!");
                valid = false;
            }
            if (systemTypePermissionExist != null)
            {
                ModelState.AddModelError("AccountExist", "Account is exist on system !!");
                valid = false;
            }

            return valid;
        }
        #endregion
        #endregion
    }
}
