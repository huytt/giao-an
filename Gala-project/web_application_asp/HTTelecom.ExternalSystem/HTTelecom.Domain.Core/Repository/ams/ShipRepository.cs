using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class ShipRepository
    {
        public List<Ship> GetAll()
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    return _data.Ship.Where(n => n.IsDeleted == false).ToList();
                }
            }
            catch
            {
                return new List<Ship>();
            }
        }

        public Ship GetById(long id)
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    return _data.Ship.Find(id);
                }
            }
            catch
            {
                return null;
            }
        }
        public Ship GetByTarget(long TargetId, string type)
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    return _data.Ship.Where(n => n.TargetId == TargetId && n.Type == type).FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Ship> GetFreeShip()
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    var x = _data.Ship.GroupBy(n => n.FreeShip).ToList();
                    var lst = new  List<Ship>();
                    foreach (var item in x)
                    {
                        if (item.Count() == 1)
                        {
                            lst.Add(item.First());
                        }
                        else
                        {
                            var rs = item.First();
                            rs.TargetId = -1;
                            lst.Add(rs);
                        }
                    }
                    return lst;
                }
            }
            catch
            {
                return new List<Ship>();
            }
        }
    }
}
