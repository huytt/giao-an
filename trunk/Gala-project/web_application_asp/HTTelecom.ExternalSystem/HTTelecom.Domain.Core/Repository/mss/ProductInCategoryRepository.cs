using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductInCategoryRepository
    {
        public List<ProductInCategory> GetAll(bool IsDeleted)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.ProductInCategory.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<ProductInCategory>();
            }
        }
        public List<ProductInCategory> GetByProduct(long ProductId)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.ProductInCategory.Where(n => n.ProductId == ProductId && n.IsActive == true && n.IsDeleted == false).ToList();
            }
            catch
            {
                return new List<ProductInCategory>();
            }
        }
    }
}
