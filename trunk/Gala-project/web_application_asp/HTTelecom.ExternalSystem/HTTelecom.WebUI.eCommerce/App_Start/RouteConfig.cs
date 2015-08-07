using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
namespace HTTelecom.WebUI.eCommerce
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            #region Article
            routes.MapRoute(
             name: "Article",
             url: "Article/Infor/{id}.html",
             defaults: new { controller = "Article", action = "Info", id = UrlParameter.Optional },
            constraints: new { id = @"\d+" },
            namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ArticleController).Namespace, }
             );
            #endregion
            #region Brand
            routes.MapRoute(
              name: "Brand",
              url: "Brand.html",
              defaults: new { controller = "Brand", action = "All", id = UrlParameter.Optional },
              namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.BrandController).Namespace, }
              );
            routes.MapRoute(
              name: "BrandInfo",
              url: "Brand/{urlName}-{id}.html",
              defaults: new { controller = "Brand", action = "Index", id = UrlParameter.Optional },
              constraints: new { id = @"\d+" },
              namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.BrandController).Namespace, }
              );
            #endregion
            #region Category
            routes.MapRoute(
              name: "CategoryInfo",
              url: "Category/{urlName}-{id}.html",
              defaults: new { controller = "Category", action = "Info", id = UrlParameter.Optional },
              constraints: new { id = @"\d+" },
              namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.CategoryController).Namespace, }
              );
            #endregion
            #region Product
            routes.MapRoute(
              name: "ProductWishlist",
              url: "WishList.html",
              defaults: new { controller = "Product", action = "WishList", id = UrlParameter.Optional }
              , namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
              );
            routes.MapRoute(
              name: "ProductCart",
              url: "Cart.html",
              defaults: new { controller = "Product", action = "Cart", id = UrlParameter.Optional }
              , namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
              );
            routes.MapRoute(
              name: "ProductPayment",
              url: "Payment.html",
              defaults: new { controller = "Product", action = "Payment", id = UrlParameter.Optional }
              , namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
              );
            routes.MapRoute(
             name: "ProductBuyNow",
             url: "BuyNow.html",
             defaults: new { controller = "Product", action = "BuyNow", id = UrlParameter.Optional }
             , namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
             );
            routes.MapRoute(
              name: "ProductPriority",
              url: "Priority.html",
              defaults: new { controller = "Product", action = "Priority", id = UrlParameter.Optional }
              , namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
              );

            routes.MapRoute(
              name: "OrdinarySale",
              url: "{urlNameStore}-1/{urlName}-{id}.html",
              defaults: new { controller = "Product", action = "OrdProduct", id = UrlParameter.Optional }
              , constraints: new { id = @"\d+" }, namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
              );
            routes.MapRoute(
             name: "TVSale",
             url: "{urlNameStore}-2/{urlName}-{id}.html",
             defaults: new { controller = "Product", action = "SaleProduct", id = UrlParameter.Optional }
             , constraints: new { id = @"\d+" }, namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
             );
            routes.MapRoute(
             name: "ChargedMediaStream",
             url: "{urlNameStore}/{urlName}-{id}.html",
             defaults: new { controller = "Product", action = "ChargeProduct", id = UrlParameter.Optional }
             , constraints: new { id = @"\d+" }, namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
             );
            routes.MapRoute(
             name: "FreeMediaStream",
             url: "{urlNameStore}/{urlName}-{id}.html",
             defaults: new { controller = "Product", action = "FreeProduct", id = UrlParameter.Optional }
             , constraints: new { id = @"\d+" }, namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.ProductController).Namespace, }
             );
            #endregion
            #region Customer
            routes.MapRoute(
              name: "CustomerLogin",
              url: "customer/login.html",
              defaults: new { controller = "Customer", action = "Login", id = UrlParameter.Optional }
              , namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.CustomerController).Namespace, }
              );
            routes.MapRoute(
              name: "CustomerSignUp",
              url: "customer/signup.html",
              defaults: new { controller = "Customer", action = "SignUp", id = UrlParameter.Optional }
              , namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.CustomerController).Namespace, }
              );
            routes.MapRoute(
              name: "CustomerProfile",
              url: "customer/profile.html",
              defaults: new { controller = "Customer", action = "Profile", id = UrlParameter.Optional }
              , namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.CustomerController).Namespace, }
              );
            #endregion

            routes.MapRoute(
              name: "Store",
              url: "{urlName}-{id}.html",
              defaults: new { controller = "Store", action = "Index"}
              , constraints: new { id = @"\d+" },
              namespaces: new string[] { typeof(HTTelecom.WebUI.eCommerce.Controllers.StoreController).Namespace, }
              );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}