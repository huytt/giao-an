using HTTelecom.Domain.Core.IRepository.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.Domain.Core.Repository.ops
{
    public class OrderRepository : IOrderRepository
    {
        private OPS_DBEntities _OPSDb;
        public OrderRepository()
        {
            _OPSDb = new OPS_DBEntities();
        }
        public long Create(Order order)
        {
            try
            {
                Random rd = new Random();
                order.OrderCode = GetCode(Convert.ToDateTime(order.DateCreated));
                _OPSDb.Order.Add(order);
                _OPSDb.SaveChanges();
                return order.OrderId;
            }
            catch
            {
                return -1;
            }
        }
        public string GetCode(DateTime date)
        {
            Random rd = new Random();
            var code = "OD" + date.Year + string.Format("{0:00}", date.Month) + "-" + string.Format("{0:000000}", rd.Next(0, 100000));
            var lst = _OPSDb.Order.Where(n => n.OrderCode == code).ToList();
            if (lst.Count > 0)
                return GetCode(date);
            else
                return code;
        }
        public bool Edit(Order order)
        {
            return true;
        }
        public bool ChangeStatus(long keyword, long StatusId)
        {
            return true;
        }
        public Order GetById(long id)
        {
            try
            {
                return _OPSDb.Order.Where(n => n.OrderId == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public Order GetByDeparment(long id, string StatusCode)
        {
            try
            {
                //IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
                //var lst = _OrderStatus.GetByDepartment(StatusCode);
                //var lst_rs = _OPSDb.Orders.Where(n => n.OrderId == id).FirstOrDefault();
                //var temp = false;
                //foreach (var item in lst)
                //{
                //    if (item.OrderStatusId == lst_rs.OrderStatusId)
                //        temp = true;
                //}
                //if (temp)
                //    return lst_rs;
                //else 
                return null;
            }
            catch
            {
                return null;
            }
        }
        public List<Order> GetAll()
        {
            try
            {
                return _OPSDb.Order.ToList();
            }
            catch
            {
                return null;
            }
        }
        public long Confrim(long OrderId, long AccountId, string StatusCodeLst)
        {
            try
            {
                //IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
                //var od = _OPSDb.Orders.Find(OrderId);
                //var OrderStatus = _OrderStatus.GetById(Convert.ToInt64(od.OrderStatusId));
                //if ((od.IsDeleted != null && od.IsDeleted == true) || (od.IsConfirmed != null && od.IsConfirmed == true) || OrderStatus.StatusCode.ToUpper().IndexOf("FAL")>=0)
                //{
                //    if (OrderStatus.StatusCode.ToUpper().IndexOf(StatusCodeLst.ToUpper()) >= 0)
                //    {
                //        return 2;
                //    }
                //    else
                //        return -1;
                //}
                //if (od.IsConfirmedBy == null)
                //{
                //    if (SearchByType(0, "", "wait", AccountId, new List<string>() { StatusCodeLst }).Count >= 1)
                //        return -1;
                //    else
                //    {
                //        od.IsConfirmedBy = AccountId;
                //        _OPSDb.SaveChanges();
                //        return 1;
                //    }
                //}
                //else if (od.IsConfirmedBy != AccountId)
                //    return -1;
                //else
                {
                    return 0;
                }
            }
            catch
            {
                return -1;
            }
        }
        public long Payment(long OrderId, long AccountId, string StatusCodeLst, string StatusCodeFst)
        {
            try
            {
                //IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
                //var od = _OPSDb.Orders.Find(OrderId);
                //if (od.IsConfirmed == null || od.IsConfirmed == false)
                //    return -1;
                //var OrderStatus = _OrderStatus.GetById(Convert.ToInt64(od.OrderStatusId));
                //if ((od.IsDeleted != null && od.IsDeleted == true) || (od.IsPaymentConfirmed != null && od.IsPaymentConfirmed == true) || OrderStatus.StatusCode.ToUpper().IndexOf(StatusCodeLst.ToUpper() + "FAL") >= 0)
                //{
                //    //if (){}
                //    //    else
                //    return 2;
                //}
                
                //if (OrderStatus.StatusCode.ToUpper().IndexOf(StatusCodeFst.ToUpper()) >= 0 || OrderStatus.StatusCode.ToUpper().IndexOf(StatusCodeLst.ToUpper()) >= 0)
                //{
                //    if (od.IsPaymentConfirmedBy == null)
                //    {
                //        if (SearchByFinance(0, "", "wait", AccountId, new List<string>() { StatusCodeLst, StatusCodeFst }).Count >= 1)
                //            return -1;
                //        else
                //        {
                //            od.IsPaymentConfirmedBy = AccountId;
                //            _OPSDb.SaveChanges();
                //            return 1;
                //        }
                //    }
                //    else
                //    {
                //        if (od.IsPaymentConfirmedBy != AccountId)
                //            return -1;
                //        else
                //        {
                //            return 0;
                            
                //        }
                //    }
                //}
                //else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }
        public long Delivery(long OrderId, long AccountId, string StatusCodeLst, string StatusCodeFst)
        {
            //try
            //{
            //    IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
            //    IOrderPaymentRepository _OrderPayment = new OrderPaymentRepository();
            //    IOrderRepository _Order = new OrderRepository();
            //    var odPayment = _OPSDb.OrderPayments.Where(n=>n.OrderId == OrderId).FirstOrDefault();
            //    if (odPayment == null)
            //        return -1;
            //    var od = _Order.GetById(OrderId);
            //    var OrderStatus = _OrderStatus.GetById(Convert.ToInt64(od.OrderStatusId));
            //    if (odPayment.IsDeleted != null && odPayment.IsDeleted == true)
            //        return -1;
            //    if ((odPayment.IsClosed != null && odPayment.IsClosed == true) || OrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0)
            //    {
            //        return 2;
            //    }
                
            //    if (OrderStatus.StatusCode.ToUpper().IndexOf(StatusCodeFst.ToUpper()) >= 0 || OrderStatus.StatusCode.ToUpper().IndexOf(StatusCodeLst.ToUpper()) >= 0)
            //    {
            //        if (odPayment.IsDeliveryConfirmedBy == null)
            //        {
            //            if (_OrderPayment.SearchByLog(0, "", "wait", AccountId).Count >= 1 || _OrderPayment.SearchByLog(0, "", "waitDelivery", AccountId).Count >= 1)
            //                return -1;
            //            else
            //            {
            //                odPayment.IsDeliveryConfirmedBy = AccountId;
            //                _OPSDb.SaveChanges();
            //                return 1;
            //            }
            //        }
            //        else
            //        {
            //            if (odPayment.IsDeliveryConfirmedBy != AccountId)
            //                return -1;
            //            else
            //            {
            //                return 0;

            //            }
            //        }
            //    }
            //    else
            //        return -1;
            //}
            //catch
            {
                return -1;
            }
        }
        public bool UnDevivery(long OrderId, long AccountId)
        {
            //try
            //{
            //    var od = _OPSDb.Orders.Find(OrderId);
            //    IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
            //    IOrderPaymentRepository _OrderPayment = new OrderPaymentRepository();
            //    var Orderstatus = _OrderStatus.GetById(Convert.ToInt64(od.OrderStatusId));
            //    var orderPayment = _OrderPayment.GetByOrderId(OrderId);
            //    if (orderPayment == null)
            //        return false ;
            //    if (AccountId == orderPayment.IsDeliveryConfirmedBy && orderPayment.IsDeliveryConfirmed != null && orderPayment.IsDeliveryConfirmed == false && Orderstatus.StatusCode.ToUpper().IndexOf("FAL") < 0)
            //    {
            //        orderPayment.IsDeliveryConfirmedBy = null;
            //        _OPSDb.SaveChanges();
            //        return true;
            //    }
            //    else return false;
            //}
            //catch
            {
                return false;
            }
        }
        public bool UnPayment(long OrderId, long AccountId)
        {
            //try
            //{
            //    var od = _OPSDb.Orders.Find(OrderId);
            //    IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
            //    var Orderstatus = _OrderStatus.GetById(Convert.ToInt64(od.OrderStatusId));
            //    if (AccountId == od.IsPaymentConfirmedBy && od.IsPaymentConfirmed != null && od.IsPaymentConfirmed == false && Orderstatus.StatusCode.ToUpper().IndexOf("FAL")<0)
            //    {
            //        od.IsPaymentConfirmedBy = null;
            //        _OPSDb.SaveChanges();
            //        return true;
            //    }
            //    else return false;
            //}
            //catch
            {
                return false;
            }
        }
        public long CheckEdit(long OrderId, long AccountId)
        {
            //try
            //{
            //    var od = _OPSDb.Orders.Find(OrderId);
            //    if (od.IsConfirmedBy == null)
            //    {
            //        if (od.IsDeleted == false)
            //        {
            //            var lstOrder = _OPSDb.Orders.Where(n => n.IsConfirmedBy == AccountId && n.IsDeleted == false && n.IsConfirmed == false).ToList();
            //            if (lstOrder.Count >= 1)
            //                return -1;
            //            else
            //                return 1;
            //        }
            //        else return 2;
            //    }
            //    else if (od.IsConfirmed == true)
            //        return 2;
            //    else
            //        if (od.IsConfirmedBy != AccountId)
            //        {
            //            if (od.IsDeleted == true)
            //                return 2;
            //            else
            //                return -1;
            //        }
            //        else
            //        {
            //            return 0;

            //        }
            //}
            //catch
            {
                return -1;
            }
        }
        public bool UnConfrim(long OrderId, long AccountId)
        {
            //try
            //{
            //    var od = _OPSDb.Orders.Find(OrderId);
            //    if (AccountId == od.IsConfirmedBy)
            //    {
            //        od.IsConfirmedBy = null;
            //        _OPSDb.SaveChanges();
            //        return true;
            //    }
            //    else return false;
            //}
            //catch
            {
                return false;
            }
        }
        public bool Delete(long id, bool IsDelete)
        {
            //try
            //{
            //    var rs = _OPSDb.Orders.Find(id);
            //    if (rs.IsConfirmed == false)
            //    {
            //        rs.IsModifiedBy = id;
            //        rs.IsDeleted = IsDelete;
            //        _OPSDb.SaveChanges();
            //        return true;
            //    }
            //    return false;
            //}
            //catch
            {
                return false;
            }
        }
        public bool SetConfirm(long OrderId, long AccountId)
        {
            //try
            //{
            //    var Order = _OPSDb.Orders.Find(OrderId);
            //    Order.IsConfirmed = true;
            //    Order.IsConfirmedBy = AccountId;
            //    Order.IsModifiedBy = AccountId;
            //    Order.DateModified = DateTime.Now;
            //    _OPSDb.SaveChanges();
            //    return true;
            //}
            //catch
            {
                return false;
            }
        }
        public bool SetConfirm(long OrderId, long AccountId, long OrderStatus)
        {
            //try
            //{
            //    IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
            //    var orderstatus = _OrderStatus.GetById(OrderStatus);
            //    var Order = _OPSDb.Orders.Find(OrderId);

            //    if (orderstatus.StatusCode.ToUpper().IndexOf("SALEND") >= 0)
            //    {
            //        Order.IsConfirmed = true;
            //        Order.IsConfirmedBy = AccountId;
            //    }
            //    if (orderstatus.StatusCode.ToUpper().IndexOf("FINEND") >= 0)
            //    {
            //        IOrderPaymentRepository _orderPayment = new OrderPaymentRepository();
            //        OrderPayment odPayment = new OrderPayment();
            //        odPayment.CustomerEmail = Order.CustomerEmail;
            //        odPayment.CustomerName = Order.CustomerName;
            //        odPayment.CustomerPhone = Order.CustomerPhone;
            //        odPayment.CustomerTotalPaid = Convert.ToDecimal(Order.TotalPaid);
            //        odPayment.DateCreated= DateTime.Now;
            //        odPayment.DateDeliveryConfirmed = DateTime.Now;
            //        odPayment.IsClosed = false;
            //        odPayment.IsDeleted = false;
            //        odPayment.IsDeliveryConfirmed = true;
            //        odPayment.IsDeliveryConfirmedBy = AccountId;
            //        odPayment.OrderId = Order.OrderId;
            //        //odPayment.MerchantSiteId = Order.MerchantSiteId;
            //        //odPayment.OrderReceiveDescription = Order.OrderReceiveDescription;
            //        //odPayment.TotalActualReceive = Order.To;
            //        //odPayment.TransactionFee = Order.TransactionFee;
            //        odPayment.TransactionId = "";
            //        if (_orderPayment.Create(odPayment) == -1)
            //            return false;
            //        Order.IsPaymentConfirmed = true;
            //        Order.DatePaymentConfirmed = DateTime.Now;
            //        Order.IsPaymentConfirmedBy = AccountId;
            //    }
            //    if (orderstatus.StatusCode.ToUpper().IndexOf("LOG") >= 0 && orderstatus.StatusCode.ToUpper().IndexOf("LOGEND") < 0 && orderstatus.StatusCode.ToUpper().IndexOf("LOGFAL") < 0)
            //    {
            //        IOrderPaymentRepository _OrderPayment = new OrderPaymentRepository();
            //        var orderPayment = _OPSDb.OrderPayments.Where(n => n.OrderId == OrderId).FirstOrDefault();
            //        if (orderPayment == null)
            //            return false;
            //        orderPayment.IsDeliveryConfirmed = true;
            //        orderPayment.IsDeliveryConfirmedBy = AccountId;
            //    }
            //    Order.IsModifiedBy = AccountId;
            //    Order.OrderStatusId = OrderStatus;
            //    Order.DateModified = DateTime.Now;
            //    _OPSDb.SaveChanges();
            //    return true;
            //}
            //catch
            {
                return false;
            }
        }
        public List<Order> SearchByType(int type, string q, string Status_option, long ConfirmBy_option, List<string> OrderStatus)
        {
            //try
            //{
            //    IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
            //    List<Order> lst = new List<Order>();
            //    foreach (var item in OrderStatus)
            //    {
            //        if (q == null)
            //            q = "";
            //        var lstOrderStatus = _OrderStatus.GetByDepartment(item);
            //        if (lstOrderStatus == null || lstOrderStatus.Count == 0)
            //            continue;
            //        else
            //        {
            //            foreach (var itemOrderStatus in lstOrderStatus)
            //            {
            //                if (ConfirmBy_option <= 0)
            //                {
            //                    if (Status_option == null || Status_option == "")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "success")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "fail")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL")>=0) && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.OrderStatusId == itemOrderStatus.OrderStatusId && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0)).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "wait")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeleted == false&& itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeleted == false&& itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsDeleted == false&& itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsConfirmed == false && n.IsDeleted == false && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                    }
            //                }
            //                else
            //                {
            //                    if (Status_option == null || Status_option == "")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "success")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.IsConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.IsConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.IsConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsConfirmed == true && n.IsConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "fail")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsConfirmedBy == ConfirmBy_option && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "wait")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.IsDeleted == false && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.IsDeleted == false&& itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0  && n.IsConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsConfirmedBy == ConfirmBy_option && n.IsDeleted == false&& itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0  && n.IsConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsDeleted == false&& itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0  && n.IsConfirmed == false && n.IsConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                    }
            //                }
            //            }
            //        }

            //    }


            //    return lst;
            //}
            //catch
            {
                return new List<Order>();
            }
        }
        public List<Order> SearchByFinance(int type, string q, string Status_option, long ConfirmBy_option, List<string> OrderStatus)
        {
            //try
            //{
            //    IOrderStatusRepository _OrderStatus = new OrderStatusRepository();
            //    List<Order> lst = new List<Order>();
            //    foreach (var item in OrderStatus)
            //    {
            //        if (q == null)
            //            q = "";
            //        var lstOrderStatus = _OrderStatus.GetByDepartment(item);
            //        if (lstOrderStatus == null || lstOrderStatus.Count == 0)
            //            continue;
            //        else
            //        {
            //            foreach (var itemOrderStatus in lstOrderStatus)
            //            {
            //                if (ConfirmBy_option <= 0)
            //                {
            //                    if (Status_option == null || Status_option == "")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "success")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsPaymentConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "fail")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0 ) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.OrderStatusId == itemOrderStatus.OrderStatusId &&(n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) &&  n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "wait")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") <0 && n.IsDeleted == false && (n.IsPaymentConfirmed == null || n.IsPaymentConfirmed == false) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsDeleted == false && (n.IsPaymentConfirmed == null || n.IsPaymentConfirmed == false) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsDeleted == false && (n.IsPaymentConfirmed == null || n.IsPaymentConfirmed == false) && n.OrderStatusId == itemOrderStatus.OrderStatusId).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsDeleted == false && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && (n.IsPaymentConfirmed == null || n.IsPaymentConfirmed == false) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                    }
            //                }
            //                else
            //                {
            //                    if (Status_option == null || Status_option == "")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsPaymentConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "success")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.IsPaymentConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.IsPaymentConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.IsPaymentConfirmed == true && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsPaymentConfirmed == true && n.IsPaymentConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "fail")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsPaymentConfirmedBy == ConfirmBy_option && (n.IsDeleted == true || itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") >= 0) && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                    }
            //                    if (Status_option != null && Status_option == "wait")
            //                    {
            //                        if (type == 1 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.OrderCode.ToUpper().IndexOf(q.ToUpper()) >= 0 && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.IsDeleted == false && n.IsPaymentConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        if (type == 2 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.CustomerName.ToUpper().IndexOf(q.ToUpper()) >= 0 && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.IsDeleted == false && n.IsPaymentConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else if (type == 3 && q != "")
            //                            lst.AddRange(_OPSDb.Orders.Where(n => n.ShipToCity.ToUpper().IndexOf(q.ToUpper()) >= 0 && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.IsDeleted == false && n.IsPaymentConfirmed == false && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                        else lst.AddRange(_OPSDb.Orders.Where(n => n.IsDeleted == false && n.IsPaymentConfirmed == false && itemOrderStatus.StatusCode.ToUpper().IndexOf("FAL") < 0 && n.IsPaymentConfirmedBy == ConfirmBy_option && n.OrderStatusId == itemOrderStatus.OrderStatusId && n.IsConfirmed != null && n.IsConfirmed == true).ToList());
            //                    }
            //                }
            //            }
            //        }

            //    }


            //    return lst;
            //}
            //catch
            {
                return new List<Order>();
            }
        }
        public bool EditByOrderCode(Order order)
        {
            //try
            //{
            //    var od = _OPSDb.Orders.Where(n => n.OrderCode == order.OrderCode).FirstOrDefault();
            //    if (od.IsConfirmed == true || od.IsDeleted == true)
            //        return false;
            //    od.ReceiverName = order.ReceiverName;
            //    od.ReceiverPhone = order.ReceiverPhone;
            //    od.ShipToAddress = order.ShipToAddress;
            //    if (order.ShipToCity != null)
            //        od.ShipToCity = order.ShipToCity;
            //    if (order.ShipToDistrict != null)
            //        od.ShipToDistrict = order.ShipToDistrict;
            //    _OPSDb.SaveChanges();
            //    return true;
            //}
            //catch
            {
                return false;
            }
        }
        public Order RefreshOrder(long id)
        {
            //try
            //{
            //    return _OPSDb.Orders.Where(n => n.OrderId == id && n.IsDeleted == false && n.IsConfirmed == false).FirstOrDefault();
            //}
            //catch
            {
                return null;
            }
        }
        public bool UpdateByOrderDetail(long OrderId, long AccountId, string description)
        {
            //try
            //{
            //    var od = _OPSDb.Orders.Find(OrderId);
            //    if (od == null)
            //        return false;
            //    decimal totalPay = 0;
            //    decimal SubTotalFee = 0;
            //    IOrderDetailRepository _orderDetail = new OrderDetailRepository();
            //    var lst = _orderDetail.GetListByOrderId(OrderId);
            //    foreach (var item in lst)
            //    {
            //        totalPay += Convert.ToDecimal(item.TotalPrice);
            //        SubTotalFee += item.UnitPrice * item.OrderQuantity;
            //    }
            //    od.TotalProduct = lst.Count;
            //    od.TotalPaid = totalPay;
            //    od.SubTotalFee = SubTotalFee;
            //    od.OrderDescription = description;
            //    _OPSDb.SaveChanges();
            //    return true;
            //}
            //catch
            {
                return false;
            }
        }
        public void SaleDetact(List<Order> lstOrder)
        {
            //try
            //{
            //    foreach (var item in lstOrder)
            //    {
            //        var od = _OPSDb.Orders.Find(item.OrderId);
            //        if ((od.IsConfirmed == null || od.IsConfirmed == false) && od.IsConfirmedBy != null)
            //        {
            //            od.IsConfirmedBy = null;
            //            _OPSDb.SaveChanges();
            //        }
            //    }
            //}
            //catch
            {
                
            }
        }
        public void SaleAdminChange(List<Order> lstOrder)
        {
            //try
            //{
            //    foreach (var item in lstOrder)
            //    {
            //        var od = _OPSDb.Orders.Find(item.OrderId);
            //        if ((od.IsPaymentConfirmed == null || od.IsPaymentConfirmed == false) && od.IsPaymentConfirmedBy != null)
            //        {
            //            od.IsPaymentConfirmedBy = null;
            //        }
            //        _OPSDb.SaveChanges();
            //    }
            //}
            //catch
            //{

            //}
        }
        //public bool CreateLog()
        //{

        //}
        public void FinanceDetact(List<Order> lstOrder)
        {
            //try
            //{
            //    foreach (var item in lstOrder)
            //    {
            //        var od = _OPSDb.Orders.Find(item.OrderId);
            //        if ((od.IsPaymentConfirmed == null || od.IsPaymentConfirmed == false) && od.IsPaymentConfirmedBy!= null)
            //        {
            //            od.IsPaymentConfirmedBy = null;
            //        }
                    
            //        _OPSDb.SaveChanges();
            //    }
            //}
            //catch
            //{

            //}
        }


        public List<Order> GetAll(bool IsDelete, bool IsActive, bool IsLocked)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.Order.Where(n => n.IsDeleted == IsDelete && n.IsActive == IsActive && n.IsLocked == IsLocked).ToList();
            }
            catch
            {
                return new List<Order>();
            }
        }

        public List<Order> GetAll(long CustomerId,bool IsDelete, bool IsActive)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.Order.Where(n =>n.CustomerId == CustomerId & n.IsDeleted == IsDelete & n.IsActive == IsActive).ToList();
            }
            catch
            {
                return new List<Order>();
            }
        }

        public bool UpdateLock(string Code, bool IsLock)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                var Order = _data.Order.Where(n => n.OrderCode == Code).FirstOrDefault();
                Order.IsLocked = IsLock;
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Order GetByCode(string Code)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.Order.Where(n => n.OrderCode == Code).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<Order> GetByNewOrder()
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.Order.Where(n => n.IsWarning == false).ToList();
            }
            catch
            {
                return new List<Order>();
            }
        }

        public void UpdateWarning(List<Order> order)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                foreach (var item in order)
                {
                    Order itemOrder = _data.Order.Find(item.OrderId);
                    itemOrder.IsWarning = true;
                };
                _data.SaveChanges();
            }
            catch
            {
                
            }
        }

        public bool UpdateStatus(string orderCode, string TransactionStatusCode, bool IsDeliveryConfirmed)
        {
            using ( OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                   var order =  _data.Order.Where(n => n.OrderCode == orderCode).FirstOrDefault();
                   order.TransactionStatusCode = TransactionStatusCode;
                   order.IsDeliveryConfirmed = IsDeliveryConfirmed;
                   _data.SaveChanges();
                   return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void UpdatePayment(long id,bool p)
        {
            try
            {
                using (OPS_DBEntities _data = new OPS_DBEntities())
                {
                    Order itemOrder = _data.Order.Find(id);
                    itemOrder.IsPaymentConfirmed = p;
                    itemOrder.DateReceive = DateTime.Now;
                    _data.SaveChanges();
                }
               
            }
            catch
            {

            }
        }
    }
}
