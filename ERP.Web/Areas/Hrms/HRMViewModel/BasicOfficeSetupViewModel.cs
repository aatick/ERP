using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Hrms.HRMViewModel
{
    public class BasicOfficeSetupViewModel
    {
        [Column(TypeName = "numeric")]
        [Display(Name = "Year")]
        public string trans_year { get; set; }

        public string trans_date { get; set; }

        [Required]
        [StringLength(55)]
        public string trans_type { get; set; }

        [Required]
        [StringLength(55)]
        public string trans_purpose { get; set; }
        [Display(Name = "In Time")]
        public string office_intime { get; set; }
        [Display(Name = "Out Time")]
        public string office_outtime { get; set; }
        [Display(Name = "Late Time")]
        public string late_time { get; set; }
        [Display(Name = "Absent Time")]
        public string absent_time { get; set; }
        //[Display(Name = "Half Office Time")]
        //public string half_OfficeTIme { get; set; }

        [StringLength(100)]
        [Display(Name = "Remarks")]
        public string remark { get; set; }

        public DateTime entry_datetime { get; set; }

        public int added_by { get; set; }

        public DateTime? edit_datetime { get; set; }

        public int? updatetimed_by { get; set; }

        [Display(Name = "Half Office Time")]
        public string half_office_outtime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? is_late_count { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? is_absent_count { get; set; }
        public int? noOfHoliDAY { get; set; }

        public IEnumerable<SelectListItem> trans_yearList { get; set; }


    }
}