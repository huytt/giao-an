using HTTelecom.Domain.Core.DataContext.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ops
{
    public class OrderPaidRepository
    {
        public List<OrderPaid> GetAll(bool IsDeleted)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.OrderPaid.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<OrderPaid>();
            }
        }
        public long Create(OrderPaid orderpaid)
        {
            using (OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                    _data.OrderPaid.Add(orderpaid);
                    _data.SaveChanges();
                    return orderpaid.OrderPaidId;
                }
                catch
                {
                    return -1;
                }
            }

        }

    }
}
