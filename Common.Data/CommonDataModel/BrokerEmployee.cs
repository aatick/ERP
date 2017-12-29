namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrokerEmployee")]
    public partial class BrokerEmployee
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public BrokerEmployee()
        //{
        //    BrokerTraders = new HashSet<BrokerTrader>();
        //    InvestorIntroducers = new HashSet<InvestorIntroducer>();
        //}

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DesignationId { get; set; }

        public int DepartmentId { get; set; }

        public int BrokerBranchId { get; set; }

        public int? PresentThanaId { get; set; }

        public int? PermanentThanaId { get; set; }

        [StringLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(300)]
        public string PresentAddress { get; set; }

        [StringLength(300)]
        public string PermanentAddress { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoiningDate { get; set; }

        [StringLength(100)]
        public string FatherName { get; set; }

        [StringLength(100)]
        public string MotherName { get; set; }

        [StringLength(100)]
        public string PhoneNo { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string MaritialStatus { get; set; }

        public bool? AuthRepresentative { get; set; }

        [StringLength(15)]
        public string EmergencyContact { get; set; }

        public string EmployeeCode { get; set; }

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

        public decimal DailyTradeTarget { get; set; }
        public decimal FundAcquisitionTargetOfYear { get; set; }
        public bool IsCalculate { get; set; }

        //public virtual BrokerBranch BrokerBranch { get; set; }

        //public virtual BrokerDepartment BrokerDepartment { get; set; }

        //public virtual LookupDesignation LookupDesignation { get; set; }

        //public virtual LookupThana LookupThana { get; set; }

        //public virtual LookupThana LookupThana1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerTrader> BrokerTraders { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorIntroducer> InvestorIntroducers { get; set; }
    }
}
