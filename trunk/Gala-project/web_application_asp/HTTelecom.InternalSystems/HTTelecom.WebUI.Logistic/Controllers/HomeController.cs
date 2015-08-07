﻿using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.ExClass;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.WebUI.Logistic.Common;
using HTTelecom.WebUI.Logistic.Filters;
using HTTelecom.WebUI.Logistic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.Logistic.Controllers
{
    public class HomeController : Controller
    {
        [SessionLoginFilter]
        public ActionResult Index()
        {
            Account accOnline = (Account)Session["Account"];
            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();
            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();

            SystemType st = _iSystemTypeService.Get_SystemTypeByCode(GlobalVariables.SystemCode);
            SystemTypePermission stp = _iSystemTypePermissionService.Get_SystemTypePermissionIsSecurityRole(accOnline.AccountId, st.SystemTypeId);
            SecurityRole sr = _iSecurityRoleService.Get_SecurityRoleById(stp.SecurityRoleId);

            int levelRoleAdmin = (int)_iSecurityRoleService.Get_SecurityRoleByCode("ADM").LevelRole;


            GlobalVariables.levelRoleAdmin = levelRoleAdmin;
            GlobalVariables.levelRole = (int)sr.LevelRole;

            ViewBag.SideBarMenu = "HomeIndex";
            return RedirectToAction("Logistic", "TTS");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Login(LoginForm loginForm)
        {
            AccountRepository _iAccountService = new AccountRepository();
            AuthenticationKeyRepository _iAuthenticationKeyService = new AuthenticationKeyRepository();
            if (ModelState.IsValid)
            {
                try
                {
                    SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();
                    SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
                    Account account = _iAccountService.Get_AccountByEmail(loginForm.Email);
                    SystemType st = _iSystemTypeService.Get_SystemTypeByCode(GlobalVariables.SystemCode);
                    SystemTypePermission stp = _iSystemTypePermissionService.Get_SystemTypePermissionIsSecurityRole(account.AccountId, st.SystemTypeId);
                    if (account == null)
                    {
                        ModelState.AddModelError("loginMessenger", "Email not exist !!");
                        loginForm.Password = null;
                        return View();
                    }
                    if (stp == null)
                    {
                        ModelState.AddModelError("roleErrors", "Access denied !!");
                        return View();
                    }
                    else
                    {
                        AuthenticationKey secure = _iAuthenticationKeyService.Get_AuthenticationKeyById(account.AuthenticationKeyId);
                        if (secure == null)
                        {
                            ModelState.AddModelError("loginMessenger", "Security invalid !!");
                            loginForm.Password = null;
                            return View();
                        }
                        string passWordEncrypt = Security.MD5Encrypt_Custom(loginForm.Password, secure.HashToken, secure.SaltToken);
                        Account acc = _iAccountService.LoginAccount(loginForm.Email, passWordEncrypt);
                        var lstPermiss = acc.GroupAccounts.Where(n => HTTelecom.Domain.Core.ExClass.WebAppConstant.LOGISTIC_DEPARTMENT.Contains(n.GroupId)).ToList();
                        if (lstPermiss.Count > 0)
                        {
                            acc.Password = null;
                            acc.OrgRole = null;
                            acc.OrgRoleId = null;
                            acc.Phone = null;
                            acc.StaffId = null;
                            acc.Gender = null;
                            //acc.Department = null;
                            acc.DepartmentGroup = null;
                            acc.DepartmentGroupId = null;
                            acc.DepartmentId = null;
                            Session["Account"] = acc;
                            if (lstPermiss[0].GroupId == WebAppConstant.PERMISSION_GROUP_LOGISTIC_MANAGER)
                                return RedirectToAction("LogisticManager", "TTS");
                            if (lstPermiss[0].GroupId == WebAppConstant.PERMISSION_GROUP_LOGISTIC_MANAGE_WAREHOUSE)
                                return RedirectToAction("LogisticManageWarehouse", "TTS");
                            if (lstPermiss[0].GroupId == WebAppConstant.PERMISSION_GROUP_LOGISTIC_MANAGE_DELIVERY)
                                return RedirectToAction("LogisticManageDelivery", "TTS");
                        }
                        ModelState.AddModelError("loginMessenger", "Email and Password invalid !!");
                        loginForm.Password = null;
                        return View(loginForm);
                    }
                }
                catch
                {
                    ModelState.AddModelError("loginMessenger", "There was an error occurs !!");
                    loginForm.Password = null;
                    return View(loginForm);
                }
            }
            else
            {
                loginForm.Password = null;
                return View(loginForm);
            }
        }
        public ActionResult Logout()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                Session.Remove("Account");

                return RedirectToAction("Login", "Home");
            }
        }

    }
}
