using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class PermissionRepository
    {
        //public IPagedList<Permission> GetList_PermissionAll(int pageNum, int pageSize)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        _data.Configuration.ProxyCreationEnabled = false;
        //        _data.Configuration.LazyLoadingEnabled = false;
        //        try
        //        {
        //            var lst_Permission = _data.Permissions.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);

        //            return lst_Permission;
        //        }
        //        catch
        //        {
        //            return new PagedList<Permission>(new List<Permission>(), 1, pageSize);
        //        }
        //    }
        //}

        //public IList<Permission> GetList_PermissionAll()
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        _data.Configuration.ProxyCreationEnabled = false;
        //        _data.Configuration.LazyLoadingEnabled = false;
        //        try
        //        {
        //            return _data.Permissions.ToList();
        //        }
        //        catch
        //        {
        //            return new List<Permission>();
        //        }
        //    }
        //}

        //public IList<Permission> GetList_PermissionAll_IsDeleted(bool isDeleted)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.Permissions.Where(a => a.IsDeleted == isDeleted).ToList();
        //        }
        //        catch
        //        {
        //            return new List<Permission>();
        //        }
        //    }
        //}

        //public IList<Permission> GetList_PermissionAll_IsActive(bool IsActive)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.Permissions.Where(a => a.IsActive == IsActive).ToList();
        //        }
        //        catch
        //        {
        //            return new List<Permission>();
        //        }
        //    }
        //}
        //public IList<Permission> GetList_PermissionAll_IsActive_IsDelete(bool IsActived, bool isDeleted)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.Permissions.Where(a => a.IsActive == IsActived && a.IsDeleted == isDeleted).ToList();
        //        }
        //        catch
        //        {
        //            return new List<Permission>();
        //        }
        //    }
        //}

        //public Permission Get_PermissionById(long PermissionId)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.Permissions.Find(PermissionId);
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public Permission Get_PermissionByCode(string PermissionCode)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            return _data.Permissions.Where(a => a.PermissionCode == PermissionCode).FirstOrDefault();
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public long Insert(Permission Permission)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            Permission.DateCreated = DateTime.Now;

        //            _data.Permissions.Add(Permission);
        //            _data.SaveChanges();

        //            return Permission.PermissionId;
        //        }
        //        catch
        //        {
        //            return -1;
        //        }

        //    }
        //}

        //public bool Update(Permission Permission)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            Permission PermissionToUpdate;
        //            PermissionToUpdate = _data.Permissions.Where(x => x.PermissionId == Permission.PermissionId).FirstOrDefault();
        //            PermissionToUpdate.PermissionKey = Permission.PermissionKey;
        //            PermissionToUpdate.PermissionName = Permission.PermissionName;
        //            PermissionToUpdate.PermissionCode = Permission.PermissionCode;
        //            PermissionToUpdate.Description = Permission.Description;

        //            PermissionToUpdate.DateModified = DateTime.Now;
        //            PermissionToUpdate.ModifiedBy = Permission.ModifiedBy;
        //            PermissionToUpdate.IsActive = Permission.IsActive;
        //            PermissionToUpdate.IsDeleted = Permission.IsDeleted;
                    
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

        //public bool UpdateActive(long permissionId, bool isActive)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        try
        //        {
        //            Permission permission = _data.Permissions.Where(x => x.PermissionId == permissionId).FirstOrDefault();

        //            permission.IsActive = isActive;
        //            _data.SaveChanges();
        //            return true;
        //        }
        //        catch { return false; }
        //    }
        //}
    }
}
