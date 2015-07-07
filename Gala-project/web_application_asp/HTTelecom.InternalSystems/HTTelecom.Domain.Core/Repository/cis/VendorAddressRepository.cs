using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.cis;
namespace HTTelecom.Domain.Core.Repository.cis
{
    public class VendorAddressRepository
    {
        public IList<VendorAddress> GetList_VendorAddressAll()
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.VendorAddresses.ToList();
                }
                catch
                {
                    return new List<VendorAddress>();
                }
            }
        }

        public IList<VendorAddress> GetList_VendorAddress_VendorId(long? vendorId)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.VendorAddresses.Where(a => a.VendorId == vendorId).ToList();
                }
                catch
                {
                    return new List<VendorAddress>();
                }
            }
        }

        public IList<VendorAddress> GetList_VendorAddressAll_IsDeleted(bool isDeleted)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.VendorAddresses.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<VendorAddress>();
                }
            }
        }

        public IList<VendorAddress> GetList_VendorAddressAll_IsActive(bool IsActive)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.VendorAddresses.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<VendorAddress>();
                }
            }
        }

        public VendorAddress Get_VendorAddressById(long VendorAddressId)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.VendorAddresses.Find(VendorAddressId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(VendorAddress VendorAddress)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    VendorAddress.DateCreated = DateTime.Now;
                    VendorAddress.DateModified = DateTime.Now;
                    _data.VendorAddresses.Add(VendorAddress);
                    _data.SaveChanges();

                    return VendorAddress.VendorAddressId;
                }
                catch
                {
                    return -1;
                }

            }
        }


        public bool UpdateActive(long VendorAddressId, bool isActive)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    VendorAddress VendorAddress = _data.VendorAddresses.Where(x => x.VendorAddressId == VendorAddressId).FirstOrDefault();

                    VendorAddress.IsActive = isActive;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public bool Update(VendorAddress VendorAddress)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    VendorAddress VendorAddressToUpdate;
                    VendorAddressToUpdate = _data.VendorAddresses.Where(x => x.VendorAddressId == VendorAddress.VendorAddressId).FirstOrDefault();
                    VendorAddressToUpdate.LocationName = VendorAddress.LocationName ?? VendorAddressToUpdate.LocationName;
                    VendorAddressToUpdate.Mobilephone = VendorAddress.Mobilephone;
                    VendorAddressToUpdate.Telephone = VendorAddress.Telephone;
                    VendorAddressToUpdate.Address = VendorAddress.Address ?? VendorAddressToUpdate.Address;
                    VendorAddressToUpdate.Ward = VendorAddress.Ward ?? VendorAddressToUpdate.Ward;
                    VendorAddressToUpdate.District = VendorAddress.District ?? VendorAddressToUpdate.District;
                    VendorAddressToUpdate.City = VendorAddress.City ?? VendorAddressToUpdate.City;
                    VendorAddressToUpdate.Email = VendorAddress.Email ?? VendorAddressToUpdate.Email;
                    VendorAddressToUpdate.GGMapLink = VendorAddress.GGMapLink ?? VendorAddressToUpdate.GGMapLink;
                    VendorAddressToUpdate.DateModified = DateTime.Now;
                    VendorAddressToUpdate.ModifiedBy = VendorAddress.ModifiedBy ?? VendorAddressToUpdate.ModifiedBy;
                    VendorAddressToUpdate.IsActive = VendorAddress.IsActive ?? VendorAddressToUpdate.IsActive;
                    VendorAddressToUpdate.IsDeleted = VendorAddress.IsDeleted ?? VendorAddressToUpdate.IsDeleted;

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
