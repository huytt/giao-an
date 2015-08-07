using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.DataContext.sms;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.Repository.sms;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace HTTelecom.WebUI.AdminPanel.Controllers
{
    public class StoreSTSController : Controller
    {
        //
        // GET: /StoreSTS/
        public ActionResult Index(int? page, int? pageSizeSelected)
        {
            int pageNumDefault = 1;
            int pageSizeDefault = 50;
            StoreRepository _iStoreService = new StoreRepository();
            StoreStatisticRepository _iStoreStisticService = new StoreStatisticRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? (int)pageSizeDefault);
            ViewBag.page = page;
            ViewBag.pageSizeSelected = pageSize;
            List<StoreStatistic> list_store = new List<StoreStatistic>();

            var tmp = _iStoreStisticService.GetList_StoreStatisticAll();
           
            foreach (var item in tmp)
            {
                if (list_store.Where(_ => _.StoreId == item.StoreId).ToList().Count == 0)
                {
                    int ststscount = tmp.Where(x => x.StoreId == item.StoreId ).ToList().Sum(_ => _.Counter) ?? 0;
                    int ststscounterMember = tmp.Where(x => x.StoreId == item.StoreId ).ToList().Sum(_ => _.CounterMember) ?? 0;
                    StoreStatistic stst = new StoreStatistic();
                    stst.StoreStatisticId = item.StoreStatisticId;
                    stst.StoreId = item.StoreId;
                    stst.Counter = ststscount;
                    stst.CounterMember = ststscounterMember;
                    list_store.Add(stst);
                }
            }

            var lst_storePage = list_store.OrderByDescending(_=>_.Counter).ToPagedList(pageNum, pageSize);
            //ViewBag.lstStore = _iStoreService.GetList_StoreAll();
            ViewBag.SideBarMenu = "StoreIndex";
            return View(lst_storePage);
        }
        private string GetAccountByName(long accountId)
        {
            AccountRepository _AccountRepository = new AccountRepository();
            var acc = _AccountRepository.Get_AccountById(Convert.ToInt64(accountId));
            return acc != null ? acc.FullName : "N/A";
        }

        [HttpGet, WebMethod]
        public ActionResult GetStoreStatistic2(string datefrom, string type,long? storeId)
        {
            string result_csv = "[";
            string result_visitor = "[";
            string result_member = "[";
            List<string> Error = new List<string>();
            try
            {
                DateTime date_from = new DateTime();
                DateTime date_to = new DateTime();
                if (datefrom == null )
                {
                    date_from = DateTime.Now;
                }
                else
                {
                    date_from = DateTime.ParseExact(datefrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                //var result;
                StoreStatisticRepository _iStoreStatisticService = new StoreStatisticRepository();
                var lst_StoreSTS = _iStoreStatisticService.GetList_StoreStatisticAll();
                switch (type)
                {
                    case "hour":
                        Random r = new Random();
                      
                            if (date_from.Day == 9)
                            {

                                List<Common.StatisticJson> a = Common.Common.ToStatisticJson(@"[{TimeStart : ""0"",TimeEnd : ""1"",Counter: ""0""},{TimeStart : ""1"",TimeEnd : ""2"",Counter: ""0""},{TimeStart : ""2"",TimeEnd : ""3"",Counter: ""0""},{TimeStart : ""3"",TimeEnd : ""4"",Counter: ""0""},{TimeStart : ""4"",TimeEnd : ""5"",Counter: ""0""},{TimeStart : ""5"",TimeEnd : ""6"",Counter: ""0""},{TimeStart : ""6"",TimeEnd : ""7"",Counter: ""0""},{TimeStart : ""7"",TimeEnd : ""8"",Counter: ""0""},{TimeStart : ""8"",TimeEnd : ""9"",Counter: ""0""},{TimeStart : ""9"",TimeEnd : ""10"",Counter: ""0""},{TimeStart : ""10"",TimeEnd : ""11"",Counter: ""0""},{TimeStart : ""11"",TimeEnd : ""12"",Counter: ""0""},{TimeStart : ""12"",TimeEnd : ""13"",Counter: ""0""},{TimeStart : ""13"",TimeEnd : ""14"",Counter: ""12""},{TimeStart : ""14"",TimeEnd : ""15"",Counter: ""0""},{TimeStart : ""15"",TimeEnd : ""16"",Counter: ""0""},{TimeStart : ""16"",TimeEnd : ""17"",Counter: ""0""},{TimeStart : ""17"",TimeEnd : ""18"",Counter: ""0""},{TimeStart : ""18"",TimeEnd : ""19"",Counter: ""0""},{TimeStart : ""19"",TimeEnd : ""20"",Counter: ""0""},{TimeStart : ""20"",TimeEnd : ""21"",Counter: ""0""},{TimeStart : ""21"",TimeEnd : ""22"",Counter: ""0""},{TimeStart : ""22"",TimeEnd : ""23"",Counter: ""0""},{TimeStart : ""23"",TimeEnd : ""24"",Counter: ""0""}]");
                            }
                            for (int i = 0; i < 24; i++)
                            {
                                int ststscount = lst_StoreSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month && this.CheckStore((long)x.StoreId,storeId)).ToList().Sum(_ => int.Parse(
                                    Common.Common.ToStatisticJson(_.Content).Where(p => p.TimeStart == i.ToString()).ToList().Count > 0 ?
                                    Common.Common.ToStatisticJson(_.Content).Where(p => p.TimeStart == i.ToString()).ToList()[0].Counter : "0"));
                                int ststscountMember = lst_StoreSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month && this.CheckStore((long)x.StoreId, storeId)).ToList().Sum(_ => int.Parse(
                                    Common.Common.ToStatisticJson(_.Content).Where(p => p.TimeStart == i.ToString()).ToList().Count > 0 ?
                                    Common.Common.ToStatisticJson(_.Content).Where(p => p.TimeStart == i.ToString()).ToList()[0].CounterMember : "0"));
                                DateTime d_tmp = date_from;
                                TimeSpan ts = new TimeSpan(i, 0, 0);
                                d_tmp = d_tmp + ts;
                                string label = ((d_tmp.Ticks -(new DateTime(1970,1,1)).Ticks ) / 10000).ToString();

                                result_csv += "["+label.ToString() + "," + ststscount + "," + ststscountMember + "],";
                                result_visitor += "[" + label.ToString() + "," + ststscount + "],";
                                result_member += "[" + label.ToString() + "," + ststscountMember + "],";
                            }
                    
                        break;
                    case "date":
                        date_to = date_from.AddDays(40);
                        if (date_to > DateTime.Now)
                        {
                            date_to = DateTime.Now.AddDays(20);
                            date_from = DateTime.Now.AddDays(-20);
                        }
                        while (date_from != date_to && date_from < date_to)
                        {
                            int ststscount = lst_StoreSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month && this.CheckStore((long)x.StoreId, storeId)).ToList().Sum(_ => _.Counter) ?? 0;
                            int countermember = lst_StoreSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month && this.CheckStore((long)x.StoreId, storeId)).ToList().Sum(_ => _.CounterMember) ?? 0;
                            string label = ((date_from.Ticks - (new DateTime(1970, 1, 1)).Ticks) / 10000).ToString();

                            result_csv +="["+ label.ToString() + "," + ststscount + "," + countermember + "],";
                            result_visitor += "[" + label.ToString() + "," + ststscount + "],";
                            result_member += "[" + label.ToString() + "," + countermember + "],";
                            date_from = date_from.AddDays(1);
                        }
                        break;
                    case "month":
                       date_to = date_from.AddMonths(40);
                       if (date_to > DateTime.Now)
                       {
                           date_to = DateTime.Now.AddMonths(20);
                           date_from = DateTime.Now.AddMonths(-20);
                       }
                        while (date_from != date_to && date_from < date_to)
                        {
                            int ststscount = lst_StoreSTS.Where(x => x.Date.Value.Month == date_from.Month && x.Date.Value.Year == date_from.Year && this.CheckStore((long)x.StoreId, storeId)).ToList().Sum(_ => _.Counter) ?? 0;
                            int countermember = lst_StoreSTS.Where(x => x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month && this.CheckStore((long)x.StoreId, storeId)).ToList().Sum(_ => _.CounterMember) ?? 0;
                            string label = ((date_from.Ticks - (new DateTime(1970, 1, 1)).Ticks) / 10000).ToString();

                            result_csv += "[" + label.ToString() + "," + ststscount + "," + countermember + "],";
                            result_visitor += "[" + label.ToString() + "," + ststscount + "],";
                            result_member += "[" + label.ToString() + "," + countermember + "],";
                            date_from = date_from.AddMonths(1);
                        }
                        break;
                    case "year":
                        date_to = date_from.AddYears(20);
                       if (date_to > DateTime.Now)
                       {
                           date_to = DateTime.Now.AddYears(10);
                           date_from = DateTime.Now.AddYears(-10);
                       }
                           
                        while (date_from != date_to && date_from < date_to)
                        {
                            int ststscount = lst_StoreSTS.Where(x => x.Date.Value.Year == date_from.Year && this.CheckStore((long)x.StoreId, storeId)).ToList().Sum(_ => _.Counter) ?? 0;
                            int countermember = lst_StoreSTS.Where(x => x.Date.Value.Year == date_from.Year && this.CheckStore((long)x.StoreId, storeId)).ToList().Sum(_ => _.CounterMember) ?? 0;
                            string label = ((date_from.Ticks - (new DateTime(1970, 1, 1)).Ticks) / 10000).ToString();

                            result_csv += "[" + label.ToString() + "," + ststscount + "," + countermember + "],";
                            result_visitor += "[" + label.ToString() + "," + ststscount + "],";
                            result_member += "[" + label.ToString() + "," + countermember + "],";
                            date_from = date_from.AddYears(1);
                        }
                        break;
                }
                result_csv += "]";
                result_visitor += "]";
                result_member += "]";
                result_csv = result_csv.Replace("],]", "]]");
                result_visitor = result_visitor.Replace("],]", "]]");
                result_member = result_member.Replace("],]", "]]");
                return Json(new { success = true, csv = result_csv, v = result_visitor,m=result_member }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
            }
            return Json(new { success = false, Error = Error }, JsonRequestBehavior.AllowGet);
            //string a = "Day,Visits,Unique Visitors\n3/9/13,5691,4346\n3/10/13,5403,4112\n3/11/13,15574,11356\n3/12/13,16211,11876\n3/13/13,16427,11966\n3/14/13,16486,12086\n3/15/13,14737,10916\n3/16/13,5838,4507\n3/17/13,5542,4202\n3/18/13,15560,11523\n3/19/13,18940,14431\n3/20/13,16970,12599\n3/21/13,17580,13094\n3/22/13,17511,13234\n3/23/13,6601,5213\n3/24/13,6158,4806\n3/25/13,17353,12639\n3/26/13,17660,12768\n3/27/13,16921,12389\n3/28/13,15964,11686\n3/29/13,12028,8891\n3/30/13,5835,4513\n3/31/13,4799,3661\n4/1/13,13037,9503\n4/2/13,16976,12666\n4/3/13,17100,12635\n4/4/13,15701,11394\n4/5/13,14378,10530\n4/6/13,5889,4521\n4/7/13,6779,5109\n4/8/13,16068,11599\n";
            //return Json(new { csv = a}, JsonRequestBehavior.AllowGet);
        }       

        private bool CheckStore(long StoreId, long? StoreIdcheck)
        {
            if (StoreIdcheck == null || StoreId == StoreIdcheck) return true;
            return false;
        }

        //[HttpPost, WebMethod]
        //public ActionResult GetStoreStatistic(string datefrom, string dateto, string type)
        //{
        //    List<string> Error = new List<string>();
        //    try
        //    {
        //        DateTime date_from = DateTime.ParseExact(datefrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        DateTime date_to = DateTime.ParseExact(dateto, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        //var result;
        //        StoreStatisticRepository _iStoreStatisticService = new StoreStatisticRepository();
        //        var lst_StoreSTS = _iStoreStatisticService.GetList_StoreStatisticAll();
        //        //
        //        List<ChartDataObj> lstChartData = new List<ChartDataObj>();//dữ liệu đầu ra
        //        switch (type)
        //        {
        //            case "hour":
        //                Random r = new Random();
        //                while (date_from != date_to && date_from < date_to)
        //                {
        //                    if (date_from.Day == 9)
        //                    {

        //                        List<Common.StatisticJson> a = Common.Common.ToStatisticJson(@"[{TimeStart : ""0"",TimeEnd : ""1"",Counter: ""0""},{TimeStart : ""1"",TimeEnd : ""2"",Counter: ""0""},{TimeStart : ""2"",TimeEnd : ""3"",Counter: ""0""},{TimeStart : ""3"",TimeEnd : ""4"",Counter: ""0""},{TimeStart : ""4"",TimeEnd : ""5"",Counter: ""0""},{TimeStart : ""5"",TimeEnd : ""6"",Counter: ""0""},{TimeStart : ""6"",TimeEnd : ""7"",Counter: ""0""},{TimeStart : ""7"",TimeEnd : ""8"",Counter: ""0""},{TimeStart : ""8"",TimeEnd : ""9"",Counter: ""0""},{TimeStart : ""9"",TimeEnd : ""10"",Counter: ""0""},{TimeStart : ""10"",TimeEnd : ""11"",Counter: ""0""},{TimeStart : ""11"",TimeEnd : ""12"",Counter: ""0""},{TimeStart : ""12"",TimeEnd : ""13"",Counter: ""0""},{TimeStart : ""13"",TimeEnd : ""14"",Counter: ""12""},{TimeStart : ""14"",TimeEnd : ""15"",Counter: ""0""},{TimeStart : ""15"",TimeEnd : ""16"",Counter: ""0""},{TimeStart : ""16"",TimeEnd : ""17"",Counter: ""0""},{TimeStart : ""17"",TimeEnd : ""18"",Counter: ""0""},{TimeStart : ""18"",TimeEnd : ""19"",Counter: ""0""},{TimeStart : ""19"",TimeEnd : ""20"",Counter: ""0""},{TimeStart : ""20"",TimeEnd : ""21"",Counter: ""0""},{TimeStart : ""21"",TimeEnd : ""22"",Counter: ""0""},{TimeStart : ""22"",TimeEnd : ""23"",Counter: ""0""},{TimeStart : ""23"",TimeEnd : ""24"",Counter: ""0""}]");
        //                    }
        //                    for (int i = 0; i < 24; i++)
        //                    {
        //                        int ststscount = lst_StoreSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month).ToList().Sum(_ => int.Parse(
        //                            Common.Common.ToStatisticJson(_.Content).Where(p => p.TimeStart == i.ToString()).ToList().Count > 0 ?
        //                            Common.Common.ToStatisticJson(_.Content).Where(p => p.TimeStart == i.ToString()).ToList()[0].Counter : "0"));
        //                        DateTime d_tmp = date_from.AddDays(1);
        //                        TimeSpan ts = new TimeSpan(i, 0, 0);
        //                        d_tmp = d_tmp + ts;
        //                        string label = (d_tmp.Ticks / 10000).ToString();
        //                        int a = r.Next(100);
        //                        lstChartData.Add(new ChartDataObj(label, ststscount));
        //                    }

        //                    //Phần tử kế tiếp:
        //                    date_from = date_from.AddDays(1);
        //                }
        //                break;
        //            case "date":
        //                while (date_from != date_to && date_from < date_to)
        //                {
        //                    int ststscount = lst_StoreSTS.Where(x => x.Date.Value.Day == date_from.Day && x.Date.Value.Year == date_from.Year && x.Date.Value.Month == date_from.Month).ToList().Sum(_ => _.Counter) ?? 0;
        //                    //string label = date_from.Day != 1 ? date_from.Day.ToString() :date_from.Day.ToString() + "/" + date_from.Month.ToString();
        //                    string label = (date_from.Ticks / 10000).ToString();
        //                    lstChartData.Add(new ChartDataObj(label, ststscount));

        //                    date_from = date_from.AddDays(1);
        //                }
        //                break;
        //            case "month":
        //                while (date_from != date_to && date_from < date_to)
        //                {
        //                    int ststscount = lst_StoreSTS.Where(x => x.Date.Value.Month == date_from.Month && x.Date.Value.Year == date_from.Year).ToList().Sum(_ => _.Counter) ?? 0;
        //                    //string label = date_from.Day != 1 ? date_from.Day.ToString() :date_from.Day.ToString() + "/" + date_from.Month.ToString();
        //                    string label = (date_from.Ticks / 10000).ToString();
        //                    lstChartData.Add(new ChartDataObj(label, ststscount));

        //                    date_from = date_from.AddMonths(1);
        //                }
        //                break;
        //            case "year":
        //                while (date_from != date_to && date_from < date_to)
        //                {
        //                    int ststscount = lst_StoreSTS.Where(x => x.Date.Value.Year == date_from.Year).ToList().Sum(_ => _.Counter) ?? 0;
        //                    //string label = date_from.Day != 1 ? date_from.Day.ToString() :date_from.Day.ToString() + "/" + date_from.Month.ToString();
        //                    string label = (date_from.Ticks / 10000).ToString();
        //                    lstChartData.Add(new ChartDataObj(label, ststscount));

        //                    date_from = date_from.AddYears(1);
        //                }
        //                break;
        //        }

        //        return Json(new { success = true, Array = lstChartData }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Error.Add(ex.Message);
        //    }
        //    return Json(new { success = false, Error = Error }, JsonRequestBehavior.AllowGet);
        //}
    }

    //public class ChartDataObj
    //{
    //    public ChartDataObj(string label, long value)
    //    {
    //        Label = label;
    //        Value = value;
    //    }
    //    public string Label { get; set; }
    //    public long Value { get; set; }
    //}
}
