namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TradeDetailsHistory")]
    public partial class TradeDetailsHistory
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int InvestorId { get; set; }

        public int TransactionTypeId { get; set; }

        public int MarketTypeId { get; set; }

        public int MarketId { get; set; }

        public int TraderId { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        public TimeSpan TransactionTime { get; set; }

        public int ShareQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalSharePrice { get; set; }

        public decimal ComissionPercentage { get; set; }

        public decimal ComissionAmount { get; set; }

        public decimal CounterPartyComissionPercentage { get; set; }

        public decimal HawlaAmount { get; set; }

        public decimal LagaAmount { get; set; }

        public decimal AITAmount { get; set; }

        [StringLength(20)]
        public string HawlaReferenceNo { get; set; }

        [StringLength(1)]
        public string HawlaType { get; set; }

        [StringLength(20)]
        public string OwnDealerId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public int? BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; } 
        public virtual BrokerTrader BrokerTrader { get; set; }

        public virtual CompanyInformation CompanyInformation { get; set; }

        public virtual InvestorDetail InvestorDetail { get; set; }

        public virtual MarketInformation MarketInformation { get; set; }

        public virtual MarketType MarketType { get; set; }

        public virtual TradeTransactionType TradeTransactionType { get; set; }
    }
}
