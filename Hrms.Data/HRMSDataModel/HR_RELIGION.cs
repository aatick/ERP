namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_RELIGION
    {
      

        [Key]
        public int religion_id { get; set; }

        [Required]
        [StringLength(35)]
        public string religion_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? religion_reportorder { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        public DateTime entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }

       
    }
}
