using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class ProvineRepository
    {
        public List<Province> GetAll()
        {
            try
            {
                AMS_DBEntities _data = new AMS_DBEntities();
                return _data.Province.ToList();
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
                return _data.Province.Find(ProvinceId);
            }
            catch
            {
                return null;
            }
        }

        public List<Province> GetByListWeight(List<Weight> lstPro)
        {
            try
            {
                var lst = new List<Province>();
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    foreach (var item in lstPro)
                    {
                        var rs = _data.Province.Find(item.TargetId);
                        lst.Add(rs);
                    }
                }
                return lst;
            }
            catch
            {
                return new List<Province>();
            }
        }
    }
}
