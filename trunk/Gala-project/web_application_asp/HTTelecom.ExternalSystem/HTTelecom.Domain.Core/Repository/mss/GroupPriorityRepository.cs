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
                return _data.GroupPriority.Find(_GroupPiorityID);
            }
        }
        public IList<GroupPriority> GetList_GroupPiorityAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                return _data.GroupPriority.ToList();
            }
        }
        public long InsertGroupPiority(GroupPriority _GroupPiority)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.GroupPriority.Add(_GroupPiority);
                _data.SaveChanges();
                return _GroupPiority.GroupPriorityId;
            }
        }
        public bool UpdateGroupPiority(GroupPriority _GroupPiority)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                GroupPriority GroupPiorityToUpdate;
                GroupPiorityToUpdate = entities.GroupPriority.Where(x => x.GroupPriorityId == _GroupPiority.GroupPriorityId).FirstOrDefault();

                GroupPiorityToUpdate.IsDeleted = _GroupPiority.IsDeleted ?? GroupPiorityToUpdate.IsDeleted;
                entities.SaveChanges();

                return true;
            }
        }
    }
}
