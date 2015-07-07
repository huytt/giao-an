using HTTelecom.Domain.Core.IRepository.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.Domain.Core.IRepository.ops
{
    public interface IOrderTrackingDeliveryRepository
    {
        long Create(OrderTrackingDelivery OrderTrackingDelivery);
        bool Edit(OrderTrackingDelivery OrderTrackingDelivery);
        bool Delete(long keyword);
        bool UnDelete(long keyword);
    }
}
