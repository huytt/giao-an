using HTTelecom.Domain.Core.Repository.acs;
using HTTelecom.WebUI.XoneAds.Filters;
using HTTelecom.WebUI.XoneAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HTTelecom.WebUI.XoneAds.Controllers
{
    public class AdsCustomerController : Controller
    {


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(SignInModel model)
        {
            try
            {
                AdsCustomerRepository _AdsCustomerRepository = new AdsCustomerRepository();
                var Customer = _AdsCustomerRepository.Login(model.email, model.password);
                if (Customer == null)
                {
                    SetMessageCurrent(new List<string>() { "Email or password not match!" });
                    return View(model);
                }
                else
                {
                    Session.Add("sessionXoneAds", Customer);
                    //FormsAuthentication.SetAuthCookie(".ASPX_XONE_AUTH", false);
                    //HttpCookie aspxauth = Request.Cookies[".ASPX_XONE_AUTH"];
                    //aspxauth.Expires = DateTime.Now.AddDays(10d);
                    //Response.Cookies.Add(aspxauth);
                    return RedirectToAction("InforPrivate", "Home");
                }
            }
            catch (Exception ex)
            {
                SetMessageCurrent(new List<string>() { ex.Message });
                return View(model);
            }
        }
        [SessionLoginFilter]
        public ActionResult Logout()
        {
            Session.Remove("sessionXoneAds");
            return RedirectToAction("Index", "Home");
        }
        private void SetMessageCurrent(List<string> message)
        {
            if (TempData["gMessageCurrent"] != null) TempData.Remove("gMessageCurrent");
            TempData.Add("gMessageCurrent", message);
        }
    }
}
