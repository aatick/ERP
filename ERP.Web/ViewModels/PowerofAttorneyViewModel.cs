using System;
using System.Web;

namespace ERP.Web.ViewModels
{
    public class PowerofAttorneyViewModel
    {
    
        public int Id { get; set; }

        public int PresentThanaId { get; set; }

        public int PermanentThanaId { get; set; }

        public int OccupationId { get; set; }

        public int CountryId { get; set; }
        public string PowerOfAttorneyName { get; set; }
        public string PowerOfAttorneyCode { get; set; }
        public string Title { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        
       // [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public string DrivingLicenseNo { get; set; }
        public string BirthCertificateNo { get; set; }
        public byte[] Photograph { get; set; }
        public byte[] Signature { get; set; }
        public HttpPostedFileBase PhotographMsg { get; set; }
        public HttpPostedFileBase SignatureMsg { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }  

        public int? PermanentDistrictId { get; set; }
        public int? PermanentDivisionId { get; set; }
        public int? PresentDistrictId { get; set; }
        public int? PresentDivisionId { get; set; }


       
    }
}
