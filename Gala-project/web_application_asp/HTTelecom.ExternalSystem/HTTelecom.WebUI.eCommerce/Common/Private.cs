using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.WebUI.eCommerce.Common;
using HTTelecom.WebUI.eCommerce.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;
namespace HTTelecom.WebUI.eCommerce.Common
{
    public static class Private
    {
        #region ProductObject Convert
        internal static List<ProductObject> ConvertListProductSimple(List<HTTelecom.Domain.Core.DataContext.mss.Product> ListProduct, UrlHelper Url)
        {
            var lst = new List<ProductObject>();
            foreach (var item in ListProduct)
            {
                ProductObject tmp = ConvertProduct(item, Url);
                if (tmp != null)
                    lst.Add(tmp);
            }
            return lst;
        }

        private static ProductObject ConvertProduct(Product item, UrlHelper Url)
        {
            if (item == null)
                return null;
            try
            {
                ProductObject _pObj_result = new ProductObject();
                _pObj_result.ProductId = Convert.ToInt64(item.ProductId);
                _pObj_result.ProductName = item.ProductName;
                _pObj_result.ProductComplexName = item.ProductComplexName;
                _pObj_result.ProductAlias = item.Alias;
                _pObj_result.Color = item.Colour;
                _pObj_result.ProductCode = item.ProductCode;
                _pObj_result.ProductStockCode = item.ProductStockCode;
                _pObj_result.StoreId = item.Store.StoreId;
                _pObj_result.StoreName = item.Store.StoreName;


                _pObj_result.Link = Private.GetLink(item.ProductId, item.Alias, item.Store.Alias, item.ProductTypeCode, Url);
                _pObj_result.StoreAlias = item.Store.Alias;
                _pObj_result.MobileOnlinePrice_write = string.Format("{0:0,0 đ}", _pObj_result.MobileOnlinePrice);
                _pObj_result.PromotePrice_write = string.Format("{0:0,0 đ}", _pObj_result.PromotePrice);
                return _pObj_result;
            }
            catch
            {
                return null;
            }
        }
        internal static List<ProductObject> ConvertListProduct(List<HTTelecom.Domain.Core.DataContext.mss.ProductInMedia> ListProductInMedia, UrlHelper Url)
        {
            var lst = new List<ProductObject>();
            foreach (var item in ListProductInMedia)
            {
                ProductObject tmp = ConvertProduct(item, Url);
                if (tmp != null)
                    lst.Add(tmp);
            }
            return lst;
        }
        internal static List<ProductObject> ConvertListProduct(List<Tuple<ProductInMedia, double>> ListProductInMedia, UrlHelper Url)
        {
            var lst = new List<ProductObject>();
            foreach (var item in ListProductInMedia)
            {
                ProductObject tmp = ConvertProduct(item.Item1, Url);
                if (tmp != null)
                    lst.Add(tmp);
            }
            return lst;
        }
        internal static ProductObject ConvertProduct(HTTelecom.Domain.Core.DataContext.mss.ProductInMedia productInMedia, UrlHelper url)
        {
            if (productInMedia == null)
                return null;
            try
            {
                ProductObject _pObj_result = new ProductObject();
                _pObj_result.ProductId = Convert.ToInt64(productInMedia.ProductId);
                _pObj_result.ProductName = productInMedia.Product.ProductName;
                _pObj_result.ProductComplexName = productInMedia.Product.ProductComplexName;
                _pObj_result.ProductAlias = productInMedia.Product.Alias;
                _pObj_result.Color = productInMedia.Product.Colour;
                _pObj_result.ProductCode = productInMedia.Product.ProductCode;
                _pObj_result.ProductStockCode = productInMedia.Product.ProductStockCode;
                _pObj_result.MobileOnlinePrice = (double)productInMedia.Product.MobileOnlinePrice;
                _pObj_result.PromotePrice = (double)productInMedia.Product.PromotePrice;
                _pObj_result.Url = productInMedia.Media.Url;
                _pObj_result.MediaName = productInMedia.Media.MediaName;
                _pObj_result.StoreId = productInMedia.Product.Store.StoreId;
                _pObj_result.StoreName = productInMedia.Product.Store.StoreName;
                _pObj_result.Link = Private.GetLink(productInMedia.Product.ProductId, productInMedia.Product.Alias, productInMedia.Product.Store.Alias, productInMedia.Product.ProductTypeCode, url);
                _pObj_result.StoreAlias = productInMedia.Product.Store.Alias;
                _pObj_result.MobileOnlinePrice_write = string.Format("{0:0,0 đ}", _pObj_result.MobileOnlinePrice);
                _pObj_result.PromotePrice_write = string.Format("{0:0,0 đ}", _pObj_result.PromotePrice);
                return _pObj_result;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region StoreObject Convert
        internal static List<StoreObject> ConvertListStore(List<HTTelecom.Domain.Core.DataContext.mss.StoreInMedia> listLtoreInMedia, UrlHelper Url)
        {
            var lst = new List<StoreObject>();
            foreach (var item in listLtoreInMedia)
            {
                StoreObject tmp = ConvertStore(item, Url);
                if (tmp != null)
                    lst.Add(tmp);
            }
            return lst;
        }
        internal static StoreObject ConvertStore(HTTelecom.Domain.Core.DataContext.mss.StoreInMedia storeInMedia, UrlHelper url)
        {
            if (storeInMedia == null)
                return null;
            try
            {
                StoreObject _pObj_result = new StoreObject();
                _pObj_result.Alias = storeInMedia.Store.Alias;
                _pObj_result.DateVerified = Convert.ToDateTime(storeInMedia.Store.DateVerified);
                _pObj_result.Link = url.Action("Index", "Store", new { id = storeInMedia.Store.StoreId, urlName = storeInMedia.Store.Alias });
                _pObj_result.MediaName = storeInMedia.Media.MediaName;
                _pObj_result.StoreCode = storeInMedia.Store.StoreCode;
                _pObj_result.StoreId = storeInMedia.Store.StoreId;
                _pObj_result.StoreName = storeInMedia.Store.StoreName;
                _pObj_result.Url = storeInMedia.Media.Url;
                return _pObj_result;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region BrandObject Convert
        internal static List<BrandObject> ConvertListBrand(List<Tuple<Brand, Media, Media>> lstBrand, UrlHelper Url)
        {
            var lst = new List<BrandObject>();
            foreach (var item in lstBrand)
            {
                BrandObject tmp = ConvertBrand(item, Url);
                if (tmp != null)
                    lst.Add(tmp);
            }
            return lst;
        }

        private static BrandObject ConvertBrand(Tuple<Brand, Media, Media> item, UrlHelper Url)
        {
            if (item == null)
                return null;
            try
            {
                BrandObject _pObj_result = new BrandObject();
                _pObj_result.Alias = item.Item1.Alias;
                _pObj_result.Banner_MediaName = item.Item3 != null ? item.Item3.MediaName : "";
                _pObj_result.Banner_Url = item.Item3 != null ? item.Item3.Url : "";
                _pObj_result.BrandId = item.Item1.BrandId;
                _pObj_result.BrandName = item.Item1.BrandName;
                _pObj_result.Link = Url.Action("Index", "Brand", new { id = item.Item1.BrandId, urlName = item.Item1.Alias });
                _pObj_result.Logo_MediaName = item.Item2 != null ? item.Item2.MediaName : "";
                _pObj_result.Logo_Url = item.Item2 != null ? item.Item2.Url : "";
                _pObj_result.VisitCount = Convert.ToInt64(item.Item1.VisitCount);
                return _pObj_result;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region CategoryObject Convert
        internal static List<CategoryObject> ConvertListCategory(List<Tuple<Category, int, Media, Media>> lstCategory, UrlHelper Url)
        {
            var lst = new List<CategoryObject>();
            foreach (var item in lstCategory)
            {
                CategoryObject tmp = ConvertCategory(item, Url);
                if (tmp != null)
                    lst.Add(tmp);
            }
            return lst;
        }

        private static CategoryObject ConvertCategory(Tuple<Category, int, Media, Media> item, UrlHelper Url)
        {
            if (item == null)
                return null;
            try
            {
                CategoryObject _pObj_result = new CategoryObject();
                _pObj_result.Alias = item.Item1.Alias;
                _pObj_result.Banner_MediaName = item.Item4 != null ? item.Item4.MediaName : "";
                _pObj_result.Banner_Url = item.Item4 != null ? item.Item4.Url : "";
                _pObj_result.CategoryId = item.Item1.CategoryId;
                _pObj_result.CategoryName = item.Item1.CategoryName;
                _pObj_result.CateLevel = item.Item1.CateLevel;
                _pObj_result.Link = Url.Action("Info", "Category", new { id = item.Item1.CategoryId, urlName = item.Item1.Alias });
                _pObj_result.Logo_MediaName = item.Item3 != null ? item.Item3.MediaName : "";
                _pObj_result.Logo_Url = item.Item3 != null ? item.Item3.Url : "";
                _pObj_result.OrderNumber = Convert.ToInt32(item.Item1.OrderNumber);
                _pObj_result.ParentCateId = item.Item1.ParentCateId;
                _pObj_result.ProductCount = item.Item2;
                _pObj_result.VisitCount = Convert.ToInt64(item.Item1.VisitCount);
                return _pObj_result;
            }
            catch
            {
                return null;
            }
        }

        #endregion
        #region  StatisticObject Convert

        #endregion
        public static string Decode(this string input)
        {
            return HttpUtility.UrlDecode(input);
        }
        public static string Encode(this string input)
        {
            return HttpUtility.UrlEncode(input);
        }
        public static double ToDateTimeUTC(this DateTime time)
        {
            DateTime date2 = new DateTime(1970, 01, 01);
            return (time - date2).TotalMilliseconds;
        }
        public static bool IsValidEmailAddress(this string emailAddress)
        {
            var valid = true;
            var isnotblank = false;

            var email = emailAddress.Trim();
            if (email.Length > 0)
            {
                // Email Address Cannot start with period.
                // Name portion must be at least one character
                // In the Name, valid characters are:  a-z 0-9 ! # _ % & ' " = ` { } ~ - + * ? ^ | / $
                // Cannot have period immediately before @ sign.
                // Cannot have two @ symbols
                // In the domain, valid characters are: a-z 0-9 - .
                // Domain cannot start with a period or dash
                // Domain name must be 2 characters.. not more than 256 characters
                // Domain cannot end with a period or dash.
                // Domain must contain a period
                isnotblank = true;
                valid = System.Text.RegularExpressions.Regex.IsMatch(email, @"\A([\w!#%&'""=`{}~\.\-\+\*\?\^\|\/\$])+@{1}\w+([-.]\w+)*\.\w+([-.]\w+)*\z", System.Text.RegularExpressions.RegexOptions.IgnoreCase) &&
                    !email.StartsWith("-") &&
                    !email.StartsWith(".") &&
                    !email.EndsWith(".") &&
                    !email.Contains("..") &&
                    !email.Contains(".@") &&
                    !email.Contains("@.");
            }

            return (valid && isnotblank);
        }
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


            ProductRepository _ProductRepository = new ProductRepository();
            var lstProduct = new List<Tuple<Product, ProductInMedia>>();
            double value = 0;
            foreach (var item in sessionObject.ListCart)
            {
                var itemProduct = _ProductRepository.GetById(item.Item1);
                value += itemProduct.PromotePrice != null ? Convert.ToDouble(itemProduct.PromotePrice) : Convert.ToDouble(itemProduct.MobileOnlinePrice);
            }
            ViewBag.totalMoneyCart = string.Format("{0:0,0 đ}", value).ToString();


            #region Alert
            AlertMessageRepository _AlertMessageRepository = new AlertMessageRepository();
            var lst = _AlertMessageRepository.GetAll(false);
            if (lst.Count > 0)
                ViewBag.gAlert = true;
            else
                ViewBag.gAlert = false;
            #endregion
            //#region Keyword
            //SearchKeywordRepository _SearchKeywordRepository = new SearchKeywordRepository();
            //ViewBag.ListKeyword = _SearchKeywordRepository.GetTop(5);
            //#endregion


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
            var lst = new List<Tuple<long, int, long, long, long>>();
            foreach (var item in lstNewCart)
            {
                lst.Add(new Tuple<long, int, long, long, long>(item.Item1, item.Item2, item.Item3, 0, 0));
            }
            if (sessionObject == null)
            {
                sessionObject = new SessionObject();
                sessionObject.lang = "vi";
                sessionObject.ListCart = lstNewCart;
                sessionObject.ListProduct = new List<long>();
                sessionObject.PaymentProduct = lst;
                Session.Add("sessionObject", sessionObject);
            }
            else
            {
                sessionObject.ListCart = lstNewCart;
                sessionObject.PaymentProduct = lst;
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
        //Link Product
        private static string GetLink(long ProductId, string ProductAlias, string StoreAlias, string ProductTypeCode, UrlHelper url)
        {
            var hrefProduct = ProductTypeCode == "PT1" ? "OrdProduct" : ProductTypeCode == "PT2" ? "SaleProduct" : ProductTypeCode == "PT3" ? "ChargeProduct" : "FreeProduct";
            return url.Action(hrefProduct, "Product", new { id = ProductId, urlName = ProductAlias, urlNameStore = StoreAlias });
        }
        internal static bool CheckProductAvailabel(ProductInMedia itemProduct)
        {
            try
            {
                List<string> lstError = new List<string>();
                var error = false;
                StoreRepository _StoreRepository = new StoreRepository();
                if (itemProduct.Product == null || _StoreRepository.CheckStoreOnline(Convert.ToInt64(itemProduct.Product.StoreId)) == false)
                    error = true;
                if (itemProduct.Product.IsVerified == false || itemProduct.Product.IsDeleted == true || itemProduct.Product.IsActive == false)
                    error = true;
                return error;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        internal static bool CheckProductAvailabel(Product product)
        {
            try
            {
                List<string> lstError = new List<string>();
                var error = false;
                StoreRepository _StoreRepository = new StoreRepository();
                if (product == null || _StoreRepository.CheckStoreOnline(Convert.ToInt64(product.StoreId)) == false)
                    error = true;
                if (product.IsVerified == false || product.IsDeleted == true || product.IsActive == false)
                    error = true;
                return error;
            }
            catch (Exception ex)
            {
                return true;
            }
        }



        internal static string GetDataWeb(string url)
        {
            try
            {
                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                //Set values for the request back
                req.Method = "GET";
                System.Net.WebResponse response = req.GetResponse();
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                return reader.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }
    }

}