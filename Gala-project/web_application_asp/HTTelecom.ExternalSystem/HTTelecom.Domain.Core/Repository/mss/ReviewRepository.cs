using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ReviewRepository
    {
        public List<Review> GetAll(bool IsDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Review.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
        }
        public List<Review> GetByProduct(string ProductCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Review.Where(n => n.ProductCode == ProductCode).ToList();
            }

        }
        public long Create(Review review)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                _data.Review.Add(review);
                _data.SaveChanges();
                return review.ReviewId;
            }
        }
        public bool Delete(long id)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var rv = _data.Review.Find(id);
                _data.Review.Remove(rv);
                _data.SaveChanges();
                return true;
            }
        }
    }
}
