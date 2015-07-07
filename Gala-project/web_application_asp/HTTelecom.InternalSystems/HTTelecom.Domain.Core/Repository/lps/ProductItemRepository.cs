using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.lps;
using HTTelecom.Domain.Core.Repository.lps;

namespace HTTelecom.Domain.Core.Repository.lps
{
    public class ProductItemRepository
    {
        public ProductItem Get_ProductItemById(long _ProductItemID)
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                try
                {
                    return _data.ProductItem.Find(_ProductItemID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<ProductItem> GetList_ProductItemAll()
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductItem.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<ProductItem>();
                }
            }
        }

        public long InsertProductItem(ProductItem _ProductItem)
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                try
                {
                    _ProductItem.IsVerified = false;
                    _ProductItem.Quantity = 0;
                    _data.ProductItem.Add(_ProductItem);
                    _data.SaveChanges();
                    return _ProductItem.ProductItemId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public bool UpdateProductItem(ProductItem _ProductItem)
        {
            using (LPS_DBEntities entities = new LPS_DBEntities())
            {
                try
                {
                    ProductItem ProductItemToUpdate;
                    ProductItemToUpdate = entities.ProductItem.Where(x => x.ProductItemId == _ProductItem.ProductItemId).FirstOrDefault();
                    ProductItemToUpdate.ProductCode = _ProductItem.ProductCode ?? ProductItemToUpdate.ProductCode;
                    ProductItemToUpdate.VendorId = _ProductItem.VendorId ?? ProductItemToUpdate.VendorId;
                    ProductItemToUpdate.ProductStatusCode = _ProductItem.ProductStatusCode ?? ProductItemToUpdate.ProductStatusCode;
                    ProductItemToUpdate.ProductBarCode = _ProductItem.ProductBarCode ?? ProductItemToUpdate.ProductBarCode;
                    ProductItemToUpdate.ProductName = _ProductItem.ProductName ?? ProductItemToUpdate.ProductName;
                    //không được tự ý cập nhật số lượng sản phẩm, có 1 method rieng để làm việc này
                    //ProductItemToUpdate.Quantity = _ProductItem.Quantity ?? ProductItemToUpdate.Quantity;
                    ProductItemToUpdate.SerialNumber = _ProductItem.SerialNumber ?? ProductItemToUpdate.SerialNumber;
                    ProductItemToUpdate.StandardPrice = _ProductItem.StandardPrice ?? ProductItemToUpdate.StandardPrice;
                    ProductItemToUpdate.ProductTermService = _ProductItem.ProductTermService ?? ProductItemToUpdate.ProductTermService;
                    ProductItemToUpdate.DateManufactured = _ProductItem.DateManufactured ?? ProductItemToUpdate.DateManufactured;
                    ProductItemToUpdate.LimitWarrantyDays = _ProductItem.LimitWarrantyDays ?? ProductItemToUpdate.LimitWarrantyDays;
                    ProductItemToUpdate.SellDate = _ProductItem.SellDate ?? ProductItemToUpdate.SellDate;
                    ProductItemToUpdate.ModifiedBy = _ProductItem.ModifiedBy ?? ProductItemToUpdate.ModifiedBy;
                    ProductItemToUpdate.IsVerified = _ProductItem.IsVerified ?? ProductItemToUpdate.IsVerified;
                    ProductItemToUpdate.IsDeleted = _ProductItem.IsDeleted ?? ProductItemToUpdate.IsDeleted;
                    ProductItemToUpdate.IsActive = _ProductItem.IsActive ?? ProductItemToUpdate.IsActive;

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

        public long QuantityAddProductItem(long _ProductItemId,long? _quantity)
        {
            using (LPS_DBEntities entities = new LPS_DBEntities())
            {                
                try
                {
                    ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
                    ProductInventory pinv = new ProductInventory();
                    ProductItem pitem = this.Get_ProductItemById((long)_ProductItemId);

                    pinv.Quantity = _quantity;
                    pinv.Code = pitem.ProductCode;
                    pinv.ProductId = pitem.ProductItemId;
                    pinv.VendorId = pitem.VendorId;
                    pinv.BrandId = 0;//updating.....
                    if (_iProductInventoryService.InsertProductInventory(pinv) == -1)
                    {
                        //không ghi log được nên, không ghi dữ liệu
                        return -1;
                    }

                    ProductItem ProductItemToUpdate;
                    ProductItemToUpdate = entities.ProductItem.Where(x => x.ProductItemId == _ProductItemId).FirstOrDefault();
                    ProductItemToUpdate.Quantity = ProductItemToUpdate.Quantity == null ? 0 : ProductItemToUpdate.Quantity;
                    if (_quantity == null)
                        return -1;
                    if (ProductItemToUpdate.Quantity + _quantity < 0)
                        return -1;
                    ProductItemToUpdate.Quantity += _quantity;      
                    entities.SaveChanges();
                    return (long)ProductItemToUpdate.Quantity;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public long QuantitySubtractProductItem(long _ProductItemId, long? _quantity)
        {
            using (LPS_DBEntities entities = new LPS_DBEntities())
            {
                try
                {
                    ProductItem ProductItemToUpdate;
                    ProductItemToUpdate = entities.ProductItem.Where(x => x.ProductItemId == _ProductItemId).FirstOrDefault();
                    ProductItemToUpdate.Quantity = ProductItemToUpdate.Quantity == null ? 0 : ProductItemToUpdate.Quantity;
                    if (_quantity == null)
                        return -1;
                    if (ProductItemToUpdate.Quantity - _quantity < 0)
                        return -1;
                    ProductItemToUpdate.Quantity -= _quantity;
                    entities.SaveChanges();
                    return (long)ProductItemToUpdate.Quantity;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }       

        public IList<ProductItem> GetList_ProductItemAll_IsDeleted(bool isDeleted)
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductItem.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<ProductItem>();
                }
            }
        }
    }
}
