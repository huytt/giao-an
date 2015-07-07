using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.WebUI.eCommerce.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class TemplateController : Controller
    {
        //
        // GET: /Template/

        //Template for Brand
        public ActionResult BrandHome()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Template for ListProduct [Home]
        [HttpGet]
        public ActionResult ProductSwip(int? type)
        {
            //type 1 : san pham moi nhat
            //type 2 : san pharm noi bat
            //     3  : san pham ban chay
            //type 4 : san pham vua xem
            if (Request.IsAjaxRequest())
            {
                type = type ?? 1;
                ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
                var lstProduct = _productInMediaRepository.GetByHome().OrderByDescending(n => n.Product.DateCreated).ToList();
                switch (Convert.ToInt32(type))
                {
                    case 1:
                        ViewBag.data = lstProduct.OrderByDescending(n => n.Product.ProductName).Take(10).ToList();
                        break;
                    case 2:
                        ViewBag.data = lstProduct.OrderBy(n => n.Product.ProductName).Take(10).ToList();
                        break;
                    case 3:
                        ViewBag.data = lstProduct.Take(10).ToList();
                        //????????????????/
                        break;
                    case 4:
                        var sessionObject = (SessionObject)Session["sessionObject"];
                        var lstViewd = sessionObject == null ? new List<long>() : sessionObject.ListProduct;
                        List<ProductInMedia> lstProductView = new List<ProductInMedia>();
                        ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                        foreach (var item in lstViewd)
                        {
                            var itemProduct = _ProductInMediaRepository.GetByProduct(item);
                            var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                            if (itemPro != null)
                                lstProductView.Add(itemPro);
                        }
                        ViewBag.data = lstProductView;
                        break;
                    default:
                        ViewBag.data = new List<ProductInMedia>();
                        break;
                }

                return PartialView(ViewBag.data);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Template for ListCategory [Home]
        [HttpGet]
        public ActionResult ListCategory(string lang, int number)
        {
            if (Request.IsAjaxRequest())
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
                Private.LoadBegin(Session, ViewBag);
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
