using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductStatusRepository
    {
        public List<ProductStatus> GetAll(bool IsDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.ProductStatus.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
        }

        public ProductStatus GetByCode(string code)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.ProductStatus.Where(n => n.ProductStatusCode == code).FirstOrDefault();
            }
        }
    }
}
