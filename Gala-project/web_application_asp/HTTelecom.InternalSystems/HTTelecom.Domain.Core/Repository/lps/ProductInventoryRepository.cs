using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.lps;
using HTTelecom.Domain.Core.Repository.lps;

namespace HTTelecom.Domain.Core.Repository.lps
{
    public class ProductInventoryRepository
    {
        public IList<ProductInventory> GetList_ProductInventoryAll()
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductInventory.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<ProductInventory>();
                }
            }
        }

        public ProductInventory Get_ProductInventoryById_ProductItemId(long ProductItemId)
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                try
                {
                    return _data.ProductInventory.Where(x=>x.ProductId == ProductItemId).OrderByDescending(o=>o.ProductInventoryId).ToList()[0];
                }
                catch (Exception ex)
                {
                   // System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }

        public long InsertProductInventory(ProductInventory _ProductInventory)
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                try
                {
                    _ProductInventory.DateCreated = DateTime.Now;
                    _data.ProductInventory.Add(_ProductInventory);
                    _data.SaveChanges();

                    return _ProductInventory.ProductInventoryId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }

            }
        }

        public bool UpdateProductInventory(ProductInventory _ProductInventory)
        {
            using (LPS_DBEntities entities = new LPS_DBEntities())
            {
                try
                {
                    ProductInventory ProductInventoryToUpdate;
                    ProductInventoryToUpdate = entities.ProductInventory.Where(x => x.ProductInventoryId == _ProductInventory.ProductInventoryId).FirstOrDefault();
                    ProductInventoryToUpdate.Quantity = _ProductInventory.Quantity ?? ProductInventoryToUpdate.Quantity;
                    ProductInventoryToUpdate.BrandId = _ProductInventory.BrandId ?? ProductInventoryToUpdate.BrandId;
                    ProductInventoryToUpdate.Code = _ProductInventory.Code ?? ProductInventoryToUpdate.Code;
                    ProductInventoryToUpdate.ProductId = _ProductInventory.ProductId ?? ProductInventoryToUpdate.ProductId;
                    ProductInventoryToUpdate.VendorId = _ProductInventory.VendorId ?? ProductInventoryToUpdate.VendorId;
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

        public ProductInventory Get_ProductInventoryById(long _ProductInventoryID)
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                try
                {
                    return _data.ProductInventory.Find(_ProductInventoryID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }

    }
}
