using HTTelecom.Domain.Core.DataContext.ams;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.AdminPanel.ViewModels
{
    public class ActionTypeViewModelIndex
    {
        public long systemTypeId { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public IPagedList<ActionType> ActionTypePager { get; set; }
        public IList<SystemType> ddl_SystemType { get; set; }
    }


    public class ActionTypeViewModelCreate
    {
        public ActionType ActionType { get; set; }

        public IList<SystemType> ddl_SystemType { get; set; }

    }

    public class ActionTypeViewModelEdit
    {
        public ActionType ActionType { get; set; }
        public IList<SystemType> ddl_SystemType { get; set; }
    }

    public class GetActionTypeDataPatrialView
    {
        public IPagedList<ActionType> ActionTypePager { get; set; }
    }
}