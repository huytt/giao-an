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
    public class SecurityRoleController : Controller
    {
        #region Action Method (key:AM):
        #region 1. Action Method: Index
        //Description: // GET: /Index/
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = pageNum;

            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

            var lst_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll(pageNum, 10);

            return View(lst_SecurityRole);
        }
        #endregion

        #region 1. Action Method: Create
        // GET: /Create/
        public ActionResult Create()
        {
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

            LoadSecurityRoleFormPage(0);

            return View(new SecurityRole());
        }

        // POST: /Create/
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SecurityRole securityRoleCollection)
        {
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

            if (ValidateSecurityRoleFormPage(securityRoleCollection) == true)
            {
                Account accOnline = (Account)Session["Account"];
                securityRoleCollection.CreatedBy = accOnline.AccountId;

                long securityRoleId = _iSecurityRoleService.Insert(securityRoleCollection);
                if (securityRoleId != -1)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("Index", "SecurityRole");
                }
                else
                {
                    ModelState.AddModelError("InsertStatus", "There was an error occurs !!");
                    LoadSecurityRoleFormPage(0);

                    return View(securityRoleCollection);
                }

            }
            else
            {
                LoadSecurityRoleFormPage(0);

                return View(securityRoleCollection);
            }
        }
        #endregion

        #region 1. Action Method: Edit
        // GET: /Edit/
        public ActionResult Edit(long id)
        {
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

            SecurityRole securityRolePage = _iSecurityRoleService.Get_SecurityRoleById(id);

            LoadSecurityRoleFormPage(id);

            return View(securityRolePage);
        }

        // POST: /Edit/
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(SecurityRole securityRoleCollection, List<long> list_SecurityRoleId)
        {
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

            if (ValidateSecurityRoleFormPage(securityRoleCollection) == true)
            {
                Account accOnline = (Account)Session["Account"];
                securityRoleCollection.ModifiedBy = accOnline.AccountId;

                SecurityRole securityRole = _iSecurityRoleService.Get_SecurityRoleById(securityRoleCollection.SecurityRoleId);
                int? levelRole = securityRole.LevelRole;
                
                if(securityRoleCollection.IsDeleted == false && securityRoleCollection.LevelRole == null)
                {
                    levelRole = _iSecurityRoleService.GetList_SecurityRoleAll_IsDeleted(false).Count + 1;
                }
                else
                {
                    levelRole = securityRole.LevelRole;
                }

                bool updateStatus = _iSecurityRoleService.Update(securityRoleCollection, levelRole);
                if (updateStatus == true)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("Index", "SecurityRole");
                }
                else
                {
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    LoadSecurityRoleFormPage(0);

                    return View(securityRoleCollection);
                }
            }
            else
            {
                LoadSecurityRoleFormPage(0);

                return View(securityRoleCollection);
            }
        }
        #endregion

        #region 1. Json Method: ChangeActive
        //Description: // POST: /ChangeActive/
        public JsonResult ChangeActive(List<long> listSecurtyRoleId, int sbool, int? page)
        {
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

            int pageNum = (page ?? 1);

            bool updateStatus = false;
            foreach (var securityRoleId in listSecurtyRoleId)
            {
                var productUpdateActive = _iSecurityRoleService.Get_SecurityRoleById(securityRoleId);
                if (sbool == -1)
                {
                    updateStatus = _iSecurityRoleService.UpdateActive(securityRoleId, (productUpdateActive.IsActive == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iSecurityRoleService.UpdateActive(securityRoleId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iSecurityRoleService.UpdateActive(securityRoleId, true);
                }
            }
            if (updateStatus == true)
            {
                var lst_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll(pageNum, 10);

                return Json(new { _listSecurityRole = lst_SecurityRole, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion

        #region Common Method (key:CM):
        #region 1. Common Method: LoadProductFormPage
        //Description: Load security role form page/
        private void LoadSecurityRoleFormPage(long securityRoleId)
        {
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
            AccountRepository _iAccountService = new AccountRepository();

            ViewBag.LevelRoleView = _iSecurityRoleService.GetList_SecurityRoleAll_IsDeleted(false).Count + 1;
            ViewBag.list_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll();
            ViewBag.ddl_SecurityRole = _iSecurityRoleService.GetList_SecurityRoleAll_IsDeleted(false);
            ViewBag.list_Account = _iAccountService.GetList_AccountAll_IsDeleted(false);
        }
        #endregion

        #region 1. Common Method: ValidateSecurityRoleFormPage
        //Description: validate security role form page/
        private bool ValidateSecurityRoleFormPage(SecurityRole securityRoleCollection)
        {
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
            bool valid = true;

            if (securityRoleCollection.SecurityRoleName == null)
            {
                ModelState.AddModelError("SecurityRoleName", "SecurityRoleName  is empty !!");
                valid = false;
            }
            if (securityRoleCollection.SecurityRoleCode == null)
            {
                ModelState.AddModelError("SecurityRoleCode", "SecurityRoleCode  is empty or don't enough 3 character !!");
                valid = false;
            }

            return valid;

        }
        #endregion
        #endregion
    }
}
