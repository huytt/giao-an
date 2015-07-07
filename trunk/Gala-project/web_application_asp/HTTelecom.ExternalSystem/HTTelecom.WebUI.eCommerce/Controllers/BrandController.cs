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

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class BrandController : Controller
    {
        private const int pageSize = 12;
        [WhitespaceFilter]
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
            var model = _BrandRepository.GetById(id);
            if (model == null || model.IsActive == false || model.IsDeleted == true)
                return RedirectToAction("Index", "Home");
            var lstProduct = _ProductRepository.GetByBrand(id);
            foreach (var item in lstProduct)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemPro != null)
                    lst.Add(itemPro);
            }
            lst = lst.GroupBy(n => n.ProductId).Select(g => g.First()).ToList();
            lst = lst.GroupBy(n => n.Product.GroupProductId).Select(g => g.First()).ToList();
            ViewBag.typeSearch = typeSearch;
            if (typeSearch == 1)
                lst = lst.OrderByDescending(n => n.Product.VisitCount).ToList();
            if (typeSearch == 2)
                lst = lst.OrderByDescending(n => n.Product.PromotePrice).ToList();
            if (typeSearch == 3)
                lst = lst.OrderBy(n => n.Product.PromotePrice).ToList();
            var mediaLogo = new Media();
            if (model.LogoMediaId != null && model.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(model.LogoMediaId));
            else mediaLogo = null;
            var mediaBanner = new Media();
            if (model.BannerMediaId != null && model.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(model.BannerMediaId));
            else mediaBanner = null;
            Tuple<Brand, Media, Media> ModelBrand = new Tuple<Brand, Media, Media>(model, mediaLogo, mediaBanner);
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
            ViewBag.Model = ModelBrand;
            ViewBag.CountProduct = lst.Count;
            ViewBag.ProductInMediaHot = lst.OrderByDescending(n => n.Product.VisitCount).Take(10).ToList();
            ViewBag.ProductInMedia = lst.ToPagedList(pageNum, pageSize);
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("Index", "Brand", new { id = id });
            return View();
        }
        [WhitespaceFilter]
        public ActionResult All()
        {
            BrandRepository _BrandRepository = new BrandRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            MediaRepository _MediaRepository = new MediaRepository();
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
            Common.Common cm = new Common.Common();
            var lstProduct = _ProductInMediaRepository.GetByHome();
            var lstProductMain = _ProductInMediaRepository.GetByHaveBrand();
            var newListProduct = cm.GetRandom(lstProduct, 10);
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
            ViewBag.ListModelBrand = ListModelBrand;
            ViewBag.ListProductMain = lstProductMain;
            ViewBag.newListProduct = newListProduct;
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("All", "Brand");
            return View();
        }
    }
}
