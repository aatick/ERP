namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LookupBankBranch")]
    public partial class LookupBankBranch
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public LookupBankBranch()
        //{
        //    InvestorAccounts = new HashSet<InvestorAccount>();
        //}

        public int Id { get; set; }

        public int? ThanaId { get; set; }

        public int BankId { get; set; }

        [Required]
        [StringLength(300)]
        public string BranchName { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(20)]
        public string RoutingNo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

    

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorAccount> InvestorAccounts { get; set; }

       // public virtual LookupBank LookupBank { get; set; }

       // public virtual LookupThana LookupThana { get; set; }
    }
}
