using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.cis;
namespace HTTelecom.Domain.Core.Repository.cis
{
    public class VendorCardRepository
    {
            public IList<VendorCard> GetList_VendorCardAll()
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    _data.Configuration.ProxyCreationEnabled = false;
                    _data.Configuration.LazyLoadingEnabled = false;
                    try
                    {
                        return _data.VendorCards.ToList();
                    }
                    catch
                    {
                        return new List<VendorCard>();
                    }
                }
            }

            public IList<VendorCard> GetList_VendorCard_VendorId(long? vendorId)
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    _data.Configuration.ProxyCreationEnabled = false;
                    _data.Configuration.LazyLoadingEnabled = false;
                    try
                    {
                        return _data.VendorCards.Where(a => a.VendorId == vendorId).ToList();
                    }
                    catch
                    {
                        return new List<VendorCard>();
                    }
                }
            }

            public IList<VendorCard> GetList_VendorCardsAll_IsDeleted(bool isDeleted)
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    try
                    {
                        return _data.VendorCards.Where(a => a.IsDeleted == isDeleted).ToList();
                    }
                    catch
                    {
                        return new List<VendorCard>();
                    }
                }
            }

            public IList<VendorCard> GetList_VendorCardsAll_IsActive(bool IsActive)
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    try
                    {
                        return _data.VendorCards.Where(a => a.IsActive == IsActive).ToList();
                    }
                    catch
                    {
                        return new List<VendorCard>();
                    }
                }
            }

            public VendorCard Get_VendorCardsById(long VendorCardId)
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    _data.Configuration.ProxyCreationEnabled = false;
                    _data.Configuration.LazyLoadingEnabled = false;
                    try
                    {
                        return _data.VendorCards.Find(VendorCardId);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            public long Insert(VendorCard VendorCard)
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    try
                    {
                        VendorCard.DateCreated = DateTime.Now;
                        VendorCard.DateModified = DateTime.Now;
                        _data.VendorCards.Add(VendorCard);
                        _data.SaveChanges();

                        return VendorCard.VendorCardId;
                    }
                    catch
                    {
                        return -1;
                    }

                }
            }


            public bool UpdateActive(long VendorCardId, bool isActive)
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    try
                    {
                        VendorCard VendorCard = _data.VendorCards.Where(x => x.VendorCardId == VendorCardId).FirstOrDefault();

                        VendorCard.IsActive = isActive;
                        _data.SaveChanges();
                        return true;
                    }
                    catch { return false; }
                }
            }

            public bool Update(VendorCard VendorCard)
            {
                using (CIS_DBEntities _data = new CIS_DBEntities())
                {
                    try
                    {
                        VendorCard VendorCardToUpdate;
                        VendorCardToUpdate = _data.VendorCards.Where(x => x.VendorCardId == VendorCard.VendorCardId).FirstOrDefault();
                        VendorCardToUpdate.CardTypeId = VendorCard.CardTypeId ?? VendorCardToUpdate.CardTypeId;
                        VendorCardToUpdate.BankId = VendorCard.BankId;
                        VendorCardToUpdate.CardNumber = VendorCard.CardNumber;
                        VendorCardToUpdate.CardHolderName = VendorCard.CardHolderName ?? VendorCardToUpdate.CardHolderName;

                        VendorCardToUpdate.ModifiedBy = VendorCard.ModifiedBy;
                        VendorCardToUpdate.IsActive = VendorCard.IsActive;
                        VendorCardToUpdate.IsDeleted = VendorCard.IsDeleted;

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
