namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_LEAVE_TYPE
    {
       
        [Key]
        public int leave_type_id { get; set; }

        [Required]
        [StringLength(15)]
        public string leave_type_office_id { get; set; }

        [Required]
        [StringLength(55)]
        public string leave_type_name { get; set; }

        [Required]
        [StringLength(5)]
        public string leave_type_shortname { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        public DateTime? entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }

      
    }
}
