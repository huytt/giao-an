using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.mss;
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

namespace HTTelecom.WebUI.eCommerce.Common
{
    public static class Private
    {
        public static void LoadBegin(HttpSessionStateBase Session, dynamic ViewBag)
        {
            var sessionObject = (SessionObject)Session["sessionObject"];
            if (sessionObject == null)
            {
                SessionObject sO = new SessionObject();
                sO.lang = "vi";
                sO.ListCart = new List<Tuple<long, int, long>>();
                sO.ListProduct = new List<long>();
                sO.PaymentProduct = new List<Tuple<long, int, long, long, long>>();
                Session.Add("sessionObject", sO);
            }
            sessionObject = (SessionObject)Session["sessionObject"];
            ViewBag.currentLanguage = sessionObject.lang;
            #region
            ResourceManager multiRes = new ResourceManager("HTTelecom.WebUI.eCommerce.Common.Lang", Assembly.GetExecutingAssembly());
            CultureInfo ci = new CultureInfo(ViewBag.currentLanguage);
            #endregion
            ViewBag.multiRes = multiRes;
            ViewBag.CultureInfo = ci;
            if (Session["sessionGala"] != null)
            {
                var acc = (Customer)Session["sessionGala"];
                WishlistRepository _WishlistRepository = new WishlistRepository();
                var lstWishlist = _WishlistRepository.GetByCustomer(acc.CustomerId).Where(n => n.IsActive == true && n.IsDeleted == false).ToList();
                ViewBag.WishLists = lstWishlist;
                ViewBag.Layout_CountWishList = _WishlistRepository.GetWishlist(lstWishlist).Count;
            }
            else { ViewBag.WishLists = new List<Wishlist>(); }
            var lstCart = sessionObject.ListCart;
            ViewBag.Layout_CountCart = lstCart == null ? 0 : lstCart.Count;
        }
        internal static void SetMessageCurrent(List<string> list, TempDataDictionary TempData)
        {
            var lst = new List<string>();
            if (TempData["gMessageCurrent"] != null)
            {
                lst.AddRange((List<string>)TempData["gMessageCurrent"]);
                TempData.Remove("gMessageCurrent");
            }
            lst.AddRange(list);
            TempData.Add("gMessageCurrent", lst);
        }

        internal static void SaveCart(List<Tuple<long, int, long>> lstNewCart, HttpSessionStateBase Session)
        {
            var sessionObject = (SessionObject)Session["sessionObject"];
            if (sessionObject == null)
            {
                sessionObject = new SessionObject();
                sessionObject.lang = "vi";
                sessionObject.ListCart = lstNewCart;
                sessionObject.ListProduct = new List<long>();
                sessionObject.PaymentProduct = new List<Tuple<long, int, long, long, long>>();
                Session.Add("sessionObject", sessionObject);
            }
            else
            {
                sessionObject.ListCart = lstNewCart;
                Session["sessionObject"] = sessionObject;
            }
        }

        internal static void GetMutilLanguage(dynamic ViewBag, HttpSessionStateBase Session)
        {
            var sessionObject = (SessionObject)Session["sessionObject"];
            ViewBag.currentLanguage = sessionObject == null ? "vi" : sessionObject.lang;
            ResourceManager multiRes = new ResourceManager("HTTelecom.WebUI.eCommerce.Common.Lang", Assembly.GetExecutingAssembly());
            CultureInfo ci = new CultureInfo(ViewBag.currentLanguage);
            ViewBag.multiRes = multiRes;
            ViewBag.CultureInfo = ci;
        }
        internal static List<ProductObject> ConvertListProduct(List<HTTelecom.Domain.Core.DataContext.mss.ProductInMedia> ListProductInMedia, UrlHelper Url)
        {
            var lst = new List<ProductObject>();
            foreach (var item in ListProductInMedia)
            {
                ProductObject tmp = ConvertProduct(item, Url);
                if(tmp!=null)
                    lst.Add(tmp);
            }
            return lst;
        }
        internal static ProductObject ConvertProduct(HTTelecom.Domain.Core.DataContext.mss.ProductInMedia productInMedia,UrlHelper url)
        {
            if (productInMedia == null)
            {
                return null;
            }
            try
            {
                ProductRepository _iProductService = new ProductRepository();
                StoreRepository _iStoreService = new StoreRepository();
                MediaRepository _iMediaService = new MediaRepository();
                ProductObject _pObj_result = new ProductObject();
                MediaTypeRepository _MediaTypeService = new MediaTypeRepository();

                Product _Product = _iProductService.GetById((long)productInMedia.ProductId);
                Store _Store = _iStoreService.GetById((long)_Product.StoreId);
                Media _Media = _iMediaService.GetById((long)productInMedia.MediaId);

                _pObj_result.ProductId = (long)productInMedia.ProductId;
                _pObj_result.ProductName = _Product.ProductName;
                _pObj_result.ProductAlias = _Product.Alias;
                _pObj_result.ProductCode = _Product.ProductCode;
                _pObj_result.ProductStockCode = _Product.ProductStockCode;
                _pObj_result.MobileOnlinePrice =(double) _Product.MobileOnlinePrice;
                _pObj_result.PromotePrice = (double)_Product.PromotePrice;
                _pObj_result.Url = _Media.Url;
                _pObj_result.MediaName = _Media.MediaName;
                _pObj_result.StoreId = _Store.StoreId;
                _pObj_result.StoreName = _Store.StoreName;
                _pObj_result.Link = Private.GetLink(_Product.ProductId, _Product.Alias, _Store.Alias, _Product.ProductTypeCode, url);
                _pObj_result.StoreAlias = _Store.Alias;
                return _pObj_result;
            }
            catch
            {
                return null;
            }      
        }

        private static string GetLink(long ProductId, string ProductAlias, string StoreAlias, string ProductTypeCode,UrlHelper url)
        {

            var hrefProduct = ProductTypeCode == "PT1" ? "OrdProduct" : ProductTypeCode == "PT2" ? "SaleProduct" : ProductTypeCode == "PT3" ? "ChargeProduct" : "FreeProduct";
            return url.Action(hrefProduct, "Product", new { id = ProductId, urlName = ProductAlias, urlNameStore = StoreAlias });
        }

    }
}