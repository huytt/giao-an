using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductTypeRepository
    {
        public IList<ProductType> GetList_ProductTypeAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductType.ToList();
                }
                catch
                {
                    return new List<ProductType>();
                }
            }
        }

        public IList<ProductType> GetList_ProductTypeAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductType.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ProductType>();
                }
            }
        }

        public IList<ProductType> GetList_ProductTypeAll_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductType.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<ProductType>();
                }
            }
        }

        public IList<ProductType> GetList_ProductTypeAll_IsDeleted_IsActive(bool isDeleted, bool isActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductType.Where(a => a.IsDeleted == isDeleted)
                                            .Where(b => b.IsActive == isActive).ToList();
                }
                catch
                {
                    return new List<ProductType>();
                }
            }
        }

        public ProductType Get_ProductTypeById(long ProductTypeId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductType.Find(ProductTypeId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public ProductType Get_ProductTypeByCode(string ProductTypeCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.ProductType.Where(a => a.ProductTypeCode == ProductTypeCode).FirstOrDefault();
            }
        }

        public long Insert(ProductType ProductType)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.ProductType.Add(ProductType);
                    _data.SaveChanges();

                    return ProductType.ProductTypeId;
                }
                catch
                {
                    return -1;
                }

            }
        }
        public bool Update(ProductType ProductType)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    ProductType ProductTypeToUpdate;
                    ProductTypeToUpdate = entities.ProductType.Where(x => x.ProductTypeId == ProductType.ProductTypeId).FirstOrDefault();
                    ProductTypeToUpdate = ProductType;
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
