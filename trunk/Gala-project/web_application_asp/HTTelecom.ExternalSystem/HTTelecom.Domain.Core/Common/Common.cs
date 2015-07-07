using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace HTTelecom.Domain.Core.Common
{
    public class Common
    {
        //hopthanhcompany.sp
        //hopthanhsp123
        private string mailServer = "hopthanhcompany.sp@gmail.com";
        private string passwordServer = "hopthanhsp123";
        string formMail = "hopthanhcompany.sp@gmail.com";
        string hostMail = "smtp.gmail.com";
        public static string ContentFielSub = "HolderId,StatusDirectionId,PriorityId,FollowingIndex,Description,DateHandIn,DateReceived,MainRecordId";
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
        public void SendMail(string header, string body, string email)
        {
            //nội dung mail
            string mail_service = mailServer;
            string mail_service_password = passwordServer;
            string mail_from = formMail;
            string mail_to = email;
            string mail_subject = header;
            string mail_body = body;
            #region load SmtpClient
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            #endregion
            client.Port = 587;
            client.Host = hostMail;
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(mail_service, mail_service_password);
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(mail_from, email, mail_subject, mail_body);
            mm.IsBodyHtml = true;
            mm.BodyEncoding = System.Text.UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //Send
            client.Send(mm);
        }
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
        public SubRecordJson(string HolderId, string StatusDirectionId, string PriorityId, string FollowingIndex, string Description, string DateHandIn, string DateReceived)
        {
            this.HolderId = HolderId;
            this.StatusDirectionId = StatusDirectionId;
            this.PriorityId = PriorityId;
            this.FollowingIndex = FollowingIndex;
            this.Description = Description;
            this.DateReceived = DateReceived;
            this.DateHandIn = DateHandIn;
        }
    }
}
