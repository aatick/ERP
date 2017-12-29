namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_JOB_TYPE
    {
        
        [Key]
        public int job_id { get; set; }

        [StringLength(15)]
        public string job_office_code { get; set; }

        [Required]
        [StringLength(75)]
        public string job_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal job_reportorder { get; set; }

        [Required]
        [StringLength(75)]
        public string job_report_caption { get; set; }

        [Column(TypeName = "numeric")]
        public decimal job_enable_flag { get; set; }

        public DateTime? job_declaration_datetime { get; set; }

        public DateTime? job_effective_datetime { get; set; }

        [StringLength(20)]
        public string job_office_order_no { get; set; }

        public DateTime? job_office_order_datetime { get; set; }

        public DateTime? job_disable_datetime { get; set; }

        public DateTime entry_datetime { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_datetime { get; set; }

        public int? updatetimed_by { get; set; }

       
    }
}
