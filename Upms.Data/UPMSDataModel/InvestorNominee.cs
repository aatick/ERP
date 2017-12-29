namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorNominee")]
    public partial class InvestorNominee
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public InvestorNominee()
        //{
        //    InvestorNomineeGuardians = new HashSet<InvestorNomineeGuardian>();
        //}

        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int PresentThanaId { get; set; }

        public int PermanentThanaId { get; set; }

        public int OccupationId { get; set; }

        public int CountryId { get; set; }

        public int RelationId { get; set; }

        [Required]
        [StringLength(100)]
        public string NomineeName { get; set; }

        [StringLength(100)]
        public string FatherName { get; set; }

        [StringLength(100)]
        public string MotherName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
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

        public decimal Percentage { get; set; }

        public bool IsMinor { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        //public virtual InvestorDetail InvestorDetail { get; set; }

        //public virtual LookupCountry LookupCountry { get; set; }

        //public virtual LookupOccupation LookupOccupation { get; set; }

        //public virtual LookupRelation LookupRelation { get; set; }

        //public virtual LookupThana LookupThana { get; set; }

        //public virtual LookupThana LookupThana1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorNomineeGuardian> InvestorNomineeGuardians { get; set; }
    }
}
