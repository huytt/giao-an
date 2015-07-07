using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.ExClass;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.WebUI.MediaSupport.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.MediaSupport.Controllers
{
    [SessionLoginFilter]
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditProFile()
        {
            return View();
        }

        public ActionResult MyAccount()
        {
            Account accOnline = (Account)Session["Account"];
            if (accOnline == null)
            {
                return View("Login");
            }
            AccountRepository _iAccountService = new AccountRepository();
            DepartmentRepository _iDepartmentService = new DepartmentRepository();
            DepartmentGroupRepository _iDepartmentGroupService = new DepartmentGroupRepository();
            OrgRoleRepository _iOrgRoleService = new OrgRoleRepository();
            
            Account acc = _iAccountService.Get_AccountById(accOnline.AccountId);



            ViewBag.Department = _iDepartmentService.Get_DepartmentById(acc.DepartmentId??0);
            ViewBag.DepartmentGroup = _iDepartmentGroupService.Get_DepartmentGroupById(acc.DepartmentGroupId??0)??null;
            ViewBag.OrgRole = _iOrgRoleService.Get_OrgRoleById(acc.OrgRoleId??0);
            return View(acc);
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldpassword, string newpassword,string renewpassword)
        {
            Account accOnline = (Account)Session["Account"];
            AccountRepository _iAccountService = new AccountRepository();
            AuthenticationKeyRepository _iAuthenticationKeyService = new AuthenticationKeyRepository();

            if (newpassword.Length < 5 || newpassword.Length > 10)
            {
                ViewBag.ResponseMessage = 5;
                return View("MyAccount");
            }
            if (newpassword != renewpassword)
            {
                ViewBag.ResponseMessage = 3;
                return View("MyAccount");
            }
            AuthenticationKey secure = _iAuthenticationKeyService.Get_AuthenticationKeyById(accOnline.AuthenticationKeyId);
            if (secure == null)
            {
                ModelState.AddModelError("loginMessenger", "Security invalid !!");
                return View();
            }

            string passWordEncrypt = Security.MD5Encrypt_Custom(oldpassword, secure.HashToken, secure.SaltToken);
            Account acc = _iAccountService.LoginAccount(accOnline.Email, passWordEncrypt);

            if (acc == null)
            {
                ViewBag.ResponseMessage = 2;
                return View("MyAccount");
            }

            passWordEncrypt = Security.MD5Encrypt_Custom(newpassword, secure.HashToken, secure.SaltToken);
            if (_iAccountService.ChangePassword(accOnline.AccountId, passWordEncrypt))
            {
                ViewBag.ResponseMessage = 1;
                return View("MyAccount");
            }
            else
            {
                ViewBag.ResponseMessage = 4;
                return View("MyAccount");
            }

        }

    }
}
