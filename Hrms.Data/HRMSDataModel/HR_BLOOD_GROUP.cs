namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_BLOOD_GROUP
    {
       
        [Key]
        public int blood_group_id { get; set; }

        [Required]
        [StringLength(10)]
        public string blood_group_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? blood_group_reportorder { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        public DateTime entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }

      
    }
}
