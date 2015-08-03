using HTTelecom.Domain.Core.DataContext.lps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.lps
{
     public class ProductItemInSizeRepository
    {
        public ProductItemInSize Get_ProductItemInSizeById(long _ProductItemInSizeID)
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                try
                {
                    return _data.ProductItemInSize.Find(_ProductItemInSizeID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<ProductItemInSize> GetList_ProductItemInSizeAll()
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductItemInSize.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<ProductItemInSize>();
                }
            }
        }

        public long InsertProductItemInSize(ProductItemInSize _ProductItemInSize)
        {
            using (LPS_DBEntities _data = new LPS_DBEntities())
            {
                try
                {
                    _ProductItemInSize.Quantity = 0;
                    _data.ProductItemInSize.Add(_ProductItemInSize);
                    _data.SaveChanges();
                    return _ProductItemInSize.ProductItemInSizeId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public bool UpdateProductItemInSize(ProductItemInSize _ProductItemInSize)
        {
            using (LPS_DBEntities entities = new LPS_DBEntities())
            {
                try
                {
                    ProductItemInSize ProductItemInSizeToUpdate;
                    ProductItemInSizeToUpdate = entities.ProductItemInSize.Where(x => x.ProductItemInSizeId == _ProductItemInSize.ProductItemInSizeId).FirstOrDefault();
                
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


        public long QuantityAddProductItemAndSize(long ProductItemId, long SizeId, int? _quantity)
        {
            using (LPS_DBEntities entities = new LPS_DBEntities())
            {
                try
                {
                    ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
                    ProductItemRepository _iProductItemService = new ProductItemRepository();
                    ProductInventory pinv = new ProductInventory();
             
                    var pitem = _iProductItemService.Get_ProductItemById(ProductItemId);
                    pinv.Quantity = _quantity;
                    pinv.Code = pitem.ProductCode;
                    pinv.ProductId = pitem.ProductItemId;
                    pinv.SizeId = SizeId;
                    pinv.VendorId = pitem.VendorId;
                    pinv.BrandId = 0;//updating.....
                    if (_iProductInventoryService.InsertProductInventory(pinv) == -1)
                    {
                        //không ghi log được nên, không ghi dữ liệu
                        return -1;
                    }

                    ProductItemInSize ProductItemInSizeToUpdate;
                    ProductItemInSizeToUpdate = entities.ProductItemInSize.Where(x => x.ProductItemId == ProductItemId &&  x.SizeId == SizeId).FirstOrDefault();
                    ProductItemInSizeToUpdate.Quantity = ProductItemInSizeToUpdate.Quantity == null ? 0 : ProductItemInSizeToUpdate.Quantity;
                    if (_quantity == null)
                        return -1;
                    if (ProductItemInSizeToUpdate.Quantity + _quantity < 0)
                        return -1;
                    ProductItemInSizeToUpdate.Quantity += _quantity;
                    entities.SaveChanges();
                    return (long)ProductItemInSizeToUpdate.Quantity;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }
        //public long QuantityAddProductItem(long _ProductItemId, long? _quantity)
        //{
            
        //}

        public bool UpdateDownQuantity(List<Tuple<long, string, int>> lstProductItemInSize)
        {
            try
            {
                using (LPS_DBEntities _data = new LPS_DBEntities())
                {
                    foreach (var item in lstProductItemInSize)
                    {
                        var ProductItem = _data.ProductItem.Where(n => n.ProductCode == item.Item2).FirstOrDefault();
                        if (ProductItem != null)
                        {
                            var rs = _data.ProductItemInSize.Where(n => n.SizeId == item.Item1 && n.ProductItemId == ProductItem.ProductItemId).FirstOrDefault();
                            rs.Quantity = rs.Quantity <= item.Item3 ? 0 : rs.Quantity - item.Item3;
                            _data.SaveChanges();
                        }
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
