using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**********************************************************************************************************
 * webpage name: StatusDirectionRepository.cs
 * originator: THIEN TRAN
 * date: 17/11/2014
 * adapt from: 
 * description: StatusDirection implement from IStatusDirectionRepository.

 * update history:
 * - 21/11/2014: Add implement methods.

 **********************************************************************************************************/

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class StatusDirectionRepository : IStatusDirectionRepository
    {

        public IList<StatusDirection> GetAll()
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {

                    return entities.StatusDirections.ToList();
                }
                catch
                {
                    return new List<StatusDirection>();
                }
            }
        }

        public IList<StatusDirection> GetList_IsDeleted(bool isDeleted)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try { 
                return entities.StatusDirections.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<StatusDirection>();
                }
            }
        }

        public IList<StatusDirection> GetList_IsActived(bool isActived)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try { 
                return entities.StatusDirections.Where(a => a.IsActive == isActived).ToList();
                }
                catch
                {
                    return new List<StatusDirection>();
                }
            }
        }

        public StatusDirection GetById(long statusDirectionId)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try { 
                return
                    entities.StatusDirections.Find(statusDirectionId);
                }
                catch
                {
                    return null;
                }
            }
        }
        public StatusDirection GetByCode(string statusDirectionCode)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.StatusDirections.Where(a => a.StatusDirectionCode == statusDirectionCode).FirstOrDefault();
            }
        }

        public long Insert(StatusDirection statusDirection)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    entities.StatusDirections.Add(statusDirection);
                    entities.SaveChanges();
                    return statusDirection.StatusDirectionId;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }

        public bool Update(StatusDirection statusDirection)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    var statusDirectionUpdate = this.GetById(statusDirection.StatusDirectionId);
                    if (statusDirectionUpdate != null)
                    {
                        statusDirectionUpdate = statusDirection;
                        entities.SaveChanges();
                    }

                    return true;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }
        public List<StatusDirection> GetByListCode(List<string> list)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                return _data.StatusDirections.Where(n => list.Contains(n.StatusDirectionCode)).ToList();
            }
            catch
            {
                return new List<StatusDirection>();
            }
        }
        public bool Remove(long statusDirectionId)
        {
            throw new NotImplementedException();
        }
    }
}