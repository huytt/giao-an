using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.cis;
using PagedList;
using HTTelecom.Domain.Core.ExClass;
using System.Text.RegularExpressions;
using HTTelecom.Domain.Core.Repository.ams;
namespace HTTelecom.Domain.Core.Repository.cis
{
    public class VendorRepository
    {
        public IList<Vendor> GetList_VendorAll()
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Vendors.ToList();
                }
                catch
                {
                    return new List<Vendor>();
                }
            }
        }
        //public IList<Vendor> GetList_Vendor_VendorTypeCode(string VendorTypeCode)
        //{
        //    using (CIS_DBEntities _data = new CIS_DBEntities())
        //    {
        //        _data.Configuration.ProxyCreationEnabled = false;
        //        _data.Configuration.LazyLoadingEnabled = false;
        //        try
        //        {
        //            return _data.Vendors.Where(a => a.VendorTypeCode == VendorTypeCode).OrderBy(b => b.VendorName).ToList();
        //        }
        //        catch
        //        {
        //            return new List<Vendor>();
        //        }
        //    }
        //}
        public IList<Vendor> GetList_VendorAll_IsDeleted(bool isDeleted)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.Vendors.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Vendor>();
                }
            }
        }
        public IList<Vendor> GetList_VendorAll_IsActive(bool IsActive)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.Vendors.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Vendor>();
                }
            }
        }
        public IList<Vendor> GetList_VendorAll_IsDeleted_IsActive(bool IsDeleted, bool IsActive)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Vendors.Where(a => a.IsDeleted == IsDeleted)
                                      .Where(b => b.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Vendor>();
                }
            }
        }
        public Vendor Get_VendorById(long VendorId)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.Vendors.Find(VendorId);
                }
                catch
                {
                    return null;
                }
            }
        }
        public long Insert(Vendor Vendor)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    Vendor.DateCreated = DateTime.Now;
                    AuthenticationKeyRepository _SecureAuthenticationRepository = new AuthenticationKeyRepository();
                    var secure = _SecureAuthenticationRepository.GetList_AuthenticationKeyAll().OrderByDescending(a => a.AuthenticationKeyId).FirstOrDefault();
                    Vendor.Password = Security.MD5Encrypt_Custom(Vendor.Password, secure.HashToken, secure.SaltToken);
                    Vendor.SecureAuthenticationId = 1;
                    Vendor.Token = CreatePassword(10);// độ dài token ==  10
                    Vendor.IsActive = false;
                    _data.Vendors.Add(Vendor);
                    _data.SaveChanges();
                    Vendor.UserName = string.Format("vd{0:000000}", Vendor.VendorId);
                    _data.SaveChanges();
                    return Vendor.VendorId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(Vendor Vendor)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    Vendor VendorToUpdate;
                    VendorToUpdate = _data.Vendors.Where(x => x.VendorId == Vendor.VendorId).FirstOrDefault();
                    VendorToUpdate.ContractId = Vendor.ContractId ?? VendorToUpdate.ContractId;
                    VendorToUpdate.VendorEmail = Vendor.VendorEmail ?? VendorToUpdate.VendorEmail;
                    VendorToUpdate.VendorFullName = Vendor.VendorFullName ?? VendorToUpdate.VendorFullName;
                    VendorToUpdate.Password = Vendor.Password ?? VendorToUpdate.Password;
                    VendorToUpdate.CompanyName = Vendor.CompanyName ?? VendorToUpdate.CompanyName;
                    VendorToUpdate.LinkWebsite = Vendor.LinkWebsite ?? VendorToUpdate.LinkWebsite;
                    VendorToUpdate.LogoFile = Vendor.LogoFile ?? VendorToUpdate.LogoFile;
                    VendorToUpdate.BusinessLicenseFile = Vendor.BusinessLicenseFile ?? VendorToUpdate.BusinessLicenseFile;
                    VendorToUpdate.Description = Vendor.Description ?? VendorToUpdate.Description;
                    VendorToUpdate.CommonService = Vendor.CommonService ?? VendorToUpdate.CommonService;
                    VendorToUpdate.Token = Vendor.Token ?? VendorToUpdate.Token;
                    VendorToUpdate.DateModified = DateTime.Now;
                    VendorToUpdate.ModifiedBy = Vendor.ModifiedBy ?? VendorToUpdate.ModifiedBy;
                    VendorToUpdate.IsActive = Vendor.IsActive ?? VendorToUpdate.IsActive;
                    VendorToUpdate.IsDeleted = Vendor.IsDeleted ?? VendorToUpdate.IsDeleted;
                    

                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public IPagedList<Vendor> GetList_VendorPagingAll(int pageNum, int pageSize)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Vendor = _data.Vendors.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);
                    return lst_Vendor;
                }
                catch
                {
                    return new PagedList<Vendor>(new List<Vendor>(), 1, pageSize);
                }
            }
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
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
                                    + "Username: " + _Vendor.UserName
                                    + "<br/>"
                                    + "Password: " + _activeString
                                    + "<br/>"
                                    + "Please click this link to active your account!"
                                    + "<br/>"
                                    + "<a href='" + _url_verify + "'>[Active Account]</a>";
                        Common cm = new Common();
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
                        Common cm = new Common();
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
                        Common cm = new Common();
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
         public string CreateSecurityToken(long VendorId)
         {
             try
             {
                 if (VendorId == 0)
                     return "";
                 CIS_DBEntities _data = new CIS_DBEntities();
                 var Vendor = _data.Vendors.Where(n => n.VendorId == VendorId).FirstOrDefault();
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

         public string GetPassChekingById(long vendorId)
         {
             try
             {
                 CIS_DBEntities _data = new CIS_DBEntities();
                 var acc = _data.Vendors.Find(vendorId);
                 return acc.Token;
             }
             catch
             {
                 return "";
             }
         }

         public Vendor GetById(long vendorId)
         {
             try
             {
                 CIS_DBEntities _data = new CIS_DBEntities();
                 var acc = _data.Vendors.Find(vendorId);
                 acc.Password = "";
                 return acc;
             }
             catch
             {
                 return null;
             }
         }
    }
}
