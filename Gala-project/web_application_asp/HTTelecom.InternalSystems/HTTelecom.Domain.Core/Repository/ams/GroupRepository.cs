using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class GroupRepository
    {
        public IList<Group> GetList_GroupAll()
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Groups.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                    return new List<Group>();
                }
            }
        }
        public long InsertGroup(Group _Group)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    _data.Groups.Add(_Group);
                    _data.SaveChanges();

                    return _Group.GroupId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                    return -1;
                }
            }
        }
        public bool UpdateGroup(Group _Group)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    Group GroupToUpdate;
                    GroupToUpdate = entities.Groups.Find(_Group.GroupId);
                    GroupToUpdate.GroupLevel = _Group.GroupLevel;
                    GroupToUpdate.OrgRoleId = _Group.OrgRoleId;
                    GroupToUpdate.GroupParentId = _Group.GroupParentId;
                    GroupToUpdate.Description = _Group.Description;
                    GroupToUpdate.GroupName = _Group.GroupName;
                    GroupToUpdate.IsActive = _Group.IsActive;
                    //GroupToUpdate.IsDeleted = _Group.IsDeleted;// khong cho delete
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
        public Group Get_GroupById(long _GroupID)
        {
            using (AMS_DBEntities _data = new AMS_DBEntities())
            {
                try
                {
                    return _data.Groups.Find(_GroupID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                    return null;
                }
            }
        }
    }
}
