using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.cis;
using PagedList;
namespace HTTelecom.Domain.Core.Repository.cis
{
    public class VendorRepository
    {

        public IList<Vendor> GetList_VendorAll()
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Vendors.ToList();
                }
                catch
                {
                    return new List<Vendor>();
                }
            }
        }

        //public IList<Vendor> GetList_Vendor_VendorTypeCode(string VendorTypeCode)
        //{
        //    using (CIS_DBEntities _data = new CIS_DBEntities())
        //    {
        //        _data.Configuration.ProxyCreationEnabled = false;
        //        _data.Configuration.LazyLoadingEnabled = false;
        //        try
        //        {
        //            return _data.Vendors.Where(a => a.VendorTypeCode == VendorTypeCode).OrderBy(b => b.VendorName).ToList();
        //        }
        //        catch
        //        {
        //            return new List<Vendor>();
        //        }
        //    }
        //}

        public IList<Vendor> GetList_VendorAll_IsDeleted(bool isDeleted)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.Vendors.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Vendor>();
                }
            }
        }

        public IList<Vendor> GetList_VendorAll_IsActive(bool IsActive)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.Vendors.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Vendor>();
                }
            }
        }
        public IList<Vendor> GetList_VendorAll_IsDeleted_IsActive(bool IsDeleted, bool IsActive)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Vendors.Where(a => a.IsDeleted == IsDeleted)
                                      .Where(b => b.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Vendor>();
                }
            }
        }

        public Vendor Get_VendorById(long VendorId)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.Vendors.Find(VendorId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(Vendor Vendor)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    Vendor.DateCreated = DateTime.Now;

                    _data.Vendors.Add(Vendor);
                    _data.SaveChanges();

                    return Vendor.VendorId;
                }
                catch
                {
                    return -1;
                }

            }
        }
        public bool Update(Vendor Vendor)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    Vendor VendorToUpdate;
                    VendorToUpdate = _data.Vendors.Where(x => x.VendorId == Vendor.VendorId).FirstOrDefault();
                    VendorToUpdate.ContractId = Vendor.ContractId ?? VendorToUpdate.ContractId;
                    VendorToUpdate.VendorEmail = Vendor.VendorEmail ?? VendorToUpdate.VendorEmail;
                    VendorToUpdate.VendorFullName = Vendor.VendorFullName ?? VendorToUpdate.VendorFullName;
                    VendorToUpdate.CompanyName = Vendor.CompanyName ?? VendorToUpdate.CompanyName;
                    VendorToUpdate.LinkWebsite = Vendor.LinkWebsite ?? VendorToUpdate.LinkWebsite;
                    VendorToUpdate.LogoFile = Vendor.LogoFile ?? VendorToUpdate.LogoFile;
                    VendorToUpdate.Description = Vendor.Description ?? VendorToUpdate.Description;
                    VendorToUpdate.CommonService = Vendor.CommonService ?? VendorToUpdate.CommonService;
                    VendorToUpdate.DateModified = DateTime.Now;
                    VendorToUpdate.ModifiedBy = Vendor.ModifiedBy ?? VendorToUpdate.ModifiedBy;
                    VendorToUpdate.IsActive = Vendor.IsActive ?? VendorToUpdate.IsActive;
                    VendorToUpdate.IsDeleted = Vendor.IsDeleted ?? VendorToUpdate.IsDeleted;

                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public IPagedList<Vendor> GetList_VendorPagingAll(int pageNum, int pageSize)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Vendor = _data.Vendors.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);
                    return lst_Vendor;
                }
                catch
                {
                    return new PagedList<Vendor>(new List<Vendor>(), 1, pageSize);
                }
            }
        }
    }
}
