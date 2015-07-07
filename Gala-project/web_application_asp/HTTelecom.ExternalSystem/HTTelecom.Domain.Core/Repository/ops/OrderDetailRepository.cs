using HTTelecom.Domain.Core.DataContext.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ops
{
    public class OrderDetailRepository
    {
        public List<OrderDetail> GetAll(bool IsDeleted)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.OrderDetail.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<OrderDetail>();
            }
        }

        public bool CreateList(List<OrderDetail> lstOrderDetail)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                foreach (var item in lstOrderDetail)
                    _data.OrderDetail.Add(item);
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<OrderDetail> GetByOrder(long OrderId)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.OrderDetail.Where(n => n.OrderId == OrderId).ToList();
            }
            catch
            {
                return new List<OrderDetail>();
            }
        }
    }
}
