using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**********************************************************************************************************
 * webpage name: MainRecordRepository.cs
 * originator: THIEN TRAN
 * date: 17/11/2014
 * adapt from: 
 * description: MainRecord implement from IMainRecordRepository.

 * update history:
 * - 21/11/2014: Add implement methods.

 **********************************************************************************************************/

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class MainRecordRepository : IMainRecordRepository
    {

        public IList<MainRecord> GetAll()
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.MainRecords.ToList();
            }
        }

        public IList<MainRecord> GetList_IsDeleted(bool isDeleted)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.MainRecords.Where(a => a.IsDeleted == isDeleted).ToList();
            }
        }

        public MainRecord GetById(long mainRecordId)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.MainRecords.Find(mainRecordId);
            }
        }

        public long Insert(MainRecord mainRecord)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    entities.MainRecords.Add(mainRecord);
                    entities.SaveChanges();
                    return mainRecord.MainRecordId;
                }
                catch
                {
                    return -1;
                }
            }
        }
        public long Create (MainRecord mainRecord)
        {
            try
            {
                TTS_DBEntities _data= new TTS_DBEntities();
                _data.MainRecords.Add(mainRecord);
                _data.SaveChanges();
                return mainRecord.MainRecordId;
            }
            catch
            {
                return -1;
            }
        }
        public bool Update(MainRecord mainRecord)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    var mainRecordUpdate = this.GetById(mainRecord.MainRecordId);
                    if (mainRecordUpdate != null)
                    {
                        mainRecordUpdate = mainRecord;
                        entities.SaveChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public List<MainRecord> GetBySaleMediaAdmin(string TaskFormCode, List<string> list, long AccountId, List<int?> lstFirst, int OrderQueue, List<int?> lstLast, string q, int filter, int tag)
        {

            //lstFirst Danh sách Request của bộ phận trc
            //lstLast Danh sách Fail của bộ phận sau
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var itemFirst = 0;
                for (int i = Convert.ToInt32(lstFirst[0]); i>=0; i--)
                {
                    var item = _data.TaskDirections.Where(n=>n.OrderQueue == i).FirstOrDefault();
                    if (item!=null && item.IsActive != null && item.IsActive == true)
                    {
                        itemFirst = i;
                        break;
                    }
                }
                var itemLast = 0;
                for (int i = Convert.ToInt32(lstLast[0]); i <= 100; i++)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i).FirstOrDefault();
                    if (item != null && item.IsActive != null && item.IsActive == true)
                    {
                        itemLast = i;
                        break;
                    }
                }
                var lstTaskFirst = _data.TaskDirections.Where(
                    n=> n.OrderQueue == OrderQueue
                    || (itemFirst == n.OrderQueue && n.IsValid == true)
                    || (itemLast == n.OrderQueue && n.IsValid == false)
                    ).ToList();
                List<long> lstTaskString = new List<long>();
                foreach (var item in lstTaskFirst)
                {
                    lstTaskString.Add(item.TaskDirectionId);
                }
                var lst = new List<MainRecord>();

                if (tag == 0)
                {
                    if(filter ==0){
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                            && n.TaskFormCode == TaskFormCode
                            ).ToList();
                        // && n.FormId.ToUpper().Contains(FormId) == true
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                            && n.TaskFormCode == TaskFormCode && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }

                }
                else if (tag == 1)
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                            && n.TaskFormCode == TaskFormCode && n.Title.ToUpper().Contains(q.ToUpper())
                            ).ToList();
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                            && n.TaskFormCode == TaskFormCode && n.Title.ToUpper().Contains(q.ToUpper())
                            && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }
                }
                else
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                            && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(q.ToUpper())
                            ).ToList();
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                            && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(q.ToUpper())
                            && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }
                }
                    //lst = _data.MainRecords.Where(
                    //    n => n.IsDeleted == false
                    //        && list.Contains(n.StatusDirectionCode) == true
                    //        && ( lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                    //        && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(FormId) == true
                    //        ).ToList();
                return lst;
            }
            catch
            {
                return new List<MainRecord>();
            }
        }
        public List<MainRecord> GetByFinanceAdmin(string TaskFormCode, List<string> list, long AccountId, List<int?> lstFirst, int OrderQueue, List<int?> lstLast, string q, int filter, int tag)
        {

            //lstFirst Danh sách Request của bộ phận trc
            //lstLast Danh sách Fail của bộ phận sau
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var itemFirst = 0;
                for (int i = Convert.ToInt32(lstFirst[0]); i >= 0; i--)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i).FirstOrDefault();
                    if (item != null && item.IsActive != null && item.IsActive == true)
                    {
                        itemFirst = i;
                        break;
                    }
                }
                var itemLast = 0;
                for (int i = Convert.ToInt32(lstLast[0]); i <= 100; i++)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i).FirstOrDefault();
                    if (item != null && item.IsActive != null && item.IsActive == true)
                    {
                        itemLast = i;
                        break;
                    }
                }
                var lstTaskFirst = _data.TaskDirections.Where(
                    n => n.OrderQueue == OrderQueue
                    || (itemFirst == n.OrderQueue && n.IsValid == true)
                    || (itemLast == n.OrderQueue && n.IsValid == false)
                    ).ToList();
                List<long> lstTaskString = new List<long>();
                foreach (var item in lstTaskFirst)
                {
                    lstTaskString.Add(item.TaskDirectionId);
                }
                var lst = new List<MainRecord>();

                if (tag == 0)
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId))
                            && n.TaskFormCode == TaskFormCode
                            ).ToList();
                        // && n.FormId.ToUpper().Contains(FormId) == true
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId))
                            && n.TaskFormCode == TaskFormCode && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }

                }
                else if (tag == 1)
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId))
                            && n.TaskFormCode == TaskFormCode && n.Title.ToUpper().Contains(q.ToUpper())
                            ).ToList();
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId))
                            && n.TaskFormCode == TaskFormCode && n.Title.ToUpper().Contains(q.ToUpper())
                            && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }
                }
                else
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId))
                            && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(q.ToUpper())
                            ).ToList();
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId))
                            && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(q.ToUpper())
                            && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }
                }
                //lst = _data.MainRecords.Where(
                //    n => n.IsDeleted == false
                //        && list.Contains(n.StatusDirectionCode) == true
                //        && ( lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                //        && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(FormId) == true
                //        ).ToList();
                return lst;
            }
            catch
            {
                return new List<MainRecord>();
            }
        }
        public List<MainRecord> GetBySaleMedia(string TaskFormCode, List<string> list, string Assign, long AccountId, int OrderQueue, string FormId, int filter, int tag)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                
                var lst = new List<MainRecord>();
                if (OrderQueue == 0)
                {
                    lst = _data.MainRecords.Where(
                       n => n.IsDeleted == false
                            && ((list.Contains(n.StatusDirectionCode) == true && n.HoldByStaffId == AccountId) || (n.StatusDirectionCode == Assign))
                            && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(FormId) == true).ToList();
                }
                else
                {
                    var lstTaskFirst = _data.TaskDirections.Where(n => n.OrderQueue == OrderQueue).ToList();
                    List<long> lstTaskString = new List<long>();
                    foreach (var item in lstTaskFirst)
                    {
                        lstTaskString.Add(item.TaskDirectionId);
                    }
                    lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && ((list.Contains(n.StatusDirectionCode) == true
                            && n.HoldByStaffId == AccountId) || (n.StatusDirectionCode == Assign)
                             && lstTaskString.Contains(n.TaskDirectionId)
                            )
                        && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(FormId) == true).ToList();
                }
                   
                return lst;
            }
            catch
            {
                return new  List<MainRecord>();
            }
        }
        public List<MainRecord> GetByFinance(string TaskFormCode, List<string> list, string Assign, long AccountId, int OrderQueue, string FormId, int filter, int tag)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();

                var lst = new List<MainRecord>();
                if (OrderQueue == 0)
                {
                    lst = _data.MainRecords.Where(
                       n => n.IsDeleted == false
                            && ((list.Contains(n.StatusDirectionCode) == true && n.HoldByStaffId == AccountId) || (n.StatusDirectionCode == Assign))
                            && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(FormId) == true).ToList();
                }
                else
                {
                    var lstTaskFirst = _data.TaskDirections.Where(n => n.OrderQueue == OrderQueue).ToList();
                    List<long> lstTaskString = new List<long>();
                    foreach (var item in lstTaskFirst)
                    {
                        lstTaskString.Add(item.TaskDirectionId);
                    }
                    lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && (
                            (list.Contains(n.StatusDirectionCode) == true && n.HoldByStaffId == AccountId) || (n.StatusDirectionCode == Assign)
                             && lstTaskString.Contains(n.TaskDirectionId)
                            )
                        && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(FormId) == true).ToList();
                }

                return lst;
            }
            catch
            {
                return new List<MainRecord>();
            }
        }
        public List<MainRecord> GetByCustomerService(List<string> TaskFormCode, List<string> list, string Assign, long AccountId, int OrderQueue, string q, int filter, int tag)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();

                var lst = new List<MainRecord>();
                if (OrderQueue == 0)
                {
                    lst = _data.MainRecords.Where(
                       n => n.IsDeleted == false
                            && ((list.Contains(n.StatusDirectionCode) == true && n.HoldByStaffId == AccountId) || (n.StatusDirectionCode == Assign))
                            && TaskFormCode.Contains(n.TaskFormCode) && n.FormId.ToUpper().Contains(q) == true).ToList();
                }
                else
                {
                    var lstTaskFirst = _data.TaskDirections.Where(n => n.OrderQueue == OrderQueue && TaskFormCode.Contains(n.TaskFormCode)).ToList();
                    List<long> lstTaskString = new List<long>();
                    foreach (var item in lstTaskFirst)
                    {
                        lstTaskString.Add(item.TaskDirectionId);
                    }
                    lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && (
                            (list.Contains(n.StatusDirectionCode) == true && n.HoldByStaffId == AccountId )
                            || (n.StatusDirectionCode == Assign)
                             && lstTaskString.Contains(n.TaskDirectionId)
                            )
                        && TaskFormCode.Contains(n.TaskFormCode) && n.FormId.ToUpper().Contains(q) == true).ToList();
                }

                return lst;
            }
            catch
            {
                return new List<MainRecord>();
            }
        }
        public List<MainRecord> GetByCustomerServiceAdmin(List<string> TaskFormCode, List<string> list, long AccountId, List<int?> lstFirst, int OrderQueue, List<int?> lstLast, string q, int filter, int tag)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var itemFirst = 0;
                for (int i = Convert.ToInt32(lstFirst[0]); i >= 0; i--)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i).FirstOrDefault();
                    if (item != null && item.IsActive != null && item.IsActive == true)
                    {
                        itemFirst = i;
                        break;
                    }
                }
                var itemLast = 0;
                for (int i = Convert.ToInt32(lstLast[0]); i <= 100; i++)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i).FirstOrDefault();
                    if (item != null && item.IsActive != null && item.IsActive == true)
                    {
                        itemLast = i;
                        break;
                    }
                }
                var lstTaskFirst = _data.TaskDirections.Where(
                    n => (n.OrderQueue == OrderQueue
                    || (itemFirst == n.OrderQueue && n.IsValid == true)
                    || (itemLast == n.OrderQueue && n.IsValid == false))
                    && TaskFormCode.Contains(n.TaskFormCode)
                    ).ToList();
                List<long> lstTaskString = new List<long>();
                foreach (var item in lstTaskFirst)
                {
                    lstTaskString.Add(item.TaskDirectionId);
                }
                var lst = new List<MainRecord>();

                if (tag == 0)
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (n.TaskDirectionId ==null || n.TaskDirectionId ==0 ||lstTaskString.Contains(n.TaskDirectionId))
                            && TaskFormCode.Contains(n.TaskFormCode)
                            ).ToList();
                        // && n.FormId.ToUpper().Contains(FormId) == true
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == null || n.TaskDirectionId == 0)
                            && TaskFormCode.Contains(n.TaskFormCode) && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }

                }
                else if (tag == 1)
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == null || n.TaskDirectionId == 0)
                            && TaskFormCode.Contains(n.TaskFormCode) && n.Title.ToUpper().Contains(q.ToUpper())
                            ).ToList();
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == null|| n.TaskDirectionId ==0)
                            && TaskFormCode.Contains(n.TaskFormCode) && n.Title.ToUpper().Contains(q.ToUpper())
                            && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }
                }
                else
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == null|| n.TaskDirectionId ==0)
                            && TaskFormCode.Contains(n.TaskFormCode) && n.FormId.ToUpper().Contains(q.ToUpper())
                            ).ToList();
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == null || n.TaskDirectionId == 0)
                            && TaskFormCode.Contains(n.TaskFormCode) && n.FormId.ToUpper().Contains(q.ToUpper())
                            && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }
                }
                //lst = _data.MainRecords.Where(
                //    n => n.IsDeleted == false
                //        && list.Contains(n.StatusDirectionCode) == true
                //        && ( lstTaskString.Contains(n.TaskDirectionId) || n.TaskDirectionId == 0)
                //        && n.TaskFormCode == TaskFormCode && n.FormId.ToUpper().Contains(FormId) == true
                //        ).ToList();
                return lst;
            }
            catch
            {
                return new List<MainRecord>();
            }
        }
        public List<MainRecord> GetByCustomerServiceAdminNotNull(List<string> TaskFormCode, List<string> list, long AccountId, List<int?> lstFirst, int OrderQueue, List<int?> lstLast, string q, int filter, int tag)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var itemFirst = 0;
                for (int i = Convert.ToInt32(lstFirst[0]); i >= 0; i--)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i).FirstOrDefault();
                    if (item != null && item.IsActive != null && item.IsActive == true)
                    {
                        itemFirst = i;
                        break;
                    }
                }
                var itemLast = 0;
                for (int i = Convert.ToInt32(lstLast[0]); i <= 100; i++)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i).FirstOrDefault();
                    if (item != null && item.IsActive != null && item.IsActive == true)
                    {
                        itemLast = i;
                        break;
                    }
                }
                var lstTaskFirst = _data.TaskDirections.Where(
                    n => (n.OrderQueue == OrderQueue
                    || (itemFirst == n.OrderQueue && n.IsValid == true)
                    || (itemLast == n.OrderQueue && n.IsValid == false))
                    && TaskFormCode.Contains(n.TaskFormCode)
                    ).ToList();
                List<long> lstTaskString = new List<long>();
                foreach (var item in lstTaskFirst)
                {
                    lstTaskString.Add(item.TaskDirectionId);
                }
                var lst = new List<MainRecord>();

                if (tag == 0)
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (n.TaskDirectionId != null && lstTaskString.Contains(n.TaskDirectionId))
                            && TaskFormCode.Contains(n.TaskFormCode)
                            ).ToList();
                        // && n.FormId.ToUpper().Contains(FormId) == true
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (n.TaskDirectionId != null && lstTaskString.Contains(n.TaskDirectionId))
                            && TaskFormCode.Contains(n.TaskFormCode) && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }

                }
                else if (tag == 1)
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (n.TaskDirectionId != null && lstTaskString.Contains(n.TaskDirectionId))
                            && TaskFormCode.Contains(n.TaskFormCode) && n.Title.ToUpper().Contains(q.ToUpper())
                            ).ToList();
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (n.TaskDirectionId != null && lstTaskString.Contains(n.TaskDirectionId) )
                            && TaskFormCode.Contains(n.TaskFormCode) && n.Title.ToUpper().Contains(q.ToUpper())
                            && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }
                }
                else
                {
                    if (filter == 0)
                    {
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (n.TaskDirectionId != null && lstTaskString.Contains(n.TaskDirectionId) )
                            && TaskFormCode.Contains(n.TaskFormCode) && n.FormId.ToUpper().Contains(q.ToUpper())
                            ).ToList();
                    }
                    else
                    {
                        var filterCode = _data.StatusDirections.Find(filter);
                        lst = _data.MainRecords.Where(
                        n => n.IsDeleted == false
                            && list.Contains(n.StatusDirectionCode) == true
                            && (n.TaskDirectionId != null && lstTaskString.Contains(n.TaskDirectionId) )
                            && TaskFormCode.Contains(n.TaskFormCode) && n.FormId.ToUpper().Contains(q.ToUpper())
                            && n.StatusDirectionCode.Contains(filterCode.StatusDirectionCode)
                            ).ToList();
                    }
                }
                return lst;
            }
            catch
            {
                return new List<MainRecord>();
            }
        }
        public bool Remove(long mainRecordId)
        {
            throw new NotImplementedException();
        }
        public bool Edit(MainRecord mainRecord)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var main = _data.MainRecords.Find(mainRecord.MainRecordId);
                main.DateModified = mainRecord.DateModified;
                main.DateClosed = mainRecord.DateModified;
                main.HoldByManagerId = mainRecord.HoldByManagerId;
                main.HoldByStaffId = mainRecord.HoldByStaffId;
                main.IsDeleted = mainRecord.IsDeleted;
                main.StatusDirectionCode = mainRecord.StatusDirectionCode;
                main.StatusProcessCode = mainRecord.StatusProcessCode;
                main.TaskDirectionId = mainRecord.TaskDirectionId;
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditHoldManager(long AccountId, long MainRecordId)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var main = _data.MainRecords.Find(MainRecordId);
                main.HoldByManagerId = AccountId;
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditHoldStaff(long AccountId, long MainRecordId)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var main = _data.MainRecords.Find(MainRecordId);
                main.HoldByStaffId = AccountId;
                _data.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditTaskDirection(long TaskDirectionId, long MainRecordId)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var main = _data.MainRecords.Find(MainRecordId);
                main.TaskDirectionId = TaskDirectionId;
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