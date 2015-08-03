using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.DataContext.ops;
using HTTelecom.Domain.Core.DataContext.sts;
using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.lps;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.Repository.ops;
using HTTelecom.Domain.Core.Repository.sts;
using HTTelecom.Domain.Core.Repository.tts;
using HTTelecom.WebUI.eCommerce.Common;
using HTTelecom.WebUI.eCommerce.Filters;
using HTTelecom.WebUI.eCommerce.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class ProductController : Controller
    {
        #region Product
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult OrdProduct(long id)
        {
            #region load
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            ReviewRepository _ReviewRepository = new ReviewRepository();
            List<ProductInMedia> lst = new List<ProductInMedia>();
            List<ProductInMedia> lstProductBanner = new List<ProductInMedia>();
            CategoryRepository _CategoryRepository = new CategoryRepository();
            ProductInCategoryRepository _ProductInCategoryRepository = new ProductInCategoryRepository();
            BrandRepository _BrandRepository = new BrandRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            ProvineRepository _ProvineRepository = new ProvineRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            WeightRepository _WeightRepository = new WeightRepository();
            WishlistRepository _WishlistRepository = new WishlistRepository();
            #endregion
            #region model : Product
            var model = _ProductRepository.GetById(id);
            if (ProductError(model) == true || model.ProductStatusCode == null || model.ProductStatusCode == "PSC2")
                return RedirectToAction("Index", "Home");
            _ProductRepository.VisitCount(id);
            ViewBag.Model = model;
            #endregion
            Private.LoadBegin(Session, ViewBag);
            var ListMedia = _ProductInMediaRepository
                .GetByProduct(id)
                .Where(n => n.Media.IsActive == true && n.Media.IsDeleted == false).ToList();
            ViewBag.ListMedia = ListMedia;
            #region remove
            //var lstProductOther = _ProductRepository.GetByStore(Convert.ToInt64(model.StoreId));
            //foreach (var item in lstProductOther)
            //{
            //    var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
            //    var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
            //    if (itemPro != null)
            //        lst.Add(itemPro);
            //}
            //ViewBag.ProductInMediaOrtherProduct = lst;
            #endregion
            #region remove
            //var lstProductCategoryOther = _ProductInCategoryRepository.GetByProduct(Convert.ToInt64(model.ProductId));
            //var lstProduct = new List<Product>();
            //foreach (var item in lstProductCategoryOther)
            //    lstProduct.AddRange(_ProductRepository.GetListByCategory(Convert.ToInt64(item.CategoryId)).Take(10).ToList());
            #endregion
            #region Brand - Remove
            //if (model.BrandId != null)
            //{
            //    var Brand = _BrandRepository.GetById(Convert.ToInt64(model.BrandId));
            //    var mediaLogo = new Media();
            //    if (Brand.LogoMediaId != null && Brand.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(Brand.LogoMediaId));
            //    else mediaLogo = null;
            //    var mediaBanner = new Media();
            //    if (Brand.BannerMediaId != null && Brand.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(Brand.BannerMediaId));
            //    else mediaBanner = null;
            //    ViewBag.Brand = new Tuple<Brand, Media, Media>(Brand, mediaLogo, mediaBanner);
            //}
            #endregion
            #region ProductByCategory - Remove
            //Common.Common _common = new Common.Common();
            //var newListProductByCategory = _common.GetRandom(lstProduct, 10);
            //List<ProductInMedia> lstProductCategory = new List<ProductInMedia>();
            //foreach (var item in newListProductByCategory)
            //{
            //    var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
            //    var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
            //    if (itemPro != null)
            //        lstProductCategory.Add(itemPro);
            //}
            //ViewBag.ListProductCategoryOther = lstProductCategory;
            #endregion
            #region Gift
            GiftRepository _GiftRepository = new GiftRepository();
            var lstGift = _GiftRepository.GetByProduct(id);
            List<Tuple<Gift, Media>> listGift = new List<Tuple<Gift, Media>>();
            foreach (var item in lstGift)
            {
                var media = new Media();
                if (item.BannerMediaId != null)
                    media = _MediaRepository.GetById(Convert.ToInt64(item.BannerMediaId));
                else media = null;
                listGift.Add(new Tuple<Gift, Media>(item, media));
            }
            ViewBag.ListGift = listGift;
            #endregion
            #region Review
            //var lstReview = _ReviewRepository.GetByProduct(model.ProductCode);
            //ViewBag.ReviewCount = lstReview.Count;
            #endregion
            #region Shipping & Delivery
            //Type :1 District , 2:Province
            var lstWeight = _WeightRepository.GetAll();
            //var lstPro = lstWeight.Where(n => n.Type == "2").ToList();
            //var lstDis = lstWeight.Where(n => n.Type == "1").ToList();
            var lstProvince = _ProvineRepository.GetAll();
            var lstDistrict = _DistrictRepository.GetAll();
            //foreach (var item in lstPro)
            //{
            //    if (lstProvince.Where(n => n.ProvinceId == item.TargetId).ToList().Count == 0)
            //        lstProvince.Add(_ProvineRepository.GetById(Convert.ToInt64(item.TargetId)));
            //}
            ViewBag.lstDistrict = lstDistrict;
            ViewBag.lstProvince = lstProvince;
            ViewBag.lstWeight = lstWeight;
            #endregion
            #region Colour
            var lstColour = _ProductRepository.GetColour(Convert.ToInt64(model.GroupProductId));
            var ListColour = new List<ProductInMedia>();
            foreach (var item in lstColour)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT_COLOUR" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemPro != null)
                    ListColour.Add(itemPro);
            }
            ViewBag.ListColour = ListColour;
            #endregion
            #region Size, SizeGlobal
            SizeGlobalRepository _SizeGlobalRepository = new SizeGlobalRepository();
            string[] lstSizeGlobal = model.SizeGlobal == null ? null : model.SizeGlobal.Split(',');
            var ListSizeGlobal = new List<SizeGlobal>();
            if (lstSizeGlobal != null)
                ListSizeGlobal = _SizeGlobalRepository.GetByListId(lstSizeGlobal);
            string[] lstSize = model.Size == null ? null : model.Size.Split(',');
            var ListSize = new List<Size>();
            SizeRepository _SizeRepository = new SizeRepository();
            if (lstSize != null)
                ListSize = _SizeRepository.GetByListId(lstSize);
            ViewBag.ListSizeGlobal = ListSizeGlobal;
            ViewBag.ListSize = ListSize;
            #endregion
            #region Recently viewed products
            ViewedProduct(id);
            var lstViewd = ListViewedProduct();
            //List<ProductInMedia> lstProductView = new List<ProductInMedia>();
            //foreach (var item in lstViewd)
            //{
            //    var itemProduct = _ProductInMediaRepository.GetByProduct(item);
            //    var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
            //    if (itemPro != null)
            //        lstProductView.Add(itemPro);
            //}
            #endregion
            #region ProductStatus
            ProductStatusRepository _ProductStatusRepository = new ProductStatusRepository();
            var status = _ProductStatusRepository.GetByCode(model.ProductStatusCode);
            ViewBag.StatusProduct = status;
            #endregion
            #region Check Logistic Quantity [remove]
            //ProductItemRepository _ProductItemRepository = new ProductItemRepository();
            //ProductItemInSizeRepository _ProductItemInSizeRepository = new ProductItemInSizeRepository();
            //var productItem = _ProductItemRepository.GetByProductCode(model.ProductStockCode);
            //var productItemInSize = productItem == null ? null : _ProductItemInSizeRepository.GetByProductItem(productItem.ProductItemId);
            //ViewBag.StatusProductItem = productItemInSize != null && productItemInSize.Sum(_=>_.Quantity) > 0 ? true : false;
            #endregion
            #region FreeShip
            ShipRepository _ShipRepository = new ShipRepository();
            var lstShip = _ShipRepository.GetFreeShip();
            var strShip = ((string)ViewBag.multiRes.GetString("alert_freeship_1", ViewBag.CultureInfo));
            foreach (var item in lstShip)
            {
                if (item.TargetId > 0)
                {
                    var _pro = lstProvince.Where(n => n.ProvinceId == item.TargetId).FirstOrDefault();
                    strShip += string.Format("{0:0,0 đ}", item.FreeShip) + ((string)ViewBag.multiRes.GetString("alert_freeship_2", ViewBag.CultureInfo)) + _pro.ProvinceName + ",";
                }
            }
            var shipOther = lstShip.Where(n => n.TargetId == -1).FirstOrDefault();
            strShip += shipOther == null ? "" : "<br>" + ((string)ViewBag.multiRes.GetString("alert_freeship_3", ViewBag.CultureInfo)) + string.Format("{0:0,0 đ}", shipOther.FreeShip) + ((string)ViewBag.multiRes.GetString("alert_freeship_4", ViewBag.CultureInfo));
            ViewBag.Freeship = strShip;
            #endregion
            ViewBag.ListProductViewed = lstViewd.Count > 0 ? true : false;
            //ViewBag.ListReview = lstReview.OrderByDescending(n => n.DateCreated).Take(5).ToList();
            ViewBag.u = Url.Action("OrdProduct", "Product", new { id = id, urlName = model.Alias, urlNameStore = model.Store.Alias });

            var lstCart = GetCart();
            ViewBag.isAddedtocart = lstCart.Where(n => n.Item1 == id).ToList().Count > 0 ? true : false;
            #region Statistic Nhúng code thống kê
            ProductLogRepository slog = new ProductLogRepository();
            ProductLog stl = new ProductLog();
            stl.ProductId = id;
            slog.ProductInsertStatistics(stl, (Session["sessionGala"] == null) ? 1 : 2);
            #endregion
            return View();
        }
        public ActionResult TranformProduct(long id, int type)
        {
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            var model = _ProductRepository.GetById(id);
            if (ProductError(model) == true)
                return RedirectToAction("Index", "Home");
            var lstProductOther = _ProductRepository.GetByStore(Convert.ToInt64(model.StoreId));
            for (int i = 0; i < lstProductOther.Count; i++)
            {
                if (lstProductOther[i].ProductId == id)
                {
                    var k = type == 1 ? i - 1 : i + 1;
                    if (type == 1 && i == 0)
                        k = lstProductOther.Count - 1;
                    if (type == 2 && i == lstProductOther.Count - 1)
                        k = 0;
                    var hrefProduct = lstProductOther[k].ProductTypeCode == "PT1" ? "OrdProduct" : lstProductOther[k].ProductTypeCode == "PT2" ? "SaleProduct" : lstProductOther[k].ProductTypeCode == "PT3" ? "ChargeProduct" : "FreeProduct";
                    return RedirectToAction(hrefProduct, "Product", new { id = lstProductOther[k].ProductId, urlName = lstProductOther[k].Alias, urlNameStore = lstProductOther[k].Alias });
                }
            }
            // var lst = new List<>
            //foreach (var item in lstProductOther)
            //{
            //    var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
            //    var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
            //    if (itemPro != null)
            //        lst.Add(itemPro);
            //}
            return RedirectToAction("Index", "Home");

        }
        public ActionResult SaleProduct(long id)
        {
            #region load
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            #endregion
            var model = _ProductRepository.GetById(id);
            if (ProductError(model) == true)
                return RedirectToAction("Index", "Home");
            var ListMedia = _ProductInMediaRepository.GetByProduct(id);
            var lstProductOther = _ProductRepository.GetByStore(Convert.ToInt64(model.StoreId));

            List<ProductInMedia> lst = new List<ProductInMedia>();
            List<ProductInMedia> lstProductBanner = new List<ProductInMedia>();
            _ProductRepository.VisitCount(id);
            ViewBag.Model = model;
            foreach (var item in lstProductOther)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemPro != null)
                    lst.Add(itemPro);
            }
            ViewBag.ListMedia = ListMedia;
            ViewBag.ProductInMediaOrtherProduct = lst;
            CategoryRepository _CategoryRepository = new CategoryRepository();
            ProductInCategoryRepository _ProductInCategoryRepository = new ProductInCategoryRepository();
            var lstProductCategoryOther = _ProductInCategoryRepository.GetByProduct(Convert.ToInt64(model.ProductId));
            var lstProduct = new List<Product>();
            foreach (var item in lstProductCategoryOther)
                lstProduct.AddRange(_ProductRepository.GetListByCategory(Convert.ToInt64(item.CategoryId)).Take(10).ToList());
            BrandRepository _BrandRepository = new BrandRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            if (model.BrandId != null)
            {
                var Brand = _BrandRepository.GetById(Convert.ToInt64(model.BrandId));
                var mediaLogo = new Media();
                if (Brand.LogoMediaId != null && Brand.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(Brand.LogoMediaId));
                else mediaLogo = null;
                var mediaBanner = new Media();
                if (Brand.BannerMediaId != null && Brand.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(Brand.BannerMediaId));
                else mediaBanner = null;
                ViewBag.Brand = new Tuple<Brand, Media, Media>(Brand, mediaLogo, mediaBanner);
            }
            Common.Common _common = new Common.Common();
            var newListProductByCategory = _common.GetRandom(lstProduct, 10);
            List<ProductInMedia> lstProductCategory = new List<ProductInMedia>();
            foreach (var item in newListProductByCategory)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemPro != null)
                    lstProductCategory.Add(itemPro);
            }
            ViewBag.ListProductCategoryOther = lstProductCategory;
            GiftRepository _GiftRepository = new GiftRepository();
            var lstGift = _GiftRepository.GetByProduct(id);
            List<Tuple<Gift, Media>> listGift = new List<Tuple<Gift, Media>>();
            foreach (var item in lstGift)
            {
                var media = new Media();
                if (item.BannerMediaId != null)
                    media = _MediaRepository.GetById(Convert.ToInt64(item.BannerMediaId));
                else media = null;
                listGift.Add(new Tuple<Gift, Media>(item, media));
            }
            var lstCart = GetCart();
            ViewBag.ListGift = listGift;
            ViewBag.u = Url.Action("OrdProduct", "Product", new { id = id });
            Private.LoadBegin(Session, ViewBag);
            ViewBag.isAddedtocart = lstCart.Where(n => n.Item1 == id).ToList().Count > 0 ? true : false;
            return View();
        }
        public ActionResult ChargeProduct(long id)
        {
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("ChargeProduct", "Product", new { id = id });
            return View();
        }
        public ActionResult FreeProduct(long id)
        {
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("FreeProduct", "Product", new { id = id });
            return View();
        }
        public ActionResult Priority()
        {
            Private.LoadBegin(Session, ViewBag);
            return View();
        }
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult ProductView(string _type, int? _step, string _sort, int? _orderby)
        {
            _type = _type == "" || _type == null ? "SELLING" : _type;
            _sort = _sort == "" || _sort == null || !(_sort == "desc" || _sort == "asc") ? "desc" : _sort;
            _orderby = _orderby == null || !(_orderby >= 1 && _orderby <= 5) ? 1 : _orderby;//có 5 kiểu sort
            _step = _step ?? -1;
            ViewBag.Type = _type;
            ViewBag.Sort = _sort;
            ViewBag.Step = _step;
            ViewBag.OrderBy = _orderby;

            Private.LoadBegin(Session, ViewBag);
            return View();
        }
        [HttpGet]
        public ActionResult Review(long id)
        {
            #region load
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            #endregion
            var model = _ProductRepository.GetById(id);
            var ListMedia = _ProductInMediaRepository.GetByProduct(id);
            var lstProductOther = _ProductRepository.GetByStore(Convert.ToInt64(model.StoreId));
            List<ProductInMedia> lst = new List<ProductInMedia>();
            List<ProductInMedia> lstProductBanner = new List<ProductInMedia>();
            _ProductRepository.VisitCount(id);
            ViewBag.Model = model;
            foreach (var item in lstProductOther)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3").FirstOrDefault();
                if (itemPro != null)
                    lst.Add(itemPro);
            }
            ViewBag.ListMedia = ListMedia;
            ViewBag.ProductInMediaOrtherProduct = lst;
            CategoryRepository _CategoryRepository = new CategoryRepository();
            ProductInCategoryRepository _ProductInCategoryRepository = new ProductInCategoryRepository();
            var lstProductCategoryOther = _ProductInCategoryRepository.GetByProduct(Convert.ToInt64(model.ProductId));
            var lstProduct = new List<Product>();
            foreach (var item in lstProductCategoryOther)
                lstProduct.AddRange(_ProductRepository.GetListByCategory(Convert.ToInt64(item.CategoryId)).Take(10).ToList());
            BrandRepository _BrandRepository = new BrandRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            if (model.BrandId != null)
            {
                var Brand = _BrandRepository.GetById(Convert.ToInt64(model.BrandId));
                var mediaLogo = new Media();
                if (Brand.LogoMediaId != null && Brand.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(Brand.LogoMediaId));
                else mediaLogo = null;
                var mediaBanner = new Media();
                if (Brand.BannerMediaId != null && Brand.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(Brand.BannerMediaId));
                else mediaBanner = null;
                ViewBag.Brand = new Tuple<Brand, Media, Media>(Brand, mediaLogo, mediaBanner);
            }
            Common.Common _common = new Common.Common();
            var newListProductByCategory = _common.GetRandom(lstProduct, 10);
            List<ProductInMedia> lstProductCategory = new List<ProductInMedia>();
            foreach (var item in newListProductByCategory)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemPro != null)
                    lstProductCategory.Add(itemPro);
            }
            ViewBag.ListProductCategoryOther = lstProductCategory;
            GiftRepository _GiftRepository = new GiftRepository();
            var lstGift = _GiftRepository.GetByProduct(id);
            List<Tuple<Gift, Media>> listGift = new List<Tuple<Gift, Media>>();
            foreach (var item in lstGift)
            {
                var media = new Media();
                if (item.BannerMediaId != null)
                    media = _MediaRepository.GetById(Convert.ToInt64(item.BannerMediaId));
                else media = null;
                listGift.Add(new Tuple<Gift, Media>(item, media));
            }
            ViewBag.ListGift = listGift;
            ViewBag.u = Url.Action("OrdProduct", "Product", new { id = id });
            //LoadBegin();
            Private.LoadBegin(Session, ViewBag);
            var lstCart = new List<Tuple<long, int>>();
            ViewBag.isAddedtocart = false;
            ViewBag.WishLists = new List<Wishlist>();
            return View();
        }
        #endregion
        #region WishLish
        [SessionLoginFilter]
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult WishList()
        {
            Private.LoadBegin(Session, ViewBag);
            //LoadBegin();
            ViewBag.u = Url.Action("WishList", "Product");
            var acc = (Customer)Session["sessionGala"];
            WishlistRepository _WishlistRepository = new WishlistRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            List<ProductInMedia> lstProduct = new List<ProductInMedia>();
            var lstWishList = _WishlistRepository.GetByCustomer(acc.CustomerId).Where(n => n.IsActive == true).ToList();
            var lstNew = new List<Wishlist>();
            //var lstSizeProduct = new List<Tuple<long, string>>();
            foreach (var item in lstWishList)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId).Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (ProductError(itemProduct.Product) == false)
                {
                    lstProduct.Add(itemProduct);
                    lstNew.Add(item);
                    //lstSizeProduct.Add(new Tuple<long, string>(Convert.ToInt64(itemProduct.ProductId), itemProduct.Product.ProductStockCode));
                }
            }
            //#region Check Quantity from Logitisc
            //ProductItemInSizeRepository _ProductItemInSizeRepository = new ProductItemInSizeRepository();
            //var lstSizeLogistic = _ProductItemInSizeRepository.GetByListProduct(lstSizeProduct);
            //ViewBag.ListSizeLogistic = lstSizeLogistic;
            //#endregion
            //#region GetSize
            //SizeRepository _SizeRepository = new SizeRepository();
            //var ListSize = _SizeRepository.GetAll(false);
            //ViewBag.ListSize = ListSize;
            //#endregion
            ViewBag.Carts = GetCart();
            ViewBag.WishLists = lstNew;
            ViewBag.ProductInMedias = lstProduct;
            return View();
        }
        #endregion
        #region Cart
        public string CartMoney()
        {
            var lstCart = GetCart();
            ProductRepository _ProductRepository = new ProductRepository();
            var lstProduct = new List<Tuple<Product, ProductInMedia>>();
            double value = 0;
            foreach (var item in lstCart)
            {
                var itemProduct = _ProductRepository.GetById(item.Item1);
                value += itemProduct.PromotePrice != null ? Convert.ToDouble(itemProduct.PromotePrice) : Convert.ToDouble(itemProduct.MobileOnlinePrice);
            }
            return string.Format("{0:0,0 đ}", value).ToString();
        }

        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult Cart()
        {
            Private.LoadBegin(Session, ViewBag);
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            SizeRepository _SizeRepository = new SizeRepository();
            #endregion
            var lstCart = GetCart();
            var lstProduct = new List<Tuple<Product, ProductInMedia>>();
            var lstSizeProduct = new List<Tuple<long, string>>();
            List<CartViewModel> lstmodel = new List<CartViewModel>();
            foreach (var item in lstCart)
            {
                var itemProduct = _ProductRepository.GetById(item.Item1);
                var itemInMedia = _ProductInMediaRepository.GetByProduct(item.Item1).Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
                if (itemInMedia == null)
                    itemInMedia = new ProductInMedia();
                if (itemProduct != null)
                {
                    lstProduct.Add(new Tuple<Product, ProductInMedia>(itemProduct, itemInMedia));
                    lstmodel.Add(new CartViewModel { ProductId = itemProduct.ProductId, Quantity = item.Item2, Size = item.Item3 });
                    lstSizeProduct.Add(new Tuple<long, string>(itemProduct.ProductId, itemProduct.ProductStockCode));
                }
            }

            #region Check Quantity from Logitisc
            //ProductItemInSizeRepository _ProductItemInSizeRepository = new ProductItemInSizeRepository();
            //var lstSizeLogistic = _ProductItemInSizeRepository.GetByListProduct(lstSizeProduct);
            //ViewBag.ListSizeLogistic = lstSizeLogistic;
            #endregion
            #region GetSize
            var ListSize = _SizeRepository.GetAll(false);
            ViewBag.ListSize = ListSize;
            #endregion
            ViewBag.ListProduct = lstProduct;
            ViewBag.u = Url.Action("Cart", "Product");
            return View(lstmodel);
        }

        [HttpPost]
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult Cart(List<CartViewModel> model, FormCollection formData)
        {
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            SizeRepository _SizeRepository = new SizeRepository();
            #endregion
            var lstCart = GetCart();
            var newCart = new List<Tuple<long, int, long>>();
            var lstProduct = new List<Tuple<Product, ProductInMedia>>();
            try
            {
                if (model == null || model.Where(n => n.Quantity == null || n.Size == 0).ToList().Count > 0)
                {

                    if (model == null || model.Count == 0)
                        throw new Exception("You do not have the product.");
                    else
                        throw new Exception("Please select size.");
                }
                foreach (var item in model)
                {
                    if (item != null && item.Quantity != null && item.Quantity > 0 && item.Quantity <= 50 && item.Size > 0)
                    {
                        var itemProduct = _ProductRepository.GetById(item.ProductId);
                        if (ProductError(itemProduct) == true)
                            throw new Exception(ViewBag.multiRes.GetString("product", ViewBag.CultureInfo) + ":" + itemProduct.ProductName + (string)ViewBag.multiRes.GetString("alert_product_close", ViewBag.CultureInfo));
                        else
                        {
                            //#region Check kho còn hàng ko
                            //ProductItemRepository _ProductItemRepository = new ProductItemRepository();
                            //ProductItemInSizeRepository _ProductItemInSizeRepository = new ProductItemInSizeRepository();
                            //var productItem = _ProductItemRepository.GetByProductCode(itemProduct.ProductStockCode);
                            //if (productItem == null) throw new Exception("Product: " + itemProduct.ProductName + " [out of stock]");
                            //var productSize = _ProductItemInSizeRepository.GetByProductItem(productItem.ProductItemId);
                            //if (productSize == null || productSize.Sum(_ => _.Quantity) < item.Quantity) throw new Exception("Product: " + itemProduct.ProductName + " [out of stock]");
                            //#endregion
                            if (itemProduct.ProductStatusCode != "PSC1")
                                throw new Exception("Product: " + itemProduct.ProductName + " [out of stock]");
                            newCart.Add(new Tuple<long, int, long>(item.ProductId, item.Quantity, item.Size));
                        }
                    }
                }
                Private.SaveCart(newCart, Session);
                return RedirectToAction("Payment");
            }
            catch (Exception ex)
            {
                var lstSizeProduct = new List<Tuple<long, string>>();
                #region catch
                if (model != null)
                {
                    model = model.Where(n => n.Quantity > 0).ToList();
                    foreach (var itemP in model)
                    {
                        var itemPr = _ProductRepository.GetById(itemP.ProductId);
                        var itemInMedia = _ProductInMediaRepository.GetByProduct(itemP.ProductId).Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
                        if (itemInMedia == null)
                            itemInMedia = new ProductInMedia();
                        if (itemPr != null)
                        {
                            lstProduct.Add(new Tuple<Product, ProductInMedia>(itemPr, itemInMedia));
                            lstSizeProduct.Add(new Tuple<long, string>(itemPr.ProductId, itemPr.ProductStockCode));
                        }
                    }
                }
                else model = new List<CartViewModel>();

                #region Check Quantity from Logitisc
                ProductItemInSizeRepository _ProductItemInSizeRepository = new ProductItemInSizeRepository();
                var lstSizeLogistic = _ProductItemInSizeRepository.GetByListProduct(lstSizeProduct);
                ViewBag.ListSizeLogistic = lstSizeLogistic;
                #endregion
                ViewBag.ListProduct = lstProduct;
                Private.LoadBegin(Session, ViewBag);
                Private.SetMessageCurrent(new List<string>() { ex.Message }, TempData);
                ViewBag.u = Url.Action("Cart", "Product");
                var ListSize = _SizeRepository.GetAll(false);
                ViewBag.ListSize = ListSize;
                return View(model);
                #endregion
            }
        }
        #endregion
        #region BuyNow
        [OutputCache(Duration = 5, VaryByParam = "id")]
        public ActionResult BuyNow(long id)
        {
            //LoadBegin();
            Private.LoadBegin(Session, ViewBag);
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            SizeRepository _SizeRepository = new SizeRepository();
            #endregion
            var item = _ProductRepository.GetById(id);
            var itemInMedia = _ProductInMediaRepository.GetByProduct(item.ProductId)
                .Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
            if (ProductError(item) == true)
                return RedirectToAction("Index", "Home");
            ViewBag.Product = item;
            ViewBag.ProductInMedia = itemInMedia;
            ViewBag.u = Url.Action("BuyNow", "Product", new { id = id });
            var ListSize = _SizeRepository.GetAll(false);
            ViewBag.ListSize = ListSize;
            ViewBag.Product = item;
            var lstSizeProduct = item.Size.Split(',');
            var sizeOne = lstSizeProduct.Count() == 1 && lstSizeProduct[0] != "" ? ListSize.Where(n => n.SizeId == Convert.ToInt64(lstSizeProduct[0])).FirstOrDefault() : null;
            if (sizeOne != null && sizeOne.Code == "OneSize")
                ViewBag.Size = sizeOne.SizeId;
            else
                ViewBag.Size = 0;
            ViewBag.ProductId = id;
            ViewBag.quantity = 1;
            return View();
        }
        [HttpPost]
        public ActionResult BuyNow(FormCollection formData)
        {
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            SizeRepository _SizeRepository = new SizeRepository();
            #endregion
            long i = 0; int j = 0;
            var id = formData["ProductId"];
            var Size = formData["SizeId"];
            long ProvinceId = 0;
            long.TryParse(formData["ProvinceId"] == null ? "0" : formData["ProvinceId"].ToString(), out ProvinceId);
            long DistrictId = 0;
            long.TryParse(formData["DistrictId"] == null ? "0" : formData["DistrictId"].ToString(), out DistrictId);
            if (id == null || long.TryParse(id, out i) == false) return RedirectToAction("Index", "Home");
            var item = _ProductRepository.GetById(Convert.ToInt64(id));
            var lstSize = item.Size.Split(',');
            Private.LoadBegin(Session, ViewBag);
            var flagSize = false;
            if (lstSize.Length > 0)
                foreach (var itemSize in lstSize)
                    if (itemSize == Size)
                        flagSize = true;
            try
            {
                if (flagSize == false)
                    SetMessageCurrent(new List<string>() { (string)ViewBag.multiRes.GetString("alert_input_size_product", ViewBag.CultureInfo) });
                var quantity = formData["numBuyNow"];
                if (quantity == null || int.TryParse(quantity, out j) == false || Convert.ToInt32(quantity) <= 0 || Convert.ToInt32(quantity) > 50 || flagSize == false)
                    throw new Exception((string)ViewBag.multiRes.GetString("alert_input_number_quantity", ViewBag.CultureInfo));
                else
                {
                    var sessionObject = (SessionObject)Session["sessionObject"];
                    if (sessionObject == null)
                    {
                        SessionObject sO = new SessionObject();
                        sO.PaymentProduct = new List<Tuple<long, int, long, long, long>>() { new Tuple<long, int, long, long, long>(Convert.ToInt64(id), Convert.ToInt32(quantity), Convert.ToInt64(Size), ProvinceId, DistrictId) };
                        Session.Add("sessionObject", sO);
                    }
                    else
                    {
                        sessionObject.PaymentProduct = new List<Tuple<long, int, long, long, long>>() { new Tuple<long, int, long, long, long>(Convert.ToInt64(id), Convert.ToInt32(quantity), Convert.ToInt64(Size), ProvinceId, DistrictId) };
                        Session["sessionObject"] = sessionObject;
                    }
                    return RedirectToAction("Payment");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Product = item;
                ViewBag.Size = Size;
                ViewBag.u = Url.Action("BuyNow", "Product", new { id = Convert.ToInt64(id) });
                ViewBag.ProductId = Convert.ToInt64(id);
                ViewBag.quantity = 1;
                var ListSize = _SizeRepository.GetAll(false);
                ViewBag.ListSize = ListSize;
                var itemInMedia = _ProductInMediaRepository.GetByProduct(item.ProductId).Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
                ViewBag.ProductInMedia = itemInMedia;
                SetMessageCurrent(new List<string>() { ex.Message });
                return View();
            }
        }
        #endregion
        #region Payment & BPN
        [SessionLoginFilter]
        public ActionResult Payment()
        {
            if (Session["sessionGala"] == null)
                return RedirectToAction("Login", "Customer", new { ur = Url.Action("Payment", "Product") });
            var sessionObject = (SessionObject)Session["sessionObject"];
            if (sessionObject == null || sessionObject.PaymentProduct == null || sessionObject.PaymentProduct.Count == 0)
                return RedirectToAction("Index", "Home");
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            BankRepository _BankRepository = new BankRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            ProvineRepository _ProvineRepository = new ProvineRepository();
            List<Product> lstProduct = new List<Product>();
            WeightRepository _WeightRepository = new WeightRepository();
            ShipRepository _ShipRepository = new ShipRepository();
            SizeRepository _SizeRepository = new SizeRepository();
            var lstWeight = new List<Tuple<long, int>>();
            #endregion
            var lst = sessionObject.PaymentProduct;
            var lstSize = new List<Size>();
            lstSize = _SizeRepository.GetAll(false);
            foreach (var item in lst)
            {
                var itemProduct = _ProductRepository.GetById(item.Item1);
                if (ProductError(itemProduct) == false)
                {
                    if (itemProduct.IsWeight != null && itemProduct.IsWeight == true)
                        lstWeight.Add(new Tuple<long, int>(item.Item1, item.Item2));
                    lstProduct.Add(itemProduct);
                    #region Statistic Nhúng code thống kê
                    ProductLogRepository slog = new ProductLogRepository();
                    ProductLog stl = new ProductLog();
                    stl.ProductId = itemProduct.ProductId;
                    slog.ProductInsertStatistics(stl,3);//add to buy page
                    #endregion
                    //lstSize.Add(_SizeRepository.GetById(Convert.ToInt64(itemProduct.Size)));
                }
            }
            ViewBag.Shippingfee = lst[0].Item4 != null && lst[0].Item4 > 0 ? _WeightRepository.GetShippingFee(lst[0].Item4, lstWeight) : 0;
            ViewBag.ListSize = lstSize;
            ViewBag.Ship = lst[0].Item4 != null && lst[0].Item4 > 0 ? _ShipRepository.GetByTarget(lst[0].Item4, "2") : null;
            ViewBag.Products = lstProduct;
            ViewBag.Districts = _DistrictRepository.GetAll();
            ViewBag.Provines = _ProvineRepository.GetAll();
            ViewBag.Banks = _BankRepository.GetAll(false);
            ViewBag.ProvinceId = sessionObject.PaymentProduct[0].Item4;
            ViewBag.DistrictId = sessionObject.PaymentProduct[0].Item5;
            ViewBag.Lst = lst;
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("Payment", "Product");
            var model = new Order();
            ViewBag.PaymentTypeName = "online_pay";

            return View(model);
        }
        [HttpPost, SessionLoginFilter]
        //[WhitespaceFilter]
        public ActionResult Payment(Order model, FormCollection formData)
        {
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            ProvineRepository _ProvinceRepository = new ProvineRepository();
            BankRepository _BankRepository = new BankRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            MainRecordRepository _MainRecordRepository = new MainRecordRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            SubRecordRepository _SubRecordRepository = new SubRecordRepository();
            AccountRepository _AccountRepository = new AccountRepository();
            ProvineRepository _ProvineRepository = new ProvineRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            SizeRepository _SizeRepository = new SizeRepository();
            #endregion
            try
            {
                Int32 outInt = 0;
                Private.LoadBegin(Session, ViewBag);
                var sessionObject = (SessionObject)Session["sessionObject"];
                if (sessionObject == null || sessionObject.PaymentProduct == null || sessionObject.PaymentProduct.Count == 0)
                    return RedirectToAction("Index", "Home");
                ViewBag.u = Url.Action("Payment", "Product");
                ViewBag.PaymentTypeName = formData["_PaymentTypeName"];
                ViewBag.ProvinceId = formData["ProvinceId"];
                ViewBag.radCardType = formData["radCardType"];
                ViewBag.DistrictId = formData["DistrictId"];
                ViewBag.chkAggre = formData["chkAggre"];
                ViewBag.card_number_1 = formData["card_number_1"];
                ViewBag.card_number_4 = formData["card_number_4"];
                ViewBag.card_number_2 = formData["card_number_2"];
                ViewBag.card_number_3 = formData["card_number_3"];
                #region check Error
                if (Int32.TryParse(ViewBag.card_number_1, out outInt) && Int32.TryParse(ViewBag.card_number_2, out outInt) && Int32.TryParse(ViewBag.card_number_3, out outInt) && Int32.TryParse(ViewBag.card_number_4, out outInt))
                    model.CardNumber = Convert.ToInt64(((string)ViewBag.card_number_1).Trim() + ((string)ViewBag.card_number_2).Trim() + ((string)ViewBag.card_number_3).Trim() + ((string)ViewBag.card_number_4).Trim());
                var paymentType = (string)formData["_PaymentTypeName"];
                if (paymentType != null)
                    model.PaymentTypeCode = paymentType == "online_pay" ? "PTC-1" : paymentType == "pre_pay" ? "PTC-2" : paymentType == "post_pay" ? "PTC-3" : "";
                var error = false;
                if (ViewBag.radCardType != "radCard_Atm" && ViewBag.radCardType != "radCard_Credit" && ViewBag.radCardType != "radCard_Debate" && paymentType != null && paymentType == "pre_pay")
                {
                    ModelState.AddModelError("radCardType", (string)ViewBag.multiRes.GetString("alert_select_card_type", ViewBag.CultureInfo));
                    error = true;
                    ViewBag.radCardType = "";
                }
                if ((ViewBag.chkAggre == null || ViewBag.chkAggre != "on"))
                {
                    ModelState.AddModelError("chkAggre", (string)ViewBag.multiRes.GetString("alert_check_agree_my_company", ViewBag.CultureInfo));
                    error = true;
                }
                if (formData["ProvinceId"] == null || formData["ProvinceId"] == "" || Convert.ToInt64(formData["ProvinceId"]) <= 0 || formData["DistrictId"] == null || formData["DistrictId"] == "" || Convert.ToInt64(formData["DistrictId"]) <= 0)
                {
                    if (formData["ProvinceId"] == null || formData["ProvinceId"] == "")
                        ModelState.AddModelError("ProvinceId", (string)ViewBag.multiRes.GetString("alert_select_provice", ViewBag.CultureInfo));
                    if (formData["DistrictId"] == null || formData["DistrictId"] == "")
                        ModelState.AddModelError("DistrictId", (string)ViewBag.multiRes.GetString("alert_select_district", ViewBag.CultureInfo));
                    error = true;
                }
                #endregion
                if (checkOrder(model) == true || error == true)
                    throw new Exception("Please fill out information");
                Province Province = _ProvinceRepository.GetById(Convert.ToInt64(formData["ProvinceId"]));
                District District = _DistrictRepository.GetById(Convert.ToInt64(formData["DistrictId"]));
                WeightRepository _WeightRepository = new WeightRepository();
                ProductItemRepository _ProductItemRepository = new ProductItemRepository();
                ProductItemInSizeRepository _ProductItemInSizeRepository = new ProductItemInSizeRepository();
                ShipRepository _ShipRepository = new ShipRepository();
                var ship = _ShipRepository.GetByTarget(Convert.ToInt64(formData["ProvinceId"]), "2");
                if (ship == null)
                    throw new Exception("You don't choice Province");
                var lst = sessionObject.PaymentProduct;
                var acc = (Customer)Session["sessionGala"];
                var lstOrderDetail = new List<OrderDetail>();
                decimal totalPrice = 0;
                var lstProduct_Weight = new List<Tuple<long, int>>();
                foreach (var item in lst)
                {
                    var ProductItem = _ProductRepository.GetById(item.Item1);
                    OrderDetail odDetail = new OrderDetail();
                    //var Item = _ProductItemRepository.GetByProductCode(ProductItem.ProductStockCode);
                    if (ProductItem.ProductStatusCode != "PSC1")
                        throw new Exception("Product: " + ProductItem.ProductName + " [out of stock]");
                    //else
                    //{
                    //    var productInSize = _ProductItemInSizeRepository.GetByProductItem(Item.ProductItemId);
                    //    if (productInSize == null || productInSize.Sum(_ => _.Quantity) < Item.Quantity)
                    //        throw new Exception("Product: " + ProductItem.ProductName + " [out of stock]");
                    //}
                    lstProduct_Weight.Add(new Tuple<long, int>(ProductItem.ProductId, item.Item2));
                    #region set properties OrderDetail
                    odDetail.DateCreated = DateTime.Now;
                    odDetail.DateModified = DateTime.Now;
                    odDetail.IsDeleted = false;
                    odDetail.OrderQuantity = item.Item2;
                    odDetail.SizeId = item.Item3;
                    odDetail.TotalWeight = odDetail.OrderQuantity * ProductItem.Weight;
                    odDetail.ProductId = item.Item1;
                    odDetail.UnitPrice = Convert.ToDecimal(ProductItem.MobileOnlinePrice);
                    odDetail.UnitPriceDiscount = ProductItem.PromotePrice != null ? Convert.ToDecimal(ProductItem.PromotePrice) : Convert.ToDecimal(ProductItem.MobileOnlinePrice);
                    odDetail.TotalPrice = odDetail.UnitPriceDiscount == null ? (odDetail.OrderQuantity * odDetail.UnitPrice) : (odDetail.OrderQuantity * odDetail.UnitPriceDiscount);
                    totalPrice += Convert.ToDecimal(odDetail.TotalPrice);
                    #endregion
                    lstOrderDetail.Add(odDetail);
                }
                #region set properties Order
                model.DateCreated = DateTime.Now;
                model.DateModified = DateTime.Now;
                model.CustomerName = acc.LastName + " " + acc.FirstName;
                model.CustomerEmail = acc.Email;
                model.CustomerPhone = acc.Phone;
                model.CustomerId = acc.CustomerId;
                model.CardHolderName = model.CardHolderName;
                model.CardNumber = model.CardNumber;
                model.CardTypeId = ViewBag.radCardType == "radCard_Atm" ? 1 : ViewBag.radCardType == "radCard_Credit" ? 2 : 3;
                model.BankId = model.BankId;
                model.TotalProduct = lst.Count;
                model.ShipToCity = Convert.ToInt64(formData["ProvinceId"]);
                model.ShipToDistrict = Convert.ToInt64(formData["DistrictId"]);
                model.IsWarning = false;
                model.CreatedBy = null;
                model.IsLocked = false;
                model.IsActive = true;
                model.IsDeleted = false;
                model.IsClosed = false;
                model.IsOrderConfirmed = model.PaymentTypeCode == "PTC-1" ? true : false;
                model.IsPaymentConfirmed = null;
                model.IsDeliveryConfirmed = null;
                model.ModifiedBy = null;
                model.OrderTypeCode = "ORD-T1";
                model.SubTotalFee = totalPrice;
                model.TaxFee = 0;
                if (totalPrice >= ship.FreeShip)
                    model.ShippingFee = 0;
                else
                    model.ShippingFee = ship.Price + _WeightRepository.GetShippingFee(Convert.ToInt64(formData["ProvinceId"]), lstProduct_Weight);
                model.TransactionFee = 0;
                model.TotalPaid = (model.TaxFee * model.SubTotalFee / 100) + model.SubTotalFee + model.ShippingFee + model.TransactionFee;
                model.TransactionStatusCode = "TRANS-TC1";
                model.TransactionCode = "";
                #endregion
                var orderId = _OrderRepository.CreateFullOrder(model, lstOrderDetail);
                if (orderId <= 0)
                {
                    throw new Exception("Don't create Order. You can try it");
                }
                var order = _OrderRepository.GetById(orderId);
                if (sessionObject.PaymentProduct != null)
                {
                    sessionObject.PaymentProduct = new List<Tuple<long, int, long, long, long>>();
                    Session["sessionObject"] = sessionObject;
                }
                MainRecord mRecord = new MainRecord();
                #region set properties MainRecord
                mRecord.DateCreated = DateTime.Now;
                mRecord.DateModified = DateTime.Now;
                mRecord.Description = "customer genegetor";
                mRecord.DocumentId = "";
                mRecord.FormId = order.OrderCode;
                mRecord.HoldByManagerId = null;
                mRecord.HoldByStaffId = null;
                mRecord.IsDeleted = false;
                mRecord.OriginatorId = _AccountRepository.Get_ByRoleNSystem("ADM", "SG");
                mRecord.TaskFormCode = model.PaymentTypeCode == "PTC-1" ? "COF-1" : model.PaymentTypeCode == "PTC-2" ? "COF-2" : model.PaymentTypeCode == "PTC-3" ? "COF-3" : "";
                mRecord.StatusDirectionCode = "SDC6";
                mRecord.StatusProcessCode = "SPC3";
                var itemTaskDirection = _TaskDirectionRepository.GetBy(mRecord.TaskFormCode, null, 1);
                mRecord.TaskDirectionId = itemTaskDirection.TaskDirectionId;
                #endregion

                SubRecord subRecord = new SubRecord();
                #region set properties subRecord
                //subRecord.MainRecordId = idMain;
                subRecord.IsDeleted = false;
                subRecord.PreviousSubId = 0;
                subRecord.ContentField = Common.Common.ContentFielSub;
                Common.Common common = new Common.Common();
                Domain.Core.Common.SubRecordJson subJson = new Domain.Core.Common.SubRecordJson(mRecord.OriginatorId.ToString(), _StatusDirectionRepository.GetByCode(mRecord.StatusDirectionCode).StatusDirectionId.ToString(), null, "0", "", DateTime.Now.ToString(), DateTime.Now.ToString());
                //subRecord.SubList = "[" + common.SubRecordJsontoString(subJson) + "]";
                #endregion

                _MainRecordRepository.CreateFullMain(mRecord, subRecord, subJson);
                if (TempData["OrderComplete"] != null)
                    TempData.Remove("OrderComplete");
                TempData.Add("OrderComplete", model.OrderId);
                //String order_id, String business, String total_amount, String shipping_fee, String tax_fee, String order_description, String url_success, String url_cancel, String url_detail)
                if (paymentType == "online_pay")
                {
                    BaoKimPayment bk = new BaoKimPayment();
                    var url = bk.createRequestUrl(order.OrderCode.ToString(), SessionKey.Business, order.TotalPaid.ToString(), order.ShippingFee.ToString(), order.TaxFee.ToString(), "WelcomeToGalagala", "http://galagala.vn:88/" + Url.Action("OrderOnlineComplete", "Product", new { code = order.OrderCode }), "http://galagala.vn:88/" + Url.Action("OrderOnlineFail", "Product", new { code = order.OrderCode }), "http://galagala.vn:88/");
                    return new RedirectResult(url);
                }
                return RedirectToAction("OrderComplete");
            }
            catch (Exception ex)
            {
                SetMessageCurrent(new List<string>() { ex.Message });
                List<Product> lstProduct = new List<Product>();
                var sessionObject = (SessionObject)Session["sessionObject"];
                var lstCart = sessionObject == null ? new List<Tuple<long, int, long, long, long>>() : sessionObject.PaymentProduct;
                foreach (var item in lstCart)
                {
                    var itemProduct = _ProductRepository.GetById(item.Item1);
                    if (ProductError(itemProduct) == false)
                        lstProduct.Add(itemProduct);
                }
                ViewBag.PaymentTypeName = formData["_PaymentTypeName"];
                ViewBag.ProvinceId = formData["ProvinceId"];
                ViewBag.radCardType = formData["radCardType"];
                ViewBag.DistrictId = formData["DistrictId"];
                ViewBag.chkAggre = formData["chkAggre"];
                ViewBag.card_number_1 = formData["card_number_1"];
                ViewBag.card_number_4 = formData["card_number_4"];
                ViewBag.card_number_2 = formData["card_number_2"];
                ViewBag.card_number_3 = formData["card_number_3"];
                ViewBag.u = Url.Action("Payment", "Product");
                ViewBag.Products = lstProduct;
                ViewBag.Districts = _DistrictRepository.GetAll();
                ViewBag.Banks = _BankRepository.GetAll(false);
                ViewBag.Provines = _ProvineRepository.GetAll();
                ViewBag.Lst = lstCart;
                var ListSize = _SizeRepository.GetAll(false);
                ViewBag.ListSize = ListSize;
                Private.LoadBegin(Session, ViewBag);
                return View(model);
            }
        }
        #region Test
        [SessionLoginFilter]
        public ActionResult TestPost()
        {
            if (Session["sessionGala"] == null)
                return RedirectToAction("Login", "Customer", new { ur = Url.Action("Payment", "Product") });
            var sessionObject = (SessionObject)Session["sessionObject"];
            if (sessionObject == null || sessionObject.PaymentProduct == null || sessionObject.PaymentProduct.Count == 0)
                return RedirectToAction("Index", "Home");
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            BankRepository _BankRepository = new BankRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            ProvineRepository _ProvineRepository = new ProvineRepository();
            List<Product> lstProduct = new List<Product>();

            #endregion
            var lst = sessionObject.PaymentProduct;
            foreach (var item in lst)
            {
                var itemProduct = _ProductRepository.GetById(item.Item1);
                //var itemInMedia = _ProductInMediaRepository.GetByProduct(item.Item1).Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
                //if (itemInMedia == null)
                //    itemInMedia = new ProductInMedia();
                if (ProductError(itemProduct) == false)
                    lstProduct.Add(itemProduct);
            }
            //ViewBag.Account = Session["sessionGala"] == null ? null : (Customer)Session["sessionGala"];
            ViewBag.Products = lstProduct;
            ViewBag.Districts = _DistrictRepository.GetAll();
            ViewBag.Provines = _ProvineRepository.GetAll();
            ViewBag.Banks = _BankRepository.GetAll(false);
            ViewBag.Lst = lst;
            //LoadBegin();
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("TestPost", "Product");
            var model = new Order();
            ViewBag.PaymentTypeName = "pre_pay";
            return View(model);
        }
        [HttpPost, SessionLoginFilter]
        public ActionResult TestPost(Order model, FormCollection formData)
        {
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            ProvineRepository _ProvinceRepository = new ProvineRepository();
            BankRepository _BankRepository = new BankRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            MainRecordRepository _MainRecordRepository = new MainRecordRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            SubRecordRepository _SubRecordRepository = new SubRecordRepository();
            AccountRepository _AccountRepository = new AccountRepository();
            ProvineRepository _ProvineRepository = new ProvineRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            #endregion
            try
            {
                Int32 outInt = 0;
                //LoadBegin();
                Private.LoadBegin(Session, ViewBag);
                var sessionObject = (SessionObject)Session["sessionObject"];
                if (sessionObject == null || sessionObject.PaymentProduct == null || sessionObject.PaymentProduct.Count == 0)
                    return RedirectToAction("Index", "Home");
                ViewBag.u = Url.Action("Payment", "Product");
                ViewBag.PaymentTypeName = formData["_PaymentTypeName"];
                ViewBag.ProvinceId = formData["ProvinceId"];
                ViewBag.radCardType = formData["radCardType"];
                ViewBag.DistrictId = formData["DistrictId"];
                ViewBag.chkAggre = formData["chkAggre"];
                ViewBag.card_number_1 = formData["card_number_1"];
                ViewBag.card_number_4 = formData["card_number_4"];
                ViewBag.card_number_2 = formData["card_number_2"];
                ViewBag.card_number_3 = formData["card_number_3"];
                if (Int32.TryParse(ViewBag.card_number_1, out outInt) && Int32.TryParse(ViewBag.card_number_2, out outInt) && Int32.TryParse(ViewBag.card_number_3, out outInt) && Int32.TryParse(ViewBag.card_number_4, out outInt))
                    model.CardNumber = Convert.ToInt64(((string)ViewBag.card_number_1).Trim() + ((string)ViewBag.card_number_2).Trim() + ((string)ViewBag.card_number_3).Trim() + ((string)ViewBag.card_number_4).Trim());
                var paymentType = (string)formData["_PaymentTypeName"];
                if (paymentType != null)
                    model.PaymentTypeCode = paymentType == "online_pay" ? "PTC-1" : paymentType == "pre_pay" ? "PTC-2" : paymentType == "post_pay" ? "PTC-3" : "";
                var error = false;
                if (ViewBag.radCardType != "radCard_Atm" && ViewBag.radCardType != "radCard_Credit" && ViewBag.radCardType != "radCard_Debate" && paymentType != null && paymentType == "pre_pay")
                {
                    ModelState.AddModelError("radCardType", (string)ViewBag.multiRes.GetString("alert_select_card_type", ViewBag.CultureInfo));
                    error = true; ViewBag.radCardType = "";
                }
                if ((ViewBag.chkAggre == null || ViewBag.chkAggre != "on") && paymentType != null && paymentType == "online_pay")
                {
                    ModelState.AddModelError("chkAggre", (string)ViewBag.multiRes.GetString("alert_check_agree_my_company", ViewBag.CultureInfo));
                    error = true;
                }
                if (formData["ProvinceId"] == null || formData["ProvinceId"] == "" || Convert.ToInt64(formData["ProvinceId"]) <= 0 || formData["DistrictId"] == null || formData["DistrictId"] == "" || Convert.ToInt64(formData["DistrictId"]) <= 0)
                {
                    if (formData["ProvinceId"] == null || formData["ProvinceId"] == "")
                        ModelState.AddModelError("ProvinceId", (string)ViewBag.multiRes.GetString("alert_select_provice", ViewBag.CultureInfo));
                    if (formData["DistrictId"] == null || formData["DistrictId"] == "")
                        ModelState.AddModelError("DistrictId", (string)ViewBag.multiRes.GetString("alert_select_district", ViewBag.CultureInfo));
                    error = true;
                }
                if (checkOrder(model) == true || error == true)
                {
                    List<Product> lstProduct = new List<Product>();
                    var lstCart = sessionObject.PaymentProduct;
                    foreach (var item in lstCart)
                    {
                        var itemProduct = _ProductRepository.GetById(item.Item1);
                        //var itemInMedia = _ProductInMediaRepository.GetByProduct(item.Item1).Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
                        //if (itemInMedia == null)
                        //    itemInMedia = new ProductInMedia();
                        if (ProductError(itemProduct) == false)
                            lstProduct.Add(itemProduct);
                    }
                    ViewBag.u = Url.Action("Payment", "Product");
                    ViewBag.Products = lstProduct;
                    ViewBag.Districts = _DistrictRepository.GetAll();
                    ViewBag.Provines = _ProvineRepository.GetAll();
                    ViewBag.Banks = _BankRepository.GetAll(false);
                    ViewBag.Lst = lstCart;
                    return View(model);
                }
                Province Province = _ProvinceRepository.GetById(Convert.ToInt64(formData["ProvinceId"]));
                District District = _DistrictRepository.GetById(Convert.ToInt64(formData["DistrictId"]));
                var lst = sessionObject.PaymentProduct;
                var acc = (Customer)Session["sessionGala"];
                var lstOrderDetail = new List<OrderDetail>();
                decimal totalPrice = 0;
                foreach (var item in lst)
                {
                    var ProductItem = _ProductRepository.GetById(item.Item1);
                    OrderDetail odDetail = new OrderDetail();
                    #region set properties OrderDetail
                    odDetail.DateCreated = DateTime.Now;
                    odDetail.DateModified = DateTime.Now;
                    odDetail.IsDeleted = false;
                    odDetail.OrderQuantity = item.Item2;
                    odDetail.ProductId = item.Item1;
                    odDetail.UnitPrice = 10000;
                    odDetail.UnitPriceDiscount = 10000;
                    odDetail.TotalPrice = odDetail.UnitPriceDiscount == null ? (odDetail.OrderQuantity * odDetail.UnitPrice) : (odDetail.OrderQuantity * odDetail.UnitPriceDiscount);
                    totalPrice += Convert.ToDecimal(odDetail.TotalPrice);
                    #endregion
                    lstOrderDetail.Add(odDetail);
                }
                #region set properties Order
                model.DateCreated = DateTime.Now;
                model.DateModified = DateTime.Now;
                model.CustomerName = acc.LastName + " " + acc.FirstName;
                model.CustomerEmail = acc.Email;
                model.CustomerPhone = acc.Phone;
                model.CustomerId = acc.CustomerId;
                model.CardHolderName = model.CardHolderName;
                model.CardNumber = model.CardNumber;
                model.CardTypeId = ViewBag.radCardType == "radCard_Atm" ? 1 : ViewBag.radCardType == "radCard_Credit" ? 2 : 3;
                model.BankId = model.BankId;
                model.TotalProduct = lst.Count;
                model.ShipToCity = Convert.ToInt64(formData["ProvinceId"]);
                model.ShipToDistrict = Convert.ToInt64(formData["DistrictId"]);
                model.IsWarning = false;
                model.CreatedBy = null;
                model.IsLocked = false;
                model.IsActive = true;
                model.IsDeleted = false;
                model.IsClosed = false;
                model.IsOrderConfirmed = false;
                model.IsPaymentConfirmed = null;
                model.IsDeliveryConfirmed = null;
                model.ModifiedBy = null;
                model.OrderTypeCode = "ORD-T1";
                model.SubTotalFee = totalPrice;
                model.TaxFee = 0;
                model.ShippingFee = 0;
                model.TransactionFee = 0;
                model.TotalPaid = (model.TaxFee * model.SubTotalFee / 100) + model.SubTotalFee + model.ShippingFee + model.TransactionFee;
                model.TransactionStatusCode = "TRANS-TC1";
                model.TransactionCode = "";
                #endregion
                #region remove
                //var orderId = _OrderRepository.Create(model);
                //if (orderId <= 0)
                //{
                //    SetMessageCurrent(new List<string>() { "Don't Create Order. Please check input." });
                //    List<Product> lstProduct = new List<Product>();
                //    var lstCart = (List<Tuple<long, int>>)Session["paymentProduct"];
                //    foreach (var item in lstCart)
                //    {
                //        var itemProduct = _ProductRepository.GetById(item.Item1);
                //        if (ProductError(itemProduct) == false)
                //            lstProduct.Add(itemProduct);
                //    }
                //    ViewBag.u = Url.Action("Payment", "Product");
                //    ViewBag.Products = lstProduct;
                //    ViewBag.Districts = _DistrictRepository.GetAll();
                //    ViewBag.Provines = _ProvineRepository.GetAll();
                //    ViewBag.Lst = lstCart;
                //    LoadBegin();
                //    return View(model);
                //}
                //foreach (var item in lstOrderDetail)
                //    item.OrderId = orderId;
                //if (_OrderDetailRepository.CreateList(lstOrderDetail) == false)
                //{
                //    _OrderRepository.Delete(orderId);
                //}
                #endregion
                var orderId = _OrderRepository.CreateFullOrder(model, lstOrderDetail);
                if (orderId <= 0)
                {
                    #region error load page
                    SetMessageCurrent(new List<string>() { (string)ViewBag.multiRes.GetString("alert_create_fail_order", ViewBag.CultureInfo) });
                    List<Product> lstProduct = new List<Product>();
                    var lstCart = sessionObject.PaymentProduct;
                    foreach (var item in lstCart)
                    {
                        var itemProduct = _ProductRepository.GetById(item.Item1);
                        if (ProductError(itemProduct) == false)
                            lstProduct.Add(itemProduct);
                    }
                    ViewBag.u = Url.Action("Payment", "Product");
                    ViewBag.Products = lstProduct;
                    ViewBag.Districts = _DistrictRepository.GetAll();
                    ViewBag.Provines = _ProvineRepository.GetAll();
                    ViewBag.Lst = lstCart;
                    //LoadBegin();
                    //Private _pri = new Private();
                    Private.LoadBegin(Session, ViewBag);
                    #endregion
                    return View(model);
                }
                var order = _OrderRepository.GetById(orderId);
                if (sessionObject != null)
                {
                    sessionObject.PaymentProduct = new List<Tuple<long, int, long, long, long>>();
                    Session["sessionObject"] = sessionObject;
                }
                //Session.Remove("paymentProduct");
                MainRecord mRecord = new MainRecord();
                #region set properties MainRecord
                mRecord.DateCreated = DateTime.Now;
                mRecord.DateModified = DateTime.Now;
                mRecord.Description = "customer genegetor";
                mRecord.DocumentId = "";
                mRecord.FormId = order.OrderCode;
                mRecord.HoldByManagerId = null;
                mRecord.HoldByStaffId = null;
                mRecord.IsDeleted = false;
                mRecord.OriginatorId = _AccountRepository.Get_ByRoleNSystem("ADM", "SG");
                mRecord.TaskFormCode = model.PaymentTypeCode == "PTC-1" ? "COF-1" : model.PaymentTypeCode == "PTC-2" ? "COF-2" : model.PaymentTypeCode == "PTC-3" ? "COF-3" : "";
                mRecord.StatusDirectionCode = "SDC6";
                mRecord.StatusProcessCode = "SPC3";
                var itemTaskDirection = _TaskDirectionRepository.GetBy(mRecord.TaskFormCode, null, 1);
                mRecord.TaskDirectionId = itemTaskDirection.TaskDirectionId;
                #endregion
                //var idMain = _MainRecordRepository.Create(mRecord);
                //if (idMain == null)
                //{
                //    return View(model);
                //}
                SubRecord subRecord = new SubRecord();
                #region set properties subRecord
                //subRecord.MainRecordId = idMain;
                subRecord.IsDeleted = false;
                subRecord.PreviousSubId = 0;
                subRecord.ContentField = Common.Common.ContentFielSub;
                Common.Common common = new Common.Common();
                Domain.Core.Common.SubRecordJson subJson = new Domain.Core.Common.SubRecordJson(mRecord.OriginatorId.ToString(), _StatusDirectionRepository.GetByCode(mRecord.StatusDirectionCode).StatusDirectionId.ToString(), null, "0", "", DateTime.Now.ToString(), DateTime.Now.ToString());
                //subRecord.SubList = "[" + common.SubRecordJsontoString(subJson) + "]";
                #endregion
                //_OrderRepository.CreateFullOrder(model, lstOrderDetail, mRecord, subRecord, subJson);
                //_SubRecordRepository.Create(subRecord);
                _MainRecordRepository.CreateFullMain(mRecord, subRecord, subJson);
                if (TempData["OrderComplete"] != null)
                    TempData.Remove("OrderComplete");
                TempData.Add("OrderComplete", model.OrderId);
                CustomerRepository _iCustomerService = new CustomerRepository();
                Customer _cus = _iCustomerService.GetById((long)model.CustomerId);
                TransactionStatusRepository _iTransactionStatusService = new TransactionStatusRepository();
                string htmlCode = "";
                using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
                {
                    htmlCode = client.DownloadString("http://localhost:10359/Product/MailOrderLayout/?OrderId=" + orderId);

                }

                if (acc.Email != null)
                {
                    string mail_to = acc.Email;
                    string mail_subject = "Hợp Thành Trading Business Co.,Ltd";
                    string mail_body = "Hello " + acc.FirstName + "!"
                                + "<br/>"
                                + "Bạn vừa đặt đơn hàng trên Galagala.! Bạn có thể xem thông tin chi tiết đơn hàng ở phía dưới"
                                + "<br/>"
                                + "Cảm ơn bạn đã đặt hàng và tin tưởng Galagala chúng tôi."
                                + "<br/>"
                                + htmlCode
                                + "<br/>";
                    //+ "<a href='" + Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("OrderInfor", "Product", new { id = model.OrderId }) + "'>[Chi tiết]</a>";
                    common.SendMail(mail_subject, mail_body, mail_to);
                }
                //String order_id, String business, String total_amount, String shipping_fee, String tax_fee, String order_description, String url_success, String url_cancel, String url_detail)
                if (paymentType == "online_pay")
                {
                    BaoKimPayment bk = new BaoKimPayment();
                    var url = bk.createRequestUrl(order.OrderCode.ToString(), SessionKey.Business, order.TotalPaid.ToString(), order.ShippingFee.ToString(), order.TaxFee.ToString(), "WelcomeToGalagala", "http://galagala.vn:88/" + Url.Action("OrderOnlineComplete", "Product", new { code = order.OrderCode }), "http://galagala.vn:88/" + Url.Action("OrderOnlineFail", "Product", new { code = order.OrderCode }), "http://galagala.vn:88/");
                    return new RedirectResult(url);
                }
                return RedirectToAction("OrderComplete");
            }
            catch (Exception ex)
            {
                SetMessageCurrent(new List<string>() { ex.Message });
                List<Product> lstProduct = new List<Product>();
                var sessionObject = (SessionObject)Session["sessionObject"];
                var lstCart = sessionObject == null ? new List<Tuple<long, int, long, long, long>>() : sessionObject.PaymentProduct;
                foreach (var item in lstCart)
                {
                    var itemProduct = _ProductRepository.GetById(item.Item1);
                    if (ProductError(itemProduct) == false)
                        lstProduct.Add(itemProduct);
                }
                ViewBag.PaymentTypeName = formData["_PaymentTypeName"];
                ViewBag.ProvinceId = formData["ProvinceId"];
                ViewBag.radCardType = formData["radCardType"];
                ViewBag.DistrictId = formData["DistrictId"];
                ViewBag.chkAggre = formData["chkAggre"];
                ViewBag.card_number_1 = formData["card_number_1"];
                ViewBag.card_number_4 = formData["card_number_4"];
                ViewBag.card_number_2 = formData["card_number_2"];
                ViewBag.card_number_3 = formData["card_number_3"];
                ViewBag.u = Url.Action("Payment", "Product");
                ViewBag.Products = lstProduct;
                ViewBag.Districts = _DistrictRepository.GetAll();
                ViewBag.Banks = _BankRepository.GetAll(false);
                ViewBag.Provines = _ProvineRepository.GetAll();
                ViewBag.Lst = lstCart;
                //LoadBegin();
                Private.LoadBegin(Session, ViewBag);
                return View(model);
            }

        }
        #endregion
        [HttpPost]
        //[AcceptVerbs(HttpVerbs.Post),HttpPost]
        public ActionResult BPN()
        {
            var param_all = "";
            LogRepository _LogRepository = new LogRepository();
            Log lgs = new Log();
            #region Content
            try
            {
                OrderRepository _OrderRepository = new OrderRepository();
                PaymentTypeRepository _PaymentTypeRepository = new PaymentTypeRepository();
                Log lg = new Log();
                lg.OrderCode = Request["order_id"];
                lg.Status = Request.Params["transaction_status"];
                lg.DateCreated = DateTime.Now;
                #region BK
                string strLive = "https://www.baokim.vn/bpn/verify";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strLive);
                //Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] param = Request.BinaryRead(HttpContext.Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                param_all = strRequest;
                req.ContentLength = strRequest.Length;
                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();
                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = streamIn.ReadLine();
                streamIn.Close();
                lg.Description = strRequest + "----------";
                lg.LogName = "BaoKim " + strResponse;
                lg.Description += "transaction_status: "
                    + Request.Params["transaction_status"]
                    + ", transaction_id: "
                    + Request["transaction_id"]
                    + ", order_id: "
                    + Request["order_id"]
                    + ", strRequest:" + strRequest;
                if (strResponse == "VERIFIED")
                {
                    string orderID = Request["order_id"];//mã đơn hàng đã thanh toán
                    string transactionID = Request["transaction_id"];
                    string transaction_status = Request["transaction_status"];//trạng thái thanh toán
                    string ngaythanhtoan = DateTime.Now.ToString();
                    string status = "Đã Thanh Toán";
                    if (transaction_status == "4" || transaction_status == "13")
                    {
                        #region thanh cong
                        OrderPaidRepository _OrderPaidRepository = new OrderPaidRepository();
                        var order = _OrderRepository.GetByCode(orderID);
                        order.TransactionStatusCode_BK = "BK-TRANS-" + transaction_status;
                        order.IsPaymentConfirmed = true;
                        //order.CustomerEmail = customer_email;
                        //order.CustomerPhone = customer_phone;
                        OrderPaid _OrderPaid = new OrderPaid();
                        _OrderPaid.OrderCode = order.OrderCode;
                        _OrderPaid.PaymentTypeId = _PaymentTypeRepository.GetByCode(order.PaymentTypeCode).PaymentTypeId;
                        _OrderPaid.CustomerPhone = order.CustomerPhone;
                        _OrderPaid.CustomerEmail = order.CustomerEmail;
                        _OrderPaid.CustomerName = order.CustomerName;
                        _OrderPaid.DateCreated = DateTime.Now;
                        _OrderPaid.DateTransaction = DateTime.Now;
                        _OrderPaid.TransactionId = transactionID;
                        _OrderPaid.BankId = order.BankId;
                        _OrderPaid.CardHolderName = order.CardHolderName;
                        _OrderPaid.CardNumber = order.CardNumber;
                        _OrderPaid.CardTypeId = order.CardTypeId;
                        _OrderPaid.IsDeleted = false;
                        #region removeCode
                        //_OrderPaid.CustomerAddress = order.CustomerAddress;
                        //_OrderPaid.CustomerEmail = customer_email;
                        //_OrderPaid.CustomerLocation = customer_location;
                        //_OrderPaid.CustomerName = customer_name;
                        //_OrderPaid.CustomerTotalPaid = Convert.ToDecimal(total_amount);
                        //_OrderPaid.CreatedOn = created_on;
                        //_OrderPaid.Checksum = checksum;
                        //_OrderPaid.MerchantSiteId = Convert.ToInt32(merchant_id);
                        //_OrderPaid.NetAmount = Convert.ToDecimal(net_amount);
                        //_OrderPaid.PaymentTypeBKCode = "BK-PT-" + payment_type;
                        #endregion
                        _OrderPaid.ReceiverName = order.ReceiverName;
                        _OrderPaid.ReceiverPhone = order.ReceiverPhone;
                        _OrderPaid.ShipToAddress = order.ShipToAddress;
                        _OrderPaid.ShipToCity = order.ShipToCity;
                        _OrderPaid.ShipToDistrict = order.ShipToDistrict;
                        _OrderPaid.TotalActualReceive = Convert.ToDecimal(_OrderPaid.NetAmount);
                        _OrderPaidRepository.Create(_OrderPaid);
                        _OrderRepository.Edit(order);

                        //#region  Update Value Quantity Logistic
                        //OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
                        //ProductRepository _ProductRepository = new ProductRepository();
                        //var lstOrderDetail = _OrderDetailRepository.GetByOrder(order.OrderId);
                        //var lstProductItemInSize = new List<Tuple<long, string, int>>();
                        //foreach (var item in lstOrderDetail)
                        //{
                        //    var product_Item = _ProductRepository.GetById(item.ProductId);
                        //    lstProductItemInSize.Add(new Tuple<long, string, int>(Convert.ToInt64(item.SizeId), product_Item.ProductStockCode, item.OrderQuantity));
                        //}
                        //ProductItemInSizeRepository _ProductItemInSizeRepository = new ProductItemInSizeRepository();
                        //_ProductItemInSizeRepository.UpdateDownQuantity(lstProductItemInSize);
                        //#endregion

                        //sendmail baobthanh cong
                        Common.Common com = new Common.Common();
                        CustomerRepository _iCustomerService = new CustomerRepository();
                        Customer cus = _iCustomerService.GetById((long)order.CustomerId);
                        if (_OrderPaid.CustomerEmail != null)
                        {
                            string mail_to = _OrderPaid.CustomerEmail;
                            string mail_subject = "Hợp Thành Trading Business Co.,Ltd";
                            string mail_body = "Hello " + cus.FirstName + " " + cus.LastName + "!"
                                        + "<br/>"
                                        + "Bạn vừa gia dịch thành công!" + "<a href='" + Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("OrderInfor", "Product", new { id = order.OrderId }) + "'>[Chi tiết]</a>"
                                        + "<br/>"
                                        + "Cảm ơn bạn đã đặt hàng và tin tưởng Galagala chúng tôi."
                                        + "<br/>";
                            //+ "<a href='" + Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("OrderInfor", "Product", new { id = model.OrderId }) + "'>[Chi tiết]</a>";
                            com.SendMail(mail_subject, mail_body, mail_to);
                        }
                        //cập nhật trạng thái đơn hàng
                        //string sql = "update tenbang set madon='" + orderID + "',donhangbaokim='" + transactionID + "',ngaygio='" + ngaythanhtoan + "',trangthai='" + status + "'";
                        #endregion
                        //CustomerRepository _iCustomerService = new CustomerRepository();
                        //Customer _cus = _iCustomerService.GetById((long)model.CustomerId);
                        //TransactionStatusRepository _iTransactionStatusService = new TransactionStatusRepository();


                        #region SendMail
                        try
                        {
                            string htmlCode = "";
                            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
                            {
                                //htmlCode = client.DownloadString("http://localhost:10359/Product/MailOrderLayout/?OrderId="+order.OrderId);
                                htmlCode = client.DownloadString(Url.Action("MailOrderLayout", "Product", new { OrderId = order.OrderId }));
                            }
                            if (order.CustomerEmail != null)
                            {
                                string mail_to = order.CustomerEmail;
                                string mail_subject = "Hợp Thành Trading Business Co.,Ltd";
                                string mail_body = htmlCode;
                                //+ "<a href='" + Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("OrderInfor", "Product", new { id = model.OrderId }) + "'>[Chi tiết]</a>";
                                Common.Common common = new Common.Common();
                                common.SendMail(mail_subject, mail_body, mail_to);
                            }
                        }
                        catch (Exception ex)
                        {
                            lg.Description += ", ExceptionSendEmail: " + ex.Message;
                        }
                        #endregion
                    }
                    else
                    {
                        #region khong thanh cong
                        //OrderPaidRepository _OrderPaidRepository = new OrderPaidRepository();
                        ////thong tin khong hop le. Log lai de theo doi   
                        //OrderPaid _OrderPaid = new OrderPaid();
                        //Random ra = new Random();
                        //_OrderPaid.OrderCode = Request.Params["order_id"] + " - " + ra.Next(0, 1000).ToString();
                        //_OrderPaid.CustomerPhone = "VERIFIED ";
                        //_OrderPaid.CustomerEmail = "---";
                        //_OrderPaid.CustomerName = "BK-TRANS-" + Request.Params["transaction_status"];
                        //_OrderPaid.PaymentTypeId = _PaymentTypeRepository.GetByCode("PTC-1").PaymentTypeId;
                        //_OrderPaid.DateCreated = DateTime.Now;
                        //_OrderPaid.DateTransaction = DateTime.Now;
                        //_OrderPaid.TransactionId = Request.Params["transaction_id"];
                        //_OrderPaid.BankId = 1;
                        //_OrderPaid.CardHolderName = "---";
                        //_OrderPaid.CardNumber = 1;
                        //_OrderPaid.CardTypeId = 1;
                        //_OrderPaid.IsDeleted = true;
                        //_OrderPaid.ReceiverName = "---";
                        //_OrderPaid.ReceiverPhone = "---";
                        //_OrderPaid.ShipToAddress = "---";
                        //_OrderPaid.ShipToCity = "---";
                        //_OrderPaid.ShipToDistrict = "---";
                        //_OrderPaid.TotalActualReceive = Convert.ToDecimal(_OrderPaid.NetAmount);
                        //_OrderPaidRepository.Create(_OrderPaid);
                        //var od = _OrderRepository.GetByCode(x);
                        //od.OrderDescription = x + "+++" + y + "---" + z;
                        //_OrderRepository.Save(od);
                        #endregion
                    }
                }
                else if (strResponse == "INVALID")
                {
                    #region INVALID
                    //OrderPaidRepository _OrderPaidRepository = new OrderPaidRepository();
                    ////thong tin khong hop le. Log lai de theo doi   
                    //OrderPaid _OrderPaid = new OrderPaid();
                    //Random ra = new Random();
                    //_OrderPaid.OrderCode = Request.Params["order_id"] + " - " + ra.Next(0, 1000).ToString();
                    //_OrderPaid.CustomerPhone = "INVALID";
                    //_OrderPaid.CustomerEmail = "---";
                    //_OrderPaid.CustomerName = "BK-TRANS-" + Request.Params["transaction_status"];
                    //_OrderPaid.PaymentTypeId = _PaymentTypeRepository.GetByCode("PTC-1").PaymentTypeId;
                    //_OrderPaid.DateCreated = DateTime.Now;
                    //_OrderPaid.DateTransaction = DateTime.Now;
                    //_OrderPaid.TransactionId = Request.Params["transaction_id"];
                    //_OrderPaid.BankId = 1;
                    //_OrderPaid.CardHolderName = "---";
                    //_OrderPaid.CardNumber = 1;
                    //_OrderPaid.CardTypeId = 1;
                    //_OrderPaid.IsDeleted = true;
                    //_OrderPaid.ReceiverName = "---";
                    //_OrderPaid.ReceiverPhone = "---";
                    //_OrderPaid.ShipToAddress = "---";
                    //_OrderPaid.ShipToCity = "---";
                    //_OrderPaid.ShipToDistrict = "---";
                    //_OrderPaid.TotalActualReceive = Convert.ToDecimal(_OrderPaid.NetAmount);
                    //_OrderPaidRepository.Create(_OrderPaid);
                    //var od = _OrderRepository.GetByCode(x);
                    //od.OrderDescription = x + "+++" + y + "---" + z;
                    //_OrderRepository.Save(od);
                    #endregion
                }
                else
                { }
                #endregion
                _LogRepository.Create(lg);
            }
            catch (Exception ex)
            {
                Log lg = new Log();
                lg.OrderCode = Request["order_id"];
                lg.Status = "0";
                lg.DateCreated = DateTime.Now;
                lg.LogName = "BaoKim Exception";
                lg.Description = param_all + "------------transaction_status: " + Request.Params["transaction_status"] + ", transaction_id: " + Request["transaction_id"] + ", order_id: " + Request["order_id"] + "error : " + ex.Message;
                _LogRepository.Create(lg);
            }
            #endregion
            return null;
        }

        public string Pay123()
        {
            //URL thanh toán của 123Pay - createOrder">
            var url = "https://sandbox.123pay.vn/miservice/createOrder1";
            //URL kiểm tra giao dịch của 123Pay - queryOrder
            var urlqueryOrder = "https://sandbox.123pay.vn/miservice/queryOrder1";
            //Mã Merchant - merchantCode
            var merchantCode = "MICODE";
            //Định danh ngân hàng - bankCode
            var bankCode = "123PAY";
            //Mật khẩu bảo mật - passcode
            var key = "MIPASSCODE";
            return "";
        }
        [SessionLoginFilter]
        public PartialViewResult MailOrderLayout(long? OrderId)
        {
            try
            {
                #region load
                ProductRepository _ProductRepository = new ProductRepository();
                ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                BankRepository _BankRepository = new BankRepository();
                DistrictRepository _DistrictRepository = new DistrictRepository();
                ProvineRepository _ProvineRepository = new ProvineRepository();
                List<Product> lstProduct = new List<Product>();
                OrderRepository _iOrderService = new OrderRepository();
                OrderDetailRepository _iOrderDetailService = new OrderDetailRepository();
                CustomerRepository _iCustomerService = new CustomerRepository();
                #endregion

                Customer cus = Session["sessionGala"] == null ? null : (Customer)Session["sessionGala"];
                var model = _iOrderService.GetById((long)OrderId);
                if (cus.CustomerId != model.CustomerId)
                {
                    SetMessageCurrent(new List<string>() { "Xin lỗi! Order này không phải của " + cus.FirstName + " " + cus.LastName });
                    return PartialView(model);
                }
                var lst = _iOrderDetailService.GetAll(false).Where(o => o.OrderId == OrderId).ToList();
                foreach (var item in lst)
                {
                    var itemProduct = _ProductRepository.GetById(item.ProductId);
                    //var itemInMedia = _ProductInMediaRepository.GetByProduct(item.Item1).Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
                    //if (itemInMedia == null)
                    //    itemInMedia = new ProductInMedia();
                    //if (ProductError(itemProduct) == false)
                    lstProduct.Add(itemProduct);
                }
                ViewBag.Products = lstProduct;
                ViewBag.LstOrderDetails = lst;

                //LoadBegin();
                Private.LoadBegin(Session, ViewBag);

                ViewBag.Cus = _iCustomerService.GetById((long)model.CustomerId);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                SetMessageCurrent(new List<string>() { "Error: " + ex.Message });
                return PartialView();
            }
        }
        #endregion
        #region Comment
        public ActionResult AddComment(string code)
        {
            if (Session["sessionGala"] == null)
            {
                return RedirectToAction("Login", "Customer", new { ur = Url.Action("AddComment", "Product", new { code = code }) });
            }
            ProductRepository _ProductRepository = new ProductRepository();
            var product = _ProductRepository.GetByCode(code);
            Private.LoadBegin(Session, ViewBag);
            if (ProductError(product) == true)
                return RedirectToAction("Index", "Home");
            ViewBag.Product = product;
            ViewBag.ProductCode = product.ProductCode;
            ViewBag.ProductId = product.ProductId;
            var model = new Review();
            model.Value = "";
            model.ProductCode = product.ProductCode;
            return View(model);
        }
        [HttpPost, SessionLoginFilter]
        public ActionResult AddComment(Review rw, FormCollection formData)
        {
            ProductRepository _ProductRepository = new ProductRepository();
            var product = new Product();
            Private.LoadBegin(Session, ViewBag);
            if (rw.ProductCode == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                product = _ProductRepository.GetByCode(rw.ProductCode);
                if (ProductError(product) == true)
                    return RedirectToAction("Index", "Home");
                if (validateError(rw) == true)
                {

                }
            }
            try
            {
                int i = 0;
                var lst = new List<string>();
                if (rw.Value == null || int.TryParse(rw.Value, out i) == false || Convert.ToInt32(rw.Value) <= 0 || Convert.ToInt32(rw.Value) > 5)
                {
                    lst.Add("Rating null");
                }
                if (rw.Content == null || rw.Content.Length == 0 || rw.Content.Length > 200)
                {
                    lst.Add("Please input content maximun 200 characters");
                }

                if (lst.Count > 0)
                {
                    ViewBag.Product = product;
                    ViewBag.code = product.ProductCode;
                    ViewBag.ProductId = product.ProductId;
                    return View(rw);
                }
                else
                {
                    rw.DateCreated = DateTime.Now;
                    var acc = (HTTelecom.Domain.Core.DataContext.cis.Customer)Session["sessionGala"];
                    rw.CustomerId = acc.CustomerId;
                    rw.CustomerName = acc.LastName + " " + acc.FirstName;
                    rw.IsDeleted = false;
                    var hrefProduct = product.ProductTypeCode == "PT1" ? "OrdProduct" : product.ProductTypeCode == "PT2" ? "SaleProduct" : product.ProductTypeCode == "PT3" ? "ChargeProduct" : "FreeProduct";
                    var href = Url.Action(hrefProduct, "Product", new { id = product.ProductId, urlName = product.Alias, urlNameStore = product.Store.Alias });
                    ReviewRepository _ReviewRepository = new ReviewRepository();
                    _ReviewRepository.Create(rw);
                    return Redirect(href);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Product = product;
                ViewBag.code = product.ProductCode;
                ViewBag.ProductId = product.ProductId;
                Private.LoadBegin(Session, ViewBag);
                return View(rw);
            }
        }
        private bool validateError(Review rw)
        {
            try
            {
                int i = 0;
                var error = false;
                if (rw.Content == null || rw.Content.Length == 0 || rw.Content.Length > 200)
                {
                    ModelState.AddModelError("Content", (string)ViewBag.multiRes.GetString("alert_review_content_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (rw.Value == null || int.TryParse(rw.Value, out i) == false || Convert.ToInt32(rw.Value) <= 0 || Convert.ToInt32(rw.Value) > 5)
                {
                    ModelState.AddModelError("Value", (string)ViewBag.multiRes.GetString("alert_review_value_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (rw.ReviewName == null || rw.ReviewName.Length > 100)
                {
                    ModelState.AddModelError("ReviewName", (string)ViewBag.multiRes.GetString("alert_reviewname_value_null", ViewBag.CultureInfo));
                    error = true;
                }
                return error;
            }
            catch (Exception ex)
            {
                SetMessageCurrent(new List<string>() { ex.Message });
                return true;
            }
        }
        #endregion
        #region Order - Infor Transaction
        public ActionResult OrderOnlineComplete(string code)
        {
            if (code == null)
                return RedirectToAction("Index", "Home");
            Private.LoadBegin(Session, ViewBag);

            #region remove
            //BaoKimPayment bk = new BaoKimPayment();
            //try
            //{
            //    if (code != null && code == Request.Params["order_id"])
            //    {
            //        var created_on = Request.Params["created_on"];
            //        var customer_address = Request.Params["customer_address"];
            //        var customer_email = Request.Params["customer_email"];
            //        var customer_location = Request.Params["customer_location"];
            //        var customer_name = Request.Params["customer_name"];
            //        var customer_phone = Request.Params["customer_phone"];
            //        var fee_amount = Request.Params["fee_amount"];
            //        var merchant_email = Request.Params["merchant_email"];
            //        var merchant_name = Request.Params["merchant_name"];
            //        var net_amount = Request.Params["net_amount"];
            //        var payment_type = Request.Params["payment_type"];
            //        var total_amount = Request.Params["total_amount"];
            //        var transaction_id = Request.Params["transaction_id"];
            //        var checksum = Request.Params["checksum"];
            //        var transaction_status = Request.Params["transaction_status"];
            //        var merchant_id = Request.Params["merchant_id"];
            //        //OrderRepository _OrderRepository = new OrderRepository();
            //        //OrderPaidRepository _OrderPaidRepository = new OrderPaidRepository();
            //        //var order = _OrderRepository.GetByCode(code);
            //        //if (order == null)
            //        //    return RedirectToAction("Index", "Home");
            //        //var checksumCreate = bk.checkSum(order.OrderCode.ToString(), transaction_id, created_on, payment_type, transaction_status, total_amount,
            //        //    net_amount, fee_amount, merchant_id, customer_name, customer_email, customer_phone, customer_address);
            //        //var checksumCreate1 = bk.checkSumMin(order.OrderCode.ToString(), transaction_id, merchant_id);
            //        ////String order_id, String transaction_id, String create_on, String payment_type, String transaction_status, String total_amount,
            //        ////String net_amount, String fee_amount, String merchant_id, String customer_name, String customer_email, String customer_phone, String customer_address
            //    }
            //    else
            //    {
            //        SetMessageCurrent(new List<string>() { "Payment failed. Please check your billing information." });
            //        //Thanh toán không thành công. Vui lòng kiểm tra lại thông tin thanh toán.
            //        return View();
            //    }

            //    return View();
            //}
            //catch
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            #endregion

            try
            {
                #region load
                ProductRepository _ProductRepository = new ProductRepository();
                ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                BankRepository _BankRepository = new BankRepository();
                DistrictRepository _DistrictRepository = new DistrictRepository();
                ProvineRepository _ProvineRepository = new ProvineRepository();
                List<Product> lstProduct = new List<Product>();
                OrderRepository _iOrderService = new OrderRepository();
                OrderDetailRepository _iOrderDetailService = new OrderDetailRepository();
                CustomerRepository _iCustomerService = new CustomerRepository();

                Order _order = _iOrderService.GetByCode(code);

                //Neu null _order
                //return  error
                #endregion
                if (_order.IsPaymentConfirmed == false)
                {
                    //Error
                    SetMessageCurrent(new List<string>() { "Error: Đơn hàng đã bị hủy!" });
                    //_pri.LoadBegin(Session, ViewBag);
                    return View();
                }
                var lstOrderDetail = _iOrderDetailService.GetAll(false).Where(o => o.OrderId == _order.OrderId).ToList();
                foreach (var item in lstOrderDetail)
                {
                    var itemProduct = _ProductRepository.GetById(item.ProductId);
                    //var itemInMedia = _ProductInMediaRepository.GetByProduct(item.Item1).Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
                    //if (itemInMedia == null)
                    //    itemInMedia = new ProductInMedia();
                    if (ProductError(itemProduct) == false)
                        lstProduct.Add(itemProduct);
                }
                ViewBag.Products = lstProduct;
                ViewBag.LstOrderDetails = lstOrderDetail;
                if (lstProduct.Count == 0)
                {
                    //Error
                    SetMessageCurrent(new List<string>() { "Danh sách product rỗng hoặc sản phẩm bị lỗi!" });
                    //_pri.LoadBegin(Session, ViewBag);
                    return View();
                }
                //_pri.LoadBegin(Session, ViewBag);
                var model = _iOrderService.GetById(_order.OrderId);
                ViewBag.Cus = _iCustomerService.GetById((long)model.CustomerId);
                return View(model);
            }
            catch (Exception ex)
            {
                //Error
                SetMessageCurrent(new List<string>() { ex.ToString() });
                //_pri.LoadBegin(Session, ViewBag);
                return View();
            }
        }
        public ActionResult OrderOnlineFail(string code)
        {
            if (code == null)
                return RedirectToAction("Index", "Home");
            OrderRepository _OrderRepository = new OrderRepository();
            _OrderRepository.Fail(code);
            //GhiLog
            Private.LoadBegin(Session, ViewBag);
            try
            {
                #region load
                ProductRepository _ProductRepository = new ProductRepository();
                ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                BankRepository _BankRepository = new BankRepository();
                DistrictRepository _DistrictRepository = new DistrictRepository();
                ProvineRepository _ProvineRepository = new ProvineRepository();
                List<Product> lstProduct = new List<Product>();
                OrderRepository _iOrderService = new OrderRepository();
                OrderDetailRepository _iOrderDetailService = new OrderDetailRepository();
                CustomerRepository _iCustomerService = new CustomerRepository();

                Order _order = _iOrderService.GetByCode(code);
                if (_order.IsPaymentConfirmed == true)
                {
                    //Error
                    SetMessageCurrent(new List<string>() { "Error: Đơn hàng đã được xử lí!" });
                    //_pri.LoadBegin(Session, ViewBag);
                    return View();
                }
                //Neu null _order
                //return  error
                #endregion
                var lstOrderDetail = _iOrderDetailService.GetAll(false).Where(o => o.OrderId == _order.OrderId).ToList();
                foreach (var item in lstOrderDetail)
                {
                    var itemProduct = _ProductRepository.GetById(item.ProductId);
                    //var itemInMedia = _ProductInMediaRepository.GetByProduct(item.Item1).Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").FirstOrDefault();
                    //if (itemInMedia == null)
                    //    itemInMedia = new ProductInMedia();
                    if (ProductError(itemProduct) == false)
                        lstProduct.Add(itemProduct);
                }
                ViewBag.Products = lstProduct;
                ViewBag.LstOrderDetails = lstOrderDetail;
                if (lstProduct.Count == 0)
                {
                    //Error
                    SetMessageCurrent(new List<string>() { "Danh sách product rỗng hoặc sản phẩm bị lỗi!" });
                    Private.LoadBegin(Session, ViewBag);
                    return View();
                }
                Private.LoadBegin(Session, ViewBag);
                var model = _iOrderService.GetById(_order.OrderId);
                ViewBag.Cus = _iCustomerService.GetById((long)model.CustomerId);
                return View(model);
            }
            catch (Exception ex)
            {
                //Error
                SetMessageCurrent(new List<string>() { ex.ToString() });
                Private.LoadBegin(Session, ViewBag);
                return View();
            }
        }
        [SessionLoginFilter]
        public ActionResult CancelOrder(string code)
        {
            OrderRepository _OrderRepository = new OrderRepository();
            var order = _OrderRepository.GetByCode(code);
            if (order.IsOrderConfirmed != true || (order.IsOrderConfirmed == true && order.IsPaymentConfirmed == null && order.PaymentTypeCode == "PTC-1"))
                _OrderRepository.Fail(code);
            else
                SetMessageCurrent(new List<string>() { "Don't cancel Order. Because, Order is Confirmed" });
            return RedirectToAction("TransactionHistory", "Customer");

        }
        public ActionResult OrderComplete()
        {
            if (TempData["OrderComplete"] == null)
                return RedirectToAction("Index", "Home");
            var odId = (long)TempData["OrderComplete"];
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            ProvineRepository _ProvinceRepository = new ProvineRepository();
            BankRepository _BankRepository = new BankRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
            ProvineRepository _ProvineRepository = new ProvineRepository();
            TransactionStatusRepository _TransactionStatusRepository = new TransactionStatusRepository();
            #endregion
            var Order = _OrderRepository.GetById(odId);
            //var OrderDetail = _OrderDetailRepository.GetByOrder(odId);
            //var Products = new List<Product>();
            //foreach (var item in OrderDetail)
            //{
            //    var itemProduct = _ProductRepository.GetById(item.ProductId);
            //    Products.Add(itemProduct);
            //}
            ViewBag.Order = Order;
            //ViewBag.OrderDetails = OrderDetail;
            //ViewBag.Products = Products;
            //ViewBag.Districts = _DistrictRepository.GetAll();
            //ViewBag.Provines = _ProvineRepository.GetAll();
            //ViewBag.Banks = _BankRepository.GetAll(false);
            //ViewBag.TransactionStatusName = _TransactionStatusRepository.GetByCode(Order.TransactionStatusCode).TransactionStatusName;
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("OrderComplete", "Product");
            return View();
        }
        [SessionLoginFilter]
        public ActionResult OrderInfor(long id)
        {
            var odId = id;
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
            TransactionStatusRepository _TransactionStatusRepository = new TransactionStatusRepository();
            #endregion
            var Order = _OrderRepository.GetById(odId);
            var OrderDetail = _OrderDetailRepository.GetByOrder(odId);
            var Products = new List<Product>();
            foreach (var item in OrderDetail)
            {
                var itemProduct = _ProductRepository.GetById(item.ProductId);
                Products.Add(itemProduct);
            }
            SizeRepository _SizeRepository = new SizeRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            ProvineRepository _ProvineRepository = new ProvineRepository();
            ViewBag.District = _DistrictRepository.GetById(Order.ShipToDistrict);
            ViewBag.Province = _ProvineRepository.GetById(Order.ShipToCity);
            ViewBag.Order = Order;
            ViewBag.OrderDetails = OrderDetail;
            ViewBag.Products = Products;
            ViewBag.ListSize = _SizeRepository.GetAll(false);
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("OrderInfor", "Product", new { id = id });
            return View();
        }
        #endregion
        #region Ajax

        public ActionResult UpdateCart(long id, int count)
        {
            try
            {
                var lstCart = GetCart();
                var itemCart = lstCart.Where(n => n.Item1 == id).FirstOrDefault();
                return Json("ok");
            }
            catch
            {
                return Json("false");
            }
        }
        [HttpPost]
        public ActionResult AddToCart(long id, long Size, int Quantity)
        {
            try
            {
                #region load
                ProductRepository productRepository = new ProductRepository();
                #endregion
                var _product = productRepository.GetById(id);
                if (ProductError(_product) == true)
                    return Json(new { result = false });
                var lstCart = GetCart();
                if (lstCart.Where(n => n.Item1 == id && n.Item3 == Size).ToList().Count == 0)
                    lstCart.Add(new Tuple<long, int, long>(id, Quantity, Size));
                else
                {
                    //var rs = lstCart.Where(n => n.Item1 == id && n.Item3 == Size).FirstOrDefault();
                    var lst = new List<Tuple<long, int, long>>();
                    foreach (var item in lstCart)
                    {
                        if (item.Item1 == id && item.Item3 == Size)
                        {
                            lst.Add(new Tuple<long, int, long>(id, item.Item2 + Quantity, Size));
                        }
                        else lst.Add(item);
                    }
                    lstCart = lst;
                    //return Json(new { result = false });
                }
                var sessionObject = (SessionObject)Session["sessionObject"];
                if (sessionObject == null)
                {
                    SessionObject sO = new SessionObject();
                    sO.PaymentProduct = new List<Tuple<long, int, long, long, long>>();
                    sO.ListProduct = new List<long>();
                    sO.ListCart = lstCart;
                    sO.lang = "vi";
                    Session.Add("sessionObject", sO);
                }
                //Session.Add("sessionCartGala", lstCart);
                else
                {
                    sessionObject.ListCart = lstCart;
                    Session["sessionObject"] = sessionObject;
                }
                ProductRepository _ProductRepository = new ProductRepository();
                var lstProduct = new List<Tuple<Product, ProductInMedia>>();
                double value = 0;
                foreach (var item in sessionObject.ListCart)
                {
                    var itemProduct = _ProductRepository.GetById(item.Item1);
                    value += itemProduct.PromotePrice != null ? Convert.ToDouble(itemProduct.PromotePrice) : Convert.ToDouble(itemProduct.MobileOnlinePrice);
                }
                ViewBag.totalMoneyCart = string.Format("{0:0,0 đ}", value).ToString();
                #region Statistic Nhúng code thống kê
                ProductLogRepository slog = new ProductLogRepository();
                ProductLog stl = new ProductLog();
                stl.ProductId = id;
                slog.ProductInsertStatistics(stl, 4);//Add to cart
                #endregion
                return Json(new { result = true, newValue = lstCart.Count, url = Url.Action("Cart", "Product"), newMoney = ViewBag.totalMoneyCart });
            }
            catch
            {
                return Json(new { result = false });
            }
        }
        [HttpPost]
        public ActionResult RemoveToCart(long id)
        {
            try
            {
                var sessionObject = (SessionObject)Session["sessionObject"];
                if (sessionObject == null || sessionObject.ListCart.Count == 0)
                    Json(new { result = false });
                #region load
                ProductRepository productRepository = new ProductRepository();
                #endregion
                var _product = productRepository.GetById(id);
                if (ProductError(_product) == true) return Json(new { result = false });
                var lstCart = GetCart();
                if (lstCart.Where(n => n.Item1 == id).ToList().Count == 0) return Json(new { result = false });
                else
                {
                    var cart = lstCart.Where(n => n.Item1 == id).FirstOrDefault();
                    lstCart.Remove(cart);
                }
                sessionObject.ListCart = lstCart;
                Session["sessionObject"] = sessionObject;
                return Json(new { result = true, newValue = lstCart.Count });
            }
            catch
            {
                return Json(new { result = false });
            }
        }
        [HttpPost]
        public ActionResult AddToWishlist(long id, bool action)
        {
            if (Session["sessionGala"] == null)
                return Json(new { result = -1 });
            #region load
            ProductRepository productRepository = new ProductRepository();
            WishlistRepository _WishlistRepository = new WishlistRepository();
            #endregion
            var _product = productRepository.GetById(id);
            if (ProductError(_product) == true)
                return Json(new { result = -1 });
            var customer = (Customer)Session["sessionGala"];
            var lstWishlst = _WishlistRepository.GetByCustomer(customer.CustomerId);
            if (lstWishlst.Where(n => n.ProductId == id).ToList().Count == 0)
            {
                var result = 1;
                if (action == true)
                {
                    Wishlist wl = new Wishlist()
                    {
                        ProductId = id,
                        CreatedBy = customer.CustomerId,
                        IsActive = action,
                        IsDeleted = false,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        ModifiedBy = customer.CustomerId,
                        CustomerId = customer.CustomerId
                    };
                    #region Statistic Nhúng code thống kê
                    ProductLogRepository slog = new ProductLogRepository();
                    ProductLog stl = new ProductLog();
                    stl.ProductId = id;
                    slog.ProductInsertStatistics(stl,5);//Add to wish list
                    #endregion
                    var rs = _WishlistRepository.Create(wl);
                    result = 2;
                }
                ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                var lstWishList = _WishlistRepository.GetByCustomer(customer.CustomerId).Where(n => n.IsDeleted == false && n.IsActive == true).ToList();
                var length = 0;
                foreach (var item in lstWishList)
                {
                    var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId).Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                    if (ProductError(itemProduct.Product) == false)
                        length++;
                }
                return Json(new { result = result, newValue = length });
            }
            else
            {
                var result = 1;
                if (action == true)
                    result = 2;
                else
                {
                    var rs = _WishlistRepository.RemoveByProduct(id);
                    if (rs == false) return Json(new { result = -1 });
                }
                ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                var lstWishList = _WishlistRepository.GetByCustomer(customer.CustomerId).Where(n => n.IsDeleted == false && n.IsActive == true).ToList();
                var length = 0;
                foreach (var item in lstWishList)
                {
                    var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId).Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                    if (ProductError(itemProduct.Product) == false)
                        length++;
                }
                return Json(new { result = result, newValue = length });
            }
        }
        [HttpPost]
        public ActionResult DeleteAllWishList()
        {
            if (Session["sessionGala"] == null)
                return Json(new { result = false });
            var acc = (Customer)Session["sessionGala"];
            WishlistRepository _WishlistRepository = new WishlistRepository();
            if (_WishlistRepository.GetByCustomer(acc.CustomerId).Where(n => n.IsActive == true).ToList().Count == 0)
                return Json(new { result = false });
            if (_WishlistRepository.DeleteAllByCustomer(acc.CustomerId) == true)
                return Json(new { result = true, newValue = 0 });
            else
                return Json(new { result = false });
        }
        #endregion
        #region Private
        private void ViewedProduct(long id)
        {
            try
            {
                var sessionObject = (SessionObject)Session["sessionObject"];
                if (sessionObject == null)
                {
                    sessionObject = new SessionObject();
                    sessionObject.lang = "vi";
                    sessionObject.ListCart = new List<Tuple<long, int, long>>();
                    sessionObject.ListProduct = new List<long>();
                    sessionObject.PaymentProduct = new List<Tuple<long, int, long, long, long>>();
                    Session.Add("sessionObject", sessionObject);
                }
                sessionObject = (SessionObject)Session["sessionObject"];
                var lstProduct = sessionObject == null ? new List<long>() : sessionObject.ListProduct;
                if (lstProduct.Where(n => n == id).ToList().Count == 0)
                {
                    if (lstProduct.Count >= 10)
                        lstProduct.RemoveAt(9);
                    lstProduct.Add(id);
                }
                sessionObject.ListProduct = lstProduct;
                Session["sessionObject"] = sessionObject;
            }
            catch
            {
            }
        }
        private List<long> ListViewedProduct()
        {
            try
            {
                var sessionObject = (SessionObject)Session["sessionObject"];
                return sessionObject == null ? new List<long>() : sessionObject.ListProduct;
            }
            catch
            {
                return new List<long>();
            }
        }
        private bool checkOrder(Order model)
        {
            var errorrs = new List<string>();
            var flag = false;
            Int64 outInt = 0;
            try
            {
                if (model.PaymentTypeCode == null || model.PaymentTypeCode == "")
                {
                    errorrs.Add((string)ViewBag.multiRes.GetString("alert_select_payment_type", ViewBag.CultureInfo));
                    flag = true;
                }
                if (model.PaymentTypeCode == "PTC-2" && (model.BankId == null || model.BankId <= 0))
                {
                    ModelState.AddModelError("BankId", (string)ViewBag.multiRes.GetString("alert_bank_null", ViewBag.CultureInfo));
                    flag = true;
                }
                if (model.PaymentTypeCode == "PTC-2" && (model.CardNumber == null || model.CardNumber.Value.ToString().Length != 16 || Int64.TryParse(model.CardNumber.Value.ToString(), out outInt) == false))
                {
                    ModelState.AddModelError("CardNumber", (string)ViewBag.multiRes.GetString("alert_input_cardnumber", ViewBag.CultureInfo));
                    flag = true;
                }
                if (model.PaymentTypeCode == "PTC-2" && (model.CardHolderName == null || model.CardHolderName.Length == 0))
                {
                    ModelState.AddModelError("CardHolderName", (string)ViewBag.multiRes.GetString("alert_input_card_name", ViewBag.CultureInfo));
                    flag = true;
                }
                if (model.ShipToAddress == null || model.ShipToAddress.Length == 0)
                {
                    ModelState.AddModelError("ShipToAddress", (string)ViewBag.multiRes.GetString("alert_input_address_null", ViewBag.CultureInfo));
                    flag = true;
                }
                if (model.ReceiverName == null || model.ReceiverName.Length == 0)
                {
                    ModelState.AddModelError("ReceiverName", (string)ViewBag.multiRes.GetString("alert_input_receivername_null", ViewBag.CultureInfo));
                    flag = true;
                }
                if (model.ReceiverPhone == null || model.ReceiverPhone.Length == 0)
                {
                    ModelState.AddModelError("ReceiverPhone", (string)ViewBag.multiRes.GetString("alert_input_phone_null", ViewBag.CultureInfo));
                    flag = true;
                }
                if (flag == true)
                    SetMessageCurrent(errorrs);
                return flag;
            }
            catch
            {
                return true;
            }
        }
        private List<Tuple<long, int, long>> GetCart()
        {
            var sessionObject = (SessionObject)Session["sessionObject"];
            if (sessionObject == null || sessionObject.ListCart.Count == null)
                return new List<Tuple<long, int, long>>();
            //if (Session["sessionCartGala"] == null) return new List<Tuple<long, int>>();
            var lstCart = sessionObject.ListCart;
            return lstCart;
        }
        private bool ProductError(Product model)
        {
            try
            {
                List<string> lstError = new List<string>();
                var error = false;
                StoreRepository _StoreRepository = new StoreRepository();
                if (model == null || _StoreRepository.CheckStoreOnline(Convert.ToInt64(model.StoreId)) == false)
                {
                    lstError.Add((string)ViewBag.multiRes.GetString("alert_product_not_find", ViewBag.CultureInfo));
                    error = true;
                }
                if (model.IsVerified == false || model.IsDeleted == true || model.IsActive == false || model.ProductStatusCode == "PSC2")
                {
                    lstError.Add((string)ViewBag.multiRes.GetString("alert_product_not_active", ViewBag.CultureInfo));
                    error = true;
                }
                if (error == true)
                    SetMessage(lstError);
                return error;
            }
            catch (Exception ex)
            {
                SetMessage(new List<string>() { ex.Message });
                return true;
            }
        }
        private void SetMessage(List<string> message)
        {
            if (TempData["gMessage"] != null) TempData.Remove("gMessage");
            TempData.Add("gMessage", message);
        }
        private void SetMessageCurrent(List<string> message)
        {
            var lst = new List<string>();
            if (TempData["gMessageCurrent"] != null)
            {
                lst.AddRange((List<string>)TempData["gMessageCurrent"]);
                TempData.Remove("gMessageCurrent");
            }
            lst.AddRange(message);
            TempData.Add("gMessageCurrent", lst);
        }
        //private void LoadWishList()
        //{
        //    if (Session["sessionGala"] != null)
        //    {
        //        var acc = (Customer)Session["sessionGala"];
        //        WishlistRepository _WishlistRepository = new WishlistRepository();
        //        var lstWishList = _WishlistRepository.GetByCustomer(acc.CustomerId);
        //        ViewBag.WishLists = lstWishList;
        //    }
        //    else
        //        ViewBag.WishLists = new List<Wishlist>();
        //}
        #region loadLange
        //private List<Wishlist> GetWishlist(List<Wishlist> lstWishList)
        //{
        //    ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();

        //    var lstNew = new List<Wishlist>();
        //    foreach (var item in lstWishList)
        //    {
        //        var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId).Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
        //        if (ProductError(itemProduct.Product) == false)
        //        {
        //            lstNew.Add(item);
        //        }
        //    }
        //    return lstNew;
        //}

        #endregion
        #endregion
    }
}
