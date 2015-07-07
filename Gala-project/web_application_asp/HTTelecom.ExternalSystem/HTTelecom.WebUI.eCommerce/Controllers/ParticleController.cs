using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.ams;
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
using PagedList;
using HTTelecom.Domain.Core.Common;
namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class ParticleController : Controller
    {
        //
        // GET: /Particle/

        public PartialViewResult Login(string ur)
        {
            ViewBag.ur = ur;
            if (Request.IsAjaxRequest())
            {
                loadCompoment();
                return PartialView();
            }
            else return PartialView(null);
        }
        public PartialViewResult SignUp()
        {
            if (Request.IsAjaxRequest())
            {
                loadCompoment();
                return PartialView();
            }
            else return PartialView(null);
        }
        public void loadCompoment()
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
            ViewBag.currentLanguage = sessionObject.lang;
            #region
            ResourceManager multiRes = new ResourceManager("HTTelecom.WebUI.eCommerce.Common.Lang", Assembly.GetExecutingAssembly());
            CultureInfo ci = new CultureInfo(ViewBag.currentLanguage);
            #endregion
            ViewBag.multiRes = multiRes;
            ViewBag.CultureInfo = ci;
        }



        #region  load Ajax

        //1. Load Menu Left
        //2. Load Province..
        //3. Load District By ProvinceId
        //4. Load Review By ProductId
        //5. Load ProductInMedia By ProductId
        [HttpGet]
        public ActionResult GetProductPriority(string _type, int? _step, string _sort,int? _orderby)
        {
            /*
             * 1.Type1: Selling Products(bán chạy)    ->"Selling"
             * 2.Type2: New Products(mới)             ->"New"
             * 3.Type3: Highlights Products(nổi bật)  ->"Highlights"
             * 4.Type4: Products Nomination(đề cử)    ->"Monination"
             */
            /*
             * 1.Sort1: Ascend      ->"asc"
             * 2.Sort2: Descending  ->"desc"
             */
            /*
             * 1._sortType 1: sort ProductId
             * 2._sortType 2: sort ProductName
             * 3._sortType 3: sort StoreId
             * 4._sortType 4: sort StoreName
             * 5._sortType 5: sort MobileOnlinePrice
             */
            ///bo qua 30....
            //Kiểm tra đối số
            _type = _type == "" || _type == null ? "SELLING" : _type;
            _sort = _sort == "" || _sort == null || !(_sort == "desc" || _sort == "asc") ? "desc" : _sort;
            _orderby = _orderby == null || !(_orderby >= 1 && _orderby <= 5) ? 1 : _orderby;//có 5 kiểu sort
            _step = _step ?? 0;

            //Load Object
            List<string> Error = new List<string>();
            ProductInPriorityRepository _iProductIsPriorityService = new ProductInPriorityRepository();
            ProductInMediaRepository _iProductInMediaService = new ProductInMediaRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            long MediaTypeId = _iMediaTypeService.GetByTypeCode("STORE-3").MediaTypeId;//lấy hình kiểu Product-Highlight-Banner
            IList<ProductInPriority> _lstProductInPriority = new List<ProductInPriority>();
            List<ProductInMedia> lstProductInMedia_Result = new List<ProductInMedia>();
            List<ProductObject> _lstpObj = new List<ProductObject>();

            try
            {
                switch (_type.ToUpper())
                {
                    case "SELLING":
                        _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(1);
                        break;
                    case "NEW":
                        _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(3);
                        break;
                    case "HIGHTLIGHTS":
                        _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(2);
                        break;
                    case "NOMINATION":
                        _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(4);
                        break;
                    default:
                        Error.Add("Error: Types error.");
                        break;
                }
                //Xử lí add ProductInMedia
                foreach (var pip in _lstProductInPriority)
                {
                    foreach (var pim in _iProductInMediaService.GetByProduct((long)pip.ProductId))
                    {
                        Media media = _iMediaService.GetById((long)pim.MediaId);
                        if (media != null && media.MediaTypeId == MediaTypeId //MediaTypeId lúc này là "STORE-3" đã lấy id ở trên
                            && media.IsDeleted == false && media.IsActive == true)
                        {
                            lstProductInMedia_Result.Add(pim);
                        }
                    }
                }
                var lst = Private.ConvertListProduct(lstProductInMedia_Result, Url);

                //sắp xếp
                switch (_orderby)
                { 
                    case 1:
                        lst = Utils.Sort(lst, n => n.ProductId, _sort);
                        break;
                    case 2:
                        lst = Utils.Sort(lst, n => n.ProductName, _sort);
                        break;
                    case 3:
                        lst = Utils.Sort(lst, n => n.StoreId, _sort);
                        break;
                    case 4:
                        lst = Utils.Sort(lst, n => n.StoreName, _sort);
                        break;
                    case 5:
                        lst = Utils.Sort(lst, n => n.MobileOnlinePrice, _sort);
                        break;
                    default:
                        lst = Utils.Sort(lst, n => n.ProductId, _sort);
                        break;
                }              

                _lstpObj = _step == -1 ? lst.Take(20).ToList() : lst.Skip(Convert.ToInt32(_step) * 10).Take(10).ToList();

                if (Error.Count == 0)
                {
                    return Json(new { IsSuccess = true, ListProductObject = _lstpObj }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Error.Add("Error: " + ex.Message);
            }
            return Json(new { IsSuccess = false, error = Error }, JsonRequestBehavior.AllowGet);
        }
        // 6 . Load Store/All ajax {_numColumn, _sortBy, _order, _step}   //1 - 10
        public ActionResult GetStore(int? _step, int? _sortBy, string _order, int? _curCount)
        {
            List<string> Error = new List<string>();
            try
            {
                _curCount = _curCount == null ? 6 : _curCount;
                _step = _step == null ? -1 : _step;
                _order = _order != null  ? _order : "";
                _sortBy = _sortBy != null ? _sortBy : 0;

                StoreRepository _iStoreService = new StoreRepository();
                List<StoreObject> lstStoreObj = new List<StoreObject>();
                StoreInMediaRepository _iStoreInMediaService = new StoreInMediaRepository();
                var lstStore = _iStoreInMediaService.GetAllStore();
                MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
                long MediaTypeId = _iMediaTypeService.GetByTypeCode("MALL-2").MediaTypeId;//lấy hình kiểu 
                MediaRepository _iMediaService = new MediaRepository();
                //Xử lí add ProductInMedia
                foreach (var item in lstStore)
                {
                    StoreObject sobj = new StoreObject();
                    sobj.StoreId = item.Store.StoreId;
                    sobj.StoreName = item.Store.StoreName;
                    sobj.StoreCode = item.Store.StoreCode;
                    sobj.Alias = item.Store.Alias;
                    sobj.MediaName = item.Media.MediaName;
                    sobj.Url = item.Media.Url;
                    sobj.DateVerified = (DateTime) item.Store.DateVerified;
                    sobj.Link = Url.Action("Index", "Store", new { id = sobj.StoreId, urlName = sobj.Alias });

                    lstStoreObj.Add(sobj);
                }

                //sắp xếp
                switch (_sortBy)
                {
                    case 0:
                        // Result = Utils.Sort(Result, n => n.StoreId, _order);
                        break;
                    case 1:
                        lstStoreObj = Utils.Sort(lstStoreObj, n => n.StoreName, _order);
                        break;
                    case 2:
                        lstStoreObj = Utils.Sort(lstStoreObj, n => n.DateVerified, _order);
                        break;
                }

                var Result = _step == -1 ? lstStoreObj.Take((int)_curCount).ToList() :
                    lstStoreObj.Skip(6).Skip(Convert.ToInt32(_step) * 4).Take(4).ToList();

                if (Error.Count == 0)
                {
                    return Json(new { IsSuccess = true, count = Result.Count,ListStoreObject = Result }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Error.Add("Error: " + ex.Message);
            }
            return Json(new { IsSuccess = false, error = Error }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetShippingFee(long? ProvinceId, string lstProduct)//xử lí ajax
        {
            List<string> error = new List<string>();
            if (ProvinceId == null || lstProduct == "")
            {
                error.Add(" Province is null.");
                Json(new { success = false, shippingfee = "0 đ", error = error }, JsonRequestBehavior.AllowGet);
            }
            if (Request.IsAjaxRequest())
            {
                ProductRepository _ProductRepository = new ProductRepository();
                try
                {
                    string a = lstProduct;
                    dynamic data_lstProduct = Newtonsoft.Json.Linq.JObject.Parse(a);
                    List<Tuple<long, int>> lstProductId = new List<Tuple<long, int>>();
                    decimal totalPrice = 0;
                    foreach (var item in data_lstProduct.lstProduct)
                    {
                        long tmp_ProductId = 0;
                        int tmp_Quantity = 0;
                        int Quantity = int.TryParse(item.Quantity.ToString(), out tmp_Quantity) ? Convert.ToInt32(item.Quantity.ToString()) : 1;
                        if (long.TryParse(item.ProductId.ToString(), out tmp_ProductId))
                        {
                            var productItem = (Product)_ProductRepository.GetById(Convert.ToInt64(item.ProductId.ToString()));
                            double? price = productItem.PromotePrice != 0 ? productItem.PromotePrice : productItem.MobileOnlinePrice;
                            totalPrice = Convert.ToDecimal(price * Quantity);
                            //productItem.
                            lstProductId.Add(Tuple.Create(tmp_ProductId, Quantity));
                        }
                    }
                    ShipRepository _ShipRepository = new ShipRepository();
                    var ship = ProvinceId == null ? null : _ShipRepository.GetByTarget(Convert.ToInt64(ProvinceId), "2");
                    if (ship == null)
                        return Json(new
                        {
                            success = false,
                            shippingfee = string.Format("{0:0,0 đ}", 0),
                            error = "ship error"
                        }, JsonRequestBehavior.AllowGet);
                    Private.GetMutilLanguage(ViewBag, Session);
                    if (totalPrice >= ship.FreeShip)
                        return Json(new
                   {
                       success = true,
                       shippingfee = 0,
                       shippingfee_write = string.Format("{0:0,0 đ}", 0),
                       ship = ship.Price,
                       ship_write = string.Format("{0:0,0 đ}", 0),
                       shipfree = ship.FreeShip,
                       shipfree_write = string.Format("{0:0,0 đ}", ship.FreeShip),
                       message = "Giá trị đơn hàng: " + string.Format("{0:0,0 đ}", totalPrice) + "</br>Miễn phí giao hàng",
                       total_ship = ship.Price,
                       total_ship_write = string.Format("{0:0,0 đ}", 0),
                       totalPrice = totalPrice,
                       error = error
                   }, JsonRequestBehavior.AllowGet);
                    WeightRepository _WeightRepository = new WeightRepository();
                    decimal shippingfee = _WeightRepository.GetShippingFee((long)ProvinceId, lstProductId);
                    //Phí giao hàng: 10000 đ (Phí giao hàng: ,Phí cồng kenh : )
                    //ViewBag.multiRes.GetString("addtocart", ViewBag.CultureInfo)
                    //"Phí giao hàng:" + (shippingfee+ ship.Price)
                    var ship_write = (ship.Price == null || ship.Price == 0) ? "Miễn phí" : string.Format("{0:0,0 đ}", ship.Price);
                    return Json(new
               {
                   success = true,
                   shippingfee = shippingfee,
                   shippingfee_write = string.Format("{0:0,0 đ}", shippingfee),
                   ship = ship.Price,
                   ship_write = string.Format("{0:0,0 đ}", ship.Price),
                   shipfree = ship.FreeShip,
                   shipfree_write = string.Format("{0:0,0 đ}", ship.FreeShip),
                   message = "Giá trị đơn hàng: " + string.Format("{0:0,0 đ}", totalPrice) + "</br> Phí vận chuyển: " + string.Format("{0:0,0 đ}", (ship.Price + shippingfee)) + " [ Phí giao hàng: " + ship_write + ", Phí cồng kềnh: " + string.Format("{0:0,0 đ}", shippingfee) + " ]",
                   totalPrice = totalPrice,
                   error = error
               }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    error.Add("Error: " + ex.Message);
                }
                return Json(new { success = false, shippingfee = string.Format("{0:0,0 đ}", 0), error = error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, shippingfee = string.Format("{0:0,0 đ}", 0), error = error }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
