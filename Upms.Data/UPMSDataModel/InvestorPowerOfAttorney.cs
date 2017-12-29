namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorPowerOfAttorney")]
    public partial class InvestorPowerOfAttorney
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public InvestorPowerOfAttorney()
        //{
        //    InvestorPowerOfAttorneyMappings = new HashSet<InvestorPowerOfAttorneyMapping>();
        //}

        public int Id { get; set; }

        public int PresentThanaId { get; set; }

        public int PermanentThanaId { get; set; }

        public int OccupationId { get; set; }

        public int CountryId { get; set; }

        [Required]
        [StringLength(100)]
        public string PowerOfAttorneyName { get; set; }
        public string PowerOfAttorneyCode { get; set; }

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

        [Required]
        [StringLength(25)]
        public string NationalId { get; set; }

        [StringLength(25)]
        public string DrivingLicenseNo { get; set; }

        [StringLength(25)]
        public string BirthCertificateNo { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photograph { get; set; }

        [Column(TypeName = "image")]
        public byte[] Signature { get; set; }

        [StringLength(300)]
        public string Remarks { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        //public virtual LookupCountry LookupCountry { get; set; }

        //public virtual LookupOccupation LookupOccupation { get; set; }

        //public virtual LookupThana LookupThana { get; set; }

        //public virtual LookupThana LookupThana1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorPowerOfAttorneyMapping> InvestorPowerOfAttorneyMappings { get; set; }
    }
}
