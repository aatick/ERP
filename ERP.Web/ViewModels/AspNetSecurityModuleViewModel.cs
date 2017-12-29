using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class AspNetSecurityModuleViewModel:BaseModel
    {
        public int AspNetSecurityModuleId { get; set; }

        public string SecurityModuleCode { get; set; }

        public string linkText { get; set; }

        public string ControllerName { get; set; }

        public string Mode { get; set; }

        public int? AspNetRoleModuleId { get; set; }

        public int? SecurityLevelId { get; set; }

        public string ActionName { get; set; }

        public int? ParentModuleId { get; set; }
        public string ParentName { get; set; }

        [Display(Name="Role Name")]
        public int? RoleId { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsMenuItem { get; set; }

        public int? DisplayOrder { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}