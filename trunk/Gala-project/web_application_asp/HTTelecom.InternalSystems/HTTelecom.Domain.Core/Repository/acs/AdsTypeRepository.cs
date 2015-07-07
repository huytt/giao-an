using HTTelecom.Domain.Core.DataContext.acs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.acs
{
    public class AdsTypeRepository
    {
        private ACS_DBEntities _data;
        public AdsTypeRepository()
        {
            _data = new ACS_DBEntities();
        }
        public List<AdsType> GetAll(bool IsDeleted,bool IsActive)
        {

            try
            {
                return _data.AdsTypes.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
            catch
            {
                return new List<AdsType>();
            }
        }
        public AdsType GetById(long id)
        {

            try
            {
                return _data.AdsTypes.Find(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
