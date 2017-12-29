namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_PERSONAL_DETAILS_PROFILE
    {
        [Key]
        public int emp_personal_details_id { get; set; }

        public int emp_id { get; set; }

        public int blood_group_id { get; set; }

        public int religion_id { get; set; }

        public int marital_status_id { get; set; }

        public int emp_country_id { get; set; }

        [Required]
        [StringLength(25)]
        public string emp_gender { get; set; }

        [Required]
        [StringLength(55)]
        public string emp_father_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal emp_father_status { get; set; }

        [Required]
        [StringLength(55)]
        public string emp_mother_name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal emp_mother_status { get; set; }

        [StringLength(55)]
        public string emp_spouse_name { get; set; }

        public DateTime? emp_spouse_dateofbirth { get; set; }

        [StringLength(255)]
        public string emp_present_address { get; set; }

        public int? emp_present_district_id { get; set; }

        [StringLength(255)]
        public string emp_permanent_address { get; set; }

        public int? emp_permanent_district_id { get; set; }

        [StringLength(15)]
        public string emp_phone_office { get; set; }

        [StringLength(15)]
        public string emp_phone_residance { get; set; }

        [StringLength(15)]
        public string emp_mobile { get; set; }

        [StringLength(35)]
        public string emp_office_mail_address { get; set; }

        [StringLength(35)]
        public string emp_personal_mail_address { get; set; }

        [StringLength(40)]
        public string emp_nation_id_no { get; set; }

        [StringLength(50)]
        public string emp_passport_no { get; set; }

        [StringLength(50)]
        public string remarks { get; set; }

        public DateTime? entry_date { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_date { get; set; }

        public int? updated_by { get; set; }

        public int? emp_present_thana_id { get; set; }

        public int? emp_permanent_thana_id { get; set; }

        [StringLength(15)]
        public string emp_spouse_contact_no { get; set; }

        [StringLength(100)]
        public string emergency_contact_person_name { get; set; }

        [StringLength(15)]
        public string emergency_contact_no { get; set; }

        public int? relation_emergency_person { get; set; }

        [Column(TypeName = "image")]
        public byte[] EMP_PICTURE_IMAGE { get; set; }

        [Column(TypeName = "image")]
        public byte[] EMP_SIGN_IMAGE { get; set; }

      
    }
}
