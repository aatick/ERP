namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IPODraft")]
    public partial class IPODraft
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int? InvestorJointId { get; set; }

        public int? IssuerBankBranchId { get; set; }
        public int? IPODeclarationId { get; set; }
        public int? CompanyId { get; set; }

        public int? CurrencyId { get; set; }

        [StringLength(20)]
        public string FDDNumber { get; set; }

        public decimal? Amount { get; set; }
        public int? InvestorShareQty { get; set; }
        public int? JoinShareQty { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DraftDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
        public int? BrokerBranchId { get; set; }
       
    }
}
