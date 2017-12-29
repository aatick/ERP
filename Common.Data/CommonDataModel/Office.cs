namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Office")]
    public partial class Office
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string OfficeCode { get; set; }

        [Required]
        [StringLength(40)]
        public string OfficeName { get; set; }

        public byte OfficeLevel { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstLevel { get; set; }

        [StringLength(10)]
        public string SecondLevel { get; set; }

        [StringLength(10)]
        public string ThirdLevel { get; set; }

        [StringLength(10)]
        public string FourthLevel { get; set; }

        [Column(TypeName = "date")]
        public DateTime OperationStartDate { get; set; }

        [StringLength(155)]
        public string OfficeAddress { get; set; }

        [StringLength(10)]
        public string PostCode { get; set; }

        public int? GeoLocationID { get; set; }

        [StringLength(45)]
        public string Email { get; set; }

        [StringLength(35)]
        public string Phone { get; set; }

        public int OrgID { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(35)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
    }
}
