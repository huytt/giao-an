using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.IRepository.tts
{
    public interface ISubRecordRepository
    {
        IList<SubRecord> GetAll();
        IList<SubRecord> GetList_IsDeleted(bool isDeleted);
        IList<SubRecord> GetList_SubRecordByMainRecordId(long mainRecordId);
        SubRecord GetById(long subRecordId);
        long Insert(SubRecord subRecord);
        bool Update(SubRecord subRecord);
        bool Remove(long subRecordId);

        long Create(SubRecord subRecord);


        bool EditSubLst(long subRecordId,string json);
    }
}
