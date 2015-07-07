using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**********************************************************************************************************
 * webpage name: SubRecordRepository.cs
 * originator: THIEN TRAN
 * date: 17/11/2014
 * adapt from: 
 * description: SubRecord implement from ISubRecordRepository.

 * update history:
 * - 21/11/2014: Add implement methods.

 **********************************************************************************************************/

namespace HTTelecom.Domain.Core.Repository.tts
{
    
    public class SubRecordRepository : ISubRecordRepository
    {

        public IList<SubRecord> GetAll()
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.SubRecords.ToList();
            }
        }

        public IList<SubRecord> GetList_IsDeleted(bool isDeleted)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.SubRecords.Where(a => a.IsDeleted == isDeleted).ToList();
            }
        }

        public IList<SubRecord> GetList_SubRecordByMainRecordId(long mainRecordId)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    return entities.SubRecords.Where(a => a.MainRecordId == mainRecordId).ToList();
                }
                catch
                {
                    return new List<SubRecord>();
                }

            }
        }

        public SubRecord GetById(long subRecordId)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.SubRecords.Find(subRecordId);
            }
        }
        public long Create(SubRecord subRecord)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                _data.SubRecords.Add(subRecord);
                _data.SaveChanges();
                return subRecord.SubRecordId;
            }
            catch
            {
                return -1;
            }
        }
        public long Insert(SubRecord subRecord)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    entities.SubRecords.Add(subRecord);
                    entities.SaveChanges();
                    return subRecord.SubRecordId;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }

        public bool Update(SubRecord subRecord)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    var subRecordUpdate = this.GetById(subRecord.SubRecordId);
                    if (subRecordUpdate != null)
                    {
                        subRecordUpdate = subRecord;
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

        public bool Remove(long subRecordId)
        {
            throw new NotImplementedException();
        }
        public bool EditSubLst(long subRecordId, string json)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var sub = _data.SubRecords.Find(subRecordId);
                sub.SubList = json;
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