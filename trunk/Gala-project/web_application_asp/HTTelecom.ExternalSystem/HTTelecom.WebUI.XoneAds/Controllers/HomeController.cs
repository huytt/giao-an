using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.ops;
using HTTelecom.Domain.Core.Repository.acs;
using HTTelecom.Domain.Core.Repository.ops;
using HTTelecom.WebUI.XoneAds.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.XoneAds.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region load
            AdsRepository _AdsRepository = new AdsRepository();
            #endregion
            var lst = _AdsRepository.GetAll(false, true);
            ViewBag.ListAds = lst;
            return View();
        }
        
        public ActionResult Infor(long id)
        {
            #region load
            AdsRepository _AdsRepository = new AdsRepository();

            #endregion
            var model = _AdsRepository.GetById(id);
            if (model.IsActive == true && model.IsDeleted == false)
            {
                ViewBag.Ads = model;
                return View();
            }
            else
                return RedirectToAction("Index");
        }
        [SessionLoginFilter]
        public ActionResult InforPrivate()
        {
            try
            {
                AdsCustomerRepository _AdsCustomerRepository = new AdsCustomerRepository();
                AdsRepository _AdsRepository = new AdsRepository();
                var acc = (AdsCustomer)Session["sessionXoneAds"];
                var Customer = _AdsCustomerRepository.GetById(acc.AdsCustomerId);
                if (Customer.IsActive == true && Customer.IsDeleted == false)
                {
                    var lstAds = _AdsRepository.GetByAdsCustomer(Customer.AdsCustomerId);
                    ViewBag.ListAds = lstAds;
                    return View();
                }
                else
                    return RedirectToAction("Index");
            }
            catch
            {

                return RedirectToAction("Index");
            }
        }
    }
}
