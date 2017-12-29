namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_LEAVE_OPENING_BALANCE
    {
        [Key]
        public int leave_opening_balance_id { get; set; }

        public int employee_id { get; set; }

        public int leave_type_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal leave_opening_balance { get; set; }

        public DateTime opening_date { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        public DateTime? entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? leave_availed { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OPENING_BALANCE_YEAR { get; set; }

      
    }
}
