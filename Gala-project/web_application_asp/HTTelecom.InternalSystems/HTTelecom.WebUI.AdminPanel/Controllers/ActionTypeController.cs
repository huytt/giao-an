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
    public class ActionTypeController : Controller
    {
        #region Action Method (key:AM):
        #region 1. Action Method: Index
        //Description: // POST: /Index/--- index page
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = pageNum;

            this.LoadActionTypeFormPage(null, 0, 0);


            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();

            var list_ActionTypePage = _iActionTypeService.GetList_ActionTypeAll(pageNum, 10);

            ViewBag.SideBarMenu = "ActionType";
            return View(list_ActionTypePage);
        }

        #endregion

        #region 1. Action Method: Create
        //Description: // GET: /Create/--- create page
        public ActionResult Create()
        {
            this.LoadActionTypeFormPage(null, 0, 0);

            ViewBag.SideBarMenu = "ActionType";

            ViewBag.loadBool = "NewModule";
            return View(new ActionType());
        }

        //Description: // POST: /Create/--- create page
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ActionType ActionTypeCollection, long? ddl_systemTypeId)
        {
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();
            ModuleTypeRepository _iModuleTypeService = new ModuleTypeRepository();
            ActionTypePermissionRepository _iActionTypePermissionService = new ActionTypePermissionRepository();
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();

            if (ddl_systemTypeId == null) ddl_systemTypeId = 0;

            if (this.ValidateActionTypeFormPage(ActionTypeCollection) == true)
            {
                Account accOnline = (Account)Session["Account"];
                ActionTypeCollection.CreatedBy = accOnline.AccountId;

                long securityRoleId = _iActionTypeService.Insert(ActionTypeCollection);
                if (securityRoleId != -1)
                {
                    long updateStatus = 0;

                    //Fix [Hung]
                    //if (ActionTypeCollection.SecurityRoleId != null && ddl_systemTypeId != 0)
                    //{
                    //    List<long> lst_AccountId = _iSystemTypePermissionService.GetList_SystemTypePermissionIsAccount((long)ActionTypeCollection.SecurityRoleId, (long)ddl_systemTypeId, true, false).Select(b => b.AccountId).ToList();
                    //    foreach (var accountId in lst_AccountId)
                    //    {
                    //        ActionTypePermission actionTypePermission = new ActionTypePermission();
                    //        actionTypePermission.ActionTypeId = ActionTypeCollection.ActionTypeId;
                    //        actionTypePermission.AccountId = accountId;

                    //        actionTypePermission.IsAllowed = true;
                    //        actionTypePermission.CreatedBy = ActionTypeCollection.CreatedBy;
                    //        actionTypePermission.IsDeleted = ActionTypeCollection.IsDeleted;

                    //        updateStatus = _iActionTypePermissionService.Insert(actionTypePermission);
                    //    }
                    //}

                    if (updateStatus != 0)
                    {
                        TempData["StatusMessage"] = "Success"; // Success
                        return RedirectToAction("Index", "ActionType");
                    }
                }

                ModelState.AddModelError("InsertStatus", "There was an error occurs !!");
                this.LoadActionTypeFormPage(ActionTypeCollection, (long)ddl_systemTypeId, 0);
                ViewBag.loadBool = "";
                return View(ActionTypeCollection);
            }
            else
            {
                this.LoadActionTypeFormPage(ActionTypeCollection, (long)ddl_systemTypeId, 0);

                ViewBag.SideBarMenu = "ActionType";
                ViewBag.loadBool = "";
                return View(ActionTypeCollection);
            }

        }
        #endregion

        #region 1. Action Method: Edit
        //Description: // GET: /Edit/--- Edit page
        public ActionResult Edit(long id, int? page, string tabActive)
        {
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();
            ModuleTypeRepository _iModuleTypeService = new ModuleTypeRepository();
            ActionTypePermissionRepository _iActionTypePermissionService = new ActionTypePermissionRepository();

            int pageNum = (page ?? 1);
            ViewBag.page = pageNum;

            var ActionTypePage = _iActionTypeService.Get_ActionTypeById(id);

            var moduleType = _iModuleTypeService.Get_ModuleTypeById((long)ActionTypePage.ModuleTypeId);

            this.LoadActionTypeFormPage(ActionTypePage, (long)moduleType.SystemTypeId, pageNum);
            if (tabActive != null)
            {
                ViewBag.TabIsActive = "tabtwo";
            }
            else
            {
                ViewBag.TabIsActive = "tabone";
            }

            ViewBag.SideBarMenu = "ActionType";
            ViewBag.loadBool = "";

            return View(ActionTypePage);
        }

        //Description: // POST: /Edit/--- Edit page
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ActionType ActionTypeCollection, long ddl_systemTypeId)
        {
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();
            ModuleTypeRepository _iModuleTypeService = new ModuleTypeRepository();

            if (this.ValidateActionTypeFormPage(ActionTypeCollection) == true)
            {
                Account accOnline = (Account)Session["Account"];
                ActionTypeCollection.ModifiedBy = accOnline.AccountId;

                bool updateStatus = _iActionTypeService.Update(ActionTypeCollection);
                if (updateStatus == true)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("Index", "ActionType");
                }

                ModelState.AddModelError("UpdateStatus", " There was an error occurs !!");

                this.LoadActionTypeFormPage(ActionTypeCollection, ddl_systemTypeId, 1);

                ViewBag.SideBarMenu = "ActionType";
                return View(ActionTypeCollection);
            }
            else
            {
                this.LoadActionTypeFormPage(ActionTypeCollection, ddl_systemTypeId, 1);

                ViewBag.SideBarMenu = "ActionType";
                ViewBag.TabIsActive = "tabone";
                return View(ActionTypeCollection);
            }
        }
        #endregion

        #region 1. Json Method: ChangeSystemTypeFilter
        public JsonResult ChangeSystemTypeFilter(long systemTypeId)
        {
            try
            {
                ModuleTypeRepository _iModuleTypeService = new ModuleTypeRepository();
                ActionTypeRepository _iActionTypeService = new ActionTypeRepository();
                var lst_ModuleType = _iModuleTypeService.GetList_ModuleTypeAll_SystemTypeId(systemTypeId);

                var ddl_ModuleType = new SelectList(lst_ModuleType, "ModuleTypeId", "ModuleTypeName");
                IList<ActionType> lst_ActionType = new List<ActionType>();

                foreach (var item in lst_ModuleType)
                {
                    var lt_ActionType = _iActionTypeService.GetList_ActionTypeAll_ModuleTypeId(item.ModuleTypeId);
                    foreach (var item1 in lt_ActionType)
                    {
                        lst_ActionType.Add(item1);
                    }
                }

                return Json(new { _ddlModuleType = ddl_ModuleType, _listActionType = lst_ActionType, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" });
            }
        }
        #endregion

        #region 1. Json Method: ChangeModuleFilter
        public JsonResult ChangeModuleFilter(long moduleId)
        {
            try
            {
                ActionTypeRepository _iActionTypeService = new ActionTypeRepository();

                var list_ActionType = _iActionTypeService.GetList_ActionTypeAll_ModuleTypeId(moduleId);

                return Json(new { _listActionType = list_ActionType, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" });
            }
        }
        #endregion

        #region 1. Action Method: ChangeActive
        //Description: // POST: /ChangeActive/
        public JsonResult ChangeActive(List<long> listActionTypeId, int sbool, int? page)
        {
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();

            int pageNum = (page ?? 1);

            bool updateStatus = false;
            foreach (var actionTypeId in listActionTypeId)
            {
                var actionTypeUpdateIsActive = _iActionTypeService.Get_ActionTypeById(actionTypeId);
                if (sbool == -1)
                {
                    updateStatus = _iActionTypeService.UpdateActive(actionTypeId, (actionTypeUpdateIsActive.IsActive == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iActionTypeService.UpdateActive(actionTypeId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iActionTypeService.UpdateActive(actionTypeId, true);
                }
            }
            if (updateStatus == true)
            {
                var lst_ActionType = _iActionTypeService.GetList_ActionTypeAll(pageNum, 10);

                return Json(new { _listActionType = lst_ActionType, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion

        #region Common Method (key:CM):
        #region 1. Common Method: LoadActionTypeFormPage
        //Description: Load form page/
        private void LoadActionTypeFormPage(ActionType ActionTypeCollection, long systemTypeId, int pageNum)
        {
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();
            AccountRepository _iAccountService = new AccountRepository();
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
            ModuleTypeRepository _iModuleTypeService = new ModuleTypeRepository();
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
            ActionTypePermissionRepository _iActionTypePermissionService = new ActionTypePermissionRepository();

            ViewBag.list_ActionType = _iActionTypeService.GetList_ActionTypeAll();
            ViewBag.list_ModuleType = _iModuleTypeService.GetList_ModuleTypeAll();
            ViewBag.list_SystemType = _iSystemTypeService.GetList_SystemTypeAll();
            ViewBag.list_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll();
            ViewBag.list_Account = _iAccountService.GetList_AccountAll_IsDeleted(false);
            ViewBag.ddl_ModuleType = new SelectList(_iModuleTypeService.GetList_ModuleTypeAll_SystemTypeId(systemTypeId), "ModuleTypeId", "ModuleTypeName");

            if (systemTypeId != 0)
            {
                ViewBag.systemTypeIdSelected = systemTypeId;
            }

            if (ActionTypeCollection != null)
            {
                if (ActionTypeCollection.ActionTypeId != 0)
                {
                    ViewBag.listActionPermissionPager = _iActionTypePermissionService.GetList_ActionTypePermissionAll_ActionTypeId(ActionTypeCollection.ActionTypeId, pageNum, 10);
                }
            }
        }
        #endregion

        #region 1. Common Method: ValidateActionTypeFormPage
        //Description: Load form page/
        private bool ValidateActionTypeFormPage(ActionType ActionTypeCollection)
        {
            bool valid = true;
            if (ActionTypeCollection.ActionTypeName == null)
            {
                ModelState.AddModelError("ActionTypeName", "ActionTypeName  is empty !!");
                valid = false;
            }
            if (ActionTypeCollection.ModuleTypeId == null)
            {
                ModelState.AddModelError("ModuleTypeId", "Module  is empty !!");
                valid = false;
            }


            return valid;
        }
        #endregion
        #endregion

    }
}
