using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.lps;
using HTTelecom.Domain.Core.DataContext.ops;
using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using HTTelecom.Domain.Core.Repository.acs;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.lps;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.Repository.ops;
using HTTelecom.Domain.Core.Repository.tts;
using HTTelecom.WebUI.Finance.Common;
using HTTelecom.WebUI.Finance.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.Finance.Controllers
{
    [SessionLoginFilter]
    public class TTSController : Controller
    {
        [HttpPost]
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
            HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
            HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
            var DepartmentCode = _DepartmentRepository.GetByAccountId(acc.AccountId);
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            TaskFormRepository _TaskFormRepository = new TaskFormRepository();
            var TaskFormCode = new List<string>() { "COF-2", "COF-3", "MRF", "COF-1" };
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
                var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                ViewBag.TaskDirection = _TaskDirectionRepository.GetAll();
                foreach (var item in ods)
                    lst.AddRange(_MainRecordRepository.GetByCustomerServiceAdminNotNull(new List<string>() { item.Item2 }, new List<string>() { "SDC1", "SDC2", "SDC3", "SDC4", "SDC5", "SDC6", "SDC7", "SDC8", "SDC9" }, acc.AccountId, new List<int?>() { item.Item1 - 1 }, item.Item1, new List<int?>() { item.Item1 + 1 }, q, Convert.ToInt32(filter), Convert.ToInt32(tag)).Where(n => (date - n.DateModified.Value).TotalMinutes <= 30).ToList());
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC1", "SDC3", "SDC6", "SDC5" }).Contains(n.StatusDirectionCode)).ToList();
            }
            else
            {
                ViewBag.isAdmin = false;
                var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                foreach (var item in ods)
                    lst.AddRange(_MainRecordRepository.GetByCustomerService(new List<string>() { item.Item2 }, new List<string>() { "SDC6", "SDC8", "SDC9" }, "SDC6", acc.AccountId, item.Item1, q, Convert.ToInt32(filter), Convert.ToInt32(tag)).Where(n => (date - n.DateModified.Value).TotalMinutes <= 30).ToList());
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList();
            }
            var rs = lst.Select(e => new { e.DateModified, e.MainRecordId, e.FormId }).ToList().OrderByDescending(n => n.DateModified).ToList();
            return Json(new { data = rs }, JsonRequestBehavior.AllowGet);
        }
        #region Action Method (key:AM):
        #region 1. Action Method: Index
        public ActionResult Index(int? page, int? filter, int? tag, string q)
        {
            if (q == null) q = "";
            ViewBag.page = page;
            if (filter == null) filter = 0;
            if (tag == null) tag = 0;
            ViewBag.q = q;
            ViewBag.tag = tag;
            ViewBag.filter = filter;
            Account acc = (Account)HttpContext.Session["Account"];
            HTTelecom.Domain.Core.Repository.ams.DepartmentRepository _DepartmentRepository = new DepartmentRepository();
            HTTelecom.Domain.Core.Repository.ams.AccountRepository _AccountRepository = new AccountRepository();
            var DepartmentCode = _DepartmentRepository.GetByAccountId(acc.AccountId);
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            TaskFormRepository _TaskFormRepository = new TaskFormRepository();
            var TaskFormCode = new List<string>() { "COF-2", "COF-3", "MRF", "COF-1" };
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
                var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                ViewBag.TaskDirection = _TaskDirectionRepository.GetAll();
                foreach (var item in ods)
                    lst.AddRange(_MainRecordRepository.GetByCustomerServiceAdminNotNull(new List<string>() { item.Item2 }, new List<string>() { "SDC1", "SDC2", "SDC3", "SDC4", "SDC5", "SDC6", "SDC7", "SDC8", "SDC9" }, acc.AccountId, new List<int?>() { item.Item1 - 1 }, item.Item1, new List<int?>() { item.Item1 + 1 }, q, Convert.ToInt32(filter), Convert.ToInt32(tag)));
                //lst = _MainRecordRepository.GetByCustomerServiceAdmin(TaskFormCode, new List<string>() { "SDC1", "SDC2", "SDC3", "SDC4", "SDC5", "SDC6", "SDC7", "SDC8", "SDC9" }, acc.AccountId, new List<int?>() { OrderQueue - 1 }, OrderQueue, new List<int?>() { OrderQueue + 1 }, q, Convert.ToInt32(filter), Convert.ToInt32(tag));
                //lst = _MainRecordRepository.GetByFinanceAdmin(TaskFormCode, new List<string>() { "SDC1", "SDC2", "SDC3", "SDC4", "SDC5", "SDC6", "SDC7", "SDC8", "SDC9" }, acc.AccountId, new List<int?>() { OrderQueue - 1 }, OrderQueue, new List<int?>() { OrderQueue + 1 }, q, Convert.ToInt32(filter), Convert.ToInt32(tag));
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC1", "SDC3", "SDC6", "SDC5" }).Contains(n.StatusDirectionCode)).ToList();
            }
            else
            {
                ViewBag.isAdmin = false;
                var ods = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                foreach (var item in ods)
                    lst.AddRange(_MainRecordRepository.GetByCustomerService(new List<string>() { item.Item2 }, new List<string>() { "SDC6", "SDC8", "SDC9" }, "SDC6", acc.AccountId, item.Item1, q, Convert.ToInt32(filter), Convert.ToInt32(tag)));
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList();
                //lst = _MainRecordRepository.GetByFinance(TaskFormCode, new List<string>() { "SDC8", "SDC9" }, "SDC6", acc.AccountId, OrderQueue, q, Convert.ToInt32(filter), Convert.ToInt32(tag));
                //lst = _MainRecordRepository.GetByCustomerService(TaskFormCode, new List<string>() { "SDC8", "SDC9" }, "SDC6", acc.AccountId, OrderQueue, q, Convert.ToInt32(filter), Convert.ToInt32(tag));
            }
            List<Common.SubRecordJson> lstSub = new List<Common.SubRecordJson>();
            foreach (var item in lst)
                lstSub.AddRange(CastList_SubRecordFull(_iSubRecordService.GetList_SubRecordByMainRecordId(item.MainRecordId).ToList()));
            ViewBag.SubList = lstSub;
            ViewBag.TaskDirection = _TaskDirectionRepository.GetAll().ToList();
            ViewBag.TaskForms = _TaskFormRepository.GetAll().ToList();
            return View(lst.Distinct().OrderByDescending(n => n.DateModified).ToList());
        }
        #endregion
        [HttpPost]
        public string TestPost()
        {
            string strLive = "http://blogcaycanh.vn/test/ghi_log";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strLive);
            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(HttpContext.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            //log.Info("strRequest: " + strRequest);
            req.ContentLength = strRequest.Length;
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadLine();
            streamIn.Close();
            return "";

        }

        #region 2. Action Method: EditCOF
        public ActionResult EditCOF(long id)
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
            ITaskFormRepository _TaskFormRepository = new TaskFormRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            PriorityRepository _PriorityRepository = new PriorityRepository();
            StatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            #endregion
            ViewBag.list_StatusDirection = _iStatusDirectionService.GetAll();
            main = _iMainRecordService.GetById(id);
            var isAdmin = _iAccountService.IsAdmin(acc.AccountId, Common.Common._Department);
            var TaskDirection = new TaskDirection();
            if (main.TaskDirectionId != null && main.TaskDirectionId != 0)
                TaskDirection = _TaskDirectionRepository.GetById(main.TaskDirectionId);
            var TaskFormCode = new List<string>() { "COF-2", "COF-3", "MRF", "COF-1" };
            var lstOrderQueue = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
            var SubRecords = _iSubRecordService.GetList_SubRecordByMainRecordId(id).ToList();
            var SubList = CastList_SubRecordFull(SubRecords.ToList());
            ViewBag.SubList = SubList;
            ViewBag.TaskFormString = _TaskFormRepository.GetByCode(main.TaskFormCode).TaskFormName;
            ViewBag.StatusDirectionName = _StatusDirectionRepository.GetByCode(main.StatusDirectionCode).StatusDirectionName;
            ViewBag.StatusProcessName = _StatusProcessRepository.GetByCode(main.StatusProcessCode).StatusProcessName;
            ViewBag.realOnly = false;
            var _Order = _OrderRepository.GetByCode(main.FormId);
            if (_Order == null)
                return RedirectToAction("Index");
            var _taskFormCode = _Order.PaymentTypeCode == "PTC-1" ? "COF-1" : _Order.PaymentTypeCode == "PTC-2" ? "COF-2" : "COF-3";
            var OrderQueue = 0;
            var lstOrQuere = lstOrderQueue.Where(n => n.Item2 == main.TaskFormCode).ToList();
            var taskDirection = _TaskDirectionRepository.GetById(main.TaskDirectionId);
            if ((taskDirection.StatusDirectionCode == "SDC1" && taskDirection.IsValid != null && taskDirection.IsValid == true && taskDirection.OrderQueue == lstOrQuere[1].Item1 - 1) || taskDirection.OrderQueue == lstOrQuere[1].Item1)
                OrderQueue = lstOrQuere[1].Item1;
            else OrderQueue = lstOrQuere[0].Item1;
            if (isAdmin == true)
            {
                #region admin
                var lstTaskDirection = new List<TaskDirection>();
                foreach (var item in lstOrderQueue)
                    lstTaskDirection.AddRange(_TaskDirectionRepository.GetListPermissionAdmin(item.Item2, item.Item1));
                List<long> lst = new List<long>();
                foreach (var item in lstTaskDirection)
                    lst.Add(item.TaskDirectionId);
                if (main.TaskDirectionId != null && main.TaskDirectionId != 0 && lst.Contains(Convert.ToInt64(main.TaskDirectionId)) == false)
                    return RedirectToAction("Index");
                if (main.HoldByManagerId == null)
                    _iMainRecordService.EditHoldManager(acc.AccountId, main.MainRecordId);
                if (main.TaskDirectionId == null)
                    _iMainRecordService.EditTaskDirection(_TaskDirectionRepository.GetByIsValidAndOrderQuere(_taskFormCode, null, OrderQueue).TaskDirectionId, main.MainRecordId);
                if ((new List<string>() { "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) || (main.StatusDirectionCode == "SDC3" && TaskDirection.OrderQueue > OrderQueue) || (main.StatusDirectionCode == "SDC1" && TaskDirection.OrderQueue < OrderQueue))
                    ViewBag.realOnly = false;
                else
                    ViewBag.realOnly = true;
                if (taskDirection.OrderQueue == lstOrQuere[1].Item1 || (taskDirection.StatusDirectionCode == "SDC1" && taskDirection.IsValid != null && taskDirection.IsValid == true && taskDirection.OrderQueue == lstOrQuere[1].Item1 - 1))
                    ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC2", "SDC3", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
                else
                    if (taskDirection.OrderQueue != null && taskDirection.OrderQueue == 1)
                        ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC1", "SDC5", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
                    else ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC1", "SDC3", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
                #endregion
            }
            else
            {
                #region user
                if (main.TaskDirectionId != null && main.TaskDirectionId != 0 && TaskDirection != null && lstOrderQueue.Where(n => n.Item1 == TaskDirection.OrderQueue).ToList().Count == 0)
                    return RedirectToAction("Index");
                if (main.StatusDirectionCode.ToUpper() != "SDC6")
                {
                    if ((new List<string>() { "SDC7", "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) == true && (main.HoldByStaffId == null || acc.AccountId == main.HoldByStaffId))
                    { }
                    else
                    {
                        SetMessage("Access deny!", "");
                        return RedirectToAction("Index");
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
                            return RedirectToAction("Index", "TTS");
                        }
                    ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
                }
                #endregion
            }

            var lstAccount = _iAccountService.GetAll();
            if (lstAccount == null)
                lstAccount = new List<Account>();
            ViewBag.listAccount = lstAccount;
            ViewBag.MainRecordId = id;
            ViewBag.isAdmin = isAdmin;
            ViewBag.Order = _Order;
            return View(main);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditCOF(FormCollection formData)
        {
            try
            {
                #region load
                Account acc = (Account)HttpContext.Session["Account"];
                AccountRepository _iAccountService = new AccountRepository();
                IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
                Common.Common commons = new Common.Common();
                ISubRecordRepository _SubRecordRepository = new SubRecordRepository();
                IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
                TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
                List<SubRecordJson> lst = new List<SubRecordJson>();
                OrderRepository _OrderRepository = new OrderRepository();
                #endregion
                long temp = 0;
                #region Error
                if (formData["MainRecordId"] == null || long.TryParse(formData["MainRecordId"].ToString(), out temp) == false)
                {
                    return RedirectToAction("EditCOF", Convert.ToInt64(formData["MainRecordId"].ToString()));
                }
                if (formData["StatusDirectionCode"] == null || long.TryParse(formData["StatusDirectionCode"].ToString(), out temp) == false)
                {
                    SetMessage("Please select StatusDirection", "");
                    return RedirectToAction("EditCOF", Convert.ToInt64(formData["MainRecordId"].ToString()));
                }
                //if (formData["DescriptionSub"] == null || formData["DescriptionSub"].ToString().Length == 0 || formData["DescriptionSub"].ToString().Length > 100)
                //{
                //    SetMessage("please input Description", "");
                //    return RedirectToAction("EditCOF", Convert.ToInt64(formData["MainRecordId"].ToString()));
                //}
                #endregion
                var mainRecord = _MainRecordRepository.GetById(Convert.ToInt64(formData["MainRecordId"].ToString()));
                var SubRecord = _SubRecordRepository.GetList_SubRecordByMainRecordId(mainRecord.MainRecordId);
                var TaskFormCode = new List<string>() { "COF-2", "COF-3", "MRF", "COF-1" };
                var lstOrderQueue = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
                var OrderQueue = 0;
                var _taskDirection = _TaskDirectionRepository.GetById(mainRecord.TaskDirectionId);
                var lstOrQuere = lstOrderQueue.Where(n => n.Item2 == mainRecord.TaskFormCode).ToList();
                if ((_taskDirection.StatusDirectionCode == "SDC1" && _taskDirection.IsValid != null && _taskDirection.IsValid == true && _taskDirection.OrderQueue == lstOrQuere[1].Item1 - 1) || _taskDirection.OrderQueue == lstOrQuere[1].Item1)
                    OrderQueue = lstOrQuere[1].Item1;
                else
                    OrderQueue = lstOrQuere[0].Item1;
                foreach (var item in SubRecord)
                    lst.AddRange(commons.ToSubRecordJson((JArray)JsonConvert.DeserializeObject(item.SubList)));
                var SubLast = lst[lst.Count - 1];
                var StatusDirectionCode = _StatusDirectionRepository.GetById(Convert.ToInt64(formData["StatusDirectionCode"].ToString())).StatusDirectionCode;
                mainRecord.DateModified = DateTime.Now;
                var isAdmin = _iAccountService.IsAdmin(acc.AccountId, Common.Common._Department);
                if (isAdmin)
                {
                    var order = _OrderRepository.GetByCode(mainRecord.FormId);
                    if (order.IsPaymentConfirmed != true && StatusDirectionCode == "SDC1" && mainRecord.TaskFormCode == "COF-1")
                    {
                        SetMessage("Order must be payment.", "");
                        return RedirectToAction("EditCOF", Convert.ToInt64(formData["MainRecordId"].ToString()));
                    }
                    if (StatusDirectionCode == "SDC3" && mainRecord.TaskFormCode == "COF-1")
                    {
                        SetMessage("Order Online don't Reject To Sale.", "");
                        return RedirectToAction("EditCOF", Convert.ToInt64(formData["MainRecordId"].ToString()));
                    }
                    
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
                        return RedirectToAction("Index");
                    if (_taskDirection.OrderQueue == lstOrQuere[1].Item1 || (_taskDirection.OrderQueue == lstOrQuere[1].Item1 - 1 && _taskDirection.StatusDirectionCode == "SDC1" && _taskDirection.IsValid != null && _taskDirection.IsValid == true))
                    {
                        if ((new List<string>() { "SDC3", "SDC7", "SDC8", "SDC9" }).Contains(mainRecord.StatusDirectionCode) == false && ((new List<string>() { "SDC3", "SDC2", "SDC6" }).Contains(StatusDirectionCode)) == false)
                            return RedirectToAction("Index");
                    }
                    else
                    {
                        if ((new List<string>() { "SDC3", "SDC7", "SDC8", "SDC9" }).Contains(mainRecord.StatusDirectionCode) == false && ((new List<string>() { "SDC3", "SDC1", "SDC6" }).Contains(StatusDirectionCode)) == false)
                            return RedirectToAction("Index");
                    }
                    #endregion

                    #region admin
                    if (StatusDirectionCode == "SDC6")
                    {
                        mainRecord.TaskDirectionId = _TaskDirectionRepository.GetByIsValidAndOrderQuere(mainRecord.TaskFormCode, null, OrderQueue).TaskDirectionId;
                        mainRecord.HoldByStaffId = null;
                    }
                    else
                    {
                        TaskDirection taskDirection = _TaskDirectionRepository
                            .GetBy(mainRecord.TaskFormCode, StatusDirectionCode, OrderQueue);
                        if (taskDirection == null)
                            return RedirectToAction("Index");
                        mainRecord.TaskDirectionId = taskDirection.TaskDirectionId;
                    }

                    #region check Ispaymented for
                    if (StatusDirectionCode == "SDC2" && (mainRecord.TaskFormCode == "COF-2" || mainRecord.TaskFormCode == "COF-3"))
                    {

                        if (order != null && order.IsPaymentConfirmed != true)
                        {
                            _OrderRepository.UpdatePayment(order.OrderId, true);
                        }
                    }
                    if (StatusDirectionCode == "SDC2")
                        _OrderRepository.UpdateStatus(mainRecord.FormId, "TRANS-TC2", true);
                    if (StatusDirectionCode == "SDC5" && mainRecord.TaskFormCode == "COF-1")
                        _OrderRepository.UpdateStatus(mainRecord.FormId, "TRANS-TC3", false);
                    #endregion

                    #region editt Main
                    mainRecord.HoldByManagerId = acc.AccountId;//Admin
                    mainRecord.HoldByStaffId = null;
                    mainRecord.StatusDirectionCode = StatusDirectionCode;
                    _MainRecordRepository.Edit(mainRecord);
                    #endregion

                    #region Update Value Quantity Logistic
                    //OrderDetailRepository _iOrderDetailsService = new OrderDetailRepository();
                    //OrderRepository _iOrderService = new OrderRepository();
                    //ProductItemRepository _iProductItemService = new ProductItemRepository();
                    //ProductRepository _ProductRepository = new ProductRepository();
                    //List<OrderDetail> lstOrderDetails = _iOrderDetailsService.GetListByOrderId(_iOrderService.GetByCode(mainRecord.FormId).OrderId);
                    //var lstProductItemInSize = new List<Tuple<long, string, int>>();
                    //foreach (var item in lstOrderDetails)
                    //{
                    //    var product_Item = _ProductRepository.GetById(item.ProductId);
                    //    lstProductItemInSize.Add(new Tuple<long, string, int>(Convert.ToInt64(item.SizeId), product_Item.ProductStockCode, item.OrderQuantity));
                    //}

                    //ProductItemInSizeRepository _ProductItemInSizeRepository = new ProductItemInSizeRepository();
                    //_ProductItemInSizeRepository.UpdateDownQuantity(lstProductItemInSize);
                    #endregion



                    #endregion
                }
                else
                {
                    #region user
                    var TaskDirection = new TaskDirection();
                    if (mainRecord.TaskDirectionId != null && mainRecord.TaskDirectionId != 0)
                        TaskDirection = _TaskDirectionRepository.GetById(mainRecord.TaskDirectionId);
                    if (mainRecord.TaskDirectionId != null && mainRecord.TaskDirectionId != 0 && TaskDirection != null && TaskDirection.OrderQueue != OrderQueue)
                        return RedirectToAction("Index");
                    if ((new List<string>() { "SDC6" }).Contains(mainRecord.StatusDirectionCode) == false)
                        return RedirectToAction("Index");
                    #endregion

                    #region user
                    mainRecord.StatusDirectionCode = StatusDirectionCode;
                    mainRecord.HoldByStaffId = acc.AccountId;
                    _MainRecordRepository.Edit(mainRecord);
                    #endregion
                }
                SubRecordJson jsonSub = new SubRecordJson(acc.AccountId.ToString(),
                    formData["StatusDirectionCode"].ToString(),
                    mainRecord.PriorityId.ToString(),
                    lst.Count.ToString(),
                    formData["DescriptionSub"].ToString(),
                    DateTime.Now.ToString(),
                    SubLast.DateHandIn,
                    mainRecord.MainRecordId.ToString());
                lst.Add(jsonSub);
                string json = commons.ListSubRecordJsontoString(lst);
                _SubRecordRepository.EditSubLst(SubRecord[SubRecord.Count - 1].SubRecordId, json);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("EditCOF", Convert.ToInt64(formData["MainRecordId"].ToString()));
            }
        }
        #endregion

        #region 3. Action Method: EditMRF
        public ActionResult EditMRF(long id)
        {
            #region load
            MainRecord main = new MainRecord();
            AccountRepository _iAccountService = new AccountRepository();
            Account acc = (Account)HttpContext.Session["Account"];
            MainRecordRepository _iMainRecordService = new MainRecordRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            ITaskFormRepository _TaskFormRepository = new TaskFormRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            PriorityRepository _PriorityRepository = new PriorityRepository();
            StatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            #endregion
            this.GetList_TaskFormDropDownList(-1, false);
            this.GetList_PriorityDropDownList(-1, false);
            this.GetList_StatusProcessDropDownList(3, false);
            IStatusDirectionRepository _iStatusDirectionService = new StatusDirectionRepository();
            ViewBag.list_StatusDirection = _iStatusDirectionService.GetAll();
            main = _iMainRecordService.GetById(id);
            var isAdmin = _iAccountService.IsAdmin(acc.AccountId, Common.Common._Department);
            var TaskDirection = new TaskDirection();
            if (main.TaskDirectionId != null)
                TaskDirection = _TaskDirectionRepository.GetById(main.TaskDirectionId);
            int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(main.TaskFormCode, Common.Common._Department));
            var SubRecords = _iSubRecordService.GetList_SubRecordByMainRecordId(id).ToList();
            var SubList = CastList_SubRecordFull(SubRecords.ToList());
            ViewBag.SubList = SubList;
            ViewBag.TaskFormString = _TaskFormRepository.GetByCode(main.TaskFormCode).TaskFormName;
            ViewBag.StatusDirectionName = _StatusDirectionRepository.GetByCode(main.StatusDirectionCode).StatusDirectionName;
            ViewBag.StatusProcessName = _StatusProcessRepository.GetByCode(main.StatusProcessCode).StatusProcessName;
            ViewBag.realOnly = false;
            if (isAdmin == true)
            {
                #region admin
                var lstTaskDirection = _TaskDirectionRepository.GetListPermissionAdmin("MRF", OrderQueue);
                List<long> lst = new List<long>();
                foreach (var item in lstTaskDirection)
                    lst.Add(item.TaskDirectionId);
                if (main.TaskDirectionId != null && lst.Contains(Convert.ToInt64(main.TaskDirectionId)) == false)
                    return RedirectToAction("Index");
                #endregion

                _iMainRecordService.EditHoldManager(acc.AccountId, main.MainRecordId);
                if ((new List<string>() { "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) || (main.StatusDirectionCode == "SDC3" && TaskDirection.OrderQueue > OrderQueue) || (main.StatusDirectionCode == "SDC1" && TaskDirection.OrderQueue < OrderQueue))
                    ViewBag.realOnly = false;
                else
                    ViewBag.realOnly = true;
                ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC1", "SDC3", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
            }
            else
            {
                #region user
                if (main.TaskDirectionId != null && main.TaskDirectionId != 0 && TaskDirection != null && TaskDirection.OrderQueue != OrderQueue)
                    return RedirectToAction("Index");
                if (main.StatusDirectionCode.ToUpper() != "SDC6")
                {
                    if ((new List<string>() { "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) == true && (main.HoldByStaffId == null || acc.AccountId == main.HoldByStaffId))
                    { }
                    else
                    {
                        SetMessage("Access deny!", "");
                        return RedirectToAction("Index");
                    }
                    ViewBag.realOnly = true;
                }
                else
                {
                    if (main.HoldByStaffId == null)
                    {
                        _iMainRecordService.EditHoldStaff(acc.AccountId, main.MainRecordId);
                    }
                    else
                        if (main.HoldByStaffId != null && main.HoldByStaffId != acc.AccountId)
                        {
                            SetMessage("Access deny!", "");
                            return RedirectToAction("Index", "TTS");
                        }
                    ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
                }
                #endregion
            }
            var lstAccount = _iAccountService.GetAll();
            if (lstAccount == null)
                lstAccount = new List<Account>();
            ViewBag.listAccount = lstAccount;
            ViewBag.MainRecordId = id;



            AdsRepository _Ads = new AdsRepository();
            var ads = _Ads.GetByCode(main.FormId);
            ViewBag.ads = ads;
            ContractRepository _ContractRepository = new ContractRepository();
            ViewBag.Contract = _ContractRepository.GetById(Convert.ToInt64(ads.ContractId));
            AdsCustomerRepository _AdsCustomerRepository = new AdsCustomerRepository();
            BankRepository _BankRepository = new BankRepository();
            ViewBag.ListBank = _BankRepository.GetAll(false, true);

            var adsCustomer = _AdsCustomerRepository.GetById(Convert.ToInt64(ads.AdsCustomerId));
            ViewBag.adsCustomer = adsCustomer;
            AdsCustomerCardRepository _AdsCustomerCardRepository = new AdsCustomerCardRepository();
            ViewBag.AdsCustomerCard = _AdsCustomerCardRepository.GetByAdsCustomer(adsCustomer.AdsCustomerId);
            AdsTypeRepository _AdsTypeRepository = new AdsTypeRepository();
            AdsCategoryRepository _AdsCategoryRepository = new AdsCategoryRepository();
            AdsContentRepository _AdsContentRepository = new AdsContentRepository();
            ViewBag.AdsContent = _AdsContentRepository.GetById(Convert.ToInt64(ads.AdsContentId));
            HTTelecom.Domain.Core.DataContext.acs.AdsType adtype = new Domain.Core.DataContext.acs.AdsType();
            adtype = _AdsTypeRepository.GetById(Convert.ToInt64(ads.AdsTypeId));
            ViewBag.AdsType = adtype;
            ViewBag.AdsCategory = _AdsCategoryRepository.GetById(Convert.ToInt64(adtype.AdsCategoryId));

            CounterCardRepository _CounterCardRepository = new CounterCardRepository();
            ViewBag.CounterCard = _CounterCardRepository.GetById(Convert.ToInt64(ads.CounterCardId));
            return View(main);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditMRF(FormCollection formData)
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
                    return RedirectToAction("EditMRF", Convert.ToInt64(formData["MainRecordId"].ToString()));
                }
                if (formData["StatusDirectionCode"] == null || long.TryParse(formData["StatusDirectionCode"].ToString(), out temp) == false)
                {
                    return RedirectToAction("EditMRF", Convert.ToInt64(formData["MainRecordId"].ToString()));
                }
                if (formData["DescriptionSub"] == null || formData["DescriptionSub"].ToString().Length == 0 || formData["DescriptionSub"].ToString().Length > 100)
                {
                    return RedirectToAction("EditMRF", Convert.ToInt64(formData["MainRecordId"].ToString()));
                }
                #endregion
                var mainRecord = _MainRecordRepository.GetById(Convert.ToInt64(formData["MainRecordId"].ToString()));
                var SubRecord = _SubRecordRepository.GetList_SubRecordByMainRecordId(mainRecord.MainRecordId);
                int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(mainRecord.TaskFormCode, Common.Common._Department));
                foreach (var item in SubRecord)
                {
                    lst.AddRange(commons.ToSubRecordJson((JArray)JsonConvert.DeserializeObject(item.SubList)));
                }
                var SubLast = lst[lst.Count - 1];
                var StatusDirectionCode = _StatusDirectionRepository.GetById(Convert.ToInt64(formData["StatusDirectionCode"].ToString())).StatusDirectionCode;
                mainRecord.DateModified = DateTime.Now;

                var isAdmin = _iAccountService.IsAdmin(acc.AccountId, Common.Common._Department);
                if (isAdmin)
                {
                    #region admin
                    var lstTaskDirection = _TaskDirectionRepository.GetListPermissionAdmin("MRF", OrderQueue);
                    List<long> lstLog = new List<long>();
                    foreach (var item in lstTaskDirection)
                        lstLog.Add(item.TaskDirectionId);
                    if (mainRecord.TaskDirectionId != null && lstLog.Contains(Convert.ToInt64(mainRecord.TaskDirectionId)) == false)
                        return RedirectToAction("Index");
                    if ((new List<string>() { "SDC3", "SDC8", "SDC9" }).Contains(mainRecord.StatusDirectionCode) == false && ((new List<string>() { "SDC3", "SDC1", "SDC6" }).Contains(StatusDirectionCode)) == false)
                        return RedirectToAction("Index");
                    if (StatusDirectionCode == "SDC6")
                    {
                        mainRecord.TaskDirectionId = _TaskDirectionRepository.GetByIsValidAndOrderQuere("MRF", null, OrderQueue).TaskDirectionId;
                    }
                    else
                    {
                        TaskDirection taskDirection = _TaskDirectionRepository.GetBy("MRF", StatusDirectionCode, OrderQueue);
                        if (taskDirection == null)
                            return RedirectToAction("Index");
                        mainRecord.TaskDirectionId = taskDirection.TaskDirectionId;
                    }
                    mainRecord.HoldByStaffId = null;//Staff
                    mainRecord.HoldByManagerId = acc.AccountId;//Admin
                    mainRecord.StatusDirectionCode = StatusDirectionCode;
                    _MainRecordRepository.Edit(mainRecord);
                    #endregion

                }
                else
                {
                    #region user
                    var TaskDirection = new TaskDirection();
                    if (mainRecord.TaskDirectionId != null)
                        TaskDirection = _TaskDirectionRepository.GetById(mainRecord.TaskDirectionId);
                    if (mainRecord.TaskDirectionId != null && mainRecord.TaskDirectionId != 0 && TaskDirection != null && TaskDirection.OrderQueue != OrderQueue)
                        return RedirectToAction("Index");
                    if ((new List<string>() { "SDC6" }).Contains(mainRecord.StatusDirectionCode) == false)
                        return RedirectToAction("Index");
                    mainRecord.StatusDirectionCode = StatusDirectionCode;
                    mainRecord.HoldByStaffId = acc.AccountId;
                    _MainRecordRepository.Edit(mainRecord);
                    #endregion
                }
                //Action Submit in sub-record
                SubRecordJson jsonSub = new SubRecordJson(acc.AccountId.ToString(), formData["StatusDirectionCode"].ToString(), mainRecord.PriorityId.ToString(), lst.Count.ToString(), formData["DescriptionSub"].ToString(), DateTime.Now.ToString(), SubLast.DateHandIn, mainRecord.MainRecordId.ToString());
                lst.Add(jsonSub);
                string json = commons.ListSubRecordJsontoString(lst);
                _SubRecordRepository.EditSubLst(SubRecord[SubRecord.Count - 1].SubRecordId, json);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("EditMRF", Convert.ToInt64(formData["MainRecordId"].ToString()));
            }
        }
        #endregion

        #region 4. Action Method: OrderViewForm
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
            ViewBag.CreateByName = _Order.CreatedBy != null ? _AccountRepository.Get_AccountById(Convert.ToInt64(_Order.CreatedBy)).FullName : "";
            return View();
        }
        #endregion
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
        #region 2. Common Method: CastList_SubRecordFull
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
        #region 3. Common Method: GetList_PriorityDropDownList
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
        #region 4. Common Method: GetList_StatusProcessDropDownList
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
        #region 5. Common Method: SetMessage
        private void SetMessage(string p1, string p2)
        {
            if (TempData["Errors"] == null)
                TempData.Add("Errors", p1);
        }
        #endregion
        #endregion
    }
}
