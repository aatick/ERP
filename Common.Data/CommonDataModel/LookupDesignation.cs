namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LookupDesignation")]
    public partial class LookupDesignation
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public LookupDesignation()
        //{
        //    BrokerEmployees = new HashSet<BrokerEmployee>();
        //}

        public int Id { get; set; }

        [StringLength(10)]
        public string DesignationShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string DesignationName { get; set; }
        public int JobTypeId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

       

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerEmployee> BrokerEmployees { get; set; }
    }
}
