using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.DataContext.lps;
using HTTelecom.Domain.Core.Repository.lps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.lps
{
    public class WeightRepository
    {
        public IList<Weight> GetList_WeightAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Weight.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Weight>();
                }
            }
        }

        public long InsertWeight(Weight _Weight)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    _data.Weight.Add(_Weight);
                    _data.SaveChanges();
                    return _Weight.WeightId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public bool UpdateWeight(Weight _Weight)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    Weight WeightToUpdate;
                    WeightToUpdate = entities.Weight.Where(x => x.WeightId == _Weight.WeightId).FirstOrDefault();
                    WeightToUpdate.Price = _Weight.Price??WeightToUpdate.Price;
                    WeightToUpdate.Type = _Weight.Type??WeightToUpdate.Type;
                    WeightToUpdate.TargetId = _Weight.TargetId??WeightToUpdate.TargetId;
                    WeightToUpdate.IsDeleted = _Weight.IsDeleted??WeightToUpdate.IsDeleted;
                    WeightToUpdate.DateLimited = _Weight.DateLimited??WeightToUpdate.DateLimited;
                    WeightToUpdate.WeightFrom = _Weight.WeightFrom ?? WeightToUpdate.WeightFrom;
                    WeightToUpdate.WeightTo = _Weight.WeightTo ?? WeightToUpdate.WeightTo;
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

      
        public Weight Get_WeightById(long _WeightID)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Weight.Find(_WeightID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }

        public IList<Weight> GetList_WeightAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Weight.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Weight>();
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
                    var count = entities.Weight.Where(x => x.Type == type && x.TargetId == target).ToList();
                    if (count.Count>0)
                    {
                        return count[0].WeightId;
                    }
                    return -1;
                }
                catch
                {
                    return -1;
                }
            }
        }


        public decimal ChangePrice(long WeightId, decimal Price)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    Weight WeightToUpdate;
                    WeightToUpdate = entities.Weight.Where(x => x.WeightId == WeightId).FirstOrDefault();
                    WeightToUpdate.Price = Price;
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
    }
}
