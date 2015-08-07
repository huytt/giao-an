using HTTelecom.Domain.Core.DataContext.ams;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class ActionTypeRepository
    {
        public IPagedList<ActionType> GetList_ActionTypeAll(int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    var lst_ActionType = _data.ActionTypes.Include("SystemType").OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);

                    return lst_ActionType;
                }
                catch
                {
                    return new PagedList<ActionType>(new List<ActionType>(), 1, pageSize);
                }
            }
        }

        public IList<ActionType> GetList_ActionTypeAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypes.ToList();
                }
                catch
                {
                    return new List<ActionType>();
                }
            }
        }

        public IList<ActionType> GetList_ActionTypeAll_SystemTypeId(long systemTypeId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    var lst_ActionType = _data.ActionTypes
                                              .Include("SystemType")
                                              .Where(a => a.SystemTypeId == systemTypeId)
                                              .OrderByDescending(b => b.DateCreated)
                                              .ToList();

                    return lst_ActionType;
                }
                catch
                {
                    return new List<ActionType>();
                }
            }
        }

        //public IList<ActionType> GetList_ActionTypeAll_ModuleTypeId(long moduleTypeId)
        //{
        //    using (AMS_DBEntities _data = new AMS_DBEntities())
        //    {
        //        _data.Configuration.ProxyCreationEnabled = false;
        //        _data.Configuration.LazyLoadingEnabled = false;
        //        try
        //        {
        //            return _data.ActionTypes.Where(a => a.ModuleTypeId == moduleTypeId).OrderByDescending(b => b.DateCreated).ToList();
        //        }
        //        catch
        //        {
        //            return new List<ActionType>();
        //        }
        //    }
        //}

        public IList<ActionType> GetList_ActionTypeAll(bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypes.Include("SystemType").Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ActionType>();
                }
            }
        }

        public IList<ActionType> GetList_ActionTypeAll(bool IsActived, bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypes.Where(a => a.IsActive == IsActived && a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ActionType>();
                }
            }
        }

        public IList<ActionType> GetList_ActionTypeAll_IsActive(bool IsActive)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypes.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<ActionType>();
                }
            }
        }
        public ActionType Get_ActionTypeById(long ActionTypeId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.ActionTypes.Find(ActionTypeId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(ActionType ActionType)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
               
                try
                {
                    ActionType.DateCreated = DateTime.Now;
                    
                    _data.ActionTypes.Add(ActionType);
                    _data.SaveChanges();

                    return ActionType.ActionTypeId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(ActionType ActionType)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    ActionType ActionTypeToUpdate;
                    ActionTypeToUpdate = _data.ActionTypes.Where(x => x.ActionTypeId == ActionType.ActionTypeId).FirstOrDefault();
                    ActionTypeToUpdate.ParentId = ActionType.ParentId;
                    ActionTypeToUpdate.ActionTypeName = ActionType.ActionTypeName;
                    ActionTypeToUpdate.URL = ActionType.URL;
                    ActionTypeToUpdate.Description = ActionType.Description;

                    ActionTypeToUpdate.DateModified = DateTime.Now;
                    ActionTypeToUpdate.ModifiedBy = ActionType.ModifiedBy;
                    ActionTypeToUpdate.IsActive = ActionType.IsActive;
                    ActionTypeToUpdate.IsDeleted = ActionType.IsDeleted;

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

        public bool UpdateActive(long ActionTypeId, bool isActive)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    ActionType ActionType = _data.ActionTypes.Where(x => x.ActionTypeId == ActionTypeId).FirstOrDefault();

                    ActionType.IsActive = isActive;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }
    }
}
