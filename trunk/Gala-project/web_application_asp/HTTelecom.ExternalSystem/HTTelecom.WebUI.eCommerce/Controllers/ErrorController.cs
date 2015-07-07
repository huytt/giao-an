using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.WebUI.eCommerce.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Error(string action, string message)
        {
            //Common.Common cm = new Common.Common();
            //cm.SendMail("ứad 1", "<a href='http://galagala.vn:88/wishlist.html' >wishtlist</a>", "lauthuy12@yahoo.com.vn");
            //cm.SendMail("ứad 2 ", "<a href='http://galagala.vn/wishlist.html' >wishtlist</a>", "lauthuy12@yahoo.com.vn");
            Exception exception = Server.GetLastError();
            Private.LoadBegin(Session, ViewBag);
            ViewBag.errorAction = action;
            ViewBag.errorMessage = message;
            return View();
        }
    }
}
