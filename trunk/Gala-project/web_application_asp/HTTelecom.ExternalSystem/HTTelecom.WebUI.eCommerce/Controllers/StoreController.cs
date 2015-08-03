using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MvcPaging;
using PagedList;
using PagedList.Mvc;
using HTTelecom.Domain.Core.Repository.cis;
using System.Resources;
using System.Globalization;
using System.Reflection;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.WebUI.eCommerce.Filters;
using HTTelecom.WebUI.eCommerce.Common;
using HTTelecom.Domain.Core.Common;
using HTTelecom.Domain.Core.Repository.sts;
using HTTelecom.Domain.Core.DataContext.sts;
namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class StoreController : Controller
    {
        private const int pageSize = 12;
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult Index(long id, int? page)
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
                return RedirectToAction("Index", "Home");
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
                return View();
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
            //List<ProductInMedia> lstProductView = new List<ProductInMedia>();
            //foreach (var item in lstViewd)
            //{
            //    var itemProduct = _ProductInMediaRepository.GetByProduct(item);
            //    var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
            //    if (itemPro != null)
            //        lstProductView.Add(itemPro);
            //}
            //ViewBag.ListProductViewed = lstProductView;
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
            ViewBag.u = Url.Action("Index", "Store", new { id = id, urlName = model.Alias, page = page });
            #region Nhúng code thống kê
            StoreLogRepository slog = new StoreLogRepository();
            StoreLog stl = new StoreLog();
            stl.StoreId = id;
            slog.StoreInsertStatistics(stl,(Session["sessionGala"] != null));
            #endregion
            
            return View();
        }
        [OutputCache(Duration = 30, VaryByParam = "id")]
        public ActionResult All(int? page, int? _numColumn, int? _sortBy, string _order)
        {
            int pageNum = (page ?? 1);
            _order = (_order != null && !_order.Equals("")) ? _order : "asc";
            _sortBy = _sortBy != null ? _sortBy : 0;

            ViewBag.page = page;
            StoreRepository _storeRepository = new StoreRepository();
            StoreInMediaRepository _StoreInMediaRepository = new StoreInMediaRepository();

            List<StoreInMedia> lstStore = _StoreInMediaRepository.GetAllStore()
                .OrderBy(n => n.Store.ShowInMallPage)
                .OrderBy(n => n.Store.VisitCount)
                .ToList();

            // Current only support 1 or 2 column.
            ViewBag.numColumn = 2;
            if (_numColumn != null)
            {
                ViewBag.numColumn = _numColumn;
            }

            // Sort: current only support Name and Time.
            // _sortBy: 0 -- None, 1 -- Name or 2 -- Time
            // _order: 0 -- Asc or 1 -- Desc
            ViewBag.sortBy = _sortBy;
            ViewBag.order = _order;
            if (_sortBy != null)
            {
                switch (_sortBy)
                {
                    case 0:
                        break;
                    case 1:
                        lstStore = Utils.Sort(lstStore,
                            n => n.Store.Alias,
                            _order);
                        break;
                    case 2:
                        lstStore = Utils.Sort(lstStore,
                            n => n.Store.DateVerified,
                            _order);
                        break;
                }
            }

            ViewBag.Stores = lstStore.ToPagedList(pageNum, 30);

            Private.LoadBegin(Session, ViewBag);
            return View();
        }
    }
}
