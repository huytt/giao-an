using HTTelecom.Domain.Core.IRepository.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.Domain.Core.Repository.ops
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        public long Create(OrderDetail orderDetail)
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                _OPSDb.OrderDetail.Add(orderDetail);
                _OPSDb.SaveChanges();
                return orderDetail.OrderDetailId;
            }
            catch
            {
                return -1;
            }
        }
        public bool Edit(OrderDetail orderDetail)
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                var or = _OPSDb.OrderDetail.Find(orderDetail.OrderDetailId);
                or.IsDeleted = orderDetail.IsDeleted;
                or.OrderQuantity = orderDetail.OrderQuantity;
                or.DateModified = orderDetail.DateModified;
                or.TotalPrice = orderDetail.TotalPrice;
                or.UnitPrice = orderDetail.UnitPrice;
                or.UnitPriceDiscount = orderDetail.UnitPriceDiscount;

                _OPSDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CreateByOrderId(List<OrderDetail> lstOrderDetail, long idOrder_rs)
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                foreach (var item in lstOrderDetail)
                {
                    item.OrderId = idOrder_rs;
                }
                foreach (var item in lstOrderDetail)
                {
                    _OPSDb.OrderDetail.Add(item);
                }
                _OPSDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<OrderDetail> GetListByOrderId(long OrderId)
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                return _OPSDb.OrderDetail.Where(n => n.OrderId == OrderId && n.IsDeleted == false).Distinct().ToList();
            }
            catch
            {
                return new List<OrderDetail>();
            }
        }
        public bool Delete(string ProductStockId, string OrderCode, long AccountId)
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                //HTTelecom.Domain.Core.Repository.mss.ProductRepository p = new HTTelecom.Domain.Core.Repository.mss.ProductRepository();

                // var OdDetail = (from a in _OPSDb.Orders
                //                 join b in _OPSDb.OrderDetails
                //                 on a.OrderId equals b.OrderId
                //                 where a.IsConfirmedBy == AccountId && a.IsDeleted == false && a.IsConfirmed == false && a.OrderCode == OrderCode && b.ProductId == p.GetByProductStockId(ProductStockId).ProductId && b.IsDeleted == false
                //                 select b).FirstOrDefault();
                // if (OdDetail != null)
                // {
                //     OdDetail.IsDeleted = true;
                //     _OPSDb.SaveChanges();
                //     return true;
                // }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(long ProductId, string OrderCode, long AccountId)
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                //HTTelecom.Domain.Core.Repository.mss.ProductRepository p = new HTTelecom.Domain.Core.Repository.mss.ProductRepository();

                //var OdDetail = (from a in _OPSDb.Orders
                //                join b in _OPSDb.OrderDetails
                //                on a.OrderId equals b.OrderId
                //                where a.IsConfirmedBy == AccountId && a.IsDeleted == false && a.IsConfirmed == false && a.OrderCode == OrderCode && b.ProductId == ProductId && b.IsDeleted == false
                //                select b).FirstOrDefault();
                //if (OdDetail != null)
                //{
                //    OdDetail.IsDeleted = true;
                //    _OPSDb.SaveChanges();
                //    return true;
                //}
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
