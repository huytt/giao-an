using HTTelecom.Domain.Core.Common;
using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class VendorRepository
    {
        public List<Vendor> GetAll(bool IsDeleted)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Vendor.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Vendor>();
            }
        }

        public string GetPassChekingById(long VendorId)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var acc = _data.Vendor.Find(VendorId);
                return acc.Token;
            }
            catch
            {
                return "";
            }
        }

        public Vendor GetById(long VendorId)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Vendor.Find(VendorId);
            }
            catch
            {
                return null;
            }
        }
        public string CreateSecurityToken(long VendorId)
        {
            try
            {
                if (VendorId == 0)
                    return "";
                CIS_DBEntities _data = new CIS_DBEntities();
                var Vendor = _data.Vendor.Where(n => n.VendorId == VendorId).FirstOrDefault();
                string se_code = DateTime.Now.Ticks.ToString();
                Vendor.Token = se_code.Substring(se_code.Length - 5, 5);
                _data.SaveChanges();
                return Vendor.Token;
            }
            catch
            {
                return "";
            }
        }
        public bool RemoveSecurityToken(long VendorId)
        {
            try
            {
                if (VendorId == 0)
                    return false;
                CIS_DBEntities _data = new CIS_DBEntities();
                var Vendor = _data.Vendor.Where(n => n.VendorId == VendorId).FirstOrDefault();

                Vendor.Token = "";
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool isActiveVendor(long VendorId, bool _isActive)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                Vendor Vendor = _data.Vendor.Where(n => n.VendorId == VendorId).FirstOrDefault();
                Vendor.Token = "";
                Vendor.IsActive = _isActive;

                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendMail_Vendor(Vendor _Vendor, string _url_verify, string _activeString, int _action)
        {
            /*
               _action: cho biết hành động của việc send mail này là gì.
               => Ta có các hành động như sau:
                    * 1: Send mail kích hoạt đăng ký.
                    * 2: Send mail thông báo đổi password thành công. 
            */
            switch (_action)
            {
                case 1://kích hoạt đăng ký.
                    try
                    {
                        string mail_to = _Vendor.VendorEmail;
                        string mail_subject = "Hợp Thành Trading Business Co.,Ltd";


                        string mail_body = "Hello " + _Vendor.VendorFullName + "!"
                                    + "<br/>"
                                    + "Congratulations you have successfully registered an account Hop Thanh Trading Business"
                                    + "<br/>"
                                    + "Please click this link to active your account!"
                                    + "<br/>"
                                    + "<a href='" + _url_verify + "'>[Active Account]</a>";
                        Common.Common cm = new Common.Common();
                        cm.SendMail(mail_subject, mail_body, _Vendor.VendorEmail);
                    }
                    catch
                    {
                        return false;
        }
                    break;
                case 2://gửi mail thông báo xác nhận reset new password
                    try
                    {
                        string mail_to = _Vendor.VendorEmail;
                        string mail_subject = "Hợp Thành Trading Business Co.,Ltd";


                        string mail_body = "Xin chào " + _Vendor.VendorFullName + "!"
                                    + "<br/>"
                                    + "Mã bảo mật của bạn là: <span><table border='0'><tr><td bgcolor='blue'><b>" + CreateSecurityToken(_Vendor.VendorId) + "</b></td></tr></table></span>"
                                    + "<br/>"
                                    + "Hãy nhấp vào đường link dưới đây để thay đổi mật khẩu"
                                    + "<br/>"
                                    + "<a href='" + _url_verify + "'>[Reset Password]</a>";
                        Common.Common cm = new Common.Common();
                        cm.SendMail(mail_subject, mail_body, _Vendor.VendorEmail);
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case 3:
                    try
                    {
                        string mail_to = _Vendor.VendorEmail;
                        string mail_subject = "Hợp Thành Trading Business Co.,Ltd";


                        string mail_body = "Xin chào " + _Vendor.VendorFullName + "!"
                                    + "<br/>"
                                    + "Mã bảo mật của bạn là: <span><table border='0'><tr><td bgcolor='blue'><b>" + CreateSecurityToken(_Vendor.VendorId) + "</b></td></tr></table></span>"
                                    + "<br/>"
                                    + "Hãy nhấp vào đường link dưới đây để thay đổi mật khẩu"
                                    + "<br/>"
                                    + "<a href='" + _url_verify + "'>[Reset Password]</a>";
                        Common.Common cm = new Common.Common();
                        cm.SendMail(mail_subject, mail_body, _Vendor.VendorEmail);
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
      

        public Vendor LogIn(string UserName, string password)
        {
            CIS_DBEntities _data = new CIS_DBEntities();
            SecureAuthenticationRepository _SecureAuthenticationRepository = new SecureAuthenticationRepository();
            var username = _data.Vendor.Where(n => n.UserName == UserName && n.IsDeleted == false).FirstOrDefault();
            if (username == null)
                return null;

            var secure = _SecureAuthenticationRepository.getById(Convert.ToInt64(username.SecureAuthenticationId));
            var password_encode = Security.MD5Encrypt_Custom(password, secure.HashToken, secure.SaltToken);
            if (password_encode == username.Password)
            {
                username.Password = null;
                return username;
            }
            return null;
        }
    }
}
