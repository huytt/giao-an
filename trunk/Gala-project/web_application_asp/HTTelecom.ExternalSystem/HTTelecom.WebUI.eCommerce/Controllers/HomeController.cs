using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.WebUI.eCommerce.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;
//using MvcPaging;
using dotless.Core;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.DataContext.mss;
using System.IO;
using System.IO.Compression;
using HTTelecom.WebUI.eCommerce.Filters;
using Newtonsoft.Json;
using System.Text;
using HTTelecom.Domain.Core.Repository.ops;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult Index()
        {
            Private.LoadBegin(Session, ViewBag);
            #region loadRepositoy
            MediaRepository _MediaRepository = new MediaRepository();
            StoreInMediaRepository _StoreInMediaRepository = new StoreInMediaRepository();
            ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
            BrandRepository _BrandRepository = new BrandRepository();
            StoreRepository _StoreRepository = new StoreRepository();
            #endregion
            var bannerMall = _MediaRepository.GetByHome().Distinct().Take(5).OrderByDescending(n => n.DateCreated).ToList();
            var lstStoreMall = _StoreInMediaRepository.GetByBannerMall();
            #region remove

            #region List Store
            var lstStore = _StoreInMediaRepository.GetByHome().Distinct().OrderByDescending(n => n.Store.DateCreated).ToList();
            var rs_store = Private.ConvertListStore(lstStore, Url);
            ViewBag.List_Store = rs_store;
            #endregion
            ////region p - 1.3
            #region List Product Sale & New
            var lstProduct_Sale = _productInMediaRepository.GetBySale().Take(10).ToList();
            ViewBag.List_Sale = Private.ConvertListProduct(lstProduct_Sale, Url);
            var lstProduct_new = _productInMediaRepository.GetByHome().OrderByDescending(n => n.Product.DateCreated).Take(10).ToList();
            ViewBag.List_New = Private.ConvertListProduct(lstProduct_new, Url);


            #endregion
            //var lstProduct = _productInMediaRepository.GetByHome().OrderByDescending(n => n.Product.DateCreated).ToList();
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
                if (mediaLogo != null)
                    Brands.Add(new Tuple<Brand, Media, Media>(item, mediaLogo, mediaBanner));
            }
            ViewBag.List_Brand = Private.ConvertListBrand(Brands, Url);
            #endregion
            #endregion
            #region Recently viewed products
            var sessionObject = (SessionObject)Session["sessionObject"];
            var lstViewd = sessionObject == null ? new List<long>() : sessionObject.ListProduct;
            #region remove
            //List<ProductInMedia> lstProductView = new List<ProductInMedia>();
            //ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
            //foreach (var item in lstViewd)
            //{
            //    var itemProduct = _ProductInMediaRepository.GetByProduct(item);
            //    var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
            //    if (itemPro != null)
            //        lstProductView.Add(itemPro);
            //}
            //ViewBag.ListProductViewedLength = lstProductView;
            #endregion

            #endregion
            #region remove
            //ViewBag.Products = lstProduct.Take(10).ToList();
            //ViewBag.ProductHots = lstProduct.OrderBy(n => n.Product.ProductName).Take(10).ToList();
            //ViewBag.ProductBuys = lstProduct.OrderByDescending(n => n.Product.ProductName).Take(10).ToList();
            #endregion
            #region remove
            //ViewBag.Stores = lstStore;
            //ViewBag.Brands = Brands.Where(n => n.Item2 != null).ToList();
            #endregion
            ViewBag.ListProductViewed = lstViewd.Count > 0 ? true : false;
            ViewBag.bannerMall = bannerMall;
            ViewBag.StoreMall = lstStoreMall;
            ViewBag.u = Url.Action("Index", "Home");
            return View();
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
        [HttpPost]
        public ActionResult Index(FormCollection formData)
        {
            var q = formData["Seach-txtSearch"].ToString();
            return RedirectToAction("Index", "Search", new { q = q });
        }
        public ActionResult IndexDesktop()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult setLanguage(string lang, string u)
        {
            if (lang == "English") lang = "en";
            else if (lang == "Chinese") lang = "zh";
            else lang = "vi";
            var sessionObject = (SessionObject)Session["sessionObject"];
            if (sessionObject == null)
            {
                sessionObject = new SessionObject();
                sessionObject.ListCart = new List<Tuple<long, int, long>>();
                sessionObject.ListProduct = new List<long>();
                sessionObject.lang = lang;
                sessionObject.PaymentProduct = new List<Tuple<long, int, long, long, long>>();
                Session.Add("sessionObject", sessionObject);
            }
            else
            {
                sessionObject.lang = lang;
                Session["sessionObject"] = sessionObject;
            }
            if (u != null && u != "") return Redirect(u);
            return RedirectToAction("Index");
        }
        #region remove
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
        //private void SetMessage(List<string> message)
        //{
        //    if (TempData["gMessage"] != null) TempData.Remove("gMessage");
        //    TempData.Add("gMessage", message);
        //}
        //public ActionResult setDevice(string version, string u)
        //{
        //    if (Session["Device"] != null) Session.Remove("Device");
        //    if (version == "Mobile") Session.Add("Device", "Mobile");
        //    else Session.Add("Device", "PC");
        //    if (u != null && u != "")
        //        return Redirect(u);
        //    return RedirectToAction("Index");
        //}
        #endregion
    }
}
