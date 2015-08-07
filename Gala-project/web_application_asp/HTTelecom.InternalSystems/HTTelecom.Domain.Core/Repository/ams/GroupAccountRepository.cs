using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class GroupAccountRepository
    {
        public IList<GroupAccount> GetList_GroupAccountAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.GroupAccounts.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                    return new List<GroupAccount>();
                }
            }
        }
        public long InsertGroupAccount(GroupAccount _GroupAccount)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    _data.GroupAccounts.Add(_GroupAccount);
                    _data.SaveChanges();

                    return _GroupAccount.GroupAccountId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                    return -1;
                }
            }
        }
        public bool UpdateGroupAccount(GroupAccount _GroupAccount)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    GroupAccount GroupAccountToUpdate;
                    GroupAccountToUpdate = entities.GroupAccounts.Find(_GroupAccount.GroupAccountId);
           
                    //GroupAccountToUpdate.IsDeleted = _GroupAccount.IsDeleted;// khong cho delete
                    entities.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                    return false;
                }
            }
        }
        public GroupAccount Get_GroupAccountById(long _GroupAccountID)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.GroupAccounts.Find(_GroupAccountID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                    return null;
                }
            }
        }
        public bool CheckExists(long AcccountId, long GroupId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                return _data.GroupAccounts.Where(_ => _.GroupId == GroupId && _.AccountId == AcccountId).ToList().Count > 0;
            }
        }
        public bool DeleteGroupAccount(long _GroupId,long _UserId)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    GroupAccount GroupAccountToUpdate;
                    var tmp = _data.GroupAccounts.Where(_ => _.GroupId == _GroupId && _.AccountId == _UserId).ToList();
                    if (tmp.Count > 0)
                    {
                        GroupAccountToUpdate = tmp[0];
                        _data.GroupAccounts.Remove(GroupAccountToUpdate);
                        _data.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                    return false;
                }
            }
        }
       
    }
}
