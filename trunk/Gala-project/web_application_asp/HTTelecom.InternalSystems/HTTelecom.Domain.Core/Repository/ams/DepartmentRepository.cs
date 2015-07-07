using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class DepartmentRepository
    {
        public IList<Department> GetList_DepartmentAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Departments.ToList();
                }
                catch
                {
                    return new List<Department>();
                }
            }
        }

        public IList<Department> GetList_DepartmentAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Departments.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Department>();
                }
            }
        }

        public IList<Department> GetList_DepartmentAll_IsActive(bool IsActive)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Departments.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Department>();
                }
            }
        }
        public IList<Department> GetList_DepartmentAll_IsActive_IsDelete(bool IsActived, bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Departments.Where(a => a.IsActive == IsActived && a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Department>();
                }
            }
        }

        public Department Get_DepartmentById(long departmentId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Departments.Find(departmentId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public Department Get_DepartmentByCode(string departmentCode)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Departments.Where(a => a.DepartmentCode == departmentCode).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(Department department)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    _data.Departments.Add(department);
                    _data.SaveChanges();

                    return department.DepartmentId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(Department department)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    Department DepartmentToUpdate;
                    DepartmentToUpdate = _data.Departments.Where(x => x.DepartmentId == department.DepartmentId).FirstOrDefault();
                    DepartmentToUpdate = department;
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

        public Department GetByAccountId(long AccountId)
        {
            try
            {
                AMS_DBEntities _data = new AMS_DBEntities();
                var item = from a in _data.Accounts
                           join b in _data.Departments
                           on a.DepartmentId equals b.DepartmentId
                           where a.AccountId == AccountId
                           select b;

                return item.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
