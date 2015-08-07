using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;
using HTTelecom.Domain.Core.ExClass;
using System.Data.Entity;
namespace HTTelecom.Domain.Core.Repository.ams
{
    public class AccountRepository
    {
        public IPagedList<Account> GetList_AccountAll(int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Account = _data.Accounts.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);

                    foreach (var item in lst_Account)
                    {
                        item.Password = null;
                    }
                    return lst_Account;
                }
                catch
                {
                    return new PagedList<Account>(new List<Account>(), 1, pageSize);
                }
            }
        }
        public IList<Account> GetList_AccountAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Account = _data.Accounts.OrderBy(a => a.FullName).ToList();
                    foreach (var item in lst_Account)
                    {
                        item.Password = null;
                    }
                    return lst_Account;
                }
                catch
                {
                    return new List<Account>();
                }
            }
        }
        public IList<Account> GetList_AccountAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Account = _data.Accounts.Where(a => a.IsDeleted == isDeleted).ToList();
                    foreach (var item in lst_Account)
                    {
                        item.Password = null;
                    }
                    return lst_Account;
                }
                catch
                {
                    return new List<Account>();
                }
            }
        }

        public Account Get_AccountById(long accountId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {

                try
                {
                    return _data.Accounts.Find(accountId);
                }
                catch
                {
                    return null;
                }

            }
        }

        public Account Get_AccountByEmail(string email)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                var item = _data.Accounts.Where(x => x.Email == email).FirstOrDefault();
                item.Password = null;
                return item;
            }
        }

        public Account LoginAccount(string email, string passWord)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    var item = _data.Accounts.Include(n=>n.GroupAccounts).Where(x => x.Email == email && x.Password == passWord).FirstOrDefault();
                    item.Password = null;
                    return item;
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(Account account)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    AuthenticationKeyRepository _iAuthenticationKeyService = new AuthenticationKeyRepository();
                    var secureKey = _iAuthenticationKeyService.GetList_AuthenticationKeyAll().OrderByDescending(a => a.AuthenticationKeyId).FirstOrDefault();

                    account.AuthenticationKeyId = secureKey.AuthenticationKeyId;
                    account.StaffId = "HT" + account.StaffId;
                    account.Password = Security.MD5Encrypt_Custom("123123", secureKey.HashToken, secureKey.SaltToken);
                    account.DateCreated = DateTime.Now;
                    account.IsDeleted = false;

                    _data.Accounts.Add(account);
                    _data.SaveChanges();

                    return account.AccountId;
                }
                catch
                {
                    return -1;
                }

            }
        }
       

        public bool Update(Account account)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    Account accountToUpdate;
                    accountToUpdate = _data.Accounts.Where(x => x.AccountId == account.AccountId).FirstOrDefault();

                    accountToUpdate.OrgRoleId = account.OrgRoleId;
                    accountToUpdate.DepartmentId = account.DepartmentId;
                    accountToUpdate.DepartmentGroupId = account.DepartmentGroupId;
                    accountToUpdate.StaffId = account.StaffId.ToUpper();
                    accountToUpdate.Email = account.Email;
                    accountToUpdate.FullName = account.FullName;
                    accountToUpdate.Gender = account.Gender;
                    accountToUpdate.Phone = account.Phone;

                    accountToUpdate.DateModified = DateTime.Now;
                    //4. call SaveChanges
                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        public bool ChangePassword(long accountID, string NewPassword)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    Account accountToUpdate;
                    accountToUpdate = _data.Accounts.Where(x => x.AccountId == accountID).FirstOrDefault();
                    accountToUpdate.Password =NewPassword;
                    accountToUpdate.DateModified = DateTime.Now;
                    
                    _data.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public List<Account> GetAll()
        {
            try
            {
                AMS_DBEntities _data = new AMS_DBEntities();
                var lst = _data.Accounts.ToList();
                foreach (var item in lst)
                {
                    item.Password = "";
                }
                return lst;
            }
            catch
            {
                return new List<Account>();
            }
        }

        public bool IsAdmin(long AccountId, string SystemCode)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
                    SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
                    SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();

                    SystemType st = _iSystemTypeService.Get_SystemTypeByCode(SystemCode);
                    SystemTypePermission stp = _iSystemTypePermissionService.Get_SystemTypePermissionIsSecurityRole(AccountId, st.SystemTypeId);
                    SecurityRole sr = _iSecurityRoleService.Get_SecurityRoleById(stp.SecurityRoleId);
                    int levelRoleAdmin = (int)_iSecurityRoleService.Get_SecurityRoleByCode("ADM").LevelRole;

                    if (sr.LevelRole <= levelRoleAdmin)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        //public long Get_ByRoleNSystem(string securityCode, string systemCode)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
        //            SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
        //            SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();

        //            SystemType st = _iSystemTypeService.Get_SystemTypeByCode(systemCode);
        //            SecurityRole sr = _iSecurityRoleService.Get_SecurityRoleByCode(securityCode);
        //            SystemTypePermission stp = _iSystemTypePermissionService.Get_SystemTypePermissionIsAccount(sr.SecurityRoleId, st.SystemTypeId);

        //            return stp.AccountId;
        //        }
        //        catch
        //        {
        //            return -1;
        //        }
        //    }
        //}

        public IPagedList<Account> SearchAccount(string keywords, int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    string wordASCII = Generates.ConvertUnicodeToASCII(keywords).ToLower();
                    var querylst_Account = _data.Accounts.ToList();
                    IList<Account> lst_Account = querylst_Account.FindAll(
                        delegate(Account account)
                        {
                            if (Generates.ConvertUnicodeToASCII(account.Email.ToLower()).Contains(wordASCII) || Generates.ConvertUnicodeToASCII(account.StaffId.ToLower()).Contains(wordASCII) || Generates.ConvertUnicodeToASCII(account.FullName.ToLower()).Contains(wordASCII))
                                return true;
                            else
                                return false;
                        }
                    );
                    return lst_Account.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);
                }
                catch { return new PagedList<Account>(new List<Account>(), 1, pageSize); }
            }
        }
    }
}
