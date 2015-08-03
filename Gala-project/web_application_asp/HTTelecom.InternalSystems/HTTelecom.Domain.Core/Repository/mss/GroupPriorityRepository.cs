using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class GroupPriorityRepository
    {
        public IList<GroupPriority> GetList_GroupPriorityAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.GroupPriority.ToList();
                }
                catch
                {
                    return new List<GroupPriority>();
                }
            }
        }

        public IList<GroupPriority> GetList_GroupPriorityAll(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.GroupPriority.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<GroupPriority>();
                }
            }
        }

        public GroupPriority Get_GroupPriorityById(long GroupPriorityId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.GroupPriority.Find(GroupPriorityId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(GroupPriority GroupPriority)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.GroupPriority.Add(GroupPriority);
                    _data.SaveChanges();

                    return GroupPriority.GroupPriorityId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(GroupPriority GroupPriority)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    GroupPriority GroupPriorityToUpdate;
                    GroupPriorityToUpdate = entities.GroupPriority.Where(x => x.GroupPriorityId == GroupPriority.GroupPriorityId).FirstOrDefault();
                    GroupPriorityToUpdate = GroupPriority;
                    entities.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
