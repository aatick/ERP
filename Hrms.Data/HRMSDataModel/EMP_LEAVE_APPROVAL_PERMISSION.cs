namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_LEAVE_APPROVAL_PERMISSION
    {
        [Key]
        public int PERMISSION_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal EMP_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal DEPT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ENABLE_DISABLE_STATUS { get; set; }
    }
}
