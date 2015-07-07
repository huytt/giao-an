using HTTelecom.Domain.Core.IRepository.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.Domain.Core.IRepository.ops
{
    public interface IOrderPaidRepository
    {
        long Create(OrderPaid odPayment);

        List<OrderPaid> SearchByLog(int type, string q, string Status_option, long ConfirmBy_option);

        OrderPaid GetByOrderId(long OrderId);

        bool Close(long OrderId, long AccountId, string Status);
    }
}
