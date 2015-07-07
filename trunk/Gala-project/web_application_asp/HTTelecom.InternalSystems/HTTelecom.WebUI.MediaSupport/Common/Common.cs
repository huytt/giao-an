using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace HTTelecom.WebUI.MediaSupport.Common
{
    public class Common
    {
        public static string _Department = "MS";
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

    #region Huycc
    public class CommonClass
    {

        #region FormatFullDateTime
        /// <summary>
        /// Huycc
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatFullDateTime(DateTime date)
        {
            string fullDate = "";
            string day, month, year, hour, minute, second = "";

            day = date.Day.ToString();

            if (date.Day < 10)
                day = "0" + day;

            month = date.Month.ToString();
            if (date.Month < 10)
                month = "0" + month;

            year = date.Year.ToString();

            hour = date.Hour.ToString();
            if (date.Hour < 10)
                hour = "0" + hour;

            minute = date.Minute.ToString();
            if (date.Minute < 10)
                minute = "0" + minute;

            second = date.Second.ToString();
            if (date.Second < 10)
                second = "0" + second;

            fullDate = day + "/" + month + "/" + year + "  " + hour + ":" + minute + ":" + second;

            return fullDate;
        }
        #endregion

        #region GenerateStringHashToken
        /// <summary>
        /// GenerateString : 8 chars random: bao gồm cả chữ hoa, thường, kí tự đặc biệt, số -
        /// Ex: Bd@1Ac#2
        /// </summary>
        /// <returns></returns>
        public static string GenerateStringHashToken()
        {
            //ex: Bd@1Ac#2

            string strGenerate = "";
            string upper = "";
            string lower = "";
            string special = "";
            string number = "";

            //Round 1.
            upper = GetRandomAlphabelLetter(0).ToString();   //Upper
            lower = GetRandomAlphabelLetter(1).ToString();   //Lower
            special = GetRandomSpecialLetter().ToString();   //Special
            number = GetRandomNumber().ToString();           //Number

            strGenerate += upper + lower + special + number;

            //Round 2.
            upper = "";
            lower = "";
            special = "";
            number = "";

            upper = GetRandomAlphabelLetter(0).ToString();   //Upper
            lower = GetRandomAlphabelLetter(1).ToString();   //Lower
            special = GetRandomSpecialLetter().ToString();   //Special
            number = GetRandomNumber().ToString();           //Number

            strGenerate += upper + lower + special + number;

            return strGenerate;
        }

        /// <summary>
        /// GetRandomLetter
        /// </summary>
        /// <param name="letterType">int - 0: Upper | 1: Lower</param>
        /// <returns>char</returns>
        private static char GetRandomAlphabelLetter(int letterType)
        {

            // ... Between 'a' and 'z' inclusize.

            Random random = new Random();
            int num = random.Next(0, 26); // Zero to 25
            char let;
            if (letterType == 0)
                let = (char)('A' + num); //Upper
            else
                let = (char)('a' + num); //Lower
            return let;
        }


        /// <summary>
        /// GetRandomSpecialLetter
        /// </summary>
        /// <returns>char - Special Letter</returns>
        private static char GetRandomSpecialLetter()
        {
            char[] arrChar = { '!', '"', '#', '$', '%', '&', '(', ')', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '^', '_', '~', '{', '|', '}' };

            int length = arrChar.Length;

            Random random = new Random();
            int num = random.Next(0, length); // Zero to length

            if (num == length)
                return arrChar[num - 1];
            return arrChar[num];
        }

        /// <summary>
        /// GetRandomNumber
        /// </summary>
        /// <returns>int - 0 to 9</returns>
        private static int GetRandomNumber()
        {
            Random random = new Random();
            return random.Next(0, 9); // Zero to 9
        }
        #endregion

        #region GenerateStringSaltToken
        /// <summary>
        /// GenerateString : 8 chars random: bao gồm cả chữ hoa, thường, kí tự đặc biệt, số -
        /// Ex: Bd@1Ac#2
        /// </summary>
        /// <returns></returns>
        public static string GenerateStringSaltToken()
        {
            //ex: dB@1cA#2

            string strGenerate = "";
            string upper = "";
            string lower = "";
            string special = "";
            string number = "";

            //Round 1.
            upper = GetRandomAlphabelLetter(0).ToString();   //Upper
            lower = GetRandomAlphabelLetter(1).ToString();   //Lower
            special = GetRandomSpecialLetter().ToString();   //Special
            number = GetRandomNumber().ToString();           //Number

            strGenerate += lower + upper + special + number;

            //Round 2.
            upper = "";
            lower = "";
            special = "";
            number = "";

            upper = GetRandomAlphabelLetter(0).ToString();   //Upper
            lower = GetRandomAlphabelLetter(1).ToString();   //Lower
            special = GetRandomSpecialLetter().ToString();   //Special
            number = GetRandomNumber().ToString();           //Number

            strGenerate += lower + upper + special + number;

            return strGenerate;
        }
        #endregion

        #region SendEmail
        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="to">string - Email address of reciever</param>
        /// <param name="cc">string - Cc</param>
        /// <param name="bcc">string - Bcc</param>
        /// <param name="subject">string - Subject of Email</param>
        /// <param name="body">string - Body of Email</param>
        /// <param name="fileAttachment">string - fileAttachment</param>
        public static void SendEmail(string to, string cc, string bcc, string subject, string body, string fileAttachment)
        {
            try
            {
                //////////////////////////////////////////////////
                string smtpAddress = ConfigurationManager.AppSettings["SmtpAddress"].ToString();
                int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"]);
                bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
                string emailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                //////////////////////////////////////////////////

                //string emailTo = "chenhchihuyckc@gmail.com, hopthanh17112014@gmail.com";
                //string subject = "Hello";
                //string body = "Hello, I'm just writing this to say Hi!";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(to);
                    mail.CC.Add(cc);
                    mail.Bcc.Add(bcc);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.Attachments.Add(new Attachment(fileAttachment));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="to">string - Email address of reciever</param>
        /// <param name="cc">string - Cc</param>
        /// <param name="bcc">string - Bcc</param>
        /// <param name="subject">string - Subject of Email</param>
        /// <param name="body">string - Body of Email</param>
        public static void SendEmail(string to, string cc, string bcc, string subject, string body)
        {
            try
            {
                //////////////////////////////////////////////////
                string smtpAddress = ConfigurationManager.AppSettings["SmtpAddress"].ToString();
                int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"]);
                bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
                string emailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                //////////////////////////////////////////////////

                //string emailTo = "chenhchihuyckc@gmail.com, hopthanh17112014@gmail.com";
                //string subject = "Hello";
                //string body = "Hello, I'm just writing this to say Hi!";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(to);
                    mail.CC.Add(cc);
                    mail.Bcc.Add(bcc);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="to">string - Email address of reciever</param>
        /// <param name="cc">string - Cc</param>
        /// <param name="subject">string - Subject of Email</param>
        /// <param name="body">string - Body of Email</param>
        public static void SendEmail(string to, string cc, string subject, string body)
        {
            try
            {
                //////////////////////////////////////////////////
                string smtpAddress = ConfigurationManager.AppSettings["SmtpAddress"].ToString();
                int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"]);
                bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
                string emailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                //////////////////////////////////////////////////

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(to);
                    mail.CC.Add(cc);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="to">string - Email address of reciever</param>
        /// <param name="subject">string - Subject of Email</param>
        /// <param name="body">string - Body of Email</param>
        public static void SendEmail(string to, string subject, string body)
        {
            try
            {
                //////////////////////////////////////////////////
                string smtpAddress = ConfigurationManager.AppSettings["SmtpAddress"].ToString();
                int portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"]);
                bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
                string emailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                //////////////////////////////////////////////////

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        #endregion
    }
    #endregion
}