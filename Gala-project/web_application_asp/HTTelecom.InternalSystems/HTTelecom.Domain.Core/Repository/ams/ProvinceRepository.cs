using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.IRepository.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class ProvinceRepository : IProvinceRepository
    {
        public List<Province> GetAll()
        {
            try
            {
                AMS_DBEntities _data = new AMS_DBEntities();
                return _data.Provinces.ToList();
            }
            catch
            {
                return new List<Province>();
            }
        }

        public Province GetById(long ProvinceId)
        {
            try
            {
                AMS_DBEntities _data = new AMS_DBEntities();
                return _data.Provinces.Find(ProvinceId);
            }
            catch
            {
                return null;
            }
        }
    }
}
