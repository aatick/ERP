namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LookupDivision")]
    public partial class LookupDivision
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public LookupDivision()
        //{
        //    LookupDistricts = new HashSet<LookupDistrict>();
        //}

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DivisionName { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LookupDistrict> LookupDistricts { get; set; }
    }
}
