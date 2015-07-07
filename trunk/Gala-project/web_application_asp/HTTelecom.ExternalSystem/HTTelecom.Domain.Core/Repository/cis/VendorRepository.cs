using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class VendorRepository
    {
        public List<Vendor> GetAll(bool IsDeleted)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Vendor.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Vendor>();
            }
        }
        public Vendor GetById(long VendorId)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Vendor.Find(VendorId);
            }
            catch
            {
                return null;
            }
        }
    }
}
