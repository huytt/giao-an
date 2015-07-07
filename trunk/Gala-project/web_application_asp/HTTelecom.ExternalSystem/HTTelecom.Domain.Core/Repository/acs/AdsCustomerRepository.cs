using HTTelecom.Domain.Core.Common;
using HTTelecom.Domain.Core.DataContext.acs;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.Repository.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.acs
{
    public class AdsCustomerRepository
    {
        public List<AdsCustomer> GetAll(bool IsDeleted, bool IsActive)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.AdsCustomer.Where(n => n.IsActive == IsActive && n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<AdsCustomer>();
            }
        }
        public AdsCustomer GetById(long id)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.AdsCustomer.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public AdsCustomer Login(string email, string password)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                SecureAuthenticationRepository _SecureAuthenticationRepository = new SecureAuthenticationRepository();
                var customer = _data.AdsCustomer.Where(n => n.Email == email && n.IsDeleted == false && n.IsActive == true).FirstOrDefault();
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
    }
}
