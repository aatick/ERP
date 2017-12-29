namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_MARITAL_STATUS
    {
      

        [Key]
        public int marital_status_id { get; set; }

        [Required]
        [StringLength(10)]
        public string marital_status_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? marital_status_reportorder { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        public DateTime entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }

       
    }
}
