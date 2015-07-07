using HTTelecom.Domain.Core.DataContext.lps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.lps
{
    public class ProductInventoryRepository
    {
        public List<ProductInventory> GetAll(bool IsDeleted)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    return _data.ProductInventory.ToList();
                }
            }
            catch
            {
                return new List<ProductInventory>();
            }
        }
        public List<ProductInventory> GetByProduct(long ProductId)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    return _data.ProductInventory.Where(n => n.ProductId == ProductId).ToList() ;
                }
            }
            catch
            {
                return new List<ProductInventory>();
            }
        }
    }
}
