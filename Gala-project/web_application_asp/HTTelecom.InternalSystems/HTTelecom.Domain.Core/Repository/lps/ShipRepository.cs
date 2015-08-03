
using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.lps
{
    public class ShipRepository
    {
        public IList<Ship> GetList_ShipAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Ship.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Ship>();
                }
            }
        }

        public long InsertShip(Ship _Ship)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    _data.Ship.Add(_Ship);
                    _data.SaveChanges();
                    return _Ship.ShipId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public bool UpdateShip(Ship _Ship)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    Ship ShipToUpdate;
                    ShipToUpdate = entities.Ship.Where(x => x.ShipId == _Ship.ShipId).FirstOrDefault();
                    ShipToUpdate.Price = _Ship.Price ?? ShipToUpdate.Price;
                    ShipToUpdate.Type = _Ship.Type ?? ShipToUpdate.Type;
                    ShipToUpdate.TargetId = _Ship.TargetId ?? ShipToUpdate.TargetId;
                    ShipToUpdate.IsDeleted = _Ship.IsDeleted ?? ShipToUpdate.IsDeleted;
                    entities.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return false;
                }
            }
        }


        public Ship Get_ShipById(long _ShipID)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Ship.Find(_ShipID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }

        public IList<Ship> GetList_ShipAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Ship.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Ship>();
                }
            }
        }

        public long CheckExists(string type, long? target)//nếu trả về true thid khu vực đã tồn tại hoặc hàm chạy lỗi => không thêm khu vực mới
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    if (type == "" || target == null)
                    {
                        return -1;
                    }
                    var count = entities.Ship.Where(x => x.Type == type && x.TargetId == target).ToList();
                    if (count.Count > 0)
                    {
                        return count[0].ShipId;
                    }
                    return -1;
                }
                catch
                {
                    return -1;
                }
            }
        }


        public decimal ChangePrice(long ShipId, decimal Price)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    Ship ShipToUpdate;
                    ShipToUpdate = entities.Ship.Where(x => x.ShipId == ShipId).FirstOrDefault();
                    ShipToUpdate.Price = Price;
                    entities.SaveChanges();
                    return (decimal)Price;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public decimal ChangeFreeShip(long ShipId, decimal FreeShip)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    Ship ShipToUpdate;
                    ShipToUpdate = entities.Ship.Where(x => x.ShipId == ShipId).FirstOrDefault();
                    ShipToUpdate.FreeShip = FreeShip;
                    entities.SaveChanges();
                    return (decimal)FreeShip;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
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
    }
}
