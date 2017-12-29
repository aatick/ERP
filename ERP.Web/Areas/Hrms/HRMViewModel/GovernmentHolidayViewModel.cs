using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Hrms.HRMViewModel
{
    public class GovernmentHolidayViewModel
    {
        //[Display(Name = "Year")]
        //public  int HolidayYear { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        //public string StartDate { get; set; }
        //public string EndDate { get; set; }
        public string Purpose { get; set; }
        public string Remarks { get; set; }

        public IEnumerable<SelectListItem> HolidayYearList { get; set; }

    }
}