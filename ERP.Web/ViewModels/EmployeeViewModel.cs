using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class EmployeeViewModel : BaseModel
    {
        public long EmployeeId { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }

        [Display(Name = "Office Name অফিসের নাম")]
        public int? OfficeId { get; set; }

        public int? NewHoId { get; set; }
        public int? NewZoneId { get; set; }
        public int? NewAreaId { get; set; }
        public int? NewBranchId { get; set; }

        //[Required(ErrorMessage = "Batch No is required")]
        [Display(Name = "Batch No (ব্যাচ নং)")]
        public string BatchNo { get; set; }

        [Display(Name = "Employee Code (কর্মকর্তা/কর্মচারী পরিচিতি নং)")]
        public string EmployeeCode { get; set; }

        [Display(Name = "Name (নাম)")]
        public string EmployeeName { get; set; }

        [Display(Name = "Date of Birth (জন্ম তারিখ)")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

         [Display(Name = "Gender (লিঙ্গ)")]
        public string Gender { get; set; }

        [Display(Name = "Marital Status (বৈ্বাহিক অবস্থা)")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Nationality (জাতীয়তা)")]
        public string Nationality { get; set; }

        [StringLength(50)]
        [Display(Name = "National ID (জাতীয় পরিচয়পত্র নং)")]
        public string NationalId { get; set; }

         [Display(Name = "Religion (ধর্ম)")]
        public string Religion { get; set; }

        [Display(Name = "Tin No (টিন নম্বর)")]
        public string TinNo { get; set; }

        [Display(Name = "Employee Image (ছবি)")]
        public byte[] EmployeeImage { get; set; }

        [Display(Name = "Gross Salary (মূল বেতন)")]
        public decimal? GrossSalary { get; set; }

        public DateTime DateOfEmployeeStatus { get; set; }

         [Display(Name = "Email (ই-মেইল)")]
        public string Email { get; set; }

        [Display(Name = "Contact Address 1 (ঠিকানা ১)")]
        public string ContactNo1 { get; set; }

        [Display(Name = "Contact Address 2 (ঠিকানা ২)")]
        public string ContactNo2 { get; set; }

        [Display(Name = "Designation (পদবী)")]
        public int DesignationId { get; set; }

        [Display(Name = "Department (বিভাগ)")]
        public int DepartmentId { get; set; }

        [Display(Name = "Joining Date (যোগদান তারিখ)")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FirstJoiningDate { get; set; }

        [Display(Name = "Status (অবস্থা)")]
        public string EmployeeStatus { get; set; }

        public string TerminationCause { get; set; }

        [Display(Name = "Rank (মর্যাদাক্রম)")]
        public string EmployeeRank { get; set; }

        [Display(Name = "Employee Image (ছবি)")]
        public HttpPostedFileBase ImgFile { get; set; }

       
        public IEnumerable<SelectListItem> RankList { get; set; }
        public IEnumerable<SelectListItem> EmployeeStatusList { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }

        public IEnumerable<SelectListItem> DesignationList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public IEnumerable<SelectListItem> MaritalList { get; set; }
        public IEnumerable<SelectListItem> ReligionList { get; set; }

        public IEnumerable<SelectListItem> HOList { get; set; }
        public IEnumerable<SelectListItem> ZoneList { get; set; }
        public IEnumerable<SelectListItem> AreaList { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }
        //Employee Education

        public long EmployeeEducationId { get; set; }
        
        [StringLength(150)]
        //[Required(ErrorMessage = "Degree Title is required")]
        [Display(Name = "Degree/Title (খেতাব)")]
        public string DegreeTitle { get; set; }

        [StringLength(250)]
        //[Required(ErrorMessage = "Concentration is required")]
        [Display(Name = "Concentration (Major/Group)(মূল বিষয়)")]
        public string Concentration { get; set; }

        [StringLength(500)]
        //[Required(ErrorMessage = "Institution Name is required")]
        [Display(Name = "Institution Name (প্রতিষ্ঠানের নাম)")]
        public string InstitutionName { get; set; }

        //[Required(ErrorMessage = "Passing Year is required")]
        [Display(Name = "Passing Year (পাসের সন)")]
        public string PassingYear { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage = "Result Type is required")]
        [Display(Name = "Result Type (ফলাফলের ধরন)")]
        public string ResultType { get; set; }

        [StringLength(11)]
        [Display(Name = "Division (বিভাগ)")]
        public string Division { get; set; }

        [StringLength(11)]
        [Display(Name = "Marks/Percentage (নম্বর/শতকরা)")]
        public string MarksPercentage { get; set; }

        [StringLength(10)]
        [Display(Name = "CGPA (সিজিপিএ)")]
        public string CGPA { get; set; }

        [StringLength(10)]
        [Display(Name = "CGPA Scale")]
        public string CGPAScale { get; set; }

        [StringLength(20)]
        [Display(Name = "Duration (স্থিতিকাল)")]
        public string Duration { get; set; }

        [StringLength(500)]
        [Display(Name = "Acheivements (অর্জন)")]
        public string Acheivements { get; set; }

        public IEnumerable<SelectListItem> ResultTypeList { get; set; }


        // Employee Address
        
        public long EmployeeAddressId { get; set; }
       
        //[Required(ErrorMessage = "Address Type is required")]
        [Display(Name = "Address Type (ঠিকানার ধরন)")]
        public string AddressType { get; set; }

        //[Required(ErrorMessage = "Country Name is required")]
        [Display(Name = "Country Name (দেশের নাম)")]
        public int CountryId { get; set; }

        //[Required(ErrorMessage = "Division Name is required")]
        [Display(Name = "State/Province Name (প্রদেশের নাম)")]
        public int StateOrProvinceId { get; set; }

        [Display(Name = "District Name (জেলার নাম)")]
        public int? DistrictId { get; set; }

        [Display(Name = "Thana Name (থানার নাম)")]
        public int? ThanaId { get; set; }

        [Display(Name = "Union Name (ইউনিয়নের নাম)")]
        public int? UnionId { get; set; }

        [Display(Name = "Street/House (সড়ক/বাড়ি নং)")]
        public string StreetOrHouse { get; set; }

        //[Required(ErrorMessage = "Zip Code is required")]
        [Display(Name = "Zip Code (পোস্ট কোড)")]
        public string ZipCode { get; set; }

        public IEnumerable<SelectListItem> AddressTypeList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> StateOrProvinceList { get; set; }
        public IEnumerable<SelectListItem> DistrictList { get; set; }
        public IEnumerable<SelectListItem> ThanaList { get; set; }
        public IEnumerable<SelectListItem> UnionList { get; set; }
        

        // Employee Reference

        public long EmployeeReferenceId { get; set; }

        //[Required(ErrorMessage = "Reference name is required")]
        [Display(Name = "Reference Name (রেফারেন্সের নাম)")]
        public string EmployeeReferenceName { get; set; }

        //[Required(ErrorMessage = "Occupation is required")]
        [Display(Name = "Occupation (পেশা)")]
        public string EmployeeReferenceOccupation { get; set; }

        //[Required(ErrorMessage = "Designation is required")]
        [Display(Name = "Designation (পদবী)")]
        public string EmployeeReferenceDesignation { get; set; }

        [Display(Name = "Relation (সম্পর্ক)")]
        public string ConnectionWithEmployee { get; set; }

        [Display(Name = "Address (ঠিকানা)")]
        public string ContactAddress { get; set; }

        [Display(Name = "Mobile (মোবাইল নম্বর)")]
        public string Mobile { get; set; }

        [Display(Name = "Telephone (টেলিফোন নম্বর)")]
        public string Telephone { get; set; }

        [Display(Name = "Fax (ফেক্স)")]
        public string Fax { get; set; }

        [Display(Name = "Email (ই-মেইল)")]
        public string RefEmail { get; set; }

        [Display(Name = "Remarks (মন্তব্য)")]
        public string Remarks { get; set; }

        
        // Employee Family Info
        public long EmployeeFamilyInfoId { get; set; }

        //[Required(ErrorMessage = "Member name is required")]
        [Display(Name = "Member Name (সদস্যের নাম)")]
        public string FamilyMemberName { get; set; }

        //[Required(ErrorMessage = "Relation is required")]
        [Display(Name = "Relation (সম্পর্ক)")]
        public string RelationWithFamilyMember { get; set; }

        //[Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender (লিঙ্গ)")]
        public string FamilyMemberGender { get; set; }

        [Display(Name = "Date of Birth (জন্ম তারিখ)")]
        //[InputMask("99/99/9999")]
        public string FamilyMemberDateOfBirth { get; set; }

        [Display(Name = "Occupation (পেশা)")]
        public string FamilyMemberOccupation { get; set; }

        public IEnumerable<SelectListItem> relationWithEmployeeList { get; set; }
        
    }
}