﻿using HTTelecom.Domain.Core.DataContext.cis;
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

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class SearchController : Controller
    {
        [WhitespaceFilter]
        public ActionResult Index(long? cate, long? brand, int? step, string q, int? typeSearch)
        {
            q = q == null ? "" : q;
            cate = cate == null ? 0 : cate;
            brand = brand == null ? 0 : brand;
            step = step == null ? 0 : step;
            ViewBag.q = q;
            ViewBag.cate = cate;
            ViewBag.step = step;
            ViewBag.brand = brand;
            #region load
            ProductRepository _ProductRepository = new ProductRepository();
            ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
            BrandRepository _brandRepository = new BrandRepository();
            CategoryRepository _CategoryRepository = new CategoryRepository();
            #endregion
            var lstProduct = _ProductRepository.GetBySearch(Convert.ToInt64(cate), Convert.ToInt64(brand), q).GroupBy(n => n.ProductId).Select(g => g.First()).ToList();
            var lstProductInMedia = new List<ProductInMedia>();
            foreach (var item in lstProduct)
            {
                var lstItemPro = _productInMediaRepository.GetByProduct(item.ProductId);
                if (lstItemPro.Count > 0)
                {
                    var itemPro = lstItemPro.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                    if (itemPro != null)
                        lstProductInMedia.Add(itemPro);
                }
            }
            typeSearch = typeSearch == null ? 0 : typeSearch;
            ViewBag.typeSearch = typeSearch;
            if (typeSearch == 0)
                lstProductInMedia = lstProductInMedia.ToList();
            if (typeSearch == 1)
                lstProductInMedia = lstProductInMedia.OrderByDescending(n => n.Product.VisitCount).ToList();
            if (typeSearch == 2)
                lstProductInMedia = lstProductInMedia.OrderByDescending(n => n.Product.PromotePrice).ToList();
            if (typeSearch == 3)
                lstProductInMedia = lstProductInMedia.OrderBy(n => n.Product.PromotePrice).ToList();
            Private.LoadBegin(Session, ViewBag);
            if (Request.IsAjaxRequest())
            {
                if (step != null && step < 10)
                {
                    var lst = lstProductInMedia.Skip((Convert.ToInt32(step) * 10)).Take(10).ToList();
                    if (lst != null && lst.Count > 0)
                    {
                        Common.Common cm = new Common.Common();
                        List<ProductJson> lstJson = new List<ProductJson>();
                        foreach (var item in lst)
                        {
                            var hrefProduct = item.Product.ProductTypeCode == "PT1" ? "OrdProduct" : item.Product.ProductTypeCode == "PT2" ? "SaleProduct" : item.Product.ProductTypeCode == "PT3" ? "ChargeProduct" : "FreeProduct";
                            lstJson.Add(new ProductJson(item.Product.ProductId.ToString(),
                                item.Product.ProductName,
                                item.Product.ProductComplexName,
                                Url.Action(hrefProduct, "Product", new { id = item.Product.ProductId, urlName = item.Product.Alias, urlNameStore = item.Product.Store.Alias }),
                                item.Product.ProductCode, item.Product.MobileOnlinePrice.Value.ToString(), item.Product.PromotePrice.Value.ToString(), item.Product.StoreId.ToString(), item.Product.Store.StoreName, item.Product.Store.StoreCode, item.Product.VisitCount.ToString(), item.Media.Url, item.Media.MediaName
                                ));
                        }
                        return Json(cm.ProductToJson(lstJson));
                    }
                    else return Json("");
                }
                else
                    return Json("");
            }
            var lstBank = _brandRepository.GetAll(false,true);
            var lstCategory = _CategoryRepository.GetAll(true, false).Where(n => n.CateLevel == 0).ToList();
           
            Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            foreach (var item in lstCategory)
            {
                var cate_mutil = new Category_MultiLang();
                cate_mutil = _Category_MultiLangRepository.GetByLanguage(item.CategoryId, ViewBag.currentLanguage);
                if (cate_mutil != null)
                    item.CategoryName = cate_mutil.CategoryName;
            }
            ViewBag.ListProductInMedia = lstProductInMedia.Take(10).ToList();
            ViewBag.ListBank = lstBank;
            ViewBag.listCategory = lstCategory.OrderBy(n=>n.OrderNumber).ToList();
            ViewBag.u = Url.Action("Index", "Search", new { cate = cate, step = step, q = q });
            return View();
        } 
        //[HttpPost]
        //public ActionResult Index(long? cate, int? step, string q, int? typeSearch,FormCollection data)
        //{
        //    return View();
        //}


    }
}
