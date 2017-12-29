namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetRoleModule")]
    public partial class AspNetRoleModule
    {
        public int AspNetRoleModuleId { get; set; }

        [Required]
        [StringLength(128)]
        public string RoleId { get; set; }

        public int ModuleId { get; set; }

        public int SecurityLevelId { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
