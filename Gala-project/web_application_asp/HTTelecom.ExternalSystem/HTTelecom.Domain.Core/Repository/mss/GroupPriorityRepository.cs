using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class GroupPriorityRepository
    {
        public GroupPriority Get_GroupPiorityById(long _GroupPiorityID)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.GroupPriority.Find(_GroupPiorityID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<GroupPriority> GetList_GroupPiorityAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.GroupPriority.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<GroupPriority>();
                }
            }
        }
        public long InsertGroupPiority(GroupPriority _GroupPiority)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.GroupPriority.Add(_GroupPiority);
                    _data.SaveChanges();
                    return _GroupPiority.GroupPriorityId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }
        public bool UpdateGroupPiority(GroupPriority _GroupPiority)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    GroupPriority GroupPiorityToUpdate;
                    GroupPiorityToUpdate = entities.GroupPriority.Where(x => x.GroupPriorityId == _GroupPiority.GroupPriorityId).FirstOrDefault();

                    GroupPiorityToUpdate.IsDeleted = _GroupPiority.IsDeleted ?? GroupPiorityToUpdate.IsDeleted;
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
