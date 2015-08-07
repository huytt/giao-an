using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.DataContext.ops;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ops;
using HTTelecom.Domain.Core.IRepository.tts;
using HTTelecom.Domain.Core.Repository.tts;
using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.ExClass;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HTTelecom.WebUI.Sale.Filters;
using HTTelecom.WebUI.Sale.Common;
using Microsoft.AspNet.SignalR;
namespace HTTelecom.WebUI.Sale.Controllers
{
    [SessionLoginFilter]
    public class TTSController : Controller
    {
        #region clear
        //public ActionResult OrderForm()
        //{
        //    loadOrderForm();
        //    return View();
        //}

        //private void loadOrderForm()
        //{
        //    #region
        //    ProvinceRepository _ProvinceRepository = new ProvinceRepository();
        //    DistrictRepository _DistrictRepository = new DistrictRepository();
        //    BankRepository _BankRepository = new BankRepository();
        //    #endregion
        //    ViewBag.ListProvince = new SelectList(_ProvinceRepository.GetAll().OrderBy(n => n.ProvinceName).ToList(), "ProvinceId", "ProvinceName", null);
        //    ViewBag.ListDistrict = _DistrictRepository.GetAll().OrderBy(n => n.DistrictName).ToList();
        //    ViewBag.ListBank = new SelectList(_BankRepository.GetAll(false, true), "BankId", "BankName", null);
        //}
        //public ActionResult OPSCreate()
        //{
        //    #region 
        //    ProvinceRepository _ProvinceRepository = new ProvinceRepository();
        //    DistrictRepository _DistrictRepository = new DistrictRepository();
        //    BankRepository _BankRepository = new BankRepository();
        //    #endregion
        //    ViewBag.ListProvince = new SelectList(_ProvinceRepository.GetAll().OrderBy(n => n.ProvinceName).ToList(), "ProvinceId", "ProvinceName", null);
        //    ViewBag.ListDistrict = _DistrictRepository.GetAll().OrderBy(n => n.DistrictName).ToList();
        //    ViewBag.ListBank = new SelectList(_BankRepository.GetAll(false,true), "BankId", "BankName", null);
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult OrderForm(Order model, FormCollection formData)
        //{
        //    //ModelState.IsValidField
        //    #region
        //    ProvinceRepository _ProvinceRepository = new ProvinceRepository();
        //    DistrictRepository _DistrictRepository = new DistrictRepository();
        //    BankRepository _BankRepository = new BankRepository();
        //    ProductRepository _ProductRepository = new ProductRepository();
        //    List<OrderDetail> listOrderDetail = new List<OrderDetail>();
        //    OrderRepository _OrderRepository = new OrderRepository();
        //    OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
        //    #endregion
        //    decimal totalPay = 0;
        //    decimal SubTotalFee = 0;
        //    bool error = false;
        //    if (validateCreateOrderForm(model) == true)
        //    {
        //        loadOrderForm();
        //        return View(model);
        //    }
        //    if ( formData["ListProductId"] == null || formData["ListProductId"] == "" || formData["txtListInputQuantity"] == null || formData["txtListInputQuantity"] == "")
        //    {
        //        SetMesage("Input order error!", "");
        //        return View(model);
        //    }
        //    try
        //    {
        //        for (int i = 0; i < formData["ListProductId"].Split(',').ToList().Count; i++)
        //        {
        //            if (_ProductRepository.GetStoreClose(Convert.ToInt64(formData["ListProductId"].Split(',').ToList()[i])) == false)
        //            {
        //                var item = _ProductRepository.GetById(Convert.ToInt64(formData["ListProductId"].Split(',').ToList()[i]));
        //                if (item.IsVerified == true && item.IsDeleted == false)
        //                {
        //                    OrderDetail itemOrderDetail = new OrderDetail();
        //                    itemOrderDetail.DateCreated = DateTime.Now;
        //                    itemOrderDetail.DateModified = DateTime.Now;
        //                    itemOrderDetail.IsDeleted = false;
        //                    itemOrderDetail.OrderQuantity = Convert.ToInt32(formData["txtListInputQuantity"].Split(',').ToList()[i]);
        //                    itemOrderDetail.ProductId = Convert.ToInt64(formData["ListProductId"].Split(',').ToList()[i]);
        //                    itemOrderDetail.UnitPrice = Convert.ToDecimal(item.MobileOnlinePrice);
        //                    itemOrderDetail.UnitPriceDiscount = Convert.ToDecimal(item.PromotePrice);
        //                    //SubTotalFee += itemOrderDetail.UnitPrice * itemOrderDetail.OrderQuantity;
        //                    if (itemOrderDetail.UnitPriceDiscount != null)
        //                    {
        //                        itemOrderDetail.TotalPrice = itemOrderDetail.UnitPriceDiscount * itemOrderDetail.OrderQuantity;
        //                    }
        //                    else
        //                    {
        //                        itemOrderDetail.TotalPrice = itemOrderDetail.UnitPrice * itemOrderDetail.OrderQuantity;
        //                    }
        //                    totalPay += Convert.ToDecimal(itemOrderDetail.TotalPrice);
        //                    listOrderDetail.Add(itemOrderDetail);
        //                }

        //            }
        //        }
        //        Order od = new Order();
        //        Province Province = _ProvinceRepository.GetById(Convert.ToInt64(formData["ProvinceId"]));
        //        model.ShipToCity = Province.Type + " " + Province.ProvinceName;
        //        District District = _DistrictRepository.GetById(Convert.ToInt64(formData["DistrictId"]));
        //        model.ShipToDistrict = District.Type + " " + District.DistrictName;
        //        model.ShippingFee = 0;
        //        model.SubTotalFee = totalPay;
        //        model.TotalPaid = model.SubTotalFee + model.SubTotalFee * (model.TaxFee / 100) + model.ShippingFee;
        //        model.DateCreated = DateTime.Now;
        //        model.IsActive = false;
        //        model.IsClosed = false;
        //        model.IsDeliveryConfirmed = false;
        //        model.IsPaymentConfirmed = false;
        //        //model.IsOrderConfirmed
        //        //model.IsWarning
        //        //model.ModifiedBy
        //        //model.OrderCode
        //        //model.OrderDescription
        //        //model.SecureKey
        //        //model.TransactionCode
        //        //model.TransactionFee
        //        //model.TransactionStatusId
        //        model.DateModified = DateTime.Now;
        //        //model.IsConfirmed = true;
        //        //model.IsConfirmedBy = ((Account)Session["Account_Order"]).AccountId;
        //        model.IsDeleted = false;
        //        model.IsWarning = false;
        //        model.TotalProduct = listOrderDetail.Count;
        //        //model.OrderStatusId = _OrderStatusRepository.GetByStateCode("WPC").OrderStatusId;
        //        if (formData["Type"] == "postPay")
        //        {
        //            model.BankId = null;
        //            model.CardHolderName = null;
        //            model.CardNumber = null;
        //            model.CardTypeId = null;
        //            model.PaymentTypeCode = "PTC-3";
        //        }
        //        else
        //        {
        //            model.PaymentTypeCode = "PTC-2";

        //        }
        //        PaymentTypeRepository _PaymentType = new PaymentTypeRepository();
        //        PaymentType type = _PaymentType.GetByCode(model.PaymentTypeCode);
        //        model.DueTransactionTime = model.DateCreated.Value.Add(TimeSpan.Parse(type.LimitTimeOnTransaction.Value.ToString()));
        //        var idOrder_rs = _OrderRepository.Create(model);
        //        _OrderDetailRepository.CreateByOrderId(listOrderDetail, idOrder_rs);
        //        SetMesage("", "Create complete");
        //        return RedirectToAction("OrderIndex");
        //    }
        //    catch
        //    {
        //        SetMesage("Don't create order. Please try againt", "");
        //        return RedirectToAction("OrderCreate");
        //    }

        //    ViewBag.ListProvince = new SelectList(_ProvinceRepository.GetAll().OrderBy(n => n.ProvinceName).ToList(), "ProvinceId", "ProvinceName", null);
        //    ViewBag.ListDistrict = _DistrictRepository.GetAll().OrderBy(n => n.DistrictName).ToList();
        //    ViewBag.ListBank = new SelectList(_BankRepository.GetAll(false, true), "BankId", "BankName", null);
        //    return View();
        //}
        #endregion

        public static List<MainRecord> gList = new List<MainRecord>();

        #region old
        public ActionResult GetNewOrder(int? page, int? filter, int? tag, string q)
        {
            if (q == null) q = "";
            ViewBag.page = page;
            if (filter == null) filter = 0;
            if (tag == null) tag = 0;
            ViewBag.q = q;
            ViewBag.tag = tag;
            ViewBag.filter = filter;
            Account acc = (Account)HttpContext.Session["Account"];
            #region load
            HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
            HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            #endregion
            var TaskFormCode = new List<string>() { "COF-2", "COF-3" };
            var DepartmentCode = _DepartmentRepository.GetByAccountId(acc.AccountId);
            var lst = new List<MainRecord>();
            var lstPriority = _PriorityRepository.GetAll();
            var lstStatusProccess = _StatusProcessRepository.GetAll();
            var lstStatusDirection = _StatusDirectionRepository.GetAll();
            var lstAccount = _AccountRepository.GetAll();
            ViewBag.lstPriority = lstPriority;
            ViewBag.lstStatusProccess = lstStatusProccess;
            ViewBag.lstStatusDirection = lstStatusDirection;
            ViewBag.lstAccount = lstAccount;
            var date = DateTime.Now;
            if (_AccountRepository.IsAdmin(acc.AccountId, Common.Common._Department))
            {
                ViewBag.isAdmin = true;
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC1", "SDC5" }).Contains(n.StatusDirectionCode)).ToList();
                var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                ViewBag.TaskDirection = _TaskDirectionRepository.GetAll();
                foreach (var item in ods)
                    lst.AddRange(_MainRecordRepository.GetByCustomerServiceAdmin(new List<string>() { item.Item2 }, new List<string>() { "SDC1", "SDC2", "SDC3", "SDC4", "SDC5", "SDC6", "SDC7", "SDC8", "SDC9" }, acc.AccountId, new List<int?>() { item.Item1 - 1 }, item.Item1, new List<int?>() { item.Item1 + 1 }, q, Convert.ToInt32(filter), Convert.ToInt32(tag)).Where(n => (date - n.DateModified.Value).TotalMinutes <= 30).ToList());
            }
            else
            {
                ViewBag.isAdmin = false;
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList();
                var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                ViewBag.ods = ods;
                foreach (var item in ods)
                    lst.AddRange(_MainRecordRepository.GetByCustomerService(new List<string>() { item.Item2 }, new List<string>() { "SDC6", "SDC7", "SDC8", "SDC9" }, "SDC6", acc.AccountId, item.Item1, q, Convert.ToInt32(filter), Convert.ToInt32(tag)).Where(n => (date - n.DateModified.Value).TotalMinutes <= 30).ToList());
            }
            var rs = lst.Select(e => new { e.DateModified, e.MainRecordId, e.FormId }).ToList().OrderByDescending(n => n.DateModified).ToList();
            return Json(new { data = rs }, JsonRequestBehavior.AllowGet);
        }

