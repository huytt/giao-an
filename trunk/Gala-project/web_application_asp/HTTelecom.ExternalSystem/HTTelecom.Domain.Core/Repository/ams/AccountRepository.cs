using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
   public  class AccountRepository
    {
       public long Get_ByRoleNSystem(string securityCode, string systemCode)
       {
           try
           {
               SystemTypeRepository _iSystemTypeService = new SystemTypeRepository();
               SecurityRoleRepository _iSecurityRoleService = new SecurityRoleRepository();
               SystemTypePermissionRepository _iSystemTypePermissionService = new SystemTypePermissionRepository();

               SystemType st = _iSystemTypeService.Get_SystemTypeByCode(systemCode);
               SecurityRole sr = _iSecurityRoleService.Get_SecurityRoleByCode(securityCode);
               SystemTypePermission stp = _iSystemTypePermissionService.Get_SystemTypePermissionIsAccount(sr.SecurityRoleId, st.SystemTypeId);

               return stp.AccountId;
           }
           catch
           {
               return -1;
           }
       }
    }
}
