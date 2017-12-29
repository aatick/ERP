namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_MANUAL_ATTEN_TYPE
    {
        [Key]
        public int manual_atten_type_id { get; set; }

        [Required]
        [StringLength(2)]
        public string atten_type { get; set; }

        [Required]
        [StringLength(50)]
        public string type_description { get; set; }

        [Required]
        [StringLength(50)]
        public string report_caption { get; set; }

        public DateTime entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }
    }
}
