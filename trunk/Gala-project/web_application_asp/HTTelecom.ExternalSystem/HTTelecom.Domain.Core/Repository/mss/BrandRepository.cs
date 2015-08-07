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
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Brand.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
        }

        #region app
        public List<Brand> GetAllApp(bool IsDeleted, bool IsActive)
        {
            MSS_DBEntities _data = new MSS_DBEntities();
            {
                return _data.Brand.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
        }

        #endregion
        public Brand GetById(long id)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    return _data.Brand.Find(id);
                }
                
            }
            catch
            {
                return null;
            }
        }

        public void UpdateVisitCount(long id)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    var brand = _data.Brand.Find(id);
                    brand.VisitCount = brand.VisitCount == null ? 1 : brand.VisitCount + 1;
                    _data.SaveChanges();
                }
            }
            catch
            {
            }
        }
    }
}
