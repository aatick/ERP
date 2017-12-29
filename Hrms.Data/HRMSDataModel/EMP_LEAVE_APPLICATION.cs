namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_LEAVE_APPLICATION
    {
        [Key]
        public int emp_leave_application_id { get; set; }

        public int employee_id { get; set; }

        public int designation_id { get; set; }

        public int branch_id { get; set; }

        public int department_id { get; set; }

        public int leave_type_id { get; set; }

        public DateTime applied_start_date { get; set; }

        public DateTime applied_end_date { get; set; }

        public int applied_no_of_days { get; set; }

        public DateTime applied_date { get; set; }

        public DateTime? approved_start_date { get; set; }

        public DateTime? approved_end_date { get; set; }

        public int? approved_no_of_days { get; set; }

        public DateTime? approved_date { get; set; }

        [StringLength(100)]
        public string purpose_of_leave { get; set; }

        [StringLength(250)]
        public string address_during_leave { get; set; }

        [StringLength(15)]
        public string contact_no { get; set; }

        public int? hrd_authorized_emp_id { get; set; }

        public int? reliever_emp_id { get; set; }

        public int? reliever_designation_id { get; set; }

        public int? reliever_branch_id { get; set; }

        public int? recom_division_head_id { get; set; }

        public int? hrd_apprved_emp_id { get; set; }

        [StringLength(25)]
        public string sanc_adv_ref_no { get; set; }

        public DateTime? sanc_adv_date { get; set; }

        public int? sanc_adv_emp_id { get; set; }
        public int? sanc_adv_desg_id { get; set; }

        public int? sanc_adv_branch_id { get; set; }

        public int? sanc_adv_leave_type_id { get; set; }

        public DateTime? sanc_adv_effect_from { get; set; }

        public DateTime? sanc_adv_effect_to { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        public DateTime? entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }

        public int leave_status { get; set; }

        public int? division_head_approv_emp_id { get; set; }

        public int? leave_cancel_emp_id { get; set; }

        public DateTime? leave_cancel_date { get; set; }

        public byte[] medical_Certificate { get; set; }
        public byte[] medical_prescription { get; set; }



    }
}
