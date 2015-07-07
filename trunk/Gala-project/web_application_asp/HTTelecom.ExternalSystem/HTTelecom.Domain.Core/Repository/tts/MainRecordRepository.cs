using HTTelecom.Domain.Core.Common;
using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class MainRecordRepository
    {
        public long Create(MainRecord mRecord)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                _data.MainRecord.Add(mRecord);
                _data.SaveChanges();
                return mRecord.MainRecordId;
            }
            catch
            {
                return -1;
            }
        }

        public void CreateFullMain(MainRecord mRecord, SubRecord subRecord, SubRecordJson sub)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (TTS_DBEntities _data = new TTS_DBEntities())
                {
                    _data.MainRecord.Add(mRecord);
                    _data.SaveChanges();
                    subRecord.MainRecordId = mRecord.MainRecordId;
                    sub.MainRecordId = mRecord.MainRecordId.ToString();
                    Common.Common common = new Common.Common();
                    subRecord.SubList = "[" + common.SubRecordJsontoString(sub) + "]";
                    _data.SubRecord.Add(subRecord);
                    _data.SaveChanges();
                }
                scope.Complete(); // If you are happy
            }
        }
    }
}
