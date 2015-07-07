using HTTelecom.Domain.Core.IRepository.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.Domain.Core.Repository.ops
{
    public class OrderTrackingDeliveryRepository : IOrderTrackingDeliveryRepository
    {
        private OPS_DBEntities _OPSDb;
        public OrderTrackingDeliveryRepository()
        {
            _OPSDb = new OPS_DBEntities();
        }
        public long Create(OrderTrackingDelivery OrderTrackingDelivery)
        {
            try
            {
                _OPSDb.OrderTrackingDelivery.Add(OrderTrackingDelivery);
                _OPSDb.SaveChanges();
                return OrderTrackingDelivery.OrderTrackingDeliveryId;
            }
            catch
            {
                return -1;
            }
        }
        public bool Edit(OrderTrackingDelivery OrderTrackingDelivery)
        {
            return false;
        }
        public bool Delete(long keyword)
        {
            try
            {
                var OrderTrackingDelivery = _OPSDb.OrderTrackingDelivery.Find(keyword);
                OrderTrackingDelivery.IsDeleted = true;
                _OPSDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UnDelete(long keyword)
        {
            try
            {
                var OrderTrackingDelivery = _OPSDb.OrderTrackingDelivery.Find(keyword);
                OrderTrackingDelivery.IsDeleted = false;
                _OPSDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
