using HTTelecom.Domain.Core.IRepository.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.Domain.Core.Repository.ops
{
    public class OrderPaidRepository : IOrderPaidRepository
    {
        private OPS_DBEntities _OPSDb;
        public OrderPaidRepository()
        {
            _OPSDb = new OPS_DBEntities();
        }
        public long Create(OrderPaid odPayment)
        {
            try
            {
                _OPSDb.OrderPaid.Add(odPayment);
                _OPSDb.SaveChanges();
                return odPayment.OrderPaidId;
            }
            catch
            {
                return -1;
            }
        }
        public List<OrderPaid> SearchByLog(int type, string q, string Status_option, long ConfirmBy_option)
        {
            try
            {
                //List<OrderPaid> lst = new List<OrderPaid>();
                //if (q == null)
                //    q = "";
                //if (ConfirmBy_option <= 0)
                //{
                //    if (Status_option == null || Status_option == "")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.ToList());
                //    }
                //    if (Status_option != null && Status_option == "success")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsClosed == true && n.IsDeliveryConfirmed == true).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsClosed == true && n.IsDeliveryConfirmed == true).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.Where(n => n.IsClosed == true && n.IsDeliveryConfirmed == true).ToList());
                //    }
                //    if (Status_option != null && Status_option == "waitDelivery")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsClosed == false && n.IsDeliveryConfirmed == true).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsClosed == false && n.IsDeliveryConfirmed == true).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.Where(n => n.IsClosed == false && n.IsDeliveryConfirmed == true).ToList());
                //    }
                //    if (Status_option != null && Status_option == "fail")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeleted !=null && (n.IsDeleted == true)).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeleted != null && (n.IsDeleted == true)).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.Where(n => n.IsDeleted != null && n.IsDeleted == true).ToList());
                //    }
                //    if (Status_option != null && Status_option == "wait")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeleted == false && (n.IsDeliveryConfirmed == null || n.IsDeliveryConfirmed == false)).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeleted == false && (n.IsDeliveryConfirmed == null || n.IsDeliveryConfirmed == false)).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.Where(n => n.IsDeleted == false && (n.IsDeliveryConfirmed == null || n.IsDeliveryConfirmed == false)).ToList());
                //    }
                //}
                //else
                //{
                //    if (Status_option == null || Status_option == "")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeliveryConfirmedBy == ConfirmBy_option ).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeliveryConfirmedBy == ConfirmBy_option ).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.Where(n => n.IsDeliveryConfirmedBy == ConfirmBy_option ).ToList());
                //    }
                //    if (Status_option != null && Status_option == "success")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPayments.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsClosedBy == ConfirmBy_option && n.IsClosed == true).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPayments.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsClosedBy == ConfirmBy_option && n.IsClosed == true ).ToList());
                //        else lst.AddRange(_OPSDb.OrderPayments.Where(n => n.IsClosed == true && n.IsClosedBy == ConfirmBy_option).ToList());
                //    }
                //    if (Status_option != null && Status_option == "waitDelivery")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsClosed == false && n.IsDeliveryConfirmed == true && n.IsDeliveryConfirmedBy == ConfirmBy_option).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsClosed == false && n.IsDeliveryConfirmed == true && n.IsDeliveryConfirmedBy == ConfirmBy_option).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.Where(n => n.IsClosed == false && n.IsDeliveryConfirmed == true && n.IsDeliveryConfirmedBy == ConfirmBy_option).ToList());
                //    }
                //    if (Status_option != null && Status_option == "fail")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeliveryConfirmedBy == ConfirmBy_option && (n.IsDeleted == true )).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeliveryConfirmedBy == ConfirmBy_option && (n.IsDeleted == true )).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.Where(n => n.IsDeliveryConfirmedBy == ConfirmBy_option && (n.IsDeleted == true )).ToList());
                //    }
                //    if (Status_option != null && Status_option == "wait")
                //    {
                //        if (type == 1 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeliveryConfirmedBy == ConfirmBy_option && n.IsDeleted == false && (n.IsDeliveryConfirmed == null || n.IsDeliveryConfirmed == false)).ToList());
                //        if (type == 2 && q != "")
                //            lst.AddRange(_OPSDb.OrderPaids.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeliveryConfirmedBy == ConfirmBy_option && n.IsDeleted == false && (n.IsDeliveryConfirmed == null || n.IsDeliveryConfirmed == false)).ToList());
                //        else lst.AddRange(_OPSDb.OrderPaids.Where(n => n.IsDeleted == false && (n.IsDeliveryConfirmed == null || n.IsDeliveryConfirmed == false) && n.IsDeliveryConfirmedBy == ConfirmBy_option).ToList());
                //    }
                //}


                return null;
            }
            catch
            {
                return new List<OrderPaid>();
            }
        }
        public OrderPaid GetByOrderId(long OrderId)
        {
            //try
            //{
            //    return _OPSDb.OrderPaids.Where(n => n.OrderId == OrderId).FirstOrDefault();
            //}
            //catch
            //{
            //    return null;
            //}
            return null;
        }
        public bool Close(long OrderId, long AccountId, string Status)
        {
            try
            {
                //var odPayment = _OPSDb.OrderPaids.Where(n => n.OrderId == OrderId).FirstOrDefault();
                //var od = _OPSDb.Orders.Find(OrderId);
                //if (odPayment == null || od == null)
                //    return false;
                //var odStatus = _OPSDb.OrderPaids.Find(od.OrderPaid);
                //if (od.OrderStatu.StatusCode.ToUpper().IndexOf(Status.ToUpper()) >= 0)
                //{
                //    if (odPayment.IsClosed == true)
                //        return false;
                //    odPayment.IsClosed = true;
                //    odPayment.IsClosedBy = AccountId;
                //    _OPSDb.SaveChanges();
                //    return true;
                //}
                //else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
