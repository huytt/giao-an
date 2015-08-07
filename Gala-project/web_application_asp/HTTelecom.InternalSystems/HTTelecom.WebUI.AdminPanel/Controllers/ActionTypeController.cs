using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.WebUI.AdminPanel.Filters;
using HTTelecom.WebUI.AdminPanel.ViewModels;
using PagedList;
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
        private int pageNumDefault = 1;
        private int pageSizeDefault = 50;

        #region Action Method (key:AM):
        #region 1. Action Method: Index
        //Description: // POST: /Index/--- index page
        public ActionResult Index(int? pageNum, int? pageSize, long systemTypeId = 0)
        {
            ActionTypeViewModelIndex indexModel = new ActionTypeViewModelIndex();

            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
            
            indexModel.systemTypeId = systemTypeId;
            indexModel.pageNum = pageNum ?? pageNumDefault;
            indexModel.pageSize = pageSize ?? pageSizeDefault;
            indexModel.ActionTypePager = _iActionTypeService.GetList_ActionTypeAll(indexModel.pageNum, indexModel.pageSize);

            indexModel.ddl_SystemType = _iSystemTypeService.GetList_SystemTypeAll(false);

            ViewBag.SideBarMenu = "ActionType";
            return View(indexModel);
        }
        public PartialViewResult GetActionTypeData(int? pageNum, int? pageSize, long systemTypeId = 0)
        {
            GetActionTypeDataPatrialView patrialView = new GetActionTypeDataPatrialView();
            ActionTypeViewModelIndex indexModel = new ActionTypeViewModelIndex();
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();


            IList<ActionType> list_ActionType = _iActionTypeService.GetList_ActionTypeAll(false);

            if (systemTypeId != 0)
            {
                list_ActionType = _iActionTypeService.GetList_ActionTypeAll_SystemTypeId(systemTypeId);
            }


            patrialView.ActionTypePager = list_ActionType.ToPagedList(pageNumDefault, pageSizeDefault);
            return PartialView(patrialView);
        }

        #endregion

        #region 1. Action Method: Create
        //Description: // GET: /Create/--- create page
        public ActionResult Create()
        {
            ActionTypeViewModelCreate createModel = new ActionTypeViewModelCreate();

            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

            createModel.ddl_SystemType = _iSystemTypeService.GetList_SystemTypeAll(false);
            
            ViewBag.SideBarMenu = "ActionType";
            return View(createModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create([Bind(Prefix = "ActionType")]ActionType actionTypeForm)
        {
            ActionTypeViewModelCreate createModel = new ActionTypeViewModelCreate();
            
            if (!ModelState.IsValid)
            {
                SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

                createModel.ddl_SystemType = _iSystemTypeService.GetList_SystemTypeAll(false);
                createModel.ActionType = actionTypeForm;

                return View(createModel);
            }
            else
            {
                ActionTypeRepository _iActionTypeService = new ActionTypeRepository();

                Account accOnline = (Account)Session["Account"];
                actionTypeForm.CreatedBy = accOnline.AccountId;
                
                _iActionTypeService.Insert(actionTypeForm);

                return RedirectToAction("Index", "ActionType");
            }

        }
        #endregion

        #region 1. Action Method: Edit
        //Description: // GET: /Edit/--- Edit page
        public ActionResult Edit(long id, string tabActive = "tabone", string sideBarMenu = "ActionType")
        {
            ActionTypeViewModelEdit editModel = new ActionTypeViewModelEdit();
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();

            if (tabActive != null)
            {
                ViewBag.TabIsActive = tabActive;
            }

            editModel.ActionType = _iActionTypeService.Get_ActionTypeById(id);
            editModel.ddl_SystemType = _iSystemTypeService.GetList_SystemTypeAll(false);

            return View(editModel);
        }

        //Description: // POST: /Edit/--- Edit page
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ActionType ActionTypeCollection, long ddl_systemTypeId)
        {
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();

            if (ModelState.IsValid)
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
                ActionTypeRepository _iActionTypeService = new ActionTypeRepository();

                IList<ActionType> lst_ActionType = new List<ActionType>();

                return Json(new { _listActionType = lst_ActionType, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
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


                return Json(new { Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
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
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
            ActionTypePermissionRepository _iActionTypePermissionService = new ActionTypePermissionRepository();

            ViewBag.list_ActionType = _iActionTypeService.GetList_ActionTypeAll();
            ViewBag.list_SystemType = _iSystemTypeService.GetList_SystemTypeAll();
            ViewBag.list_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll();
            ViewBag.list_Account = _iAccountService.GetList_AccountAll_IsDeleted(false);

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
        #endregion

    }
}
