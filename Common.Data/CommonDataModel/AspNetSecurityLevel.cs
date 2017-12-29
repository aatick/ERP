namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetSecurityLevel")]
    public partial class AspNetSecurityLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AspNetSecurityLevelId { get; set; }

        [Required]
        [StringLength(10)]
        public string SecurityLevelCode { get; set; }

        [Required]
        [StringLength(50)]
        public string SecurityLevelName { get; set; }
    }
}
