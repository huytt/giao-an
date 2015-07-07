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
    public class SystemTypeController : Controller
    {
        #region Action Method (key:AM):
        #region 1. Action Method: Index
        //Description: // POST: /Index/--- index page
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = pageNum;

            this.LoadSystemTypeFormPage(0,0);

            
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

            var lst_SystemType = _iSystemTypeService.GetList_SystemTypeAll(pageNum, 10);

            ViewBag.SideBarMenu = "SystemType";
            return View(lst_SystemType);
        }
        #endregion

        #region 1. Action Method: Create
        //Description: // GET: /Create/--- create page
        public ActionResult Create()
        {
            this.LoadSystemTypeFormPage(0,0);

            ViewBag.SideBarMenu = "SystemType";
            return View(new SystemType());
        }

        //Description: // POST: /Create/--- create page
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SystemType SystemTypeCollection)
        {
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

            if (this.ValidateSystemTypeFormPage(SystemTypeCollection) == true)
            {
                Account accOnline = (Account)Session["Account"];
                SystemTypeCollection.CreatedBy = accOnline.AccountId;

                long securityRoleId = _iSystemTypeService.Insert(SystemTypeCollection);
                if (securityRoleId != -1)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("Index", "SystemType");
                }
                else
                {
                    ModelState.AddModelError("InsertStatus", "There was an error occurs !!");
                    this.LoadSystemTypeFormPage(0,0);

                    return View(SystemTypeCollection);
                }
            }
            else
            {
                this.LoadSystemTypeFormPage(0,0);

                ViewBag.SideBarMenu = "SystemType";
                return View(new SystemType());
            }

        }
        #endregion

        #region 1. Action Method: Edit
        //Description: // GET: /Edit/--- Edit page
        public ActionResult Edit(long id, int? page, string tabActive)
        {
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();

            int pageNum = (page ?? 1);
            ViewBag.page = pageNum;

            var systemTypePage = _iSystemTypeService.Get_SystemTypeById(id);

            this.LoadSystemTypeFormPage(id, pageNum);
            if (tabActive != null)
            {
                ViewBag.TabIsActive = "tabtwo";
            }
            else
            {
                ViewBag.TabIsActive = "tabone";
            }

            ViewBag.SideBarMenu = "SystemType";
            return View(systemTypePage);
        }

        //Description: // POST: /Edit/--- Edit page
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(SystemType SystemTypeCollection)
        {
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

            if (this.ValidateSystemTypeFormPage(SystemTypeCollection) == true)
            {
                Account accOnline = (Account)Session["Account"];
                SystemTypeCollection.ModifiedBy = accOnline.AccountId;

                bool updateStatus = _iSystemTypeService.Update(SystemTypeCollection);
                if (updateStatus == true)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("Index", "SystemType");
                }
                else
                {
                    ModelState.AddModelError("UpdateStatus", "<strong>UpdateStatus</strong> There was an error occurs !!");
                    this.LoadSystemTypeFormPage(0,0);

                    return View(SystemTypeCollection);
                }

            }
            else
            {
                this.LoadSystemTypeFormPage(0,0);

                ViewBag.SideBarMenu = "SystemType";
                return View(SystemTypeCollection);
            }
        }
        #endregion

        #region 1. Action Method: ChangeActive
        //Description: // POST: /ChangeActive/
        public JsonResult ChangeActive(List<long> listSystemTypeId, int sbool, int? page)
        {
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

            int pageNum = (page ?? 1);

            bool updateStatus = false;
            foreach (var systemTypeId in listSystemTypeId)
            {
                var productUpdateIsActive = _iSystemTypeService.Get_SystemTypeById(systemTypeId);
                if (sbool == -1)
                {
                    updateStatus = _iSystemTypeService.UpdateActive(systemTypeId, (productUpdateIsActive.IsActive == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iSystemTypeService.UpdateActive(systemTypeId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iSystemTypeService.UpdateActive(systemTypeId, true);
                }
            }
            if (updateStatus == true)
            {
                var lst_SystemType = _iSystemTypeService.GetList_SystemTypeAll(pageNum, 10);

                return Json(new { _listSystemType = lst_SystemType, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
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
        private void LoadSystemTypeFormPage(long systemTypeId,int pageNum)
        {
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
            AccountRepository _iAccountService = new AccountRepository();
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

            ViewBag.list_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll_IsActive_IsDeleted(true, false);
            ViewBag.list_Account = _iAccountService.GetList_AccountAll_IsDeleted(false);
            ViewBag.list_SystemType = _iSystemTypeService.GetList_SystemTypeAll();

            if (systemTypeId != 0)
            {
                ViewBag.listSystemPermissionPager = _iSystemTypePermissionService.GetList_SystemTypePermissionAll_SystemTypeId(systemTypeId, pageNum, 10);
            }
        }
        #endregion

        #region 1. Common Method: ValidateSystemTypeFormPage
        //Description: Load form page/
        private bool ValidateSystemTypeFormPage(SystemType SystemTypeCollection)
        {
            bool valid = true;
            if (SystemTypeCollection.SystemName == null)
            {
                ModelState.AddModelError("SystemName", "SystemName  is empty !!");
                valid = false;
            }
            if (SystemTypeCollection.SystemCode == null)
            {
                ModelState.AddModelError("SystemCode", "SystemCode  is empty or don't enough 3 character !!");
                valid = false;
            }

            return valid;
        }
        #endregion
        #endregion
    }
}
