using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class MailTemplateRepository
    {
        public MailTemplate Get_MailTemplateById(long _MailTemplateID)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    return _data.MailTemplate.Find(_MailTemplateID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<MailTemplate> GetList_MailTemplateAll()
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.MailTemplate.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<MailTemplate>();
                }
            }
        }

        public long InsertMailTemplate(MailTemplate _MailTemplate)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                try
                {
                    _MailTemplate.Content = Regex.Replace(_MailTemplate.Content, @"\s+", " ");
                    _data.MailTemplate.Add(_MailTemplate);
                    _data.SaveChanges();
                    return _MailTemplate.MailTemplateId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public bool UpdateMailTemplate(MailTemplate _MailTemplate)
        {
            using (CIS_DBEntities entities = new CIS_DBEntities())
            {
                try
                {
                    MailTemplate MailTemplateToUpdate;
                    MailTemplateToUpdate = entities.MailTemplate.Where(x => x.MailTemplateId == _MailTemplate.MailTemplateId).FirstOrDefault();
                    MailTemplateToUpdate.Content = _MailTemplate.Content ?? MailTemplateToUpdate.Content;
                    MailTemplateToUpdate.ShortDescription = _MailTemplate.ShortDescription ?? MailTemplateToUpdate.ShortDescription;
                    MailTemplateToUpdate.DateCreated = _MailTemplate.DateCreated ?? MailTemplateToUpdate.DateCreated;
                    MailTemplateToUpdate.CreatedBy = _MailTemplate.CreatedBy ?? MailTemplateToUpdate.CreatedBy;
                    MailTemplateToUpdate.DateModified = _MailTemplate.DateModified ?? MailTemplateToUpdate.DateModified;
                    MailTemplateToUpdate.ModifiedBy = _MailTemplate.ModifiedBy ?? MailTemplateToUpdate.ModifiedBy;
                    MailTemplateToUpdate.IsDeleted = _MailTemplate.IsDeleted ?? MailTemplateToUpdate.IsDeleted;
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
    }
}
