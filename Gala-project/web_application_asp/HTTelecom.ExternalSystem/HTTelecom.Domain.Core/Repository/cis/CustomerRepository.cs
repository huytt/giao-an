using HTTelecom.Domain.Core.Common;
using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class CustomerRepository
    {
        public List<Customer> GetAll(bool IsDeleted)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Customer.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Customer>();
            }
        }

        public bool CheckEmail(string email)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var lst = _data.Customer.Where(n => n.Email == email && n.IsDeleted == false).ToList();
                if (lst.Count > 0)
                    return true;
                return false;
            }
            catch
            {
                return true;
            }
        }

        public long Create(Customer model)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                SecureAuthenticationRepository _SecureAuthenticationRepository = new SecureAuthenticationRepository();

                var secure = _SecureAuthenticationRepository.getById(Convert.ToInt64(model.SecureAuthenticationId));
                model.Password = Security.MD5Encrypt_Custom(model.Password, secure.HashToken, secure.SaltToken);
                _data.Customer.Add(model);
                _data.SaveChanges();
                return model.CustomerId;
            }
            catch
            {
                return -1;
            }
        }

        public Customer Login(string email, string password)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                SecureAuthenticationRepository _SecureAuthenticationRepository = new SecureAuthenticationRepository();
                var customer = _data.Customer.Where(n => n.Email == email && n.IsDeleted == false).FirstOrDefault();
                if (customer == null)
                    return null;

                var secure = _SecureAuthenticationRepository.getById(Convert.ToInt64(customer.SecureAuthenticationId));
                var password_encode = Security.MD5Encrypt_Custom(password, secure.HashToken, secure.SaltToken);
                if (password_encode == customer.Password)
                {
                    customer.Password = null;
                    return customer;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Customer GetById(long CustomerId)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var acc = _data.Customer.Find(CustomerId);
                acc.Password = "";
                return acc;
            }
            catch
            {
                return null;
            }
        }

        public Customer GetByEmail(string _customerEmail)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var acc = _data.Customer.Where(c => c.Email == _customerEmail).First();
                return acc;
            }
            catch
            {
                return null;
            }
        }

        public string ResetPassword(long _cId, string pChecking)//hàm tự động tổng hợp trả về một password mới. Trước đó hàm tự động cập nhật New pass
        {           
            try
            {
                if (this.GetPassChekingById(_cId) != pChecking)
                    return "";
                CIS_DBEntities _data = new CIS_DBEntities();
                SecureAuthenticationRepository _SecureAuthenticationRepository = new SecureAuthenticationRepository();
                var secure = _SecureAuthenticationRepository.getById(Convert.ToInt64(this.GetById(_cId).SecureAuthenticationId));
                //tạo password mới
                string newpass = RandomString(6);
                var password_encode = Security.MD5Encrypt_Custom(newpass, secure.HashToken, secure.SaltToken);
                Customer acc = _data.Customer.Where(c => c.CustomerId == _cId).First();
                acc.Password = password_encode;
                acc.DateModifiedPassword = DateTime.Now;
                _data.SaveChanges();
                return newpass;
            }
            catch
            {
                return "";
            }
        }

        private string RandomString(int length)//tự động tạo ngẫu nhiên một chuỗi mới với [length] ký tự
        {
            Random _random = new Random(Environment.TickCount);
            string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            StringBuilder builder = new StringBuilder(length);

            for (int i = 0; i < length; ++i)
                builder.Append(chars[_random.Next(chars.Length)]);

            return builder.ToString();
        }
        public string GetPassChekingById(long CustomerId)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var acc = _data.Customer.Find(CustomerId);
                return  Regex.Replace(acc.Password, @"[^0-9a-zA-Z]+", "").Substring(0, 5);
            }
            catch
            {
                return "";
            }
        }


        public long UpdatePassword(long CustomerId, string pass, string newPass)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var acc = _data.Customer.Find(CustomerId);
                SecureAuthenticationRepository _SecureAuthenticationRepository = new SecureAuthenticationRepository();
                var customer = _data.Customer.Where(n => n.Email == acc.Email && n.IsDeleted == false && n.IsActive == true).FirstOrDefault();
                if (customer == null)
                    return -1;
                var secure = _SecureAuthenticationRepository.getById(Convert.ToInt64(customer.SecureAuthenticationId));
                var password_encode = Security.MD5Encrypt_Custom(pass, secure.HashToken, secure.SaltToken);
                if (password_encode == customer.Password)
                {
                    customer.Password = Security.MD5Encrypt_Custom(newPass, secure.HashToken, secure.SaltToken);
                    customer.DateModifiedPassword = DateTime.Now;
                    _data.SaveChanges();
                    return 1;
                }
                else
                    return -1;
                //return acc;
            }
            catch
            {
                return -1;
            }
        }

        public bool UpdateAvatar(long CustomerId, string url)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var acc = _data.Customer.Find(CustomerId);
                SecureAuthenticationRepository _SecureAuthenticationRepository = new SecureAuthenticationRepository();
                var customer = _data.Customer.Where(n => n.Email == acc.Email && n.IsDeleted == false && n.IsActive == true).FirstOrDefault();
                if (customer == null)
                    return false;

                customer.AvatarPhotoUrl = url;
                customer.DateModified = DateTime.Now;
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public long UpdateForgotPassword(long CustomerId, string securityCode, string newPass)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var acc = _data.Customer.Find(CustomerId);
                SecureAuthenticationRepository _SecureAuthenticationRepository = new SecureAuthenticationRepository();
                var customer = _data.Customer.Where(n => n.Email == acc.Email && n.IsDeleted == false && n.IsActive == true).FirstOrDefault();
                if (customer == null)
                    return -1;
                var secure = _SecureAuthenticationRepository.getById(Convert.ToInt64(customer.SecureAuthenticationId));
                if (securityCode == customer.SecurityToken)
                {
                    customer.Password = Security.MD5Encrypt_Custom(newPass, secure.HashToken, secure.SaltToken);
                    customer.DateModifiedPassword = DateTime.Now;
                    _data.SaveChanges();
                    this.RemoveSecurityToken(CustomerId);
                    return 1;
                }
                else
                    return -1;
                //return acc;
            }
            catch
            {
                return -1;
            }
        }

        public string CreateSecurityToken(long CustomerId)
        {
            try
            {
                if (CustomerId == 0)
                    return "";
                CIS_DBEntities _data = new CIS_DBEntities();
                var customer = _data.Customer.Where(n => n.CustomerId == CustomerId).FirstOrDefault();
                string se_code = DateTime.Now.Ticks.ToString();
                customer.SecurityToken = se_code.Substring(se_code.Length - 5, 5);
                _data.SaveChanges();
                return customer.SecurityToken;
            }
            catch
            {
                return "";
            }
        }

        public bool RemoveSecurityToken(long CustomerId)
        {
            try
            {
                if (CustomerId == 0)
                    return false;
                CIS_DBEntities _data = new CIS_DBEntities();
                var customer = _data.Customer.Where(n => n.CustomerId == CustomerId).FirstOrDefault();
               
                customer.SecurityToken = "";
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(long CustomerId, Customer model)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var acc = _data.Customer.Find(CustomerId);
                acc.Phone = model.Phone;
                acc.FirstName = model.FirstName;
                acc.LastName = model.LastName;
                acc.Address = model.Address;
                acc.DateModified = DateTime.Now;
                acc.Gender = model.Gender;
                acc.DateOfBirth = model.DateOfBirth;
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool isActiveCustomer(long CustomerId,bool _isActive)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                Customer customer = _data.Customer.Where(n => n.CustomerId == CustomerId).FirstOrDefault();
                customer.IsActive = _isActive;
                if (_isActive == true)
                {
                    customer.DateActive = DateTime.Now;
                }
                _data.SaveChanges();      
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendMail_Customer(Customer _customer, string _url_verify,string _activeString,int _action)
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
                        string mail_to = _customer.Email;
                        string mail_subject = "Hợp Thành Trading Business Co.,Ltd";


                        string mail_body = "Hello " + _customer.FirstName + "!"
                                    + "<br/>"
                                    + "Congratulations you have successfully registered an account Hop Thanh Trading Business"
                                    + "<br/>"
                                    + "Please click this link to active your account!"
                                    + "<br/>"
                                    + "<a href='" + _url_verify + "'>[Active Account]</a>";
                        Common.Common cm = new Common.Common();
                        cm.SendMail(mail_subject, mail_body, _customer.Email);    
                    }
                    catch 
                    {
                        return false;
                    }
                    break;
                case 2://gửi mail thông báo xác nhận reset new password
                    try
                    {
                        string mail_to = _customer.Email;
                        string mail_subject = "Hợp Thành Trading Business Co.,Ltd";


                        string mail_body = "Xin chào " + _customer.FirstName + "!"
                                    + "<br/>"
                                    + "Mã bảo mật của bạn là: <span><table border='0'><tr><td bgcolor='blue'><b>" + CreateSecurityToken(_customer.CustomerId) + "</b></td></tr></table></span>"
                                    + "<br/>"
                                    + "Hãy nhấp vào đường link dưới đây để thay đổi mật khẩu"
                                    + "<br/>"
                                    + "<a href='" + _url_verify + "'>[Reset Password]</a>";
                        Common.Common cm = new Common.Common();
                        cm.SendMail(mail_subject, mail_body, _customer.Email);
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                case 3:
                    try
                    {
                        string mail_to = _customer.Email;
                        string mail_subject = "Hợp Thành Trading Business Co.,Ltd";


                        string mail_body = "Xin chào " + _customer.FirstName + "!"
                                    + "<br/>"
                                    + "Mã bảo mật của bạn là: <span><table border='0'><tr><td bgcolor='blue'><b>" + CreateSecurityToken(_customer.CustomerId) + "</b></td></tr></table></span>"
                                    + "<br/>"
                                    + "Hãy nhấp vào đường link dưới đây để thay đổi mật khẩu"
                                    + "<br/>"
                                    + "<a href='" + _url_verify + "'>[Reset Password]</a>";
                        Common.Common cm = new Common.Common();
                        cm.SendMail(mail_subject, mail_body, _customer.Email);
                    }
                    catch
                    {
                        return false;
                    }
                    break;
                    break;
                default:
                    break;
            }
            return true;
        }
      
    }
}
