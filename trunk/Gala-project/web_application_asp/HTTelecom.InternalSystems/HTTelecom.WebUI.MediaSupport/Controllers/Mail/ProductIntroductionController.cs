using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.WebUI.MediaSupport.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.MediaSupport.Controllers.Mail
{
    [SessionLoginFilter]
    public class ProductIntroductionController : Controller
    {
        //
        // GET: /ProductIntroduction/

        public ActionResult Index(long ? id)
        {
            return View();
        }
        public ActionResult PreviewMail(long? id)
        {
            MailTemplateRepository _iMailtemplateService = new MailTemplateRepository();
            MailTemplate _temp = new MailTemplate();
            if (id != null)
            {
                _temp = _iMailtemplateService.Get_MailTemplateById((long)id);
            }
            
            return View(_temp);
        }
    }
}
