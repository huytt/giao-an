using HTTelecom.Domain.Core.Repository.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.WebUI.AdminPanel.Filters;

namespace HTTelecom.WebUI.AdminPanel.Controllers
{
    [SessionLoginFilter]
    public class AccountController : Controller
    {
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = page;

            AccountRepository _iAccountService = new AccountRepository();

            var lst_Account = _iAccountService.GetList_AccountAll(pageNum, 10);

            return View(lst_Account);
        }

        public ActionResult Create()
        {
            LoadAccountFormPage(0);

            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Account accountCollection)
        {
            if (ValidateAccountFormPage(accountCollection) == true)
            {
                AccountRepository _iAccountService = new AccountRepository();

                long accountId = _iAccountService.Insert(accountCollection);
                if (accountId != -1)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("InsertStatus", "There was an error occurs !!");
                    LoadAccountFormPage(0);

                    return View(accountCollection);
                }
            }
            else
            {
                LoadAccountFormPage(0);

                return View(accountCollection);
            }
        }

        public ActionResult Edit(long id)
        {
            AccountRepository _iAccountService = new AccountRepository();

            var account = _iAccountService.Get_AccountById(id);

            LoadAccountFormPage(id);

            return View(account);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Account accountCollection)
        {
            if (ValidateAccountFormPage(accountCollection) == true)
            {
                AccountRepository _iAccountService = new AccountRepository();

                bool updateStatus = _iAccountService.Update(accountCollection);
                if (updateStatus == true)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    LoadAccountFormPage(0);

                    return View(accountCollection);
                }
            }
            else
            {
                LoadAccountFormPage(0);

                return View(accountCollection);
            }
        }

        public JsonResult LoadDDL_DepartmentGroup(long departmentId)
        {
            try
            {
                DepartmentGroupRepository _iDepartmentGroupService = new DepartmentGroupRepository();

                var ddl_DepartmentGroup = new SelectList(_iDepartmentGroupService.GetList_DepartmentGroup_DepartmentId(departmentId), "DepartmentGroupId", "DepartmentGroupName");
                return Json(new { ddl_DepartmentGroup = ddl_DepartmentGroup, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" });
            }
        }

        private void LoadAccountFormPage(long accountId)
        {
            DepartmentRepository _iDepartmentService = new DepartmentRepository();
            DepartmentGroupRepository _iDepartmentGroupService = new DepartmentGroupRepository();
            OrgRoleRepository _iOrgRoleService = new OrgRoleRepository();
            AccountRepository _iAccountService = new AccountRepository();

            ViewBag.list_Department = _iDepartmentService.GetList_DepartmentAll_IsActive_IsDelete(true, false);
            ViewBag.list_DepartmentGroup = _iDepartmentGroupService.GetList_DepartmentGroupAll_IsActive_IsDeleted(true, false);
            ViewBag.list_OrgRole = _iOrgRoleService.GetList_OrgRoleAll_IsActive_IsDeleted(true, false);
            ViewBag.list_Account = _iAccountService.GetList_AccountAll_IsDeleted(false);
        }
        private bool ValidateAccountFormPage(Account accountCollection)
        {
            bool valid = true;
            AccountRepository _iAccountService = new AccountRepository();
            var lst_Account = _iAccountService.GetList_AccountAll();
            if (accountCollection.StaffId == null)
            {
                ModelState.AddModelError("StaffId", "StaffID  is empty or don't enough 6 digits number !!");
                valid = false;
            }
            if (accountCollection.FullName == null)
            {
                ModelState.AddModelError("FullName", "FullName  is empty !!");
                valid = false;
            }
            if (accountCollection.Email == null)
            {
                ModelState.AddModelError("Email", "Email  is empty !!");
                valid = false;
            }
            if (accountCollection.StaffId != null || accountCollection.Email != null)
            {
                foreach (var item in lst_Account)
                {
                    if (item.AccountId != accountCollection.AccountId)
                    {
                        if (accountCollection.StaffId == item.StaffId.Substring(2) && accountCollection.AccountId == 0)
                        {
                            ModelState.AddModelError("StaffId", "StaffId  is exist !!");
                            valid = false;
                        }
                        else if (accountCollection.StaffId == item.StaffId)
                        {
                            ModelState.AddModelError("StaffId", "StaffId  is exist !!");
                            valid = false;
                        }
                        if (accountCollection.Email == item.Email)
                        {
                            ModelState.AddModelError("Email", "Email  is exist !!");
                            valid = false;
                        }
                    }
                }
            }
            return valid;
        }
    }
}
