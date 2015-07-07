using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.WebUI.eCommerce.Common;
using HTTelecom.WebUI.eCommerce.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class ArticleController : Controller
    {
        //[OutputCache(VaryByParam = "none", Duration = 3600)]
        [WhitespaceFilter]
        public PartialViewResult Index(string lang)
        {
            lang = lang == null ? "vi" : lang;
            #region load
            ArticleRepository _ArticleRepository = new ArticleRepository();
            ArticleTypeRepository _ArticleTypeRepository = new ArticleTypeRepository();
            #endregion
            var LstArticle = _ArticleRepository.GetAll(false, lang);
            var LstArticleType = _ArticleTypeRepository.GetAll(false, lang);
            ViewBag.ListArticle = LstArticle;
            ViewBag.ListArticleType = LstArticleType;
            return PartialView();
        }
        [WhitespaceFilter]
        public ActionResult Info(long id)
        {
            #region load
            ArticleRepository _ArticleRepository = new ArticleRepository();
            ArticleTypeRepository _ArticleTypeRepository = new ArticleTypeRepository();
            #endregion
            var model = _ArticleRepository.GetById(id);
            if (model == null)
                return RedirectToAction("Index", "Home");
            ViewBag.u = Url.Action("Info", "Article", new { id = id });
            Private.LoadBegin(Session, ViewBag);
            return View(model);
        }
    }
}
