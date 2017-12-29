namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_ROLE
    {
       
        [Key]
        public int role_id { get; set; }

        [StringLength(10)]
        public string role_office_id { get; set; }

        [Required]
        [StringLength(75)]
        public string role_name { get; set; }

        [Required]
        [StringLength(5)]
        public string role_short_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? role_reportorder { get; set; }

        [Required]
        [StringLength(75)]
        public string role_report_caption { get; set; }

        [Column(TypeName = "numeric")]
        public decimal role_enable_flag { get; set; }

        public DateTime? role_declaration_datetime { get; set; }

        public DateTime? role_effective_datetime { get; set; }

        [StringLength(20)]
        public string role_office_order_no { get; set; }

        public DateTime? role_office_order_datetime { get; set; }

        public DateTime? role_disable_datetime { get; set; }

        public DateTime entry_datetime { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_datetime { get; set; }

        public int? updatetimed_by { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? role_type { get; set; }

        
    }
}
