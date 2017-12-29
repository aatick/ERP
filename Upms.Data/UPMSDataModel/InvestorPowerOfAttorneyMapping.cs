namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorPowerOfAttorneyMapping")]
    public partial class InvestorPowerOfAttorneyMapping
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int PowerOfAttorneyId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidTo { get; set; }

        public bool IsValid { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        //public virtual InvestorDetail InvestorDetail { get; set; }

        //public virtual InvestorPowerOfAttorney InvestorPowerOfAttorney { get; set; }
    }
}
