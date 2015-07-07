using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class SecurityRoleRepository
    {
        public IPagedList<SecurityRole> GetList_SecurityRoleAll(int pageNum, int pageSize)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SecurityRoles.OrderBy(a => a.LevelRole).ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<SecurityRole>(new List<SecurityRole>(), 1, pageSize);
                }
            }
        }

        public IList<SecurityRole> GetList_SecurityRoleAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SecurityRoles.OrderBy(a => a.LevelRole).ToList();
                }
                catch
                {
                    return new List<SecurityRole>();
                }
            }
        }

        public IList<SecurityRole> GetList_SecurityRoleAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SecurityRoles.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<SecurityRole>();
                }
            }
        }

        public IList<SecurityRole> GetList_SecurityRoleAll_IsActived(bool IsActived)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SecurityRoles.Where(a => a.IsActive == IsActived).ToList();
                }
                catch
                {
                    return new List<SecurityRole>();
                }
            }
        }

        public IList<SecurityRole> GetList_SecurityRoleAll_IsActive_IsDeleted(bool IsActive, bool IsDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.SecurityRoles.Where(a => a.IsActive == IsActive && a.IsDeleted == IsDeleted).ToList();
                }
                catch
                {
                    return new List<SecurityRole>();
                }
            }
        }

        public SecurityRole Get_SecurityRoleById(long securityRoleId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SecurityRoles.Find(securityRoleId);
                }
                catch { return null; }
            }
        }

        public SecurityRole Get_SecurityRoleByCode(string securityRoleCode)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.SecurityRoles.Where(a => a.SecurityRoleCode == securityRoleCode).FirstOrDefault();
                }
                catch { return null; }
            }
        }

        public long Insert(SecurityRole securityRole)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    int levelRole = this.GetList_SecurityRoleAll_IsDeleted(false).Count + 1;

                    securityRole.DateCreated = DateTime.Now;
                    securityRole.LevelRole = levelRole;

                    _data.SecurityRoles.Add(securityRole);
                    _data.SaveChanges();

                    return securityRole.SecurityRoleId;
                }
                catch { return -1; }

            }
        }

        public bool Update(SecurityRole securityRole, int? levelRoleCurrent)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    SecurityRole SecurityRoleToUpdate;
                    SecurityRoleToUpdate = _data.SecurityRoles.Where(x => x.SecurityRoleId == securityRole.SecurityRoleId).FirstOrDefault();

                    SecurityRoleToUpdate.SecurityRoleName = securityRole.SecurityRoleName;
                    securityRole.SecurityRoleCode = securityRole.SecurityRoleCode;
                    SecurityRoleToUpdate.LevelRole = securityRole.LevelRole;
                    SecurityRoleToUpdate.Description = securityRole.Description;

                    SecurityRoleToUpdate.DateModified = DateTime.Now;
                    SecurityRoleToUpdate.ModifiedBy = securityRole.ModifiedBy;
                    SecurityRoleToUpdate.IsActive = securityRole.IsActive;
                    SecurityRoleToUpdate.IsDeleted = securityRole.IsDeleted;

                    if (securityRole.IsDeleted == true)
                    {
                        var lst_SecurityRole = _data.SecurityRoles.Where(x => x.IsDeleted == false
                                                        && x.LevelRole > levelRoleCurrent
                                                        ).ToList();
                        foreach (var systemRole in lst_SecurityRole)
                        {
                            this.UpdateLevelRole(systemRole.SecurityRoleId, (int)systemRole.LevelRole - 1);
                        }

                        this.UpdateLevelRole(securityRole.SecurityRoleId, null);
                    }
                    else
                    {
                        if (securityRole.LevelRole == null)
                        {
                            SecurityRoleToUpdate.LevelRole = levelRoleCurrent;
                        }
                    }

                    if (levelRoleCurrent != securityRole.LevelRole)
                    {
                        IList<SecurityRole> lst_SecurityRole = new List<SecurityRole>();
                        if (levelRoleCurrent < securityRole.LevelRole)
                        {
                            lst_SecurityRole = _data.SecurityRoles.Where(x => x.IsDeleted == false
                                                        && x.LevelRole > levelRoleCurrent && x.LevelRole <= securityRole.LevelRole
                                                        ).ToList();
                        }
                        else
                        {
                            lst_SecurityRole = _data.SecurityRoles.Where(x => x.IsDeleted == false
                                                        && x.LevelRole < levelRoleCurrent && x.LevelRole >= securityRole.LevelRole
                                                        ).ToList();
                        }

                        foreach (var systemRole in lst_SecurityRole)
                        {
                            if (levelRoleCurrent < securityRole.LevelRole)
                                systemRole.LevelRole = systemRole.LevelRole - 1;
                            else
                                systemRole.LevelRole = systemRole.LevelRole + 1;

                            this.UpdateLevelRole(systemRole.SecurityRoleId, (int)systemRole.LevelRole);
                        }
                    }



                    //4. call SaveChanges
                    _data.SaveChanges();

                    return true;
                }
                catch { return false; }
            }
        }

        private bool UpdateLevelRole(long securityRoleId, int? levelRole)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    SecurityRole securityRole = _data.SecurityRoles.Where(x => x.SecurityRoleId == securityRoleId).FirstOrDefault();

                    securityRole.LevelRole = levelRole;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public bool UpdateActive(long securityRoleId, bool isActive)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    SecurityRole securityRole = _data.SecurityRoles.Where(x => x.SecurityRoleId == securityRoleId).FirstOrDefault();

                    securityRole.IsActive = isActive;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }
    }
}
