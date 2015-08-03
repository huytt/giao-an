using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.WebUI.MediaSupport.Common;
using HTTelecom.WebUI.MediaSupport.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.MediaSupport.Controllers.Mail
{
    [SessionLoginFilter]
    public class MailManagerController : Controller
    {
        public static List<Element> _gElement = new List<Element>();
        public static List<EmailContent> _gEmailContent = new List<EmailContent>();
        public static List<Template> _gTemplate = new List<Template>();
        //
        // GET: /MailManager/

        public ActionResult Index()
        {
            //Random rd = new Random();
            //_gElement.Add(new Element { ElementId = rd.Next(100), TemplateId = rd.Next(11) });
            //ViewBag.List = _gElement;
            MailTemplateRepository _iMailtemplateService = new MailTemplateRepository();
            
            var model = _iMailtemplateService.GetList_MailTemplateAll();
      
            return View((List<MailTemplate>)model);
        }

        [HttpGet]
        public ActionResult Edit(long? id)
        {
            List<string> Error = new List<string>();
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index", "MailManager");
                }
                MailTemplateRepository _iMailTemplateService = new MailTemplateRepository();
                var _mailTemplate = _iMailTemplateService.Get_MailTemplateById((long) id);
                return View(_mailTemplate);
            }
            catch(Exception ex)
            {
                Error.Add(ex.Message);
            }
            TempData["ResponseErrorMessage"] = Error;
            return RedirectToAction("Index", "MailManager");
        }
        [HttpPost]
        public ActionResult Edit(MailTemplate _mailTemplate, string ShortDescription)
        {
            List<string> Error = new List<string>();
            MailTemplateRepository _iMailTemplateService = new MailTemplateRepository();
            AccountRepository _iAccountService = new AccountRepository();
            try
            {
                _mailTemplate.DateModified = DateTime.Now;
                Account accOnline = (Account)Session["Account"];
                _mailTemplate.ModifiedBy = accOnline.AccountId;
                _mailTemplate.Content = _mailTemplate.Content.Decode();
                if (_iMailTemplateService.UpdateMailTemplate(_mailTemplate))
                {
                    TempData["ResponseErrorMessage"] = new List<string>();
                    return View(_mailTemplate);
                }
                Error.Add("Update mail template fails.");
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
            }

            TempData["ResponseErrorMessage"] = Error;
            return View(_mailTemplate);
        }

        [HttpGet]
        public ActionResult Create()
        {
            MailTemplate mailtemp = new MailTemplate();
            mailtemp.DateCreated = DateTime.Now;
            AccountRepository _iAccountService = new AccountRepository();
            Account accOnline = (Account)Session["Account"];
            ViewBag.CreatedBy = _iAccountService.Get_AccountById(accOnline.AccountId);
            mailtemp.CreatedBy = accOnline.AccountId;
            return View(mailtemp);
        }
        public ActionResult TemplateNewMail()
        {
            try
            {

            }
            catch (Exception ex)
            {
                
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(MailTemplate _mailTemplate)
        {
            List<string> Error = new List<string>();
            try
            {
                MailTemplateRepository _iMailTemplateService = new MailTemplateRepository();
                _mailTemplate.DateCreated = DateTime.Now;
                AccountRepository _iAccountService = new AccountRepository();
                Account accOnline = (Account)Session["Account"];
                ViewBag.CreatedBy = _iAccountService.Get_AccountById(accOnline.AccountId);
                _mailTemplate.CreatedBy = accOnline.AccountId;
                _mailTemplate.Content = _mailTemplate.Content.Decode();
                _mailTemplate.IsDeleted = false;
                if (_iMailTemplateService.InsertMailTemplate(_mailTemplate)!=-1)
                {
                    TempData["ResponseErrorMessage"] = new List<string>();
                    return RedirectToAction("Index", "MailManager");
                }
                Error.Add("Create mail fails.");
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
            }

            TempData["ResponseErrorMessage"] = Error;
            return View(_mailTemplate);
        }

        [HttpPost]
        public ActionResult GetCustomerList()
        {
            List<string> Error = new List<string>();
            try
            {
                CustomerRepository _iCustomerService = new CustomerRepository();
                var lstCustomer = _iCustomerService.GetAllCustomer();
                //sẽ có 1 bộ filter ở đây
                List<object> lstObj = new List<object>();
                foreach (var item in lstCustomer)
                {
                    lstObj.Add(new
                    {
                        CustomerId = item.CustomerId,
                        CustomerName = item.FirstName + " " + item.LastName,
                        Email = item.Email
                    });
                }
                return Json(new { success = true, lstcustomer = lstObj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
            }
            return Json(new { success = false, Error = Error }, JsonRequestBehavior.AllowGet);
        }

    }
}
