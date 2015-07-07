using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductInPriorityRepository
    {        
        public ProductInPriority Get_ProductInPriorityById(long _ProductInPriorityID)
        {            
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ProductInPriority.Find(_ProductInPriorityID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<ProductInPriority> GetList_ProductInPriorityAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductInPriority.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<ProductInPriority>();
                }
            }
        }


        public long InsertProductInPriority(ProductInPriority _ProductInPriority)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.ProductInPriority.Add(_ProductInPriority);
                    _data.SaveChanges();
                    return _ProductInPriority.ProductInPriorityId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public bool UpdateProductInPriority(ProductInPriority _ProductInPriority)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    ProductInPriority ProductInPriorityToUpdate;
                    ProductInPriorityToUpdate = entities.ProductInPriority.Where(x => x.ProductInPriorityId == _ProductInPriority.ProductInPriorityId).FirstOrDefault();
                    
                    ProductInPriorityToUpdate.IsDeleted = _ProductInPriority.IsDeleted ?? ProductInPriorityToUpdate.IsDeleted;
                    entities.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return false;
                }
            }
        }


        public IList<ProductInPriority> GetList_ProductInPriorityWithType(long _GroupPiorityId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductInPriority.Where(d => d.GroupPriorityId == _GroupPiorityId).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<ProductInPriority>();
                }
            }
        }
    }
}
