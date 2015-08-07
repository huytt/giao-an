using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class BrandInVendorRepository
    {
        public List<BrandInVendor> GetAll()
        {
            using (MSS_DBEntities data = new MSS_DBEntities())
            {
                return data.BrandInVendor.Where(_ => _.IsDeleted == false).ToList();
            }
        }
    }
}
