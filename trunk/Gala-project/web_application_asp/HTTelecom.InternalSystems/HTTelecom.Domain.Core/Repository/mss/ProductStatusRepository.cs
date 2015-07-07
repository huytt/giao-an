using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductStatusRepository
    {
        public IList<ProductStatus> GetList_ProductStatusAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductStatus.ToList();
                }
                catch
                {
                    return new List<ProductStatus>();
                }
            }
        }

        public IList<ProductStatus> GetList_ProductStatusAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductStatus.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ProductStatus>();
                }
            }
        }

        public IList<ProductStatus> GetList_ProductStatus_IsDeleted_IsActive(bool isDeleted, bool isActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductStatus.Where(a => a.IsDeleted == isDeleted)
                                              .Where(b => b.IsActive == isActive).ToList();
                }
                catch
                {
                    return new List<ProductStatus>();
                }
            }
        }

        public IList<ProductStatus> GetList_ProductStatusAll_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductStatus.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<ProductStatus>();
                }
            }
        }

        public ProductStatus Get_ProductStatusById(long ProductStatusId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductStatus.Find(ProductStatusId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public ProductStatus Get_ProductStatusByCode(string ProductStatusCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.ProductStatus.Where(a => a.ProductStatusCode == ProductStatusCode).FirstOrDefault();
            }
        }

        public long Insert(ProductStatus ProductStatus)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.ProductStatus.Add(ProductStatus);
                    _data.SaveChanges();

                    return ProductStatus.ProductStatusId;
                }
                catch
                {
                    return -1;
                }

            }
        }
        public bool Update(ProductStatus ProductStatus)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    ProductStatus ProductStatusToUpdate;
                    ProductStatusToUpdate = entities.ProductStatus.Where(x => x.ProductStatusId == ProductStatus.ProductStatusId).FirstOrDefault();
                    ProductStatusToUpdate = ProductStatus;
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