        #region Action Method (key:AM):
        public ActionResult Sale(int? page, int? filter, int? tag, string q)
        {
            if (q == null) q = "";
            ViewBag.page = page;
            if (filter == null) filter = 0;
            if (tag == null) tag = 0;
            ViewBag.q = q;
            ViewBag.tag = tag;
            ViewBag.filter = filter;
            Account acc = (Account)HttpContext.Session["Account"];
            #region load
            HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
            HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            #endregion
            var TaskFormCode = new List<string>() { "COF-2", "COF-3" };
            var DepartmentCode = _DepartmentRepository.GetByAccountId(acc.AccountId);
            var lst = new List<MainRecord>();
            var lstPriority = _PriorityRepository.GetAll();
            var lstStatusProccess = _StatusProcessRepository.GetAll();
            var lstStatusDirection = _StatusDirectionRepository.GetAll();
            var lstAccount = _AccountRepository.GetAll();
            ViewBag.lstPriority = lstPriority;
            ViewBag.lstStatusProccess = lstStatusProccess;
            ViewBag.lstStatusDirection = lstStatusDirection;
            ViewBag.lstAccount = lstAccount;
            //int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(Common.Common._Department));
            if (_AccountRepository.IsAdmin(acc.AccountId, Common.Common._Department))
            {
                ViewBag.isAdmin = true;
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC1", "SDC5" }).Contains(n.StatusDirectionCode)).ToList();
                var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                ViewBag.TaskDirection = _TaskDirectionRepository.GetAll();
                foreach (var item in ods)
                    lst.AddRange(_MainRecordRepository.GetByCustomerServiceAdmin(new List<string>() { item.Item2 }, new List<string>() { "SDC1", "SDC2", "SDC3", "SDC4", "SDC5", "SDC6", "SDC7", "SDC8", "SDC9" }, acc.AccountId, new List<int?>() { item.Item1 - 1 }, item.Item1, new List<int?>() { item.Item1 + 1 }, q, Convert.ToInt32(filter), Convert.ToInt32(tag)));
                //lst = _MainRecordRepository.GetByCustomerServiceAdmin(TaskFormCode, new List<string>() { "SDC1", "SDC2", "SDC3", "SDC4", "SDC5", "SDC6", "SDC7", "SDC8", "SDC9" }, acc.AccountId, new List<int?>() { OrderQueue - 1 }, OrderQueue, new List<int?>() { OrderQueue + 1 }, q, Convert.ToInt32(filter), Convert.ToInt32(tag));
            }
            else
            {
                ViewBag.isAdmin = false;
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList();
                var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                ViewBag.ods = ods;
                foreach (var item in ods)
                    lst.AddRange(_MainRecordRepository.GetByCustomerService(new List<string>() { item.Item2 }, new List<string>() { "SDC6", "SDC7", "SDC8", "SDC9" }, "SDC6", acc.AccountId, item.Item1, q, Convert.ToInt32(filter), Convert.ToInt32(tag)));
                //lst = _MainRecordRepository.GetByCustomerService(TaskFormCode, new List<string>() {"SDC6", "SDC7", "SDC8", "SDC9" }, "SDC6", acc.AccountId, OrderQueue, q, Convert.ToInt32(filter), Convert.ToInt32(tag));
            }
            List<Common.SubRecordJson> lstSub = new List<Common.SubRecordJson>();
            foreach (var item in lst)
                lstSub.AddRange(CastList_SubRecordFull(_iSubRecordService.GetList_SubRecordByMainRecordId(item.MainRecordId).ToList()));
            ViewBag.SubList = lstSub;
            return View(lst.OrderByDescending(n => n.DateModified).ToList());
        }

        //public ActionResult Sale(int? page, int? filter, int? tag, string q)
        //{
        //    if (q == null) q = "";
        //    ViewBag.page = page;
        //    if (filter == null) filter = 0;
        //    if (tag == null) tag = 0;
        //    ViewBag.q = q;
        //    ViewBag.tag = tag;
        //    ViewBag.filter = filter;
        //    Account acc = (Account)HttpContext.Session["Account"];
        //    #region load
        //    HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
        //    HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
        //    IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
        //    IPriorityRepository _PriorityRepository = new PriorityRepository();
        //    IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
        //    IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
        //    TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
        //    SubRecordRepository _iSubRecordService = new SubRecordRepository();
        //    #endregion
        //    var TaskFormCode = new List<string>() { WebAppConstant.COF_ONLINE_PAY, WebAppConstant.COF_PRE_PAY, WebAppConstant.COF_POST_PAY };
        //    var DepartmentCode = _DepartmentRepository.GetByAccountId(acc.AccountId);
        //    var lst = new List<MainRecord>();
        //    var lstPriority = _PriorityRepository.GetAll();
        //    var lstStatusProccess = _StatusProcessRepository.GetAll();
        //    var lstStatusDirection = _StatusDirectionRepository.GetAll();
        //    var lstAccount = _AccountRepository.GetAll();
        //    ViewBag.lstPriority = lstPriority;
        //    ViewBag.lstStatusProccess = lstStatusProccess;
        //    ViewBag.lstStatusDirection = lstStatusDirection;
        //    ViewBag.lstAccount = lstAccount;
        //    ViewBag.isAdmin = false;
        //    List<StatusDirection> temp = null;

        //    var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(WebAppConstant.DP_SALE_GALAGALA, TaskFormCode);

        //    if (_AccountRepository.IsAdmin(acc.AccountId, WebAppConstant.DP_SALE_GALAGALA))
        //    {
        //        ViewBag.isAdmin = true;
        //        ViewBag.TaskDirection = _TaskDirectionRepository.GetAll();
        //        temp = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { WebAppConstant.SDC_REQUEST, WebAppConstant.SDC_CANCEL }).Contains(n.StatusDirectionCode)).ToList();

        //        foreach (var item in ods)
        //        {
        //            lst.AddRange(_MainRecordRepository.GetBySaleAdmin(
        //                item.Item2,
        //                new List<string>() { WebAppConstant.SDC_REQUEST, WebAppConstant.SDC_ACCEPT, WebAppConstant.SDC_REJECT, WebAppConstant.SDC_HOLD, WebAppConstant.SDC_CANCEL, WebAppConstant.SDC_ASSIGN, WebAppConstant.SDC_SUBMIT, WebAppConstant.SDC_COMPLETE, WebAppConstant.SDC_RETURN },
        //                acc.AccountId,
        //                item.Item1,
        //                q,
        //                Convert.ToInt32(filter),
        //                Convert.ToInt32(tag)));
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.ods = ods;
        //        temp = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { WebAppConstant.SDC_COMPLETE, WebAppConstant.SDC_RETURN }).Contains(n.StatusDirectionCode)).ToList();

        //        foreach (var item in ods)
        //        {
        //            lst.AddRange(_MainRecordRepository.GetBySale(
        //                item.Item2,
        //                new List<string>() { WebAppConstant.SDC_ASSIGN, WebAppConstant.SDC_SUBMIT, WebAppConstant.SDC_COMPLETE, WebAppConstant.SDC_RETURN },
        //                acc.AccountId,
        //                item.Item1,
        //                q));
        //        }
        //    }

        //    ViewBag.ListStatusDirection = temp;

        //    List<Common.SubRecordJson> lstSub = new List<Common.SubRecordJson>();
        //    foreach (var item in lst)
        //    {
        //        lstSub.AddRange(CastList_SubRecordFull(_iSubRecordService.GetList_SubRecordByMainRecordId(item.MainRecordId).ToList()));
        //    }

        //    ViewBag.SubList = lstSub;

        //    return View(lst.OrderByDescending(n => n.DateModified).ToList());
        //}

