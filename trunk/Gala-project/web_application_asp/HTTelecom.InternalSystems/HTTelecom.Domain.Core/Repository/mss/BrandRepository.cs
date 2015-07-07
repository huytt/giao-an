using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.ExClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class BrandRepository
    {
        public IList<Brand> GetList_BrandAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Brand.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Brand>();
                }
            }
        }

        public IList<Brand> GetList_BrandAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Brand.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Brand>();
                }
            }
        }

        public IList<Brand> GetList_BrandAll_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Brand.Where(a => a.IsActive == IsActive).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Brand>();
                }
            }
        }

        public IList<Brand> GetList_BrandAll_IsDeleted_IsActive(bool IsDeleted, bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Brand.Where(a => a.IsDeleted == IsDeleted).Where(b => b.IsActive == IsActive).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Brand>();
                }
            }
        }

        public Brand Get_BrandById(long BrandId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Brand.Find(BrandId);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }

        public long Insert(Brand Brand)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Brand.DateCreated = DateTime.Now;
                    Brand.Alias = Generates.generateAlias(Brand.BrandName);
                    Brand.VisitCount = 0;
                    Brand.IsActive = true;
                    Brand.IsDeleted = false;
                    _data.Brand.Add(Brand);
                    _data.SaveChanges();

                    return Brand.BrandId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }

            }
        }

        public bool Update(Brand Brand)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    Brand BrandToUpdate;
                    BrandToUpdate = entities.Brand.Where(x => x.BrandId == Brand.BrandId).FirstOrDefault();

                    BrandToUpdate.LogoMediaId = Brand.LogoMediaId ?? BrandToUpdate.LogoMediaId;
                    BrandToUpdate.BannerMediaId = Brand.BannerMediaId ?? BrandToUpdate.BannerMediaId;
                    BrandToUpdate.BrandName = Brand.BrandName ?? BrandToUpdate.BrandName;
                    BrandToUpdate.Alias = Generates.generateAlias(Brand.BrandName) ?? BrandToUpdate.Alias;
                    BrandToUpdate.Description = Brand.Description ?? BrandToUpdate.Description;
                    BrandToUpdate.MetaTitle = Brand.MetaTitle ?? BrandToUpdate.MetaTitle;
                    BrandToUpdate.MetaKeywords = Brand.MetaKeywords ?? BrandToUpdate.MetaKeywords;
                    BrandToUpdate.MetaDescription = Brand.MetaDescription ?? BrandToUpdate.MetaDescription;
                    BrandToUpdate.DateModified = DateTime.Now;
                    BrandToUpdate.IsActive = Brand.IsActive ?? BrandToUpdate.IsActive;
                    BrandToUpdate.IsDeleted = Brand.IsDeleted ?? BrandToUpdate.IsDeleted;

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

        public bool UpdateActive(long brandId, bool isActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Brand brand = _data.Brand.Where(x => x.BrandId == brandId).FirstOrDefault();

                    brand.IsActive = isActive;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

    }
}
