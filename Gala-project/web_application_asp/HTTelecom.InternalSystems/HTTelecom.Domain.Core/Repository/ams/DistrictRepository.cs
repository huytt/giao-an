using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.IRepository.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class DistrictRepository : IDistrictRepository
    {
        public List<District> GetAll()
        {
            try
            {
                AMS_DBEntities _data = new AMS_DBEntities();
                return _data.Districts.ToList();
            }
            catch
            {
                return new List<District>();
            }
        }

        public District GetById(long DistrictId)
        {
            try
            {
                AMS_DBEntities _data = new AMS_DBEntities();
                return _data.Districts.Find(DistrictId);
            }
            catch
            {
                return null;
            }
        }
    }
}
