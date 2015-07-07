using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using HTTelecom.WebUI.eCommerce.Filters;
using HTTelecom.WebUI.eCommerce.Common;

namespace HTTelecom.WebUI.eCommerce.Controllers
{

    public class CategoryController : Controller
    {
        private const int pageSize = 10;
        [WhitespaceFilter]
        //[OutputCache(VaryByParam = "none", Duration = 3600)]
        public PartialViewResult Index(string lang)
        {
            #region load
            CategoryRepository _CategoryRepository = new CategoryRepository();
            Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            lang = lang == null ? "vi" : lang;
            #endregion
            var LstCate = _CategoryRepository.GetAllAndProductCount(true, false);
            List<Tuple<Category, int, Media, Media, Category_MultiLang>> lst = new List<Tuple<Category, int, Media, Media, Category_MultiLang>>();
            foreach (var item in LstCate)
            {
                var mediaLogo = new Media();
                if (item.Item1.LogoMediaId != null && item.Item1.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(item.Item1.LogoMediaId));
                else mediaLogo = null;
                var mediaBanner = new Media();
                if (item.Item1.BannerMediaId != null && item.Item1.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(item.Item1.BannerMediaId));
                else mediaBanner = null;
                var cate_mutil = new Category_MultiLang();
                cate_mutil = _Category_MultiLangRepository.GetByLanguage(item.Item1.CategoryId, lang);
                lst.Add(new Tuple<Category, int, Media, Media, Category_MultiLang>(item.Item1, item.Item2, mediaLogo, mediaBanner, cate_mutil));
            }
            ViewBag.ListCategory = lst;
            Private.LoadBegin(Session, ViewBag);
            return PartialView();
        }

