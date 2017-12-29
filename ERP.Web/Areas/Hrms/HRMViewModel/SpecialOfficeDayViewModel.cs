using System.ComponentModel.DataAnnotations;

namespace Hrms.HRMViewModel
{
    public class SpecialOfficeDayViewModel
    {
        [Display(Name = "Year")]
        public string trans_year { get; set; }

        public string trans_date { get; set; }

        [Required]
        [StringLength(55)]
        public string trans_type { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
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
        [Display(Name = "Half Office Time")]
        public string half_OfficeTIme { get; set; }

     
       
        
        public bool IsLateCount { get; set; }
        [Display(Name = "Late Count")]
        public string LateCount { get; set; }

        public bool IsAbsentCount { get; set; }
        [Display(Name = "Absent Count")]
        public string AbsentCount { get; set; }

        public bool IsSetHoliDay { get; set; }
        [Display(Name = "Set Holiday As Office Day")]
        public string SetHoliDay { get; set; }

        public bool IsSetDayMandatory { get; set; }
        [Display(Name = "Set Saturday As Mandatory Office Day")]
        public string SetDayMandatory { get; set; }

        [StringLength(100)]
        public string Purpose { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        public int noOfHoliDAY { get; set; }

    }
}