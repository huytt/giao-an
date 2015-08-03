using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Encoder = System.Drawing.Imaging.Encoder;

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

    public static class Public
    {
        public static string Decode(this string input)
        {
            return HttpUtility.UrlDecode(input);
        }
        public static bool checkDeparment(long EmployeeId, long VendorId)
        {
            try
            {
                //Load het Cac Vendor cua Employee... 
                //Khi Create StoreId
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool checkDeparment_Store(long EmployeeId, long StoreId)
        {
            try
            {
                //Load het Cac Vendor cua Employee... 
                //Khi Create StoreId
                return true;
            }
            catch
            {
                return false;
            }
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

    public class ImageUpload
    {
        // set default size here
        public int Width { get; set; }

        public int Height { get; set; }

        // folder for the upload, you can put this in the web.config
        private readonly string UploadPath = "~/Images/Items/";

        public ImageResult RenameUploadFile(HttpPostedFileBase file, Int32 counter = 0)
        {
            var fileName = Path.GetFileName(file.FileName);

            string prepend = "item_";
            string finalFileName = prepend + ((counter).ToString()) + "_" + fileName;
            if (System.IO.File.Exists
                (HttpContext.Current.Request.MapPath(UploadPath + finalFileName)))
            {
                //file exists => add country try again
                return RenameUploadFile(file, ++counter);
            }
            //file doesn't exist, upload item but validate first
            return UploadFile(file, finalFileName);
        }

        private ImageResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            ImageResult imageResult = new ImageResult { Success = true, ErrorMessage = null };

            var path =
          Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);
            string extension = Path.GetExtension(file.FileName);

            //make sure the file is valid
            if (!ValidateExtension(extension))
            {
                imageResult.Success = false;
                imageResult.ErrorMessage = "Invalid Extension";
                return imageResult;
            }

            try
            {
                file.SaveAs(path);

                Image imgOriginal = Image.FromFile(path);

                //pass in whatever value you want 
                //Image imgActual = Scale(imgOriginal);
                //imgOriginal.Dispose();
                //imgActual.Save(path);
                //imgActual.Dispose();

                imageResult.ImageName = fileName;

                return imageResult;
            }
            catch (Exception ex)
            {
                // you might NOT want to show the exception error for the user
                // this is generaly logging or testing

                imageResult.Success = false;
                imageResult.ErrorMessage = ex.Message;
                return imageResult;
            }
        }

        private bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        private Image Scale(Image imgPhoto, int maxWidth, int maxHeight, int quality, string filePath)
        {
            float originalWidth = imgPhoto.Width;
            float originalHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // To preserve the aspect ratio
            float ratioX = maxWidth / originalWidth;
            float ratioY = maxHeight / originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Create an Encoder object for the Quality parameter.
            Encoder encoder = Encoder.Quality;

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(filePath, imageCodecInfo, encoderParameters);

            //// force resize, might distort image
            //if (Width != 0 && Height != 0)
            //{
            //    destWidth = Width;
            //    destHeight = Height;
            //}
            //// change size proportially depending on width or height
            //else if (Height != 0)
            //{
            //    destWidth = (float)(Height * originalWidth) / originalHeight;
            //    destHeight = Height;
            //}
            //else
            //{
            //    destWidth = Width;
            //    destHeight = (float)(originalHeight * Width / originalWidth);
            //}

            //Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
            //                            PixelFormat.Format16bppRgb555);
            //bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //grPhoto.DrawImage(imgPhoto,
            //    new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
            //    new Rectangle(sourceX, sourceY, (int)originalWidth, (int)originalHeight),
            //    GraphicsUnit.Pixel);

            //grPhoto.Dispose();

            return null;
        }
        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
    }
    public class ImageResult
    {
        public bool Success { get; set; }
        public string ImageName { get; set; }
        public string ErrorMessage { get; set; }

    }

    #region Class for Email
    public class Template
    {
        public string header { get; set; }
        public string footer { get; set; }
        public long id { get; set; }
    }
    public class EmailContent
    {
        public long TemplateId { get; set; }
        public string Content { get; set; }
        public long Id { get; set; }
    }
    public class Element
    {
        public long ElementId { get; set; }
        public long TemplateId { get; set; }
    }
    #endregion
}