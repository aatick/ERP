namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LookupDistrict")]
    public partial class LookupDistrict
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public LookupDistrict()
        //{
        //    LookupThanas = new HashSet<LookupThana>();
        //}

        public int Id { get; set; }

        public int DivisionId { get; set; }

        [Required]
        [StringLength(50)]
        public string DistrictName { get; set; }

        //public virtual LookupDivision LookupDivision { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LookupThana> LookupThanas { get; set; }
    }
}
