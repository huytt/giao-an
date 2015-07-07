using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.AdminPanel.Controllers
{
    public class ActionTypePermissionController : Controller
    {
        #region 1. Action Method: ChangeAllow
        //Description: // POST: /ChangeAllow/
        public JsonResult ChangeAllow(long ActionTypeId, List<long> listActionPermissionId, int sbool, int? page)
        {
            ActionTypePermissionRepository _iActionTypePermissionService = new ActionTypePermissionRepository();

            int pageNum = (page ?? 1);
            Account accOnline = (Account)Session["Account"];

            bool updateStatus = false;
            foreach (var ActionPermissionId in listActionPermissionId)
            {
                var productUpdateIsAllowed = _iActionTypePermissionService.Get_ActionTypePermissionById(ActionPermissionId);
                if (sbool == -1)
                {
                    updateStatus = _iActionTypePermissionService.UpdateIsAllowed(ActionPermissionId, (productUpdateIsAllowed.IsAllowed == true ? false : true), accOnline.AccountId);
                }
                if (sbool == 0)
                {
                    updateStatus = _iActionTypePermissionService.UpdateIsAllowed(ActionPermissionId, false, accOnline.AccountId);
                }
                if (sbool == 1)
                {
                    updateStatus = _iActionTypePermissionService.UpdateIsAllowed(ActionPermissionId, true, accOnline.AccountId);
                }
            }
            if (updateStatus == true)
            {
                var lst_ActionTypePermission = _iActionTypePermissionService.GetList_ActionTypePermissionAll_ActionTypeId(ActionTypeId, pageNum, 10);

                return Json(new { _listActionTypePermission = lst_ActionTypePermission, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
