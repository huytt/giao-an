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
        private const int pageSize = 12;
        //[OutputCache(VaryByParam = "none", Duration = 3600)]
        [ChildActionOnly]
        public PartialViewResult Index(string lang)
        {
            #region older
            //#region load
            //CategoryRepository _CategoryRepository = new CategoryRepository();
            //Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            //MediaRepository _MediaRepository = new MediaRepository();
            //lang = lang == null ? "vi" : lang;
            //#endregion
            //var LstCate = _CategoryRepository.GetAllAndProductCount(true, false);
            //List<Tuple<Category, int, Media, Media, Category_MultiLang>> lst = new List<Tuple<Category, int, Media, Media, Category_MultiLang>>();
            //foreach (var item in LstCate)
            //{
            //    var mediaLogo = new Media();
            //    if (item.Item1.LogoMediaId != null && item.Item1.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(item.Item1.LogoMediaId));
            //    else mediaLogo = null;
            //    var mediaBanner = new Media();
            //    if (item.Item1.BannerMediaId != null && item.Item1.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(item.Item1.BannerMediaId));
            //    else mediaBanner = null;
            //    var cate_mutil = new Category_MultiLang();
            //    cate_mutil = _Category_MultiLangRepository.GetByLanguage(item.Item1.CategoryId, lang);
            //    lst.Add(new Tuple<Category, int, Media, Media, Category_MultiLang>(item.Item1, item.Item2, mediaLogo, mediaBanner, cate_mutil));
            //}
            //ViewBag.ListCategory = lst;
            #endregion

            #region load
            CategoryRepository _CategoryRepository = new CategoryRepository();
            Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            lang = lang == null ? "vi" : lang;
            #endregion
            var LstCate = _CategoryRepository.GetAllAndProductCount(true, false);
            List<Tuple<Category, int, Media, Media>> lst = new List<Tuple<Category, int, Media, Media>>();
            foreach (var item in LstCate)
            {
                var mediaLogo = new Media();
                if (item.Item1.LogoMediaId != null && item.Item1.LogoMediaId > 0)
                    mediaLogo = _MediaRepository.GetById(Convert.ToInt64(item.Item1.LogoMediaId));
                else mediaLogo = null;
                var mediaBanner = new Media();
                if (item.Item1.BannerMediaId != null && item.Item1.BannerMediaId > 0)
                    mediaBanner = _MediaRepository.GetById(Convert.ToInt64(item.Item1.BannerMediaId));
                else mediaBanner = null;
                var cate_mutil = new Category_MultiLang();
                cate_mutil = _Category_MultiLangRepository.GetByLanguage(item.Item1.CategoryId, lang);
                item.Item1.CategoryName = cate_mutil != null ? cate_mutil.CategoryName : item.Item1.CategoryName;
                lst.Add(new Tuple<Category, int, Media, Media>(item.Item1, item.Item2, mediaLogo, mediaBanner));
            }
            var rs = Private.ConvertListCategory(lst, Url);
            ViewBag.lv1 = rs.Where(n => n.CateLevel == 0).OrderBy(n => n.OrderNumber).ToList();
            ViewBag.lv2 = rs.Where(n => n.CateLevel == 1).OrderBy(n => n.OrderNumber).ToList();
            ViewBag.lv3 = rs.Where(n => n.CateLevel == 2).OrderBy(n => n.OrderNumber).ToList();


            Private.LoadBegin(Session, ViewBag, Url);
            return PartialView();
        }
        [ChildActionOnly]
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
            Private.LoadBegin(Session, ViewBag,Url);
            return PartialView();
        }
         [OutputCache(Duration = 15, VaryByParam = "none")]
        public ActionResult Info(long id, int? step, int? typeSearch)
        {
            int pageNum = (step ?? 1);
            ViewBag.step = step;
            Private.LoadBegin(Session, ViewBag,Url);
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
            #region model info
            var mediaBanner = new Media();
            if (model.BannerMediaId != null && model.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(model.BannerMediaId));
            else
                mediaBanner = null;
            Tuple<Category, Media, Media> ModelCategory = new Tuple<Category, Media, Media>(model, null, mediaBanner);
            #endregion

            #region get Level Category
            var model_mutil = _Category_MultiLangRepository.GetByLanguage(model.CategoryId, ViewBag.currentLanguage);
            if (model_mutil != null)
            {
                model.CategoryName = model_mutil.CategoryName;
                model.Alias = model_mutil.Alias;
                model.Description = model_mutil.Description;
            }
            var lstParentCategory = _CategoryRepository.GetListParentCategory(model, (string)ViewBag.currentLanguage);
            ViewBag.cate0 = lstParentCategory.Item1;
            ViewBag.cate1 = lstParentCategory.Item2;
            #endregion


            List<Tuple<Category, int>> ListTotalCategory = _CategoryRepository.GetAllAndProductCount(model.CategoryId, ViewBag.currentLanguage);
            //ListTotalCategory = ListTotalCategory.Where(n => n.Item1.ParentCateId == model.CategoryId).ToList();
            #region Product
            var lstProduct = _ProductRepository.GetListByCategory(id);
            foreach (var item in ListTotalCategory)
                lstProduct.AddRange(_ProductRepository.GetListByCategory(item.Item1.CategoryId));
            #endregion
            var err = new List<long>();
            #region ListProductMedia
            foreach (var item in lstProduct)
            {
                var itemProductGroup = _ProductInMediaRepository.GetByGroup(Convert.ToInt64(item.GroupProductId), "STORE-3").FirstOrDefault();
                //var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
                //var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemProductGroup != null)
                    lst.Add(itemProductGroup);
                else
                {
                    err.Add(item.ProductId);
                }
            }
            lst = lst.GroupBy(n => n.Product.GroupProductId).Select(g => g.First()).ToList();
            #endregion
            ViewBag.ListChildren = ListTotalCategory.Where(n => n.Item1.ParentCateId == model.CategoryId).ToList();
            ViewBag.CountProduct = lst.Count;
            ViewBag.ListProduct = Private.ConvertListProduct(lst.Take(20).ToList(), Url);
            ViewBag.Model = ModelCategory;
            ViewBag.step = 0;
            ViewBag.u = Url.Action("Info", "Category", new { id = id, step = step, typeSearch = typeSearch });

            ViewBag.gCateLv = model.CateLevel;
            ViewBag.gCateName_1 =
                lstParentCategory.Item1 != null ?
                lstParentCategory.Item1.CategoryName : "";
            ViewBag.gCateId_1 =
                lstParentCategory.Item1 != null ?
                lstParentCategory.Item1.CategoryId : -1;
            ViewBag.gCateName_2 =
                lstParentCategory.Item2 != null ?
                lstParentCategory.Item2.CategoryName : "";
            ViewBag.gCateId_2 =
                lstParentCategory.Item2 != null ?
                lstParentCategory.Item2.CategoryId : -1;
            ViewBag.ListError = err;
            return View();
        }

        [HttpGet]
        public ActionResult ListJson()
        {
            return Json(new object { });
        }
    }
}
