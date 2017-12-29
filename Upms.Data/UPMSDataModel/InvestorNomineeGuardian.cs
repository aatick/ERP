namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorNomineeGuardian")]
    public partial class InvestorNomineeGuardian
    {
        public int Id { get; set; }

        public int? NomineeId { get; set; }

        public int? PresentThanaId { get; set; }

        public int? PermanentThanaId { get; set; }

        public int? OccupationId { get; set; }

        public int? CountryId { get; set; }

        public string GuardianName { get; set; }

        [StringLength(100)]
        public string FatherName { get; set; }

        [StringLength(100)]
        public string MotherName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        [StringLength(300)]
        public string PresentAddress { get; set; }

        [StringLength(300)]
        public string PermanentAddress { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Mobile { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        
        [StringLength(25)]
        public string NationalId { get; set; }

        [StringLength(25)]
        public string DrivingLicenseNo { get; set; }

        [StringLength(25)]
        public string BirthCertificateNo { get; set; }

        public bool IsResident { get; set; }

        public bool HasPassport { get; set; }

        [StringLength(20)]
        public string PassportNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PassportIssueDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PassportExpireDate { get; set; }

        [StringLength(20)]
        public string PassportIssuePlace { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photograph { get; set; }

        [Column(TypeName = "image")]
        public byte[] Signature { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        //public virtual InvestorNominee InvestorNominee { get; set; }

        //public virtual LookupCountry LookupCountry { get; set; }

        //public virtual LookupOccupation LookupOccupation { get; set; }

        //public virtual LookupThana LookupThana { get; set; }

        //public virtual LookupThana LookupThana1 { get; set; }
    }
}
