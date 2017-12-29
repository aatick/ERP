using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class BrokerEmployeeViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public int BrokerBranchId { get; set; }
        public int? PresentThanaId { get; set; }
        public int? PermanentThanaId { get; set; }
        public string Title { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string MaritialStatus { get; set; }
        public bool? AuthRepresentative { get; set; }
        public string EmergencyContact { get; set; }
        public byte[] Photograph { get; set; }
        public byte[] Signature { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }

        public string EmployeeCode { get; set; }


        public long RowSl { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string BrokerBranchName { get; set; }
        public string PresentThanaName { get; set; }
        public string PermanentThanaName { get; set; }
        public string DateOfBirthMsg { get; set; }
        public string JoiningDateMsg { get; set; }


        public int? PreDivisionId { get; set; }
        public int? PerDivisionId { get; set; }
        public int? PreDistrictId { get; set; }
        public int? PerDistrictId { get; set; }
        public string BrokerName { get; set; }
        public string RoleName { get; set; }
        
        public HttpPostedFileBase PhotographMsg { get; set; }
        public HttpPostedFileBase SignatureMsg { get; set; }

        public IEnumerable<SelectListItem> DesignatioList { get; set; } 
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> BrokerBranchList { get; set; }
        public IEnumerable<SelectListItem> MaritalStatusList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
    }
}