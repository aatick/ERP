namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorTransactionDaily")]
    public partial class InvestorTransactionDaily
    {
        public int Id { get; set; }

        public int? InvestorId { get; set; }

        public int? CompanyId { get; set; }

        public int? MarketTypeId { get; set; }

        public int? GroupId { get; set; }

        public int? MarketId { get; set; }

        public int TransactionTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        public int ShareQuantity { get; set; }

        public decimal AverageUnitPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MaturedDate { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal CommissionRate { get; set; }

        public decimal CommissionAmount { get; set; }

        public decimal CounterPartyRate { get; set; }

        public bool? IsShowInLedger { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public decimal HawlaAmount { get; set; }

        public decimal LagaAmount { get; set; }

        public decimal AITAmount { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public int? BrokerBranchId { get; set; }

        public decimal Amount { get; set; }
        public string TransactionMode { get; set; }
        public int? InvestorBranchId { get; set; }
        public bool? IsDishonour { get; set; } 

    }
}
