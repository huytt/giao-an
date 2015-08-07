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
                    return _data.Weights.ToList();
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
                    _data.Weights.Add(_Weight);
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
                    WeightToUpdate = entities.Weights.Where(x => x.WeightId == _Weight.WeightId).FirstOrDefault();
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
                    return _data.Weights.Find(_WeightID);
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
                    return _data.Weights.Where(a => a.IsDeleted == isDeleted).ToList();
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
                    var count = entities.Weights.Where(x => x.Type == type && x.TargetId == target).ToList();
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
                    WeightToUpdate = entities.Weights.Where(x => x.WeightId == WeightId).FirstOrDefault();
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

        public decimal GetShippingFee(long ProvinceId, List<Tuple<long, int>> lstProduct)//xử lí tính toán
        {
            try
            {
                decimal _shippingfee = 0;
                //DistrictRepository _iDistrictService = new DistrictRepository();
                //ProvineRepository _iProvinceService = new ProvineRepository();
                WeightRepository _iWeightService = new WeightRepository();
                HTTelecom.Domain.Core.Repository.mss.ProductRepository _iProductService = new HTTelecom.Domain.Core.Repository.mss.ProductRepository();
                List<Weight> lstweight = _iWeightService.GetAll();
                //tính giá tiền từng sản phẩm theo khu vực, sau đó cộng lại
                foreach (var item in lstProduct)
                {
                    HTTelecom.Domain.Core.DataContext.mss.Product p = _iProductService.GetById(item.Item1);
                    int Quantity = item.Item2;

                    if (p.IsWeight != null && p.IsWeight == true)
                    {
                        decimal price = _iWeightService.GetShippingFeeOfProductAndArea(p.Weight * Quantity, "2", (long)ProvinceId);
                        if (price != -1)
                            _shippingfee += price;
                    }
                }
                return _shippingfee;
            }
            catch
            {
                return 0;
            }
        }

        public decimal GetShippingFeeOfProductAndArea(double? weight, string Type, long ProvinceId)
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    var tmp = _data.Weights.Where(n => n.IsDeleted == false
                        && n.Type == Type
                        && n.TargetId == ProvinceId).OrderBy(w => w.WeightTo).ToList();
                    int i = 0;
                    for (i = 0; i < tmp.Count; i++)
                    {
                        //nếu khối lượng nằm trong khoảng định giá thì break và return luôn
                        if ((double)tmp[i].WeightFrom < weight && weight <= (double)tmp[i].WeightTo //nằm trong khoảng [from ... to]
                            && tmp[i].WeightFrom != tmp[i].WeightTo)//ko thuộc phần tử cuối cùng (phần tử phụ thu)
                        {
                            break;
                        }
                    }
                    if (weight < tmp[tmp.Count - 1].WeightTo)//return giá
                    {
                        return (decimal)tmp[i].Price;
                    }
                    else// khối lượng vượt mức khoảng định giá, cứ thêm 1 Kg thì thêm bấy nhiêu phụ thu
                    {
                        int ww = (int)Math.Ceiling(((double)weight - (double)tmp[tmp.Count - 1].WeightTo) / 1000);
                        return (decimal)tmp[tmp.Count - 2].Price + (ww * (decimal)tmp[tmp.Count - 1].Price);
                    }
                }
            }
            catch
            {
                return -1;//lỗi, return -1
            }
        }

        public List<Weight> GetAll()
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    return _data.Weights.Where(n => n.IsDeleted == false).ToList();
                }
            }
            catch
            {
                return new List<Weight>();
            }
        }
    }
}
