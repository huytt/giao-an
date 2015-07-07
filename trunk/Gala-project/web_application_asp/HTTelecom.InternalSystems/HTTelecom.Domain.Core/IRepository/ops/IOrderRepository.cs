using HTTelecom.Domain.Core.IRepository.ops;
using HTTelecom.Domain.Core.DataContext.ops;
//using OPS001.Models.ops001;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTelecom.Domain.Core.IRepository.ops
{
    public interface IOrderRepository
    {
        long Create(Order order);
        bool Edit(Order order);
        bool ChangeStatus(long keyword, long StatusId);

        Order GetById(long id);
        Order GetByDeparment(long id,string StatusCode);
        List<Order> GetAll();

        long Confrim(long OrderId, long AccountId, string StatusCodeFst);
        long Payment(long OrderId, long AccountId, string StatusCodeLst, string StatusCodeFst);
        long Delivery(long OrderId, long AccountId, string StatusCodeLst, string StatusCodeFst);
        bool UnDevivery(long OrderId, long AccountId);
        bool UnPayment(long OrderId, long AccountId);

        bool Delete(long id, bool IsDelete);

        bool UnConfrim(long OrderId, long AccountId);

        long CheckEdit(long id, long AccountId);

        bool SetConfirm(long OrderId, long AccountId);
        bool SetConfirm(long OrderId, long AccountId, long OrderStatus);

        List<Order> SearchByType(int type, string q, string Status_option, long ConfirmBy_option, List<string> OrderStatus);

        bool EditByOrderCode(Order od);


        bool UpdateByOrderDetail(long OrderId, long AccountId, string description);

        Order RefreshOrder(long id);

        List<Order> SearchByFinance(int type, string order_search, string Sts_opt, long ConfirmBy, List<string> OrderStatus);



        void SaleDetact(List<Order> lstOrder);
    }
}
