using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace HTTelecom.Domain.Core.ExClass
{
    public class Generates
    {
        public static string generateAlias(string content)
        {
            content = content.ToLower();
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = content.Normalize(NormalizationForm.FormD).Trim();

            string kq = regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd')
                        .Replace('\u0110', 'D')
                        .Replace(",", "-")
                        .Replace("*", "-")
                        .Replace(".", "-")
                        .Replace("!", "")
                        .Replace("'", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace(";", "-")
                        .Replace("/", "-")
                        .Replace("%", "ptram")
                        .Replace("&", "va")
                        .Replace("?", "")
                        .Replace('"', '-')
                        .Replace(' ', '-');
            while (kq.Contains("--"))
                kq = kq.Replace("--", "-");
            while (kq.Contains("  "))
                kq = kq.Replace("  ", " ");
            return kq;
        }

        public static string ConvertUnicodeToASCII(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            //text = text.Replace(" ", "-"); //Comment lại để không đưa khoảng trắng thành ký tự -

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static bool SendEmail(string userName, string Password, string host, int port, string subject, string body, string email)
        {
            try
            {
                using (var smtpclient = new SmtpClient())
                {
                    smtpclient.EnableSsl = true;
                    smtpclient.Host = host;
                    smtpclient.Port = port;
                    smtpclient.UseDefaultCredentials = true;
                    smtpclient.Credentials = new NetworkCredential(userName, Password);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(userName),
                        Subject = subject,
                        Body = body,
                        Priority = MailPriority.Normal,
                    };

                    msg.To.Add(email);

                    smtpclient.Send(msg);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public string generateStoreId(long id)
        {
            return @String.Format("S{0:000000}", id);
        }
        public string generateShowProductId(long id)
        {
            return @String.Format("P{0:00000000}", id);
        }
        public string generateProductStockId(long StoreId, long ProductId)
        {
            return @String.Format("P-{0:000000}", StoreId) + "-" + @String.Format("{0:00000000}", ProductId);
        }
        public static string HTXoneServer()
        {
            return "http://ht-xone.net:8005";
        }
        public static string HTXoneLocalhost()
        {
            return "http://localhost:6348";
        }
        public static CultureInfo SetLanguage(string culture)
        {
            CultureInfo ci = new CultureInfo(culture);
            return ci;
        }
    }
}
