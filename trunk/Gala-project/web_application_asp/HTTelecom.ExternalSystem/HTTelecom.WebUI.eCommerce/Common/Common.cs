using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mail;

namespace HTTelecom.WebUI.eCommerce.Common
{
    public class Common
    {
        private string mailServer = "hopthanh.tester01@gmail.com";
        private string passwordServer = "hopthanh";
        string formMail = "hopthanh.tester01@gmail.com";
        string hostMail = "smtp.gmail.com";
        public string ProductToJson(List<ProductJson> lst)
        {
            return JsonConvert.SerializeObject(lst);
        }
        public static string _Department = "SG";
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
        public List<HTTelecom.Domain.Core.DataContext.mss.Product> GetRandom(List<HTTelecom.Domain.Core.DataContext.mss.Product> lst, int count)
        {
            if (count >= lst.Count)
                return lst;
            else
            {
                var rs = new List<HTTelecom.Domain.Core.DataContext.mss.Product>();
                Random rd = new Random();
                for (int i = 0; i < count; i++)
                {
                    var index = rd.Next(0, lst.Count - 1);
                    if (rs.Where(n => n.ProductId == lst[index].ProductId).ToList().Count == 0)
                    {
                        rs.Add(lst[index]);
                        lst.RemoveAt(index);
                    }
                    else
                        i--;
                }
                return rs;
            }
        }
        public List<HTTelecom.Domain.Core.DataContext.mss.ProductInMedia> GetRandom(List<HTTelecom.Domain.Core.DataContext.mss.ProductInMedia> lst, int count)
        {
            if (count >= lst.Count)
                return lst;
            else
            {
                var rs = new List<HTTelecom.Domain.Core.DataContext.mss.ProductInMedia>();
                Random rd = new Random();
                for (int i = 0; i < count; i++)
                {
                    var index = rd.Next(0, lst.Count - 1);
                    if (rs.Where(n => n.ProductId == lst[index].ProductId).ToList().Count == 0)
                    {
                        rs.Add(lst[index]);
                        lst.RemoveAt(index);
                    }
                    else
                        i--;
                }
                return rs;
            }
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

        public bool isValidDate(string Date, string Month, string Year)
        {
            DateTime x = DateTime.Now;
            if (DateTime.TryParse(Month + "/" + Date + "/" + Year, out x))
                return true;
            else
                return false;
        }

    }
    public class SessionObject
    {
        //Danh sach Add to cart, List<ProductId, QUantity, Size>// Neu PaymentProduct co Province or DistrictId > 0 thi load TotalWeght
        public List<Tuple<long, int, long>> ListCart { get; set; }

        //Danh sach san pham vua xem {max :10}
        public List<long> ListProduct { get; set; }
        //Buy Now: ProductId, QUantity, Size,ProvinceId, DistrictId:  -------  default: 0, 0.. 
        public List<Tuple<long, int, long, long, long>> PaymentProduct { get; set; }
        //Language : vi, zh , en
        public string lang { get; set; }
    }

    public class ProductObject
    {
        public ProductObject() { }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductComplexName { get; set; }
        public string ProductAlias { get; set; }
        public string ProductCode { get; set; }
        public string Link { get; set; }
        public string Color { get; set; }
        public string ProductStockCode { get; set; }
        public double MobileOnlinePrice { get; set; }
        public string MobileOnlinePrice_write { get; set; }
        public double PromotePrice { get; set; }
        public string PromotePrice_write { get; set; }

        public string Url { get; set; }
        public string MediaName { get; set; }
        public long StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreAlias { get; set; }

    }

    public class StoreObject
    {
        public StoreObject() { }
        public long StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string Alias { get; set; }
        public string Link { get; set; }
        public string MediaName { get; set; }
        public string Url { get; set; }
        public DateTime DateVerified { get; set; }
    }

    public class BrandObject
    {
        public BrandObject() { }
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        public long VisitCount { get; set; }
        public string Alias { get; set; }
        public string Link { get; set; }
        public string Logo_MediaName { get; set; }
        public string Logo_Url { get; set; }
        public string Banner_MediaName { get; set; }
        public string Banner_Url { get; set; }
    }

    public class CategoryObject
    {
        public CategoryObject() { }
        public string Alias { get; set; }
        public long CategoryId { get; set; }
        public int OrderNumber { get; set; }
        public string CategoryName { get; set; }
        public Nullable<long> VisitCount { get; set; }
        public string Link { get; set; }
        public Nullable<long> ParentCateId { get; set; }
        public Nullable<int> CateLevel { get; set; }
        public int ProductCount { get; set; }
        public string Logo_MediaName { get; set; }
        public string Logo_Url { get; set; }
        public string Banner_MediaName { get; set; }
        public string Banner_Url { get; set; }
    }




    public static class HTXoneServer
    {
        public static string Connect = "http://galagala.vn:8888/";
        //public static string Connect = "http://localhost:4938/";
    }
    public class ProductJson
    {
        public ProductJson() { }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductComplexName { get; set; }
        public string link { get; set; }
        public string ProductCode { get; set; }
        public string OnlineMobilePrice { get; set; }
        public string PromotePrice { get; set; }
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string VisitCount { get; set; }
        public string MediaUrl { get; set; }
        public string MediaName { get; set; }
        public ProductJson(string ProductId, string ProductName, string ProductComplexName, string link, string ProductCode, string OnlineMobilePrice
            , string PromotePrice, string StoreId, string StoreName, string StoreCode, string VisitCount, string MediaUrl, string MediaName)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.ProductComplexName = ProductComplexName;
            this.link = link;
            this.ProductCode = ProductCode;
            this.OnlineMobilePrice = OnlineMobilePrice;
            this.PromotePrice = PromotePrice;
            this.StoreId = StoreId;
            this.StoreName = StoreName;
            this.StoreCode = StoreCode;
            this.VisitCount = VisitCount;
            this.MediaUrl = MediaUrl;
            this.MediaName = MediaName;
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
    public static class HtmlHelper
    {
        // positive look behind for ">", one or more whitespace (non-greedy), positive lookahead for "<"
        private static readonly Regex InsignificantHtmlWhitespace = new Regex(@"(?<=>)\s+?(?=<)");

        // Known not to handle HTML comments or CDATA correctly, which we don't use.
        public static string RemoveInsignificantHtmlWhiteSpace(string html)
        {
            return InsignificantHtmlWhitespace.Replace(html, String.Empty).Trim();
        }
    }
}
