using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.AdminPanel.Common
{
    public class Common
    {
        //public static string _Department = "CS";
        #region StatisticJson
        public static List<StatisticJson> ToStatisticJson(JArray Json)
        {
            List<StatisticJson> items = Json.Select(x => new StatisticJson
            {
                Counter = (string)x["Counter"],
                CounterMember = (string)x["CounterMember"],
                TimeEnd = (string)x["TimeEnd"],
                TimeStart = (string)x["TimeStart"]
            }).ToList();
            return items;
        }

        public static List<StatisticJson> ToStatisticJson(string _content)
        {
            if (_content == "" || _content==null)
                return new List<StatisticJson>();
            List<StatisticJson> items = ((JArray)JsonConvert.DeserializeObject(_content)).Select(x => new StatisticJson
            {
                Counter = (string)x["Counter"],
                CounterMember = (string)x["CounterMember"],
                TimeEnd = (string)x["TimeEnd"] == "0" ? "24" : (string)x["TimeEnd"],
                TimeStart = (string)x["TimeStart"] == "0" ? "24" : (string)x["TimeStart"]
            }).ToList();
            return items;
        }
        public string StatisticJsontoString(StatisticJson Json)
        {
            return JsonConvert.SerializeObject(Json);
        }
        public string ListStatisticJsontoString(List<StatisticJson> lst)
        {
            return JsonConvert.SerializeObject(lst);
        }
        #endregion
        #region SubRecord
        public List<SubRecordJson> ToSubRecordJson(JArray Json)
        {
            List<SubRecordJson> items = Json.Select(x => new SubRecordJson
            {
                HolderId = (string)x["HolderId"],
                StatusDirectionId = (string)x["StatusDirectionId"],
                PriorityId = (string)x["PriorityId"],
                FollowingIndex = (string)x["FollowingIndex"],
                Description = (string)x["Description"],
                DateReceived = (string)x["DateReceived"],
                DateHandIn = (string)x["DateHandIn"],
                MainRecordId = (string)x["MainRecordId"]
            }).ToList();
            return items;
        }
        public string SubRecordJsontoString(SubRecordJson Json)
        {
            return JsonConvert.SerializeObject(Json);
        }
        public string ListSubRecordJsontoString(List<SubRecordJson> lst)
        {
            return JsonConvert.SerializeObject(lst);
        }
        #endregion
    }
    public class StatisticJson
    {
        public StatisticJson() { }
        public StatisticJson(string TimeStart, string TimeEnd, string Counter,string CounterMember)
        {
            this.Counter = Counter;
            this.TimeEnd = TimeEnd;
            this.TimeStart = TimeStart;
            this.CounterMember = CounterMember;
        }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Counter { get; set; }
        public string CounterMember { get; set; }
    }
    public class SubRecordJson
    {
        public SubRecordJson() { }
        public string HolderId { get; set; }
        public string StatusDirectionId { get; set; }
        public string PriorityId { get; set; }
        public string FollowingIndex { get; set; }
        public string Description { get; set; }
        public string DateReceived { get; set; }
        public string DateHandIn { get; set; }
        public string MainRecordId { get; set; }
        public SubRecordJson(string HolderId, string StatusDirectionId, string PriorityId, string FollowingIndex, string Description, string DateHandIn, string DateReceived, string MainRecordId)
        {
            this.HolderId = HolderId;
            this.StatusDirectionId = StatusDirectionId;
            this.PriorityId = PriorityId;
            this.FollowingIndex = FollowingIndex;
            this.Description = Description;
            this.DateReceived = DateReceived;
            this.DateHandIn = DateHandIn;
            this.MainRecordId = MainRecordId;
        }
    }
}