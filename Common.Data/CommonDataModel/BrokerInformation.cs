namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrokerInformation")]
    public partial class BrokerInformation
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public BrokerInformation()
        //{
        //    BrokerBranches = new HashSet<BrokerBranch>();
        //}

        public int Id { get; set; }

        [StringLength(10)]
        public string BrokerCode { get; set; }
        [StringLength(10)]
        public string BrokerCodeCSE { get; set; }

        [StringLength(10)]
        public string BrokerShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string BrokerName { get; set; }

        [StringLength(30)]
        public string RegistrationNo { get; set; }

        public int TotalTrader { get; set; }

        public bool IsOwner { get; set; }

        public decimal FreeLimit { get; set; }

        public decimal AuthCapital { get; set; }

        public decimal PaidUpCapital { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IssueDate { get; set; }

        [StringLength(15)]
        public string DepositMode { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

       

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerBranch> BrokerBranches { get; set; }
    }
}
