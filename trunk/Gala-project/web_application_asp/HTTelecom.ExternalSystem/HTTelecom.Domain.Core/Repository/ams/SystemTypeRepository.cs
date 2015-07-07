using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class SystemTypeRepository
    {
        public SystemType Get_SystemTypeByCode(string systemCode)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                return entities.SystemType.Where(a => a.SystemCode == systemCode).FirstOrDefault();
            }
        }
    }
}
