using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class OrgRoleRepository
    {
        public IList<OrgRole> GetList_OrgRoleAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.OrgRoles.ToList();
                }
                catch
                {
                    return new List<OrgRole>();
                }
            }
        }

        public IList<OrgRole> GetList_OrgRoleAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.OrgRoles.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<OrgRole>();
                }
            }
        }

        public IList<OrgRole> GetList_OrgRoleAll_IsActive(bool IsActive)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.OrgRoles.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<OrgRole>();
                }
            }
        }

        public IList<OrgRole> GetList_OrgRoleAll_IsActive_IsDeleted(bool IsActive, bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.OrgRoles.Where(a => a.IsActive == IsActive && a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<OrgRole>();
                }
            }
        }

        public OrgRole Get_OrgRoleById(long orgRoleId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.OrgRoles.Find(orgRoleId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(OrgRole orgRole)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    _data.OrgRoles.Add(orgRole);
                    _data.SaveChanges();

                    return orgRole.OrgRoleId;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }

            }
        }

        public bool Update(OrgRole orgRole)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    OrgRole OrgRoleToUpdate;
                    OrgRoleToUpdate = _data.OrgRoles.Where(x => x.OrgRoleId == orgRole.OrgRoleId).FirstOrDefault();
                    OrgRoleToUpdate = orgRole;
                    //4. call SaveChanges
                    _data.SaveChanges();

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
