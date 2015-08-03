using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.lps;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.DataContext.ops;
using HTTelecom.Domain.Core.DataContext.sts;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.lps;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.Repository.ops;
using HTTelecom.Domain.Core.Repository.sts;
using HTTelecom.Domain.Core.Repository.tts;
using HTTelecom.WebUI.eCommerce.Common;
using HTTelecom.WebUI.eCommerce.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class VendorController : Controller
    {
        [SessionVendorFilter,HttpGet]
        public ActionResult Index()
        {
            #region Load
            VendorRepository _VendorRepository = new VendorRepository();
            StoreRepository _iStoreService = new StoreRepository();
            var v_s = (HTTelecom.Domain.Core.DataContext.cis.Vendor)Session["sessionVendorGala"];
            var ven = _VendorRepository.GetById(v_s.VendorId);
            ContractRepository _ContractRepository = new ContractRepository();
            StoreStatisticRepository _iStoreRepository = new StoreStatisticRepository();
            BrandInVendorRepository _iBrandInVendorService = new BrandInVendorRepository();
            BrandRepository _iBrandRepository = new BrandRepository();
            OrderRepository _iOrderSevice = new OrderRepository();
            ProductRepository _iProductService = new ProductRepository();
            OrderDetailRepository _iOrderDetailService = new OrderDetailRepository();
            VendorRepository _iVendorService = new VendorRepository();
            ProductItemRepository _iProductItemService = new ProductItemRepository();
            ProductItemInSizeRepository _iProductItemInSizeService = new ProductItemInSizeRepository();
            SizeRepository _iSizeService = new SizeRepository();
            ViewBag.Contract = ven.ContractId == null ? null : _ContractRepository.GetById(Convert.ToInt64(ven.ContractId));
            #endregion

            #region Statistic ProductAll
            //ProductRepository _ProductRepository = new ProductRepository();
            //var ListProduct = _ProductRepository.GetByStore(49);
            //var lstData = new List<object>();
            //var data = new[] { 1, 2 };
            //foreach (var item in ListProduct)
            //{
            //    lstData.Add(new[] { 1, 2 });
            //}
            #endregion

            #region order
            List<OrderDetail> lstOrderDetails = new List<OrderDetail>();
            List<Order> lstOrder = new List<Order>();
            //một vendor chỉ có 1 store
            //1 store thì có nhiều sản phẩm
            var tmp_store = _iStoreService.GetByVendorId(v_s.VendorId);
            List<Product> lst_product_of_vendor = new List<Product>();
            decimal total_allOrder = 0;
            if (tmp_store != null)
            {
                lst_product_of_vendor = _iProductService.GetByStore(tmp_store.StoreId);//một sản phẩm chỉ thuộc một store, 1 store chỉ thuộc 1 Vendor       
                if (lst_product_of_vendor.Count > 0)
                {
                    foreach (var item in (List<Product>)lst_product_of_vendor)
                    {
                        //lấy ra tất cả order details có liên quan đến sản phẩm item
                        lstOrderDetails.AddRange(_iOrderDetailService.GetAll(false).Where(_ => _.ProductId == item.ProductId).ToList());
                    }

                    foreach (var item in lstOrderDetails)//duyệt orderdetails để lấy những order có liên quan
                    {
                        if (lstOrder.Where(_ => _.OrderId == item.OrderId).ToList().Count > 0)//loại bỏ những order details đã Add
                        {
                            lstOrder.Add(_iOrderSevice.GetById(item.OrderId));
                        }
                    }
                    total_allOrder = lstOrder.Where(x => x.IsPaymentConfirmed == true && x.IsDeliveryConfirmed == null).Sum(_ => _.TotalPaid).Value;
                }
            }
            ViewBag.TotalAllOrderSold = total_allOrder;

            #endregion

            #region Product view [hiển thị biểu đồ các sản phẩm bán chạy nhất] - hiển thị 10 sp đầu tiên

            //danh sách này thuộc những Order nằm trong trạng thái [IsPaymentConfirmed == true && IsDeliveryConfirmed == null]
            var lst_productSelling = from l in lstOrderDetails.Where(_ => _iOrderSevice.GetById(_.OrderId).IsPaymentConfirmed == true && _iOrderSevice.GetById(_.OrderId).IsDeliveryConfirmed == null).ToList()
                                     group l by new { l.ProductId } into lp
                                     select new { lp.Key.ProductId, counterP = lp.Count() };
            lst_productSelling = lst_productSelling.OrderByDescending(_ => _.counterP);
            List<object> lst_obj = new List<object>();
            string json_p = "[";
            int count = 5;// top 5 san phẩm bán chạy nhất
            foreach (var item in lst_productSelling.OrderByDescending(_ => _.counterP))
            {
                if (count == 0)
                    break;
                lst_obj.Add(new { ProductName = item.ProductId, Counter = item.counterP });
                json_p += "[\"" + _iProductService.GetById(item.ProductId).ProductName + "\"," + item.counterP + "],";
                count--;
            }
            json_p += "]";
            json_p = json_p.Replace("],]", "]]");
            ViewBag.DataProduct = json_p;
            #endregion

            #region Brand
            List<Brand> lstBrand = new List<Brand>();

            foreach (var item in _iBrandInVendorService.GetAll().Where(_ => _.VendorId == v_s.VendorId).ToList())
            {
                lstBrand.Add(_iBrandRepository.GetById((long)item.BrandId));
            }

            ViewBag.lstBrand = lstBrand;
            #endregion

            #region  Category

            #endregion

            #region Store
            var lstStore = _iStoreService.GetAll().Where(_ => _.VendorId == v_s.VendorId).ToList();
            List<StoreStatistic> lst_tmp_Store = new List<StoreStatistic>();
            foreach (var item in lstStore)
            {
                item.VisitCount = _iStoreRepository.GetList_StoreStatisticAll().Where(_ => _.StoreId == item.StoreId).Sum(c => c.Counter);
            }
            ViewBag.lstStore = lstStore;
            StoreInMediaRepository _StoreInMediaRepository = new StoreInMediaRepository();
            ViewBag.Image = lstStore.Count > 0 ?
                _StoreInMediaRepository.GetByStoreId(lstStore[0].StoreId)
                .Where(n => n.Media.MediaType.MediaTypeCode == "MALL-2").FirstOrDefault() : null;
            #endregion

            #region hiển thị số lượng tồn của sản phẩm trong kho

            List<object> lst_object = new List<object>();
            foreach (var item in lst_product_of_vendor)//duyệt tất cả sản phẩm thuộc Store
            {
                var tmp = _iProductItemService.GetByProductCode(item.ProductStockCode);
                if (tmp != null)// Trường hợp 1: sản phẩm này đã nhập kho (có ở ProductItem)
                {
                    var tmp_pis = _iProductItemInSizeService.GetByProductItem(tmp.ProductItemId);
                    foreach (var pis in tmp_pis)
                    {
                        lst_object.Add(new
                        {
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            ProductCode = item.ProductStockCode,
                            Quantity = pis.Quantity,
                            SizeId = pis.SizeId,
                            SizeName = _iSizeService.GetById((long)pis.SizeId).Code
                        });
                    }
                  
                }
                else// Trường hợp 2: sản phẩm này chưa nhập kho (ko có ở ProductItem)
                {
                    lst_object.Add(new
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        ProductCode = item.ProductStockCode,
                        Quantity = 0,
                        SizeId = 0,
                        SizeName = ""
                    });
                }
            }
            ViewBag.ProductInventoryQuantity = lst_object.OrderBy(_ => _.GetType().GetProperty("Quantity").GetValue(_, null));
            #endregion

            #region Hiển thị những sản phẩm trả tiền nhưng chưa giao hàng
            var lst_product_paid = lst_product_of_vendor.Where(_ =>
                (
                    _iOrderDetailService.GetAll(false).Where(x =>
                        (
                            x.ProductId == _.ProductId &&
                            _iOrderSevice.GetById(x.OrderId).IsPaymentConfirmed != null &&
                            _iOrderSevice.GetById(x.OrderId).IsPaymentConfirmed == true &&
                            _iOrderSevice.GetById(x.OrderId).IsDeliveryConfirmed == null
                        )//DK1: product này phải nằm trong Orderdetails và Orderdetails [IsPaymentConfirmed == true && IsDeliveryConfirmed == null]
                        ||
                        (
                            x.ProductId == _.ProductId &&
                            CheckIsOrderFinance(x.OrderId)
                        )//DK2: Order chưa thanh toán và đã đến bộ phận [AF || LG]
                    ).ToList().Count > 0
                )


                    ).ToList();
            ViewBag.ListProductPaid = lst_product_paid;
            #endregion

            Private.LoadBegin(Session, ViewBag);
            return View(ven);
        }

        private bool CheckIsOrderFinance(long OrderId)
        {
            try
            {
                TaskDirectionRepository _iTaskDirectionService = new TaskDirectionRepository();
                MainRecordRepository _iMainRecordService = new MainRecordRepository();
                OrderRepository _iOrderService = new OrderRepository();

                var MainRecord = _iMainRecordService.GetByFormId(_iOrderService.GetById((long)OrderId).OrderCode);
                var _td = _iTaskDirectionService.GetById(MainRecord.TaskDirectionId);
                if ((_td.TaskFormCode == "COF-2" || _td.TaskFormCode == "COF-3")
                    && ((_td.SystemCode == "AF" && _td.OrderQueue == 3)
                    || (_td.SystemCode == "LG" && _td.IsValid != null && _td.IsValid != true)))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public ActionResult JsonStatisticInfoVendor(long? VendorId)
        {
            List<string> Error = new List<string>();
            if (VendorId == null)
            {
                Error.Add("Vendor does not Exists.");
                return Json(new { success = false, Error = Error }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                #region load
                StoreStatisticRepository _iStoreStatisticService = new StoreStatisticRepository();
                StoreRepository _iStoreService = new StoreRepository();
                ProductStatisticRepository _iProductStatisticService = new ProductStatisticRepository();
                ProductRepository _iProductService = new ProductRepository();
                ProductRepository _ProductRepository = new ProductRepository();
                #endregion
                #region remove
                //var ListProduct = _ProductRepository.GetByStore(49)
                //    .GroupBy(n => n.GroupProductId)
                //    .Select(g => g.First())
                //    .OrderBy(n => n.DateCreated).ToList();
                //var lstData = new List<object>();
                ////var data = new[] { 1, 2 };
                //var rd = new Random();
                //foreach (var item in ListProduct)
                //{
                //    lstData.Add(new[] { Convert.ToDateTime(item.DateCreated).ToDateTimeUTC(), Convert.ToInt64(item.VisitCount) });
                //}
                #endregion
                #region xử lí biểu đồ trang thái
                StoreStatisticRepository _iStoreStisticService = new StoreStatisticRepository();
                //xử lí Store
                var tmp = _iStoreService.GetAll().Where(_ => _.VendorId == (long)VendorId).ToList();
                List<StoreStatistic> list_store = new List<StoreStatistic>();
                var lstData_store = new List<object>();
                var lstData_product = new List<object>();
                if (tmp.Count > 0)
                {
                    DateTime date_to = DateTime.Now.AddDays(1);
                    DateTime date_from = date_to.AddDays(-30);//lấy trạng thái kể từ 30 ngày về trước, [update later....]
                    var lst_StoreSTS = _iStoreStatisticService.GetList_StoreStatisticAll();
                    var lst_ProductSTS = _iProductStatisticService.GetList_ProductStatisticAll();
                    while (date_from != date_to && date_from < date_to)
                    {
                        int ststscountS = lst_StoreSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month && this.CheckStore((long)x.StoreId, tmp[0].StoreId)).ToList().Sum(_ => _.Counter) ?? 0;
                        // int countermemberS = lst_StoreSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month && this.CheckStore((long)x.StoreId, tmp[0].StoreId)).ToList().Sum(_ => _.CounterMember) ?? 0;
                        int ststscountP = 0;
                        foreach (var item in _ProductRepository.GetByStore(tmp[0].StoreId))
                        {
                            ststscountP += lst_ProductSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month && this.CheckProduct((long)x.ProductId, item.ProductId)).ToList().Sum(_ => _.Counter) ?? 0;
                        }

                        lstData_store.Add(new[] { date_from.ToDateTimeUTC(), ststscountS });
                        lstData_product.Add(new[] { date_from.ToDateTimeUTC(), ststscountP });
                        date_from = date_from.AddDays(1);
                    }
                }

                #endregion
                return Json(new { vsProduct = lstData_product, vsStore = lstData_store }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
            }
            return Json(new { success = false, Error = Error }, JsonRequestBehavior.AllowGet);

            //return Json(new {vsProduct= lstData }, JsonRequestBehavior.AllowGet);
        }

        private bool CheckStore(long StoreId, long? StoreIdcheck)
        {
            if (StoreIdcheck == null || StoreId == StoreIdcheck) return true;
            return false;
        }
        private bool CheckProduct(long ProductId, long? ProductIdcheck)
        {
            if (ProductIdcheck == null || ProductId == ProductIdcheck) return true;
            return false;
        }

        [SessionVendorFilter]
        public ActionResult Logout()
        {
            Session.Remove("sessionVendorGala");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ActiveVendorRegister(long cId, string p)//khi Vendor đăng kí xong họ sẽ đi đến đường dẫn kích hoạt tài khoản
        {
            Private.LoadBegin(Session, ViewBag);
            VendorRepository _iVendorService = new VendorRepository();
            Vendor cus = _iVendorService.GetById(cId);
            if (cus.IsActive == true)//Tài khoản đã đăng ký và kích hoạt.
            {
                TempData["gMessageCurrent"] = "activated";
                return View("ReportRegister");
            }

            if (p != null)
            {
                if (p != _iVendorService.GetPassChekingById(cId))
                {
                    TempData["gMessageCurrent"] = "passCheckingWrong";
                    return View("ReportRegister");
                }
            }
            else
            {
                TempData["gMessageCurrent"] = "passCheckingNull";
                return View("ReportRegister");
            }

            _iVendorService.isActiveVendor(cId, true);
            TempData["gMessageCurrent"] = "active";
            return View("ReportRegister");
        }
        public ActionResult ReportRegister()
        {
            ViewBag.u = Url.Action("ReportRegister", "Vendor");
            Private.LoadBegin(Session, ViewBag);
            return View();
        }

    }
}
