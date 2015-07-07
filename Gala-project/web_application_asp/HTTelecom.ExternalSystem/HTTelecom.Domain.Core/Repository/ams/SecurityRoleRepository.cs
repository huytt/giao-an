using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class SecurityRoleRepository
    {
        public SecurityRole Get_SecurityRoleByCode(string securityRoleCode)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                return entities.SecurityRole.Where(a => a.SecurityRoleCode == securityRoleCode).FirstOrDefault();
            }
        }

    }
}
