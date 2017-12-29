using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class StaffInfoViewModel : BaseModel
    {
        [Display(Name = "Staff ID")]
        public long StaffId { get; set; }

        [Display(Name="Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "Name")]
        public string StaffName { get; set; }

        [Display(Name = "Category")]
        public string StaffCategory { get; set; }
        public int OfficeId { get; set; }

       [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

         [Display(Name = "Department")]
        public string DepartmentName { get; set; }

       [Display(Name = "Designation")]
        public int DesignationId { get; set; }

        [Display(Name = "Designation")]
        public string DesignationName { get; set; }

        public long RowS { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase ImgFile { get; set; }

        [Display(Name = "Image")]
        public byte[] StaffImage { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Father's Name")]
        public string FatherName { get; set; }

        [Display(Name = "Mother's Name")]
        public string MotherName { get; set; }

        [Display(Name = "Spouse Name")]
        public string SpouseName { get; set; }

        [Display(Name = "Joining Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JoiningDate { get; set; }
        public string IsReleasedYN { get; set; }
        public bool? IsReleased { get; set; }
         [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime? ReleasedDate { get; set; }

        [Display(Name = "Present Address")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string PresentAddress { get; set; }

        [Display(Name = "Permanent Address")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string PermanentAddress { get; set; }

        [Display(Name = "National ID")]
        public string NationalId { get; set; }

        public long PersonId { get; set; }
        public string PersonName { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> StaffNameList { get; set; }
        public IEnumerable<SelectListItem> DesignationList { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
    }
}