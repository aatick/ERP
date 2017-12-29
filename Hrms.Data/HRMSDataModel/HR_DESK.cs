namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_DESK
    {
       

        [Key]
        public int desk_id { get; set; }

        public int dept_id { get; set; }

        [StringLength(10)]
        public string desk_office_id { get; set; }

        [Required]
        [StringLength(75)]
        public string desk_name { get; set; }

        [Required]
        [StringLength(5)]
        public string desk_short_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? desk_reportorder { get; set; }

        [Required]
        [StringLength(75)]
        public string desk_report_caption { get; set; }

        [Column(TypeName = "numeric")]
        public decimal desk_enable_flag { get; set; }

        public DateTime? desk_declaration_datetime { get; set; }

        public DateTime? desk_effective_datetime { get; set; }

        [StringLength(20)]
        public string desk_office_order_no { get; set; }

        public DateTime? desk_office_order_datetime { get; set; }

        public DateTime? desk_disable_datetime { get; set; }

        public DateTime entry_datetime { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_datetime { get; set; }

        public int? updatetimed_by { get; set; }

       
    }
}
