namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrokerDepositoryPariticipant")]
    public partial class BrokerDepositoryPariticipant
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public BrokerDepositoryPariticipant()
        //{
        //    BrokerDPBranches = new HashSet<BrokerDPBranch>();
        //}

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string DPCode { get; set; }

        [Required]
        [StringLength(10)]
        public string BORefNo { get; set; }

        [Required]
        [StringLength(100)]
        public string DepositoryParticipantName { get; set; }
// ReSharper disable once InconsistentNaming
        public string DPShortName { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }

        [StringLength(100)]
        public string ContactNo { get; set; }

        public bool PayInOut { get; set; }

        [StringLength(20)]
        public string ClearingAccDSE { get; set; }

        [StringLength(20)]
        public string ClearingAccCSE { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

       

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerDPBranch> BrokerDPBranches { get; set; }
    }
}
