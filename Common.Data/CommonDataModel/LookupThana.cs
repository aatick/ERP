namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LookupThana")]
    public partial class LookupThana
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public LookupThana()
        //{
        //    BrokerBranches = new HashSet<BrokerBranch>();
        //    BrokerDPBranches = new HashSet<BrokerDPBranch>();
        //    BrokerEmployees = new HashSet<BrokerEmployee>();
        //    BrokerEmployees1 = new HashSet<BrokerEmployee>();
        //    InvestorDetails = new HashSet<InvestorDetail>();
        //    InvestorDetails1 = new HashSet<InvestorDetail>();
        //    InvestorJointHolders = new HashSet<InvestorJointHolder>();
        //    InvestorJointHolders1 = new HashSet<InvestorJointHolder>();
        //    InvestorNominees = new HashSet<InvestorNominee>();
        //    InvestorNominees1 = new HashSet<InvestorNominee>();
        //    InvestorNomineeGuardians = new HashSet<InvestorNomineeGuardian>();
        //    InvestorNomineeGuardians1 = new HashSet<InvestorNomineeGuardian>();
        //    InvestorPowerOfAttorneys = new HashSet<InvestorPowerOfAttorney>();
        //    InvestorPowerOfAttorneys1 = new HashSet<InvestorPowerOfAttorney>();
        //    LookupBankBranches = new HashSet<LookupBankBranch>();
        //}

        public int Id { get; set; }

        public int DistrictId { get; set; }

        [Required]
        [StringLength(50)]
        public string ThanaName { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerBranch> BrokerBranches { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerDPBranch> BrokerDPBranches { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerEmployee> BrokerEmployees { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerEmployee> BrokerEmployees1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorDetail> InvestorDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorDetail> InvestorDetails1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorJointHolder> InvestorJointHolders { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorJointHolder> InvestorJointHolders1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorNominee> InvestorNominees { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorNominee> InvestorNominees1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorNomineeGuardian> InvestorNomineeGuardians { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorNomineeGuardian> InvestorNomineeGuardians1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorPowerOfAttorney> InvestorPowerOfAttorneys { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorPowerOfAttorney> InvestorPowerOfAttorneys1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LookupBankBranch> LookupBankBranches { get; set; }

        //public virtual LookupDistrict LookupDistrict { get; set; }
    }
}
