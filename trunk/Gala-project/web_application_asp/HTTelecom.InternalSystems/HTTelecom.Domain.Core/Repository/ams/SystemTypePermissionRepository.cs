using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class SystemTypePermissionRepository
    {
        public IPagedList<SystemTypePermission> GetList_SystemTypePermissionAll(int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SystemTypePermissions.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<SystemTypePermission>(new List<SystemTypePermission>(), 1, pageSize);
                }
            }
        }
        public IList<SystemTypePermission> GetList_SystemTypePermissionAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SystemTypePermissions.OrderByDescending(a => a.DateCreated).ToList();
                }
                catch
                {
                    return new List<SystemTypePermission>();
                }
            }
        }
        public IList<SystemTypePermission> GetList_SystemTypePermissionAll_IsAllowed(bool isAllowed)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypePermissions.Where(a => a.IsAllowed == isAllowed).ToList();
                }
                catch
                {
                    return new List<SystemTypePermission>();
                }
            }
        }
        public IPagedList<SystemTypePermission> GetList_SystemTypePermissionAll_SystemTypeId(long systemTypeId, int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SystemTypePermissions.Where(a => a.SystemTypeId == systemTypeId).OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<SystemTypePermission>(new List<SystemTypePermission>(), 1, pageSize);
                }
            }
        }
        public IList<SystemTypePermission> GetList_SystemTypePermissionAll_SystemTypeId(long systemTypeId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypePermissions.Where(a => a.SystemTypeId == systemTypeId).ToList();
                }
                catch
                {
                    return new List<SystemTypePermission>();
                }
            }
        }

        public SystemTypePermission Get_SystemTypePermissionById(long SystemTypePermissionId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypePermissions.Find(SystemTypePermissionId);
                }
                catch
                {
                    return null;
                }
            }
        }
        public SystemTypePermission Get_SystemTypePermission_SystemTypeId_AccountId(long systemTypeId, long accountId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypePermissions.Where(a => a.AccountId == accountId && a.SystemTypeId == systemTypeId).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }


        public long Insert(SystemTypePermission systemTypePermission)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    systemTypePermission.DateCreated = DateTime.Now;
                    systemTypePermission.IsDeleted = false;
                    systemTypePermission.IsAllowed = true;

                    _data.SystemTypePermissions.Add(systemTypePermission);
                    _data.SaveChanges();

                    return systemTypePermission.SystemTypePermissionId;
                }
                catch
                {
                    throw new Exception("Insert was an error occurs or object already exists !!!");
                }

            }
        }

        public bool Update(SystemTypePermission systemTypePermission)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    SystemTypePermission SystemTypePermissionToUpdate;
                    SystemTypePermissionToUpdate = entities.SystemTypePermissions.Where(x => x.SystemTypePermissionId == systemTypePermission.SystemTypePermissionId).FirstOrDefault();
                    SystemTypePermissionToUpdate = systemTypePermission;
                    //4. call SaveChanges
                    entities.SaveChanges();

                    return true;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }

        public bool UpdateIsAllowed(long systemTypePermissionId, bool isAllowed, long accountUpdate)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    SystemTypePermission systemTypePermission = _data.SystemTypePermissions.Where(x => x.SystemTypePermissionId == systemTypePermissionId).FirstOrDefault();

                    systemTypePermission.IsAllowed = isAllowed;
                    systemTypePermission.ModifiedBy = accountUpdate;
                    systemTypePermission.DateModified = DateTime.Now;

                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public bool UpdateIsDeleted(long systemTypePermissionId, bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    SystemTypePermission systemTypePermission = _data.SystemTypePermissions.Where(x => x.SystemTypePermissionId == systemTypePermissionId).FirstOrDefault();

                    systemTypePermission.IsDeleted = isDeleted;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public SystemTypePermission Get_SystemTypePermissionIsSecurityRole(long accountId, long systemTypeId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypePermissions.Where(a => a.AccountId == accountId && a.SystemTypeId == systemTypeId).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }
        public IList<SystemTypePermission> GetList_SystemTypePermissionIsAccount(long securityRoleId, long systemTypeId, bool isAllowed, bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypePermissions.Where(a => a.SecurityRoleId == securityRoleId && a.SystemTypeId == systemTypeId && a.IsAllowed == isAllowed && a.IsDeleted == isDeleted).ToList();
                }
                catch
                { 
                    return new List<SystemTypePermission>(); 
                }
            }
        }
    }
}
