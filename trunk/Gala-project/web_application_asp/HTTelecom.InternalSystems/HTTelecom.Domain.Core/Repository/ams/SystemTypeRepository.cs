using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class SystemTypeRepository
    {
        public IPagedList<SystemType> GetList_SystemTypeAll(int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SystemTypes.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<SystemType>(new List<SystemType>(), 1, pageSize);
                }
            }
        }

        public IList<SystemType> GetList_SystemTypeAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SystemTypes.ToList();
                }
                catch
                {
                    return new List<SystemType>();
                }
            }
        }

        public IList<SystemType> GetList_SystemTypeAll(bool isDeleted)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                return entities.SystemTypes.Where(a => a.IsDeleted == isDeleted).ToList();
            }
        }

        public IList<SystemType> GetList_SystemTypeAll_IsActived(bool IsActived)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypes.Where(a => a.IsActive == IsActived).ToList();
                }
                catch
                {
                    return new List<SystemType>();
                }
            }
        }

        public IList<SystemType> GetList_SystemTypeAll_IsActive_IsDeleted(bool IsActive, bool IsDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SystemTypes.Where(a => a.IsActive == IsActive && a.IsDeleted == IsDeleted).ToList();
                }
                catch
                {
                    return new List<SystemType>();
                }
            }
        }

        public SystemType Get_SystemTypeById(long systemTypeCode)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypes.Find(systemTypeCode);
                }
                catch
                {
                    return null;
                }
            }
        }

        public SystemType Get_SystemTypeByCode(string systemCode)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SystemTypes.Where(a => a.SystemCode == systemCode).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(SystemType systemType)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    systemType.DateCreated = DateTime.Now;
                    systemType.Version = "1.0.0.0.0.1";

                    _data.SystemTypes.Add(systemType);
                    _data.SaveChanges();


                    return systemType.SystemTypeId;
                }
                catch { return -1; }

            }
        }

        public bool Update(SystemType systemType)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    SystemType SystemTypeToUpdate = _data.SystemTypes.Where(x => x.SystemTypeId == systemType.SystemTypeId).FirstOrDefault();
                    SystemTypeToUpdate.SystemName = systemType.SystemName;
                    SystemTypeToUpdate.SystemCode = systemType.SystemCode;
                    SystemTypeToUpdate.URL = systemType.URL;
                    SystemTypeToUpdate.Description = systemType.Description;

                    SystemTypeToUpdate.IsActive = systemType.IsActive;
                    SystemTypeToUpdate.IsDeleted = systemType.IsDeleted;
                    SystemTypeToUpdate.ModifiedBy = systemType.ModifiedBy;
                    SystemTypeToUpdate.DateModified = DateTime.Now;

                    SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();
                    var lst_SystemTypePermission = _iSystemTypePermissionService.GetList_SystemTypePermissionAll_SystemTypeId(systemType.SystemTypeId);

                    bool updateStatus = true;
                    if (SystemTypeToUpdate.IsDeleted == true)
                    {
                        foreach (var item in lst_SystemTypePermission)
                        {
                            updateStatus = _iSystemTypePermissionService.UpdateIsDeleted(item.SystemTypePermissionId, true);
                        }
                    }
                    else
                    {
                        foreach (var item in lst_SystemTypePermission)
                        {
                            if (item.IsDeleted == true)
                            {
                                updateStatus = _iSystemTypePermissionService.UpdateIsDeleted(item.SystemTypePermissionId, false);
                            }
                        }
                    }
                    _data.SaveChanges();

                    return true;

                }
                catch { return false; }
            }
        }

        public bool UpdateActive(long systemTypeId, bool isActive)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    SystemType systemType = _data.SystemTypes.Where(x => x.SystemTypeId == systemTypeId).FirstOrDefault();

                    systemType.IsActive = isActive;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }
    }
}
