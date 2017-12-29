namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_PROFILE
    {
 

        [Key]
        public int emp_id { get; set; }

        [Required]
        [StringLength(15)]
        public string emp_office_code { get; set; }

        [Required]
        [StringLength(100)]
        public string emp_name { get; set; }

        public string OfficeEmail { get; set; }
        public DateTime emp_datetimeof_birth { get; set; }

        public DateTime emp_joining_datetime { get; set; }

        public DateTime emp_confirmation_datetime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal emp_status_id { get; set; }

        public DateTime? emp_resignation_datetime { get; set; }

        public int emp_job_id { get; set; }

        public int emp_desg_id { get; set; }

        public int emp_branch_id { get; set; }

        public int? emp_dept_id { get; set; }

        public int? emp_desk_id { get; set; }

        public int? emp_role_id { get; set; }

        public int? emp_salary_acc_bank_id { get; set; }
        public int? emp_salary_acc_branch_id { get; set; }

        public string emp_salary_acc_no { get; set; }

        public DateTime? emp_status_change_datetime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? emp_increment_flag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? emp_increment_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? emp_noof_increment { get; set; }

        public int currency_id { get; set; }

        public DateTime? entry_datetime { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_datetime { get; set; }

        public int? updatetimed_by { get; set; }

        [StringLength(30)]
        public string id_card_bar_code { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? is_arrear_available { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? last_trans_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? basic_pay_scale_id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? contract_period_term { get; set; }

        public DateTime? contract_finish_datetime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? has_bond { get; set; }

        public DateTime? resignation_accept_datetime { get; set; }

        public DateTime? release_datetime { get; set; }

        [StringLength(35)]
        public string release_order_no { get; set; }

        public DateTime? file_close_datetime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? is_roster_employee { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? is_salary_stop { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? attendance_type { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? active_inactive_status { get; set; }

    }
}
