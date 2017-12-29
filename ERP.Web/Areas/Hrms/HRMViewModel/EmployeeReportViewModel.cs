using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Hrms.HRMViewModel
{
    public class EmployeeReportViewModel
    {
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Employee Id")]
        public string EmployeeCode { get; set; }
        public string EmployeeId { get; set; }
        public string DateType { get; set; }
        public string DateField { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }

        [Display(Name = "Branch")]
        public int BranchName { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Display(Name = "Department")]
        public int DepartmentName { get; set; }
        public string JobType { get; set; }
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }

        [Display(Name = "Designation")]
        public int DesignationName { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        public int ActiveStatusYesNo { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Display(Name = "Attendance Type")]
        public string AttendanceType { get; set; }

        public string EmpOfficeCode { get; set; }

        public string ReportName { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        [Display(Name = "Active Status")]
        public int EmployeeActiveStatus { get; set; }
        public IEnumerable<SelectListItem> ReportNameList { get; set; }
        public IEnumerable<SelectListItem> EmployeeActiveStatusList { get; set; }
    }
}