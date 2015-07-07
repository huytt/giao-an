using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.ExClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class GiftRepository
    {
        public IList<Gift> GetList_GiftAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Gift.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Gift>();
                }
            }
        }

        public long InsertGift(Gift _gift)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _gift.DateCreated = DateTime.Now;  
                    _data.Gift.Add(_gift);
                    _data.SaveChanges();

                    return _gift.GiftId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }

            }
        }


        public bool UpdateGift(Gift _gift)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    Gift giftToUpdate;
                    giftToUpdate = entities.Gift.Where(x => x.GiftId == _gift.GiftId).FirstOrDefault();

                    giftToUpdate.BannerMediaId = _gift.BannerMediaId ?? giftToUpdate.BannerMediaId;
                    giftToUpdate.GiftName = _gift.GiftName ?? giftToUpdate.GiftName;
                    giftToUpdate.Description = _gift.Description??giftToUpdate.Description;
                    giftToUpdate.DateModified = DateTime.Now;
                    giftToUpdate.IsDeleted = _gift.IsDeleted??giftToUpdate.IsDeleted;
                    giftToUpdate.GiftName = _gift.GiftName??giftToUpdate.GiftName;
                    giftToUpdate.GiftPrice = _gift.GiftPrice ?? giftToUpdate.GiftPrice;

                    entities.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return false;
                }
            }
        }

        public Gift Get_GiftById(long _giftID)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Gift.Find(_giftID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }

        public IList<Gift> GetList_GiftAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Gift.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Gift>();
                }
            }
        }
    }
}
