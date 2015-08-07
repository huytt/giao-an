using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class GiftRepository
    {
        public Gift GetById(long id)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Gift.Find(id);
            }
        }
        public List<Gift> GetAll(bool IsDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Gift.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
        }
        public List<Gift> GetByProduct(long ProductId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Gift.Where(n => n.ProductId == ProductId && n.IsDeleted == false).ToList();
            }
        }
    }
}
