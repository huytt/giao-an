using HTTelecom.Domain.Core.DataContext.lps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.lps
{
    public class ProductItemRepository
    {
        public List<ProductItem> GetAll(bool IsDeleted, bool IsVerified, bool IsActive)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    return _data.ProductItem.Where(n => n.IsDeleted == IsDeleted && n.IsVerified == IsVerified && n.IsActive == IsActive).ToList();
                }
            }
            catch
            {
                return new List<ProductItem>();
            }
        }

        public List<ProductItem> GetAllFollowVendor(bool IsDeleted, bool IsVerified, bool IsActive,long VendorId)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    return _data.ProductItem.Where(n => n.IsDeleted == IsDeleted //&& n.IsVerified == IsVerified 
                        && n.IsActive == IsActive&& n.VendorId == VendorId).ToList();
                }
            }
            catch
            {
                return new List<ProductItem>();
            }
        }
        public ProductItem GetById(long Id)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    return _data.ProductItem.Find(Id);
                }
            }
            catch
            {
                return null;
            }
        }
        public ProductItem GetByProductCode(string code)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    return _data.ProductItem.Where(n=>n.ProductCode == code).FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
