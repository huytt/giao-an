using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Model
{
    public class ActionTypeModel
    {
        public ActionTypeModel()
        {
            this.ActionTypePermissions = new HashSet<ActionTypePermission>();
        }
        public long ActionTypeId { get; set; }
        [Required(ErrorMessage = "System is null")]
        public long SystemTypeId { get; set; }
        [Required(ErrorMessage = "Controller is null")]
        public string ControllerName { get; set; }
        public Nullable<long> ParentId { get; set; }
        [Required(ErrorMessage = "Action is null")]
        public string ActionTypeName { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<ActionTypePermission> ActionTypePermissions { get; set; }
        public virtual SystemType SystemType { get; set; }
    }
}
