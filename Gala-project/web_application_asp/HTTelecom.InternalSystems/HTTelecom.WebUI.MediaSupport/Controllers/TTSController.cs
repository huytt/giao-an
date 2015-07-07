using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using HTTelecom.Domain.Core.Repository.acs;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.tts;
using HTTelecom.WebUI.MediaSupport.Common;
using HTTelecom.WebUI.MediaSupport.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.MediaSupport.Controllers
{
    [SessionLoginFilter]
    public class TTSController : Controller
    {
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
            var TaskFormCode = "MRF";
            var lst = new List<MainRecord>();
            var lstPriority = _PriorityRepository.GetAll();
            var lstStatusProccess = _StatusProcessRepository.GetAll();
            var lstStatusDirection = _StatusDirectionRepository.GetAll();
            var lstAccount = _AccountRepository.GetAll();
            ViewBag.lstPriority = lstPriority;
            ViewBag.lstStatusProccess = lstStatusProccess;
            ViewBag.lstStatusDirection = lstStatusDirection;
            ViewBag.lstAccount = lstAccount;
            int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(Common.Common._Department));
            if (_AccountRepository.IsAdmin(acc.AccountId, Common.Common._Department))
            {
                lst = _MainRecordRepository.GetByFinanceAdmin(TaskFormCode, new List<string>() { "SDC1", "SDC2", "SDC3", "SDC4", "SDC5", "SDC6", "SDC7", "SDC8", "SDC9" }, acc.AccountId, new List<int?>() { OrderQueue - 1 }, OrderQueue, new List<int?>() { OrderQueue + 1 }, q, Convert.ToInt32(filter), Convert.ToInt32(tag));
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC1", "SDC2", "SDC3" }).Contains(n.StatusDirectionCode)).ToList();
            }
            else
            {
                ViewBag.ListStatusDirection = ((List<StatusDirection>)ViewBag.lstStatusDirection).Where(n => (new List<string>() { "SDC8", "SDC9" }).Contains(n.StatusDirectionCode)).ToList();
                lst = _MainRecordRepository.GetByFinance(TaskFormCode, new List<string>() { "SDC8", "SDC9" }, "SDC6", acc.AccountId, OrderQueue, q, Convert.ToInt32(filter), Convert.ToInt32(tag));
            }
            List<Common.SubRecordJson> lstSub = new List<Common.SubRecordJson>();
            foreach (var item in lst)
            {
                lstSub.AddRange(
                    CastList_SubRecordFull(_iSubRecordService.GetList_SubRecordByMainRecordId(item.MainRecordId).ToList())
                    );
            }
            ViewBag.SubList = lstSub;
            return View(lst.OrderByDescending(n => n.DateModified).ToList());
        }
        #endregion

        #region 2. Action Method: EditMRF
        public ActionResult EditMRF(long id)
        {
            MainRecord main = new MainRecord();
            this.GetList_TaskFormDropDownList(-1, false);
            this.GetList_PriorityDropDownList(-1, false);
            this.GetList_StatusProcessDropDownList(3, false);
            IStatusDirectionRepository _iStatusDirectionService = new StatusDirectionRepository();
            ViewBag.list_StatusDirection = _iStatusDirectionService.GetAll();
            AccountRepository _iAccountService = new AccountRepository();
            Account acc = (Account)HttpContext.Session["Account"];
            MainRecordRepository _iMainRecordService = new MainRecordRepository();
            TaskDirectionRepository _TaskDirectionRepository = new TaskDirectionRepository();
            main = _iMainRecordService.GetById(id);
            var isAdmin = _iAccountService.IsAdmin(acc.AccountId, Common.Common._Department);
            var TaskDirection = new TaskDirection();
            if (main.TaskDirectionId != null)
                TaskDirection = _TaskDirectionRepository.GetById(main.TaskDirectionId);
            int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(main.TaskFormCode,Common.Common._Department));
            if (isAdmin == true)
            {
                var lstTaskDirection = _TaskDirectionRepository.GetListPermissionAdmin("MRF", OrderQueue);
                List<long> lst = new List<long>();
                foreach (var item in lstTaskDirection)
                {
                    lst.Add(item.TaskDirectionId);
                }
                if (main.TaskDirectionId != null && lst.Contains(Convert.ToInt64(main.TaskDirectionId)) == false)
                {
                    return RedirectToAction("Index");
                }
            }
            else
                //Employee
                if (main.TaskDirectionId != null && main.TaskDirectionId != 0 && TaskDirection != null && TaskDirection.OrderQueue != OrderQueue)
                    return RedirectToAction("Index");
            ITaskFormRepository _TaskFormRepository = new TaskFormRepository();
            StatusDirectionRepository _StatusDirectionRepository = new StatusDirectionRepository();
            PriorityRepository _PriorityRepository = new PriorityRepository();
            StatusProcessRepository _StatusProcessRepository = new StatusProcessRepository();
            SubRecordRepository _iSubRecordService = new SubRecordRepository();
            var SubRecords = _iSubRecordService.GetList_SubRecordByMainRecordId(id).ToList();
            var SubList = CastList_SubRecordFull(SubRecords.ToList());
            ViewBag.SubList = SubList;
            ViewBag.TaskFormString = _TaskFormRepository.GetByCode(main.TaskFormCode).TaskFormName;
            ViewBag.StatusDirectionName = _StatusDirectionRepository.GetByCode(main.StatusDirectionCode).StatusDirectionName;
            ViewBag.StatusProcessName = _StatusProcessRepository.GetByCode(main.StatusProcessCode).StatusProcessName;
            //ViewBag.PriorityName = _PriorityRepository.GetById(Convert.ToInt64(main.PriorityId)).PriorityName;
            ViewBag.realOnly = false;
            if (isAdmin == true)
            {
                _iMainRecordService.EditHoldManager(acc.AccountId, main.MainRecordId);
                if ((new List<string>() { "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) || (main.StatusDirectionCode == "SDC3" && TaskDirection.OrderQueue > OrderQueue) || (main.StatusDirectionCode == "SDC1" && TaskDirection.OrderQueue < OrderQueue))
                {
                    ViewBag.realOnly = false;
                }
                else
                    ViewBag.realOnly = true;
                ViewBag.ListStatusDirection = new SelectList(((List<StatusDirection>)ViewBag.list_StatusDirection).Where(n => (new List<string>() { "SDC2", "SDC3", "SDC6" }).Contains(n.StatusDirectionCode)).ToList(), "StatusDirectionId", "StatusDirectionName");
            }
            else
            {
                if (main.StatusDirectionCode.ToUpper() != "SDC6")
                {
                    if ((new List<string>() { "SDC8", "SDC9" }).Contains(main.StatusDirectionCode) == true && (main.HoldByStaffId == null || acc.AccountId == main.HoldByStaffId))
                    {}
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
                int OrderQueue = Convert.ToInt32(_TaskDirectionRepository.GetOrderQueueByDepartment(mainRecord.TaskFormCode,Common.Common._Department));
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
                    var lstTaskDirection = _TaskDirectionRepository.GetListPermissionAdmin("MRF", OrderQueue);
                    List<long> lstLog = new List<long>();
                    foreach (var item in lstTaskDirection)
                    {
                        lstLog.Add(item.TaskDirectionId);
                    }
                    if (mainRecord.TaskDirectionId != null && lstLog.Contains(Convert.ToInt64(mainRecord.TaskDirectionId)) == false)
                    {
                        SetMessage("iuu", "");
                        return RedirectToAction("Index");
                    }
                    if ((new List<string>() { "SDC3", "SDC8", "SDC9" }).Contains(mainRecord.StatusDirectionCode) == false && ((new List<string>() { "SDC3", "SDC2", "SDC6" }).Contains(StatusDirectionCode)) == false)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var TaskDirection = new TaskDirection();
                    if (mainRecord.TaskDirectionId != null)
                        TaskDirection = _TaskDirectionRepository.GetById(mainRecord.TaskDirectionId);
                    if (mainRecord.TaskDirectionId != null && mainRecord.TaskDirectionId != 0 && TaskDirection != null && TaskDirection.OrderQueue != OrderQueue)
                        return RedirectToAction("Index");
                    if ((new List<string>() { "SDC6" }).Contains(mainRecord.StatusDirectionCode) == false)
                    {
                        return RedirectToAction("Index");
                    }
                }
                if (isAdmin)
                {
                    if (StatusDirectionCode == "SDC6")
                    {
                        mainRecord.TaskDirectionId = _TaskDirectionRepository.GetByIsValidAndOrderQuere("MRF",null, OrderQueue).TaskDirectionId;
                        mainRecord.HoldByStaffId = null;
                    }
                    else
                    {
                        TaskDirection taskDirection = _TaskDirectionRepository.GetBy("MRF",StatusDirectionCode, OrderQueue);
                        if (taskDirection == null)
                            return RedirectToAction("Index");
                        mainRecord.TaskDirectionId = taskDirection.TaskDirectionId;
                    }
                    mainRecord.HoldByManagerId = acc.AccountId;//Admin
                    mainRecord.HoldByStaffId = null;
                    mainRecord.StatusDirectionCode = StatusDirectionCode;
                    _MainRecordRepository.Edit(mainRecord);
                }
                else
                {
                    mainRecord.StatusDirectionCode = StatusDirectionCode;
                    mainRecord.HoldByStaffId = acc.AccountId;
                    _MainRecordRepository.Edit(mainRecord);
                }
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

        #region 5. Common Method: GetList_AreaProblemDropDownList
        private void SetMessage(string p1, string p2)
        {

        }
        #endregion
        #endregion
    }
}
