namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LookupCountry")]
    public partial class LookupCountry
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }
    }
}
