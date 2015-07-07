using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class BrandRepository
    {
        public List<Brand> GetAll(bool IsDeleted, bool IsActive)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Brand.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive ).ToList();
            }
            catch
            {
                return new List<Brand>();
            }
        }

        public Brand GetById(long id)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Brand.Find(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
