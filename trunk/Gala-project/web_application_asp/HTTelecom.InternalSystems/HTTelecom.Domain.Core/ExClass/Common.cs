using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace HTTelecom.Domain.Core.ExClass
{
    public class Common
    {
        private string mailServer = "hopthanhcompany.sp@gmail.com";
        private string passwordServer = "hopthanhsp123";
        string formMail = "hopthanhcompany.sp@gmail.com";
        string hostMail = "smtp.gmail.com";

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
}
