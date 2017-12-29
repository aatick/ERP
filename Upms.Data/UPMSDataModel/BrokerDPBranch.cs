namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrokerDPBranch")]
    public partial class BrokerDPBranch
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public BrokerDPBranch()
        //{
        //    InvestorAccounts = new HashSet<InvestorAccount>();
        //}

        public int Id { get; set; }

        public int DepositoryParticipantId { get; set; }

        public int? ThanaId { get; set; }

        [Required]
        [StringLength(100)]
        public string DPBranchName { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Fax { get; set; }

        [StringLength(100)]
        public string ContactName { get; set; }

        [StringLength(100)]
        public string ContactMobile { get; set; }

        [StringLength(100)]
        public string ContactEmail { get; set; }

        [StringLength(100)]
        public string ContactFax { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        //public virtual BrokerDepositoryPariticipant BrokerDepositoryPariticipant { get; set; }

        //public virtual LookupThana LookupThana { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorAccount> InvestorAccounts { get; set; }
    }
}
