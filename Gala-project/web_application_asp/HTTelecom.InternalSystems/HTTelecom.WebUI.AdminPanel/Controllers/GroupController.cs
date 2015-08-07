using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.WebUI.AdminPanel.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.AdminPanel.Controllers
{
    [SessionLoginFilter]
    public class GroupController : Controller
    {
        //
        // GET: /Group/
        public ActionResult Index()
        {            
            List<string> Error = new List<string>();
            try 
	        { 
                GroupRepository _iGroupService = new GroupRepository();
                AccountRepository _iAccountService = new AccountRepository();
                GroupAccountRepository _iGroupAccountService = new GroupAccountRepository();
                #region load list group
                string listGroup = "{\"data\":[";
                foreach (var item in _iGroupService.GetList_GroupAll())
                {
                    listGroup += "{\"GroupId\" : " + item.GroupId.ToString()
                        + ", \"OrgRoleId\": " + ((item.OrgRoleId == null) ? "0" : item.OrgRoleId .ToString())
                        + ", \"GroupParentId\": " + item.GroupParentId.ToString()
                        + ", \"GroupName\": \"" + item.GroupName + "\""
                        + ", \"Description\": \"" + item.Description + "\""
                        + ", \"IsActive\": \"" + item.IsActive + "\""
                        + ", \"IsDeleted\": \"" + item.IsDeleted + "\""
                        + "},";
                }
                listGroup += "]}";
                listGroup = listGroup.Replace("},]","}]");
                ViewBag.ListGroup = listGroup;
                #endregion
                #region load list Group account
                listGroup = "{\"GroupAccount\":[";
                foreach (var item in _iGroupAccountService.GetList_GroupAccountAll())
                {
                    Account acc = _iAccountService.Get_AccountById((long)item.AccountId);
                    listGroup += "{\"GroupId\" : " + item.GroupId.ToString()
                        + ", \"AccountId\": " + item.AccountId.ToString()
                        + ", \"FullName\": \"" + acc.FullName + "\""
                        + ", \"Email\": \"" + acc.Email + "\""           
                        + "},";
                }
                listGroup += "]}";
                listGroup = listGroup.Replace("},]", "}]");
                ViewBag.ListGroupAccount = listGroup;
                #endregion
            }
	        catch (Exception ex)
	        {
                Error.Add(ex.Message);
	        }
            if (Error.Count>0)
                TempData["ErrorMessage"] = Error;          
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long ? id)
        {
            List<string> Error = new List<string>();
            GroupRepository _iGroupService = new GroupRepository();
            Group Group = new Group();
            if (id == null)
            {
                Error.Add("GroupId does not exist.");
            }
            else
            {                
                try
                {                    
                    Group = _iGroupService.Get_GroupById((long)id);
                }
                catch (Exception ex)
                {
                    Error.Add(ex.Message);
                }
            }           
           
            if (Error.Count > 0)
            {
                TempData["ErrorMessage"] = Error;
                LoadGroup(0);
                return RedirectToAction("Index","Group");
            }
            LoadGroup((long)id);
            return View(Group);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Group _group, string ListUser,string Description)
        {
            List<string> Error = new List<string>();
            GroupRepository _iGroupService = new GroupRepository();
            try
            {
                if (_group.GroupParentId == null || (_iGroupService.Get_GroupById((long)_group.GroupParentId) == null) || _group.GroupParentId == _group.GroupId)
                {
                    _group.GroupParentId = 0;
                    _group.GroupLevel = 0;
                }
                else
                {
                    Group GParent = _iGroupService.Get_GroupById((long)_group.GroupParentId);
                    _group.GroupLevel = GParent.GroupLevel + 1;
                }

                if (ValidateGroup(_group))
                {
                    if (_iGroupService.UpdateGroup(_group))
                    {                       
                        UpdateChildLevel(_group);
                        if (ListUser != "")
                        { 
                            List<string> lstUserId = ListUser.Substring(0,ListUser.Length-1).Split('|').ToList();
                            if (!AddListUserToGroup(lstUserId.Select(s => long.Parse(s)).ToList(), _group.GroupId, Description))                       
                            {
                                Error.Add("Update Group Successfully. But insert User to Group is fails.");     
                            } 
                        }
                        LoadGroup(_group.GroupId);
                        return RedirectToAction("Index", "Group");                   
                    }
                    else
                    { 
                        Error.Add("Update Fails.");                       
                    }
                }
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
            }
            if (Error.Count > 0)
            {
                TempData["ErrorMessage"] = Error;
            }
            LoadGroup(_group.GroupId);
            return View(_group);
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<string> Error = new List<string>();
            Group _group = new Group();
            try
            {
                this.LoadGroup(0);
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
            }
            if (Error.Count > 0)
                TempData["ErrorMessage"] = Error;
            return View(_group);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Group _group)
        {
            List<string> Error = new List<string>();
            GroupRepository _iGroupService = new GroupRepository();
      
            try
            {
                if (_group.GroupParentId == null || (_iGroupService.Get_GroupById((long)_group.GroupParentId) == null))
                {
                    _group.GroupParentId = 0;
                    _group.GroupLevel = 0;
                }
                else
                { 
                    Group GParent = _iGroupService.Get_GroupById((long) _group.GroupParentId);
                    _group.GroupLevel = GParent.GroupLevel + 1;
                }

                if (ValidateGroup(_group))
                {
                    if (_iGroupService.InsertGroup(_group)==-1)   
                    {
                        Error.Add("Update Fails.");
                    }
                    else
                    {
                        return RedirectToAction("Index","Group");
                    }
                }               
            }
            catch (Exception ex)
            {
                Error.Add(ex.Message);
            }
            if(Error.Count>0)
            {
                TempData["ErrorMessage"] = Error;
            }
            LoadGroup(0);
            return View(_group);
        }
        private void LoadGroup(long _GroupId)
        {
            GroupRepository _iGroupService = new GroupRepository();
            ActionTypeRepository _iActionTypeService = new ActionTypeRepository();
            #region load groud != 0
            if (_GroupId != 0)
            { 
                AccountRepository _iAccountService = new AccountRepository();
                GroupAccountRepository _iGroupAccountService = new GroupAccountRepository();
                var tmp_lstUser = _iAccountService.GetList_AccountAll().Where(_ => _.IsDeleted == false).ToList();                
                ViewBag.ListUser = tmp_lstUser;
                var lstIdUserInGroup = _iGroupAccountService.GetList_GroupAccountAll().Where(_ => _.GroupId == _GroupId).Select(x => x.AccountId).ToList<long?>();
                ViewBag.ListUserInGroup = _iAccountService.GetList_AccountAll().Where(_ => _.IsDeleted == false && (lstIdUserInGroup.Where(k=>k == _.AccountId).ToList().Count>0)).ToList();
            }
            #endregion

            #region Load list parent
            List<Group> tmp = new List<Group>();
            tmp  = _iGroupService.GetList_GroupAll().ToList();
            OutLstGroup = new List<Group>();
            SortGroup(tmp, tmp.Where(_ => _.GroupParentId == 0).ToList());
            foreach (var item in OutLstGroup)
            {
                #region add blank
                string blank = "";
                for (int i = 0; i < item.GroupLevel; i++)
                {
                    if (item.GroupLevel > 0)
                        blank += "~~";
                }
                #endregion
                item.GroupName = blank + "[" + item.Description + "] - " + item.GroupName;
            }
            ViewBag.ListParentGroup = OutLstGroup;
            #endregion           
            
            #region Load action Type
            var tmp2 = _iActionTypeService.GetList_ActionTypeAll().Where(_=>_.IsActive == true && _.IsDeleted == false).ToList();
            ViewBag.lstActionType = tmp2;
            var tmp22 = _iActionTypeService.GetList_ActionTypeAll();
            #endregion
        }
        private bool ValidateGroup(Group _group)
        {
            bool result = true;
            if (_group.GroupName == null)
            { 
                 ModelState.AddModelError("GroupName", "Group Name is empty !!");
                 result = false;
            }

            if (_group.Description == null)
            {
                ModelState.AddModelError("Description", "Description Name is empty !!");
                result = false;
            }
            return result;
        }
        List<Group> OutLstGroup;
        private void SortGroup(List<Group> lstGroup,List<Group> lstParent)
        {            
            if (lstParent.Count == 0)
            {
                return;
            }           
            foreach (var item in lstParent)
	        {
                OutLstGroup.Add(item);
                List<Group> tmp = lstGroup.Where(_ => _.GroupParentId == item.GroupId).ToList();
                if (tmp.Count > 0)
                    SortGroup(lstGroup, tmp);
                
	        }
        }
        private void UpdateChildLevel(Group _group)
        { 
            GroupRepository _iGroupService = new GroupRepository();
            var tmp = _iGroupService.GetList_GroupAll().Where(_ => _.GroupParentId == _group.GroupId).ToList();
            if (tmp.Count > 0)
            {
                foreach (var item in tmp)
                {
                    item.GroupLevel = _group.GroupLevel + 1;
                    _iGroupService.UpdateGroup(item);
                    UpdateChildLevel(item);
                }
            }
        }
        public bool AddListUserToGroup(List<long> lstUserId, long GroupId, string Description)
        {
            try
            {
                GroupAccountRepository _iGroupAccountService = new GroupAccountRepository();
                List<long> lstIdUserGroup = _iGroupAccountService.GetList_GroupAccountAll().Where(_ => _.GroupId == GroupId).Select(k => (long)k.AccountId).ToList();
                foreach (var item in lstUserId)
                {
                    GroupAccount ga = new GroupAccount();
                    ga.AccountId = item;
                    ga.GroupId = GroupId;
                    ga.Description = Description;
                    ga.DateModified = DateTime.Now;
                    ga.DateCreated = DateTime.Now;
                    ga.CreatedBy = ((Account)Session["Account"]).AccountId;
                    ga.ModifiedBy = ((Account)Session["Account"]).AccountId;
                    ga.IsDeleted = false;
                    if (!_iGroupAccountService.CheckExists(item, GroupId))
                    {
                        _iGroupAccountService.InsertGroupAccount(ga);
                    }
                    else
                    {
                        lstIdUserGroup.Remove(item);
                    }
                }
                foreach (var item in lstIdUserGroup)
                {
                    _iGroupAccountService.DeleteGroupAccount(GroupId,item);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                return false;
            }

        }
        public bool AddAcctionTypeToGroup(List<long> lstActionTypeId, long GroupId, string Description)
        {
            try
            {
                ActionTypePermissionRepository _iActionTypePermissionService = new ActionTypePermissionRepository();
                List<long> lstIdUserGroup = _iActionTypePermissionService.GetList_ActionTypePermissionAll().Where(_ => _.GroupId == GroupId).Select(k => (long)k.ActionTypeId).ToList();
                foreach (var item in lstActionTypeId)
                {
                    ActionTypePermission ga = new ActionTypePermission();
                    ga.ActionTypeId = item;
                    ga.GroupId = GroupId;
                    ga.DateModified = DateTime.Now;
                    ga.DateCreated = DateTime.Now;
                    ga.CreatedBy = ((Account)Session["Account"]).AccountId;
                    ga.ModifiedBy = ((Account)Session["Account"]).AccountId;
                    ga.IsDeleted = false;
                    if (!_iActionTypePermissionService.CheckExists(item, GroupId))
                    {
                        _iActionTypePermissionService.Insert(ga);
                    }
                    else
                    {
                        lstIdUserGroup.Remove(item);
                    }
                }
                foreach (var item in lstIdUserGroup)
                {
                    _iActionTypePermissionService.DeleteActionTypePermission(GroupId, item);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.Message.ToString());
                return false;
            }

        }
        //public ActionResult Delete()
        //{
        //    return View();
        //}
    }
}
