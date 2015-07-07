using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductInCategoryRepository
    {

        public IList<ProductInCategory> GetList_ProductInCategoryAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInCategory.ToList();
                }
                catch
                {
                    return new List<ProductInCategory>();
                }
            }
        }

        public IList<ProductInCategory> GetList_ProductInCategoryAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInCategory.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ProductInCategory>();
                }
            }
        }

        public IList<ProductInCategory> GetList_ProductInCategoryAll_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                return _data.ProductInCategory.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<ProductInCategory>();
                }
            }
        }

        public ProductInCategory Get_ProductInCategoryById(long ProductInCategoryId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInCategory.Find(ProductInCategoryId);
                }
                catch
                {
                    return null;
                }
            }
        }
        public IList<ProductInCategory> GetList_ProductInCategory_ProductId(long ProductId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductInCategory.Where(a => a.ProductId == ProductId).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(ProductInCategory ProductInCategory)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    ProductInCategory.IsActive = true;
                    ProductInCategory.IsDeleted = false;

                    _data.ProductInCategory.Add(ProductInCategory);
                    _data.SaveChanges();

                    return ProductInCategory.ProductInCategoryId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(ProductInCategory ProductInCategory)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    ProductInCategory ProductInCategoryToUpdate;
                    ProductInCategoryToUpdate = _data.ProductInCategory.Where(x => x.ProductInCategoryId == ProductInCategory.ProductInCategoryId).FirstOrDefault();
                    
                    ProductInCategoryToUpdate.ProductId = ProductInCategory.ProductId;
                    ProductInCategoryToUpdate.CategoryId = ProductInCategory.CategoryId;
                    ProductInCategoryToUpdate.IsActive = ProductInCategory.IsActive;
                    ProductInCategoryToUpdate.IsDeleted = ProductInCategory.IsDeleted;
                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool UpdateIsActive(long id, bool? IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    ProductInCategory ProductInCategoryToUpdate;
                    ProductInCategoryToUpdate = _data.ProductInCategory.Where(x => x.ProductInCategoryId == id).FirstOrDefault();

                    ProductInCategoryToUpdate.IsActive = IsActive ?? ProductInCategoryToUpdate.IsActive;
                    
                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsDeleteCategory(long _categoryId,bool _isDelete)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {

                try
                {
                    List<ProductInCategory> lst_ProductInCategoryToUpdate;
                    lst_ProductInCategoryToUpdate = this.GetList_ProductInCategoryAll().Where(x => x.CategoryId == _categoryId).ToList();
                    foreach (ProductInCategory item in lst_ProductInCategoryToUpdate)
                    {
                        ProductInCategory ProductInCategoryToUpdate;
                        ProductInCategoryToUpdate = entities.ProductInCategory.Where(x => x.ProductInCategoryId == item.ProductInCategoryId).FirstOrDefault();
                        ProductInCategoryToUpdate.IsDeleted = _isDelete;
                        entities.SaveChanges();
                    }
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
