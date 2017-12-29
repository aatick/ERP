namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MarketGroup")]
    public partial class MarketGroup
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public MarketGroup()
        //{
        //    CompanyGroupChangeHistories = new HashSet<CompanyGroupChangeHistory>();
        //    CompanyGroupChangeHistories1 = new HashSet<CompanyGroupChangeHistory>();
        //    CompanyInformations = new HashSet<CompanyInformation>();
        //}

        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(10)]
        public string GroupName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

       

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyGroupChangeHistory> CompanyGroupChangeHistories { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyGroupChangeHistory> CompanyGroupChangeHistories1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyInformation> CompanyInformations { get; set; }
    }
}
