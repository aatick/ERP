namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetSecurityModule")]
    public partial class AspNetSecurityModule
    {
        public int AspNetSecurityModuleId { get; set; }

        [Required]
        [StringLength(10)]
        public string SecurityModuleCode { get; set; }

        [Required]
        [StringLength(100)]
        public string LinkText { get; set; }

        [StringLength(100)]
        public string ControllerName { get; set; }

        [StringLength(100)]
        public string ActionName { get; set; }

        public int? ParentModuleId { get; set; }

        public int? RoleId { get; set; }
        public string ProjectShortName { get; set; }
        public bool? IsActive { get; set; }

        public bool? IsMenuItem { get; set; }

        public int? DisplayOrder { get; set; }
        public int MenuLevel { get; set; }
        public string AreaName { get; set; }
    }
}