        public ActionResult SaleEdit(long id)
        {
            #region load
            MainRecord main = new MainRecord();
            this.GetList_TaskFormDropDownList(-1, false);
            this.GetList_PriorityDropDownList(-1, false);
            this.GetList_StatusProcessDropDownList(3, false);
            IStatusDirectionRepository _iStatusDirectionService = new StatusDirectionRepository();
            AccountRepository _iAccountService = new AccountRepository();
            Account acc = (Account)HttpContext.Session["Account"];
            MainRecordRepository _iMainRecordService = new MainRecordRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            #endregion
            ViewBag.list_StatusDirection = _iStatusDirectionService.GetAll();
            main = _iMainRecordService.GetById(id);
            var isAdmin = _iAccountService.IsAdmin(acc.AccountId, Common.Common._Department);
            var TaskDirection = new TaskDirection();
            if (main.TaskDirectionId != null && main.TaskDirectionId != 0)
                TaskDirection = _TaskDirectionRepository.GetById(main.TaskDirectionId);
            //int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(main.TaskFormCode, Common.Common._Department));
            var TaskFormCode = new List<string>() { "COF-2", "COF-3" };
            var lstOrderQueue = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
            if (isAdmin == true)
            {
                var lstTaskDirection = new List<TaskDirection>();
                //foreach (var item in TaskFormCode)
                //    lstTaskDirection.AddRange(_TaskDirectionRepository.GetListPermissionAdmin(item, OrderQueue));
                foreach (var item in lstOrderQueue)
                    lstTaskDirection.AddRange(_TaskDirectionRepository.GetListPermissionAdmin(item.Item2, item.Item1));
                List<long> lst = new List<long>();
                foreach (var item in lstTaskDirection)
                    lst.Add(item.TaskDirectionId);
                if (main.TaskDirectionId != null && main.TaskDirectionId != 0 && lst.Contains(Convert.ToInt64(main.TaskDirectionId)) == false)
                    return RedirectToAction("Sale");
            }
            else
            {
                //Employee
                if (main.TaskDirectionId != null && main.TaskDirectionId != 0 && TaskDirection != null && lstOrderQueue.Where(n => n.Item1 == TaskDirection.OrderQueue).ToList().Count == 0)
                    return RedirectToAction("Sale");
            }
            #region load
            ITaskFormRepository _TaskFormRepository = new TaskFormRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            PriorityRepository _PriorityRepository = new PriorityRepository();
            StatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            #endregion
            var SubRecords = _iSubRecordService.GetList_SubRecordByMainRecordId(id).ToList();
            var SubList = CastList_SubRecordFull(SubRecords.ToList());
            ViewBag.SubList = SubList;
            ViewBag.TaskFormString = _TaskFormRepository.GetByCode(main.TaskFormCode).TaskFormName;
            ViewBag.StatusDirectionName = _StatusDirectionRepository.GetByCode(main.StatusDirectionCode).StatusDirectionName;
            ViewBag.StatusProcessName = _StatusProcessRepository.GetByCode(main.StatusProcessCode).StatusProcessName;
            //ViewBag.PriorityName = _PriorityRepository.GetById(Convert.ToInt64(main.PriorityId)).PriorityName;
            ViewBag.realOnly = false;
            var _Order = _OrderRepository.GetByCode(main.FormId);
            if (_Order == null) { return RedirectToAction("Sale"); }
            var _taskFormCode = _Order.PaymentTypeCode == "PTC-1" ? "COF-1" : _Order.PaymentTypeCode == "PTC-2" ? "COF-2" : "COF-3";
            var OrderQueue = lstOrderQueue.Where(n => n.Item2 == main.TaskFormCode).FirstOrDefault().Item1;
            if (isAdmin == true)
            {
                if (main.HoldByManagerId == null)
                    _iMainRecordService.EditHoldManager(acc.AccountId, main.MainRecordId);
                if (main.TaskDirectionId == null)
                    _iMainRecordService.EditTaskDirection(_TaskDirectionRepository.GetByIsValidAndOrderQuere(_taskFormCode, null, OrderQueue).TaskDirectionId, main.MainRecordId);
                if ((new List<string>() { "SDC7", "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) || (main.StatusDirectionCode == "SDC3" && TaskDirection.OrderQueue > OrderQueue) || (main.StatusDirectionCode == "SDC1" && TaskDirection.OrderQueue < OrderQueue))
                    ViewBag.realOnly = false;
                else
                    ViewBag.realOnly = true;
                ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC1", "SDC5", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
            }
            else
            {
                if (main.StatusDirectionCode.ToUpper() != "SDC6")
                {
                    if ((new List<string>() { "SDC7", "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) == true && (main.HoldByStaffId == null || acc.AccountId == main.HoldByStaffId))
                    { }
                    else
                    {
                        SetMessage("Access deny!", "");
                        return RedirectToAction("Sale");
                    }
                    ViewBag.realOnly = true;
                }
                else
                {
                    if (main.HoldByStaffId == null)
                        _iMainRecordService.EditHoldStaff(acc.AccountId, main.MainRecordId);
                    else
                        if (main.HoldByStaffId != null && main.HoldByStaffId != acc.AccountId)
                        {
                            SetMessage("Access deny!", "");
                            return RedirectToAction("Sale", "TTS");
                        }
                    ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
                }
            }
            var lstAccount = _iAccountService.GetAll();
            if (lstAccount == null)
                lstAccount = new List<Account>();
            ViewBag.listAccount = lstAccount;
            ViewBag.MainRecordId = id;
            return View(main);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SaleEdit(FormCollection formData)
        {
            try
            {
                Account acc = (Account)HttpContext.Session["Account"];
                AccountRepository _iAccountService = new AccountRepository();
                IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
                Common.Common commons = new Common.Common();
                ISubRecordRepository _SubRecordRepository = new SubRecordRepository();
                IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
                TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
                List<SubRecordJson> lst = new List<SubRecordJson>();
                long temp = 0;
                #region Error
                if (formData["MainRecordId"] == null || long.TryParse(formData["MainRecordId"].ToString(), out temp) == false)
                {
                    return RedirectToAction("SaleEdit", Convert.ToInt64(formData["MainRecordId"].ToString()));
                }
                if (formData["StatusDirectionCode"] == null || long.TryParse(formData["StatusDirectionCode"].ToString(), out temp) == false)
                {
                    return RedirectToAction("SaleEdit", Convert.ToInt64(formData["MainRecordId"].ToString()));
                }
                //if (formData["DescriptionSub"] == null || formData["DescriptionSub"].ToString().Length == 0 || formData["DescriptionSub"].ToString().Length > 100)
                //{
                //    return RedirectToAction("SaleEdit", Convert.ToInt64(formData["MainRecordId"].ToString()));
                //}
                #endregion
                var mainRecord = _MainRecordRepository.GetById(Convert.ToInt64(formData["MainRecordId"].ToString()));
                var SubRecord = _SubRecordRepository.GetList_SubRecordByMainRecordId(mainRecord.MainRecordId);
                if (SubRecord.Count == 0)
                {
                    SubRecord subRecord = new SubRecord();
                    subRecord.MainRecordId = mainRecord.MainRecordId;
                    subRecord.IsDeleted = false;
                    subRecord.PreviousSubId = 0;
                    subRecord.ContentField = Common.Common.ContentFielSub;
                    Common.Common common = new Common.Common();
                    Common.SubRecordJson subJson = new Common.SubRecordJson(mainRecord.OriginatorId.ToString(), _StatusDirectionRepository.GetByCode(mainRecord.StatusDirectionCode).StatusDirectionId.ToString(), mainRecord.PriorityId.ToString(), "0", "", DateTime.Now.ToString(), DateTime.Now.ToString(), subRecord.MainRecordId.ToString());
                    subRecord.SubList = "[" + common.SubRecordJsontoString(subJson) + "]";
                    var id = _SubRecordRepository.Create(subRecord);
                    var SubItem = _SubRecordRepository.GetById(id);
                    SubRecord.Add(SubItem);
                }
                //int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(mainRecord.TaskFormCode,Common.Common._Department));
                var TaskFormCode = new List<string>() { "COF-2", "COF-3" };
                var lstOrderQueue = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                foreach (var item in SubRecord)
                    lst.AddRange(commons.ToSubRecordJson((JArray)JsonConvert.DeserializeObject(item.SubList)));
                var SubLast = lst[lst.Count - 1];
                var StatusDirectionCode = _StatusDirectionRepository.GetById(Convert.ToInt64(formData["StatusDirectionCode"].ToString())).StatusDirectionCode;
                mainRecord.DateModified = DateTime.Now;
                var isAdmin = _iAccountService.IsAdmin(acc.AccountId, Common.Common._Department);
                var OrderQueue = lstOrderQueue.Where(n => n.Item2 == mainRecord.TaskFormCode).FirstOrDefault().Item1;
                if (isAdmin)
                {
                    #region admin
                    var lstTaskDirection = new List<TaskDirection>();
                    //foreach (var item in TaskFormCode)
                    //    lstTaskDirection.AddRange(_TaskDirectionRepository.GetListPermissionAdmin(item, OrderQueue));
                    foreach (var item in lstOrderQueue)
                        lstTaskDirection.AddRange(_TaskDirectionRepository.GetListPermissionAdmin(item.Item2, item.Item1));
                    List<long> lstLog = new List<long>();
                    foreach (var item in lstTaskDirection)
                        lstLog.Add(item.TaskDirectionId);
                    if (mainRecord.TaskDirectionId != null && mainRecord.TaskDirectionId != 0 && lstLog.Contains(Convert.ToInt64(mainRecord.TaskDirectionId)) == false)
                        return RedirectToAction("Sale");
                    if ((new List<string>() { "SDC3", "SDC7", "SDC8", "SDC9" }).Contains(mainRecord.StatusDirectionCode) == false && ((new List<string>() { "SDC5", "SDC1", "SDC6" }).Contains(StatusDirectionCode)) == false)
                        return RedirectToAction("Sale");
                    #endregion
                    #region admin
                    if (StatusDirectionCode == "SDC6")
                    {
                        mainRecord.TaskDirectionId = _TaskDirectionRepository.GetByIsValidAndOrderQuere(mainRecord.TaskFormCode, null, OrderQueue).TaskDirectionId;
                        mainRecord.HoldByStaffId = null;
                    }
                    else
                    {
                        TaskDirection taskDirection = _TaskDirectionRepository.GetBy(mainRecord.TaskFormCode, StatusDirectionCode, OrderQueue);
                        if (taskDirection == null)
                            return RedirectToAction("Sale");
                        mainRecord.TaskDirectionId = taskDirection.TaskDirectionId;
                    }
                    if (StatusDirectionCode == "SDC5")
                    {
                        OrderRepository _OrderRepository = new OrderRepository();
                        _OrderRepository.UpdateStatus(mainRecord.FormId, "TRANS-TC3", false);
                    }
                    mainRecord.HoldByManagerId = acc.AccountId;//Admin
                    mainRecord.HoldByStaffId = null;
                    mainRecord.StatusDirectionCode = StatusDirectionCode;
                    _MainRecordRepository.Edit(mainRecord);
                    #endregion
                }
                else
                {
                    #region user
                    var TaskDirection = new TaskDirection();
                    if (mainRecord.TaskDirectionId != null && mainRecord.TaskDirectionId != 0)
                        TaskDirection = _TaskDirectionRepository.GetById(mainRecord.TaskDirectionId);
                    if (mainRecord.TaskDirectionId != null && mainRecord.TaskDirectionId != 0 && TaskDirection != null && TaskDirection.OrderQueue != OrderQueue)
                        return RedirectToAction("Sale");
                    if ((new List<string>() { "SDC6" }).Contains(mainRecord.StatusDirectionCode) == false)
                        return RedirectToAction("Sale");
                    #endregion

                    #region user
                    mainRecord.StatusDirectionCode = StatusDirectionCode;
                    mainRecord.HoldByStaffId = acc.AccountId;
                    _MainRecordRepository.Edit(mainRecord);
                    #endregion
                }
                #region edit SubRecord
                SubRecordJson jsonSub = new SubRecordJson(acc.AccountId.ToString(), formData["StatusDirectionCode"].ToString(), mainRecord.PriorityId.ToString(), lst.Count.ToString(), formData["DescriptionSub"].ToString(), DateTime.Now.ToString(), SubLast.DateHandIn, mainRecord.MainRecordId.ToString());
                lst.Add(jsonSub);
                string json = commons.ListSubRecordJsontoString(lst);
                _SubRecordRepository.EditSubLst(SubRecord[SubRecord.Count - 1].SubRecordId, json);
                #endregion
                return RedirectToAction("Sale");
            }
            catch
            {
                return RedirectToAction("SaleEdit", Convert.ToInt64(formData["MainRecordId"].ToString()));
            }
        }


        #endregion
        #endregion


        public ActionResult GetNotification(int? page, int? filter, int? tag, string q)
        {
            #region filter
            if (q == null) q = "";
            ViewBag.page = page;
            if (filter == null) filter = 0;
            if (tag == null) tag = 0;
            ViewBag.q = q;
            ViewBag.tag = tag;
            ViewBag.filter = filter;
            #endregion
            #region load
            HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
            HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            #endregion
            Account acc = (Account)HttpContext.Session["Account"];
            var lst = new List<MainRecord>();
            var List_Allow_Edit = new List<Tuple<string, string, string, bool?>>();
            if (acc.GroupAccounts.Where(n => n.GroupId == WebAppConstant.PERMISSION_GROUP_SALE_CALL).ToList().Count > 0)
            {
                //Sale Call
                List_Allow_Edit = new List<Tuple<string, string, string, bool?>>() 
            { 
            new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_HOLD,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_HOLD,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_HOLD,null)
            };
            }

            if (acc.GroupAccounts.Where(n => n.GroupId == WebAppConstant.PERMISSION_GROUP_SALE_EXCEPTION).ToList().Count > 0)
            {

                //Sale Exception
                List_Allow_Edit = new List<Tuple<string, string, string, bool?>>() 
            { 
            new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            };
            }

            if (acc.GroupAccounts.Where(n => n.GroupId == WebAppConstant.PERMISSION_GROUP_SALE_MANAGER).ToList().Count > 0)
            {
                //Sale Exception
                List_Allow_Edit = new List<Tuple<string, string, string, Nullable<bool>>>() 
            { 
            new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            };

            }

            //Sale Manager


            var ListDirection_ALLOW = _TaskDirectionRepository.GetListPermissionEdit(List_Allow_Edit);
            lst = _MainRecordRepository.GetBySaleException(ListDirection_ALLOW, acc.AccountId);
            var date = DateTime.Now;



            var rs = lst.Select(e => new { e.DateModified, e.MainRecordId, e.FormId }).ToList().OrderByDescending(n => n.DateModified).ToList();
            return Json(new { data = rs }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// JQ-COF-2-D500
        /// </summary>
        /// <returns></returns>
        #region SaleCall
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_CALL)]
        public ActionResult SaleCall(int? page, int? filter, int? tag, string q)
        {
            #region filter
            //Index
            if (q == null) q = "";
            ViewBag.page = page;
            if (filter == null) filter = 0;
            if (tag == null) tag = 0;
            ViewBag.q = q;
            ViewBag.tag = tag;
            ViewBag.filter = filter;
            #endregion
            Account acc = (Account)HttpContext.Session["Account"];
            #region load
            HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
            HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            #endregion
            var TaskFormCode = new List<string>() { WebAppConstant.COF_ONLINE_PAY, WebAppConstant.COF_PRE_PAY, WebAppConstant.COF_POST_PAY };
            var DepartmentCode = _DepartmentRepository.GetByAccountId(acc.AccountId);
            var lst = new List<MainRecord>();
            var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL, TaskFormCode);
            foreach (var item in ods)
            {
                lst.AddRange(_MainRecordRepository.GetBySale(
                        item.Item2,
                        acc.AccountId,
                        item.Item1,
                        q));
            }
            List<Common.SubRecordJson> lstSub = new List<Common.SubRecordJson>();
            foreach (var item in lst)
                lstSub.AddRange(CastList_SubRecordFull(_iSubRecordService.GetList_SubRecordByMainRecordId(item.MainRecordId).ToList()));
            ViewBag.SubList = lstSub;
            return View(lst.OrderByDescending(n => n.DateModified).ToList());
        }
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_CALL)]
        public ActionResult SaleCallEdit(long id)
        {
            #region load
            MainRecord main = new MainRecord();
            IStatusDirectionRepository _iStatusDirectionService = new StatusDirectionRepository();
            AccountRepository _iAccountService = new AccountRepository();
            Account acc = (Account)HttpContext.Session["Account"];
            MainRecordRepository _iMainRecordService = new MainRecordRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            ITaskFormRepository _TaskFormRepository = new TaskFormRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            PriorityRepository _PriorityRepository = new PriorityRepository();
            StatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            #endregion
            main = _iMainRecordService.GetById(id);
            #region List Task Edit
            var List_Allow_Edit = new List<Tuple<string, string, string, bool?>>() { 
            new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_HOLD,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_HOLD,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_HOLD,null)
            //,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            //,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            //,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            };
            var ListDirection_ALLOW = _TaskDirectionRepository.GetListPermissionEdit(List_Allow_Edit);
            #endregion
            #region check eror
            if (ListDirection_ALLOW.Where(n => n.TaskDirectionId == main.TaskDirectionId).ToList().Count == 0)
                return RedirectToAction("SaleCall");
            #endregion
            if (main.HoldByStaffId == null)
                _iMainRecordService.EditHoldStaff(acc.AccountId, main.MainRecordId);
            else
                if (main.HoldByStaffId != acc.AccountId)
                {
                    SetMessage("Access deny!", "");
                    return RedirectToAction("SaleCall", "TTS");
                }
            #region load Data
            var _Order = _OrderRepository.GetByCode(main.FormId);
            if (_Order == null) { return RedirectToAction("SaleCallEdit"); }
            var SubRecords = _iSubRecordService.GetList_SubRecordByMainRecordId(id).ToList();
            var SubList = CastList_SubRecordFull(SubRecords.ToList());
            ViewBag.SubList = SubList;
            ViewBag.TaskFormString = _TaskFormRepository.GetByCode(main.TaskFormCode).TaskFormName;
            ViewBag.StatusDirectionName = _StatusDirectionRepository.GetByCode(main.StatusDirectionCode).StatusDirectionName;
            ViewBag.StatusProcessName = _StatusProcessRepository.GetByCode(main.StatusProcessCode).StatusProcessName;
            #endregion
            #region Load Content
            var lstAccount = _iAccountService.GetAll();
            if (lstAccount == null)
                lstAccount = new List<Account>();
            ViewBag.listAccount = lstAccount;
            ViewBag.MainRecordId = id;
            #endregion
            return View(main);
        }
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_CALL), HttpPost]
        public ActionResult SaleCallEdit(FormCollection data)
        {
            Account acc = (Account)HttpContext.Session["Account"];
            var statusDirectionCode = data["StatusDirectionCode"].ToString();
            var mainRecordId = Convert.ToInt64(data["MainRecordId"].ToString());
            var DescriptionSub = data["DescriptionSub"].ToString();

            #region Check Permission
            //Chua check MainRecord có thuộc bộ edit ko


            #endregion


            if (statusDirectionCode.Equals(WebAppConstant.SDC_REJECT) || statusDirectionCode.Equals(WebAppConstant.SDC_REQUEST))
            {
                ISubRecordRepository _SubRecordRepository = new SubRecordRepository();
                IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
                IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
                TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();

                var mainRecord = _MainRecordRepository.GetById(mainRecordId);
                var SubRecord = _SubRecordRepository.GetList_SubRecordByMainRecordId(mainRecord.MainRecordId);

                #region Create Sub if not exist
                if (SubRecord.Count == 0)
                {
                    SubRecord subRecord = new SubRecord();
                    subRecord.MainRecordId = mainRecord.MainRecordId;
                    subRecord.IsDeleted = false;
                    subRecord.PreviousSubId = 0;
                    subRecord.ContentField = Common.Common.ContentFielSub;
                    Common.Common common = new Common.Common();
                    Common.SubRecordJson subJson = new Common.SubRecordJson(mainRecord.OriginatorId.ToString(), _StatusDirectionRepository.GetByCode(mainRecord.StatusDirectionCode).StatusDirectionId.ToString(), mainRecord.PriorityId.ToString(), "0", "", DateTime.Now.ToString(), DateTime.Now.ToString(), subRecord.MainRecordId.ToString());
                    subRecord.SubList = "[" + common.SubRecordJsontoString(subJson) + "]";
                    var id = _SubRecordRepository.Create(subRecord);
                    var SubItem = _SubRecordRepository.GetById(id);
                    SubRecord.Add(SubItem);
                }
                #endregion

                #region addSub
                Common.Common commons = new Common.Common();
                List<SubRecordJson> lst = new List<SubRecordJson>();
                foreach (var item in SubRecord)
                    lst.AddRange(commons.ToSubRecordJson((JArray)JsonConvert.DeserializeObject(item.SubList)));
                var SubLast = lst[lst.Count - 1];
                SubRecordJson jsonSub = new SubRecordJson(acc.AccountId.ToString(), statusDirectionCode, mainRecord.PriorityId.ToString(), lst.Count.ToString(), data["DescriptionSub"].ToString(), DateTime.Now.ToString(), SubLast.DateHandIn, mainRecord.MainRecordId.ToString());
                lst.Add(jsonSub);
                string json = commons.ListSubRecordJsontoString(lst);
                _SubRecordRepository.EditSubLst(SubRecord[SubRecord.Count - 1].SubRecordId, json);
                #endregion

                #region Edit MainRecord
                mainRecord.StatusDirectionCode = statusDirectionCode;
                mainRecord.DateModified = DateTime.Now;
                mainRecord.TaskDirectionId = _TaskDirectionRepository.GetBy(
                    mainRecord.TaskFormCode,
                    WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,
                    statusDirectionCode).TaskDirectionId;
                mainRecord.HoldByStaffId = null;
                _MainRecordRepository.Edit(mainRecord);
                #endregion

            }
            return RedirectToAction("SaleCall", "TTS");
        }
        #endregion
        /// <summary>
        /// JQ-COF-3-D500
        /// </summary>
        /// <returns></returns>
        #region SaleExption
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_EXCEPTION)]
        public ActionResult SaleExption(int? page, int? filter, int? tag, string q, string type)
        {
            #region filter
            if (q == null) q = "";
            ViewBag.page = page;
            if (filter == null) filter = 0;
            if (tag == null) tag = 0;
            ViewBag.q = q;
            ViewBag.tag = tag;
            ViewBag.filter = filter;
            #endregion
            #region load
            HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
            HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            #endregion
            Account acc = (Account)HttpContext.Session["Account"];
            var lst = new List<MainRecord>();

            var List_Allow_Edit = new List<Tuple<string, string, string, bool?>>() 
            { 
            new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            };
            var ListDirection_ALLOW = _TaskDirectionRepository.GetListPermissionEdit(List_Allow_Edit);
            lst = _MainRecordRepository.GetBySaleException(ListDirection_ALLOW, acc.AccountId);
            var ListTask = _TaskDirectionRepository.GetAll().ToList();
            ViewBag.ListTask = ListTask;
            var lstTask = new List<long>();
            switch (type)
            {
                case "SALE_CALL":
                    lstTask = ListTask.Where(n => n.SystemCode == WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL).ToList().Select(n => n.TaskDirectionId).ToList();
                    lst = lst.Where(n => n.StatusDirectionCode == WebAppConstant.SDC_REJECT && lstTask.Contains(n.TaskDirectionId)).ToList();
                    break;
                case "SALE_MANAGER":
                    lstTask = ListTask.Where(n => n.SystemCode == WebAppConstant.GROUP_SALE_GALAGALA_MANAGER).ToList().Select(n => n.TaskDirectionId).ToList();
                    lst = lst.Where(n => n.StatusDirectionCode == WebAppConstant.SDC_REJECT && lstTask.Contains(n.TaskDirectionId)).ToList();
                    break;
                default:
                    break;
            }


            #region Load List Sub
            List<Common.SubRecordJson> lstSub = new List<Common.SubRecordJson>();
            foreach (var item in lst)
                lstSub.AddRange(CastList_SubRecordFull(_iSubRecordService.GetList_SubRecordByMainRecordId(item.MainRecordId).ToList()));
            ViewBag.SubList = lstSub;
            #endregion
            #region List Account
            var lstAccount = _AccountRepository.GetAll();
            ViewBag.lstAccount = lstAccount;
            #endregion
            return View(lst.OrderByDescending(n => n.DateModified).ToList());
        }
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_EXCEPTION)]
        public ActionResult SaleExptionEdit(long id)
        {
            #region load
            MainRecord main = new MainRecord();
            IStatusDirectionRepository _iStatusDirectionService = new StatusDirectionRepository();
            AccountRepository _iAccountService = new AccountRepository();
            Account acc = (Account)HttpContext.Session["Account"];
            MainRecordRepository _iMainRecordService = new MainRecordRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            ITaskFormRepository _TaskFormRepository = new TaskFormRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            PriorityRepository _PriorityRepository = new PriorityRepository();
            StatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            #endregion
            main = _iMainRecordService.GetById(id);
            #region List Task Edit
            var List_Allow_Edit = new List<Tuple<string, string, string, bool?>>() { 
            new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,WebAppConstant.SDC_REJECT,false)
            };
            var ListDirection_ALLOW = _TaskDirectionRepository.GetListPermissionEdit(List_Allow_Edit);
            #endregion
            #region check eror
            if (ListDirection_ALLOW.Where(n => n.TaskDirectionId == main.TaskDirectionId).ToList().Count == 0)
                return RedirectToAction("SaleExption");
            #endregion
            if (main.HoldByStaffId == null)
                _iMainRecordService.EditHoldStaff(acc.AccountId, main.MainRecordId);
            else
                if (main.HoldByStaffId != acc.AccountId)
                {
                    SetMessage("Access deny!", "");
                    return RedirectToAction("SaleExption", "TTS");
                }
            #region load Data
            var _Order = _OrderRepository.GetByCode(main.FormId);
            if (_Order == null) { return RedirectToAction("SaleExption"); }
            var SubRecords = _iSubRecordService.GetList_SubRecordByMainRecordId(id).ToList();
            var SubList = CastList_SubRecordFull(SubRecords.ToList());
            ViewBag.SubList = SubList;
            ViewBag.TaskFormString = _TaskFormRepository.GetByCode(main.TaskFormCode).TaskFormName;
            ViewBag.StatusDirectionName = _StatusDirectionRepository.GetByCode(main.StatusDirectionCode).StatusDirectionName;
            ViewBag.StatusProcessName = _StatusProcessRepository.GetByCode(main.StatusProcessCode).StatusProcessName;
            #endregion
            #region Load Content
            var lstAccount = _iAccountService.GetAll();
            if (lstAccount == null)
                lstAccount = new List<Account>();
            ViewBag.listAccount = lstAccount;
            ViewBag.MainRecordId = id;
            #endregion
            return View(main);
        }
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_EXCEPTION), HttpPost]
        public ActionResult SaleExptionEdit(FormCollection data)
        {
            Account acc = (Account)HttpContext.Session["Account"];
            var statusDirectionCode = data["StatusDirectionCode"].ToString();
            var mainRecordId = Convert.ToInt64(data["MainRecordId"].ToString());
            #region Check Permission
            //Chua check MainRecord có thuộc bộ edit ko
            #endregion
            if (statusDirectionCode.Equals(WebAppConstant.SDC_REJECT) || statusDirectionCode.Equals(WebAppConstant.SDC_REQUEST))
            {
                #region  Load
                ISubRecordRepository _SubRecordRepository = new SubRecordRepository();
                IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
                IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
                TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
                #endregion
                var mainRecord = _MainRecordRepository.GetById(mainRecordId);
                var SubRecord = _SubRecordRepository.GetList_SubRecordByMainRecordId(mainRecord.MainRecordId);
                #region Neu chua co SubRecord, create moi
                if (SubRecord.Count == 0)
                {
                    SubRecord subRecord = new SubRecord();
                    subRecord.MainRecordId = mainRecord.MainRecordId;
                    subRecord.IsDeleted = false;
                    subRecord.PreviousSubId = 0;
                    subRecord.ContentField = Common.Common.ContentFielSub;
                    Common.Common common = new Common.Common();
                    Common.SubRecordJson subJson = new Common.SubRecordJson(mainRecord.OriginatorId.ToString(), _StatusDirectionRepository.GetByCode(mainRecord.StatusDirectionCode).StatusDirectionId.ToString(), mainRecord.PriorityId.ToString(), "0", "", DateTime.Now.ToString(), DateTime.Now.ToString(), subRecord.MainRecordId.ToString());
                    subRecord.SubList = "[" + common.SubRecordJsontoString(subJson) + "]";
                    var id = _SubRecordRepository.Create(subRecord);
                    var SubItem = _SubRecordRepository.GetById(id);
                    SubRecord.Add(SubItem);
                }
                #endregion
                #region Save 1 SubRecord
                Common.Common commons = new Common.Common();
                List<SubRecordJson> lst = new List<SubRecordJson>();
                foreach (var item in SubRecord)
                    lst.AddRange(commons.ToSubRecordJson((JArray)JsonConvert.DeserializeObject(item.SubList)));
                var SubLast = lst[lst.Count - 1];
                mainRecord.DateModified = DateTime.Now;
                SubRecordJson jsonSub = new SubRecordJson(acc.AccountId.ToString(), statusDirectionCode, mainRecord.PriorityId.ToString(), lst.Count.ToString(), data["DescriptionSub"].ToString(), DateTime.Now.ToString(), SubLast.DateHandIn, mainRecord.MainRecordId.ToString());
                lst.Add(jsonSub);
                string json = commons.ListSubRecordJsontoString(lst);
                _SubRecordRepository.EditSubLst(SubRecord[SubRecord.Count - 1].SubRecordId, json);
                #endregion
                #region Edit MainRecord
                mainRecord.StatusDirectionCode = statusDirectionCode;
                mainRecord.TaskDirectionId = _TaskDirectionRepository.GetBy(
                    mainRecord.TaskFormCode,
                    WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,
                    statusDirectionCode).TaskDirectionId;
                mainRecord.HoldByStaffId = null;
                _MainRecordRepository.Edit(mainRecord);
                #endregion
            }
            return RedirectToAction("SaleExption", "TTS");
        }
        #endregion
        /// <summary>
        ///  JQ-COF-1-D500
        /// </summary>
        /// <returns></returns>
        #region SaleManager
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_MANAGER)]
        public ActionResult SaleManager(int? page, int? filter, int? tag, string q, string type)
        {
            #region filter
            if (q == null) q = "";
            ViewBag.page = page;
            if (filter == null) filter = 0;
            if (tag == null) tag = 0;
            ViewBag.q = q;
            ViewBag.tag = tag;
            ViewBag.filter = filter;
            #endregion
            #region load
            HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
            HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            #endregion
            Account acc = (Account)HttpContext.Session["Account"];
            var lst = new List<MainRecord>();
            var List_Allow_Edit = new List<Tuple<string, string, string, Nullable<bool>>>() 
            { 
            new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            };
            var ListDirection_ALLOW = _TaskDirectionRepository.GetListPermissionEdit(List_Allow_Edit);
            lst = _MainRecordRepository.GetBySaleException(ListDirection_ALLOW, acc.AccountId);
            var ListTask = _TaskDirectionRepository.GetAll().ToList();
            ViewBag.ListTask = ListTask;
            var lstTask = new List<long>();
            switch (type)
            {
                case "NEW":
                    lstTask = ListTask.Where(n => n.SystemCode == WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL).ToList().Select(n => n.TaskDirectionId).ToList();
                    lst = lst.Where(n => n.StatusDirectionCode == WebAppConstant.SDC_REQUEST && lstTask.Contains(n.TaskDirectionId)).ToList();
                    break;
                case "SALE":
                    lstTask = ListTask.Where(n => n.SystemCode == WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION).ToList().Select(n => n.TaskDirectionId).ToList();
                    lst = lst.Where(n => lstTask.Contains(n.TaskDirectionId)).ToList();
                    break;
                case "ACC":
                    lstTask = ListTask.Where(n => n.SystemCode == WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF
                         && n.StatusDirectionCode == WebAppConstant.SDC_REJECT
                         && n.IsValid == false).ToList().Select(n => n.TaskDirectionId).ToList();
                    lst = lst.Where(n => lstTask.Contains(n.TaskDirectionId)).ToList();
                    break;
                case "LOG":
                    lstTask = ListTask.Where(n => n.SystemCode == WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER
                         && n.StatusDirectionCode == WebAppConstant.SDC_REJECT
                         && n.IsValid == null).ToList().Select(n => n.TaskDirectionId).ToList();
                    lst = lst.Where(n => lstTask.Contains(n.TaskDirectionId)).ToList();
                    break;
                default:
                    break;
            }

            #region Load List Sub
            List<Common.SubRecordJson> lstSub = new List<Common.SubRecordJson>();
            foreach (var item in lst)
                lstSub.AddRange(CastList_SubRecordFull(_iSubRecordService.GetList_SubRecordByMainRecordId(item.MainRecordId).ToList()));
            ViewBag.SubList = lstSub;
            #endregion
            #region List Account
            var lstAccount = _AccountRepository.GetAll();
            ViewBag.lstAccount = lstAccount;

            #endregion
            return View(lst.OrderByDescending(n => n.DateModified).ToList());
        }
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_MANAGER)]
        public ActionResult SaleManagerEdit(long id)
        {
            #region load
            MainRecord main = new MainRecord();
            IStatusDirectionRepository _iStatusDirectionService = new StatusDirectionRepository();
            AccountRepository _iAccountService = new AccountRepository();
            Account acc = (Account)HttpContext.Session["Account"];
            MainRecordRepository _iMainRecordService = new MainRecordRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            ITaskFormRepository _TaskFormRepository = new TaskFormRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            PriorityRepository _PriorityRepository = new PriorityRepository();
            StatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            #endregion
            main = _iMainRecordService.GetById(id);
            #region List Task Edit
            var List_Allow_Edit = new List<Tuple<string, string, string, Nullable<bool>>>() 
            { 
            new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_CALL,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REQUEST,true)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_SALE_EXCEPTION,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_LOGISTIC_MANAGER,WebAppConstant.SDC_REJECT,null)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_ONLINE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_POST_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            ,new Tuple<string,string,string,bool?>(WebAppConstant.COF_PRE_PAY,WebAppConstant.GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF,WebAppConstant.SDC_REJECT,false)
            };
            var ListDirection_ALLOW = _TaskDirectionRepository.GetListPermissionEdit(List_Allow_Edit);
            #endregion
            #region check eror
            if (ListDirection_ALLOW.Where(n => n.TaskDirectionId == main.TaskDirectionId).ToList().Count == 0)
                return RedirectToAction("SaleManager");
            #endregion
            if (main.HoldByManagerId == null)
                _iMainRecordService.EditHoldManager(acc.AccountId, main.MainRecordId);
            else
                if (main.HoldByManagerId != acc.AccountId)
                {
                    SetMessage("Access deny!", "");
                    return RedirectToAction("SaleManager", "TTS");
                }
            #region load Data
            var _Order = _OrderRepository.GetByCode(main.FormId);
            if (_Order == null) { return RedirectToAction("SaleManager"); }
            var SubRecords = _iSubRecordService.GetList_SubRecordByMainRecordId(id).ToList();
            var SubList = CastList_SubRecordFull(SubRecords.ToList());
            ViewBag.SubList = SubList;
            ViewBag.TaskFormString = _TaskFormRepository.GetByCode(main.TaskFormCode).TaskFormName;
            ViewBag.StatusDirectionName = _StatusDirectionRepository.GetByCode(main.StatusDirectionCode).StatusDirectionName;
            ViewBag.StatusProcessName = _StatusProcessRepository.GetByCode(main.StatusProcessCode).StatusProcessName;
            #endregion
            #region Load Content
            var lstAccount = _iAccountService.GetAll();
            if (lstAccount == null)
                lstAccount = new List<Account>();
            ViewBag.listAccount = lstAccount;
            ViewBag.MainRecordId = id;
            #endregion
            return View(main);
        }
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_MANAGER), HttpPost]
        public ActionResult SaleManagerEdit(FormCollection data)
        {
            Account acc = (Account)HttpContext.Session["Account"];
            var statusDirectionCode = data["StatusDirectionCode"].ToString();
            var mainRecordId = Convert.ToInt64(data["MainRecordId"].ToString());
            #region Check Permission
            //Chua check MainRecord có thuộc bộ edit ko
            #endregion
            if (statusDirectionCode.Equals(WebAppConstant.SDC_REJECT) || statusDirectionCode.Equals(WebAppConstant.SDC_REQUEST) || statusDirectionCode.Equals(WebAppConstant.SDC_CANCEL))
            {
                #region  Load
                ISubRecordRepository _SubRecordRepository = new SubRecordRepository();
                IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
                IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
                TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
                #endregion
                var mainRecord = _MainRecordRepository.GetById(mainRecordId);
                var SubRecord = _SubRecordRepository.GetList_SubRecordByMainRecordId(mainRecord.MainRecordId);
                #region Neu chua co SubRecord, create moi
                if (SubRecord.Count == 0)
                {
                    SubRecord subRecord = new SubRecord();
                    subRecord.MainRecordId = mainRecord.MainRecordId;
                    subRecord.IsDeleted = false;
                    subRecord.PreviousSubId = 0;
                    subRecord.ContentField = Common.Common.ContentFielSub;
                    Common.Common common = new Common.Common();
                    Common.SubRecordJson subJson = new Common.SubRecordJson(mainRecord.OriginatorId.ToString(), _StatusDirectionRepository.GetByCode(mainRecord.StatusDirectionCode).StatusDirectionId.ToString(), mainRecord.PriorityId.ToString(), "0", "", DateTime.Now.ToString(), DateTime.Now.ToString(), subRecord.MainRecordId.ToString());
                    subRecord.SubList = "[" + common.SubRecordJsontoString(subJson) + "]";
                    var id = _SubRecordRepository.Create(subRecord);
                    var SubItem = _SubRecordRepository.GetById(id);
                    SubRecord.Add(SubItem);
                }
                #endregion
                #region Save 1 SubRecord
                Common.Common commons = new Common.Common();
                List<SubRecordJson> lst = new List<SubRecordJson>();
                foreach (var item in SubRecord)
                    lst.AddRange(commons.ToSubRecordJson((JArray)JsonConvert.DeserializeObject(item.SubList)));
                var SubLast = lst[lst.Count - 1];
                mainRecord.DateModified = DateTime.Now;
                SubRecordJson jsonSub = new SubRecordJson(acc.AccountId.ToString(), statusDirectionCode, mainRecord.PriorityId.ToString(), lst.Count.ToString(), data["DescriptionSub"].ToString(), DateTime.Now.ToString(), SubLast.DateHandIn, mainRecord.MainRecordId.ToString());
                lst.Add(jsonSub);
                string json = commons.ListSubRecordJsontoString(lst);
                _SubRecordRepository.EditSubLst(SubRecord[SubRecord.Count - 1].SubRecordId, json);
                #endregion
                #region Edit MainRecord
                mainRecord.StatusDirectionCode = statusDirectionCode;
                mainRecord.TaskDirectionId = _TaskDirectionRepository.GetBy(
                    mainRecord.TaskFormCode,
                    WebAppConstant.GROUP_SALE_GALAGALA_MANAGER,
                    statusDirectionCode).TaskDirectionId;
                mainRecord.HoldByManagerId = null;
                _MainRecordRepository.Edit(mainRecord);
                #endregion
            }
            return RedirectToAction("SaleManager", "TTS");
        }
        [HttpGet]
        [PermissionFilter(Permission_GroupId = WebAppConstant.PERMISSION_GROUP_SALE_MANAGER)]
        public ActionResult RemoveHolder(long mainId)
        {
            MainRecordRepository _MainRecordRepository = new MainRecordRepository();
            _MainRecordRepository.EditHoldStaff(null, mainId);
            var context = GlobalHost.ConnectionManager.GetHubContext<SystemUser>();
            context.Clients.Group("manager").targetOrder(mainId, "", "");
            return RedirectToAction("SaleManager", "TTS");
        }
        #endregion



        #region 3. Action Method: Create
        public ActionResult Create()
        {
            loadCreate();
            if (TempData["ad"] != null)
                ViewBag.adCode = TempData["ad"].ToString();
            else ViewBag.adCode = "";
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(MainRecord model)
        {
            try
            {
                if (validateMainRecord(model) == true)
                {
                    model.OriginatorId = ((Account)Session["Account"]).AccountId;//UserId
                    AccountRepository _iAccountService = new AccountRepository();
                    var isAdmin = _iAccountService.IsAdmin(((Account)Session["Account"]).AccountId, Common.Common._Department);
                    TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
                    OrderRepository _OrderRepository = new OrderRepository();
                    var _Order = _OrderRepository.GetByCode(model.FormId);
                    if (_Order.IsLocked == true || _Order.PaymentTypeCode == "PTC-1")
                        return RedirectToAction("Sale");
                    model.TaskFormCode = _Order.PaymentTypeCode == "PTC-1" ? "COF-1" : _Order.PaymentTypeCode == "PTC-2" ? "COF-2" : "COF-3";
                    if (isAdmin == true)
                    {
                        model.HoldByStaffId = null;
                        model.StatusDirectionCode = "SDC1";//Request
                        //if (_Order.OrderTypeCode == "ORD-T1")
                        //{
                        //    //Online Order
                        //    model.HoldByManagerId = null;
                        //}
                        if (_Order.OrderTypeCode == "ORD-T2")
                        {
                            //Call Order
                            model.HoldByManagerId = ((Account)Session["Account"]).AccountId;
                        }
                        var TaskDirection = _TaskDirectionRepository.GetBy(model.TaskFormCode, Common.Common._Department, "SDC1", true, 1);
                        if (TaskDirection == null)
                        {
                            loadCreate();
                            return View(model);
                        }
                        model.TaskDirectionId = TaskDirection.TaskDirectionId;
                    }
                    else
                    {
                        model.HoldByManagerId = null;
                        //if (_Order.OrderTypeCode == "ORD-T1")
                        //{
                        //    //Online Order
                        //    model.HoldByStaffId = ((Account)Session["Account"]).AccountId;
                        //}
                        if (_Order.OrderTypeCode == "ORD-T2")
                        {
                            //Call Order
                            model.HoldByStaffId = ((Account)Session["Account"]).AccountId;
                        }
                        model.StatusDirectionCode = "SDC7";
                    }
                    model.IsDeleted = false;
                    model.StatusProcessCode = "SPC1";
                    model.DateCreated = DateTime.Now;
                    model.DateModified = DateTime.Now;
                    IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
                    IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
                    IPriorityRepository _PriorityRepository = new PriorityRepository();
                    var idMain = _MainRecordRepository.Create(model);
                    if (idMain == null)
                    {
                        loadCreate();
                        return View(model);
                    }
                    _OrderRepository.UpdateLock(model.FormId, true);
                    //PTC-1,PTC-2,PTC-3
                    SubRecord subRecord = new SubRecord();
                    subRecord.MainRecordId = idMain;
                    subRecord.IsDeleted = false;
                    subRecord.PreviousSubId = 0;
                    subRecord.ContentField = Common.Common.ContentFielSub;
                    Common.Common common = new Common.Common();
                    Common.SubRecordJson subJson = new Common.SubRecordJson(model.OriginatorId.ToString(), _StatusDirectionRepository.GetByCode(model.StatusDirectionCode).StatusDirectionId.ToString(), model.PriorityId.ToString(), "0", "", DateTime.Now.ToString(), DateTime.Now.ToString(), subRecord.MainRecordId.ToString());
                    subRecord.SubList = "[" + common.SubRecordJsontoString(subJson) + "]";
                    ISubRecordRepository _SubRecordRepository = new SubRecordRepository();
                    _SubRecordRepository.Create(subRecord);
                    return RedirectToAction("SaleEdit", new { id = idMain });
                }
                else
                {
                    loadCreate();
                    return View(model);
                }
            }
            catch
            {
                loadCreate();
                return View(model);
            }

        }
        #endregion
        public ActionResult OrderViewForm(string code)
        {
            #region load
            OrderRepository _OrderRepository = new OrderRepository();
            OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
            BankRepository _BankRepository = new BankRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            AccountRepository _AccountRepository = new AccountRepository();
            SizeRepository _SizeRepository = new SizeRepository();
            #endregion
            var _Order = _OrderRepository.GetByCode(code);
            ViewBag.Order = _Order;
            ViewBag.OrderDetail = _OrderDetailRepository.GetListByOrderId(_Order.OrderId);
            if (_Order.BankId != null)
            {
                var bank = _BankRepository.GetById(Convert.ToInt64(_Order.BankId));
                ViewBag.BankName = bank == null ? "" : bank.BankName;
            }
            else
                ViewBag.BankName = "";
            List<long> lst = new List<long>();
            foreach (var item in (List<OrderDetail>)ViewBag.OrderDetail)
                lst.Add(item.ProductId);
            var lstProduct = _ProductRepository.GetListProductByListId(lst);
            ViewBag.Products = lstProduct;
            ViewBag.ListSize = _SizeRepository.GetList_SizeAll();
            if (_Order.CreatedBy != null)
            {
                var acc = _AccountRepository.Get_AccountById(Convert.ToInt64(_Order.CreatedBy));
                ViewBag.CreateByName = acc != null ? acc.FullName : "";
            }
            else
                ViewBag.CreateByName = "";
            ProvinceRepository _ProvinceRepository = new ProvinceRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            var order_Province = _ProvinceRepository.GetById(_Order.ShipToCity);
            var order_District = _DistrictRepository.GetById(_Order.ShipToDistrict);
            ViewBag.DistrictName = order_District != null ? order_District.Type + " " + order_District.DistrictName : "";
            ViewBag.ProvinceName = order_Province != null ? order_Province.Type + " " + order_Province.ProvinceName : "";
            return View();
        }
        #region 2. Action Method: OrderForm
        public ActionResult OrderForm()
        {
            loadOrderForm();
            return View();
        }
        [HttpPost]
        public ActionResult OrderForm(Order model, FormCollection formData)
        {
            #region
            ProvinceRepository _ProvinceRepository = new ProvinceRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            BankRepository _BankRepository = new BankRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            OrderRepository _OrderRepository = new OrderRepository();
            OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
            #endregion
            decimal totalPay = 0;
            //decimal SubTotalFee = 0;

            try
            {
                ViewBag.ListProvince = new SelectList(_ProvinceRepository.GetAll().OrderBy(n => n.ProvinceName).ToList(), "ProvinceId", "ProvinceName", null);
                ViewBag.ListDistrict = _DistrictRepository.GetAll().OrderBy(n => n.DistrictName).ToList();
                ViewBag.ListBank = new SelectList(_BankRepository.GetAll(false, true), "BankId", "BankName", null);

                if (validateCreateOrderForm(model, formData) == true)
                {
                    for (int i = 0; i < formData["ListProductId"].Split(',').ToList().Count; i++)
                    {
                        if (_ProductRepository.GetStoreClose(Convert.ToInt64(formData["ListProductId"].Split(',').ToList()[i])) == false)
                        {
                            var item = _ProductRepository.GetById(Convert.ToInt64(formData["ListProductId"].Split(',').ToList()[i]));
                            if (item.IsVerified == true && item.IsDeleted == false)
                            {
                                OrderDetail itemOrderDetail = new OrderDetail();
                                itemOrderDetail.DateCreated = DateTime.Now;
                                itemOrderDetail.DateModified = DateTime.Now;
                                itemOrderDetail.IsDeleted = false;
                                itemOrderDetail.OrderQuantity = Convert.ToInt32(formData["txtListInputQuantity"].Split(',').ToList()[i]);
                                itemOrderDetail.ProductId = Convert.ToInt64(formData["ListProductId"].Split(',').ToList()[i]);
                                itemOrderDetail.UnitPrice = Convert.ToDecimal(item.MobileOnlinePrice);
                                itemOrderDetail.UnitPriceDiscount = item.PromotePrice != null ? Convert.ToDecimal(item.PromotePrice) : itemOrderDetail.UnitPrice;
                                //SubTotalFee += itemOrderDetail.UnitPrice * itemOrderDetail.OrderQuantity;
                                if (itemOrderDetail.UnitPriceDiscount != null)
                                    itemOrderDetail.TotalPrice = itemOrderDetail.UnitPriceDiscount * itemOrderDetail.OrderQuantity;
                                else
                                    itemOrderDetail.TotalPrice = itemOrderDetail.UnitPrice * itemOrderDetail.OrderQuantity;
                                totalPay += Convert.ToDecimal(itemOrderDetail.TotalPrice);
                                listOrderDetail.Add(itemOrderDetail);
                            }
                        }
                    }
                    Order od = new Order();
                    Province Province = _ProvinceRepository.GetById(Convert.ToInt64(formData["ProvinceId"]));
                    model.ShipToCity = Convert.ToInt64(formData["ProvinceId"]);
                    District District = _DistrictRepository.GetById(Convert.ToInt64(formData["DistrictId"]));
                    model.ShipToDistrict = Convert.ToInt64(formData["DistrictId"]);
                    model.ShippingFee = 0;
                    model.SubTotalFee = totalPay;
                    model.TotalPaid = model.SubTotalFee + model.SubTotalFee * (model.TaxFee / 100) + model.ShippingFee;
                    model.DateCreated = DateTime.Now;
                    model.IsActive = true;
                    model.IsClosed = false;
                    model.IsLocked = false;
                    model.IsDeleted = false;
                    model.IsWarning = false;
                    model.IsDeliveryConfirmed = false;
                    model.IsPaymentConfirmed = false;
                    model.IsOrderConfirmed = false;
                    model.IsWarning = false;
                    //model.SecureKey
                    //model.TransactionStatusId
                    model.DateModified = DateTime.Now;
                    model.ModifiedBy = ((Account)Session["Account"]).AccountId;
                    model.CreatedBy = ((Account)Session["Account"]).AccountId;

                    model.TotalProduct = listOrderDetail.Count;
                    if (formData["typeRad"] == "postPay")
                    {
                        model.BankId = null;
                        model.CardHolderName = null;
                        model.CardNumber = null;
                        model.CardTypeId = null;
                        model.PaymentTypeCode = "PTC-3";
                    }
                    else
                        model.PaymentTypeCode = "PTC-2";
                    model.OrderTypeCode = "ORD-T2";
                    PaymentTypeRepository _PaymentType = new PaymentTypeRepository();
                    PaymentType type = _PaymentType.GetByCode(model.PaymentTypeCode);
                    model.DueTransactionTime = model.DateCreated.Value.Add(TimeSpan.Parse(type.LimitTimeOnTransaction.Value.ToString()));
                    var idOrder_rs = _OrderRepository.Create(model);
                    _OrderDetailRepository.CreateByOrderId(listOrderDetail, idOrder_rs);
                    SetMessage("", "Create complete");
                    TempData.Add("ad", _OrderRepository.GetById(idOrder_rs).OrderCode);

                    return RedirectToAction("Create");
                }
                loadOrderForm();
                return View(model);
            }
            catch
            {
                loadOrderForm();
                return View(model);
            }
        }
        #endregion


        #region 8. Common Method: loadOrderForm
        private void loadOrderForm()
        {
            #region declare
            ProvinceRepository _ProvinceRepository = new ProvinceRepository();
            DistrictRepository _DistrictRepository = new DistrictRepository();
            BankRepository _BankRepository = new BankRepository();
            #endregion
            ViewBag.ListProvince = new SelectList(_ProvinceRepository.GetAll().OrderBy(n => n.ProvinceName).ToList(), "ProvinceId", "ProvinceName", null);
            ViewBag.ListDistrict = _DistrictRepository.GetAll().OrderBy(n => n.DistrictName).ToList();
            ViewBag.ListBank = new SelectList(_BankRepository.GetAll(false, true), "BankId", "BankName", null);
        }
        #endregion

        #region 9. Common Method: validateCreateOrderForm
        private bool validateCreateOrderForm(Order model, FormCollection formData)
        {
            try
            {
                bool valid = true;
                var time = DateTime.Now;

                //---------------------Customer Information       
                if (model.CustomerName == null || model.CustomerName == "")
                {
                    ModelState.AddModelError("Order.CustomerName", "Customer Information - Customer Name required");
                    valid = false;
                }

                if (model.CustomerEmail == null || model.CustomerEmail == "")
                {
                    ModelState.AddModelError("Order.CustomerEmail", "Customer Information - Customer Email required");
                    valid = false;
                }
                if (model.CustomerPhone == null || model.CustomerPhone == "")
                {
                    ModelState.AddModelError("Order.CustomerPhone", "Customer Information - Customer Phone required");
                    valid = false;
                }


                //---------------------Customer Bank Information
                if (model.BankId == null || model.BankId == 0)
                {
                    ModelState.AddModelError("Order.BankId", "Customer Bank Information - Bank required");
                    valid = false;
                }
                if (model.CardHolderName == null || model.CardHolderName.Trim().Length == 0)
                {
                    ModelState.AddModelError("Order.CardHolderName", "Customer Bank Information - Account Name required");
                    valid = false;
                }
                long cardNumberCustomer;
                bool IsDTCardNumberCustomer = long.TryParse(model.CardNumber.ToString(), out cardNumberCustomer);
                if (model.CardNumber == null || model.CardNumber.ToString().Trim().Length == 0 || IsDTCardNumberCustomer == false)
                {
                    ModelState.AddModelError("Order.CardNumber", "Customer Bank Information - Account Number required and must be number");
                    valid = false;
                }

                //---------------------Shipping Information       
                if (model.ReceiverName == null || model.ReceiverName == "")
                {
                    ModelState.AddModelError("Order.ReceiverName", "Shipping Information - Receiver Name required");
                    valid = false;
                }

                if (model.ReceiverPhone == null || model.ReceiverPhone == "")
                {
                    ModelState.AddModelError("Order.ReceiverPhone", "Shipping Information - Receiver Phone required");
                    valid = false;
                }
                if (model.ShipToAddress == null || model.ShipToAddress == "")
                {
                    ModelState.AddModelError("Order.ShipToAddress", "Shipping Information - Address required");
                    valid = false;
                }
                //if (model.ShipToDistrict == null || model.ShipToDistrict == "")
                //{
                //    ModelState.AddModelError("Order.ShipToDistrict", "Shipping Information - District required");
                //    valid = false;
                //}
                //if (model.ShipToCity == null || model.ShipToCity == "")
                //{
                //    ModelState.AddModelError("Order.ShipToCity", "Shipping Information - Province required");
                //    valid = false;
                //}

                //---------------------List Product Order       
                if (formData["ListProductId"] == null || formData["ListProductId"] == "" || formData["txtListInputQuantity"] == null || formData["txtListInputQuantity"] == "")
                {
                    ModelState.AddModelError("Order.ListProduct", "Order Information - Product required");
                    valid = false;
                }

                return valid;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Common Method (key:CM):
        #region 1. Common Method: GetList_AreaProblemDropDownList
        private void GetList_TaskFormDropDownList(long selected, bool isDelete)
        {
            ITaskFormRepository _iTaskFormService = new TaskFormRepository();
            var list_TaskForm = _iTaskFormService.GetList_IsDeleted(isDelete);
            object Object = null;
            if (selected != -1)
            {
                foreach (var item in list_TaskForm)
                {
                    if (item.TaskFormId == selected)
                    {
                        Object = item.TaskFormId;
                        break;
                    }
                }
            }
            ViewBag.lst_DDL_TaskForm = new SelectList(list_TaskForm, "TaskFormId", "TaskFormName", Object);
        }
        #endregion

        #region 2. Common Method: GetList_PriorityDropDownList
        //Description: get Priority dropdownlist 
        private void GetList_PriorityDropDownList(long selected, bool isDelete)
        {
            IPriorityRepository _iPriorityService = new PriorityRepository();
            var list_Priority = _iPriorityService.GetList_IsDeleted(isDelete);
            object Object = null;
            if (selected != -1)
            {
                foreach (var item in list_Priority)
                {
                    if (item.PriorityId == selected)
                    {
                        Object = item.PriorityId;
                        break;
                    }
                }
            }
            ViewBag.lst_DDL_Priority = new SelectList(list_Priority, "PriorityId", "PriorityName", Object);
        }
        #endregion

        #region 3. Common Method: GetList_StatusProcessDropDownList
        //Description: get Priority dropdownlist 
        private void GetList_StatusProcessDropDownList(long selected, bool isDelete)
        {
            IStatusProcessRepository _iStatusProcessService = new StatusProcessRepository();
            var list_StatusProcess = _iStatusProcessService.GetList_IsDeleted(isDelete);
            object Object = null;
            if (selected != -1)
            {
                foreach (var item in list_StatusProcess)
                {
                    if (item.StatusProcessId == selected)
                    {
                        Object = item.StatusProcessId;
                        break;
                    }
                }
            }
            ViewBag.lst_DDL_StatusProcess = new SelectList(list_StatusProcess, "StatusProcessId", "StatusProcessName", Object);
        }
        #endregion

        #region 4. Common Method: GetList_StatusDirectionDropDownList
        //Description: get Priority dropdownlist 
        private void GetList_StatusDirectionDropDownList(long selected, bool isDelete)
        {
            IStatusDirectionRepository _iStatusDirectionService = new StatusDirectionRepository();
            ViewBag.list_StatusDirection = _iStatusDirectionService.GetList_IsDeleted(isDelete);
            object Object = null;
            if (selected != -1)
            {
                foreach (var item in ViewBag.list_StatusDirection)
                {
                    if (item.StatusDirectionId == selected)
                    {
                        Object = item.StatusDirectionId;
                        break;
                    }
                }
            }
            ViewBag.lst_DDL_StatusDirection = new SelectList(ViewBag.list_StatusDirection, "StatusDirectionId", "StatusDirectionName", Object);
        }
        #endregion

        #region 5. Common Method: CastList_SubRecordFull
        //Description: get Priority dropdownlist 
        private List<Common.SubRecordJson> CastList_SubRecordFull(List<SubRecord> lst_SubRecord)
        {
            var lastSub = lst_SubRecord.LastOrDefault();
            List<Common.SubRecordJson> lst_SubRecordFull = new List<Common.SubRecordJson>();

            foreach (var subRecordJson in lst_SubRecord)
            {
                Common.Common commons = new Common.Common();
                lst_SubRecordFull.AddRange(commons.ToSubRecordJson((JArray)JsonConvert.DeserializeObject(subRecordJson.SubList)));
            }

            return lst_SubRecordFull;
        }
        #endregion

        #region 6. Common Method: loadCreate
        private void loadCreate()
        {
            ITaskFormRepository _iTaskFormService = new TaskFormRepository();
            ViewBag.taskForm = _iTaskFormService.GetById(2);
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            ViewBag.LstPriority = _PriorityRepository.GetAll();
            OrderRepository _OrderRepository = new OrderRepository();
            ViewBag.LstOrder = _OrderRepository.GetAll(false, true, false).Where(n => n.PaymentTypeCode == "PTC-2" || n.PaymentTypeCode == "PTC-3").ToList();
        }
        #endregion

        #region 7. Common Method: validateMainRecord
        private bool validateMainRecord(MainRecord model)
        {
            try
            {
                bool valid = true;
                if (model.FormId != null)
                {
                    OrderRepository _OrderRepository = new OrderRepository();
                    var FormId = _OrderRepository.GetByCode(model.FormId.ToString());
                    if (FormId == null || FormId.IsLocked == true || FormId.IsDeleted == true || FormId.IsActive == false)
                    {
                        ModelState.AddModelError("MainRecord.OPSForm", "Order Form is non-existed or be locked");
                        valid = false;
                    }
                    return valid;
                }
                ModelState.AddModelError("MainRecord.OPSForm", "Order Form Id required");
                valid = false;

                return valid;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 8. Common Method: SetMessage
        private void SetMessage(string error, string complete)
        {
            if (TempData["MessageError"] == null)
                TempData.Add("MessageError", error);
            else
                TempData["MessageError"] = error;
            if (TempData["MessageComplete"] == null)
                TempData.Add("MessageComplete", complete);
            else
                TempData["MessageComplete"] = complete;
        }
        #endregion
        #endregion
    }
}
