using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.tts;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HTTelecom.WebUI.Sale
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapHubs();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }

    public class AccountNotification
    {
        public string id { get; set; }
        public long AccountId { get; set; }
        public string StaffId { get; set; }
        public string token { get; set; }
        public int Type { get; set; }
    }
    public class SystemUser : Hub
    {
        public static List<AccountNotification> gList;
        private string[] lstConnect(List<AccountNotification> lst)
        {

            //var lst = gList.Where(n => n.AccountId == accountId).ToList();
            var lstArray = new string[lst.Count];
            for (int i = 0; i < lst.Count; i++)
                lstArray[i] = lst[i].id;
            return lstArray;
        }
        public void Login(long accountid, string token, int type, string staffid)
        {
            var id = Context.ConnectionId;
            if (gList == null)
                gList = new List<AccountNotification>();
            //Check Db token
            if (gList.Where(n => n.AccountId == accountid).ToList().Count == 0)
            {
                gList.Add(new AccountNotification() { id = id, AccountId = accountid, token = token, Type = type, StaffId = staffid });
                var mess = "";
                if (type == 1)
                    mess = "EmployeeId: " + accountid + " connected";

                else if (type == 2)
                    mess = "EmployeeId[Exception]: " + accountid + " connected";
                else
                    mess = "ManagerId : " + accountid + " connected";
                if (type == 3)
                {
                    Groups.Add(Context.ConnectionId, "manager");
                    Clients.Caller.listUser(gList.GroupBy(n => n.AccountId).Select(f => f.First()).Select(n => new { n.StaffId, n.AccountId }).ToList());
                }
                Clients.AllExcept(lstConnect(gList.Where(n => n.AccountId == accountid).ToList())).newEmployee(accountid, staffid, mess);

            }
            else
            {
                var target = gList.Where(n => n.AccountId == accountid).FirstOrDefault();
                if (target.id != id)
                    gList.Add(new AccountNotification() { id = id, AccountId = accountid, token = token, Type = type, StaffId = staffid });
                else
                    target.token = token;
                if (type == 3)
                {
                    Groups.Add(Context.ConnectionId, "manager");
                    Clients.Caller.listUser(gList.GroupBy(n => n.AccountId).Select(f => f.First()).Select(n => new { n.StaffId, n.AccountId }).ToList());
                }
            }
        }
        public void Connect(long accountId, string token, int type, string staffid)
        {
            var id = Context.ConnectionId;
            var target = gList.Where(n => n.AccountId == accountId && n.token == token && n.Type == type).FirstOrDefault();
            if (id != target.id)
                gList.Add(new AccountNotification() { id = id, AccountId = accountId, token = token, Type = type, StaffId = staffid });
        }
        public void SelectOrder(string mainId, string AccountId)
        {
            MainRecordRepository _MainRecordRepository = new MainRecordRepository();
            AccountRepository _AccountRepository = new AccountRepository();
            long o = 0;
            if (long.TryParse(mainId, out o) == false)
                return;
            var mr = _MainRecordRepository.GetById(Convert.ToInt64(mainId));
            if (mr.HoldByStaffId == null)
            {
                if (_MainRecordRepository.EditHoldStaff(Convert.ToInt64(AccountId), Convert.ToInt64(mainId)) == true)
                {
                    Clients.All.hideOrder(mainId, AccountId);
                    Clients.Group("manager").targetOrder(mainId, AccountId, AccountId.ToString());
                }
            }
        }
        public override Task OnDisconnected()
        {
            try
            {

                if (gList == null)
                    gList = new List<AccountNotification>();
                var item = gList.Where(n => n.id == Context.ConnectionId).FirstOrDefault();
                long id = item.AccountId;
                var contextId = item.id;
                gList.Remove(item);
                Groups.Remove(contextId, "manager");
                var lstItem = gList.Where(n => n.AccountId == id).ToList();
                if (lstItem.Count == 0)
                    Clients.Group("manager").onDisconnected(id);
            }
            catch
            {

            }

            return base.OnDisconnected();
        }
    }
}