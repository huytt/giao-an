using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class CounterCardRepository
    {
        private CIS_DBEntities _data;
        public CounterCardRepository()
        {
            _data = new CIS_DBEntities();
        }

        public List<CounterCard> GetAll(bool IsDeleted, bool IsActive)
        {
            try
            {
                return _data.CounterCards.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
            catch
            {
                return new List<CounterCard>();
            }
        }
        public CounterCard GetById(long id)
        {
            try
            {
                return _data.CounterCards.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public long GetByBankId(long BankId)
        {
            try
            {
                return _data.CounterCards.Where(n => n.BankId == BankId).FirstOrDefault().CounterCardId;
            }
            catch
            {
                return -1;
            }
            //_data.Database.Connection.Open();
            //var tran = _data.Database.Connection.BeginTransaction();

            ////tran.Rollback();
            //using (tran)
            //{
            //    try
            //{
            //    _data.CardTypes.Add(new CardType { CardTypeName="sss",CreatedBy=1,DateCreated = DateTime.Now,IsActive=false,IsDeleted=false});
            //    _data.SaveChanges();
            //    _data.Banks.Add(new Bank { BankName = "------" });
            //    _data.SaveChanges();
            //    tran.Commit();
            //    return 1;
            //}
            //catch
            //{
            //    tran.Rollback();
            //    return -1;
            //}
            //}
        }
    }
}
