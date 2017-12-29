namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_NOMINEE_RELATION
    {
      

        [Key]
        public int nominee_relation_id { get; set; }

        [Required]
        [StringLength(55)]
        public string nominee_relation_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nominee_relation_reportorder { get; set; }

        [Column(TypeName = "numeric")]
        public decimal enable_flag { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        public DateTime entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }

       
    }
}
