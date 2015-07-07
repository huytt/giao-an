using HTTelecom.Domain.Core.DataContext.ops;
using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace HTTelecom.Domain.Core.Repository.ops
{
    public class OrderRepository
    {
        public List<Order> GetAll(bool IsDeleted)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.Order.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Order>();
            }
        }

        public long Create(Order model)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                Random rd = new Random();
                model.OrderCode = GetCode(Convert.ToDateTime(model.DateCreated));
                _data.Order.Add(model);
                _data.SaveChanges();
                return model.OrderId;
            }
            catch
            {
                return -1;
            }
        }
        public string GetCode(DateTime date)
        {
            OPS_DBEntities _data = new OPS_DBEntities();
            Random rd = new Random();
            var code = "OD" + date.Year + string.Format("{0:00}", date.Month) + "-" + string.Format("{0:000000}", rd.Next(0, 100000));
            var lst = _data.Order.Where(n => n.OrderCode == code).ToList();
            if (lst.Count > 0)
                return GetCode(date);
            else
                return code;
        }

        public Order GetById(long orderId)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.Order.Find(orderId);
            }
            catch
            {
                return null;
            }
        }

        public long CreateFullOrder(Order model, List<OrderDetail> lstOrderDetail)
        {
            try
            {
                long flag = -1;
                model.OrderCode = GetCode(Convert.ToDateTime(model.DateCreated));
                using (TransactionScope scope = new TransactionScope())
                {
                    using (OPS_DBEntities _data = new OPS_DBEntities())
                    {
                        _data.Order.Add(model);
                        _data.SaveChanges();
                        foreach (var item in lstOrderDetail)
                        {
                            item.OrderId = model.OrderId;
                            _data.OrderDetail.Add(item);
                        }
                        _data.SaveChanges();
                    }
                    flag = model.OrderId;
                    scope.Complete(); // If you are happy
                }
                return flag;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }


        public List<Order> GetByCustomer(long CustomerId)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.Order.Where(n => n.CustomerId == CustomerId).ToList();
            }
            catch
            {
                return new List<Order>();
            }
        }

        public Order GetByCode(string code)
        {
            using (OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                    return _data.Order.Where(n => n.OrderCode == code).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }

        }

        public bool Edit(Order order)
        {
            using (OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                    var od = _data.Order.Find(order.OrderId);
                    od.TransactionStatusCode_BK = order.TransactionStatusCode_BK;
                    od.CustomerEmail = order.CustomerEmail;
                    od.CustomerPhone = order.CustomerPhone;
                    od.IsPaymentConfirmed = order.IsPaymentConfirmed;
                    _data.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Save(Order order)
        {
            using (OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                    var od = _data.Order.Find(order.OrderId);
                    od.OrderDescription = od.OrderDescription == null ? "" : od.OrderDescription;
                    od.OrderDescription += order.OrderDescription;
                    od.DateModified = DateTime.Now;
                    _data.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void Fail(string code)
        {
            using (OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                    var od = _data.Order.Where(n => n.OrderCode == code).FirstOrDefault();
                    if (od.IsPaymentConfirmed == null && od.IsDeliveryConfirmed == null)
                    {
                        od.IsPaymentConfirmed = false;//Code
                        od.DateModified = DateTime.Now;
                        _data.SaveChanges();
                    }
                }
                catch
                {
                }
            }
        }
    }

    class myEnlistmentClass : IEnlistmentNotification
    {
        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            Console.WriteLine("Prepare notification received");

            //Perform transactional work

            //If work finished correctly, reply prepared
            preparingEnlistment.Prepared();

            // otherwise, do a ForceRollback
            preparingEnlistment.ForceRollback();
        }

        public void Commit(Enlistment enlistment)
        {
            Console.WriteLine("Commit notification received");

            //Do any work necessary when commit notification is received

            //Declare done on the enlistment
            enlistment.Done();
        }

        public void Rollback(Enlistment enlistment)
        {
            Console.WriteLine("Rollback notification received");

            //Do any work necessary when rollback notification is received

            //Declare done on the enlistment
            enlistment.Done();
        }

        public void InDoubt(Enlistment enlistment)
        {
            Console.WriteLine("In doubt notification received");

            //Do any work necessary when indout notification is received

            //Declare done on the enlistment
            enlistment.Done();
        }
    }

}
