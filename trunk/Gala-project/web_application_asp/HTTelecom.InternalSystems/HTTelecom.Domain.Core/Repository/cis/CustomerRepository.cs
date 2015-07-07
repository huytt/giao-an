using HTTelecom.Domain.Core.DataContext.cis;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class CustomerRepository
    {
        public Customer GetByCode(string CustomerCode)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                //var _cusLst = _data.Customers.Where(n=>n.)
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateCustomer(Customer _cus)
        {
            using (CIS_DBEntities entities = new CIS_DBEntities())
            {
                try
                {
                    Customer CustomerToUpdate;
                    CustomerToUpdate = entities.Customers.Where(x => x.CustomerId == _cus.CustomerId).FirstOrDefault();
                    CustomerToUpdate.IsActive = _cus.IsActive ?? CustomerToUpdate.IsActive;
                    CustomerToUpdate.IsDeleted = _cus.IsDeleted ?? CustomerToUpdate.IsDeleted;
                    CustomerToUpdate.Email = _cus.Email ?? CustomerToUpdate.Email;
                    CustomerToUpdate.FirstName = _cus.FirstName ?? CustomerToUpdate.FirstName;
                    CustomerToUpdate.LastName = _cus.LastName ?? CustomerToUpdate.LastName;
                    CustomerToUpdate.Gender = _cus.Gender ?? CustomerToUpdate.Gender;
                    CustomerToUpdate.Phone = _cus.Phone ?? CustomerToUpdate.Phone;
                    CustomerToUpdate.Career = _cus.Career ?? CustomerToUpdate.Career;
                    CustomerToUpdate.Address = _cus.Address ?? CustomerToUpdate.Address;
                    CustomerToUpdate.DateOfBirth = _cus.DateOfBirth ?? CustomerToUpdate.DateOfBirth;
                    CustomerToUpdate.InBlackList = _cus.InBlackList ?? CustomerToUpdate.InBlackList;
                    CustomerToUpdate.AvatarPhotoUrl = _cus.AvatarPhotoUrl ?? CustomerToUpdate.AvatarPhotoUrl;
                    CustomerToUpdate.DateModified = DateTime.Now;

                    entities.SaveChanges();              
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

        public Customer GetByEmail(string CustomerEmail)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var _cusLst = _data.Customers.Where(n => n.Email == CustomerEmail).ToList();
                if (_cusLst.Count != 1)
                    return null;
                else
                    return _cusLst[0];
            }
            catch
            {
                return null;
            }
        }


        public Customer GetById(long CustomerId)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.Customers.Find(CustomerId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<Customer> GetAllCustomer()
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                var _cusLst = _data.Customers.ToList();
                return _cusLst;
            }
            catch
            {
                return null;
            }
        }
        public List<Customer> GetFilterCustomer(string email,string firstname,string lastname,string gender,string phone,string address,string dateofbirth)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                List<Customer> result = new List<Customer>();
                if (email == "" && firstname == "" && lastname == "" && gender == "" && phone == "" && address == "" && dateofbirth == "")
                    return this.GetAllCustomer();
                var _cusLst = _data.spFilterCustomer(email, firstname, lastname, gender, phone, address, dateofbirth).ToList();
                foreach (var item in _cusLst)
                {
                    Customer c = new Customer();
                    c.Address = item.Address;
                    c.AvatarPhotoUrl = item.AvatarPhotoUrl;
                    c.Career = item.Career;
                    c.CustomerId = item.CustomerId;
                    c.CustomerRoleId = item.CustomerRoleId;
                    c.DateActive = item.DateActive;
                    c.DateCreated = item.DateCreated;
                    c.DateModified = item.DateModified;
                    c.DateModifiedPassword = item.DateModifiedPassword;
                    c.DateOfBirth = item.DateOfBirth;
                    c.Email = item.Email;
                    c.FirstName = item.FirstName;
                    c.Gender = item.Gender;
                    c.GiftPoint = item.GiftPoint;
                    c.InBlackList = item.InBlackList;
                    c.IsActive = item.IsActive;
                    c.IsDeleted = item.IsDeleted;
                    c.LastIpAddress = item.LastIpAddress;
                    c.LastName = item.LastName;
                    c.Password = item.Password;
                    c.Phone = item.Phone;
                    c.SecureAuthenticationId = item.SecureAuthenticationId;
                    c.SecurityToken = item.SecurityToken;
                    c.Xoney = item.Xoney;                    
                    result.Add(c);
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        public IPagedList<Customer> GetList_CustomerPagingAll(int pageNum, int pageSize, string email, string firstname, string lastname, string gender, string phone, string address, string dateofbirth)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Customer = this.GetFilterCustomer(email, firstname, lastname, gender, phone, address, dateofbirth).OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);

                    foreach (var item in lst_Customer)
                    {
                        item.Password = null;
                    }
                    return lst_Customer;
                }
                catch
                {
                    return new PagedList<Customer>(new List<Customer>(), 1, pageSize);
                }
            }
        }

        public IPagedList<Customer> GetList_CustomerPagingAll(int pageNum, int pageSize)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Customer = _data.Customers.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);

                    foreach (var item in lst_Customer)
                    {
                        item.Password = null;
                    }
                    return lst_Customer;
                }
                catch
                {
                    return new PagedList<Customer>(new List<Customer>(), 1, pageSize);
                }
            }
        }

        public string CreateSecurityToken(long CustomerId)
        {
            try
            {
                if (CustomerId == 0)
                    return "";
                CIS_DBEntities _data = new CIS_DBEntities();
                var customer = _data.Customers.Where(n => n.CustomerId == CustomerId).FirstOrDefault();
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
                var customer = _data.Customers.Where(n => n.CustomerId == CustomerId).FirstOrDefault();

                customer.SecurityToken = "";
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
