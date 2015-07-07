using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class SystemTypePermissionRepository
    {
        public SystemTypePermission Get_SystemTypePermissionIsAccount(long securityRoleId, long systemTypeId)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    return entities.SystemTypePermission.Where(a => a.SecurityRoleId == securityRoleId).Where(b => b.SystemTypeId == systemTypeId).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
