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
using Newtonsoft.Json;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.DataContext.ams;

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class AppController : Controller
    {
        //
        // GET: /App/
        private const int pageSize = 12;
        public string OrdProduct(long id)
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
            if (Private.CheckProductAvailabel(model) == true || model.ProductStatusCode == null || model.ProductStatusCode == "PSC2")
                return "";
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
            var lstPro = lstWeight.Where(n => n.Type == "2").ToList();
            var lstDis = lstWeight.Where(n => n.Type == "1").ToList();
            var lstProvince = new List<Province>();
            var lstDistrict = _DistrictRepository.GetAll();
            foreach (var item in lstPro)
            {
                if (lstProvince.Where(n => n.ProvinceId == item.TargetId).ToList().Count == 0)
                    lstProvince.Add(_ProvineRepository.GetById(Convert.ToInt64(item.TargetId)));
            }
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
            var sessionObject = (SessionObject)Session["sessionObject"];
            var lstViewd = sessionObject.ListProduct;
            List<ProductInMedia> lstProductView = new List<ProductInMedia>();
            foreach (var item in lstViewd)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item);
                var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (itemPro != null)
                    lstProductView.Add(itemPro);
            }
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
            model.ProductInCategory.Clear();
            model.ProductInCategory=null;
            model.Store.Product.Clear();
            model.Store.Product = null;
            model.Store.StoreInMedia.Clear();
            model.Store.StoreInMedia = null;
            model.ProductInMedia.Clear();
            model.ProductInMedia = null;
            model.Gift.Clear();
            model.Gift = null;
            model.Brand.Product.Clear();
            model.Brand.Product = null;
            var data = new
            {
                status = status,
                ListSizeGlobal = ListSizeGlobal,
                ListSize = ListSize,
                ListColour = Json_ProductInMedia(ListColour),
                ListProductViewed = Json_ProductInMedia(lstProductView),
                ProductInMedia = Json_ProductInMedia(ListMedia.Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-2").ToList()),
                complexBanner = Json_ProductInMedia(ListMedia.Where(n => n.Media.MediaType.MediaTypeCode == "PRODUCT-1").ToList()),
                model = model
            };
            return JsonConvert.SerializeObject(data);
        }
        public string Brand(long id, int? page, int? typeSearch)
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
                return "";

            #endregion

            #region List Product & Filter
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
            var sessionObject = (SessionObject)Session["sessionObject"];
            var lstViewd = sessionObject == null ? new List<long>() : sessionObject.ListProduct;
            ViewBag.ListProductViewed = lstViewd.Count > 0 ? true : false;
            #endregion
            ViewBag.Model = ModelBrand;
            ViewBag.CountProduct = lst.Count;
            ViewBag.ProductInMediaHot = lst.OrderByDescending(n => n.Product.VisitCount).Take(10).ToList();
            ViewBag.ProductInMedia = lst.ToPagedList(pageNum, pageSize);
            _BrandRepository.UpdateVisitCount(id);
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("Index", "Brand", new { id = id });
            var data = new
            {
                product_hot = Json_ProductInMedia(ViewBag.ProductInMediaHot),
                product_buy = Json_ProductInMedia(lst.Skip(pageNum * pageSize).Take(pageSize).ToList()),
                Count = lst.Count,
                brand = Json_Brand(new List<Tuple<Brand, Media, Media>>() { ModelBrand })
            };
            return JsonConvert.SerializeObject(data);
        }
        public string Store(long id, int? page)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = page;
            var toDay = DateTime.Now;
            #region load
            StoreRepository _storeRepository = new StoreRepository();
            StoreInMediaRepository _StoreInMediaRepository = new StoreInMediaRepository();
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            VendorRepository _VendorRepository = new VendorRepository();
            #endregion
            var model = _storeRepository.GetById(id);
            #region remove
            //var store = _data.Store.Where(n => n.StoreId == id && n.IsVerified == true && n.IsDeleted == false && n.IsActive == true && n.OnlineDate.HasValue == true && n.OfflineDate.HasValue == true).FirstOrDefault();
            //if (store == null)
            //    return false;
            //if ((toDay - store.OnlineDate.Value).TotalMinutes >= 0 && (store.OfflineDate.Value - toDay).TotalMinutes >= 0)
            //    return true;
            //var checkOnline = _storeRepository.CheckStoreOnline(id);
            #endregion
            if (model == null || model.IsActive == false || model.IsDeleted == true || model.IsVerified == false || model.OnlineDate.HasValue == false || model.OfflineDate.HasValue == false || (model.OnlineDate.HasValue == true && model.OfflineDate.HasValue == true && (model.OfflineDate.Value - model.OnlineDate.Value).TotalMinutes < 0))
                return "";
            Private.LoadBegin(Session, ViewBag);
            ViewBag.Store = model;

            ViewBag.showStore = true;
            var lst = _storeRepository.GetStoreOrther(id);
            ViewBag.ListOtherStore = lst;
            var lstStoreMedia = _StoreInMediaRepository.GetByStoreId(id);
            ViewBag.LstStoreInMedia = lstStoreMedia;
            if ((toDay - model.OnlineDate.Value).TotalMinutes < 0)
            {
                ViewBag.showStore = false;
                return "";
            }
            var lstProduct = _ProductRepository.GetByStore(id);
            List<ProductInMedia> lstProductInMedia = new List<ProductInMedia>();
            List<ProductInMedia> lstProductInMediaBanner = new List<ProductInMedia>();
            foreach (var item in lstProduct)
            {
                var lstItem = _ProductInMediaRepository.GetByProduct(item.ProductId).Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).ToList();
                lstProductInMedia.AddRange(lstItem);
                if (item.ShowInStorePage == true)
                    lstProductInMediaBanner.AddRange(lstItem);
            }
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
            #region remove
            //int currentPage = page.HasValue ? page.Value : 1;
            //ViewBag.currentPage = currentPage;
            //if (Request.IsAjaxRequest())
            //    return PartialView("AjaxProductMediaInStore", lstProductInMedia.ToPagedList(currentPage, 6));
            #endregion
            _storeRepository.VisitCount(id);
            ViewBag.ListProductViewed = lstViewd.Count > 0 ? true : false;
            ViewBag.Vendor = _VendorRepository.GetById(Convert.ToInt64(model.VendorId));
            ViewBag.LstProductInMedia = lstProductInMedia.ToPagedList(pageNum, pageSize);
            ViewBag.lstProductInMediaBanner = lstProductInMediaBanner;
            var data = new
            {
                CountProduct = lst.Count,
                lstProductInMediaBanner = Json_ProductInMedia(lstProductInMediaBanner),
                lstProductView = Json_ProductInMedia(lstProductView),
                LstProductInMedia = Json_ProductInMedia(lstProductInMedia.Skip(pageNum * pageSize).Take(pageSize).ToList())
            };
            return JsonConvert.SerializeObject(data);
        }
        public string CategoryInfo(long id, int? page, int? typeSearch)
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
                return "";
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
            var data = new
            {
                ListChildren = Json_Category(lstChildren),
                CountProduct = lst.Count,
                Category = Json_Category(new List<Tuple<Category, Media, Media>>() { ModelCategory }),
                ProductInMediaHot = Json_ProductInMedia(ViewBag.ProductInMediaHot),
                ProductInMedia = Json_ProductInMedia(lst.Skip(pageNum * pageSize).Take(pageSize).ToList())
            };
            return JsonConvert.SerializeObject(data);
        }

        private List<Tuple<Category, Media, Media>> Json_Category(List<Tuple<Category, Media, Media>> lstChildren)
        {
            foreach (var item in lstChildren)
            {
                item.Item1.Description = null;
                if (item.Item2 != null && item.Item2.MediaType == null)
                    item.Item2.MediaType = new MediaType();
                if (item.Item3 != null && item.Item3.MediaType == null)
                    item.Item3.MediaType = new MediaType();
                if (item.Item2 != null)
                {
                    item.Item2.MediaType.Media.Clear();
                    item.Item2.MediaType.Media = null;
                }
                if (item.Item3 != null)
                {
                    item.Item3.MediaType.Media.Clear();
                    item.Item3.MediaType.Media = null;
                }
                item.Item1.ProductInCategory.Clear();
                item.Item1.ProductInCategory = null;
            }
            return lstChildren;
        }
        #region API for App
        public string home_app(string lang)
        {
            lang = lang ?? "vi";
            //LoadBegin();
            //LoadWishList();
            #region loadRepositoy
            MediaRepository _MediaRepository = new MediaRepository();
            StoreInMediaRepository _StoreInMediaRepository = new StoreInMediaRepository();
            ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
            BrandRepository _BrandRepository = new BrandRepository();
            #endregion
            #region mall
            var bannerMall = _MediaRepository.GetByHome().Distinct().Take(5).OrderByDescending(n => n.DateCreated).ToList();
            var lstStoreMall = _StoreInMediaRepository.GetByBannerMall();
            #endregion
            #region Store
            var lstStore = _StoreInMediaRepository.GetByHome().Distinct().OrderByDescending(n => n.Store.DateCreated).ToList();
            #endregion

            #region Product
            var lstProduct = _productInMediaRepository.GetByHome().GroupBy(i => i.ProductId).Select(g => g.First()).OrderByDescending(n => n.Product.DateCreated).ToList();
            ViewBag.Products = lstProduct.Take(10).ToList();
            var ProductHots = lstProduct.OrderBy(n => n.Product.ProductName).Take(10).ToList();
            var ProductBuys = lstProduct.OrderByDescending(n => n.Product.ProductName).Take(10).ToList();
            #endregion

            #region Brand
            var lstBrand = _BrandRepository.GetAll(false, true);
            List<Tuple<Brand, Media, Media>> Brands = new List<Tuple<Brand, Media, Media>>();
            foreach (var item in lstBrand)
            {
                var mediaLogo = new Media();
                var mediaBanner = new Media();
                if (item.LogoMediaId != null && item.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(item.LogoMediaId));
                else mediaLogo = null;
                if (item.BannerMediaId != null && item.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(item.BannerMediaId));
                else mediaBanner = null;
                Brands.Add(new Tuple<Brand, Media, Media>(item, mediaLogo, mediaBanner));
            }
            #endregion

            #region category
            CategoryRepository _CategoryRepository = new CategoryRepository();
            Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            var LstCate = _CategoryRepository.GetCateLevel(0).OrderBy(n => n.OrderNumber).ToList();
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
            #endregion

            #region Article
            #region load
            ArticleRepository _ArticleRepository = new ArticleRepository();
            ArticleTypeRepository _ArticleTypeRepository = new ArticleTypeRepository();
            #endregion
            var LstArticle = _ArticleRepository.GetAll(false, lang);
            foreach (var item in LstArticle)
            {
                item.ArticleType = null;
            }
            var LstArticleType = _ArticleTypeRepository.GetAll(false, lang);
            foreach (var item in LstArticleType)
            {
                item.Article = null;
            }
            #endregion
            var bannerMall_JS = bannerMall;
            var Stores_JS = lstStore;
            var Brands_JS = Brands;
            //, store = Stores_JS, brand = Brands_JS, product_hot = ProductHots, product_buy = ProductBuys
            var data = new
            {
                store = Json_StoreInMedia(lstStore),
                brand = Json_Brand(Brands),
                mall = Json_media(bannerMall),
                product_hot = Json_ProductInMedia(ViewBag.Products),
                product_buy = Json_ProductInMedia(ProductBuys),
                category = Json_Category(lst),
                article = (LstArticle),
                articletype = (LstArticleType)
            };
            return JsonConvert.SerializeObject(data);
        }

        public string category_app(string lang)
        {
            #region load
            CategoryRepository _CategoryRepository = new CategoryRepository();
            Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            lang = lang == null ? "vi" : lang;
            #endregion
            var LstCate = _CategoryRepository.GetAllAndProductCount(true, false);
            List<Tuple<Category, Media, Media, Category_MultiLang, int>> lst = new List<Tuple<Category, Media, Media, Category_MultiLang, int>>();
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
                lst.Add(new Tuple<Category, Media, Media, Category_MultiLang, int>(item.Item1, mediaLogo, mediaBanner, cate_mutil, item.Item2));
            }
            ViewBag.ListCategory = lst;
            //LoadBegin();
            Private.LoadBegin(Session, ViewBag);
            return JsonConvert.SerializeObject(Json_Category_All(lst));
        }
        #region Json
        private List<Tuple<Category, Media, Media, Category_MultiLang>> Json_Category(List<Tuple<Category, Media, Media, Category_MultiLang>> lst)
        {
            foreach (var item in lst)
            {
                item.Item1.Description = null;
                if (item.Item2 != null && item.Item2.MediaType == null)
                    item.Item2.MediaType = new MediaType();
                if (item.Item3 != null && item.Item3.MediaType == null)
                    item.Item3.MediaType = new MediaType();
                if (item.Item2 != null)
                {
                    item.Item2.MediaType.Media.Clear();
                    item.Item2.MediaType.Media = null;
                }
                if (item.Item3 != null)
                {
                    item.Item3.MediaType.Media.Clear();
                    item.Item3.MediaType.Media = null;
                }
                item.Item1.ProductInCategory.Clear();
                item.Item1.ProductInCategory = null;

            }
            return lst;
        }
        private List<Tuple<Category, Media, Media, Category_MultiLang, int>> Json_Category_All(List<Tuple<Category, Media, Media, Category_MultiLang, int>> lst)
        {
            foreach (var item in lst)
            {
                item.Item1.Description = null;
                if (item.Item2 != null && item.Item2.MediaType == null)
                    item.Item2.MediaType = new MediaType();
                if (item.Item3 != null && item.Item3.MediaType == null)
                    item.Item3.MediaType = new MediaType();
                if (item.Item2 != null)
                {
                    item.Item2.MediaType.Media.Clear();
                    item.Item2.MediaType.Media = null;
                }
                if (item.Item3 != null)
                {
                    item.Item3.MediaType.Media.Clear();
                    item.Item3.MediaType.Media = null;
                }
                item.Item1.ProductInCategory.Clear();
                item.Item1.ProductInCategory = null;

            }
            return lst;
        }
        public List<Media> Json_media(List<Media> media)
        {
            foreach (var item in media)
            {
                if (item.MediaType == null)
                    item.MediaType = new MediaType();
                item.MediaType.Media = null;
            }
            return media;
        }
        public List<Tuple<Brand, Media, Media>> Json_Brand(List<Tuple<Brand, Media, Media>> media)
        {

            foreach (var item in media)
            {
                item.Item1.Description = null;
                if (item.Item2 != null && item.Item2.MediaType == null)
                    item.Item2.MediaType = new MediaType();
                if (item.Item3 != null && item.Item3.MediaType == null)
                    item.Item3.MediaType = new MediaType();
                if (item.Item2 != null)
                {
                    item.Item2.MediaType.Media.Clear();
                    item.Item2.MediaType.Media = null;
                }
                if (item.Item3 != null)
                {
                    item.Item3.MediaType.Media.Clear();
                    item.Item3.MediaType.Media = null;
                }
                item.Item1.Product.Clear();
                item.Item1.Product = null;
            }
            return media;
        }
        public List<StoreInMedia> Json_StoreInMedia(List<StoreInMedia> media)
        {
            foreach (var item in media)
            {
                item.Store.Description = null;
                if (item.Media == null)
                    item.Media = new Media();
                if (item.Media.MediaType == null)
                    item.Media.MediaType = new MediaType();
                if (item.Media.MediaType.Media != null)
                {
                    item.Media.MediaType.Media.Clear();
                    item.Media.MediaType.Media = null;
                }
                item.Media.StoreInMedia = null;
                item.Media.ProductInMedia = null;
                item.Store.Product.Clear();
                item.Store.Product = null;
                item.Store.StoreInMedia.Clear();
                item.Store.StoreInMedia = null;
            }
            return media;
        }
        public List<ProductInMedia> Json_ProductInMedia(List<ProductInMedia> media)
        {
            var lst = media;
            foreach (var item in lst)
            {
                if (item.Media == null)
                    item.Media = new Media();
                if (item.Media.MediaType == null)
                    item.Media.MediaType = new MediaType();
                if (item.Media.MediaType.Media != null)
                {
                    item.Media.MediaType.Media.Clear();
                    item.Media.MediaType.Media = null;
                }
                item.Media.StoreInMedia = null;
                item.Media.ProductInMedia = null;
                if (item.Product.Store.StoreInMedia != null)
                    item.Product.Store.StoreInMedia.Clear();
                //item.Product.Store.StoreInMedia = new List<StoreInMedia>();
                item.Product.Store.StoreInMedia = null;
                if (item.Product.Store.Product != null)
                    item.Product.Store.Product.Clear();
                item.Product.Store.Product = null;
                if (item.Product.ProductInCategory != null)
                    item.Product.ProductInCategory.Clear();
                item.Product.ProductInCategory = null;
                if (item.Product.ProductInMedia != null)
                    item.Product.ProductInMedia.Clear();
                item.Product.Gift.Clear();
                item.Product.Gift = null;
                item.Product.ProductInMedia = null;
                if (item.Product.Brand != null)
                {
                    item.Product.Brand = null;
                }
            }
            return lst;
        }
        #endregion

        #endregion

    }
}
