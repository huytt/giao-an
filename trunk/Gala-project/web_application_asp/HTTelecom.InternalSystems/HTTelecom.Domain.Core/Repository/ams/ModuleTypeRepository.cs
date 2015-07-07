using HTTelecom.Domain.Core.DataContext.ams;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class ModuleTypeRepository
    {
        public IPagedList<ModuleType> GetList_ModuleTypeAll(int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_ModuleType = _data.ModuleTypes.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);

                    return lst_ModuleType;
                }
                catch
                {
                    return new PagedList<ModuleType>(new List<ModuleType>(), 1, pageSize);
                }
            }
        }

        public IList<ModuleType> GetList_ModuleTypeAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ModuleTypes.ToList();
                }
                catch
                {
                    return new List<ModuleType>();
                }
            }
        }

        public IList<ModuleType> GetList_ModuleTypeAll_SystemTypeId(long systemTypeId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ModuleTypes.Where(a => a.SystemTypeId == systemTypeId).OrderByDescending(b => b.DateCreated).ToList();
                }
                catch
                {
                    return new List<ModuleType>();
                }
            }
        }

        public IList<ModuleType> GetList_ModuleTypeAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ModuleTypes.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ModuleType>();
                }
            }
        }

        public IList<ModuleType> GetList_ModuleTypeAll_IsActive(bool IsActive)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ModuleTypes.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<ModuleType>();
                }
            }
        }
        public IList<ModuleType> GetList_ModuleTypeAll_IsActive_IsDelete(bool IsActived, bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ModuleTypes.Where(a => a.IsActive == IsActived && a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ModuleType>();
                }
            }
        }

        public ModuleType Get_ModuleTypeById(long ModuleTypeId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ModuleTypes.Find(ModuleTypeId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(ModuleType ModuleType)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    ModuleType.DateCreated = DateTime.Now;

                    _data.ModuleTypes.Add(ModuleType);
                    _data.SaveChanges();

                    return ModuleType.ModuleTypeId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(ModuleType ModuleType)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    ModuleType ModuleTypeToUpdate;
                    ModuleTypeToUpdate = _data.ModuleTypes.Where(x => x.ModuleTypeId == ModuleType.ModuleTypeId).FirstOrDefault();
                    ModuleTypeToUpdate.SystemTypeId = ModuleType.SystemTypeId;
                    ModuleTypeToUpdate.ModuleTypeName = ModuleType.ModuleTypeName;
                    ModuleTypeToUpdate.Version = ModuleType.Version;
                    ModuleTypeToUpdate.Description = ModuleType.Description;

                    ModuleTypeToUpdate.DateModified = DateTime.Now;
                    ModuleTypeToUpdate.ModifiedBy = ModuleType.ModifiedBy;
                    ModuleTypeToUpdate.IsActive = ModuleType.IsActive;
                    ModuleTypeToUpdate.IsDeleted = ModuleType.IsDeleted;

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

        public bool UpdateActive(long ModuleTypeId, bool isActive)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    ModuleType ModuleType = _data.ModuleTypes.Where(x => x.ModuleTypeId == ModuleTypeId).FirstOrDefault();

                    ModuleType.IsActive = isActive;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }
    }
}
