using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.Repository.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class WishlistRepository
    {
        public List<Wishlist> GetAll(bool IsDeleted)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Wishlist.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Wishlist>();
            }
        }

        public List<Wishlist> GetByCustomer(long CustomerId)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Wishlist.Where(n => n.CustomerId == CustomerId).ToList();
            }
            catch
            {
                return new List<Wishlist>();
            }
        }

        public long Create(Wishlist wl)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                _data.Wishlist.Add(wl);
                _data.SaveChanges();
                return wl.WishlistId;
            }
            catch
            {
                return -1;
            }
        }

        public bool UpdateActive(long id, bool action)
        {
            try
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    Wishlist wl = _data.Wishlist.Where(n => n.ProductId == id).FirstOrDefault();
                    wl.IsActive = action;
                    _data.SaveChanges();
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAllByCustomer(long CustomerId)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                List<Wishlist> wl = _data.Wishlist.Where(n => n.CustomerId == CustomerId).ToList();
                foreach (var item in wl)
                {
                    item.IsActive = false;
                }
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveByProduct(long id)
        {
            try
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {

                    List<Wishlist> wl = _data.Wishlist.Where(n => n.ProductId == id).ToList();
                    foreach (var item in wl)
                    {
                        _data.Wishlist.Remove(item);
                    }
                    _data.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Wishlist> GetWishlist(List<Wishlist> lstWishList)
        {
            ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();

            var lstNew = new List<Wishlist>();
            foreach (var item in lstWishList)
            {
                var itemProduct = _ProductInMediaRepository.GetByProduct(item.ProductId).Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                if (_ProductInMediaRepository.ProductError(itemProduct.Product) == false)
                {
                    lstNew.Add(item);
                }
            }
            return lstNew;
        }
    }
}
