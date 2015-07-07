using HTTelecom.Domain.Core.IRepository.ops;
using HTTelecom.Domain.Core.DataContext.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTelecom.Domain.Core.IRepository.ops
{
    public interface IOrderDetailRepository
    {
        long Create(OrderDetail orderDetail);
        bool Edit(OrderDetail orderDetail);
        bool CreateByOrderId(List<OrderDetail> lstOrderDetail, long idOrder_rs);
        List<OrderDetail> GetListByOrderId(long OrderId);

        bool Delete(string ProductStockId, string OrderCode,long AccountId);

        bool Delete(long ProductId, string OrderCode, long AccountId);
    }
}
