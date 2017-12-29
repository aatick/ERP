namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IPOGroupMaster")]
    public partial class IPOGroupMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IPOGroupMaster()
        {
            IPOGroupDetails = new HashSet<IPOGroupDetail>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string GroupCode { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public int LeaderId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IPOGroupDetail> IPOGroupDetails { get; set; }
    }
}