        public PartialViewResult Children(string lang, int number)
        {
            #region load
            CategoryRepository _CategoryRepository = new CategoryRepository();
            Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            lang = lang == null ? "vi" : lang;
            #endregion
            var LstCate = _CategoryRepository.GetCateLevel(number).OrderBy(n => n.OrderNumber).ToList();
            List<Tuple<Category, Media, Media, Category_MultiLang>> lst = new List<Tuple<Category, Media, Media, Category_MultiLang>>();
            foreach (var item in LstCate)
            {
                var mediaLogo = new Media();
                if (item.LogoMediaId != null && item.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(item.LogoMediaId));
                else mediaLogo = null;
                var mediaBanner = new Media();
                if (item.BannerMediaId != null && item.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(item.BannerMediaId));
                else mediaBanner = null;
                var cate_mutil = new Category_MultiLang();
                cate_mutil = _Category_MultiLangRepository.GetByLanguage(item.CategoryId, lang);
                lst.Add(new Tuple<Category, Media, Media, Category_MultiLang>(item, mediaLogo, mediaBanner, cate_mutil));
            }
            ViewBag.ListCategory = lst;
            Private.LoadBegin(Session, ViewBag);
            return PartialView();
        }
        [WhitespaceFilter]
        public ActionResult Info(long id, int? page, int? typeSearch)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = page;
            Private.LoadBegin(Session, ViewBag);
            typeSearch = typeSearch == null ? 1 : typeSearch;
            #region load
            CategoryRepository _CategoryRepository = new CategoryRepository();
            ProductInCategoryRepository _ProductInCategoryRepository = new ProductInCategoryRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            List<ProductInMedia> lst = new List<ProductInMedia>();
            MediaRepository _MediaRepository = new MediaRepository();
            Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            #endregion
            var model = _CategoryRepository.GetById(id);
            if (model == null || model.IsActive == false || model.IsDeleted == true)
                return RedirectToAction("Index", "Home");
            var model_mutil = _Category_MultiLangRepository.GetByLanguage(model.CategoryId, ViewBag.currentLanguage);
            if (model_mutil != null)
            {
                model.CategoryName = model_mutil.CategoryName;
                model.Alias = model_mutil.Alias;
                model.Description = model_mutil.Description;
            }
            if (model.CateLevel > 0 && model.ParentCateId != null && model.ParentCateId != 0)
            {
                var cate1 = _CategoryRepository.GetById(Convert.ToInt64(model.ParentCateId));
                if (cate1 != null)
                {
                    var cate1_mutil = _Category_MultiLangRepository.GetByLanguage(cate1.CategoryId, ViewBag.currentLanguage);
                    cate1.CategoryName = cate1_mutil == null ? cate1.CategoryName : cate1_mutil.CategoryName;
                    cate1.Alias = cate1_mutil == null ? cate1.Alias : cate1_mutil.Alias;
                }
                Category cate0 = null;
                if (cate1 != null && cate1.CateLevel > 0)
                    cate0 = _CategoryRepository.GetById(Convert.ToInt64(cate1.ParentCateId));
                if (cate0 != null)
                {
                    var cate0_mutil = _Category_MultiLangRepository.GetByLanguage(cate0.CategoryId, ViewBag.currentLanguage);
                    cate0.CategoryName = cate0_mutil == null ? cate0.CategoryName : cate0_mutil.CategoryName;
                    cate0.Alias = cate0_mutil == null ? cate0.Alias : cate0_mutil.Alias;
                }
                ViewBag.Cate1 = cate1;
                ViewBag.Cate0 = cate0;
            }
            var lstProduct = _ProductRepository.GetListByCategory(id);
            var lstCategoryChildren = new List<Category>();
            lstCategoryChildren = _CategoryRepository.GetListChildrenCategoryByCategoryId(id).Where(n => n.IsActive == true && n.IsDeleted == false).ToList();
            foreach (var item in lstCategoryChildren)
            {
                Category_MultiLang cate_mutil = _Category_MultiLangRepository.GetByLanguage(item.CategoryId, ViewBag.currentLanguage);
                if (cate_mutil != null)
                {
                    item.CategoryName = cate_mutil.CategoryName;
                    item.Alias = cate_mutil.Alias;
                    item.Description = cate_mutil.Description;
                }
                lstProduct.AddRange(_ProductRepository.GetListByCategory(item.CategoryId));
            }
            #region will Remove
            if (lstProduct.Count == 0)
            {
                TempData.Add("AlertMessage", "You not permission open.");
                return RedirectToAction("Index", "Home");
            }
            #endregion
            foreach (var item in lstProduct)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemPro != null)
                    lst.Add(itemPro);
            }
            lst = lst.GroupBy(n => n.ProductId).Select(g => g.First()).ToList();
            ViewBag.typeSearch = typeSearch;
            if (typeSearch == 1)
                lst = lst.OrderByDescending(n => n.Product.VisitCount).ToList();
            if (typeSearch == 2)
                lst = lst.OrderByDescending(n => n.Product.PromotePrice).ToList();
            if (typeSearch == 3)
                lst = lst.OrderBy(n => n.Product.PromotePrice).ToList();
            var lstChildren = new List<Tuple<Category, Media, Media>>();
            if (model.CateLevel != 2)
            {
                var cateLevel = model.CateLevel + 1;
                var lstChildern = lstCategoryChildren.Where(n => n.CateLevel == cateLevel).OrderBy(n => n.OrderNumber).ToList();
                foreach (var item in lstChildern)
                {
                    var mediaLogoItem = new Media();
                    if (model.LogoMediaId != null && item.LogoMediaId > 0) mediaLogoItem = _MediaRepository.GetById(Convert.ToInt64(item.LogoMediaId));
                    else mediaLogoItem = null;
                    var mediaBannerItem = new Media();
                    if (model.BannerMediaId != null && item.BannerMediaId > 0) mediaBannerItem = _MediaRepository.GetById(Convert.ToInt64(item.BannerMediaId));
                    else mediaBannerItem = null;
                    lstChildren.Add(new Tuple<Category, Media, Media>(item, mediaLogoItem, mediaBannerItem));
                }
            }
            #region model
            var mediaLogo = new Media();
            if (model.LogoMediaId != null && model.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(model.LogoMediaId));
            else mediaLogo = null;
            var mediaBanner = new Media();
            if (model.BannerMediaId != null && model.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(model.BannerMediaId));
            else mediaBanner = null;
            Tuple<Category, Media, Media> ModelCategory = new Tuple<Category, Media, Media>(model, mediaLogo, mediaBanner);
            #endregion
            ViewBag.ListChildren = lstChildren;
            ViewBag.CountProduct = lst.Count;
            ViewBag.Model = ModelCategory;
            ViewBag.ProductInMediaHot = lst.OrderByDescending(n => n.Product.VisitCount).Take(10).ToList();
            ViewBag.ProductInMedia = lst.ToPagedList(pageNum, pageSize);
            #region Recently viewed products
            var sessionObject = (SessionObject)Session["sessionObject"];
            var lstViewd = sessionObject == null ? new List<long>() : sessionObject.ListProduct;
            List<ProductInMedia> lstProductView = new List<ProductInMedia>();
            foreach (var item in lstViewd)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemPro != null)
                    lstProductView.Add(itemPro);
            }
            ViewBag.ListProductViewed = lstProductView;
            #endregion
            ViewBag.u = Url.Action("Info", "Category", new { id = id, page = page, typeSearch = typeSearch });
            return View();
        }
    }
}
