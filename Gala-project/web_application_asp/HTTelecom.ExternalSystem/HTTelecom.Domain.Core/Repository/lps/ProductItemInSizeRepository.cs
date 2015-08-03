using HTTelecom.Domain.Core.DataContext.lps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.lps
{
    public class ProductItemInSizeRepository
    {
        public List<ProductItemInSize> GetAll(bool IsDeleted)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    return _data.ProductItemInSize.ToList();
                }
            }
            catch
            {
                return new List<ProductItemInSize>();
            }
        }
        public List<ProductItemInSize> GetByProductItem(long ProductItemId)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    return _data.ProductItemInSize.Where(n => n.ProductItemId == ProductItemId).ToList();
                }
            }
            catch
            {
                return new List<ProductItemInSize>();
            }
        }

        public bool UpdateDownQuantity(List<Tuple<long, string, int>> lstProductItemInSize)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    foreach (var item in lstProductItemInSize)
                    {
                        var ProductItem = _data.ProductItem.Where(n => n.ProductCode == item.Item2).FirstOrDefault();
                        if (ProductItem != null)
                        {
                            var rs = _data.ProductItemInSize.Where(n => n.SizeId == item.Item1 && n.ProductItemId == ProductItem.ProductItemId).FirstOrDefault();
                            rs.Quantity = rs.Quantity <= item.Item3 ? 0 : rs.Quantity - item.Item3;
                            _data.SaveChanges();
                        }
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Tuple<long, string, long, bool>> GetByListProduct(List<Tuple<long, string>> lstSizeProduct)
        {
            //input: ProductId, ProductStokCode
            //output: ProductId, ProductStokCode, SizeId, true{còn hang}: false{hết hàng}
            try
            {
                var lst = new List<Tuple<long, string, long, bool>>();
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    foreach (var item in lstSizeProduct)
                    {
                        var ProductItem = _data.ProductItem.Where(n => n.ProductCode == item.Item2).FirstOrDefault();
                        if (ProductItem != null)
                        {
                            var rs = _data.ProductItemInSize.Where(n => n.ProductItemId == ProductItem.ProductItemId).ToList();
                            foreach (var item_Size in rs)
                            {
                                if (item_Size.Quantity > 0)
                                    lst.Add(new Tuple<long, string, long, bool>(item.Item1, item.Item2, Convert.ToInt64(item_Size.SizeId), true));
                                else lst.Add(new Tuple<long, string, long, bool>(item.Item1, item.Item2, Convert.ToInt64(item_Size.SizeId), false));
                            }
                            //rs.Quantity = rs.Quantity <= item.Item3 ? 0 : rs.Quantity - item.Item3;
                        }
                    }
                    return lst;
                }
            }
            catch
            {
                return new List<Tuple<long, string, long, bool>>();
            }
        }
    }
}
