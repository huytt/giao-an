using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class RolePermissionRepository
    {
        //public IPagedList<RolePermission> GetList_RolePermissionAll(int pageNum, int pageSize)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        _data.Configuration.ProxyCreationEnabled = false;
        //        _data.Configuration.LazyLoadingEnabled = false;
        //        try
        //        {
        //            var lst_RolePermission = _data.RolePermissions.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);

        //            return lst_RolePermission;
        //        }
        //        catch
        //        {
        //            return new PagedList<RolePermission>(new List<RolePermission>(), 1, pageSize);
        //        }
        //    }
        //}

        //public IList<RolePermission> GetList_RolePermissionAll()
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        _data.Configuration.ProxyCreationEnabled = false;
        //        _data.Configuration.LazyLoadingEnabled = false;
        //        try
        //        {
        //            return _data.RolePermissions.ToList();
        //        }
        //        catch
        //        {
        //            return new List<RolePermission>();
        //        }
        //    }
        //}

        //public IList<RolePermission> GetList_RolePermissionAll_IsDeleted(bool isDeleted)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.RolePermissions.Where(a => a.IsDeleted == isDeleted).ToList();
        //        }
        //        catch
        //        {
        //            return new List<RolePermission>();
        //        }
        //    }
        //}

        //public IList<RolePermission> GetList_RolePermissionAll_IsActive(bool IsActive)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.RolePermissions.Where(a => a.IsActive == IsActive).ToList();
        //        }
        //        catch
        //        {
        //            return new List<RolePermission>();
        //        }
        //    }
        //}
        //public IList<RolePermission> GetList_RolePermissionAll_IsActive_IsDeleted(bool IsActived, bool isDeleted)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.RolePermissions.Where(a => a.IsActive == IsActived && a.IsDeleted == isDeleted).ToList();
        //        }
        //        catch
        //        {
        //            return new List<RolePermission>();
        //        }
        //    }
        //}

        //public RolePermission Get_RolePermissionById(long RolePermissionId)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.RolePermissions.Find(RolePermissionId);
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public long Insert(RolePermission RolePermission)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            RolePermission.DateCreated = DateTime.Now;

        //            _data.RolePermissions.Add(RolePermission);
        //            _data.SaveChanges();

        //            return RolePermission.RolePermissionId;
        //        }
        //        catch
        //        {
        //            return -1;
        //        }

        //    }
        //}

        //public bool Update(RolePermission RolePermission)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            RolePermission RolePermissionToUpdate;
        //            RolePermissionToUpdate = _data.RolePermissions.Where(x => x.RolePermissionId == RolePermission.RolePermissionId).FirstOrDefault();
        //            RolePermissionToUpdate.SecurityRoleId = RolePermission.SecurityRoleId;
        //            RolePermissionToUpdate.PermissionId = RolePermission.PermissionId;

        //            RolePermissionToUpdate.DateModified = DateTime.Now;
        //            RolePermissionToUpdate.ModifiedBy = RolePermission.ModifiedBy;
        //            RolePermissionToUpdate.IsActive = RolePermission.IsActive;
        //            RolePermissionToUpdate.IsDeleted = RolePermission.IsDeleted;
                    
        //            //4. call SaveChanges
        //            _data.SaveChanges();

        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //}

        //public bool UpdateActive(long RolePermissionId, bool isActive)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            RolePermission RolePermission = _data.RolePermissions.Where(x => x.RolePermissionId == RolePermissionId).FirstOrDefault();

        //            RolePermission.IsActive = isActive;
        //            _data.SaveChanges();
        //            return true;
        //        }
        //        catch { return false; }
        //    }
        //}
    }
}
