namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorInterestRateHistory")]
    public partial class InvestorInterestRateHistory
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public decimal InterestRate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidTo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public virtual InvestorDetail InvestorDetail { get; set; }


        public int? BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; } 
    }
}
