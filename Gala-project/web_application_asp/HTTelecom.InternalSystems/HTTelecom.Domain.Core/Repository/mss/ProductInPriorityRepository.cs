using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductInPriorityRepository
    {
        public IList<ProductInPriority> GetList_ProductInPriorityAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInPriority.ToList();
                }
                catch
                {
                    return new List<ProductInPriority>();
                }
            }
        }

        public IList<ProductInPriority> GetList_ProductInPriorityAll(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInPriority.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ProductInPriority>();
                }
            }
        }

        public ProductInPriority Get_ProductInPriorityById(long ProductInPriorityId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInPriority.Find(ProductInPriorityId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(ProductInPriority ProductInPriority)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                   var tempOrder =_data.ProductInPriority.Where(n => n.GroupPriorityId == ProductInPriority.GroupPriorityId).Max(x => x.OrderNumber) ;
                  if (tempOrder == null)
                  {
                    ProductInPriority.OrderNumber = 1;
                  }
                  else
                  {
                      ProductInPriority.OrderNumber = tempOrder + 1;
                  }
                    _data.ProductInPriority.Add(ProductInPriority);
                    _data.SaveChanges();

                    return ProductInPriority.ProductInPriorityId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        //public long Insert(ProductInPriority ProductInPriority)
        //{
        //    using (MSS_DBEntities _data = new MSS_DBEntities())
        //    {
        //        try
        //        {
        //            //_data.ProductInPriority.Where(n=>n.GroupPriorityId== ProductInPriority.GroupPriorityId)
        //            var orderNumber = OrderNumber(Convert.ToInt64(ProductInPriority.GroupPriorityId));
        //            _data.ProductInPriority.Add(ProductInPriority);
        //            _data.SaveChanges();

        //            return ProductInPriority.ProductInPriorityId;
        //        }
        //        catch
        //        {
        //            return -1;
        //        }

        //    }
        //}

        //private int OrderNumber(long GroupPriorityId)
        //{
        //    using (MSS_DBEntities _data = new MSS_DBEntities())
        //    {
        //        var lst = _data.ProductInPriority.Where(n => n.GroupPriorityId == GroupPriorityId).ToList();
        //        return lst.Count + 1;

        //    }
        //}

        public bool Update(ProductInPriority ProductInPriority)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    ProductInPriority ProductInPriorityToUpdate;
                    ProductInPriorityToUpdate = entities.ProductInPriority.Where(x => x.ProductInPriorityId == ProductInPriority.ProductInPriorityId).FirstOrDefault();
                    ProductInPriorityToUpdate = ProductInPriority;
                    entities.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
