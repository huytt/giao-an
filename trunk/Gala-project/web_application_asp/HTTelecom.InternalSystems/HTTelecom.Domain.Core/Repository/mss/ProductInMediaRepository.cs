using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductInMediaRepository
    {
        public IList<ProductInMedia> GetList_ProductInMediaAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInMedia.ToList();
                }
                catch
                {
                    return new List<ProductInMedia>();
                }
            }
        }

        public IList<ProductInMedia> Get_ProductInMedia_ProductId(long productId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductInMedia.Where(a => a.ProductId == productId).ToList();
                }
                catch
                {
                    return new List<ProductInMedia>();
                }
            }
        }

        public ProductInMedia Get_ProductInMediaById(long ProductInMediaId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInMedia.Find(ProductInMediaId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(ProductInMedia ProductInMedia)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.ProductInMedia.Add(ProductInMedia);
                    _data.SaveChanges();

                    return ProductInMedia.ProductInMediaId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(ProductInMedia ProductInMedia)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    ProductInMedia ProductInMediaToUpdate;
                    ProductInMediaToUpdate = entities.ProductInMedia.Where(x => x.ProductInMediaId == ProductInMedia.ProductInMediaId).FirstOrDefault();
                    ProductInMediaToUpdate = ProductInMedia;
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
