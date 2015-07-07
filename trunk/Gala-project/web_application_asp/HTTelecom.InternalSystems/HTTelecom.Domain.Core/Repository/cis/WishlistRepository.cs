using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.cis;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class WishlistRepository
    {
        private CIS_DBEntities _data;
        public WishlistRepository()
        {
            _data = new CIS_DBEntities();
        }

        public List<Wishlist> GetAll(bool IsDeleted, bool IsActive)
        {
            try
            {
                return _data.Wishlists.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
            catch
            {
                return new List<Wishlist>();
            }
        }
    }
}
