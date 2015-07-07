using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class CustomerCardRepository
    {
        public IList<CustomerCard> GetList_CustomerCardAll()
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.CustomerCards.ToList();
                }
                catch
                {
                    return new List<CustomerCard>();
                }
            }
        }

        public IList<CustomerCard> GetList_CustomerCard_CustomerId(long? CustomerId)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.CustomerCards.Where(a => a.CustomerId == CustomerId).ToList();
                }
                catch
                {
                    return new List<CustomerCard>();
                }
            }
        }

        public IList<CustomerCard> GetList_CustomerCardsAll_IsDeleted(bool isDeleted)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.CustomerCards.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<CustomerCard>();
                }
            }
        }

        public IList<CustomerCard> GetList_CustomerCardsAll_IsActive(bool IsActive)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.CustomerCards.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<CustomerCard>();
                }
            }
        }

        public CustomerCard Get_CustomerCardsById(long CustomerCardId)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.CustomerCards.Find(CustomerCardId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(CustomerCard CustomerCard)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    CustomerCard.DateCreated = DateTime.Now;
                    CustomerCard.DateModified = DateTime.Now;
                    _data.CustomerCards.Add(CustomerCard);
                    _data.SaveChanges();

                    return CustomerCard.CustomerCardId;
                }
                catch
                {
                    return -1;
                }

            }
        }


        public bool UpdateActive(long CustomerCardId, bool isActive)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    CustomerCard CustomerCard = _data.CustomerCards.Where(x => x.CustomerCardId == CustomerCardId).FirstOrDefault();

                    CustomerCard.IsActive = isActive;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public bool Update(CustomerCard CustomerCard)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    CustomerCard CustomerCardToUpdate;
                    CustomerCardToUpdate = _data.CustomerCards.Where(x => x.CustomerCardId == CustomerCard.CustomerCardId).FirstOrDefault();
                    CustomerCardToUpdate.CardTypeId = CustomerCard.CardTypeId;
                    CustomerCardToUpdate.BankId = CustomerCard.BankId;
                    CustomerCardToUpdate.CardNumber = CustomerCard.CardNumber;
                    CustomerCardToUpdate.CardHolderName = CustomerCard.CardHolderName ?? CustomerCardToUpdate.CardHolderName;

                    CustomerCardToUpdate.ModifiedBy = CustomerCard.ModifiedBy;
                    CustomerCardToUpdate.IsActive = CustomerCard.IsActive;
                    CustomerCardToUpdate.IsDeleted = CustomerCard.IsDeleted;

                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

    }
}
