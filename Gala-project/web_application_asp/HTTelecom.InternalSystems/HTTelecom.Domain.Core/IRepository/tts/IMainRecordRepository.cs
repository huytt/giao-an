using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.IRepository.tts
{
    public interface IMainRecordRepository
    {
        IList<MainRecord> GetAll();
        IList<MainRecord> GetList_IsDeleted(bool isDeleted);
        MainRecord GetById(long mainRecordId);
        long Insert(MainRecord mainRecord);
        bool Update(MainRecord mainRecord);
        bool Remove(long mainRecordId);
        long Create(MainRecord model);
        List<MainRecord> GetBySaleMediaAdmin(string TaskFormCode, List<string> list, long AccountId, List<int?> lstFirst, int OrderQueue, List<int?> lstLast, string q, int filter, int tag);
        List<MainRecord> GetByFinanceAdmin(string TaskFormCode, List<string> list, long AccountId, List<int?> lstFirst, int OrderQueue, List<int?> lstLast, string q, int filter, int tag);
        List<MainRecord> GetBySaleMedia(string TaskFormCode, List<string> list, string Assign, long AccountId, int OrderQueue, string q, int filter, int tag);
        List<MainRecord> GetByFinance(string TaskFormCode, List<string> list, string Assign, long AccountId, int OrderQueue, string q, int filter, int tag);
        bool Edit(MainRecord mainRecord);

        List<MainRecord> GetByCustomerService(List<string> TaskFormCode, List<string> list, string Assign, long AccountId, int OrderQueue, string q, int filter, int tag);

        List<MainRecord> GetByCustomerServiceAdmin(List<string> TaskFormCode, List<string> list, long AccountId, List<int?> lstFirst, int OrderQueue, List<int?> lstLast, string q, int filter, int tag);
        List<MainRecord> GetByCustomerServiceAdminNotNull(List<string> TaskFormCode, List<string> list, long AccountId, List<int?> lstFirst, int OrderQueue, List<int?> lstLast, string q, int filter, int tag);
    }
}
