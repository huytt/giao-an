using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class DepartmentGroupRepository
    {
        public IList<DepartmentGroup> GetList_DepartmentGroupAll()
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    return entities.DepartmentGroups.ToList();
                }
                catch
                {
                    return new List<DepartmentGroup>();
                }
            }
        }

        public IList<DepartmentGroup> GetList_DepartmentGroupAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    return entities.DepartmentGroups.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<DepartmentGroup>();
                }
            }
        }

        public IList<DepartmentGroup> GetList_DepartmentGroupAll_IsActive(bool IsActive)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    return entities.DepartmentGroups.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<DepartmentGroup>();
                }
            }
        }
        public IList<DepartmentGroup> GetList_DepartmentGroupAll_IsActive_IsDeleted(bool IsActive, bool isDeleted)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    return entities.DepartmentGroups.Where(a => a.IsActive == IsActive && a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<DepartmentGroup>();
                }
            }
        }
        public IList<DepartmentGroup> GetList_DepartmentGroup_DepartmentId(long departmentId)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    return entities.DepartmentGroups.Where(a => a.DepartmentId == departmentId).ToList();
                }
                catch
                {
                    return new List<DepartmentGroup>();
                }
            }
        }

        public DepartmentGroup Get_DepartmentGroupById(long departmentGroupId)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    return entities.DepartmentGroups.Find(departmentGroupId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(DepartmentGroup departmentGroup)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    entities.DepartmentGroups.Add(departmentGroup);
                    entities.SaveChanges();

                    return departmentGroup.DepartmentGroupId;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }

            }
        }

        public bool Update(DepartmentGroup departmentGroup)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    DepartmentGroup DepartmentGroupToUpdate;
                    DepartmentGroupToUpdate = entities.DepartmentGroups.Where(x => x.DepartmentGroupId == departmentGroup.DepartmentGroupId).FirstOrDefault();
                    DepartmentGroupToUpdate = departmentGroup;
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
    }
}
