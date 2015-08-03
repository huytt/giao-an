using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.ops;
using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.Repository.ops;
using HTTelecom.Domain.Core.Repository.tts;
using HTTelecom.WebUI.AdminPanel.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.AdminPanel.Controllers
{
    [SessionLoginFilter]
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index(int? page, int? filter, int? tag, string q)
        {

            #region Load
            IMainRecordRepository _MainRecordRepository = new MainRecordRepository();
            IPriorityRepository _PriorityRepository = new PriorityRepository();
            IStatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            IStatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            TaskFormRepository _TaskFormRepository = new TaskFormRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            #endregion

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

            var TaskFormCode = new List<string>() { "COF-2", "COF-3", "MRF", "COF-1" };
            var lst = new List<MainRecord>();
            lst = _MainRecordRepository.GetAll().ToList();
            var lstPriority = _PriorityRepository.GetAll();
            var lstStatusProccess = _StatusProcessRepository.GetAll();
            var lstStatusDirection = _StatusDirectionRepository.GetAll();
            var lstAccount = _AccountRepository.GetAll();
            ViewBag.lstPriority = lstPriority;
            ViewBag.lstStatusProccess = lstStatusProccess;
            ViewBag.lstStatusDirection = lstStatusDirection;
            ViewBag.lstAccount = lstAccount;
            //int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(Common.Common._Department));
            List<Common.SubRecordJson> lstSub = new List<Common.SubRecordJson>();
            List<Order> lstOrder = new List<Order>();
            lstOrder = _OrderRepository.GetAll();
            foreach (var item in lst)
                lstSub.AddRange(CastList_SubRecordFull(_iSubRecordService.GetList_SubRecordByMainRecordId(item.MainRecordId).ToList()));
            ViewBag.SubList = lstSub;
            ViewBag.TaskDirection = _TaskDirectionRepository.GetAll().ToList();
            ViewBag.TaskForms = _TaskFormRepository.GetAll().ToList();
            ViewBag.ListOrder = lstOrder;
            return View(lst.Distinct().OrderByDescending(n => n.DateModified).ToList());
        }
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
            DistrictRepository _DistrictRepository = new DistrictRepository();
            ProvinceRepository _ProvinceRepository = new ProvinceRepository();
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
            ViewBag.ListBank = _BankRepository.GetAll(false, true);
            ViewBag.ListSize = _SizeRepository.GetList_SizeAll();
            ViewBag.CreateByName = _Order.CreatedBy != null ? _AccountRepository.Get_AccountById(Convert.ToInt64(_Order.CreatedBy)).FullName : "";
            ViewBag.District = _DistrictRepository.GetById(_Order.ShipToDistrict);
            ViewBag.Province = _ProvinceRepository.GetById(_Order.ShipToCity);
            return View();
        }
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
            DistrictRepository _DistrictRepository = new DistrictRepository();
            ProvinceRepository _ProvinceRepository = new ProvinceRepository();



            #endregion
            ViewBag.list_StatusDirection = _iStatusDirectionService.GetAll();
            main = _iMainRecordService.GetById(id);

            //var isAdmin = _iAccountService.IsAdmin(acc.AccountId, Common.Common._Department);
            var TaskDirection = new TaskDirection();
            if (main.TaskDirectionId != null && main.TaskDirectionId != 0)
                TaskDirection = _TaskDirectionRepository.GetById(main.TaskDirectionId);
            //int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(main.TaskFormCode,Common.Common._Department));
            var TaskFormCode = new List<string>() { "COF-2", "COF-3", "MRF", "COF-1" };
            //var lstOrderQueue = _TaskDirectionRepository.GetListOrderQueueByDepartment(Common.Common._Department, TaskFormCode);
            //if (isAdmin == true)
            //{
            //    var lstTaskDirection = new List<TaskDirection>();
            //    //foreach (var item in TaskFormCode)
            //    //    lstTaskDirection.AddRange(_TaskDirectionRepository.GetListPermissionAdmin(item, OrderQueue));
            //    foreach (var item in lstOrderQueue)
            //        lstTaskDirection.AddRange(_TaskDirectionRepository.GetListPermissionAdmin(item.Item2, item.Item1));
            //    List<long> lst = new List<long>();
            //    foreach (var item in lstTaskDirection)
            //        lst.Add(item.TaskDirectionId);
            //    if (main.TaskDirectionId != null && main.TaskDirectionId != 0 && lst.Contains(Convert.ToInt64(main.TaskDirectionId)) == false)
            //        return RedirectToAction("Index");
            //}
            //else
            //if (main.TaskDirectionId != null && main.TaskDirectionId != 0 && TaskDirection != null && lstOrderQueue.Where(n => n.Item1 == TaskDirection.OrderQueue).ToList().Count == 0)
            //    return RedirectToAction("Index");
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

            if (_Order == null) { return RedirectToAction("Index"); }
            var _taskFormCode = _Order.PaymentTypeCode == "PTC-1" ? "COF-1" : _Order.PaymentTypeCode == "PTC-2" ? "COF-2" : "COF-3";
            var OrderQueue = 0;
            //var lstOrQuere = lstOrderQueue.Where(n => n.Item2 == main.TaskFormCode).ToList();
            var taskDirection = _TaskDirectionRepository.GetById(main.TaskDirectionId);
            //if ((taskDirection.StatusDirectionCode == "SDC1" && taskDirection.IsValid != null && taskDirection.IsValid == true && taskDirection.OrderQueue == lstOrQuere[1].Item1 - 1) || taskDirection.OrderQueue == lstOrQuere[1].Item1)
            //    OrderQueue = lstOrQuere[1].Item1;
            //else OrderQueue = lstOrQuere[0].Item1;
            //if (isAdmin == true)
            //{
            //    if (main.HoldByManagerId == null)
            //        _iMainRecordService.EditHoldManager(acc.AccountId, main.MainRecordId);
            //    if (main.TaskDirectionId == null)
            //        _iMainRecordService.EditTaskDirection(_TaskDirectionRepository.GetByIsValidAndOrderQuere(_taskFormCode, null, OrderQueue).TaskDirectionId, main.MainRecordId);
            //    if ((new List<string>() { "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) || (main.StatusDirectionCode == "SDC3" && TaskDirection.OrderQueue > OrderQueue) || (main.StatusDirectionCode == "SDC1" && TaskDirection.OrderQueue < OrderQueue))
            //        ViewBag.realOnly = false;
            //    else
            //        ViewBag.realOnly = true;
            //    if (taskDirection.OrderQueue == lstOrQuere[1].Item1 || (taskDirection.StatusDirectionCode == "SDC1" && taskDirection.IsValid != null && taskDirection.IsValid == true && taskDirection.OrderQueue == lstOrQuere[1].Item1 - 1))
            //        ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC2", "SDC3", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
            //    else
            //        if (taskDirection.OrderQueue != null && taskDirection.OrderQueue == 1)
            //            ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC1", "SDC5", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
            //        else ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC1", "SDC3", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
            //}
            //else
            //{
            //    if (main.StatusDirectionCode.ToUpper() != "SDC6")
            //    {
            //        if ((new List<string>() { "SDC7", "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) == true && (main.HoldByStaffId == null || acc.AccountId == main.HoldByStaffId))
            //        { }
            //        else
            //        {
            //            SetMessage("Access deny!", "");
            //            return RedirectToAction("Index");
            //        }
            //        ViewBag.realOnly = true;
            //    }
            //    else
            //    {
            //        if (main.HoldByStaffId == null)
            //            _iMainRecordService.EditHoldStaff(acc.AccountId, main.MainRecordId);
            //        else
            //            if (main.HoldByStaffId != null && main.HoldByStaffId != acc.AccountId)
            //            {
            //                SetMessage("Access deny!", "");
            //                return RedirectToAction("Index", "TTS");
            //            }
            //        ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
            //    }
            //}
            ViewBag.realOnly = true;
            var lstAccount = _iAccountService.GetAll();
            if (lstAccount == null)
                lstAccount = new List<Account>();
            ViewBag.listAccount = lstAccount;
            ViewBag.MainRecordId = id;
            return View(main);
        }
        #endregion


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
    }
}
