﻿using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class DistrictRepository
    {
        public List<District> GetAll()
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    return _data.District.ToList();
                }
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
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    return _data.District.Find(DistrictId);
                }
            }
            catch
            {
                return null;
            }
        }

        public List<District> GetByListWeight(List<Weight> lstDis)
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    var lst = new List<District>();
                    foreach (var item in lstDis)
                    {
                        var rs = _data.District.Find(item.TargetId);
                        lst.Add(rs);
                    }
                    return lst;
                }
            }
            catch
            {
                return new List<District>();
            }
        }
    }
}
