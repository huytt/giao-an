using HTTelecom.Domain.Core.DataContext.acs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.acs
{
    public class AdsCategoryRepository
    {
       private ACS_DBEntities _data;
       public AdsCategoryRepository()
        {
            _data = new ACS_DBEntities();
        }
       public List<AdsCategory> GetAll(bool IsDeleted, bool IsActive)
        {

            try
            {
                return _data.AdsCategories.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
            catch
            {
                return new List<AdsCategory>();
            }
        }
       public AdsCategory GetById(long id)
        {

            try
            {
                return _data.AdsCategories.Find(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
