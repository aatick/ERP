namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HR_LEAVE_RULES
    {
        [Key]
        public int leave_type_rules_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal leave_type_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal carried_forward_nextyear_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal maxnoofdays_atatime_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal maxnoofdays_atatime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal maxnoofdays_one_year { get; set; }

        [Column(TypeName = "numeric")]
        public decimal combined_with_holiday_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal carried_forward_after_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal earn_leave_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal earn_oneleave_per_noofdays { get; set; }

        [Column(TypeName = "numeric")]
        public decimal maxnodays_after_carri_forward { get; set; }

        [Column(TypeName = "numeric")]
        public decimal encashment_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal maxnoofdays_for_encashment { get; set; }

        [Column(TypeName = "numeric")]
        public decimal gender_type { get; set; }

        [Column(TypeName = "numeric")]
        public decimal totalservicelength_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal maxtimesatservicelength { get; set; }

        [Column(TypeName = "numeric")]
        public decimal interval_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal noofdays_interval { get; set; }

        [Column(TypeName = "numeric")]
        public decimal count_experience_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal minnoofdays_ata_time_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal minnoofdays_ata_time { get; set; }

        [Column(TypeName = "numeric")]
        public decimal servicelength_afterjoin_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal servicelength_after_joindays { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        [Column(TypeName = "numeric")]
        public decimal enable_flag { get; set; }

        public DateTime enable_date { get; set; }

        public DateTime? disable_date { get; set; }

        public DateTime entry_date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal added_by { get; set; }

        public DateTime? edit_date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? updated_by { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? suffixed_prefixed_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? bridge_cal_flag { get; set; }
    }
}
