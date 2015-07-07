using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class WeightRepository
    {
        public List<Weight> GetAll()
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    return _data.Weight.Where(n => n.IsDeleted == false).ToList();
                }
            }
            catch
            {
                return new List<Weight>();
            }
        }

        public decimal GetShippingFeeOfProductAndArea(double? weight, string Type,long ProvinceId)
        {
            try
            {
                using (AMS_DBEntities _data = new AMS_DBEntities())
                {
                    var tmp = _data.Weight.Where(n => n.IsDeleted == false                  
                        && n.Type == Type
                        && n.TargetId == ProvinceId).OrderBy(w=>w.WeightTo).ToList();
                    int i = 0;
                    for (i = 0; i < tmp.Count;i++ )
                    {
                        //nếu khối lượng nằm trong khoảng định giá thì break và return luôn
                        if ((double)tmp[i].WeightFrom < weight && weight <= (double)tmp[i].WeightTo //nằm trong khoảng [from ... to]
                            && tmp[i].WeightFrom != tmp[i].WeightTo)//ko thuộc phần tử cuối cùng (phần tử phụ thu)
                        {
                            break;
                        }
                    }
                    if (weight<tmp[tmp.Count-1].WeightTo)//return giá
                    {
                        return (decimal)tmp[i].Price;
                    }
                    else// khối lượng vượt mức khoảng định giá, cứ thêm 1 Kg thì thêm bấy nhiêu phụ thu
                    {
                        int ww = (int)Math.Ceiling(((double)weight - (double)tmp[tmp.Count - 1].WeightTo)/1000);
                        return (decimal)tmp[tmp.Count - 2].Price + ( ww* (decimal)tmp[tmp.Count - 1].Price);
                    }                
                }
            }
            catch
            {
                return -1;//lỗi, return -1
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
                        if(price!=-1)
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
    }
}
