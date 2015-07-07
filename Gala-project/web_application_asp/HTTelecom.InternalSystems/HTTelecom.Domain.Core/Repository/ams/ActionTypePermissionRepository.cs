using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class ActionTypePermissionRepository
    {
        public IPagedList<ActionTypePermission> GetList_ActionTypePermissionAll(int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ActionTypePermissions.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<ActionTypePermission>(new List<ActionTypePermission>(), 1, pageSize);
                }
            }
        }
        public IList<ActionTypePermission> GetList_ActionTypePermissionAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ActionTypePermissions.OrderByDescending(a => a.DateCreated).ToList();
                }
                catch
                {
                    return new List<ActionTypePermission>();
                }
            }
        }
        public IList<ActionTypePermission> GetList_ActionTypePermissionAll_IsAllowed(bool isAllowed)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypePermissions.Where(a => a.IsAllowed == isAllowed).ToList();
                }
                catch
                {
                    return new List<ActionTypePermission>();
                }
            }
        }
        public IPagedList<ActionTypePermission> GetList_ActionTypePermissionAll_ActionTypeId(long actionTypeId, int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ActionTypePermissions.Where(a => a.ActionTypeId == actionTypeId).OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<ActionTypePermission>(new List<ActionTypePermission>(), 1, pageSize);
                }
            }
        }
        public IList<ActionTypePermission> GetList_ActionTypePermissionAll_SystemTypeId(long actionTypeId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypePermissions.Where(a => a.ActionTypeId == actionTypeId).ToList();
                }
                catch
                {
                    return new List<ActionTypePermission>();
                }
            }
        }

        public ActionTypePermission Get_ActionTypePermissionById(long ActionTypePermissionId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypePermissions.Find(ActionTypePermissionId);
                }
                catch
                {
                    return null;
                }
            }
        }
        public ActionTypePermission Get_ActionTypePermission_SystemTypeId_AccountId(long actionTypeId, long accountId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypePermissions.Where(a => a.AccountId == accountId && a.ActionTypeId == actionTypeId).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }


        public long Insert(ActionTypePermission ActionTypePermission)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    ActionTypePermission.DateCreated = DateTime.Now;

                    _data.ActionTypePermissions.Add(ActionTypePermission);
                    _data.SaveChanges();

                    return ActionTypePermission.ActionTypePermissionId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(ActionTypePermission ActionTypePermission)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    ActionTypePermission ActionTypePermissionToUpdate;
                    ActionTypePermissionToUpdate = entities.ActionTypePermissions.Where(x => x.ActionTypePermissionId == ActionTypePermission.ActionTypePermissionId).FirstOrDefault();
                    ActionTypePermissionToUpdate = ActionTypePermission;
                    //4. call SaveChanges
                    entities.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateIsAllowed(long ActionTypePermissionId, bool isAllowed, long accountUpdate)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    ActionTypePermission ActionTypePermission = _data.ActionTypePermissions.Where(x => x.ActionTypePermissionId == ActionTypePermissionId).FirstOrDefault();

                    ActionTypePermission.IsAllowed = isAllowed;
                    ActionTypePermission.ModifiedBy = accountUpdate;
                    ActionTypePermission.DateModified = DateTime.Now;

                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public bool UpdateIsDeleted(long ActionTypePermissionId, bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    ActionTypePermission ActionTypePermission = _data.ActionTypePermissions.Where(x => x.ActionTypePermissionId == ActionTypePermissionId).FirstOrDefault();

                    ActionTypePermission.IsDeleted = isDeleted;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }
    }
}
