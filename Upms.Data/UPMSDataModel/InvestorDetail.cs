namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

     [Table("InvestorDetails")]
    public partial class InvestorDetail
    {
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public InvestorDetail()
        //{
        //    InvestorAccounts = new HashSet<InvestorAccount>();
        //    InvestorBrokerageRateHistories = new HashSet<InvestorBrokerageRateHistory>();
        //    InvestorCompanies = new HashSet<InvestorCompany>();
        //    InvestorNominees = new HashSet<InvestorNominee>();
        //    InvestorInterestRateHistories = new HashSet<InvestorInterestRateHistory>();
        //    InvestorIntroducers = new HashSet<InvestorIntroducer>();
        //    InvestorIntroducers1 = new HashSet<InvestorIntroducer>();
        //    InvestorJointHolders = new HashSet<InvestorJointHolder>();
        //    InvestorPowerOfAttorneyMappings = new HashSet<InvestorPowerOfAttorneyMapping>();
        //}
       
        public int Id { get; set; }

        public int? OccupationId { get; set; }

        public int? CountryId { get; set; }

        public int? PresentThanaId { get; set; }

        public int? PermanentThanaId { get; set; }

        [Required]
        [StringLength(10)]
        public string InvestorCode { get; set; }

        [StringLength(10)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string InvestorName { get; set; }

        [StringLength(100)]
        public string FatherName { get; set; }

        [StringLength(100)]
        public string MotherName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(300)]
        public string PresentAddress { get; set; }

        [StringLength(300)]
        public string PermanentAddress { get; set; }

        public bool IsResident { get; set; }

        [StringLength(100)]
        public string PhoneRes { get; set; }

        [StringLength(100)]
        public string PhoneOffice { get; set; }

        [StringLength(100)]
        public string Mobile { get; set; }

        [StringLength(15)]
        public string SMSMobile { get; set; }

        [StringLength(15)]
        public string EmergencyContactNo { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string SystemEmail { get; set; }

        [StringLength(100)]
        public string Fax { get; set; }

        [StringLength(25)]
        public string NationalId { get; set; }

        [StringLength(20)]
        public string ETIN { get; set; }

        [StringLength(25)]
        public string DrivingLicenseNo { get; set; }

        [StringLength(25)]
        public string BirthCertificateNo { get; set; }

        public bool HasPassport { get; set; }

        [StringLength(20)]
        public string PassportNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PassportIssueDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PassportExpireDate { get; set; }

        [StringLength(20)]
        public string PassportIssuePlace { get; set; }

        [StringLength(300)]
        public string SpecialInstruction { get; set; }

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
        public int? IsCompany { get; set; }
       

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorAccount> InvestorAccounts { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorBrokerageRateHistory> InvestorBrokerageRateHistories { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorCompany> InvestorCompanies { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorNominee> InvestorNominees { get; set; }

        //public virtual LookupCountry LookupCountry { get; set; }

        //public virtual LookupOccupation LookupOccupation { get; set; }

        //public virtual LookupThana LookupThana { get; set; }

        //public virtual LookupThana LookupThana1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorInterestRateHistory> InvestorInterestRateHistories { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorIntroducer> InvestorIntroducers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorIntroducer> InvestorIntroducers1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorJointHolder> InvestorJointHolders { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorPowerOfAttorneyMapping> InvestorPowerOfAttorneyMappings { get; set; }
    }
}
