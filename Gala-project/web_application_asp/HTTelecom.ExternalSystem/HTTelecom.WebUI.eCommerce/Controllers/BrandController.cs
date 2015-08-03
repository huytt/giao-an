using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PagedList;
using PagedList.Mvc;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using HTTelecom.WebUI.eCommerce.Filters;
using HTTelecom.WebUI.eCommerce.Common;
using HTTelecom.Domain.Core.DataContext.sts;
using HTTelecom.Domain.Core.Repository.sts;

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class BrandController : Controller
    {
        private const int pageSize = 12;
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult Index(long id, int? page, int? typeSearch)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = page;
            typeSearch = typeSearch == null ? 1 : typeSearch;
            #region load
            BrandRepository _BrandRepository = new BrandRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            List<ProductInMedia> lst = new List<ProductInMedia>();
            MediaRepository _MediaRepository = new MediaRepository();
            #endregion
            #region Brand
            var model = _BrandRepository.GetById(id);
            if (model == null || model.IsActive == false || model.IsDeleted == true)
                return RedirectToAction("Index", "Home");

            #endregion
            #region List Product & Filter
            //var lstProduct = _ProductRepository.GetByBrand(id);
            //foreach (var item in lstProduct)
            //{
            //    var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
            //    var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
            //    if (itemPro != null)
            //        lst.Add(itemPro);
            //}
            //lst = lst.GroupBy(n => n.ProductId).Select(g => g.First()).ToList();
            //lst = lst.GroupBy(n => n.Product.GroupProductId).Select(g => g.First()).ToList();
            //ViewBag.typeSearch = typeSearch;
            //if (typeSearch == 1)
            //    lst = lst.OrderByDescending(n => n.Product.VisitCount).ToList();
            //if (typeSearch == 2)
            //    lst = lst.OrderByDescending(n => n.Product.PromotePrice).ToList();
            //if (typeSearch == 3)
            //    lst = lst.OrderBy(n => n.Product.PromotePrice).ToList();

            #endregion
            #region Media of Brand
            var mediaLogo = new Media();
            if (model.LogoMediaId != null && model.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(model.LogoMediaId));
            else mediaLogo = null;
            var mediaBanner = new Media();
            if (model.BannerMediaId != null && model.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(model.BannerMediaId));
            else mediaBanner = null;
            Tuple<Brand, Media, Media> ModelBrand = new Tuple<Brand, Media, Media>(model, mediaLogo, mediaBanner);
            #endregion
            #region Recently viewed products
            //var sessionObject = (SessionObject)Session["sessionObject"];
            //var lstViewd = sessionObject == null ? new List<long>() : sessionObject.ListProduct;
            //ViewBag.ListProductViewed = lstViewd.Count > 0 ? true : false;
            #endregion
            ViewBag.Model = ModelBrand;
            ViewBag.CountProduct = lst.Count;
            //ViewBag.ProductInMedia = lst.ToPagedList(pageNum, pageSize);
            //ViewBag.ProductInMedia = lst.Take(10).ToList();
            _BrandRepository.UpdateVisitCount(id);
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("Index", "Brand", new { id = id });
            ViewBag.step = 0;
            #region Nhúng code thống kê
            BrandLogRepository _iBrandLogService = new BrandLogRepository();
            BrandLog stl = new BrandLog();
            stl.BrandId = id;
            _iBrandLogService.BrandInsertStatistics(stl, (Session["sessionGala"] != null));
            #endregion
            return View();
        }
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult All()
        {
            BrandRepository _BrandRepository = new BrandRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            #region List Brand
            var lstBrand = _BrandRepository.GetAll(false, true);
            List<Tuple<Brand, Media, Media>> ListModelBrand = new List<Tuple<Brand, Media, Media>>();
            foreach (var item in lstBrand)
            {
                if (item != null && item.IsActive == true && item.IsDeleted == false)
                {
                    var mediaLogo = new Media();
                    if (item.LogoMediaId != null && item.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(item.LogoMediaId));
                    else mediaLogo = null;
                    var mediaBanner = new Media();
                    if (item.BannerMediaId != null && item.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(item.BannerMediaId));
                    else mediaBanner = null;
                    ListModelBrand.Add(new Tuple<Brand, Media, Media>(item, mediaLogo, mediaBanner));
                }
            }
            ViewBag.ListModelBrand = ListModelBrand;
            #endregion
            #region Recently viewed products
            var sessionObject = (SessionObject)Session["sessionObject"];
            var lstViewd = sessionObject == null ? new List<long>() : sessionObject.ListProduct;
            ViewBag.ListProductViewed = lstViewd.Count > 0 ? true : false;
            #endregion
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("All", "Brand");
            return View();
        }
    }
}
